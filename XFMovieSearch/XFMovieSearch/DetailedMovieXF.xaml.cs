using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.MovieApi.MovieDb.Movies;
using Xamarin.Forms;

namespace XFMovieSearch
{
    public partial class DetailedMovieXF : ContentPage
    {
		private MovieAPI _movieAPI;
		private MovieDTO _currMovie;

		public DetailedMovieXF(MovieDTO movieInfo)
		{
			this._movieAPI = new MovieAPI();
			this._currMovie = movieInfo;
			LoadDetails();

			InitializeComponent();
		}

		private async void LoadDetails()
		{
			Movie details = await this._movieAPI.GetMovie(this._currMovie.id);

			description.Text = details.Overview;
			runtime.Text = details.Runtime.ToString() + " min";
			genres.Text = this._movieAPI.getGenres(details.Genres);
		}
    }
}
