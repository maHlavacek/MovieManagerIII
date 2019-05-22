using MovieManager.Core.DataTransferObjects;
using MovieManager.Core.Entities;
using System.Collections.Generic;

namespace MovieManager.Core.Contracts
{
    public interface ICategoryRepository
    {
        void AddRange(IEnumerable<Category> categories);

        (string category, int movies) CategorieWithTheMostMovies();

        IEnumerable<Statistic> GetStatistic();

        IEnumerable<Statistic> GetAvgStatistic();

        IEnumerable<CategoryForOverViewDTO> GetCategoryForOverViewDTOs();

        Category GetCategoryById(int id);
    }
}
