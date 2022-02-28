using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Application.UserOperations.Commands.CreateUser;
using WebApi.DBOperations;


namespace WebApi.AddControllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserControllers : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        public IMapper _mapper;

        public IConfiguration _configuration {get;set;}

        public UserControllers(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpPost]

        public IActionResult Create([FromBody] CreateUserModel newUser)
        {
            
            CreateUserCommand command = new CreateUserCommand(_context, _mapper);

            command.Model = newUser;
            command.Handle();
            return Ok();
        }

        [HttpPost("connect/token")]

        public IActionResult CreateToken([FromBody] CreateTokenModel login)
        {
            
            CreateTokenCommand command = new CreateTokenCommand(_context, _mapper,_configuration);
 
            command.Model = login;
            var token = command.Handle();
            return Ok();
        }





    }
}