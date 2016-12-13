using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XFMovieSearch
{
    public partial class MainPage : ContentPage
    {
        private MovieAPI _api;
        public MainPage()
        {
            InitializeComponent();
            _api = new MovieAPI();
            this.Title = "Movie Search";
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            if(MainEntry.Text == null)
            {
                return;
            }
            this._indicator.IsRunning = true;
            var query = MainEntry.Text;
            var movieFound = await this._api.SearchForMovies(query);
            var resultTitle = movieFound.FirstOrDefault();
            this._indicator.IsRunning = false;
            
            if (movieFound == null)
            {
                this.MovieLabel.Text = "No movie found";
                return;
            }
           await Navigation.PushAsync(new MovieListXF() {BindingContext = movieFound});
        }
    }
}
