using MovieManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Utils;

namespace MovieManager.Core
{
    public class ImportController
    {
        const string Filename = "movies.csv";

        /// <summary>
        /// Liefert die Movies mit den dazugehörigen Kategorien
        /// </summary>
        public static IEnumerable<Movie> ReadFromCsv()
        {
           // string path = MyFile.GetFullNameInApplicationTree(Filename);
            string[][] text = MyFile.ReadStringMatrixFromCsv(Filename, true);
            // TODO .ToList()
            var category = text.GroupBy(line => line[2]).Select(s => new Category { CategoryName = s.Key }).ToList();          
            var movies = text.Select(line => new Movie
            {
                Title = line[0],
                Category = category.SingleOrDefault(c => c.CategoryName == line[2]),
                Year = int.Parse(line[1]),
                Duration = int.Parse(line[3])
            }
            );
            return movies;
        }

    }
}
