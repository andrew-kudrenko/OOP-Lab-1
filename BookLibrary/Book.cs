namespace BookLibrary
{
    class Book
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public string PublishedAt { get; set; }
    }

    class CreateBookDto
    {
        public string Name { get; set; }

        public string Author { get; set; }

        public string PublishedAt { get; set; }
    }
}
