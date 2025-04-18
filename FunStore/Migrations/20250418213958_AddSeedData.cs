using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FunStore.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO [dbo].[Products]
                   ([Title],[Type],[Author],[PageCount],[Duration],[Expiration],[Video_Author],[Video_Duration],[Price])
                VALUES
                   ('Video club membership', 0, null, null, 12, null, null, null, 45.99),
		           ('Book club membership', 0, null, null, 12, null, null, null, 34.78),
		           ('Week 1s top five viewed dances on YouTube', 1, null, null, null, null, 'BBC', '8:57', 2.44),
		           ('10 Best Places to Visit in England - Travel Video', 1, null, null, null, null, 'touropia', '14:04', 6.99),
		           ('Amazing Places to Visit in England | England Travel Guide', 1, null, null, null, null, 'Vacation Destination', '0:51', 0.78),
		           ('Clean Code: A Handbook of Agile Software Craftsmanship', 2, 'Robert C. Martin', 464, null, null, null, null, 56.82),
		           ('Introduction to Algorithms', 2, 'Thomas H. Cormen, Charles E. Leiserson, Ronald L. Rivest, Clifford Stein', 1292, null, null, null, null, 86.22),
		           ('Structure and Interpretation of Computer Programs (SICP)', 2, 'Harold Abelson, Gerald Jay Sussman, Julie Sussman', 657, null, null, null, null, 67.13);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
