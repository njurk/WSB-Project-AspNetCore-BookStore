using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class Alltablesfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookGenres_Books_BookIdBook",
                table: "BookGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_BookGenres_Genres_GenreIdGenre",
                table: "BookGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Suppliers_SupplierIdSupplier",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryItems_Books_BookIdBook",
                table: "DeliveryItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryItems_Deliveries_DeliveryIdDelivery",
                table: "DeliveryItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Books_BookIdBook",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderIdOrder",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserIdUser",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Books_BookIdBook",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_UserIdUser",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_BookIdBook",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_UserIdUser",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserIdUser",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_BookIdBook",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_OrderIdOrder",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryItems_BookIdBook",
                table: "DeliveryItems");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryItems_DeliveryIdDelivery",
                table: "DeliveryItems");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_SupplierIdSupplier",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_BookGenres_BookIdBook",
                table: "BookGenres");

            migrationBuilder.DropIndex(
                name: "IX_BookGenres_GenreIdGenre",
                table: "BookGenres");

            migrationBuilder.DropColumn(
                name: "BookIdBook",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "UserIdUser",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "UserIdUser",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BookIdBook",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "OrderIdOrder",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "BookIdBook",
                table: "DeliveryItems");

            migrationBuilder.DropColumn(
                name: "DeliveryIdDelivery",
                table: "DeliveryItems");

            migrationBuilder.DropColumn(
                name: "SupplierIdSupplier",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "BookIdBook",
                table: "BookGenres");

            migrationBuilder.DropColumn(
                name: "GenreIdGenre",
                table: "BookGenres");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_IdBook",
                table: "Reviews",
                column: "IdBook");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_IdUser",
                table: "Reviews",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_IdUser",
                table: "Orders",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_IdBook",
                table: "OrderItems",
                column: "IdBook");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_IdOrder",
                table: "OrderItems",
                column: "IdOrder");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryItems_IdBook",
                table: "DeliveryItems",
                column: "IdBook");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryItems_IdDelivery",
                table: "DeliveryItems",
                column: "IdDelivery");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_IdSupplier",
                table: "Deliveries",
                column: "IdSupplier");

            migrationBuilder.CreateIndex(
                name: "IX_BookGenres_IdBook",
                table: "BookGenres",
                column: "IdBook");

            migrationBuilder.CreateIndex(
                name: "IX_BookGenres_IdGenre",
                table: "BookGenres",
                column: "IdGenre");

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenres_Books_IdBook",
                table: "BookGenres",
                column: "IdBook",
                principalTable: "Books",
                principalColumn: "IdBook",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenres_Genres_IdGenre",
                table: "BookGenres",
                column: "IdGenre",
                principalTable: "Genres",
                principalColumn: "IdGenre",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Suppliers_IdSupplier",
                table: "Deliveries",
                column: "IdSupplier",
                principalTable: "Suppliers",
                principalColumn: "IdSupplier",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryItems_Books_IdBook",
                table: "DeliveryItems",
                column: "IdBook",
                principalTable: "Books",
                principalColumn: "IdBook",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryItems_Deliveries_IdDelivery",
                table: "DeliveryItems",
                column: "IdDelivery",
                principalTable: "Deliveries",
                principalColumn: "IdDelivery",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Books_IdBook",
                table: "OrderItems",
                column: "IdBook",
                principalTable: "Books",
                principalColumn: "IdBook",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_IdOrder",
                table: "OrderItems",
                column: "IdOrder",
                principalTable: "Orders",
                principalColumn: "IdOrder",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_IdUser",
                table: "Orders",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Books_IdBook",
                table: "Reviews",
                column: "IdBook",
                principalTable: "Books",
                principalColumn: "IdBook",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_IdUser",
                table: "Reviews",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookGenres_Books_IdBook",
                table: "BookGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_BookGenres_Genres_IdGenre",
                table: "BookGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Suppliers_IdSupplier",
                table: "Deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryItems_Books_IdBook",
                table: "DeliveryItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryItems_Deliveries_IdDelivery",
                table: "DeliveryItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Books_IdBook",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_IdOrder",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_IdUser",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Books_IdBook",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_IdUser",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_IdBook",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_IdUser",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Orders_IdUser",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_IdBook",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_IdOrder",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryItems_IdBook",
                table: "DeliveryItems");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryItems_IdDelivery",
                table: "DeliveryItems");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_IdSupplier",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_BookGenres_IdBook",
                table: "BookGenres");

            migrationBuilder.DropIndex(
                name: "IX_BookGenres_IdGenre",
                table: "BookGenres");

            migrationBuilder.AddColumn<int>(
                name: "BookIdBook",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserIdUser",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserIdUser",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BookIdBook",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderIdOrder",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BookIdBook",
                table: "DeliveryItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryIdDelivery",
                table: "DeliveryItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SupplierIdSupplier",
                table: "Deliveries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BookIdBook",
                table: "BookGenres",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GenreIdGenre",
                table: "BookGenres",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookIdBook",
                table: "Reviews",
                column: "BookIdBook");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserIdUser",
                table: "Reviews",
                column: "UserIdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserIdUser",
                table: "Orders",
                column: "UserIdUser");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_BookIdBook",
                table: "OrderItems",
                column: "BookIdBook");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderIdOrder",
                table: "OrderItems",
                column: "OrderIdOrder");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryItems_BookIdBook",
                table: "DeliveryItems",
                column: "BookIdBook");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryItems_DeliveryIdDelivery",
                table: "DeliveryItems",
                column: "DeliveryIdDelivery");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_SupplierIdSupplier",
                table: "Deliveries",
                column: "SupplierIdSupplier");

            migrationBuilder.CreateIndex(
                name: "IX_BookGenres_BookIdBook",
                table: "BookGenres",
                column: "BookIdBook");

            migrationBuilder.CreateIndex(
                name: "IX_BookGenres_GenreIdGenre",
                table: "BookGenres",
                column: "GenreIdGenre");

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenres_Books_BookIdBook",
                table: "BookGenres",
                column: "BookIdBook",
                principalTable: "Books",
                principalColumn: "IdBook",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenres_Genres_GenreIdGenre",
                table: "BookGenres",
                column: "GenreIdGenre",
                principalTable: "Genres",
                principalColumn: "IdGenre",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Suppliers_SupplierIdSupplier",
                table: "Deliveries",
                column: "SupplierIdSupplier",
                principalTable: "Suppliers",
                principalColumn: "IdSupplier",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryItems_Books_BookIdBook",
                table: "DeliveryItems",
                column: "BookIdBook",
                principalTable: "Books",
                principalColumn: "IdBook",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryItems_Deliveries_DeliveryIdDelivery",
                table: "DeliveryItems",
                column: "DeliveryIdDelivery",
                principalTable: "Deliveries",
                principalColumn: "IdDelivery",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Books_BookIdBook",
                table: "OrderItems",
                column: "BookIdBook",
                principalTable: "Books",
                principalColumn: "IdBook",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderIdOrder",
                table: "OrderItems",
                column: "OrderIdOrder",
                principalTable: "Orders",
                principalColumn: "IdOrder",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserIdUser",
                table: "Orders",
                column: "UserIdUser",
                principalTable: "Users",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Books_BookIdBook",
                table: "Reviews",
                column: "BookIdBook",
                principalTable: "Books",
                principalColumn: "IdBook",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_UserIdUser",
                table: "Reviews",
                column: "UserIdUser",
                principalTable: "Users",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
