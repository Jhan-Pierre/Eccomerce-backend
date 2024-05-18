using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eccomerce.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_module",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tb_modul__3213E83F43A81771", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_role",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tb_role__3213E83F7E51A857", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_shift",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tb_shift__3213E83F10078CE2", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_state",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tb_state__3213E83F91740684", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_permission",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: false),
                    module_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tb_permi__3213E83F88405BCA", x => x.id);
                    table.ForeignKey(
                        name: "FK__tb_permis__modul__440B1D61",
                        column: x => x.module_id,
                        principalTable: "tb_module",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tb_user",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false),
                    password = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    phone = table.Column<string>(type: "char(9)", unicode: false, fixedLength: true, maxLength: 9, nullable: false),
                    first_name = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: false),
                    last_name = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    state_id = table.Column<int>(type: "int", nullable: false),
                    shift_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tb_user__3213E83F88F1299A", x => x.id);
                    table.ForeignKey(
                        name: "FK__tb_user__role_id__4CA06362",
                        column: x => x.role_id,
                        principalTable: "tb_role",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__tb_user__shift_i__4E88ABD4",
                        column: x => x.shift_id,
                        principalTable: "tb_shift",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__tb_user__state_i__4D94879B",
                        column: x => x.state_id,
                        principalTable: "tb_state",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tb_role_permission",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_id = table.Column<int>(type: "int", nullable: true),
                    permission_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tb_role___3213E83FDD340459", x => x.id);
                    table.ForeignKey(
                        name: "FK__tb_role_p__permi__47DBAE45",
                        column: x => x.permission_id,
                        principalTable: "tb_permission",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__tb_role_p__role___46E78A0C",
                        column: x => x.role_id,
                        principalTable: "tb_role",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "UQ__tb_modul__72E12F1B76F214DE",
                table: "tb_module",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_permission_module_id",
                table: "tb_permission",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "UQ__tb_permi__72E12F1B34CF7E69",
                table: "tb_permission",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__tb_role__72E12F1B45DD89E8",
                table: "tb_role",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_role_permission_permission_id",
                table: "tb_role_permission",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_role_permission_role_id",
                table: "tb_role_permission",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "UQ__tb_shift__72E12F1BF47B25B9",
                table: "tb_shift",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__tb_state__72E12F1B269BE59F",
                table: "tb_state",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_user_role_id",
                table: "tb_user",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_user_shift_id",
                table: "tb_user",
                column: "shift_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_user_state_id",
                table: "tb_user",
                column: "state_id");

            migrationBuilder.CreateIndex(
                name: "UQ__tb_user__AB6E61649E1D72D7",
                table: "tb_user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__tb_user__B43B145FEE1267BE",
                table: "tb_user",
                column: "phone",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_role_permission");

            migrationBuilder.DropTable(
                name: "tb_user");

            migrationBuilder.DropTable(
                name: "tb_permission");

            migrationBuilder.DropTable(
                name: "tb_role");

            migrationBuilder.DropTable(
                name: "tb_shift");

            migrationBuilder.DropTable(
                name: "tb_state");

            migrationBuilder.DropTable(
                name: "tb_module");
        }
    }
}
