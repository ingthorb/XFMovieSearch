using System;
using System.Collections.Generic;
using System.Linq;
using DM.MovieApi.MovieDb.Movies;
using Xamarin.Forms;

namespace XFMovieSearch
{
    public partial class MovieSearchXF : ContentPage
    {
        private MovieAPI _api;

		private List<MovieDTO> _movieList;

        public MovieSearchXF()
        {
            InitializeComponent();

            _api = new MovieAPI();
			_movieList = new List<MovieDTO>();

            this.Title = "Movie Search";
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
			this._movieList.Clear();

			if (MainEntry.Text == null || MainEntry.Text == "")
            {
                return;
            }

            this._indicator.IsRunning = true;

            var query = MainEntry.Text;
            var moviesFound = await this._api.SearchForMovies(query);
            if (moviesFound == null)
            {
                this.MovieLabel.Text = "No movies found";
                return;
            }

            foreach (MovieInfo info in moviesFound)
			{
				var allCrewMembers = await this._api.GetMovieCredits(info.Id);

				string firstThree = "";
				if (allCrewMembers != null && allCrewMembers.CastMembers != null)
				{
					firstThree = this._api.GetTopThreeCastMembers(allCrewMembers.CastMembers.ToList());
				}

				MovieDTO newMovie = new MovieDTO(info.Id, info.Title ?? "", firstThree ?? " ", info.PosterPath ?? "", 
				                                 info.ReleaseDate.Year.ToString() ?? "", info.BackdropPath ?? "");

				this._movieList.Add(newMovie);
			}

            this._indicator.IsRunning = false;
            
			await Navigation.PushAsync(new MovieListXF() {BindingContext = this._movieList});
        }
    }
}
