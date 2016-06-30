using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace 拍卖系统.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    GroupICO = table.Column<string>(nullable: true),
                    GroupIDX = table.Column<string>(nullable: true),
                    GroupName = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Auctions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BidCount = table.Column<int>(nullable: false),
                    Bidnb = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    EndStatus = table.Column<int>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    Gid = table.Column<int>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    Mid = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    NowPrice = table.Column<decimal>(nullable: false),
                    Onset = table.Column<decimal>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    StepSize = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auctions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuctionRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Bided = table.Column<decimal>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Gid = table.Column<int>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    Mid = table.Column<int>(nullable: false),
                    Money = table.Column<decimal>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Goods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Picture = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AgentId = table.Column<int>(nullable: false),
                    AgentLevelId = table.Column<int>(nullable: false),
                    AgentTime = table.Column<DateTime>(nullable: false),
                    Area = table.Column<string>(nullable: true),
                    Avatar = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    ClickCount = table.Column<int>(nullable: false),
                    Commission = table.Column<decimal>(nullable: false),
                    Commission_Pay = table.Column<decimal>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Credit1 = table.Column<int>(nullable: false),
                    Credit2 = table.Column<decimal>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false),
                    IsAgent = table.Column<bool>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    LevelId = table.Column<int>(nullable: false),
                    Mobile = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    NickName = table.Column<string>(nullable: true),
                    NoticeSet = table.Column<string>(nullable: true),
                    OpenId = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    Weixin = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false),
                    ItemDesc = table.Column<string>(nullable: true),
                    ItemICO = table.Column<string>(nullable: true),
                    ItemIDX = table.Column<string>(nullable: true),
                    ItemName = table.Column<string>(nullable: true),
                    ItemURL = table.Column<string>(nullable: true),
                    MenuModelId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItems_Menus_MenuModelId",
                        column: x => x.MenuModelId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_MenuModelId",
                table: "MenuItems",
                column: "MenuModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "Auctions");

            migrationBuilder.DropTable(
                name: "AuctionRecords");

            migrationBuilder.DropTable(
                name: "Goods");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Menus");
        }
    }
}
