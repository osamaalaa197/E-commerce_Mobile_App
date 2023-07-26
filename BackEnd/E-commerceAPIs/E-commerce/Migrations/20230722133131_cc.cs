using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace E_commerce.Migrations
{
    /// <inheritdoc />
    public partial class cc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "isLikes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userID = table.Column<string>(type: "text", nullable: false),
                    UsersId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_isLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_isLikes_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IsLikeProducts",
                columns: table => new
                {
                    IslikeId = table.Column<int>(type: "integer", nullable: false),
                    ProductsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IsLikeProducts", x => new { x.IslikeId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_IsLikeProducts_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IsLikeProducts_isLikes_IslikeId",
                        column: x => x.IslikeId,
                        principalTable: "isLikes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IsLikeProducts_ProductsId",
                table: "IsLikeProducts",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_isLikes_UsersId",
                table: "isLikes",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IsLikeProducts");

            migrationBuilder.DropTable(
                name: "isLikes");
        }
    }
}
