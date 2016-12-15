using DM.MovieApi.MovieDb.Movies;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using DLToolkit.Forms.Controls;

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

			FlowListView.Init();

			ListenForTap();
        }

		private void ListenForTap()
		{
			flowView.FlowItemTapped += async (sender, e) =>
			{
				if (e.Item == null)
				{
					return;
				}

				await Navigation.PushAsync(new DetailedMovieXF((MovieDTO)e.Item) { BindingContext = e.Item });
			};
		}

        public async Task GetPopularList()
        {
            this._indicator.IsRunning = true;
            var popMovies = await this._movieAPI.GetPopularMovies();
            foreach (MovieInfo info in popMovies)
            {
                var allCrewMembers = await this._movieAPI.GetMovieCredits(info.Id);

				string firstThree = "";

				if (allCrewMembers != null && allCrewMembers.CastMembers != null)
				{
					firstThree = this._movieAPI.GetTopThreeCastMembers(allCrewMembers.CastMembers.ToList());	
				}
                
                MovieDTO newMovie = new MovieDTO(info.Id, info.Title ?? "", firstThree ?? " ", info.PosterPath ?? "",
                                                 info.ReleaseDate.Year.ToString() ?? "", info.BackdropPath ?? "");

                this._movieList.Add(newMovie);
            }
            BindingContext = this._movieList;
            this._indicator.IsRunning = false;
			this._indicator.IsVisible = false;
        }

		//private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
  //      {
            
  //      }


    }
}
