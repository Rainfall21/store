using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Store.Data.EF
{
    public class StoreDbContext : DbContext
    {
        public DbSet<BookDto> Books { get; set; }
        public DbSet<OrderDto> Orders { get; set; }
        public DbSet<OrderItemDto> OrderItems { get; set; }

        public StoreDbContext(DbContextOptions<StoreDbContext> options)
            :base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            BuildBooks(modelBuilder);
            BuildOrders(modelBuilder);
            BuildOrderItems(modelBuilder);
        }

        private void BuildOrderItems(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItemDto>(action =>
            {
                action.Property(dto => dto.Price)
                      .HasColumnType("money");

                action.HasOne(dto => dto.Order)
                      .WithMany(dto => dto.Items)
                      .IsRequired();
            });
        }

        private static void BuildOrders(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDto>(action =>
            {
                action.Property(dto => dto.CellPhone)
                      .HasMaxLength(20);

                action.Property(dto => dto.DeliveryUniqueCode)
                      .HasMaxLength(40);

                action.Property(dto => dto.DeliveryPrice)
                      .HasColumnType("money");

                action.Property(dto => dto.DeliveryParameters)
                      .HasConversion(value => JsonConvert.SerializeObject(value),
                                     value => JsonConvert.DeserializeObject<Dictionary<string, string>>(value))
                      .Metadata.SetValueComparer(DictionaryComparer);
                action.Property(dto => dto.PaymentServiceName)
                      .HasMaxLength(40);

                action.Property(dto => dto.PaymentParameters)
                      .HasConversion(value => JsonConvert.SerializeObject(value),
                                     value => JsonConvert.DeserializeObject<Dictionary<string, string>>(value))
                      .Metadata.SetValueComparer(DictionaryComparer);
            });
        }

        private static readonly ValueComparer DictionaryComparer = new ValueComparer<Dictionary<string, string>>(
                                (dictionary1, dictionary2) => dictionary1.SequenceEqual(dictionary2),
                                dictionary => dictionary.Aggregate(0, (a, p) =>
                                HashCode.Combine(HashCode.Combine(a, p.Key.GetHashCode()), p.Value.GetHashCode())));

        private static void BuildBooks(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookDto>(action =>
            {
                action.Property(dto => dto.Isbn)
                      .HasMaxLength(17)
                      .IsRequired();

                action.Property(dto => dto.Title)
                      .IsRequired();

                action.Property(dto => dto.Price)
                      .HasColumnType("money");

                action.HasData(
                    new BookDto
                    {
                        Id = 1,
                        Isbn = "ISBN9781451673319",
                        Author = "Ray Bradbury",
                        Title = "Fahrenheit 451",
                        Description = "Guy Montag is a fireman. His job is to destroy the most illegal of commodities, " +
                                      "the printed book, along with the houses in which they are hidden. " +
                                      "Montag never questions the destruction and ruin his actions produce, " +
                                      "returning each day to his bland life and wife, Mildred, who spends all day " +
                                      "with her television “family.” But when he meets an eccentric young neighbor, " +
                                      "Clarisse, who introduces him to a past where people didn’t live in fear and to a " +
                                      "present where one sees the world through the ideas in books instead of the mindless " +
                                      "chatter of television, Montag begins to question everything he has ever known.",
                        Price = 8.28m,
                    },
                    new BookDto
                    {
                        Id = 2,
                        Isbn = "ISBN1501192264",
                        Author = "Stephen King",
                        Title = "The Green Mile",
                        Description = "Welcome to Cold Mountain Penitentiary, home to the Depression-worn men of E Block. " +
                                      "Convicted killers all, each awaits his turn to walk “the Green Mile,” the lime-colored " +
                                      "linoleum corridor leading to a final meeting with Old Sparky, Cold Mountain’s electric " +
                                      "chair. Prison guard Paul Edgecombe has seen his share of oddities over the years " +
                                      "working the Mile, but he’s never seen anything like John Coffey—a man with the body " +
                                      "of a giant and the mind of a child, condemned for a crime terrifying in its violence " +
                                      "and shocking in its depravity. And in this place of ultimate retribution, " +
                                      "Edgecombe is about to discover the terrible, wondrous truth about John Coffey—a " +
                                      "truth that will challenge his most cherished beliefs….",
                        Price = 14.18m,
                    },
                    new BookDto
                    {
                        Id = 3,
                        Isbn = "ISBN9780316769174",
                        Author = "J. D. Salinger",
                        Title = "The Catcher in the Rye",
                        Description = "The hero-narrator of The Catcher in the Rye is an ancient child of sixteen, a native" +
                                      "New Yorker named Holden Caulfield. Through circumstances that tend to preclude adult," +
                                      "secondhand description,he leaves his prep school in Pennsylvania and goes underground " +
                                      "in New York City for three days. The boy himself is at once too simple and too complex " +
                                      "for us to make any final comment about him or his story.Perhaps the safest thing we can " +
                                      "say about Holden is that he was born in the world not just strongly attracted to beauty " +
                                      "but, almost, hopelessly impaled on it.",
                        Price = 12.78m,
                    });
            });
        }
    }
}
