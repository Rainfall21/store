using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Data.EF.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Isbn = table.Column<string>(maxLength: 17, nullable: false),
                    Author = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CellPhone = table.Column<string>(maxLength: 20, nullable: true),
                    DeliveryUniqueCode = table.Column<string>(maxLength: 40, nullable: true),
                    DeliveryDescription = table.Column<string>(nullable: true),
                    DeliveryPrice = table.Column<decimal>(type: "money", nullable: false),
                    DeliveryParameters = table.Column<string>(nullable: true),
                    PaymentServiceName = table.Column<string>(maxLength: 40, nullable: true),
                    PaymentDescription = table.Column<string>(nullable: true),
                    PaymentParameters = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Count = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Description", "Isbn", "Price", "Title" },
                values: new object[] { 1, "Ray Bradbury", "Guy Montag is a fireman. His job is to destroy the most illegal of commodities, the printed book, along with the houses in which they are hidden. Montag never questions the destruction and ruin his actions produce, returning each day to his bland life and wife, Mildred, who spends all day with her television “family.” But when he meets an eccentric young neighbor, Clarisse, who introduces him to a past where people didn’t live in fear and to a present where one sees the world through the ideas in books instead of the mindless chatter of television, Montag begins to question everything he has ever known.", "ISBN9781451673319", 8.28m, "Fahrenheit 451" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Description", "Isbn", "Price", "Title" },
                values: new object[] { 2, "Stephen King", "Welcome to Cold Mountain Penitentiary, home to the Depression-worn men of E Block. Convicted killers all, each awaits his turn to walk “the Green Mile,” the lime-colored linoleum corridor leading to a final meeting with Old Sparky, Cold Mountain’s electric chair. Prison guard Paul Edgecombe has seen his share of oddities over the years working the Mile, but he’s never seen anything like John Coffey—a man with the body of a giant and the mind of a child, condemned for a crime terrifying in its violence and shocking in its depravity. And in this place of ultimate retribution, Edgecombe is about to discover the terrible, wondrous truth about John Coffey—a truth that will challenge his most cherished beliefs….", "ISBN1501192264", 14.18m, "The Green Mile" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Description", "Isbn", "Price", "Title" },
                values: new object[] { 3, "J. D. Salinger", "The hero-narrator of The Catcher in the Rye is an ancient child of sixteen, a nativeNew Yorker named Holden Caulfield. Through circumstances that tend to preclude adult,secondhand description,he leaves his prep school in Pennsylvania and goes underground in New York City for three days. The boy himself is at once too simple and too complex for us to make any final comment about him or his story.Perhaps the safest thing we can say about Holden is that he was born in the world not just strongly attracted to beauty but, almost, hopelessly impaled on it.", "ISBN9780316769174", 12.78m, "The Catcher in the Rye" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
