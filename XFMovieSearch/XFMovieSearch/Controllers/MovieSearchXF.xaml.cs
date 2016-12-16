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
            searchbar.SearchCommand = new Command(() => { OnSearch(); });
        }
        

        private async void OnSearch()
        {
			this._movieList.Clear();
            if (this._moviesFound != null)
            {
                this._moviesFound.Clear();
            }
			MovieLabel.IsVisible = false;
            SearchResultView.IsVisible = false;

            if (searchbar.Text == null || searchbar.Text == "")
            {
                return;
            }

            this._indicator.IsRunning = true;
			this._indicator.IsVisible = true;
            var query = searchbar.Text;

            SearchResultView.ItemsSource = null;

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
				IsVisible = true;
                this.MovieLabel.Text = "No movies found";
                return;
            }
            else
            {
                this.MovieLabel.Text = "";
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
			this._indicator.IsVisible = false;
            BindingContext = this._movieList;
            SearchResultView.IsVisible = true;
            SearchResultView.ItemsSource = this._movieList;

        }
        private async void SearchResultView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            await Navigation.PushAsync(new DetailedMovieXF((MovieDTO)e.SelectedItem) { BindingContext = e.SelectedItem });
        }
    }
}
