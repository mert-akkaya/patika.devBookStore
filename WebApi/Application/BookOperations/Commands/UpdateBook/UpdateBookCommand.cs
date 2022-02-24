using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommand{


        public int BookId{get;set;}
        public UpdateBookModel Model{get;set;}
        private readonly BookStoreDbContext _dbContext;
        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle(){
            var result = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);

            if (result is null)
            {
                throw new InvalidOperationException("Kitap bulunamadı");
            }
            result.GenreId = Model.GenreId != default ? Model.GenreId : result.GenreId;
            result.Title = Model.Title != default ? Model.Title : result.Title;

            _dbContext.SaveChanges();
        }

    }
    public class UpdateBookModel{
        public string Title { get; set; }
        public int GenreId { get; set; }

    }
}