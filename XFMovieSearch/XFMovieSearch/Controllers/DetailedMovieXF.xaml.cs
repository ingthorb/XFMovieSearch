using DM.MovieApi.MovieDb.Movies;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;

namespace XFMovieSearch
{
    public partial class DetailedMovieXF : ContentPage
    {
		private MovieAPI _movieAPI;
        private Movie _details;
        private MovieCredit _credits; 
		private MovieDTO _currMovie;
        private string _castMembers;

        public DetailedMovieXF()
		{
			InitializeComponent();
		}

		public DetailedMovieXF(MovieDTO movieInfo)
		{
			this._movieAPI = new MovieAPI();
			this._currMovie = movieInfo;
			LoadDetails();

			InitializeComponent();
		}

        private async void LoadDetails()
        {
            try
            {
                this._details = await this._movieAPI.GetMovie(this._currMovie.id);
                this._credits = await this._movieAPI.GetMovieCredits(this._details.Id);
            }
            catch (ArgumentNullException)
            {
                await DisplayAlert("Alert", "You have tried to get too many movies", "OK");
            }
           
			var castMembers = this._credits.CastMembers.ToList();

            if (castMembers != null)
            { 
				this._castMembers = this._movieAPI.GetTopCastMembers(castMembers, 5); 
			}

            if (this._details != null)
            {
                description.Text = this._details.Overview ?? "";
                runtime.Text = this._details.Runtime.ToString() + " min" ?? "";
                genres.Text = this._movieAPI.GetGenres(this._details.Genres) ?? "";
                rating.Text = "☆ " + this._details.VoteAverage.ToString();

                if (this._castMembers != null)
                {
                    actors.Text = this._castMembers;
                }
            }
        }

    }
}

