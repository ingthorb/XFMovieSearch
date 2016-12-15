using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Genres;
using DM.MovieApi.MovieDb.Movies;

namespace XFMovieSearch
{
    public class MovieAPI
    {
        private IApiMovieRequest _movieApi;

        public MovieAPI()
        {
            MovieDbFactory.RegisterSettings(new MovieDbSettings());
            this._movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
        }

        // Methods which call the API

        public async Task<List<MovieInfo>> SearchForMovies(string query)
        {
            ApiSearchResponse<MovieInfo> response = await this._movieApi.SearchByTitleAsync(query);

            return response.Results.ToList();
        }

        public async Task<MovieCredit> GetMovieCredits(int movieId)
        {
            ApiQueryResponse<MovieCredit> response = await this._movieApi.GetCreditsAsync(movieId);

            return response.Item;
        }

        public async Task<List<MovieInfo>> GetTopMovies()
        {
            
            ApiSearchResponse<MovieInfo> response = await this._movieApi.GetTopRatedAsync();

            return response.Results.ToList() ?? null;
        }


        public async Task<List<MovieInfo>> GetPopularMovies()
        {
            ApiSearchResponse<MovieInfo> response = await this._movieApi.GetPopularAsync();

			return response.Results.ToList() ?? null;
        }

        public async Task<Movie> GetMovie(int movieId)
        {
            ApiQueryResponse<Movie> response = await this._movieApi.FindByIdAsync(movieId);

            return response.Item;
        }

        // Methods which manipulate data from the API

        public string GetTopThreeCastMembers(List<MovieCastMember> members)
        {
            string res = "";
            if (members != null && members.Count > 0)
            {
                for (int i = 0; i < members.Count; i++)
                {
                    if (i == 3)
                    {
                        break;
                    }

                    res += members[i].Name;

                    if (i != members.Count - 1)
                    {
                        res += ", ";
                    }
                }
            }
            if (res.Length > 0)
            {
                res = res.Substring(0, res.Length - 2);
            }
            return res;
        }

        public string GetTopTenCastList(List<MovieCastMember> members)
        {
            string res = "";
            if (members != null && members.Count > 0)
            {
                for (int i = 0; i < members.Count; i++)
                {
                    if (i == 10)
                    {
                        break;
                    }

                    res += members[i].Name;

                    if (i != members.Count - 1)
                    {
                        res += ", ";
                    }
                }
            }
            if (res.Length > 0)
            {
                res = res.Substring(0, res.Length - 2);
            }
            return res;
        }

		public string getGenres(IReadOnlyList<Genre> genres)
		{
			string genresStr = "";
			for (var i = 0; i < genres.Count; i++)
			{
				genresStr += genres[i].Name;

				if (i < genres.Count - 1)
				{
					genresStr += ", ";
				}
			}

			return genresStr;
		}
    }
}