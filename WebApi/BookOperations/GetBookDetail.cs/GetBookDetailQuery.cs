using System;
using System.Linq;
using AutoMapper;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBookDetail
{


    public class GetBookDetailQuery
    {

        public int BookId { get; set; }

        public IMapper _mapper;
        private readonly BookStoreDbContext _dbContext;
        public GetBookDetailQuery(BookStoreDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Where(item => item.Id == BookId).SingleOrDefault();
            if (book is null)
            {
                throw new InvalidOperationException("Kitap bulunamadı");
            }
             BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book);  //new BookDetailViewModel();
            // vm.Title = book.Title;
            // vm.Genre = ((GenreEnum)book.GenreId).ToString();
            // vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy");
            // vm.PageCount = book.PageCount;
            return vm;
        }
    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}