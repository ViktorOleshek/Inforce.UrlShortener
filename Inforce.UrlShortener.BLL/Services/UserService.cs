using AutoMapper;
using Inforce.UrlShortener.Abstraction.DTOs;
using Inforce.UrlShortener.Abstraction.IRepositories;
using Inforce.UrlShortener.Abstraction.IServices;
using Inforce.UrlShortener.BLL.Validation;
using Inforce.UrlShortener.Entities;

namespace Inforce.UrlShortener.BLL.Services
{
    public class UserService : BaseService<User, UserDto>, IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.userRepository = unitOfWork.UserRepository;
        }

        protected override IRepository<User> Repository { get => this.userRepository; }

        public async Task<UserDto> AuthenticateAsync(string username, string password)
        {
            var user = await this.UnitOfWork.UserRepository.GetByUsernameAsync(username);
            if (user == null || user.Password != password)
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            return this.Mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetByUsernameAsync(string username)
        {
            var user = await this.UnitOfWork.UserRepository.GetByUsernameAsync(username);
            if (user == null)
            {
                return null;
            }

            return this.Mapper.Map<UserDto>(user);
        }

        public override async Task AddAsync(UserDto model)
        {
            var existingUser = await this.UnitOfWork.UserRepository.GetByUsernameAsync(model.Login);
            if (existingUser != null)
            {
                throw new MarketException("Username is already taken.");
            }

            await base.AddAsync(model); // добавити хешування паролю
        }

        protected override void Validate(UserDto model)
        {
            if (string.IsNullOrWhiteSpace(model.Login) || string.IsNullOrWhiteSpace(model.Password))
            {
                throw new ArgumentException("Username and password are required.");
            }
        }
    }

}
