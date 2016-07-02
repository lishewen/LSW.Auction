using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace 拍卖系统.Data.Migrations
{
    public partial class Menu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Menus_MenuModelId",
                table: "MenuItems");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_MenuModelId",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "MenuModelId",
                table: "MenuItems");

            migrationBuilder.AddColumn<string>(
                name: "ControllerName",
                table: "Menus",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ActionName",
                table: "MenuItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "MenuItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_GroupId",
                table: "MenuItems",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Menus_GroupId",
                table: "MenuItems",
                column: "GroupId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Menus_GroupId",
                table: "MenuItems");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_GroupId",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "ControllerName",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "ActionName",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "MenuItems");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "MenuItems",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MenuModelId",
                table: "MenuItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_MenuModelId",
                table: "MenuItems",
                column: "MenuModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Menus_MenuModelId",
                table: "MenuItems",
                column: "MenuModelId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
