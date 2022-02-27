
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery{


        public int AuthorId { get; set; }
          public IMapper _mapper;
        private readonly IBookStoreDbContext _dbContext;
        public GetAuthorDetailQuery(IBookStoreDbContext dbContext ,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public AuthorDetailViewModel Handle(){
             var author = _dbContext.Authors.SingleOrDefault(a=>a.Id == AuthorId );
             if (author is null)
            {
                throw new InvalidOperationException("Author is not found");
            }
            AuthorDetailViewModel vm = _mapper.Map<AuthorDetailViewModel>(author);
            return vm;
        }
    }

    public class AuthorDetailViewModel{
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Surname { get; set; }
    }
}