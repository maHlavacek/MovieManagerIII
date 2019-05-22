using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MovieManager.Core.Contracts;
using MovieManager.Core.DataTransferObjects;
using MovieManager.Core.Entities;

namespace MovieManager.Persistence
{
    internal class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddRange(IEnumerable<Category> categories)
        {
            _dbContext.AddRange(categories);
        }

        public (string category, int movies) CategorieWithTheMostMovies()
        {
            return _dbContext.Categories.Select(s => ValueTuple.Create(s.CategoryName, s.Movies.Count()))
                .OrderByDescending(o => o.Item2)
                .FirstOrDefault();
        }

        public IEnumerable<Statistic> GetStatistic()
        {
            //TODO .OrderBy(o => o.Categorie);
            return _dbContext.Categories.Include(m => m.Movies).Select(s => new Statistic
            {
                Categorie = s.CategoryName,
                Count = s.Movies.Count(),
                Duration = s.Movies.Sum(m => m.Duration)
            }).OrderBy(o => o.Categorie)
            .ToList();
        }

        //TODO
        public IEnumerable<Statistic> GetAvgStatistic()
        {
            return _dbContext.Categories.Select(s => new Statistic
            {
                Categorie = s.CategoryName,
                Duration = s.Movies.Average(a => a.Duration),
            }).OrderByDescending(o => o.Duration)
              .ThenBy(c => c.Categorie)
              .ToList();
        }

        public IEnumerable<CategoryForOverViewDTO> GetCategoryForOverViewDTOs()
        {
            return _dbContext.Categories.Select(c => new CategoryForOverViewDTO
            {
                CategoryName = c.CategoryName,
                Count = c.Movies.Count(),
                AVGDuration = (int)c.Movies.Average(m => m.Duration),
                Id = c.Id
                
            }
            ).ToList();
        }

        public Category GetCategoryById(int id)
        {
            return _dbContext.Categories
                   .SingleOrDefault(c => c.Id == id);
        }
    }
}