using MovieManager.Core;
using MovieManager.Core.Entities;
using MovieManager.Persistence;
using System;
using System.Linq;

namespace MovieManager.ImportConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            InitData();
            AnalyzeData();

            Console.WriteLine();
            Console.Write("Beenden mit Eingabetaste ...");
            Console.ReadLine();
        }

        private static void InitData()
        {
            Console.WriteLine("***************************");
            Console.WriteLine("          Import");
            Console.WriteLine("***************************");

            Console.WriteLine("Import der Movies und Categories in die Datenbank");
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Console.WriteLine("Datenbank löschen");
                //TODO: Datenbank löschen
                unitOfWork.DeleteDatabase();
                Console.WriteLine("Datenbank migrieren");
                //TODO: Migrationen anstoßen
                unitOfWork.MigrateDatabase();
                Console.WriteLine("Movies/Categories werden eingelesen");

                var movies = ImportController.ReadFromCsv().ToArray();
                if (movies.Length == 0)
                {
                    Console.WriteLine("!!! Es wurden keine Movies eingelesen");
                    return;
                }

                //TODO:
                var categories = movies.GroupBy(gb => gb.Category).Select(s => s.Key);
                //TODO: Kategorien ermitteln
                Console.WriteLine($"  Es wurden {movies.Count()} Movies in {categories.Count()} Kategorien eingelesen!");

                //TODO: Movies und Kategorien in die Datenbank schreiben
                unitOfWork.MovieRepository.AddRange(movies);
                unitOfWork.CategoryRepository.AddRange(categories);
                unitOfWork.Save();

                Console.WriteLine();
            }
        }

        private static void AnalyzeData()
        {
            Console.WriteLine("***************************");
            Console.WriteLine("        Statistik");
            Console.WriteLine("***************************");


            using (UnitOfWork unitOfWork = new UnitOfWork())
            {

                // Längster Film: Bei mehreren gleichlangen Filmen, soll jener angezeigt werden, dessen Titel im Alphabet am weitesten vorne steht.
                // Die Dauer des längsten Films soll in Stunden und Minuten angezeigt werden!
                //TODO

                var longes = unitOfWork.MovieRepository.LongestMovie();
                //TODO Formatierung der Zeit in h und min
                Console.WriteLine("Längster Film: {0}; Länge {1:D2} h {2:D2} min", longes.Title, longes.Duration / 60, longes.Duration % 60);
                //TODO
                Console.WriteLine();

                // Top Kategorie:
                //   - Jene Kategorie mit den meisten Filmen.
                //TODO
                var categoryWithTheMostMovies = unitOfWork.CategoryRepository.CategorieWithTheMostMovies();
                Console.WriteLine("Kategorie mit den meisten Filmen: '{0}'; Filme {1}", categoryWithTheMostMovies.category, categoryWithTheMostMovies.movies);
                //TODO
                Console.WriteLine();

                // Jahr der Kategorie "Action":
                //  - In welchem Jahr wurden die meisten Action-Filme veröffentlicht?
                //TODO

                //TODO
                var mostAction = unitOfWork.MovieRepository.YearOfTheMostAction();
                Console.WriteLine("Jahr der Action-Filme: {0}", mostAction);
                Console.WriteLine();
                // Kategorie Auswertung (Teil 1):
                //   - Eine Liste in der je Kategorie die Anzahl der Filme und deren Gesamtdauer dargestellt wird.
                //   - Sortiert nach dem Namen der Kategorie (aufsteigend).
                //   - Die Gesamtdauer soll in Stunden und Minuten angezeigt werden!
                //TODO
                var statistic = unitOfWork.CategoryRepository.GetStatistic();
                Console.WriteLine("Kategorie Auswertung:");
                Console.WriteLine();
                Console.WriteLine("Kategorie   Anzahl   Gesamtdauer");
                Console.WriteLine("================================");
                foreach (var item in statistic)
                {
                    //TODO Formatierung
                    Console.Write("{0,-13} {1,-6} {2}", item.Categorie,item.Count, item.GetTimeForOutput());
                    Console.WriteLine();
                }
                Console.WriteLine();

                // Kategorie Auswertung (Teil 2):
                //   - Alle Kategorien und die durchschnittliche Dauer der Filme der Kategorie
                //   - Absteigend sortiert nach der durchschnittlichen Dauer der Filme.
                //     Bei gleicher Dauer dann nach dem Namen der Kategorie aufsteigend sortieren.
                //   - Die Gesamtdauer soll in Stunden, Minuten und Sekunden angezeigt werden!
                //TODO

                //TODO
                var avgStatistic = unitOfWork.CategoryRepository.GetAvgStatistic();
                Console.WriteLine("Kategorie     durchschn.Gesamtdauer");
                Console.WriteLine("===================================");

                foreach (var item in avgStatistic)
                {
                    Console.Write("{0,-13} {1}", item.Categorie, item.GetTimeForOutput(true));
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }

        private static string GetDurationAsString(double minutes, bool withSeconds = true)
        {
            throw new NotImplementedException();
        }
    }
}
