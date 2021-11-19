﻿using System;
using System.Linq;

namespace Store.Memory
{
    public class BookRepository : IBookRepository
    {
        private readonly Book[] books = new[]
        {
            new Book(1, "Shawshank Redemption"),
            new Book(2, "The Green Mile"),
            new Book(3, "The Catcher in the Rye"),
        };

        public Book[] GettAllByTitle(string titlePart)
        {
            return books.Where(book => book.Title.Contains(titlePart))
                        .ToArray();
        }
    }
}