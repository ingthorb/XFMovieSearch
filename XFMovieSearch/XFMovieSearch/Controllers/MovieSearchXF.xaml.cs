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
        private MovieCredit _crew;
		private List<MovieDTO> _movieList;
        private List<MovieInfo> _moviesFound;

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
            try
            {
              this._moviesFound = await this._api.SearchForMovies(query);
            }
            catch (ArgumentNullException)
            {
                await DisplayAlert("Alert", "You have tried to get too many movies", "OK");
            }
            if (this._moviesFound == null)
            {
                this.MovieLabel.Text = "No movies found";
                return;
            }

            foreach (MovieInfo info in this._moviesFound)
			{

                try
                {
                    this._crew = await this._api.GetMovieCredits(info.Id);
                }
                catch (ArgumentNullException)
                {
                    await DisplayAlert("Alert", "You have tried to get too many movies", "OK");
                }
                string firstThree = "";
				if (this._crew != null && this._crew.CastMembers != null)
				{
					firstThree = this._api.GetTopThreeCastMembers(this._crew.CastMembers.ToList());
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
