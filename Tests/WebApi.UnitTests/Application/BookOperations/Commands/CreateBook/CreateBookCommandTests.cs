using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi;
using WebApi.BookOperations.Commands.CreateBook;
using WebApi.DBOperations;
using Xunit;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        
        private readonly IMapper _mapper;

        public CreateBookCommandTests(CommonTestFixture fixture){
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn(){
            //arrange(hazılrık)
            var book =  new Book {Title = "Test_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn", GenreId = 1, PageCount = 200, PublishDate = new System.DateTime(2001, 06, 12) };
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            command.Model = new CreateBookModel{Title=book.Title};
            //act & assert (çalıştırma - doğrulama)

            FluentActions.Invoking(()=>command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut");


        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated(){
            //arrange(hazılrık)
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);

            CreateBookModel model = new CreateBookModel(){
                Title = "Hobbit",
                GenreId= 1,
                PageCount= 1000,
                PublishDate = DateTime.Now.Date.AddYears(-10),
            };

           command.Model=model;
            //act

            FluentActions.Invoking(()=>command.Handle()).Invoke();

            //assert

            var book = _context.Books.SingleOrDefault(x=>x.Title == model.Title);

            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);

        }
        
    }
}