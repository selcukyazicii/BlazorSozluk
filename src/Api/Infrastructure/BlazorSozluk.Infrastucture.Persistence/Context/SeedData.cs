﻿using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Common.Infrastructure;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infrastucture.Persistence.Context
{
    public class SeedData
    {
        private static List<User> GetUsers()
        {
            var result = new Faker<User>("tr")
                .RuleFor(x => x.Id, x => Guid.NewGuid())
                .RuleFor(x => x.CreateDate,
                 x => x.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(x => x.FirstName, x => x.Person.FirstName)
                .RuleFor(x => x.LastName, x => x.Person.LastName)
                .RuleFor(x => x.Email, x => x.Internet.Email())
                .RuleFor(x => x.UserName, x => x.Internet.UserName())
                .RuleFor(x => x.Password, x =>PasswordEncryptor.Encrypt(x.Internet.Password()))
                .RuleFor(x => x.EmailConfirmed, x => x.PickRandom(true, false))
                .Generate(500);
            return result;
        }
        public async Task SeedAsync(IConfiguration configuration)
        {
            var dbContextBuilder = new DbContextOptionsBuilder();
            dbContextBuilder.UseSqlServer(configuration["BlazorSozlukConnectionString"]);
            var context=new BlazorSozlukContext(dbContextBuilder.Options);
            var users = GetUsers();
            var userId = users.Select(x => x.Id);
            await context.Users.AddRangeAsync(users);

            var guids=Enumerable.Range(0,150).Select(x=>Guid.NewGuid()).ToList();
            int counter = 0;
            var entries = new Faker<Entry>("tr")
                .RuleFor(x => x.Id, x=>guids[counter++])
                .RuleFor(x => x.CreateDate,
                 x => x.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(x => x.Subject, x => x.Lorem.Sentence(5, 5))
                .RuleFor(x => x.Content, x => x.Lorem.Paragraph(2))
                .RuleFor(x => x.CreatedById, x => x.PickRandom(userId))
                .Generate(150);
            await context.Entries.AddRangeAsync(entries);

            var comments = new Faker<EntryComment>("tr")
                .RuleFor(x => x.Id, x=> Guid.NewGuid())
                .RuleFor(x => x.CreateDate,
                 x => x.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(x => x.Content, x => x.Lorem.Paragraph(2))
                .RuleFor(x => x.CreatedById, x => x.PickRandom(userId))
                .RuleFor(x=>x.EntryId, x=>x.PickRandom(guids))
                .Generate(1000);
            await context.EntryComments.AddRangeAsync(comments);
            await context.SaveChangesAsync();
        }
    }
}
