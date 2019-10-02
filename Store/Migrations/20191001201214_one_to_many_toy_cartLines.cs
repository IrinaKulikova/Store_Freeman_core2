using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Migrations
{
    public partial class one_to_many_toy_cartLines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartLine_Orders_OrderID",
                table: "CartLine");

            migrationBuilder.DropForeignKey(
                name: "FK_CartLine_Toys_ToyId",
                table: "CartLine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartLine",
                table: "CartLine");

            migrationBuilder.RenameTable(
                name: "CartLine",
                newName: "CartLines");

            migrationBuilder.RenameIndex(
                name: "IX_CartLine_ToyId",
                table: "CartLines",
                newName: "IX_CartLines_ToyId");

            migrationBuilder.RenameIndex(
                name: "IX_CartLine_OrderID",
                table: "CartLines",
                newName: "IX_CartLines_OrderID");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Toys",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Toys",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartLines",
                table: "CartLines",
                column: "CartLineID");

            migrationBuilder.AddForeignKey(
                name: "FK_CartLines_Orders_OrderID",
                table: "CartLines",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartLines_Toys_ToyId",
                table: "CartLines",
                column: "ToyId",
                principalTable: "Toys",
                principalColumn: "ToyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartLines_Orders_OrderID",
                table: "CartLines");

            migrationBuilder.DropForeignKey(
                name: "FK_CartLines_Toys_ToyId",
                table: "CartLines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartLines",
                table: "CartLines");

            migrationBuilder.RenameTable(
                name: "CartLines",
                newName: "CartLine");

            migrationBuilder.RenameIndex(
                name: "IX_CartLines_ToyId",
                table: "CartLine",
                newName: "IX_CartLine_ToyId");

            migrationBuilder.RenameIndex(
                name: "IX_CartLines_OrderID",
                table: "CartLine",
                newName: "IX_CartLine_OrderID");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Toys",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Toys",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartLine",
                table: "CartLine",
                column: "CartLineID");

            migrationBuilder.AddForeignKey(
                name: "FK_CartLine_Orders_OrderID",
                table: "CartLine",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartLine_Toys_ToyId",
                table: "CartLine",
                column: "ToyId",
                principalTable: "Toys",
                principalColumn: "ToyId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
