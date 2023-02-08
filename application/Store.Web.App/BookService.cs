using Store.Web.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Web.App
{
    public class BookService
    {
        private readonly IBookRepository BookRepository;
        public BookService(IBookRepository bookRepository)
        {
            this.BookRepository = bookRepository;
        }

        public async Task<BookModel> GetByIdAsync(int id)
        {
            var book = await BookRepository.GetByIdAsync(id);

            return Map(book);
        }

        public async Task<IReadOnlyCollection<BookModel>> GetAllByQueryAsync(string query)
        {
            var books = Book.IsIsbn(query)
                ? await BookRepository.GetAllByIsbnAsync(query)
                : await BookRepository.GetAllByTitleOrAuthorAsync(query);

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
