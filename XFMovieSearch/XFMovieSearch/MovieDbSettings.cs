using System;
using DM.MovieApi;

namespace XFMovieSearch
{
    public class MovieDbSettings : IMovieDbSettings
    {
        public MovieDbSettings()
        {
        }

        public string ApiKey
        {
            get
            {
                return "0fc53d6debe639a3c6368629b5e43cc1";
            }
        }

        public string ApiUrl
        {
            get
            {
                return "http://api.themoviedb.org/3/";
            }
        }
    }
}