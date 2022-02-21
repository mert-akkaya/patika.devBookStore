using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using WebApi.Validation.CreateBookValidation;

namespace WebApi.AddControllers
{

    [ApiController]
    [Route("[controller]")]
    public class BookControllers : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        public IMapper _mapper;

        public BookControllers(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            try
            {
                query.BookId = id;
                var result = query.Handle();
                return Ok(result);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }


        }

        [HttpPost]

        public IActionResult AddBook([FromBody] CreateBookModel book)
        {
            
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);

            command.Model = book;
            CreateBookCommandValidator validationRules = new CreateBookCommandValidator();
            validationRules.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }


        [HttpPut("{id}")]

        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel book)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            try
            {
                command.BookId = id;
                command.Model = book;
                command.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            try
            {
                command.BookId = id;
                command.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

            return Ok();
        }


    }
}