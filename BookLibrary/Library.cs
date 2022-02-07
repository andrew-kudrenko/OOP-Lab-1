using System;
using System.Collections.Generic;

namespace BookLibrary
{
    using BookInfo = ValueTuple<Book, uint>;

    class Library
    {
        private readonly List<BookInfo> books = new();

        public List<BookInfo> GetAll() => books;

        public void Remove(string id)
        {
            int index = FindIndex(id);
            bool exists = index != -1;

            if (exists)
            {
                var info = books[index];

                info.Item2--;

                if (Empty(info))
                {
                    books.RemoveAt(index);
                }

                OnRemove?.Invoke(info);
            }
        }

        public Book Add(CreateBookDto from)
        {
            Book book = BookFactory.Create(from);

            var info = new BookInfo(book, 0);
            
            books.Add(info);
            OnAdd?.Invoke(info);

            return book;
        }

        public void UpdateCount(string id, uint value)
        {
            int index = FindIndex(id);

            if (index != -1)
            {
                var info = books[index];
                info.Item2 = value;

                OnCountUpdated?.Invoke(info);
            }
        }

        public delegate void InfoHandler (BookInfo info);

        public event InfoHandler OnAdd;

        public event InfoHandler OnRemove;

        public event InfoHandler OnCountUpdated;

        private int FindIndex(string id) => books.FindIndex((b) => b.Item1.Id == id);
        
        private static bool Empty(BookInfo info) => info.Item2 <= 0;
    }
}
