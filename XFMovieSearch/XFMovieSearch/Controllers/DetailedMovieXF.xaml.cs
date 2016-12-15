using DM.MovieApi.MovieDb.Movies;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XFMovieSearch
{
    public partial class DetailedMovieXF : ContentPage
    {
		private MovieAPI _movieAPI;
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
            
            Movie details = await this._movieAPI.GetMovie(this._currMovie.id);
            var cast = await this._movieAPI.GetMovieCredits(details.Id);
            var castMembers = cast.CastMembers;
            if(castMembers != null)
            { getTopFiveCast(castMembers); }
            if (details != null)
            {
                description.Text = details.Overview ?? "";
                runtime.Text = details.Runtime.ToString() + " min" ?? "";
                genres.Text = this._movieAPI.getGenres(details.Genres) ?? "";
                rating.Text = "☆ " + details.VoteAverage.ToString();
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

