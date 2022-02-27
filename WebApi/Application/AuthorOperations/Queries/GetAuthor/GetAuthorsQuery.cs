
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthor
{
    public class GetAuthorsQuery{


          public IMapper _mapper;
        private readonly IBookStoreDbContext _dbContext;
        public GetAuthorsQuery(IBookStoreDbContext dbContext ,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<AuthorsViewModel> Handle(){
             var authors = _dbContext.Authors.OrderBy(author=>author.Id).ToList<Author>();
            List<AuthorsViewModel> vm = _mapper.Map<List<AuthorsViewModel>>(authors); //new List<BooksViewModel>();
            return vm;
        }
    }

    public class AuthorsViewModel{
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Surname { get; set; }
    }
}