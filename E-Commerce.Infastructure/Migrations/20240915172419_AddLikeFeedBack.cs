using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLikeFeedBack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LikeFeedBacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductFeedBackId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeFeedBacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikeFeedBacks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikeFeedBacks_ProductFeedBacks_ProductFeedBackId",
                        column: x => x.ProductFeedBackId,
                        principalTable: "ProductFeedBacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LikeFeedBacks_ProductFeedBackId",
                table: "LikeFeedBacks",
                column: "ProductFeedBackId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeFeedBacks_UserId",
                table: "LikeFeedBacks",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikeFeedBacks");
        }
    }
}
