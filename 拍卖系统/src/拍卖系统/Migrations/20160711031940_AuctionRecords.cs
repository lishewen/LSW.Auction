using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace 拍卖系统.Migrations
{
    public partial class AuctionRecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AuctionRecords_Gid",
                table: "AuctionRecords",
                column: "Gid");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_Gid",
                table: "Auctions",
                column: "Gid");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Goods_Gid",
                table: "Auctions",
                column: "Gid",
                principalTable: "Goods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionRecords_Auctions_Gid",
                table: "AuctionRecords",
                column: "Gid",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Goods_Gid",
                table: "Auctions");

            migrationBuilder.DropForeignKey(
                name: "FK_AuctionRecords_Auctions_Gid",
                table: "AuctionRecords");

            migrationBuilder.DropIndex(
                name: "IX_AuctionRecords_Gid",
                table: "AuctionRecords");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_Gid",
                table: "Auctions");
        }
    }
}
