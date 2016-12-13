using System;
using DM.MovieApi.MovieDb.Movies;

namespace XFMovieSearch
{
	public class MovieDTO
	{
		private int _id;

		private string _title;

		private string _firstThreeCastMembers;

		private string _imgPath;

		private string _releaseYear;

		private string _backdropPath;

		public MovieDTO(int id, string title, string firstThreeCastMembers, string imgPath, string releaseYear,
							  string backdropPath)
		{
			this._id = id;
			this._title = title;
			this._firstThreeCastMembers = firstThreeCastMembers;
			this._imgPath = imgPath;
			this._releaseYear = releaseYear;
			this._backdropPath = backdropPath;
		}

		public int id => this._id;

		public string title => this._title;

		public string firstThreeCastMembers => this._firstThreeCastMembers;

		public string imgPath => this._imgPath;

		public string releaseYear => this._releaseYear;

		public string backdropPath => this._backdropPath;

		public override string ToString()
		{
			return string.Format("[MovieDTO: id={0}, title={1}, firstThreeCastMembers={2}, imgPath={3}, releaseYear={4}, backdropPath={5}]", id, title, firstThreeCastMembers, imgPath, releaseYear, backdropPath);
		}
	}
}