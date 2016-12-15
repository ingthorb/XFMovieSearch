using Xamarin.Forms;

namespace XFMovieSearch
{
    public partial class MovieListXF : ContentPage
    {
        public MovieListXF()
        {
            InitializeComponent();
        }

        private async void SearchResultView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

			await Navigation.PushAsync(new DetailedMovieXF((MovieDTO)e.SelectedItem) {BindingContext = e.SelectedItem});
        }
    }
}
