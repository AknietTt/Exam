using AutoMapper;

using Exam.Domain.Entities;
using Exam.Domain.IRepository;
using Exam.Infrastructure.DTOs;
using Exam.Options;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Infrastructure.Service {
    public class UserService {
        private IUserRepository _userRepository;
        private readonly IOptions<JwtOptions> _options;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper, IOptions<JwtOptions> options) {
            _userRepository = userRepository;
            _mapper = mapper;
            _options = options;
        }
        public async Task<UserDto> GetUserById(Guid id) {
            var userEntity = await _userRepository.FirstOrDefault(x => x.Id == id);
            if (userEntity == null)
                throw new Exception("Не найден пользователь");
            var userDto = _mapper.Map<UserEntity, UserDto>(userEntity);
            return userDto;
        }

        public async Task<string> Authorize(AuthDto authDto) {
            var user = await _userRepository.FirstOrDefault(x => x.Email == authDto.Email);
            if (user == null)
                throw new Exception("Данный пользователь не аутентифицирован в системе");
            if (BCrypt.Net.BCrypt.Verify(authDto.Password, user.Password) == false)
                throw new Exception("Данный пользователь не авторизован в системе");

            return GenerateToken(user);
        }

        public async Task Registration(RegisterDto registrationDTo) {
            var user = await _userRepository.FirstOrDefault(x => x.Email == registrationDTo.Email);
            if (user != null)
                throw new Exception("Данный пользователь уже зарегистрирован в системе");
            var userSave = _mapper.Map<UserEntity>(registrationDTo);
            userSave.Password = BCrypt.Net.BCrypt.HashPassword(userSave.Password, BCrypt.Net.BCrypt.GenerateSalt());
            await _userRepository.AddAsync(userSave);
            await _userRepository.SaveChangesAsync();
        }

        private string GenerateToken(UserEntity user) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_options.Value.Key);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Issuer = _options.Value.Issue,
                Audience = _options.Value.Audience,
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("id", user.Id.ToString()),
                new Claim("email", user.Email)
            }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
