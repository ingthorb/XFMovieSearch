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

        public TopList()
        {
            InitializeComponent();
            this._movieAPI = new MovieAPI();
        }

        public async Task GetTopList()
        {
            this._indicator.IsRunning = true;
            var topMovies = await this._movieAPI.GetTopMovies();
            this._topMovies = topMovies;
            BindingContext = this._topMovies;
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
