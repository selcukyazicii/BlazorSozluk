﻿using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Infrastucture.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepositories<User>, IUserRepository
    {
        public UserRepository(BlazorSozlukContext dbContext):base(dbContext)
        {

        }
    }
}
