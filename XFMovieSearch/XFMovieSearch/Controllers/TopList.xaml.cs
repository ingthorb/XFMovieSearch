using DM.MovieApi.MovieDb.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XFMovieSearch
{
    public partial class TopList : ContentPage
    {
        private MovieAPI _movieAPI;
        protected List<MovieInfo> _topMovies;
        protected List<MovieDTO> _movieList;

        public TopList()
        {
            InitializeComponent();
            this._movieAPI = new MovieAPI();
            this._movieList = new List<MovieDTO>();
        }

        public async Task GetTopList()
        {
            this._indicator.IsRunning = true;
            var topMovies = await this._movieAPI.GetTopMovies();
            foreach (MovieInfo info in topMovies)
            {
                var allCrewMembers = await this._movieAPI.GetMovieCredits(info.Id);
                System.Diagnostics.Debug.WriteLine("Inside top list");

				string firstThree = "";

				if (allCrewMembers != null && allCrewMembers.CastMembers != null)
				{
					firstThree = this._movieAPI.GetTopThreeCastMembers(allCrewMembers.CastMembers.ToList());
				}

                MovieDTO newMovie = new MovieDTO(info.Id, info.Title ?? "", firstThree ?? " ", info.PosterPath ?? "",
                                                 info.ReleaseDate.Year.ToString() ?? "", info.BackdropPath ?? "");

                this._movieList.Add(newMovie);
            }
            this._topMovies = topMovies;
            BindingContext = this._movieList;
            this._indicator.IsRunning = false;
			this._indicator.IsVisible = false;
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
