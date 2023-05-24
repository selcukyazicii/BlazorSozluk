﻿using BlazorSozluk.Api.Domain.Models;
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
    public class EntryCommentVoteEntityConfiguration:BaseEntityConfiguration<EntryCommentVote>
    {
        public override void Configure(EntityTypeBuilder<EntryCommentVote> builder)
        {
            base.Configure(builder);

            builder.ToTable("entrycommentvote", BlazorSozlukContext.DEFAULT_SCHEMA);

            builder.HasOne(x => x.EntryComment)
                .WithMany(x => x.EntryCommentVotes)
                .HasForeignKey(x => x.EntryCommentId);


        }
    }
}
