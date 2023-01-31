using Store.Web.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public class BookService
    {
        private readonly IBookRepository BookRepository;
        public BookService(IBookRepository bookRepository)
        {
            this.BookRepository = bookRepository;
        }

        public BookModel GetById (int id)
        {
            var book = BookRepository.GetById (id);

            return Map(book);
        }
        public IReadOnlyCollection <BookModel> GetAllByQuery (string query)
        {
            var books = Book.IsIsbn(query)
                ? BookRepository.GetAllByIsbn(query)
                : BookRepository.GetAllByTitleOrAuthor(query);

            return books.Select(Map)
                        .ToArray();
        }

        private BookModel Map(Book book)
        {
            return new BookModel
            {
                Id = book.Id,
                Isbn = book.Isbn,
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                Price = book.Price
            };
        }
    }
}
