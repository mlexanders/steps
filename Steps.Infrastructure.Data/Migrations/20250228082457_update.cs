using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Steps.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:CollationDefinition:case_insensitive", "und-u-ks-primary,und-u-ks-primary,icu,False");

            migrationBuilder.CreateTable(
                name: "Contest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GeneratedAthletesListId = table.Column<Guid>(type: "uuid", nullable: true),
                    LateAthletesListId = table.Column<Guid>(type: "uuid", nullable: true),
                    PreAthletesListId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeneratedAthletesLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContestId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneratedAthletesLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneratedAthletesLists_Contest_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LateAtheletesLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContestId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LateAtheletesLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LateAtheletesLists_Contest_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreAthletesLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContestId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreAthletesLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreAthletesLists_Contest_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Login = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false, collation: "case_insensitive"),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    ContestId = table.Column<Guid>(type: "uuid", nullable: true),
                    ContestId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Contest_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contest",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_Contest_ContestId1",
                        column: x => x.ContestId1,
                        principalTable: "Contest",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GroupBlocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Numbers = table.Column<List<int>>(type: "integer[]", nullable: false),
                    ExitTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ContestId = table.Column<Guid>(type: "uuid", nullable: false),
                    GeneratedAthletesListId = table.Column<Guid>(type: "uuid", nullable: true),
                    LateAthletesListId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupBlocks_Contest_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupBlocks_GeneratedAthletesLists_GeneratedAthletesListId",
                        column: x => x.GeneratedAthletesListId,
                        principalTable: "GeneratedAthletesLists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GroupBlocks_LateAtheletesLists_LateAthletesListId",
                        column: x => x.LateAthletesListId,
                        principalTable: "LateAtheletesLists",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Clubs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, collation: "case_insensitive"),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clubs_User_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Entries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    IsSuccess = table.Column<bool>(type: "boolean", nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ContestId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    EntryAthletesListId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entries_Contest_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Entries_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, collation: "case_insensitive"),
                    Address = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    ClubId = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_User_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntryAthletesLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EntryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryAthletesLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntryAthletesLists_Entries_EntryId",
                        column: x => x.EntryId,
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Athletes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExitTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsAppeared = table.Column<bool>(type: "boolean", nullable: true),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Athletes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Athletes_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AthleteEntryAthletesList",
                columns: table => new
                {
                    AthletesId = table.Column<Guid>(type: "uuid", nullable: false),
                    EntryAthletesListsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AthleteEntryAthletesList", x => new { x.AthletesId, x.EntryAthletesListsId });
                    table.ForeignKey(
                        name: "FK_AthleteEntryAthletesList_Athletes_AthletesId",
                        column: x => x.AthletesId,
                        principalTable: "Athletes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AthleteEntryAthletesList_EntryAthletesLists_EntryAthletesLi~",
                        column: x => x.EntryAthletesListsId,
                        principalTable: "EntryAthletesLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AthleteGroupBlock",
                columns: table => new
                {
                    AthletesId = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupBlocksId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AthleteGroupBlock", x => new { x.AthletesId, x.GroupBlocksId });
                    table.ForeignKey(
                        name: "FK_AthleteGroupBlock_Athletes_AthletesId",
                        column: x => x.AthletesId,
                        principalTable: "Athletes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AthleteGroupBlock_GroupBlocks_GroupBlocksId",
                        column: x => x.GroupBlocksId,
                        principalTable: "GroupBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AthletePreAthletesList",
                columns: table => new
                {
                    AthletesId = table.Column<Guid>(type: "uuid", nullable: false),
                    PreAthletesListsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AthletePreAthletesList", x => new { x.AthletesId, x.PreAthletesListsId });
                    table.ForeignKey(
                        name: "FK_AthletePreAthletesList_Athletes_AthletesId",
                        column: x => x.AthletesId,
                        principalTable: "Athletes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AthletePreAthletesList_PreAthletesLists_PreAthletesListsId",
                        column: x => x.PreAthletesListsId,
                        principalTable: "PreAthletesLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AthleteEntryAthletesList_EntryAthletesListsId",
                table: "AthleteEntryAthletesList",
                column: "EntryAthletesListsId");

            migrationBuilder.CreateIndex(
                name: "IX_AthleteGroupBlock_GroupBlocksId",
                table: "AthleteGroupBlock",
                column: "GroupBlocksId");

            migrationBuilder.CreateIndex(
                name: "IX_AthletePreAthletesList_PreAthletesListsId",
                table: "AthletePreAthletesList",
                column: "PreAthletesListsId");

            migrationBuilder.CreateIndex(
                name: "IX_Athletes_TeamId",
                table: "Athletes",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_Name",
                table: "Clubs",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_OwnerId",
                table: "Clubs",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Contest_Name",
                table: "Contest",
                column: "Name",
                unique: true)
                .Annotation("Relational:Collation", new[] { "case_insensitive" });

            migrationBuilder.CreateIndex(
                name: "IX_Entries_ContestId",
                table: "Entries",
                column: "ContestId");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_UserId",
                table: "Entries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EntryAthletesLists_EntryId",
                table: "EntryAthletesLists",
                column: "EntryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GeneratedAthletesLists_ContestId",
                table: "GeneratedAthletesLists",
                column: "ContestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupBlocks_ContestId",
                table: "GroupBlocks",
                column: "ContestId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupBlocks_GeneratedAthletesListId",
                table: "GroupBlocks",
                column: "GeneratedAthletesListId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupBlocks_LateAthletesListId",
                table: "GroupBlocks",
                column: "LateAthletesListId");

            migrationBuilder.CreateIndex(
                name: "IX_LateAtheletesLists_ContestId",
                table: "LateAtheletesLists",
                column: "ContestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PreAthletesLists_ContestId",
                table: "PreAthletesLists",
                column: "ContestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ClubId",
                table: "Teams",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_Name",
                table: "Teams",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_OwnerId",
                table: "Teams",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_User_ContestId",
                table: "User",
                column: "ContestId");

            migrationBuilder.CreateIndex(
                name: "IX_User_ContestId1",
                table: "User",
                column: "ContestId1");

            migrationBuilder.CreateIndex(
                name: "IX_User_Login",
                table: "User",
                column: "Login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AthleteEntryAthletesList");

            migrationBuilder.DropTable(
                name: "AthleteGroupBlock");

            migrationBuilder.DropTable(
                name: "AthletePreAthletesList");

            migrationBuilder.DropTable(
                name: "EntryAthletesLists");

            migrationBuilder.DropTable(
                name: "GroupBlocks");

            migrationBuilder.DropTable(
                name: "Athletes");

            migrationBuilder.DropTable(
                name: "PreAthletesLists");

            migrationBuilder.DropTable(
                name: "Entries");

            migrationBuilder.DropTable(
                name: "GeneratedAthletesLists");

            migrationBuilder.DropTable(
                name: "LateAtheletesLists");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Clubs");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Contest");
        }
    }
}
