using System;

namespace BookLibrary
{
    class BookFactory
    {
        private static long prevId = DateTimeOffset.Now.ToUnixTimeSeconds();

        public static Book Create(CreateBookDto from)
        {
            Book book = new();

            book.Id = GenerateId();

            book.Name = from.Name;
            book.Author = from.Author;
            book.PublishedAt = from.PublishedAt;

            return book;
        }

        private static string GenerateId() => (++prevId).ToString();
   
    }
}
