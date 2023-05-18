using BlazorSozluk.Api.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Domain.Models
{
    public class EntryFavorite:BaseEntity
    {
        public Guid EntryId { get; set; }   

    }
}
