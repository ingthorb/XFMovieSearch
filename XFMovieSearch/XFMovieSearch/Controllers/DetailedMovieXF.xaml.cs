using DM.MovieApi.MovieDb.Movies;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

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
           
            var castMembers = this._credits.CastMembers;
            if(castMembers != null)
            { getTopFiveCast(castMembers); }
            if (this._details != null)
            {
                description.Text = this._details.Overview ?? "";
                runtime.Text = this._details.Runtime.ToString() + " min" ?? "";
                genres.Text = this._movieAPI.getGenres(this._details.Genres) ?? "";
                rating.Text = "☆ " + this._details.VoteAverage.ToString();
                if(this._castMembers != null)
                {
                    actors.Text = this._castMembers;
                }
            }
        }
        private void getTopFiveCast(IReadOnlyList<MovieCastMember> members)
        {
            string actors = "";

            if (members != null && members != null)
            {
                for (var i = 0; i < members.Count; i++)
                {
                    if (i == 5)
                    {
                        break;
                    }
                    actors += members[i].Name;
                    actors += "\n";
                }
                if (actors.Length > 0)
                {
                    actors = actors.Substring(0, actors.Length - 1);
                }
            }
            this._castMembers = actors;
        }

    }
}

