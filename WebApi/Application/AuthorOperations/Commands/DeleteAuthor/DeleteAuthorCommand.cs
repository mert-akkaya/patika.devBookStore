using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {

        public int AuthorId{get;set;}
        private readonly IBookStoreDbContext _dbContext;

        public IMapper _mapper;

         public DeleteAuthorCommand(IBookStoreDbContext dbContext,IMapper mapper)  
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(a => a.Id == AuthorId);

            if(author is null){
                throw new InvalidOperationException("Author is not found");
            }
            if(_dbContext.Books.FirstOrDefault(book=>book.AuthorId == AuthorId) is not null){
                throw new InvalidOperationException("The author whose book is published cannot be deleted.");
            }

            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();

        }
    }

}