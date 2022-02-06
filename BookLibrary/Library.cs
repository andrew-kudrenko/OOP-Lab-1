using System;
using System.Collections.Generic;

namespace BookLibrary
{
    using BookInfo = ValueTuple<Book, uint>;

    class Library
    {
        public List<BookInfo> Books { get; }

        public List<BookInfo> GetAll() => Books;

        public void Remove(string id)
        {
            int index = FindIndex(id);
            bool exists = index != -1;

            if (exists)
            {
                var info = Books[index];

                info.Item2--;

                if (Empty(info))
                {
                    Books.RemoveAt(index);
                }

                OnRemove(info);
            }
        }

        public void Add(ICreateBookDto from)
        {
            Book book = BookFactory.Create(from);

            var info = new BookInfo(book, 0);
            
            Books.Add(info);
            OnAdd(info);
        }

        public void UpdateCount(string id, uint value)
        {
            int index = FindIndex(id);

            if (index != -1)
            {
                var info = Books[index];
                info.Item2 = value;
            }
        }

        public delegate void InfoHandler (BookInfo info);

        public event InfoHandler OnAdd;

        public event InfoHandler OnRemove;

        private int FindIndex(string id) => Books.FindIndex((b) => b.Item1.Id == id);
        
        private static bool Empty(BookInfo info) => info.Item2 <= 0;
    }
}
