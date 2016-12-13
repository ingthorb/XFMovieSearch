using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XFMovieSearch
{
    public partial class DetailedMovieXF : ContentPage
    {
        private object selectedItem;

        public DetailedMovieXF()
        {
            InitializeComponent();
        }

        public DetailedMovieXF(object selectedItem)
        {
            this.selectedItem = selectedItem;
           // MovieLabel.Text = selectedItem.ToString();
        }
    }
}
