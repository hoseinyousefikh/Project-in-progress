using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace App.Infra.Data.Db.SqlServer.Ef.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ProfilePicture = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShebaNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegisterAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleType = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleStatus = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Experts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleStatus = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HomeServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false),
                    ExpertsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeServices_Experts_ExpertsId",
                        column: x => x.ExpertsId,
                        principalTable: "Experts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HomeServices_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpertSkills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ExpertId = table.Column<int>(type: "int", nullable: false),
                    HomeServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpertSkills_Experts_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "Experts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpertSkills_HomeServices_HomeServiceId",
                        column: x => x.HomeServiceId,
                        principalTable: "HomeServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExecutionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExecutionTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OrderStatus = table.Column<int>(type: "int", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    HomeServiceId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ExpertsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Experts_ExpertsId",
                        column: x => x.ExpertsId,
                        principalTable: "Experts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_HomeServices_HomeServiceId",
                        column: x => x.HomeServiceId,
                        principalTable: "HomeServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpertProposals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    ProposedPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProposalDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProposalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkCompletionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProposedExecutionTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    CustomerSelectionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProposalStatus = table.Column<int>(type: "int", nullable: false),
                    IsSelectedByCustomer = table.Column<bool>(type: "bit", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ExpertId = table.Column<int>(type: "int", nullable: false),
                    CommentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertProposals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpertProposals_Experts_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "Experts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpertProposals_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdersId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pictures_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ExpertId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    ExpertProposalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_ExpertProposals_ExpertProposalId",
                        column: x => x.ExpertProposalId,
                        principalTable: "ExpertProposals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Experts_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "Experts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, null, "Admin", "ADMIN" },
                    { 2, null, "Customer", "CUSTOMER" },
                    { 3, null, "Expert", "EXPERT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "Balance", "CardNumber", "CityId", "ConcurrencyStamp", "Description", "Email", "EmailConfirmed", "FirstName", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "RegisterAt", "RoleId", "RoleType", "SecurityStamp", "ShebaNumber", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, null, 1000000m, null, null, "16293623-2faa-4780-8528-0ae75989ec69", null, "admin@example.com", true, "حسین", false, "یوسفی", false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEDONgP98N91UL5YMEmJE0ZG+UKwp2A5UydXI2VwgRvE/1jUZOJvvHIbSMJHAySADtA==", null, false, null, new DateTime(2025, 3, 10, 7, 59, 31, 22, DateTimeKind.Local).AddTicks(4593), 1, 1, "e720c964-a5ab-4052-81d4-0b1b4264346c", null, false, "admin" },
                    { 2, 0, null, 500000m, null, null, "f19f3637-460f-40ac-a94b-c75226c8cbc8", null, "expert1@example.com", true, "کارشناس", false, "یک", false, null, "EXPERT1@EXAMPLE.COM", "EXPERT1", "AQAAAAIAAYagAAAAEARzzPcHJAUUgFm8pmtF0QvxLM+Sxs5jggrUU1J7tcoRhDlU+ftcpwhbZGQCuyK+yg==", null, false, null, new DateTime(2025, 3, 10, 7, 59, 31, 109, DateTimeKind.Local).AddTicks(2224), 3, 3, "2ed90266-b384-4d08-9b82-f82410b88e78", null, false, "expert1" },
                    { 3, 0, null, 600000m, null, null, "375755e4-e8e1-477e-86f7-f0f20e6f1ae2", null, "expert2@example.com", true, "کارشناس", false, "دو", false, null, "EXPERT2@EXAMPLE.COM", "EXPERT2", "AQAAAAIAAYagAAAAEH30HpwRJdJ0au8QHFVU2Wgxs3aAwW69uUcvbgP19M6mAbW5nFyemn7U1JE7SWld9A==", null, false, null, new DateTime(2025, 3, 10, 7, 59, 31, 245, DateTimeKind.Local).AddTicks(9321), 3, 3, "b4a2dfe3-8fef-4690-b221-279a24668b6c", null, false, "expert2" },
                    { 4, 0, null, 700000m, null, null, "4e2da07c-8220-4030-a48c-2aaf3d75d3c5", null, "expert3@example.com", true, "کارشناس", false, "سه", false, null, "EXPERT3@EXAMPLE.COM", "EXPERT3", "AQAAAAIAAYagAAAAEAILsayBxxwdJHJRXi4Plw0Q6diCUFfS1KgnFPfPp6IqrfSgOGjQP8hw1W3WrQta2A==", null, false, null, new DateTime(2025, 3, 10, 7, 59, 31, 344, DateTimeKind.Local).AddTicks(2479), 3, 3, "c05035e9-fcb6-426e-ac50-b262208d9146", null, false, "expert3" },
                    { 5, 0, null, 200000m, null, null, "0f91ad9f-fba5-4160-bdf9-a2c97d49b5d3", null, "customer1@example.com", true, "مشتری", false, "یک", false, null, "CUSTOMER1@EXAMPLE.COM", "CUSTOMER1", "AQAAAAIAAYagAAAAEAitaJBUzaS94KtC6/1B0AZKl16hATWfBxOgX8zQBSzkgL/gV86MNAwuzp59q5b7OA==", null, false, null, new DateTime(2025, 3, 10, 7, 59, 31, 455, DateTimeKind.Local).AddTicks(3487), 2, 2, "3d4a7823-9313-46a5-877f-314cc63c18d4", null, false, "customer1" },
                    { 6, 0, null, 300000m, null, null, "a8f57a12-35bd-49c0-8e1e-2b4682f53c54", null, "customer2@example.com", true, "مشتری", false, "دو", false, null, "CUSTOMER2@EXAMPLE.COM", "CUSTOMER2", "AQAAAAIAAYagAAAAEOsdkie4h1fSJSIrJhOJFZqNsCb/z4B54lIigHSLNB8EuGmV711xBbIRKgYmsUvo6Q==", null, false, null, new DateTime(2025, 3, 10, 7, 59, 31, 542, DateTimeKind.Local).AddTicks(7641), 2, 2, "a5bb9bf3-eecc-43c3-9d23-757c5a16da89", null, false, "customer2" },
                    { 7, 0, null, 400000m, null, null, "0417a288-b9df-43bb-ace3-bba13ba0b7b2", null, "customer3@example.com", true, "مشتری", false, "سه", false, null, "CUSTOMER3@EXAMPLE.COM", "CUSTOMER3", "AQAAAAIAAYagAAAAEJTyn07FhZAGwSsKlvlcZpNjCLhuCV3h51PYl7orL5lAlUoZqDydwYwa5NEKGX6Ovg==", null, false, null, new DateTime(2025, 3, 10, 7, 59, 31, 634, DateTimeKind.Local).AddTicks(5870), 2, 2, "69a2420b-6b71-4438-badd-f75714e4ad2f", null, false, "customer3" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, "/images/icon/01.webp", "تمیز کاری" },
                    { 2, "/images/icon/08.webp", "ساختمان" },
                    { 3, "/images/icon/03.webp", "تعمیرات اشیا" },
                    { 4, "/images/icon/05.webp", "اسباب‌کشی و حمل بار" },
                    { 5, "/images/icon/06.webp", "خودرو" },
                    { 6, "/images/icon/04.webp", "سلامت و زیبایی" },
                    { 7, "/images/icon/02.webp", "سازمان‌ها و مجتمع‌ها" },
                    { 8, "/images/icon/07.webp", "سایر" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, false, "آذربایجان شرقی" },
                    { 2, false, "آذربایجان غربی" },
                    { 3, false, "اردبیل" },
                    { 4, false, "اصفهان" },
                    { 5, false, "البرز" },
                    { 6, false, "ایلام" },
                    { 7, false, "بوشهر" },
                    { 8, false, "تهران" },
                    { 9, false, "چهارمحال و بختیاری" },
                    { 10, false, "خراسان جنوبی" },
                    { 11, false, "خراسان رضوی" },
                    { 12, false, "خراسان شمالی" },
                    { 13, false, "خوزستان" },
                    { 14, false, "زنجان" },
                    { 15, false, "سمنان" },
                    { 16, false, "سیستان و بلوچستان" },
                    { 17, false, "فارس" },
                    { 18, false, "قزوین" },
                    { 19, false, "قم" },
                    { 20, false, "کردستان" },
                    { 21, false, "کرمان" },
                    { 22, false, "کرمانشاه" },
                    { 23, false, "کهگیلویه و بویراحمد" },
                    { 24, false, "گلستان" },
                    { 25, false, "گیلان" },
                    { 26, false, "لرستان" },
                    { 27, false, "مازندران" },
                    { 28, false, "مرکزی" },
                    { 29, false, "هرمزگان" },
                    { 30, false, "همدان" },
                    { 31, false, "یزد" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 3, 2 },
                    { 2, 6 }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "IsDeleted", "RoleStatus", "UserId" },
                values: new object[,]
                {
                    { 1, false, 2, 5 },
                    { 2, false, 2, 6 },
                    { 3, false, 2, 7 }
                });

            migrationBuilder.InsertData(
                table: "Experts",
                columns: new[] { "Id", "Biography", "IsDeleted", "Rating", "RoleStatus", "UserId" },
                values: new object[,]
                {
                    { 1, "من بهترین کارشناس هستم و توانمندی‌های زیادی دارم.", false, 4.5, 2, 2 },
                    { 2, "کارشناس عالی در زمینه طراحی و برنامه‌نویسی.", false, 4.7000000000000002, 2, 3 },
                    { 3, "من در مشاوره و برنامه‌ریزی کسب‌وکار تخصص دارم.", false, 4.7999999999999998, 2, 4 }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryId", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, 1, "/images/sub/01-tamizkary/01.webp", "نظافت و پذیرایی" },
                    { 2, 1, "/images/sub/01-tamizkary/02.webp", "شستشوی " },
                    { 3, 1, "/images/sub/01-tamizkary/03.webp", " کارواش" },
                    { 4, 2, "/images/sub/02-sakhteman/08.webp", "سرمایشی و گرمایشی" },
                    { 5, 2, "/images/sub/02-sakhteman/07.webp", "تعمیرات ساختمان" },
                    { 6, 2, "/images/sub/02-sakhteman/04.webp", "لوله کشی" },
                    { 7, 2, "/images/sub/02-sakhteman/06.webp", "طراحی و بازسازی ساختمان" },
                    { 8, 2, "/images/sub/02-sakhteman/05.webp", "برق‌کاری" },
                    { 9, 3, "/images/sub/03-tamiratAshya/10.webp", "سرمایشی و گرمایشی" },
                    { 10, 3, "/images/sub/03-tamiratAshya/09.webp", "نصب و تعمیر لوازم خانگی" },
                    { 11, 3, "/images/sub/03-tamiratAshya/12.webp", "خدمات کامپیوتری" },
                    { 12, 3, "/images/sub/03-tamiratAshya/11.webp", "تعمیرات موبایل" },
                    { 13, 4, "/images/sub/04-hamlBar/13.webp", "باربری و جابه‌جایی" },
                    { 14, 5, "/images/sub/05-khodro/14.webp", "خدمات و تعمیر خودرو" },
                    { 15, 5, "/images/sub/05-khodro/15.webp", "کارواش" },
                    { 16, 6, "/images/sub/06-salamat-zibayi/17.webp", "زیبایی بانوان" },
                    { 17, 6, "/images/sub/06-salamat-zibayi/18.webp", "پزشکی و پرستاری" },
                    { 18, 6, "/images/sub/06-salamat-zibayi/19.webp", "حیوانات خانگی" },
                    { 19, 6, "/images/sub/06-salamat-zibayi/16.webp", "مشاوره" },
                    { 20, 6, "/images/sub/06-salamat-zibayi/21.webp", "پیرایش و زیبایی آقایان" },
                    { 21, 6, "/images/sub/06-salamat-zibayi/20.webp", "ورزش" },
                    { 22, 7, "/images/sub/07-sazman/22.webp", "خدمات شرکتی" },
                    { 23, 7, "/images/sub/07-sazman/t.webp", "تامین نیروی انسانی" },
                    { 24, 8, "/images/sub/08-sayer/25.webp", "خیاطی و تعمیرات لباس" },
                    { 25, 8, "/images/sub/08-sayer/23.webp", "مجالس و رویدادها" },
                    { 26, 8, "/images/sub/08-sayer/27.webp", "آموزش" },
                    { 27, 8, "/images/sub/08-sayer/24.webp", "خدمات فوری آچاره" },
                    { 28, 8, "/images/sub/08-sayer/26.webp", "همه فن حریف" }
                });

            migrationBuilder.InsertData(
                table: "HomeServices",
                columns: new[] { "Id", "BasePrice", "Description", "ExpertsId", "ImageUrl", "Name", "SubCategoryId" },
                values: new object[,]
                {
                    { 1, 500000m, "نظافت معمولی برای منازل و دفاتر شامل گردگیری، جاروکشی و تی کشی", null, "/images/HomeService/01-nezafat/08.jpg", "سرویس عادی نظافت", 1 },
                    { 2, 800000m, "نظافت ویژه شامل شستشوی کامل کف، دیوارها و وسایل خانه", null, "/images/HomeService/01-nezafat/03.jpg", "سرویس ویژه نظافت", 1 },
                    { 3, 1200000m, "نظافت تخصصی همراه با استفاده از مواد شوینده خاص و خوشبوکننده‌های ویژه", null, "/images/HomeService/01-nezafat/01.jpg", "سرویس لوکس نظافت", 1 },
                    { 4, 600000m, "تمیز کردن و شستشوی کامل راه پله‌ها و پاگردها", null, "/images/HomeService/01-nezafat/02.jpg", "نظافت راه پله", 1 },
                    { 5, 700000m, "نظافت اضطراری با تیم حرفه‌ای در کمترین زمان ممکن", null, "/images/HomeService/01-nezafat/07.jpg", "سرویس نظافت فوری", 1 },
                    { 6, 900000m, "ارائه خدمات پذیرایی در مراسمات، مهمانی‌ها و رویدادهای خاص", null, "/images/HomeService/01-nezafat/04.jpg", "پذیرایی", 1 },
                    { 7, 550000m, "اعزام کارگر ساده جهت کمک در امور نظافتی و خدماتی", null, "/images/HomeService/01-nezafat/06.jpg", "کارگر ساده", 1 },
                    { 8, 950000m, "سمپاشی منازل و ادارات برای از بین بردن حشرات موذی", null, "/images/HomeService/01-nezafat/05.jpg", "سمپاشی فضای داخلی", 1 },
                    { 9, 500000m, "شستشوی انواع فرش، مبلمان و سطوح در محل با استفاده از تجهیزات پیشرفته.", null, "/images/HomeService/02-shostesho/04.jpg", "شستشو در محل", 2 },
                    { 10, 700000m, "شستشوی انواع فرش و قالی با رعایت اصول استاندارد و مواد شوینده‌ی مناسب.", null, "/images/HomeService/02-shostesho/03.jpg", "قالیشویی", 2 },
                    { 11, 300000m, "خشکشویی لباس، پرده و ملحفه با دستگاه‌های پیشرفته و مواد شوینده‌ی استاندارد.", null, "/images/HomeService/02-shostesho/01.jpg", "خشکشویی", 2 },
                    { 12, 450000m, "شستشو و اتو کردن انواع پرده با حفظ کیفیت و جلوگیری از آسیب به پارچه.", null, "/images/HomeService/02-shostesho/02.jpg", "پرده‌شویی", 2 },
                    { 13, 2500000m, "سرامیک بدنه خودرو برای محافظت در برابر خط و خش و آلودگی", null, "/images/HomeService/03-karvash1/09.jpg", "سرامیک خودرو", 3 },
                    { 14, 600000m, "شستشوی خودرو با تکنولوژی نانو بدون نیاز به آب", null, "/images/HomeService/03-karvash1/02.jpg", "کارواش نانو", 3 },
                    { 15, 500000m, "شستشوی کامل بدنه خودرو با آب و مواد شوینده استاندارد", null, "/images/HomeService/03-karvash1/01.jpg", "کارواش با آب", 3 },
                    { 16, 800000m, "واکس و پولیش حرفه‌ای برای افزایش درخشندگی و ماندگاری رنگ خودرو", null, "/images/HomeService/03-karvash1/04.jpg", "واکس و پولیش خودرو", 3 },
                    { 17, 1200000m, "نظافت کامل داخل خودرو شامل صندلی‌ها، داشبورد و کفپوش", null, "/images/HomeService/03-karvash1/05.jpg", "صفر شویی خودرو", 3 },
                    { 18, 700000m, "شستشوی موتور خودرو با مواد مخصوص بدون آسیب به اجزای الکتریکی", null, "/images/HomeService/03-karvash1/06.jpg", "موتور شویی خودرو", 3 },
                    { 19, 2000000m, "برطرف کردن خط و خش‌های سطحی و براق‌سازی مجدد رنگ خودرو", null, "/images/HomeService/03-karvash1/03.jpg", "احیای رنگ خودرو", 3 },
                    { 20, 5000000m, "صافکاری و نقاشی کامل خودرو بدون نیاز به باز و بست قطعات", null, "/images/HomeService/03-karvash1/07.jpg", "صافکاری و نقاشی خودرو", 3 },
                    { 21, 800000m, "نصب شیشه دودی استاندارد و مجاز برای خودرو", null, "/images/HomeService/03-karvash1/08.jpg", "نصب شیشه دودی", 3 },
                    { 22, 1000000m, "تعمیر و سرویس انواع آبگرمکن‌های دیواری و ایستاده", null, "/images/HomeService/04-sarmayesh/07.jpg", "تعمیر سرویس آبگرمکن", 4 },
                    { 23, 1200000m, "نصب و تعمیر انواع شوفاژ و سیستم‌های گرمایشی", null, "/images/HomeService/04-sarmayesh1/08.jpg", "نصب و تعمیر رادیاتور شوفاژ", 4 },
                    { 24, 2500000m, "سرویس و نگهداری سیستم‌های موتورخانه‌ای", null, "/images/HomeService/04-sarmayesh/02.jpg", "تعمیر و نگهداری موتورخانه", 4 },
                    { 25, 800000m, "نصب و تعمیر بخاری گازی و دودکش استاندارد", null, "/images/HomeService/04-sarmayesh/03.jpg", "نصب و تعمیر بخاری گازی", 4 },
                    { 26, 1500000m, "تعمیر و شارژ گاز انواع کولرهای گازی", null, "/images/HomeService/04-sarmayesh/01.jpg", "تعمیر کولر گازی", 4 },
                    { 27, 500000m, "سرویس و تعمیر انواع فن‌های تهویه مطبوع", null, "/images/HomeService/04-sarmayesh/04.jpg", "تعمیر و سرویس فن", 4 },
                    { 28, 4000000m, "تعمیر و نگهداری چیلرهای صنعتی و خانگی", null, "/images/HomeService/04-sarmayesh/06.jpg", "سرویس و تعمیر چیلر", 4 },
                    { 29, 1800000m, "ساخت و نصب کانال کولر با استانداردهای بهینه‌سازی", null, "/images/HomeService/04-sarmayesh/05.jpg", "کانال‌سازی کولر", 4 },
                    { 30, 3000000m, "نصب و اجرای سنگ نما و کف", null, "/images/HomeService/05-tamirSakhteman/18.jpg", "سنگ کاری", 5 },
                    { 31, 1500000m, "نصب و اجرای انواع کاغذ دیواری مدرن", null, "/images/HomeService/05-tamirSakhteman/19.jpg", "نصب کاغذ دیواری", 5 },
                    { 32, 800000m, "ساخت و نصب توری پنجره و درب", null, "/images/HomeService/05-tamirSakhteman/15.jpg", "ساخت و نصب توری", 5 },
                    { 33, 2500000m, "اجرای نقاشی دکوراتیو و پتینه‌کاری دیوارها", null, "/images/HomeService/05-tamirSakhteman/02.jpg", "پتینه و دیوارنگاری", 5 },
                    { 34, 1800000m, "اجرای جوشکاری برای درب و پنجره‌های فلزی", null, "/images/HomeService/05-tamirSakhteman/05.jpg", "جوشکاری", 5 },
                    { 35, 2000000m, "ساخت و تعمیر قطعات آهنی و سازه‌های فلزی", null, "/images/HomeService/05-tamirSakhteman/06.jpg", "آهنگری", 5 },
                    { 36, 1000000m, "طراحی، دوخت و نصب پرده‌های سفارشی", null, "/images/HomeService/05-tamirSakhteman/07.jpg", "دوخت و نصب پرده", 5 },
                    { 37, 3000000m, "نصب کاشی و سرامیک کف و دیوار", null, "/images/HomeService/05-tamirSakhteman/08.jpg", "کاشی کاری و سرامیک‌کاری", 5 },
                    { 38, 2000000m, "اجرای کفپوش پارکت و لمینت", null, "/images/HomeService/05-tamirSakhteman/10.jpg", "نصب کفپوش", 5 },
                    { 39, 2500000m, "اجرای گچ‌بری سقف و دیوار", null, "/images/HomeService/05-tamirSakhteman/17.jpg", "گچ کاری و گچ‌بری", 5 },
                    { 40, 800000m, "خدمات تخصصی تخلیه چاه و لوله بازکنی با تجهیزات پیشرفته و بدون تخریب.", null, "/images/HomeService/06-lolekeshi/08.jpg", "تخلیه چاه و لوله بازکنی", 6 },
                    { 41, 350000m, "نصب و تعمیر انواع شیرآلات ساختمانی و صنعتی با بهترین کیفیت.", null, "/images/HomeService/06-lolekeshi/10.jpg", "تعمیر شیرآلات", 6 },
                    { 42, 600000m, "نصب، تعمیر و سرویس انواع پمپ آب و منبع ذخیره آب.", null, "/images/HomeService/06-lolekeshi/01.jpg", "پمپ و منبع آب", 6 },
                    { 43, 500000m, "نصب و تعمیر دستگاه‌های تصفیه آب خانگی و صنعتی با ضمانت کیفیت.", null, "/images/HomeService/06-lolekeshi/02.jpg", "نصب و تعمیر دستگاه تصفیه", 6 },
                    { 44, 750000m, "تشخیص ترکیدگی لوله بدون تخریب و تعمیر با استفاده از دستگاه‌های پیشرفته.", null, "/images/HomeService/06-lolekeshi/03.jpg", "تعمیر و تشخیص ترکیدگی لوله", 6 },
                    { 45, 450000m, "نصب و تعمیر انواع سرویس بهداشتی و توالت فرنگی با بهترین تجهیزات.", null, "/images/HomeService/06-lolekeshi/04.jpg", "نصب سرویس و توالت فرنگی", 6 },
                    { 46, 400000m, "نصب، تعمیر و تعویض انواع فلاش تانک توکار و روکار.", null, "/images/HomeService/06-lolekeshi/05.jpg", "نصب و تعمیر فلاش تانک", 6 },
                    { 47, 900000m, "لوله‌کشی گاز ساختمان با رعایت استانداردهای ایمنی و استفاده از تجهیزات مناسب.", null, "/images/HomeService/06-lolekeshi/06.jpg", "لوله کشی گاز", 6 },
                    { 48, 500000m, "نصب و تعمیر انواع سینک ظرفشویی و رفع نشتی آب.", null, "/images/HomeService/06-lolekeshi/07.jpg", "نصب و تعمیر سینک ظرفشویی", 6 },
                    { 49, 850000m, "اجرای لوله‌کشی آب و فاضلاب برای ساختمان‌های مسکونی و تجاری.", null, "/images/HomeService/06-lolekeshi/08.jpg", "لوله‌کشی آب و فاضلاب", 6 },
                    { 50, 2500000m, "طراحی داخلی منزل با جدیدترین متدهای روز و استفاده از متریال باکیفیت.", null, "/images/HomeService/07-tarahiSakhteman/01.webp", "طراحی داخلی منزل", 7 },
                    { 51, 3500000m, "طراحی دکوراسیون فضای اداری و تجاری برای ایجاد محیطی شیک و کاربردی.", null, "/images/HomeService/07-tarahiSakhteman/08.jpg", "طراحی دکوراسیون اداری", 7 },
                    { 52, 5000000m, "بازسازی کامل ساختمان شامل طراحی داخلی، دیوارکشی، کف‌سازی و نورپردازی.", null, "/images/home_services/renovation.jpg", "بازسازی و نوسازی ساختمان", 7 },
                    { 53, 1200000m, "نصب انواع پارکت و لمینت با طرح‌ها و رنگ‌های متنوع.", null, "/images/home_services/parquet_installation.jpg", "نصب پارکت و لمینت", 7 },
                    { 54, 1000000m, "نصب کاغذ دیواری و پوسترهای دیواری با طرح‌های مدرن و کلاسیک.", null, "/images/home_services/wallpaper_installation.jpg", "کاغذ دیواری و پوستر دیواری", 7 },
                    { 55, 1800000m, "اجرای سقف کاذب و نورپردازی سقف برای جلوه‌ای خاص در دکوراسیون داخلی.", null, "/images/home_services/false_ceiling.jpg", "طراحی و اجرای سقف کاذب", 7 },
                    { 56, 3000000m, "طراحی، ساخت و نصب کابینت‌های مدرن و کلاسیک برای آشپزخانه.", null, "/images/home_services/kitchen_cabinet.jpg", "طراحی و ساخت کابینت آشپزخانه", 7 },
                    { 57, 1300000m, "نصب دیوارپوش و کفپوش PVC مناسب برای فضاهای مسکونی و تجاری.", null, "/images/home_services/pvc_floor_wall.jpg", "نصب دیوارپوش و کفپوش PVC", 7 },
                    { 58, 2000000m, "اجرای انواع نورپردازی داخلی و خارجی با طراحی مدرن و حرفه‌ای.", null, "/images/home_services/lighting_design.jpg", "نورپردازی داخلی و خارجی", 7 },
                    { 59, 900000m, "نقاشی ساختمان با رنگ‌های باکیفیت و اجرای طرح‌های متنوع.", null, "/images/home_services/painting_home.jpg", "نقاشی ساختمان", 7 },
                    { 60, 1500000m, "اجرای سیم‌کشی و کابل‌کشی استاندارد برای ساختمان‌های مسکونی و اداری.", null, "/images/HomeService/08-Baghkary/10.jpg", "سیم کشی و کابل کشی", 8 },
                    { 61, 1200000m, "نصب و تعمیر انواع آیفون‌های صوتی و تصویری با تجهیزات مدرن.", null, "/images/HomeService/08-Baghkary/03.jpg", "نصب و تعمیر آیفون صوتی و تصویری", 8 },
                    { 62, 800000m, "نصب انواع لوستر و چراغ‌های سقفی و دیواری با رعایت ایمنی.", null, "/images/HomeService/08-Baghkary/04.jpg", "نصب لوستر و چراغ", 8 },
                    { 63, 1800000m, "بررسی و رفع خرابی‌های سیم‌کشی برق و ارتقا سیستم برق ساختمان.", null, "/images/HomeService/08-Baghkary/01.jpg", "رفع خرابی و سیم کشی مجدد", 8 },
                    { 64, 1000000m, "نصب، تنظیم و تعمیر انواع تلویزیون‌های LCD و LED.", null, "/images/HomeService/08-Baghkary/05.jpg", "نصب و تعمیر تلویزیون", 8 },
                    { 65, 2500000m, "نصب و راه‌اندازی دوربین‌های مداربسته و سیستم‌های نظارتی.", null, "/images/HomeService/08-Baghkary/06.jpg", "نصب و تعمیر دوربین مداربسته", 8 },
                    { 66, 700000m, "نصب، تعویض و تعمیر انواع کلید و پریز برق با رعایت استانداردهای ایمنی.", null, "/images/HomeService/08-Baghkary/07.jpg", "نصب و تعمیر کلید و پریز", 8 },
                    { 67, 900000m, "نصب، تعویض و ارتقا فیوزهای برق ساختمان برای افزایش ایمنی.", null, "/images/HomeService/08-Baghkary/11.jpg", "نصب و تعویض فیوز", 8 },
                    { 68, 3000000m, "تعمیر، نگهداری و سرویس دوره‌ای آسانسورهای مسکونی و تجاری.", null, "/images/HomeService/08-Baghkary/08.jpg", "تعمیر و سرویس آسانسور", 8 },
                    { 69, 2800000m, "نصب و تعمیر کرکره‌های برقی برای مغازه‌ها و پارکینگ‌ها.", null, "/images/HomeService/08-Baghkary/09.jpg", "نصب و تعمیر کرکره برقی", 8 },
                    { 70, 1700000m, "نصب انواع هواکش و تهویه مطبوع برای ساختمان‌های مسکونی و تجاری.", null, "/images/HomeService/08-Baghkary/02.jpg", "نصب هواکش و تهویه مطبوع", 8 },
                    { 71, 2000000m, "تعمیر و سرویس کولرهای گازی و سیستم‌های تهویه مطبوع.", null, "/images/HomeService/09-sarmayesh/01.jpg", "تعمیر کولرگازی", 9 },
                    { 72, 800000m, "تعمیر و سرویس انواع فن‌ها برای تهویه هوا در ساختمان‌ها.", null, "/images/HomeService/09-sarmayesh/04.jpg", "تعمیر و سرویس فن", 9 },
                    { 73, 4000000m, "سرویس، تعمیر و نگهداری سیستم‌های چیلر برای تهویه مطبوع ساختمان‌ها.", null, "/images/HomeService/09-sarmayesh/06.jpg", "سرویس و تعمیر چیلر", 9 },
                    { 74, 1800000m, "نصب و کانال‌سازی کولرهای گازی و مرکزی برای تهویه مناسب در فضاهای مختلف.", null, "/images/HomeService/09-sarmayesh/05.jpg", "کانال سازی کولر", 9 },
                    { 75, 2500000m, "نصب و تعمیر انواع یخچال فریزرها با رعایت استانداردهای ایمنی.", null, "/images/HomeService/10-nasbVtamir/09.jpg", "نصب و تعمیر یخچال فریزر", 10 },
                    { 76, 2200000m, "نصب و تعمیر انواع ماشین‌های لباسشویی اتوماتیک و نیمه‌اتوماتیک.", null, "/images/HomeService/10-nasbVtamir/03.jpg", "نصب و تعمیر لباسشویی", 10 },
                    { 77, 1500000m, "نصب، تعمیر و سرویس اجاق گازهای رومیزی و فر دار.", null, "/images/HomeService/10-nasbVtamir/06.jpg", "نصب و تعمیر اجاق گاز", 10 },
                    { 78, 2000000m, "نصب و تعمیر انواع ماشین‌های ظرفشویی اتوماتیک و رومیزی.", null, "/images/HomeService/10-nasbVtamir/02.jpg", "نصب و تعمیر ماشین ظرفشویی", 10 },
                    { 79, 1800000m, "تعمیرات تخصصی انواع تلویزیون‌های LED، LCD و OLED.", null, "/images/HomeService/10-nasbVtamir/01.jpg", "تعمیرات تخصصی تلویزیون", 10 },
                    { 80, 1500000m, "نصب، تعمیر و سرویس ماکروویو و مایکروفرهای مختلف.", null, "/images/HomeService/10-nasbVtamir/05.jpg", "نصب و تعمیر ماکروویو", 10 },
                    { 81, 1200000m, "نصب و تعمیر هودهای آشپزخانه با انواع سیستم‌های تهویه.", null, "/images/HomeService/10-nasbVtamir/04.jpg", "نصب و تعمیر هود آشپزخانه", 10 },
                    { 82, 1000000m, "تعمیر و سرویس انواع جاروبرقی‌های خانگی و صنعتی.", null, "/images/HomeService/10-nasbVtamir/08.jpg", "تعمیر جاروبرقی", 10 },
                    { 83, 700000m, "تعمیر و سرویس انواع چرخ‌های خیاطی خانگی و صنعتی.", null, "/images/HomeService/10-nasbVtamir/07.jpg", "تعمیر چرخ خیاطی", 10 },
                    { 84, 1500000m, "تعمیرات تخصصی کامپیوتر و لپ‌تاپ شامل سخت‌افزار و نرم‌افزار.", null, "/images/HomeService/11-khadamatcamputer/02.webp", "تعمیر کامپیوتر و لپ‌تاپ", 11 },
                    { 85, 1000000m, "تعمیرات تخصصی ماشین‌های اداری مانند پرینتر، فکس، کپی.", null, "/images/HomeService/11-khadamatcamputer/04.webp", "تعمیر ماشین‌های اداری", 11 },
                    { 86, 2000000m, "راه‌اندازی و پشتیبانی شبکه‌های کامپیوتری و سرورهای سازمانی.", null, "/images/HomeService/11-khadamatcamputer/01.webp", "پشتیبانی شبکه و سرور", 11 },
                    { 87, 800000m, "نصب، تنظیم و تعمیر مودم‌ها و مشکلات مربوط به اینترنت.", null, "/images/HomeService/11-khadamatcamputer/03.webp", "مودم و اینترنت", 11 },
                    { 88, 3000000m, "طراحی سایت‌های حرفه‌ای و لوگوهای برند برای کسب‌وکارها.", null, "/images/HomeService/11-khadamatcamputer/05.jpg", "طراحی سایت و لوگو", 11 },
                    { 89, 1000000m, "تعمیرات تخصصی صفحه نمایش‌های تاچ و LCD انواع دستگاه‌ها.", null, "/images/HomeService/12-tamiratMobile/02.jpg", "خدمات تعمیرات تاچ و ال‌سی‌دی", 12 },
                    { 90, 700000m, "تعویض، تعمیر و سرویس انواع باتری گوشی، لپ‌تاپ و دستگاه‌های دیگر.", null, "/images/HomeService/12-tamiratMobile/08.jpg", "خدمات باتری", 12 },
                    { 91, 1500000m, "عیب‌یابی و تعمیر بردهای الکترونیکی دستگاه‌ها و تجهیزات مختلف.", null, "/images/HomeService/12-tamiratMobile/04.jpg", "خدمات عیب‌یابی و تعمیر برد", 12 },
                    { 92, 500000m, "خدمات نرم‌افزاری شامل نصب، تعمیر و پشتیبانی نرم‌افزارها.", null, "/images/HomeService/12-tamiratMobile/05.jpg", "خدمات نرم‌افزاری", 12 },
                    { 93, 800000m, "تعمیر و سرویس اسپیکرهای خانگی، بلوتوثی و دیگر انواع دستگاه‌های صوتی.", null, "/images/HomeService/12-tamiratMobile/06.jpg", "خدمات اسپیکر", 12 },
                    { 94, 600000m, "تعویض و تعمیر فریم و قاب انواع دستگاه‌ها از جمله گوشی‌های موبایل.", null, "/images/HomeService/12-tamiratMobile/07.jpg", "خدمات فریم و قاب", 12 },
                    { 95, 1200000m, "تعمیر، نصب و سرویس انواع دوربین‌های مدار بسته و دوربین‌های گوشی.", null, "/images/HomeService/12-tamiratMobile/09.jpg", "خدمات دوربین", 12 },
                    { 96, 700000m, "تعمیر و تعویض انواع سنسورهای مختلف دستگاه‌ها و تجهیزات.", null, "/images/HomeService/12-tamiratMobile/01.jpg", "خدمات سنسور", 12 },
                    { 97, 3000000m, "خدمات اسباب کشی با استفاده از وانت و نیسان برای جابجایی راحت و سریع.", null, "/images/HomeService/13-BarbarivJabejayi/06.jpg", "اسباب کشی با وانت و نیسان", 13 },
                    { 98, 5000000m, "حمل بار از شهری به شهر دیگر با تضمین ایمنی و سرعت بالا.", null, "/images/HomeService/13-BarbarivJabejayi/04.jpg", "حمل بار بین شهری", 13 },
                    { 99, 1500000m, "ارائه خدمات بسته بندی حرفه‌ای برای اسباب کشی و جابجایی کالا.", null, "/images/HomeService/13-BarbarivJabejayi/07.jpg", "سرویس بسته بندی", 13 },
                    { 100, 1000000m, "اعزام کارگر برای کمک به جابه‌جایی و حمل وسایل.", null, "/images/HomeService/13-BarbarivJabejayi/08.jpg", "کارگر جابه جایی", 13 },
                    { 101, 2500000m, "خدمات جابه‌جایی گاوصندوق‌های سنگین با تجهیزات ویژه.", null, "/images/HomeService/13-BarbarivJabejayi/01.jpg", "جابه جایی گاوصندوق", 13 },
                    { 102, 3500000m, "حمل و نقل نخاله‌ها و ضایعات ساختمانی با ماشین‌های مخصوص.", null, "/images/HomeService/13-BarbarivJabejayi/05.jpg", "حمل نخاله و ضایعات ساختمانی", 13 },
                    { 103, 2000000m, "اجاره انبار و سوله برای نگهداری کالاها و وسایل.", null, "/images/HomeService/13-BarbarivJabejayi/02.jpg", "اجاره انبار و سوله", 13 },
                    { 104, 4000000m, "خدمات اسباب‌کشی ویژه برای شرکت‌ها و ادارات.", null, "/images/HomeService/13-BarbarivJabejayi/03.jpg", "اسباب کشی شرکت‌ها", 13 },
                    { 105, 500000m, "خدمات باتری به باتری خودرو در مواقع ضروری.", null, "/images/HomeService/14-khadamatkhodro/01.jpg", "باتری به باتری", 14 },
                    { 106, 800000m, "تعمیرات و سرویس‌های برق خودرو و باتری‌های آن.", null, "/images/HomeService/14-khadamatkhodro/04.jpg", "برق و باتری خودرو", 14 },
                    { 107, 1500000m, "خدمات مکانیکی خودرو شامل تعمیرات و سرویس‌های مختلف.", null, "/images/HomeService/14-khadamatkhodro/05.jpg", "مکانیکی خودرو", 14 },
                    { 108, 1200000m, "خدمات امداد جاده‌ای برای خودروهایی که دچار مشکل شده‌اند.", null, "/images/HomeService/14-khadamatkhodro/10.jpg", "امداد خودرو", 14 },
                    { 109, 2000000m, "خدمات کارشناسی خودرو برای خرید، فروش و ارزیابی وضعیت فنی خودرو.", null, "/images/HomeService/14-khadamatkhodro/06.jpg", "کارشناسی خودرو", 14 },
                    { 110, 3000000m, "خدمات حمل خودرو از مکانی به مکان دیگر با تجهیزات تخصصی.", null, "/images/HomeService/14-khadamatkhodro/02.jpg", "حمل خودرو", 14 },
                    { 111, 500000m, "خدمات پنچرگیری برای انواع خودروها.", null, "/images/HomeService/14-khadamatkhodro/03.jpg", "پنچر گیری", 14 },
                    { 112, 700000m, "خدمات تعویض لاستیک خودرو با توجه به نیاز و نوع خودرو.", null, "/images/HomeService/14-khadamatkhodro/08.jpg", "تعویض لاستیک", 14 },
                    { 113, 600000m, "خدمات تست دیاگ خودرو برای شناسایی ایرادات و مشکلات فنی.", null, "/images/HomeService/14-khadamatkhodro/09.jpg", "تست دیاگ", 14 },
                    { 114, 800000m, "خدمات تعویض لنت خودرو برای بهبود عملکرد ترمز.", null, "/images/HomeService/14-khadamatkhodro/11.jpg", "تعویض لنت خودرو", 14 },
                    { 115, 400000m, "خدمات تعویض شمع خودرو برای بهبود عملکرد موتور.", null, "/images/HomeService/14-khadamatkhodro/07.jpg", "تعویض شمع خودرو", 14 },
                    { 116, 2000000m, "خدمات سرامیک بدنه خودرو برای حفظ رنگ و جلوگیری از آسیب‌ها.", null, "/images/HomeService/15-Karvash/08.jpg", "سرامیک خودرو", 15 },
                    { 117, 800000m, "کارواش با استفاده از تکنولوژی نانو برای شستشو و محافظت بیشتر.", null, "/images/HomeService/15-Karvash/02.jpg", "کارواش نانو", 15 },
                    { 118, 500000m, "خدمات کارواش با استفاده از آب برای شستشوی خودرو.", null, "/images/HomeService/15-Karvash/01.jpg", "کارواش با آب", 15 },
                    { 119, 1500000m, "خدمات واکس و پولیش برای براق کردن و حفظ بدنه خودرو.", null, "/images/HomeService/15-Karvash/04.jpg", "واکس و پولیش خودرو", 15 },
                    { 120, 2000000m, "خدمات صفر شویی برای تمیز کردن کامل خودرو به صورت حرفه‌ای.", null, "/images/HomeService/15-Karvash/09.jpg", "صفر شویی خودرو", 15 },
                    { 121, 1200000m, "خدمات شستشوی موتور خودرو برای حفظ عملکرد بهتر.", null, "/images/HomeService/15-Karvash/05.jpg", "موتور شویی خودرو", 15 },
                    { 122, 2500000m, "خدمات احیای رنگ خودرو برای بازگرداندن درخشش و زیبایی رنگ.", null, "/images/HomeService/15-Karvash/03.jpg", "احیا رنگ خودرو", 15 },
                    { 123, 1000000m, "نصب شیشه دودی برای جلوگیری از تابش نور مستقیم و حفظ حریم خصوصی.", null, "/images/HomeService/15-Karvash/07.jpg", "نصب شیشه دودی", 15 },
                    { 124, 300000m, "خدمات آرایشی ناخن شامل کاشت و طراحی ناخن.", null, "/images/HomeService/16-zibayiBanovan/02.jpg", "خدمات ناخن", 16 },
                    { 125, 600000m, "خدمات رنگ مو برای بانوان با استفاده از محصولات باکیفیت.", null, "/images/HomeService/16-zibayiBanovan/07.jpg", "رنگ مو بانوان", 16 },
                    { 126, 800000m, "خدمات مش لایت و بالیاژ برای ایجاد ظاهر مدرن و طبیعی.", null, "/images/HomeService/16-zibayiBanovan/03.jpg", "مش لایت بالیاژ", 16 },
                    { 127, 200000m, "خدمات براشینگ مو برای ظاهر صاف و براق.", null, "/images/HomeService/16-zibayiBanovan/05.jpg", "براشینگ مو بانوان", 16 },
                    { 128, 100000m, "خدمات اصلاح صورت برای داشتن پوستی صاف و شاداب.", null, "/images/HomeService/16-zibayiBanovan/04.jpg", "اصلاح صورت", 16 },
                    { 129, 400000m, "خدمات کوتاهی مو بانوان با استفاده از تکنیک‌های روز.", null, "/images/HomeService/16-zibayiBanovan/06.jpg", "کوتاهی مو بانوان", 16 },
                    { 130, 1000000m, "خدمات کراتینه و ویتامینه برای تقویت و نرم شدن مو.", null, "/images/HomeService/16-zibayiBanovan/10.jpg", "کراتینه و ویتامینه مو", 16 },
                    { 131, 500000m, "خدمات کاشت مژه برای داشتن مژه‌هایی بلند و جذاب.", null, "/images/HomeService/16-zibayiBanovan/01.jpg", "کاشت مژه", 16 },
                    { 132, 600000m, "خدمات لمینت و لیفت مژه برای فرم‌دهی به مژه‌ها.", null, "/images/HomeService/16-zibayiBanovan/09.jpg", "لمینت و لیفت مژه", 16 },
                    { 133, 300000m, "خدمات اپیلاسیون در منزل برای راحتی شما.", null, "/images/HomeService/16-zibayiBanovan/00.jpg", "اپیلاسیون در خانه", 16 },
                    { 134, 300000m, "خدمات مراقبت و نگهداری از بیمار در منزل.", null, "/images/HomeService/17-Parastari/01.jpg", "مراقبت و نگهداری", 17 },
                    { 135, 150000m, "خدمات پرستاری و تزریقات در منزل برای بیماران.", null, "/images/HomeService/17-Parastari/05.jpg", "پرستاری و تزریقات", 17 },
                    { 136, 500000m, "خدمات معاینه پزشکی برای تشخیص بیماری‌ها و ارائه درمان‌های لازم.", null, "/images/HomeService/17-Parastari/02.jpg", "معاینه پزشکی", 17 },
                    { 137, 400000m, "خدمات پیراپزشکی شامل فیزیوتراپی، ارتوپدی، و سایر خدمات تخصصی.", null, "/images/HomeService/17-Parastari/03.jpg", "پیرا پزشکی", 17 },
                    { 138, 200000m, "خدمات آزمایشگاهی و نمونه‌گیری در منزل برای تشخیص دقیق‌تر بیماری‌ها.", null, "/images/HomeService/17-Parastari/04.jpg", "آزمایش و نمونه گیری", 17 },
                    { 139, 1000000m, "خدمات هتل‌های حیوانات خانگی برای مراقبت از حیوانات در طول مدت غیبت صاحب آن‌ها.", null, "/images/HomeService/18-heyvanat/04.jpg", "هتل های حیوانات خانگی", 18 },
                    { 140, 500000m, "خدمات دامپزشکی در محل برای معاینه و درمان حیوانات خانگی.", null, "/images/HomeService/18-heyvanat/02.jpg", "خدمات دامپزشکی در محل", 18 },
                    { 141, 700000m, "خدمات تربیتی برای حیوانات خانگی جهت آموزش رفتارهای مطلوب.", null, "/images/HomeService/18-heyvanat/01.jpg", "خدمات تربیتی حیوانات خانگی", 18 },
                    { 142, 300000m, "خدمات شستشو و حمام برای حیوانات خانگی برای بهداشت و تمیزی.", null, "/images/HomeService/18-heyvanat/05.jpg", "خدمات شستشو حیوانات خانگی", 18 },
                    { 143, 100000m, "فروشگاه لوازم حیوانات خانگی شامل غذا، اسباب‌بازی و لوازم جانبی.", null, "/images/HomeService/18-heyvanat/03.jpg", "پت شاپ", 18 },
                    { 144, 500000m, "خدمات مشاوره در زمینه مسائل مالی و مالیاتی برای کسب‌وکارها و افراد.", null, "/images/HomeService/19-Moshavere/01.jpg", "مشاوره مالی و مالیاتی", 19 },
                    { 145, 200000m, "خدمات کوتاهی موی سر و اصلاح صورت برای آقایان.", null, "/images/HomeService/20-PirayeshAghayan/02.jpg", "کوتاهی موی سر و اصلاح صورت", 20 },
                    { 146, 300000m, "خدمات مراقبت از پوست، مو و زیبایی برای آقایان.", null, "/images/HomeService/20-PirayeshAghayan/03.jpg", "مراقبت های زیبایی آقایان", 20 },
                    { 147, 500000m, "خدمات گریم و آرایش داماد برای روز عروسی.", null, "/images/HomeService/20-PirayeshAghayan/01.jpg", "گریم داماد", 20 },
                    { 148, 350000m, "کلاس‌های تمرینی سی ایکس در منزل برای تقویت بدن و عضلات.", null, "/images/HomeService/21-Varzesh/03.jpg", "کلاس سی ایکس در خانه", 21 },
                    { 149, 300000m, "ارائه برنامه‌های ورزشی اختصاصی در منزل برای تناسب اندام.", null, "/images/HomeService/21-Varzesh/05.jpg", "برنامه ورزشی در خانه", 21 },
                    { 150, 400000m, "کلاس‌های یوگا در منزل برای افزایش انعطاف‌پذیری و آرامش ذهنی.", null, "/images/HomeService/21-Varzesh/02.jpg", "کلاس یوگا در خانه", 21 },
                    { 151, 350000m, "کلاس‌های پیلاتس در منزل برای تقویت بدن و بهبود وضعیت posture.", null, "/images/HomeService/21-Varzesh/04.jpg", "کلاس پیلاتس در خانه", 21 },
                    { 152, 400000m, "کلاس‌های فیتنس برای افزایش قدرت بدنی و تناسب اندام در منزل.", null, "/images/HomeService/21-Varzesh/06.jpg", "کلاس فیتنس در خانه", 21 },
                    { 153, 250000m, "تمرینات اصلاحی برای بهبود وضعیت بدن و جلوگیری از آسیب‌ها.", null, "/images/HomeService/21-Varzesh/01.jpg", "حرکت اصلاحی", 21 },
                    { 154, 1000000m, "خدمات مخصوص سازمان‌های کوچک مانند حسابداری، مشاوره، پشتیبانی و غیره.", null, "/images/HomeService/21-Varzesh/02.jpg", "خدمات شرکتی با سازمان کوچک", 22 },
                    { 155, 3000000m, "خدمات مدیریتی و پشتیبانی برای سازمان‌های بزرگ.", null, "/images/HomeService/21-Varzesh/03.jpg", "خدمات شرکتی با سازمان بزرگ", 22 },
                    { 156, 1500000m, "خدمات پشتیبانی برای تیم‌های فروش از جمله آموزش، مشاوره، و ارائه راهکارهای فروش.", null, "/images/HomeService/21-Varzesh/01.jpg", "پشتیبانی فروش", 22 },
                    { 157, 2000000m, "خدمات مربوط به استخدام خدمتگزار خانگی برای انجام امور منزل.", null, "/images/HomeService/23-TaminNiroyeEnsani/01.jpg", "استخدام خدمتگزار", 23 },
                    { 158, 100000m, "خدمات تعمیرات و ترمیم لباس‌های مختلف مانند پاره شدن و تغییر سایز.", null, "/images/HomeService/24-TamiratLebas/02.jpg", "تعمیرات لباس", 24 },
                    { 159, 200000m, "خدمات دوخت و طراحی لباس‌های زنانه.", null, "/images/HomeService/24-TamiratLebas/03.jpg", "دوخت لباس زنانه", 24 },
                    { 160, 150000m, "خدمات تعمیر کیف و کفش از جمله تعویض بند، ترمیم و رنگ‌آمیزی.", null, "/images/HomeService/24-TamiratLebas/01.jpg", "تعمیر کیف و کفش", 24 },
                    { 161, 1000000m, "خدمات تهیه کیک و شیرینی مخصوص مجالس و مهمانی‌ها.", null, "/images/HomeService/25-Majales/09.jpg", "کیک و شیرینی", 25 },
                    { 162, 200000m, "خدمات ارسال هدیه‌های متنوع برای عزیزان شما.", null, "/images/HomeService/25-Majales/01.jpg", "ارسال هدیه", 25 },
                    { 163, 300000m, "خدمات مهمانداری برای مجالس و مهمانی‌ها.", null, "/images/HomeService/25-Majales/02.jpg", "مهماندار مجالس", 25 },
                    { 164, 1500000m, "خدمات آماده‌سازی و سرو فینگر فود برای مهمانی‌ها.", null, "/images/HomeService/25-Majales/08.jpg", "فینگر فود", 25 },
                    { 165, 5000000m, "خدمات عکاسی و فیلمبرداری حرفه‌ای برای مجالس و مهمانی‌ها.", null, "/images/HomeService/25-Majales/05.jpg", "عکاس و فیلمبرداری", 25 },
                    { 166, 2000000m, "خدمات موسیقی برای مجالس و جشن‌ها.", null, "/images/HomeService/25-Majales/03.jpg", "موزیک", 25 },
                    { 167, 4000000m, "خدمات تشریفات و دکوراسیون برای مجالس و جشن‌ها.", null, "/images/HomeService/25-Majales/07.jpg", "تشریفات مجالس", 25 },
                    { 168, 3500000m, "خدمات آشپزی و سرو غذا در محل برای مهمانی‌ها.", null, "/images/HomeService/25-Majales/06.jpg", "آشپری در محل", 25 },
                    { 169, 1000000m, "خدمات سفره‌آرایی برای مجالس و مهمانی‌ها.", null, "/images/HomeService/25-Majales/10.jpg", "سفره آرایی", 25 },
                    { 170, 2000000m, "خدمات دکوراسیون تولد و جشن‌های دیگر.", null, "/images/HomeService/25-Majales/04.jpg", "دکور تولد", 25 },
                    { 171, 1500000m, "خدمات آمادگی برای کنکور در رشته‌های مختلف.", null, "/images/HomeService/26-Amozesh/02.jpg", "آمادگی برای کنکور", 26 },
                    { 172, 5000000m, "دوره‌های آموزشی دانشگاه گروه صنعتی گلرنگ.", null, "/images/HomeService/26-Amozesh/01.jpg", "دانشگاه گروه صنعتی گلرنگ", 26 },
                    { 173, 1000000m, "دوره‌های آموزش زبان‌های خارجی با اساتید مجرب.", null, "/images/HomeService/26-Amozesh/04.jpg", "آموزش زبان‌های خارجی", 26 },
                    { 174, 800000m, "دوره‌های آموزش دروس از ابتدایی تا متوسطه.", null, "/images/HomeService/26-Amozesh/03.jpg", "آموزش ابتدایی تا متوسطه", 26 },
                    { 175, 2000000m, "دوره‌های تخصصی و دروس دانشگاهی برای دانشجویان.", null, "/images/HomeService/26-Amozesh/05.jpg", "آموزش دروس دانشگاهی", 26 },
                    { 176, 500000m, "خدمات امداد و درمان فوری برای حوادث و بیماری‌های مختلف.", null, "/images/HomeService/27-KhadamatFori/05.jpg", "امداد و درمان", 27 },
                    { 177, 200000m, "خدمات باز کردن درب خودرو در مواقع اضطراری.", null, "/images/HomeService/27-KhadamatFori/04.jpg", "باز کردن درب خودرو", 27 },
                    { 178, 150000m, "خدمات ساخت کلید برای انواع درب‌ها و خودروها.", null, "/images/HomeService/27-KhadamatFori/01.jpg", "کلید سازی", 27 },
                    { 179, 300000m, "خدمات تعمیر و تعویض جک و ریموت خودرو.", null, "/images/HomeService/27-KhadamatFori/02.jpg", "جک و ریموت", 27 },
                    { 180, 250000m, "خدمات رفع اتصالی و قطعی برق در منازل و خودروها.", null, "/images/HomeService/27-KhadamatFori/03.jpg", "رفع اتصالی و قطعی برق", 27 },
                    { 181, 5000000m, "خدمات فروش گوسفند زنده برای مصرف خوراکی و دیگر موارد.", null, "/images/HomeService/28-HameFanHarif/01.jpg", "گوسفند زنده", 28 },
                    { 182, 50000m, "خدمات تایپ متون و اسناد به صورت دقیق و سریع.", null, "/images/HomeService/28-HameFanHarif/01.jpg", "تایپ متون", 28 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "BasePrice", "CustomerId", "Description", "ExecutionDate", "ExecutionTime", "ExpertsId", "HomeServiceId", "IsApproved", "IsDeleted", "OrderStatus", "PaymentStatus", "RequestDate" },
                values: new object[,]
                {
                    { 1, 1500.00m, 1, "پروژه برای طراحی و توسعه وب سایت", new DateTime(2025, 3, 17, 7, 59, 31, 733, DateTimeKind.Local).AddTicks(275), new TimeSpan(5, 0, 0, 0, 0), null, 1, true, false, 1, 1, new DateTime(2025, 3, 7, 7, 59, 31, 733, DateTimeKind.Local).AddTicks(250) },
                    { 2, 2000.00m, 2, "پروژه طراحی اپلیکیشن موبایل", new DateTime(2025, 3, 20, 7, 59, 31, 733, DateTimeKind.Local).AddTicks(306), new TimeSpan(7, 0, 0, 0, 0), null, 2, true, false, 2, 2, new DateTime(2025, 3, 8, 7, 59, 31, 733, DateTimeKind.Local).AddTicks(302) },
                    { 3, 2500.00m, 3, "پروژه طراحی سیستم مدیریت محتوا", new DateTime(2025, 3, 25, 7, 59, 31, 733, DateTimeKind.Local).AddTicks(314), new TimeSpan(10, 0, 0, 0, 0), null, 3, false, false, 3, 1, new DateTime(2025, 3, 9, 7, 59, 31, 733, DateTimeKind.Local).AddTicks(311) },
                    { 4, 3000.00m, 2, "پروژه طراحی و توسعه فروشگاه آنلاین", new DateTime(2025, 3, 13, 7, 59, 31, 733, DateTimeKind.Local).AddTicks(324), new TimeSpan(3, 0, 0, 0, 0), null, 4, true, false, 4, 1, new DateTime(2025, 3, 5, 7, 59, 31, 733, DateTimeKind.Local).AddTicks(321) },
                    { 5, 1000.00m, 3, "پروژه مشاوره و آموزش آنلاین", new DateTime(2025, 3, 30, 7, 59, 31, 733, DateTimeKind.Local).AddTicks(331), new TimeSpan(15, 0, 0, 0, 0), null, 5, true, false, 1, 2, new DateTime(2025, 2, 28, 7, 59, 31, 733, DateTimeKind.Local).AddTicks(328) }
                });

            migrationBuilder.InsertData(
                table: "ExpertProposals",
                columns: new[] { "Id", "CommentId", "CustomerSelectionDate", "ExpertId", "IsApproved", "IsDeleted", "IsSelectedByCustomer", "OrderId", "ProposalDate", "ProposalDescription", "ProposalStatus", "ProposedExecutionTime", "ProposedPrice", "WorkCompletionDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 3, 11, 7, 59, 31, 735, DateTimeKind.Local).AddTicks(5522), 1, true, false, false, 1, new DateTime(2025, 3, 5, 7, 59, 31, 735, DateTimeKind.Local).AddTicks(5488), "پیشنهاد انجام پروژه طراحی وب سایت", 1, new TimeSpan(60, 0, 0, 0, 0), 2000.00m, new DateTime(2025, 5, 10, 7, 59, 31, 735, DateTimeKind.Local).AddTicks(5511) },
                    { 2, 2, new DateTime(2025, 3, 12, 7, 59, 31, 735, DateTimeKind.Local).AddTicks(5536), 2, false, false, false, 2, new DateTime(2025, 3, 6, 7, 59, 31, 735, DateTimeKind.Local).AddTicks(5529), "پیشنهاد انجام پروژه طراحی اپلیکیشن موبایل", 3, new TimeSpan(30, 0, 0, 0, 0), 1500.00m, new DateTime(2025, 4, 10, 7, 59, 31, 735, DateTimeKind.Local).AddTicks(5532) },
                    { 3, 3, new DateTime(2025, 3, 13, 7, 59, 31, 735, DateTimeKind.Local).AddTicks(5548), 3, true, false, true, 3, new DateTime(2025, 3, 7, 7, 59, 31, 735, DateTimeKind.Local).AddTicks(5541), "پیشنهاد انجام پروژه ساخت وب سایت فروشگاهی", 1, new TimeSpan(90, 0, 0, 0, 0), 2500.00m, new DateTime(2025, 6, 10, 7, 59, 31, 735, DateTimeKind.Local).AddTicks(5544) },
                    { 4, 4, new DateTime(2025, 3, 15, 7, 59, 31, 735, DateTimeKind.Local).AddTicks(5561), 2, false, false, false, 4, new DateTime(2025, 3, 8, 7, 59, 31, 735, DateTimeKind.Local).AddTicks(5554), "پیشنهاد انجام پروژه طراحی لوگو", 1, new TimeSpan(15, 0, 0, 0, 0), 1800.00m, new DateTime(2025, 4, 10, 7, 59, 31, 735, DateTimeKind.Local).AddTicks(5557) },
                    { 5, 5, new DateTime(2025, 3, 14, 7, 59, 31, 735, DateTimeKind.Local).AddTicks(5572), 3, true, false, false, 5, new DateTime(2025, 3, 9, 7, 59, 31, 735, DateTimeKind.Local).AddTicks(5566), "پیشنهاد انجام پروژه طراحی گرافیکی", 1, new TimeSpan(45, 0, 0, 0, 0), 2200.00m, new DateTime(2025, 4, 10, 7, 59, 31, 735, DateTimeKind.Local).AddTicks(5569) }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "ExpertId", "ExpertProposalId", "IsApproved", "IsDeleted", "Rating", "Text" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 8, 7, 59, 31, 739, DateTimeKind.Local).AddTicks(7948), 1, 1, 1, true, false, 4.5, "پیشنهاد خوبی است ولی نیاز به اصلاحاتی دارد." },
                    { 2, new DateTime(2025, 3, 9, 7, 59, 31, 739, DateTimeKind.Local).AddTicks(7966), 2, 2, 2, true, false, 5.0, "عالی بود، همه چیز به درستی انجام شد." },
                    { 3, new DateTime(2025, 3, 7, 7, 59, 31, 739, DateTimeKind.Local).AddTicks(7990), 3, 3, 3, false, false, 3.5, "کار شما خوب است ولی زمان تحویل کمی دیر بود." },
                    { 4, new DateTime(2025, 3, 5, 7, 59, 31, 739, DateTimeKind.Local).AddTicks(7994), 2, 2, 4, true, false, 4.0, "پیشنهادها عالی بودند، فقط نیاز به هماهنگی بیشتر با تیم داشتیم." },
                    { 5, new DateTime(2025, 3, 6, 7, 59, 31, 739, DateTimeKind.Local).AddTicks(8001), 3, 3, 5, true, false, 5.0, "کار خیلی خوب و سریع انجام شد، از همکاری با شما راضی هستم." }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CityId",
                table: "AspNetUsers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CustomerId",
                table: "Comments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ExpertId",
                table: "Comments",
                column: "ExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ExpertProposalId",
                table: "Comments",
                column: "ExpertProposalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpertProposals_ExpertId",
                table: "ExpertProposals",
                column: "ExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertProposals_OrderId",
                table: "ExpertProposals",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Experts_UserId",
                table: "Experts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpertSkills_ExpertId",
                table: "ExpertSkills",
                column: "ExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertSkills_HomeServiceId",
                table: "ExpertSkills",
                column: "HomeServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeServices_ExpertsId",
                table: "HomeServices",
                column: "ExpertsId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeServices_SubCategoryId",
                table: "HomeServices",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ExpertsId",
                table: "Orders",
                column: "ExpertsId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_HomeServiceId",
                table: "Orders",
                column: "HomeServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_OrdersId",
                table: "Pictures",
                column: "OrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategories",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "ExpertSkills");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ExpertProposals");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "HomeServices");

            migrationBuilder.DropTable(
                name: "Experts");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
