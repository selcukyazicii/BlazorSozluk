using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Infrastucture.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infrastructure.Persistence.Repositories
{
    public class EntryRepository:GenericRepositories<Entry>,IEntryRepository
    {
        public EntryRepository(BlazorSozlukContext dbContext) : base(dbContext)
        {

        }
    }
}
