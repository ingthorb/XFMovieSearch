using DM.MovieApi.MovieDb.Movies;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using DLToolkit.Forms.Controls;
using System;

namespace XFMovieSearch
{
    public partial class PopularPageXF : ContentPage
    {
        private MovieAPI _movieAPI;
        protected List<MovieDTO> _movieList;
        private List<MovieInfo> _popular;

        public PopularPageXF()
        {
            InitializeComponent();
            this._movieAPI = new MovieAPI();
            this._movieList = new List<MovieDTO>();

			FlowListView.Init();

			ListenForTap();
        }

		private void ListenForTap()
		{
			flowView.FlowItemTapped += async (sender, e) =>
			{
				if (e.Item == null)
				{
					return;
				}

				await Navigation.PushAsync(new DetailedMovieXF((MovieDTO)e.Item) { BindingContext = e.Item });
			};
		}

        public async Task GetPopularList()
        {
			this._movieList.Clear();

            this._indicator.IsRunning = true;
            try
            {
                this._popular = await this._movieAPI.GetPopularMovies();
            }
            catch (ArgumentNullException)
            {
                await DisplayAlert("Alert", "You have tried to get too many movies", "OK");
            }
            foreach (MovieInfo info in this._popular)
            {
                var allCrewMembers = await this._movieAPI.GetMovieCredits(info.Id);

				string firstThree = "";

				if (allCrewMembers != null && allCrewMembers.CastMembers != null)
				{
					firstThree = this._movieAPI.GetTopThreeCastMembers(allCrewMembers.CastMembers.ToList());	
				}
                
                MovieDTO newMovie = new MovieDTO(info.Id, info.Title ?? "", firstThree ?? " ", info.PosterPath ?? "",
                                                 info.ReleaseDate.Year.ToString() ?? "", info.BackdropPath ?? "");

                this._movieList.Add(newMovie);
            }
            BindingContext = this._movieList;
            this._indicator.IsRunning = false;
			this._indicator.IsVisible = false;
        }

    }
}
