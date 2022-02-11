using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lipar.Data.Migrations
{
    public partial class initlipar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "General");

            migrationBuilder.EnsureSchema(
                name: "Application");

            migrationBuilder.EnsureSchema(
                name: "Financial");

            migrationBuilder.EnsureSchema(
                name: "Portal");

            migrationBuilder.EnsureSchema(
                name: "Organization");

            migrationBuilder.EnsureSchema(
                name: "Core");

            migrationBuilder.CreateTable(
                name: "ActivityLogTypes",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemKeyword = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttributeControlTypes",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeControlTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommandTypes",
                schema: "Organization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommentStatuses",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactUsTypes",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUsTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentTypes",
                schema: "Organization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiscountTypes",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailAccounts",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Host = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Port = table.Column<int>(type: "int", nullable: false),
                    EnableSsl = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnabledTypes",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnabledTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FaqGroups",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaqGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GenericAttributes",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KeyGroup = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Key = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenericAttributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LanguageCultures",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Seo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageCultures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medias",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    AltAttribute = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    MimeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PasswordFormatTypes",
                schema: "Organization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordFormatTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PositionTypes",
                schema: "Organization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributes",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(1000)", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(2000)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelatedProducts",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId2 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTypes",
                schema: "Organization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ViewStatuses",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivityLogs",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comment = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    ActivityLogTypeId = table.Column<int>(type: "int", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityName = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityLogs_ActivityLogTypes_ActivityLogTypeId",
                        column: x => x.ActivityLogTypeId,
                        principalSchema: "General",
                        principalTable: "ActivityLogTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Commands",
                schema: "Organization",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CommandTypeId = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RemoverId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RemoveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commands_Commands_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Organization",
                        principalTable: "Commands",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Commands_CommandTypes_CommandTypeId",
                        column: x => x.CommandTypeId,
                        principalSchema: "Organization",
                        principalTable: "CommandTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactUs",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactUsTypeId = table.Column<int>(type: "int", nullable: false),
                    Body = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactUs_ContactUsTypes_ContactUsTypeId",
                        column: x => x.ContactUsTypeId,
                        principalSchema: "General",
                        principalTable: "ContactUsTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Provinces_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "General",
                        principalTable: "Countries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MessageTemplates",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Template = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EmailAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageTemplates_EmailAccounts_EmailAccountId",
                        column: x => x.EmailAccountId,
                        principalSchema: "General",
                        principalTable: "EmailAccounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QueuedEmails",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriorityId = table.Column<int>(type: "int", nullable: false),
                    From = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FromName = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    To = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ToName = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimeSend = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueuedEmails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QueuedEmails_EmailAccounts_EmailAccountId",
                        column: x => x.EmailAccountId,
                        principalSchema: "General",
                        principalTable: "EmailAccounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Centers",
                schema: "Organization",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnabledTypeId = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Centers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Centers_EnabledTypes_EnabledTypeId",
                        column: x => x.EnabledTypeId,
                        principalSchema: "Core",
                        principalTable: "EnabledTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryDates",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: true),
                    EnabledTypeId = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    RemoverId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RemoveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryDates_EnabledTypes_EnabledTypeId",
                        column: x => x.EnabledTypeId,
                        principalSchema: "Core",
                        principalTable: "EnabledTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShippingCosts",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: true),
                    Price = table.Column<decimal>(type: "DECIMAL(18,3)", nullable: false),
                    EnabledTypeId = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    RemoverId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RemoveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingCosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShippingCosts_EnabledTypes_EnabledTypeId",
                        column: x => x.EnabledTypeId,
                        principalSchema: "Core",
                        principalTable: "EnabledTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Faqs",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FaqGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Question = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    Answer = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faqs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Faqs_FaqGroups_FaqGroupId",
                        column: x => x.FaqGroupId,
                        principalSchema: "General",
                        principalTable: "FaqGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MediaBinaries",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MediaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BinaryData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaBinaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaBinaries_Medias_MediaId",
                        column: x => x.MediaId,
                        principalSchema: "General",
                        principalTable: "Medias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Organization",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnabledTypeId = table.Column<int>(type: "int", nullable: false),
                    UserGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserTypeId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    NationalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailVerified = table.Column<bool>(type: "bit", nullable: false),
                    CellPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CellPhoneVerified = table.Column<bool>(type: "bit", nullable: false),
                    LastIpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FailedLoginAttempts = table.Column<int>(type: "int", nullable: true),
                    CannotLoginUntilDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoverId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RemoveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_EnabledTypes_EnabledTypeId",
                        column: x => x.EnabledTypeId,
                        principalSchema: "Core",
                        principalTable: "EnabledTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Genders_GenderId",
                        column: x => x.GenderId,
                        principalSchema: "Core",
                        principalTable: "Genders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_UserTypes_UserTypeId",
                        column: x => x.UserTypeId,
                        principalSchema: "Organization",
                        principalTable: "UserTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    LanguageCultureId = table.Column<int>(type: "int", nullable: false),
                    UniqueSeoCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViewStatusId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Languages_LanguageCultures_LanguageCultureId",
                        column: x => x.LanguageCultureId,
                        principalSchema: "General",
                        principalTable: "LanguageCultures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Languages_ViewStatuses_ViewStatusId",
                        column: x => x.ViewStatusId,
                        principalSchema: "Core",
                        principalTable: "ViewStatuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalSchema: "General",
                        principalTable: "Provinces",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                schema: "Organization",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnabledTypeId = table.Column<int>(type: "int", nullable: false),
                    DepartmentTypeId = table.Column<int>(type: "int", nullable: false),
                    CenterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Centers_CenterId",
                        column: x => x.CenterId,
                        principalSchema: "Organization",
                        principalTable: "Centers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Departments_Departments_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Organization",
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Departments_DepartmentTypes_DepartmentTypeId",
                        column: x => x.DepartmentTypeId,
                        principalSchema: "Organization",
                        principalTable: "DepartmentTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Departments_EnabledTypes_EnabledTypeId",
                        column: x => x.EnabledTypeId,
                        principalSchema: "Core",
                        principalTable: "EnabledTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "Organization",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CenterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RemoverId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RemoveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Centers_CenterId",
                        column: x => x.CenterId,
                        principalSchema: "Organization",
                        principalTable: "Centers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                schema: "Financial",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PaymentUri = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ServiceUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RedirectUri = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TransactionCost = table.Column<int>(type: "int", nullable: true),
                    EnabledTypeId = table.Column<int>(type: "int", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Banks_EnabledTypes_EnabledTypeId",
                        column: x => x.EnabledTypeId,
                        principalSchema: "Core",
                        principalTable: "EnabledTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Banks_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Organization",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IncludeInTopMenu = table.Column<bool>(type: "bit", nullable: false),
                    EnabledTypeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RemoverId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RemoveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Application",
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Categories_EnabledTypes_EnabledTypeId",
                        column: x => x.EnabledTypeId,
                        principalSchema: "Core",
                        principalTable: "EnabledTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Categories_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Organization",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "Portal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RemoverId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RemoveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Portal",
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Categories_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Organization",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DynamicPages",
                schema: "Portal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(1000)", nullable: false),
                    Title = table.Column<string>(type: "NVARCHAR(1000)", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    IncludeInTopMenu = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ViewStatusId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: true),
                    RemoverId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RemoveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DynamicPages_Users_RemoverId",
                        column: x => x.RemoverId,
                        principalSchema: "Organization",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DynamicPages_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Organization",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DynamicPages_ViewStatuses_ViewStatusId",
                        column: x => x.ViewStatusId,
                        principalSchema: "Core",
                        principalTable: "ViewStatuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Galleries",
                schema: "Portal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(MAX)", maxLength: 3000, nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViewStatusId = table.Column<int>(type: "int", nullable: false),
                    VisitedCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MetaKeywords = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RemoverId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RemoveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galleries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Galleries_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Organization",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Galleries_ViewStatuses_ViewStatusId",
                        column: x => x.ViewStatusId,
                        principalSchema: "Core",
                        principalTable: "ViewStatuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StaticPages",
                schema: "Portal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    Title = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    Body = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    MetaKeywords = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    MetaDescription = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    IncludeInTopMenu = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    ViewStatusId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: true),
                    RemoverId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RemoveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaticPages_Users_RemoverId",
                        column: x => x.RemoverId,
                        principalSchema: "Organization",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StaticPages_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Organization",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StaticPages_ViewStatuses_ViewStatusId",
                        column: x => x.ViewStatusId,
                        principalSchema: "Core",
                        principalTable: "ViewStatuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserPasswords",
                schema: "Organization",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordFormatTypeId = table.Column<int>(type: "int", nullable: false),
                    PasswordSalt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPasswords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPasswords_PasswordFormatTypes_PasswordFormatTypeId",
                        column: x => x.PasswordFormatTypeId,
                        principalSchema: "Organization",
                        principalTable: "PasswordFormatTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserPasswords_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Organization",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LocaleStringResources",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourceName = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    ResourceValue = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocaleStringResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocaleStringResources_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "General",
                        principalTable: "Languages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menus_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "General",
                        principalTable: "Languages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UrlRecords",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    EnabledTypeId = table.Column<int>(type: "int", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UrlRecords_EnabledTypes_EnabledTypeId",
                        column: x => x.EnabledTypeId,
                        principalSchema: "Core",
                        principalTable: "EnabledTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UrlRecords_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "General",
                        principalTable: "Languages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserAddresses",
                schema: "Organization",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAddresses_Cities_CityId",
                        column: x => x.CityId,
                        principalSchema: "General",
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserAddresses_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "General",
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserAddresses_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalSchema: "General",
                        principalTable: "Provinces",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserAddresses_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Organization",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                schema: "Organization",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PositionTypeId = table.Column<int>(type: "int", nullable: false),
                    EnabledTypeId = table.Column<int>(type: "int", nullable: false),
                    Default = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CenterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RemoverId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RemoveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positions_Centers_CenterId",
                        column: x => x.CenterId,
                        principalSchema: "Organization",
                        principalTable: "Centers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Positions_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "Organization",
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Positions_EnabledTypes_EnabledTypeId",
                        column: x => x.EnabledTypeId,
                        principalSchema: "Core",
                        principalTable: "EnabledTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Positions_PositionTypes_PositionTypeId",
                        column: x => x.PositionTypeId,
                        principalSchema: "Organization",
                        principalTable: "PositionTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Positions_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Organization",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                schema: "Organization",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Commands_CommandId",
                        column: x => x.CommandId,
                        principalSchema: "Organization",
                        principalTable: "Commands",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Organization",
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BankPorts",
                schema: "Financial",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MerchantId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MerchantKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Password = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    TerminalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: true),
                    EnabledTypeId = table.Column<int>(type: "int", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankPorts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankPorts_Banks_BankId",
                        column: x => x.BankId,
                        principalSchema: "Financial",
                        principalTable: "Banks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BankPorts_EnabledTypes_EnabledTypeId",
                        column: x => x.EnabledTypeId,
                        principalSchema: "Core",
                        principalTable: "EnabledTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShowOnHomePage = table.Column<bool>(type: "bit", nullable: false),
                    MetaKeywords = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaTitle = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    AllowCustomerReviews = table.Column<bool>(type: "bit", nullable: false),
                    CallForPrice = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "DECIMAL(18,3)", nullable: false),
                    SpecialOffer = table.Column<bool>(type: "bit", nullable: false),
                    Discount = table.Column<decimal>(type: "DECIMAL(18,3)", nullable: true),
                    DiscountTypeId = table.Column<int>(type: "int", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Width = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Length = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    IsDownload = table.Column<bool>(type: "bit", nullable: false),
                    Published = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShippingCostId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeliveryDateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RemoverId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RemoveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "Application",
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_DeliveryDates_DeliveryDateId",
                        column: x => x.DeliveryDateId,
                        principalSchema: "Application",
                        principalTable: "DeliveryDates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_DiscountTypes_DiscountTypeId",
                        column: x => x.DiscountTypeId,
                        principalSchema: "Application",
                        principalTable: "DiscountTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_ShippingCosts_ShippingCostId",
                        column: x => x.ShippingCostId,
                        principalSchema: "Application",
                        principalTable: "ShippingCosts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Organization",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                schema: "Portal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Body = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    MetaKeywords = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    CommentStatusId = table.Column<int>(type: "int", nullable: false),
                    VisitedCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ViewStatusId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReadingTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageId = table.Column<int>(type: "int", nullable: true),
                    RemoverId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RemoveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "Portal",
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Blogs_CommentStatuses_CommentStatusId",
                        column: x => x.CommentStatusId,
                        principalSchema: "Core",
                        principalTable: "CommentStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Blogs_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "General",
                        principalTable: "Languages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Blogs_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Organization",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Blogs_ViewStatuses_ViewStatusId",
                        column: x => x.ViewStatusId,
                        principalSchema: "Core",
                        principalTable: "ViewStatuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "News",
                schema: "Portal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Body = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    MetaKeywords = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    ReadingTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentStatusId = table.Column<int>(type: "int", nullable: false),
                    VisitedCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ViewStatusId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: true),
                    RemoverId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RemoveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "Portal",
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_News_CommentStatuses_CommentStatusId",
                        column: x => x.CommentStatusId,
                        principalSchema: "Core",
                        principalTable: "CommentStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_News_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "General",
                        principalTable: "Languages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_News_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Organization",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_News_ViewStatuses_ViewStatusId",
                        column: x => x.ViewStatusId,
                        principalSchema: "Core",
                        principalTable: "ViewStatuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DynamicPageDetails",
                schema: "Portal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    Title = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    Body = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    MetaKeywords = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    MetaDescription = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    ViewStatusId = table.Column<int>(type: "int", nullable: false),
                    DynamicPageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RemoverId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RemoveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicPageDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DynamicPageDetails_DynamicPages_DynamicPageId",
                        column: x => x.DynamicPageId,
                        principalSchema: "Portal",
                        principalTable: "DynamicPages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DynamicPageDetails_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Organization",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DynamicPageDetails_ViewStatuses_ViewStatusId",
                        column: x => x.ViewStatusId,
                        principalSchema: "Core",
                        principalTable: "ViewStatuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Gallery_Media_Mapping",
                schema: "Portal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GalleryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MediaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gallery_Media_Mapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gallery_Media_Mapping_Galleries_GalleryId",
                        column: x => x.GalleryId,
                        principalSchema: "Portal",
                        principalTable: "Galleries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Gallery_Media_Mapping_Medias_MediaId",
                        column: x => x.MediaId,
                        principalSchema: "General",
                        principalTable: "Medias",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    MenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ViewStatusId = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Parameters = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItems_MenuItems_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "General",
                        principalTable: "MenuItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MenuItems_Menus_MenuId",
                        column: x => x.MenuId,
                        principalSchema: "General",
                        principalTable: "Menus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PositionRoles",
                schema: "Organization",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PositionRoles_Positions_PositionId",
                        column: x => x.PositionId,
                        principalSchema: "Organization",
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PositionRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Organization",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankPortId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShoppingCartRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_BankPorts_BankPortId",
                        column: x => x.BankPortId,
                        principalSchema: "Financial",
                        principalTable: "BankPorts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_UserAddresses_UserAddressId",
                        column: x => x.UserAddressId,
                        principalSchema: "Organization",
                        principalTable: "UserAddresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Organization",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Product_Media_Mapping",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MediaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Media_Mapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Media_Mapping_Medias_MediaId",
                        column: x => x.MediaId,
                        principalSchema: "General",
                        principalTable: "Medias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_Media_Mapping_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Application",
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Product_ProductAttribute_Mapping",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TextPrompt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttributeControlTypeId = table.Column<int>(type: "int", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductAttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_ProductAttribute_Mapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_ProductAttribute_Mapping_AttributeControlTypes_AttributeControlTypeId",
                        column: x => x.AttributeControlTypeId,
                        principalSchema: "Application",
                        principalTable: "AttributeControlTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_ProductAttribute_Mapping_ProductAttributes_ProductAttributeId",
                        column: x => x.ProductAttributeId,
                        principalSchema: "Application",
                        principalTable: "ProductAttributes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_ProductAttribute_Mapping_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Application",
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductComments",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentText = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    ReplyText = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    CommentStatusId = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductComments_CommentStatuses_CommentStatusId",
                        column: x => x.CommentStatusId,
                        principalSchema: "Core",
                        principalTable: "CommentStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductComments_ProductComments_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Application",
                        principalTable: "ProductComments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductComments_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Application",
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductComments_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Organization",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductQuestions",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionText = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    ViewStatusId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductQuestions_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Application",
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductQuestions_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Organization",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductQuestions_ViewStatuses_ViewStatusId",
                        column: x => x.ViewStatusId,
                        principalSchema: "Core",
                        principalTable: "ViewStatuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartItems",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShoppingCartItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    AttributeJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Application",
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Organization",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Blog_Media_Mapping",
                schema: "Portal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MediaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog_Media_Mapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blog_Media_Mapping_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalSchema: "Portal",
                        principalTable: "Blogs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Blog_Media_Mapping_Medias_MediaId",
                        column: x => x.MediaId,
                        principalSchema: "General",
                        principalTable: "Medias",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BlogComments",
                schema: "Portal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Body = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    CommentStatusId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogComments_BlogComments_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Portal",
                        principalTable: "BlogComments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BlogComments_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalSchema: "Portal",
                        principalTable: "Blogs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BlogComments_CommentStatuses_CommentStatusId",
                        column: x => x.CommentStatusId,
                        principalSchema: "Core",
                        principalTable: "CommentStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BlogComments_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Organization",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "News_Media_Mapping",
                schema: "Portal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MediaId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MediaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News_Media_Mapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_Media_Mapping_Medias_MediaId1",
                        column: x => x.MediaId1,
                        principalSchema: "General",
                        principalTable: "Medias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_News_Media_Mapping_News_MediaId",
                        column: x => x.MediaId,
                        principalSchema: "Portal",
                        principalTable: "News",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NewsComments",
                schema: "Portal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Body = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CommentStatusId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsComments_CommentStatuses_CommentStatusId",
                        column: x => x.CommentStatusId,
                        principalSchema: "Core",
                        principalTable: "CommentStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NewsComments_News_NewsId",
                        column: x => x.NewsId,
                        principalSchema: "Portal",
                        principalTable: "News",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NewsComments_NewsComments_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Portal",
                        principalTable: "NewsComments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NewsComments_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Organization",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductDiscountTypeId = table.Column<int>(type: "int", nullable: true),
                    ProductDiscountPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProductCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductCategoryName = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: true),
                    ShippingCostId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShippingCostName = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: true),
                    ShippingCostPriority = table.Column<int>(type: "int", nullable: false),
                    DeliveryDateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeliveryDateName = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: true),
                    DeliveryDatePriority = table.Column<int>(type: "int", nullable: false),
                    ProductAttributeJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "Application",
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributeValues",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(1000)", nullable: false),
                    Price = table.Column<decimal>(type: "DECIMAL(18,3)", nullable: true),
                    IsPreSelected = table.Column<bool>(type: "bit", nullable: false),
                    ProductAttributeMappingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributeValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttributeValues_Product_ProductAttribute_Mapping_ProductAttributeMappingId",
                        column: x => x.ProductAttributeMappingId,
                        principalSchema: "Application",
                        principalTable: "Product_ProductAttribute_Mapping",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductAnswers",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnswerText = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    ProductQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ViewStatusId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAnswers_ProductQuestions_ProductQuestionId",
                        column: x => x.ProductQuestionId,
                        principalSchema: "Application",
                        principalTable: "ProductQuestions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductAnswers_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Organization",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductAnswers_ViewStatuses_ViewStatusId",
                        column: x => x.ViewStatusId,
                        principalSchema: "Core",
                        principalTable: "ViewStatuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_ActivityLogTypeId",
                schema: "General",
                table: "ActivityLogs",
                column: "ActivityLogTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BankPorts_BankId",
                schema: "Financial",
                table: "BankPorts",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_BankPorts_EnabledTypeId",
                schema: "Financial",
                table: "BankPorts",
                column: "EnabledTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_EnabledTypeId",
                schema: "Financial",
                table: "Banks",
                column: "EnabledTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_UserId",
                schema: "Financial",
                table: "Banks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_Media_Mapping_BlogId",
                schema: "Portal",
                table: "Blog_Media_Mapping",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_Media_Mapping_MediaId",
                schema: "Portal",
                table: "Blog_Media_Mapping",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogComments_BlogId",
                schema: "Portal",
                table: "BlogComments",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogComments_CommentStatusId",
                schema: "Portal",
                table: "BlogComments",
                column: "CommentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogComments_ParentId",
                schema: "Portal",
                table: "BlogComments",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogComments_UserId",
                schema: "Portal",
                table: "BlogComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_CategoryId",
                schema: "Portal",
                table: "Blogs",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_CommentStatusId",
                schema: "Portal",
                table: "Blogs",
                column: "CommentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_LanguageId",
                schema: "Portal",
                table: "Blogs",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_UserId",
                schema: "Portal",
                table: "Blogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_ViewStatusId",
                schema: "Portal",
                table: "Blogs",
                column: "ViewStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_EnabledTypeId",
                schema: "Application",
                table: "Categories",
                column: "EnabledTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                schema: "Application",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UserId",
                schema: "Application",
                table: "Categories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId1",
                schema: "Portal",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UserId1",
                schema: "Portal",
                table: "Categories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Centers_EnabledTypeId",
                schema: "Organization",
                table: "Centers",
                column: "EnabledTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_ProvinceId",
                schema: "General",
                table: "Cities",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Commands_CommandTypeId",
                schema: "Organization",
                table: "Commands",
                column: "CommandTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Commands_ParentId",
                schema: "Organization",
                table: "Commands",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactUs_ContactUsTypeId",
                schema: "General",
                table: "ContactUs",
                column: "ContactUsTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryDates_EnabledTypeId",
                schema: "Application",
                table: "DeliveryDates",
                column: "EnabledTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_CenterId",
                schema: "Organization",
                table: "Departments",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_DepartmentTypeId",
                schema: "Organization",
                table: "Departments",
                column: "DepartmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_EnabledTypeId",
                schema: "Organization",
                table: "Departments",
                column: "EnabledTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ParentId",
                schema: "Organization",
                table: "Departments",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageDetails_DynamicPageId",
                schema: "Portal",
                table: "DynamicPageDetails",
                column: "DynamicPageId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageDetails_UserId",
                schema: "Portal",
                table: "DynamicPageDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPageDetails_ViewStatusId",
                schema: "Portal",
                table: "DynamicPageDetails",
                column: "ViewStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPages_RemoverId",
                schema: "Portal",
                table: "DynamicPages",
                column: "RemoverId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPages_UserId",
                schema: "Portal",
                table: "DynamicPages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPages_ViewStatusId",
                schema: "Portal",
                table: "DynamicPages",
                column: "ViewStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Faqs_FaqGroupId",
                schema: "General",
                table: "Faqs",
                column: "FaqGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Galleries_UserId",
                schema: "Portal",
                table: "Galleries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Galleries_ViewStatusId",
                schema: "Portal",
                table: "Galleries",
                column: "ViewStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Gallery_Media_Mapping_GalleryId",
                schema: "Portal",
                table: "Gallery_Media_Mapping",
                column: "GalleryId");

            migrationBuilder.CreateIndex(
                name: "IX_Gallery_Media_Mapping_MediaId",
                schema: "Portal",
                table: "Gallery_Media_Mapping",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_LanguageCultureId",
                schema: "General",
                table: "Languages",
                column: "LanguageCultureId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_ViewStatusId",
                schema: "General",
                table: "Languages",
                column: "ViewStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_LocaleStringResources_LanguageId",
                schema: "General",
                table: "LocaleStringResources",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaBinaries_MediaId",
                schema: "General",
                table: "MediaBinaries",
                column: "MediaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_MenuId",
                schema: "General",
                table: "MenuItems",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_ParentId",
                schema: "General",
                table: "MenuItems",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_LanguageId",
                schema: "General",
                table: "Menus",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageTemplates_EmailAccountId",
                schema: "General",
                table: "MessageTemplates",
                column: "EmailAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_News_CategoryId",
                schema: "Portal",
                table: "News",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_News_CommentStatusId",
                schema: "Portal",
                table: "News",
                column: "CommentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_News_LanguageId",
                schema: "Portal",
                table: "News",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_News_UserId",
                schema: "Portal",
                table: "News",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_News_ViewStatusId",
                schema: "Portal",
                table: "News",
                column: "ViewStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_News_Media_Mapping_MediaId",
                schema: "Portal",
                table: "News_Media_Mapping",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_News_Media_Mapping_MediaId1",
                schema: "Portal",
                table: "News_Media_Mapping",
                column: "MediaId1");

            migrationBuilder.CreateIndex(
                name: "IX_NewsComments_CommentStatusId",
                schema: "Portal",
                table: "NewsComments",
                column: "CommentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsComments_NewsId",
                schema: "Portal",
                table: "NewsComments",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsComments_ParentId",
                schema: "Portal",
                table: "NewsComments",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsComments_UserId",
                schema: "Portal",
                table: "NewsComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                schema: "Application",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BankPortId",
                schema: "Application",
                table: "Orders",
                column: "BankPortId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserAddressId",
                schema: "Application",
                table: "Orders",
                column: "UserAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                schema: "Application",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PositionRoles_PositionId",
                schema: "Organization",
                table: "PositionRoles",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_PositionRoles_RoleId",
                schema: "Organization",
                table: "PositionRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_CenterId",
                schema: "Organization",
                table: "Positions",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_DepartmentId",
                schema: "Organization",
                table: "Positions",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_EnabledTypeId",
                schema: "Organization",
                table: "Positions",
                column: "EnabledTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_PositionTypeId",
                schema: "Organization",
                table: "Positions",
                column: "PositionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_UserId",
                schema: "Organization",
                table: "Positions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Media_Mapping_MediaId",
                schema: "Application",
                table: "Product_Media_Mapping",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Media_Mapping_ProductId",
                schema: "Application",
                table: "Product_Media_Mapping",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductAttribute_Mapping_AttributeControlTypeId",
                schema: "Application",
                table: "Product_ProductAttribute_Mapping",
                column: "AttributeControlTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductAttribute_Mapping_ProductAttributeId",
                schema: "Application",
                table: "Product_ProductAttribute_Mapping",
                column: "ProductAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductAttribute_Mapping_ProductId",
                schema: "Application",
                table: "Product_ProductAttribute_Mapping",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAnswers_ProductQuestionId",
                schema: "Application",
                table: "ProductAnswers",
                column: "ProductQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAnswers_UserId",
                schema: "Application",
                table: "ProductAnswers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAnswers_ViewStatusId",
                schema: "Application",
                table: "ProductAnswers",
                column: "ViewStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValues_ProductAttributeMappingId",
                schema: "Application",
                table: "ProductAttributeValues",
                column: "ProductAttributeMappingId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_CommentStatusId",
                schema: "Application",
                table: "ProductComments",
                column: "CommentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_ParentId",
                schema: "Application",
                table: "ProductComments",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_ProductId",
                schema: "Application",
                table: "ProductComments",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_UserId",
                schema: "Application",
                table: "ProductComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuestions_ProductId",
                schema: "Application",
                table: "ProductQuestions",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuestions_UserId",
                schema: "Application",
                table: "ProductQuestions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuestions_ViewStatusId",
                schema: "Application",
                table: "ProductQuestions",
                column: "ViewStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                schema: "Application",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_DeliveryDateId",
                schema: "Application",
                table: "Products",
                column: "DeliveryDateId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_DiscountTypeId",
                schema: "Application",
                table: "Products",
                column: "DiscountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ShippingCostId",
                schema: "Application",
                table: "Products",
                column: "ShippingCostId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId",
                schema: "Application",
                table: "Products",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_CountryId",
                schema: "General",
                table: "Provinces",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_QueuedEmails_EmailAccountId",
                schema: "General",
                table: "QueuedEmails",
                column: "EmailAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_CommandId",
                schema: "Organization",
                table: "RolePermissions",
                column: "CommandId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                schema: "Organization",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_CenterId",
                schema: "Organization",
                table: "Roles",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingCosts_EnabledTypeId",
                schema: "Application",
                table: "ShippingCosts",
                column: "EnabledTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ProductId",
                schema: "Application",
                table: "ShoppingCartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_UserId",
                schema: "Application",
                table: "ShoppingCartItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StaticPages_RemoverId",
                schema: "Portal",
                table: "StaticPages",
                column: "RemoverId");

            migrationBuilder.CreateIndex(
                name: "IX_StaticPages_UserId",
                schema: "Portal",
                table: "StaticPages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StaticPages_ViewStatusId",
                schema: "Portal",
                table: "StaticPages",
                column: "ViewStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_UrlRecords_EnabledTypeId",
                schema: "General",
                table: "UrlRecords",
                column: "EnabledTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UrlRecords_LanguageId",
                schema: "General",
                table: "UrlRecords",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_CityId",
                schema: "Organization",
                table: "UserAddresses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_CountryId",
                schema: "Organization",
                table: "UserAddresses",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_ProvinceId",
                schema: "Organization",
                table: "UserAddresses",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_UserId",
                schema: "Organization",
                table: "UserAddresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPasswords_PasswordFormatTypeId",
                schema: "Organization",
                table: "UserPasswords",
                column: "PasswordFormatTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPasswords_UserId",
                schema: "Organization",
                table: "UserPasswords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EnabledTypeId",
                schema: "Organization",
                table: "Users",
                column: "EnabledTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GenderId",
                schema: "Organization",
                table: "Users",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserTypeId",
                schema: "Organization",
                table: "Users",
                column: "UserTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityLogs",
                schema: "General");

            migrationBuilder.DropTable(
                name: "Blog_Media_Mapping",
                schema: "Portal");

            migrationBuilder.DropTable(
                name: "BlogComments",
                schema: "Portal");

            migrationBuilder.DropTable(
                name: "ContactUs",
                schema: "General");

            migrationBuilder.DropTable(
                name: "DynamicPageDetails",
                schema: "Portal");

            migrationBuilder.DropTable(
                name: "Faqs",
                schema: "General");

            migrationBuilder.DropTable(
                name: "Gallery_Media_Mapping",
                schema: "Portal");

            migrationBuilder.DropTable(
                name: "GenericAttributes",
                schema: "General");

            migrationBuilder.DropTable(
                name: "LocaleStringResources",
                schema: "General");

            migrationBuilder.DropTable(
                name: "MediaBinaries",
                schema: "General");

            migrationBuilder.DropTable(
                name: "MenuItems",
                schema: "General");

            migrationBuilder.DropTable(
                name: "MessageTemplates",
                schema: "General");

            migrationBuilder.DropTable(
                name: "News_Media_Mapping",
                schema: "Portal");

            migrationBuilder.DropTable(
                name: "NewsComments",
                schema: "Portal");

            migrationBuilder.DropTable(
                name: "OrderDetails",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "PositionRoles",
                schema: "Organization");

            migrationBuilder.DropTable(
                name: "Product_Media_Mapping",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "ProductAnswers",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "ProductAttributeValues",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "ProductComments",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "QueuedEmails",
                schema: "General");

            migrationBuilder.DropTable(
                name: "RelatedProducts",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "RolePermissions",
                schema: "Organization");

            migrationBuilder.DropTable(
                name: "Settings",
                schema: "General");

            migrationBuilder.DropTable(
                name: "ShoppingCartItems",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "StaticPages",
                schema: "Portal");

            migrationBuilder.DropTable(
                name: "UrlRecords",
                schema: "General");

            migrationBuilder.DropTable(
                name: "UserPasswords",
                schema: "Organization");

            migrationBuilder.DropTable(
                name: "ActivityLogTypes",
                schema: "General");

            migrationBuilder.DropTable(
                name: "Blogs",
                schema: "Portal");

            migrationBuilder.DropTable(
                name: "ContactUsTypes",
                schema: "General");

            migrationBuilder.DropTable(
                name: "DynamicPages",
                schema: "Portal");

            migrationBuilder.DropTable(
                name: "FaqGroups",
                schema: "General");

            migrationBuilder.DropTable(
                name: "Galleries",
                schema: "Portal");

            migrationBuilder.DropTable(
                name: "Menus",
                schema: "General");

            migrationBuilder.DropTable(
                name: "News",
                schema: "Portal");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "Positions",
                schema: "Organization");

            migrationBuilder.DropTable(
                name: "Medias",
                schema: "General");

            migrationBuilder.DropTable(
                name: "ProductQuestions",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "Product_ProductAttribute_Mapping",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "EmailAccounts",
                schema: "General");

            migrationBuilder.DropTable(
                name: "Commands",
                schema: "Organization");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "Organization");

            migrationBuilder.DropTable(
                name: "PasswordFormatTypes",
                schema: "Organization");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "Portal");

            migrationBuilder.DropTable(
                name: "CommentStatuses",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Languages",
                schema: "General");

            migrationBuilder.DropTable(
                name: "BankPorts",
                schema: "Financial");

            migrationBuilder.DropTable(
                name: "UserAddresses",
                schema: "Organization");

            migrationBuilder.DropTable(
                name: "Departments",
                schema: "Organization");

            migrationBuilder.DropTable(
                name: "PositionTypes",
                schema: "Organization");

            migrationBuilder.DropTable(
                name: "AttributeControlTypes",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "ProductAttributes",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "CommandTypes",
                schema: "Organization");

            migrationBuilder.DropTable(
                name: "LanguageCultures",
                schema: "General");

            migrationBuilder.DropTable(
                name: "ViewStatuses",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Banks",
                schema: "Financial");

            migrationBuilder.DropTable(
                name: "Cities",
                schema: "General");

            migrationBuilder.DropTable(
                name: "Centers",
                schema: "Organization");

            migrationBuilder.DropTable(
                name: "DepartmentTypes",
                schema: "Organization");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "DeliveryDates",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "DiscountTypes",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "ShippingCosts",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "Provinces",
                schema: "General");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Organization");

            migrationBuilder.DropTable(
                name: "Countries",
                schema: "General");

            migrationBuilder.DropTable(
                name: "EnabledTypes",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Genders",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "UserTypes",
                schema: "Organization");
        }
    }
}
