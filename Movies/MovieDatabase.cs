using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Movies
{
    /// <summary>
    /// A class representing a database of movies
    /// </summary>
    public static class MovieDatabase
    {
        private static List<Movie> movies = new List<Movie>();

        public static List<Movie> Movies {
            get
            {
                using (StreamReader file = System.IO.File.OpenText("movies.json"))
                {
                    string json = file.ReadToEnd();
                    movies = JsonConvert.DeserializeObject<List<Movie>>(json);
                }
                return movies;
            }
        }

        public static List<Movie> Search (List<Movie> movies, string name) 
        {
            List<Movie> results = new List<Movie>();

            foreach(Movie m in movies)
            { 
                foreach(Movie movie in movies)
                {
                    if (movie.Title != null && movie.Title.Contains(name, StringComparison.OrdinalIgnoreCase))
                    {
                        results.Add(movie);
                    }
                }
            }
            return results;
        }

        public static List<Movie> FilterByMPAA(List<Movie> movies, List<string> mpaa)
        {
            List<Movie> results = new List<Movie>();

            foreach(Movie movie in movies)
            {
                if (mpaa.Contains(movie.MPAA_Rating))
                {
                    results.Add(movie);
                }
            }
            return results;
        }

        public static List<Movie> FilterByMinIMDB(List<Movie> movies, float minIMDB)
        {
            List<Movie> results = new List<Movie>();

            foreach(Movie movie in movies)
            {
                if (movie.IMDB_Rating is null)
                    continue; 
                if (movie.IMDB_Rating >= minIMDB)
                {
                    results.Add(movie);
                }
            }

            return results;
        }
    }
}
