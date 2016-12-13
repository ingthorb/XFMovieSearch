using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.MovieApi.MovieDb.Movies;
using Xamarin.Forms;

namespace XFMovieSearch
{
    public partial class MainPage : ContentPage
    {
        private MovieAPI _api;

		private List<MovieDTO> _movieList;

        public MainPage()
        {
            InitializeComponent();

            _api = new MovieAPI();
			_movieList = new List<MovieDTO>();

            this.Title = "Movie Search";
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
			if (MainEntry.Text == null || MainEntry.Text == "")
            {
                return;
            }

            this._indicator.IsRunning = true;

            var query = MainEntry.Text;
            var moviesFound = await this._api.SearchForMovies(query);

			foreach (MovieInfo info in moviesFound)
			{
				var allCrewMembers = await this._api.GetMovieCredits(info.Id);

				var firstThree = this._api.GetTopThreeCastMembers(allCrewMembers.CastMembers.ToList());

				MovieDTO newMovie = new MovieDTO(info.Id, info.Title, firstThree ?? " ", info.PosterPath, 
				                                 info.ReleaseDate.Year.ToString(), info.BackdropPath);

				this._movieList.Add(newMovie);
			}

            this._indicator.IsRunning = false;
            
            if (moviesFound == null)
            {
                this.MovieLabel.Text = "No movies found";
                return;
            }

			await Navigation.PushAsync(new MovieListXF() {BindingContext = this._movieList});
        }
    }
}
