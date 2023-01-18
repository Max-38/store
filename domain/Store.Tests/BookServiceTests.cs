using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace Store.Tests
{
    public class BookServiceTests
    {
        [Fact]
        public void GetAllByQuery_WithIsbn_CallsGetAllByIsbn()
        {
            var bookRepositoryStud = new Mock<IBookRepository>();
            bookRepositoryStud.Setup(x => x.GetAllByIsbn(It.IsAny<string>()))
                              .Returns(new[]{new Book(1, "", "", "", "", 0m)});

            bookRepositoryStud.Setup(x => x.GetAllByTitleOrAuthor(It.IsAny<string>()))
                              .Returns(new[] { new Book(2, "", "", "", "", 0m) });

            var bookService = new BookService(bookRepositoryStud.Object);
            var validIsbn = "ISBN 12345-67890";
            var actual = bookService.GetAllByQuery(validIsbn);
            
            Assert.Collection(actual, book => Assert.Equal(1, book.Id));
        }

        [Fact]
        public void GetAllByQuery_WithAuthor_CallsGetAllByTitleOrAuthor()
        {
            var bookRepositoryStud = new Mock<IBookRepository>();
            bookRepositoryStud.Setup(x => x.GetAllByIsbn(It.IsAny<string>()))
                              .Returns(new[] { new Book(1, "", "", "", "", 0m) });

            bookRepositoryStud.Setup(x => x.GetAllByTitleOrAuthor(It.IsAny<string>()))
                              .Returns(new[] { new Book(2, "", "", "", "", 0m) });

            var bookService = new BookService(bookRepositoryStud.Object);
            var invalidIsbn = "12345-67890";
            var actual = bookService.GetAllByQuery(invalidIsbn);

            Assert.Collection(actual, book => Assert.Equal(2, book.Id));
        }
    }
}
