using System;
using System.Linq;

namespace Store.Memory
{
    public class BookRepository : IBookRepository
    {
        private readonly Book[] books = new[]
        {
            new Book(1, "ISBN 12345-54321","G. Orwell", "1984"),
            new Book(2, "ISBN 12345-54322","S. King", "The Green Mile"),
            new Book(3, "ISBN 12345-54323","J.D. Salinger", "Catcher in the Rye"),
        };

        public Book[] GetAllByIsbn(string isbn)
        {
            return books.Where(book => book.Isbn == isbn)
                        .ToArray();
        }

        public Book[] GetAllByTitleOrAuthor(string query)
        {
            return books.Where(book => book.Author.Contains(query)
                                     ||book.Title.Contains(query))
                        .ToArray();
        }
    }
}
