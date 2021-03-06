using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommand{

        public int BookId { get; set; }
        private readonly IBookStoreDbContext _dbContext;

        public DeleteBookCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle(){
             var book = _dbContext.Books.FirstOrDefault(x => x.Id == BookId);

            if (book is null)
            {
                throw new InvalidOperationException("Kitap Bulunamad─▒");
            }

            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();

        }
    }

}