using System;

namespace BookLibrary
{
    class Program
    {
        static void Main()
        {
            Library library = new();

            AddEventHandlers(library);

            FillLibrary(library, SampleBooks());
            UpdateCountRandomly(library, new Tuple<uint, uint>(1, 50));

            RemoveRandomBook(library);

            Logger.Info("All is done");
            Console.ReadKey();
        }

        private static void AddEventHandlers(Library library)
        {
            library.OnAdd += (info) =>
            {
                (var book, var count) = info;

                string message = $"Book `{book.Name}` has been added. The current total equals {count}";

                Logger.Success(message);
            };

            library.OnRemove += (info) => Logger.Warn($"Book `{info.Item1.Name}` has been removed");

            library.OnCountUpdated += (info) => Logger.Success($"Count of `{info.Item1.Name}` now is {info.Item2}");

        }

        private static void FillLibrary(Library library, CreateBookDto[] books)
        {
            foreach (var b in books)
            {
                library.Add(b);
            }
        }
        private static void UpdateCountRandomly(Library library, Tuple<uint, uint> range)
        {
            var randomizer = new Random();

            int min = (int)range.Item1;
            int max = (int)range.Item2;

            foreach (var b in library.GetAll())
            {
                library.UpdateCount(b.Item1.Id, (uint)randomizer.Next(min, max));
            }
        }

        private static void RemoveRandomBook(Library library)
        {
            int total = library.GetAll().Count;
            Logger.Info($"Total {total}");

            if (total > 0)
            {
                var index = new Random().Next(0, total);
                (var book, _) = library.GetAll()[index];

                library.Remove(book.Id);
            }
        }

        private static CreateBookDto[] SampleBooks()
        {
            CreateBookDto crimeAndPinishment = new();

            crimeAndPinishment.Name = "Crime and punishment";
            crimeAndPinishment.Author = "Fyodor Dostoevskiy";
            crimeAndPinishment.PublishedAt = "1866";

            CreateBookDto financier = new();

            financier.Name = "Financier";
            financier.Author = "Theodore Dreiser";
            financier.PublishedAt = "1912";


            CreateBookDto state = new();

            state.Name = "State";
            state.Author = "Plato";
            state.PublishedAt = "360 before our era";

            CreateBookDto scaffold = new();

            scaffold.Name = "Scaffold";
            scaffold.Author = "Chingiz Aitmatov";
            scaffold.PublishedAt = "1986";

            CreateBookDto richManPoorMan = new();

            richManPoorMan.Name = "Rich Man Poor Man";
            richManPoorMan.Author = "Irwin Shaw";
            richManPoorMan.PublishedAt = "1969";

            return new [] { crimeAndPinishment, financier, richManPoorMan, state, scaffold };
        }
    }
}
