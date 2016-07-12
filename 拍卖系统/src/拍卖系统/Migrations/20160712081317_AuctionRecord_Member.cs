using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace 拍卖系统.Migrations
{
    public partial class AuctionRecord_Member : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AuctionRecords_Mid",
                table: "AuctionRecords",
                column: "Mid");

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionRecords_Members_Mid",
                table: "AuctionRecords",
                column: "Mid",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionRecords_Members_Mid",
                table: "AuctionRecords");

            migrationBuilder.DropIndex(
                name: "IX_AuctionRecords_Mid",
                table: "AuctionRecords");
        }
    }
}
