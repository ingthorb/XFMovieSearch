using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.MovieApi.MovieDb.Movies;
using Xamarin.Forms;

namespace XFMovieSearch
{
    public partial class MovieListXF : ContentPage
    {
        public MovieListXF()
        {
            InitializeComponent();
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
