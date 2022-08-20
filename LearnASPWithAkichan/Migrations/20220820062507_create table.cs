using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnASPWithAkichan.Migrations
{
    public partial class createtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "account",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    user_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    passWord = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    role = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "department",
                columns: table => new
                {
                    id = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    address = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    phone = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_department", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "student",
                columns: table => new
                {
                    id = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    birth = table.Column<DateTime>(type: "date", nullable: false),
                    address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    home_town = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    department_id = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: true),
                    account_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student", x => x.id);
                    table.ForeignKey(
                        name: "FK__student__account__36B12243",
                        column: x => x.account_id,
                        principalTable: "account",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__student__departm__35BCFE0A",
                        column: x => x.department_id,
                        principalTable: "department",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "subject",
                columns: table => new
                {
                    id = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    credits = table.Column<int>(type: "int", nullable: true),
                    department_id = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subject", x => x.id);
                    table.ForeignKey(
                        name: "FK__subject__departm__2B3F6F97",
                        column: x => x.department_id,
                        principalTable: "department",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "class_session",
                columns: table => new
                {
                    id = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    amount = table.Column<int>(type: "int", nullable: false),
                    point_class = table.Column<double>(type: "float", nullable: true),
                    point_mid = table.Column<double>(type: "float", nullable: true),
                    point_end = table.Column<double>(type: "float", nullable: true),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    begin_date = table.Column<DateTime>(type: "date", nullable: false),
                    end_date = table.Column<DateTime>(type: "date", nullable: false),
                    common_class = table.Column<bool>(type: "bit", nullable: false),
                    department_id = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: true),
                    subject_id = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_class_session", x => x.id);
                    table.ForeignKey(
                        name: "FK__class_ses__depar__31EC6D26",
                        column: x => x.department_id,
                        principalTable: "department",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__class_ses__subje__32E0915F",
                        column: x => x.subject_id,
                        principalTable: "subject",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "prerequisite_subject",
                columns: table => new
                {
                    subject_id = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    prerequisite_subject_id = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__prerequi__5E9C16D777E628E1", x => new { x.subject_id, x.prerequisite_subject_id });
                    table.ForeignKey(
                        name: "FK__prerequis__prere__2F10007B",
                        column: x => x.prerequisite_subject_id,
                        principalTable: "subject",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__prerequis__subje__2E1BDC42",
                        column: x => x.subject_id,
                        principalTable: "subject",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "regist_class",
                columns: table => new
                {
                    student_id = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    class_session_id = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    credits = table.Column<int>(type: "int", nullable: true),
                    regist_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__regist_c__249FCF5C89324CF5", x => new { x.class_session_id, x.student_id });
                    table.ForeignKey(
                        name: "FK__regist_cl__class__3A81B327",
                        column: x => x.class_session_id,
                        principalTable: "class_session",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__regist_cl__stude__398D8EEE",
                        column: x => x.student_id,
                        principalTable: "student",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "UQ__account__7C9273C407D22EDA",
                table: "account",
                column: "user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_class_session_department_id",
                table: "class_session",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "IX_class_session_subject_id",
                table: "class_session",
                column: "subject_id");

            migrationBuilder.CreateIndex(
                name: "UQ__departme__72E12F1BB1B2FDEF",
                table: "department",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__departme__B43B145FE3EA6A38",
                table: "department",
                column: "phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_prerequisite_subject_prerequisite_subject_id",
                table: "prerequisite_subject",
                column: "prerequisite_subject_id");

            migrationBuilder.CreateIndex(
                name: "IX_regist_class_student_id",
                table: "regist_class",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_student_account_id",
                table: "student",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_student_department_id",
                table: "student",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "IX_subject_department_id",
                table: "subject",
                column: "department_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "prerequisite_subject");

            migrationBuilder.DropTable(
                name: "regist_class");

            migrationBuilder.DropTable(
                name: "class_session");

            migrationBuilder.DropTable(
                name: "student");

            migrationBuilder.DropTable(
                name: "subject");

            migrationBuilder.DropTable(
                name: "account");

            migrationBuilder.DropTable(
                name: "department");
        }
    }
}
