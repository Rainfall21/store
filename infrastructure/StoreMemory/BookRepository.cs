using System;
using System.Collections.Generic;
using System.Linq;

namespace Store.Memory
{
    public class BookRepository : IBookRepository
    {
        private readonly Book[] books = new[]
        {
            new Book(1, "ISBN 12345-54321","G. Orwell", "1984",
                "Written more than 70 years ago, 1984 was George Orwell’s chilling prophecy about the future. " +
                "And while 1984 has come and gone, his dystopian vision of a government that will do anything" +
                " to control the narrative is timelier than ever...",7.82m),
            new Book(2, "ISBN 12345-54322","S. King", "The Green Mile","Welcome to Cold Mountain Penitentiary, " +
                "home to the Depression-worn men of E Block. Convicted killers all, each awaits his turn to walk" +
                " the Green Mile, keeping a date with Old Sparky, Cold Mountain's electric chair. " +
                "Prison guard Paul Edgecombe has seen his share of oddities in his years working the Mile. " +
                "But he's never seen anyone like John Coffey, a man with the body of a giant and the mind of a child," +
                "condemned for a crime terrifying in its violence and shocking in its depravity. " +
                "In this place of ultimate retribution, Edgecombe is about to discover the terrible, " +
                "wondrous truth about Coffey, " +
                "a truth that will challenge his most cherished beliefs and yours.", 8.46m),
            new Book(3, "ISBN 12345-54323","Ray Bradbury", "Fahrenheit 451","Guy Montag is a fireman. " +
                "In his world, where television rules and literature is on the brink of extinction, " +
                "firemen start fires rather than put them out. His job is to destroy the most illegal of commodities," +
                " the printed book, along with the houses in which they are hidden. " +
                "Montag never questions the destruction and ruin his actions produce, " +
                "returning each day to his bland life and wife, Mildred, " +
                "who spends all day with her television family. " +
                "But then he meets an eccentric young neighbor, Clarisse, " +
                "who introduces him to a past where people didn’t live in fear and to a present where one sees " +
                "the world through the ideas in books instead of the mindless chatter of television. " +
                "When Mildred attempts suicide and Clarisse suddenly disappears, " +
                "Montag begins to question everything he has ever known. He starts hiding books in his home, " +
                "and when his pilfering is discovered, the fireman has to run for his life.",9.45m),
        };

        public Book[] GetAllByIds(IEnumerable<int> bookIds)
        {
            var foundBooks = from book in books
                             join bookId in bookIds on book.Id equals bookId
                             select book;
            return foundBooks.ToArray();
        }

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

        public Book GetById(int id)
        {
            return books.Single(book => book.Id == id);
        }
    }
}
