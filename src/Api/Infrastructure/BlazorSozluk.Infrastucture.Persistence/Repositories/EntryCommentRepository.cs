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
    public class EntryCommentRepository : GenericRepositories<EntryComment>, IEntryCommentRepository
    {
        public EntryCommentRepository(BlazorSozlukContext dbContext) : base(dbContext)
        {
        }
    }
}
