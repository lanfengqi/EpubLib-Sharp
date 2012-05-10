using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nl.siegmann.epublib.domain;

namespace nl.siegmann.epublib.epub
{
    public class BookProcessor
    {
        public static readonly BookProcessor IDENTITY_BOOKPROCESSOR = new BookProcessor();

        public virtual Book processBook(Book book)
        {
            return book;
        }
    }
}
