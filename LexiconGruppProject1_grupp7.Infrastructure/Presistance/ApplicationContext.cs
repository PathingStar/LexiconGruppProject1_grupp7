using LexiconGruppProject1_grupp7.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiconGruppProject1_grupp7.Infrastructure.Presistance;

public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
{
    public DbSet<Story> Stories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Story>()
            .Property(e => e.Title)
            .IsRequired();

        modelBuilder.Entity<Story>()
            .Property(e => e.Content)
            .IsRequired();

        modelBuilder.Entity<Story>().HasData(
            new Story
            {
                Title = "Tilda the Turtle's Big Race",
                Content = "Tilda the turtle was slow—but she never gave up. One day, she challenged Ricky the Rabbit to a race.\r\n“Ha! You’ll never win!” laughed Ricky.\r\nThe race began. Ricky zoomed ahead but stopped for a nap under a shady tree. Tilda kept going. Step by step. Slow but steady.\r\nWhen Ricky woke up—Tilda was already at the finish line!\r\n“Never underestimate a turtle,” she said with a smile."
            },
            new Story
            {
                Title = "Olly the Octopus Learns to Paint",
                Content = "Olly the octopus loved colors but didn’t know how to paint. He tried to copy the seahorses, but he kept getting paint on his arms!\r\n“Maybe painting isn’t for me,” he sighed.\r\nThen he had an idea: why not paint with all eight arms at once?\r\nHe swirled, splashed, and danced—soon the ocean floor was filled with beautiful, colorful shapes!\r\nNow everyone comes to Olly’s Art Show every Sunday."
            },
            new Story
            {
                Title = "Benny and the Stuck Balloon",
                Content = "Benny the bird found a red balloon stuck in a tree.\r\n“It’s too high!” he chirped. “But I must help!”\r\nHe called his friends—Milly the mouse, Freddy the frog, and Lily the ladybug. Together they made a tower!\r\nMilly stood on Freddy, Freddy on Lily, and Benny flew up top—POP! The balloon floated free and everyone cheered.\r\nThey didn’t keep the balloon—but they kept the memory."
            }
            );
    }
}
