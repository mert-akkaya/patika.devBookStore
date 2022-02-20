using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBooks;
using WebApi.DBOperations;

namespace WebApi.AddControllers
{

    [ApiController]
    [Route("[controller]")]
    public class BookControllers : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public BookControllers(BookStoreDbContext context){
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBooks(){
          GetBooksQuery query = new GetBooksQuery(_context);
          var result = query.Handle();
          return Ok(result);
        }

        [HttpGet("{id}")]
        public Book GetById(int id){
            return _context.Books.Where(item=>item.Id == id).SingleOrDefault();
        }

        [HttpPost]

        public IActionResult AddBook([FromBody]CreateBookModel book){
        
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = book;
                command.Handle();
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
            return Ok();
        }


        [HttpPut("{id}")]

        public IActionResult UpdateBook(int id,[FromBody] Book book){
            var result = _context.Books.SingleOrDefault(x=>x.Id == book.Id);

            if(result is null){
                return BadRequest();
            }
            result.GenreId = book.GenreId != default ? book.GenreId : result.GenreId;
            result.PageCount = book.PageCount != default ? book.PageCount : result.PageCount;
            result.PublishDate = book.PublishDate != default ? book.PublishDate : result.PublishDate;
            result.Title = book.Title != default ? book.Title : result.Title;

            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteBook(int id){
            var book = _context.Books.FirstOrDefault(x=>x.Id == id);

            if (book is null)
            {
                return BadRequest();
            }

            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }

        
    }
}