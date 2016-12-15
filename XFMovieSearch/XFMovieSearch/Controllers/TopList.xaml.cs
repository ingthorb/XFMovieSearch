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
        protected List<MovieInfo> _topMovies;
        protected List<MovieDTO> _movieList;
        private MovieCredit _crew;

        public TopList()
        {
            InitializeComponent();
            this._movieAPI = new MovieAPI();
            this._movieList = new List<MovieDTO>();
        }

        public async Task GetTopList()
        {
			this._movieList.Clear();

			listview.IsVisible = false;
			this._indicator.IsVisible = true;
            this._indicator.IsRunning = true;
           
            try
            {
				this._topMovies = await this._movieAPI.GetTopMovies();
            }
            catch (ArgumentNullException)
            {
                await DisplayAlert("Alert", "You have tried to get too many movies", "OK");
            }

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
					firstThree = this._movieAPI.GetTopThreeCastMembers(this._crew.CastMembers.ToList());
				}

                MovieDTO newMovie = new MovieDTO(info.Id, info.Title ?? "", firstThree ?? " ", info.PosterPath ?? "",
                                                 info.ReleaseDate.Year.ToString() ?? "", info.BackdropPath ?? "");

                this._movieList.Add(newMovie);
            }

            BindingContext = this._movieList;
            this._indicator.IsRunning = false;
			this._indicator.IsVisible = false;
			listview.IsVisible = true;
        }
        
        private async void Listview_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

			await Navigation.PushAsync(new DetailedMovieXF((MovieDTO)e.SelectedItem) { BindingContext = e.SelectedItem });
        }

    }
}
