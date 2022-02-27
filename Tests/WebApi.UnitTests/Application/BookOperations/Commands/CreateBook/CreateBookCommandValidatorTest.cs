using System;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi;
using WebApi.BookOperations.Commands.CreateBook;
using WebApi.DBOperations;
using WebApi.Validation.CreateBookValidation;
using Xunit;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests:IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData("The Lord Of The Rings",100,1)]
        [InlineData("T",0,0)]
        [InlineData("",0,0)]
        [InlineData("",0,1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title,int pageCount,int genreId){
            //arrange(hazılrık)

            CreateBookCommand command = new CreateBookCommand(null,null);

            command.Model = new CreateBookModel(){
                Title = title,
                GenreId= genreId,
                PageCount= pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1),
            };


          
            //act  (çalıştırma)

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //assert (doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]

        public void WhenDateTımeEqualNowIsGiven_Validator_ShouldBeReturnError(){
            CreateBookCommand command = new CreateBookCommand(null,null);

            command.Model = new CreateBookModel(){
                Title = "Lord of the rings",
                GenreId= 1,
                PageCount= 100,
                PublishDate = DateTime.Now.Date,
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //assert (doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldBeReturnError(){
            CreateBookCommand command = new CreateBookCommand(null,null);

            command.Model = new CreateBookModel(){
                Title = "Lord of the rings",
                GenreId= 1,
                PageCount= 100,
                PublishDate = DateTime.Now.Date.AddYears(-2),
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //assert (doğrulama)
            result.Errors.Count.Should().Equals(0);
        }
        
    }
}