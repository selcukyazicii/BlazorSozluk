using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Infrastucture.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infrastucture.Persistence.EntityConfigurations.EntryComment
{
    public class EntryCommentFavoriteEntityConfiguration:BaseEntityConfiguration<EntryCommentFavorite>
    {
        public override void Configure(EntityTypeBuilder<EntryCommentFavorite> builder)
        {
            base.Configure(builder);
            builder.ToTable("entrycommentfavorite", BlazorSozlukContext.DEFAULT_SCHEMA);

            builder.HasOne(x => x.EntryComments)
                .WithMany(x => x.EntryCommentFavorites)
                .HasForeignKey(x => x.EntryCommentId);

            builder.HasOne(x => x.CreatedUser)
                .WithMany(x => x.EntryCommentFavorites)
                .HasForeignKey(x => x.CreatedById);
        }
    }
}
