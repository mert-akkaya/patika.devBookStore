using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {

        public CreateAuthorModel Model{get;set;}
        private readonly BookStoreDbContext _dbContext;

        public IMapper _mapper;

        public CreateAuthorCommand(BookStoreDbContext dbContext,IMapper mapper)  
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(a => a.Surname == Model.Surname);

            if(author is not null){
                throw new InvalidOperationException("Author already exist");
            }

            author = _mapper.Map<Author>(Model);

            _dbContext.Add(author);
            _dbContext.SaveChanges();

        }
    }

    public class CreateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public DateTime Birthday { get; set; }
    }
}