using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MovieManager.Core.Contracts;
using MovieManager.Core.Entities;

namespace MovieManager.Persistence
{

    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MovieRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddRange(IEnumerable<Movie> movies)
        {
            _dbContext.AddRange(movies);
        }

        public IEnumerable<Movie> GetMoviesByCategoryId(int id)
        {
            return _dbContext
                   .Movies
                   .Where(m => m.CategoryId == id)
                   .OrderBy(m => m.Title)
                   .ToList();
        }

        public Movie LongestMovie()
        {
           return _dbContext.Movies.OrderByDescending(m => m.Duration).ThenBy(m => m.Title).ToArray().FirstOrDefault();
        }

        public int YearOfTheMostAction()
        {
            //TODO
            return _dbContext.Movies.Where(c => c.Category.CategoryName == "Action")
                .GroupBy(gb => gb.Year).Select(s => new
                {
                    Year = s.Key,
                    Count = s.Count()
                }).OrderByDescending(o => o.Count).ToArray().First().Year;               
        }
    }
}