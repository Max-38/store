using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Store.Data.EF.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Isbn = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CellPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DeliveryUniqueCode = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DeliveryDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryPrice = table.Column<decimal>(type: "money", nullable: false),
                    DeliveryParameters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentParameters = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Description", "Isbn", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "D. Knuth", "Первый том серии книг Искусство программирования начинается с описания основных понятий и методов программирования. Затем автор сосредотачивается на рассмотрении информационных структур - представлении информации внутри компьютера, структурных связях между элементами данных и о способам эффективной работы с ними. Для методов имитации, символьных вычислений, числовых методов, методов разработки программного обеспечения даны примеры элементарных приложений. По сравнению с предыдущим изданием, добавлены десятки простых, но в то же время очень важных алгоритмов. В соответствии с современными направлениями исследований был существенно переработан также раздел математического введения.", "ISBN0201038013", 490m, "Art Of Programming" },
                    { 2, "M. Fowler", "Помимо описания различных методов рефакторинга, автор приводит подробный каталог более чем с семьюдесятью рефакторингами и полезными указаниями, которые научат вас, когда их следует применять. Книга содержит подробное описание свыше 70 методов рефакторинга, причем не только теоретическое их описание, но и практические примеры на языке программирования Java. Следует учесть, что изложенные в книге идеи применимы к любому объектно-ориентированному языку программирования.", "ISBN0201485672", 2250m, "Refactoring" },
                    { 3, "B. W. Kernighan, D. M. Ritchie", "Перед Вами классическая книга по языку программирования C (Си), написанная самими разработчиками этого языка и выдержавшая в США уже 34 переиздания! Книга является как практически исчерпывающим справочником, так и учебным пособием по самому распространенному языку программирования C (Си). Предлагаемое второе издание книги было существенно переработано по сравнению с первым в связи с появлением стандарта ANSI C, для которого она частично послужила основой. Книга Язык программирования C (Си) не рекомендуется для чтения новичкам; для своего изучения она требует знания основ программирования и компьютеров Книга Язык программирования C (Си) предназначена для широкого круга программистов и компьютерных специалистов. Книга может использоваться как учебное пособие для высших учебных заведений. переработанное и дополненное.", "ISBN0131101633", 1775m, "C Programming Language" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_OrderId",
                table: "Items",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
