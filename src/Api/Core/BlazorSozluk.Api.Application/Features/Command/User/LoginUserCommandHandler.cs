using AutoMapper;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Command.User
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user =await userRepository.GetSingleAsync(x => x.Email == request.EmailAddress);
            if (user == null)
                throw new DatabaseValidationException("User not found!");
            var password=PasswordEncryptor.Encrypt(request.Password);
            if (user.Password != password)
                throw new DatabaseValidationException("Password is wrong!");
            if (!user.EmailConfirmed)
                throw new DatabaseValidationException("Email address not confirmed yet");
            var result = mapper.Map<LoginUserViewModel>(user);
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email.ToString()),
                new Claim(ClaimTypes.Name,user.UserName.ToString()),
                new Claim(ClaimTypes.GivenName,user.FirstName.ToString()),
                new Claim(ClaimTypes.Surname,user.LastName.ToString()),
            };
            result.Token = GenerateToken(claims);
            return result;
        }
        private string GenerateToken(Claim[]claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthConfig:Secret"]));
            var creds= new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(10);
            var token=new JwtSecurityToken(claims:claims,
                expires:expiry,
                signingCredentials: creds,
                notBefore:DateTime.Now);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
