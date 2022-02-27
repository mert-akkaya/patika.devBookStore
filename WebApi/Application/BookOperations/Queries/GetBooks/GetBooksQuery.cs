

using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.Queries.GetBooks
{
    public class GetBooksQuery{


          public IMapper _mapper;
        private readonly IBookStoreDbContext _dbContext;
        public GetBooksQuery(IBookStoreDbContext dbContext ,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<BooksViewModel> Handle(){
             var books = _dbContext.Books.Include(x=>x.Genre).OrderBy(x=>x.Id).ToList<Book>();
            List<BooksViewModel> vm = _mapper.Map<List<BooksViewModel>>(books); //new List<BooksViewModel>();
            // foreach (var book in books)
            // {
            //     vm.Add(new BooksViewModel{
            //         Title = book.Title,
            //         Genre = ((GenreEnum)book.GenreId).ToString(),
            //         PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy"),
            //         PageCount = book.PageCount
            //     });
            // }

            return vm;
        }
    }

    public class BooksViewModel{
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}