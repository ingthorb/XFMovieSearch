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
        //private List<MovieInfo> _movieFound;

        public MovieListXF(string title)
        {
            InitializeComponent();
            MainLabel.Text = title;
        }
    }
}
