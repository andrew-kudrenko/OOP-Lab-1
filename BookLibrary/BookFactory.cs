using System;

namespace BookLibrary
{
    class BookFactory
    {
        public static Book Create(ICreateBookDto from)
        {
            Book book = new();

            book.Id = GenerateId();
            

            book.Name = from.Name;
            book.Author = from.Author;
            book.PublishedAt = from.PublishedAt;

            return book;
        }

        private static string GenerateId()
        {
            return DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
        }
    }
}
