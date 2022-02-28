using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DBOperations;
using Xunit;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateAuthorCommandTest(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }


        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(_context,_mapper);
            CreateAuthorModel model = new CreateAuthorModel(){
                Name="Test_WhenValidInputsAreGiven_Author_ShouldBeCreated",
                Surname="test",
                Birthday= System.DateTime.Now.AddYears(-3),
            };
            command.Model = model;
            //acts
            FluentActions.Invoking(()=>command.Handle()).Invoke();
            //assert

            var author = _context.Authors.SingleOrDefault(x=>x.Name == model.Name);
            author.Should().NotBeNull();
            author.Name.Should().Be(model.Name);
            author.Surname.Should().Be(model.Surname);
            author.Birthday.Should().Be(model.Birthday);

            
        }
    }
}