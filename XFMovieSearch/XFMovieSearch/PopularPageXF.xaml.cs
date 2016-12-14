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
        protected List<MovieInfo> _popularMovies;
        public PopularPageXF()
        {
            InitializeComponent();
            this._movieAPI = new MovieAPI();
            System.Diagnostics.Debug.WriteLine("Testing");
        }

        public async Task GetPopularList()
        {
            this._indicator.IsRunning = true;
            var popMovies = await this._movieAPI.GetPopularMovies();
            this._popularMovies = popMovies;
            BindingContext = this._popularMovies;
            System.Diagnostics.Debug.WriteLine("*************PopMovies" + this._popularMovies);
            this._indicator.IsRunning = false;
            System.Diagnostics.Debug.WriteLine("*************GetPopulr");
        }

        private async void Listview_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            await Navigation.PushAsync(new DetailedMovieXF(e.SelectedItem));
        }
    }
}
