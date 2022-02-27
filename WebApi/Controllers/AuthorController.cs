using System;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.DBOperations;
using WebApi.Validation.CreateAuhtorValidation;

namespace WebApi.AddControllers
{

    [ApiController]
    [Route("[controller]")]
    public class AuthorControllers : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        public IMapper _mapper;

        public AuthorControllers(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
           
                query.AuthorId = id;
                var result = query.Handle();
                return Ok(result);
        }

        [HttpPost]

        public IActionResult AddAuhtor([FromBody] CreateAuthorModel author)
        {
            
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);

            command.Model = author;
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }


        [HttpPut("{id}")]

        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel author)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context,_mapper);
            
                command.AuthorId = id;
                command.Model = author;
                command.Handle();

            return Ok();
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteAuthor(int id)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context,_mapper);
            
                command.AuthorId = id;
                command.Handle();

            return Ok();
        }


    }
}