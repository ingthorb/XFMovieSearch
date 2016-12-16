using DM.MovieApi.MovieDb.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XFMovieSearch
{
    public partial class TopList : ContentPage
    {
        private MovieAPI _movieAPI;
        private List<MovieInfo> _topMovies;
        private List<MovieDTO> _movieList;
        private MovieCredit _crew;

        public TopList()
        {
            InitializeComponent();

            this._movieAPI = new MovieAPI();
            this._movieList = new List<MovieDTO>();

            GetTopList();
			ListRefreshListener();
        }

		public void ListRefreshListener()
		{
			listview.Refreshing += (sender, e) =>
			{
				GetTopList();
			};
		}

        public async void GetTopList()
        {
			// Refresh on pull works on Android if the list is cleared. On iOS, however, we must
			// overwrite the old list, but not clear it.
			if (Device.OS == TargetPlatform.Android)
			{
				this._movieList.Clear();
			}

			listview.IsVisible = false;
			listview.IsRefreshing = true;
            _indicator.IsVisible = true;
            _indicator.IsRunning = true;

            try
            {
                this._topMovies = await this._movieAPI.GetTopMovies();
            }
            catch (ArgumentNullException)
            {
                await DisplayAlert("Alert", "You have tried to get too many movies", "OK");
            }

            if (this._topMovies != null)
            {
                foreach (MovieInfo info in this._topMovies)
                {
                    try
                    {
                        this._crew = await this._movieAPI.GetMovieCredits(info.Id);
                    }
                    catch (ArgumentNullException)
                    {
                        await DisplayAlert("Alert", "You have tried to get too many movies", "OK");
                    }

                    string firstThree = "";
                    if (this._crew != null && this._crew.CastMembers != null)
                    {
                        firstThree = this._movieAPI.GetTopCastMembers(this._crew.CastMembers.ToList(), 3);
                    }

                    MovieDTO newMovie = new MovieDTO(info.Id, info.Title ?? "", firstThree ?? " ", info.PosterPath ?? "",
                                                     info.ReleaseDate.Year.ToString() ?? "", info.BackdropPath ?? "");

                    this._movieList.Add(newMovie);
                }

                BindingContext = this._movieList;

				_indicator.IsRunning = false;
				_indicator.IsVisible = false;
				listview.IsRefreshing = false;
                listview.IsVisible = true;

            }
        }
        
        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

			await Navigation.PushAsync(new DetailedMovieXF((MovieDTO)e.SelectedItem) { BindingContext = e.SelectedItem });
        }

    }
}
