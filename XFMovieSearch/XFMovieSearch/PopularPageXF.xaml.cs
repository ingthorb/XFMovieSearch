using DM.MovieApi.MovieDb.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XFMovieSearch
{
    public partial class PopularPageXF : ContentPage
    {
        private MovieAPI _movieAPI;
        protected List<MovieDTO> _movieList;

        public PopularPageXF()
        {
            InitializeComponent();
            this._movieAPI = new MovieAPI();
            this._movieList = new List<MovieDTO>();
        }
        public async Task GetPopularList()
        {
            this._indicator.IsRunning = true;
            var popMovies = await this._movieAPI.GetPopularMovies();
            foreach (MovieInfo info in popMovies)
            {
                var allCrewMembers = await this._movieAPI.GetMovieCredits(info.Id);
                System.Diagnostics.Debug.WriteLine("Inside top list");
                var firstThree = this._movieAPI.GetTopThreeCastMembers(allCrewMembers.CastMembers.ToList());

                MovieDTO newMovie = new MovieDTO(info.Id, info.Title, firstThree ?? " ", info.PosterPath,
                                                 info.ReleaseDate.Year.ToString(), info.BackdropPath);

                this._movieList.Add(newMovie);
            }
            BindingContext = this._movieList;
            this._indicator.IsRunning = false;
        }
        private async void Listview_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            await Navigation.PushAsync(new DetailedMovieXF());
        }
    }
}
