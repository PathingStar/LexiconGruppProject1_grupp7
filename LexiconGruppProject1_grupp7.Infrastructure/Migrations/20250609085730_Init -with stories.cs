using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LexiconGruppProject1_grupp7.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initwithstories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Stories",
                columns: new[] { "Id", "Content", "Title" },
                values: new object[,]
                {
                    { 1, "Tilda the turtle was slow—but she never gave up. One day, she challenged Ricky the Rabbit to a race.\r\n“Ha! You’ll never win!” laughed Ricky.\r\nThe race began. Ricky zoomed ahead but stopped for a nap under a shady tree. Tilda kept going. Step by step. Slow but steady.\r\nWhen Ricky woke up—Tilda was already at the finish line!\r\n“Never underestimate a turtle,” she said with a smile.", "Tilda the Turtle's Big Race" },
                    { 2, "Olly the octopus loved colors but didn’t know how to paint. He tried to copy the seahorses, but he kept getting paint on his arms!\r\n“Maybe painting isn’t for me,” he sighed.\r\nThen he had an idea: why not paint with all eight arms at once?\r\nHe swirled, splashed, and danced—soon the ocean floor was filled with beautiful, colorful shapes!\r\nNow everyone comes to Olly’s Art Show every Sunday.", "Olly the Octopus Learns to Paint" },
                    { 3, "Benny the bird found a red balloon stuck in a tree.\r\n“It’s too high!” he chirped. “But I must help!”\r\nHe called his friends—Milly the mouse, Freddy the frog, and Lily the ladybug. Together they made a tower!\r\nMilly stood on Freddy, Freddy on Lily, and Benny flew up top—POP! The balloon floated free and everyone cheered.\r\nThey didn’t keep the balloon—but they kept the memory.", "Benny and the Stuck Balloon" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stories");
        }
    }
}
