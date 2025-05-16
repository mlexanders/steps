using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Steps.Services.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:CollationDefinition:case_insensitive", "und-u-ks-primary,und-u-ks-primary,icu,False");

            migrationBuilder.CreateTable(
                name: "ScheduleFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    Data = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestAthleteElement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Degree = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    AgeCategory = table.Column<string>(type: "text", nullable: false),
                    Element1 = table.Column<string>(type: "text", nullable: false),
                    Element2 = table.Column<string>(type: "text", nullable: false),
                    Element3 = table.Column<string>(type: "text", nullable: false),
                    Element4 = table.Column<string>(type: "text", nullable: false),
                    Element5 = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestAthleteElement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Login = table.Column<string>(type: "character varying(320)", maxLength: 320, nullable: false, collation: "case_insensitive"),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ScheduleFileId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contests_ScheduleFiles_ScheduleFileId",
                        column: x => x.ScheduleFileId,
                        principalTable: "ScheduleFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
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
                        name: "FK_Clubs_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CounterContest",
                columns: table => new
                {
                    CounterId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContestId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CounterContest", x => new { x.ContestId, x.CounterId });
                    table.ForeignKey(
                        name: "FK_CounterContest_Contests_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CounterContest_Users_CounterId",
                        column: x => x.CounterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupBlocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContestId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupBlocks_Contests_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JudgeContest",
                columns: table => new
                {
                    JudgeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContestId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JudgeContest", x => new { x.ContestId, x.JudgeId });
                    table.ForeignKey(
                        name: "FK_JudgeContest_Contests_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JudgeContest_Users_JudgeId",
                        column: x => x.JudgeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_Teams_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Athletes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AgeCategory = table.Column<int>(type: "integer", nullable: false),
                    Degree = table.Column<int>(type: "integer", nullable: false),
                    AthleteType = table.Column<int>(type: "integer", nullable: false),
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
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entries_Contests_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Entries_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Entries_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "FinalScheduledCell",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExitTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SequenceNumber = table.Column<int>(type: "integer", nullable: false),
                    AthleteId = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupBlockId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinalScheduledCell", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinalScheduledCell_Athletes_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Athletes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinalScheduledCell_GroupBlocks_GroupBlockId",
                        column: x => x.GroupBlockId,
                        principalTable: "GroupBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreScheduledCell",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    ExitTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SequenceNumber = table.Column<int>(type: "integer", nullable: false),
                    AthleteId = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupBlockId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreScheduledCell", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreScheduledCell_Athletes_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Athletes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreScheduledCell_GroupBlocks_GroupBlockId",
                        column: x => x.GroupBlockId,
                        principalTable: "GroupBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContestId = table.Column<Guid>(type: "uuid", nullable: false),
                    AthleteId = table.Column<Guid>(type: "uuid", nullable: false),
                    JudgeId = table.Column<Guid>(type: "uuid", nullable: false),
                    RatingId = table.Column<Guid>(type: "uuid", nullable: false),
                    Scores = table.Column<List<int>>(type: "integer[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestResults_Athletes_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Athletes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestResults_Contests_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestResults_Users_JudgeId",
                        column: x => x.JudgeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AthleteEntry",
                columns: table => new
                {
                    AthletesId = table.Column<Guid>(type: "uuid", nullable: false),
                    EntryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AthleteEntry", x => new { x.AthletesId, x.EntryId });
                    table.ForeignKey(
                        name: "FK_AthleteEntry_Athletes_AthletesId",
                        column: x => x.AthletesId,
                        principalTable: "Athletes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AthleteEntry_Entries_EntryId",
                        column: x => x.EntryId,
                        principalTable: "Entries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AthleteId = table.Column<Guid>(type: "uuid", nullable: false),
                    TestResultId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContestId = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupBlockId = table.Column<Guid>(type: "uuid", nullable: false),
                    TotalScore = table.Column<int>(type: "integer", nullable: false),
                    CertificateDegree = table.Column<int>(type: "integer", nullable: false),
                    AgeCategory = table.Column<int>(type: "integer", nullable: false),
                    IsComplete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rating_Athletes_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Athletes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rating_Contests_ContestId",
                        column: x => x.ContestId,
                        principalTable: "Contests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rating_GroupBlocks_GroupBlockId",
                        column: x => x.GroupBlockId,
                        principalTable: "GroupBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rating_TestResults_TestResultId",
                        column: x => x.TestResultId,
                        principalTable: "TestResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TestAthleteElement",
                columns: new[] { "Id", "AgeCategory", "Degree", "Element1", "Element2", "Element3", "Element4", "Element5", "Type" },
                values: new object[,]
                {
                    { new Guid("00123435-0244-42c2-a6be-c0b185c35bed"), "Juniors", "Higher", "Шпагат с захватом ноги", "Элементы чир-акробатики ЭРИЭЛ (маховое боковое сальто) стойка на руках", "3 разных пируэта -Джазовый или классический на 1080 (3 оборота).-Пируэт на вторую позицию (а ля секонд) минимум 4 оборота-Лэг Холд (вперед или  сторону)", "Комбинация чир- прыжков в темп ХЕДЛЕР вперед или в сторону+ТОЙ ТАЧ+ПАЙК Или СТЭГ-СИТ+ТОЙ ТАЧ+ПАЙК", "Комбинация базовых положений рук с другими элементами (прыжками, махами, вращениями и т.д.) минимум на 32 счета.", "CheerFreestyle" },
                    { new Guid("03889f6f-9c0c-4deb-9ed1-0f3ea789eee3"), "Juniors", "Second", "Шпагат (2 вида)", "Колесо с опорой на одну руку", "Два разных пируэта-Пируэт аттитюд или арабеск—-«Солнышко", "Чир-прыжки  -ХЕДЛЕР вперед или  в сторону,---ТОЙ ТАЧ,-ПАЙК", "Комбинация базовых положений рук с другими элементами (ЧИР ДАНС) (прыжками, махами, вращениями и т.д.) минимум на 16 счетов", "CheerFreestyle" },
                    { new Guid("169708ca-247f-46ee-8e32-9bd141c377f9"), "YoungerChildren", "Third", "Фиксация положения лодочка на спине, фиксация положения лодочка на животе (по 3 сек)", "Мост из положения лежа и лечь обратно. Фиксация 3 сек", "Равновесия: Положение «арабеск» на всей стопе(фиксация 3 сек) Поднятая назад нога вытянута и отведена на 45 или 90 градусов. Фиксация 3 сек.", "Чир прыжки:   «ТАК», «Стредл»", "Базовые положения рук «Правая диагональ», «Левая диагональ,»»Ти позиция»,» ломаное Т»", "CheerFreestyle" },
                    { new Guid("1da76132-1eb5-48d1-91e6-ea48d977437a"), "YoungerChildren", "Higher", "Равновесие «Арабеск» (Ласточка) на любую ногу", "Кувырок вперед, кувырок назад", "Комбинация из 4х любых Чир положения рук.", "Шпагат (один любой)", "Рондат", "Cheer" },
                    { new Guid("20d05371-4141-4cba-b620-002ab98d57f3"), "Juniors", "First", "Шпагат (3 вида)", "Сложный элемент чир акробатики с отрывом от пола (на выбор)", "2 разных пируэта-Джазовый или классический на 720 минимум.-Пируэт на вторую позицию (а ля секонд) минимум 4 оборота", "Комбинация чир-прыжков  в темп ХЕДЛЕР вперед или в сторону+ТОЙ ТАЧ+ТОЙ ТАЧ", "Комбинация базовых положений рук с другими элементами (прыжками, махами, вращениями и т.д.) минимум на 24 счета.", "CheerFreestyle" },
                    { new Guid("21e9f32a-b76e-42ff-805f-eb7c08fcfb0f"), "Baby", "Higher", "Складка ноги вместе, складка ноги врозь. (касаясь грудью, фиксация 3 сек) ", "Упор лежа-прогиб назад,согнуть колени «Рыбка»", "Релеве на одной ноге.на всей стопе,вторая нога вытянута вперед на 30 градусов ", "Прыжок «Банана Сплит» ", "Положение рук Хай Ви ,Лоу Ви ", null },
                    { new Guid("2532c449-54a4-4cb0-bec2-7ae7ffb4036e"), "Baby", "Second", "Складка ноги вместе (касаясь грудью, фиксация 3 сек) ", "«Полумост» Лежа на спине поднять таз и захватить руками пятки одноименной согнутой ноги ", "Релеве по 2 позиции на 2х ногах", "Комбинация прыжков Сотэ по 6 и 2 позиции", "Положение рук «Руки на бедрах» ", null },
                    { new Guid("25ac73a9-3959-492b-ac9f-afb6edc48c66"), "Juniors", "Second", "Складка ноги вместе, складка нови врозь (касаясь грудью, фиксация 3 сек)", "Скорпион (женщины).", "С разбега: Вальсет-рондат-180рондат.", "Фляк с места.", "Стойка на руках.", "Cheer" },
                    { new Guid("2762fed4-0979-445c-9317-b5087116eeb9"), "Youth", "Higher", "Равновесие «Лук-стрела» (юниорки).", "Переворот вперед через одну руку.", "Фляк с места.", "Маховое сальто", "Связка две восьмерки: Чир-положения рук (любые) +чир прыжки (обязательно минимум 3 подряд). Быстрый темп.", "Cheer" },
                    { new Guid("2aad787d-4290-416f-8c35-a702fa9e7059"), "BoysGirls", "Higher", "Шпагат (два на выбор) (девочки)", "Скорпион – 1 нога (девочки)", "Переворот вперед - рондат.", "Чир прыжки: «Той тач» 2 в темп.", "Чир-положения рук: связка одна восьмерка (любые положения рук). Средний темп.", "Cheer" },
                    { new Guid("2d988641-9e29-4d28-822d-1e54f53b014d"), "BoysGirls", "First", "Шпагат (3 вида)", "Колесо с опорой на две руки", "Джаз-пируэт на 720", "Чир прыжки –-ХЕДЛЕР вперед-ТОЙ ТАЧ", "Комбинация базовых положений рук с другими элементами  (прыжками, махами, вращениями и т.д.) минимум на 16 счетов", "CheerFreestyle" },
                    { new Guid("33bd07ef-9017-4a38-8d8e-92d723ecfb05"), "Juniors", "Third", "Шпагат (один на выбор)", "Колесо с опорой на две руки", "Джазовый пируэт минимум на 720 градусов", "Чир прыжки-ХЕРКИ,-ХЕДЛЕР вперед,", "Комбинация базовых положений рук с другими элементами (прыжками, махами, вращениями и т.д) минимум на 8 счетов.", "CheerFreestyle" },
                    { new Guid("399126da-de21-4148-9975-26947f337f58"), "BoysGirls", "Second", "Складка ноги вместе, складка нови врозь (касаясь грудью, фиксация 3 сек)", "Мост из положения стоя и встать с моста.", "Колесо с опорой на две руки.", "Стойка на голове и руках.", "Чир прыжки: «ТАК», «Той Тач».", "Cheer" },
                    { new Guid("3bf3fd78-b8a2-43eb-825c-f607d6b10f81"), "YoungerChildren", "Higher", "Шпагат (3 вида)", "Колесо с опорой на две руки", "Джазовый пируэт на 720(2 оборота)", "Чир прыжки -ТОЙ ТАЧ", "Комбинация базовых положений рук с другими элементами (прыжками, махами, вращениями и т.д.) минимум на 16 счетов", "CheerFreestyle" },
                    { new Guid("43a080b5-6bca-4ca6-bae8-9aca8e71ab09"), "Youth", "Fourth", "Складка ноги вместе, складка нови врозь. (касаясь грудью, фиксация 3 сек)", "Кувырок вперед", "Повороты на двух ногах с продвижением с удержанием точки -«шэне» (минимум 3)", "Чир прыжки: «Абстракт»,  «Стредл» ", "Комбинация из  базовых положений рук, минимум 6 положений.-Кинжалы-Тачдаун-Нижний Тачдаун-Эль (правая и левая)-Лук и стрела-Панч Ап", "CheerFreestyle" },
                    { new Guid("45b91fc5-0858-4c66-aa21-ca2cc220b993"), "Baby", "Fourth", "Сед на полу,ноги вытянуты- 1.стопы на себя 2-вытянуть носки", "Удержание прямых ног сидя на полу", "Положение пассе сидя на полу", "Прыжок Сотэ по 6 позиции", "Положение «Клин» (Чистая позиция)", null },
                    { new Guid("48ba0eea-5f60-4748-8563-ff7f2ea6ab8c"), "BoysGirls", "Third", "Шпагат (один на выбор)", "Удержание вытянутой  ноги (Флажок)-на любую ногу", "Повороты на двух ногах с продвижением с удержанием точки -«шэне» (минимум 3).Фиксировать остановку.", "Чир прыжки: «Абстракт»", "Комбинация из  базовых положений рук, минимум 6 положений. Комбинация из 6 базовых положений рук--Кинжалы-Тачдаун-Нижний Тачдаун-Эль (правая и левая)-Лук и стрела-Панч Ап", "CheerFreestyle" },
                    { new Guid("4bd90988-7f23-40dd-8c09-552a61123025"), "BoysGirls", "First", "Перекидка вперед на 2 ноги.", "Стойка на руках кувырок вперед", "Чир прыжки: «Той Тач», «Пайк».", "Шпагат поперечный.", "Рондат.", "Cheer" },
                    { new Guid("4c669b05-33d2-47fc-8ed9-3a5cbcc20553"), "Youth", "First", "Шпагат (3 вида)", "Колесо с опорой на одну руку", "Два разных пируэта – -Пируэт аттитюд-«Солнышко»", "Чир-прыжки-ХЕДЛЕР вперед-ТОЙ ТАЧ,-ПАЙК", "Комбинация базовых положений рук с другими элементами (ЧИР ДАНС) (прыжками, махами, вращениями и т.д.) минимум на 16 счетов", "CheerFreestyle" },
                    { new Guid("4df14af8-5f5e-400b-be3f-5facd73ce6f7"), "YoungerChildren", "Fourth", "Пресс 10 раз (лежа на спине, ноги согнуты в коленях);", "Прыжок вокруг себя 180+180 (положение рук свободное);", "Чир прыжок «Стредл»;", "Складка нови врозь (касаясь грудью, фиксация 3 сек)", "Равновесия: «Либерти» (цапелька) на любую ногу (фиксация 3 сек)", "Cheer" },
                    { new Guid("63de6d87-776a-4bde-9c0c-af7af59337fc"), "Youth", "First", "Скорпион (юниорки).", "С разбега: рондат-отскок-рондат.", "Стойка на руках.", "Переворот назад.", "Связка одна восьмерка: Чир-положения рук (любы)+чир прыжки(обязательно минимум 2 подряд). Средний темп.", "Cheer" },
                    { new Guid("7e200741-9944-442c-a629-6a6b6aae3248"), "BoysGirls", "Higher", "Шпагат с захватом ноги", "Колесо с опорой на одну руку ", "2разных пируэта – -Джазовый или классический на 720,-«Солнышко»", "Чир-прыжки-ХЕДЛЕР-ТОЙ ТАЧ,-ПАЙК или ДВОЙНАЯ ДЕВЯТКА", "Комбинация базовых положений рук с другими элементами (ЧИР ДАНС) (прыжками, махами, вращениями и т.д.) минимум на 24 счета", "CheerFreestyle" },
                    { new Guid("831478e5-0002-49f5-94fe-3db452cc7b81"), "YoungerChildren", "First", "Шпагат (2 вида)", "Кувырок вперед", "Джазовый пируэт на 360", "Чир прыжки «ХЕРДЛЕР ВПЕРЕД»", "Комбинация базовых положений рук с другими элементами (прыжками, махами,вращениями и т. д) минимум на 8 счетов ", "CheerFreestyle" },
                    { new Guid("85ea24a6-ad0a-45c9-a968-aedd006a42c4"), "BoysGirls", "Third", "Мост из положения стоя и лечь обратно (фиксация 3 сек).", "Кувырок вперед, кувырок назад", "Равновесие «Арабеск» (Ласточка) на любую ногу", "Чир прыжки: «ТАК»+«Стредл». Два подряд.", "Положения рук: Хай Ви, Лоу Ви, Ти, Правая диагональ, Левая диагональ.", "Cheer" },
                    { new Guid("8613749e-cb44-4716-a82b-3c2cb9619b1d"), "Juniors", "First", "Равновесие «Лук-стрела» (женщины).", "Рондат-фляк.", "Той тач + Фляк", "Маховое сальто", "Связка три восьмерки (положения рук+чир прыжки (минимум 2 подряд)+махи ногами. Средний темп.", "Cheer" },
                    { new Guid("87631c44-76f7-486d-84a5-13835c4ff06c"), "Youth", "Second", "Шпагат (2 вида)", "Колесо с опорой на две руки", "Джаз-пируэт на 720", "Чир прыжки-ХЕДЛЕР ВПЕРЕД-ТОЙ ТАЧ", "Комбинация базовых положений рук с другими элементами  (прыжками, махами, вращениями и т.д.) минимум на 16 счетов", "CheerFreestyle" },
                    { new Guid("8a1c9fbb-eba8-4e67-a24c-ff06b4e279f7"), "Youth", "Third", "Шпагат (один  на выбор)", "Высокие махи вперед  и в сторону (4 маха)", "Тур пике – 3 пируэта.", "Чир прыжки –– ХЕРКИ-«С»джамп", "Комбинация из  базовых положений рук, минимум 8 положений.", "CheerFreestyle" },
                    { new Guid("8ca7a4f3-8924-4f45-9f28-3e8a94db84ba"), "YoungerChildren", "Second", "Шпагат (один на выбор)", "Удержание вытянутой ноги (Флажок)-на любую ногу", "Повороты на двух ногах с продвижением с удержанием точки «шэне» (минимум 3)", "Чир прыжки: «ХЕРКИ»", "Комбинация из 6 базовых положений рук--Кинжалы-Тачдаун-Нижний Тачдаун-Эль (правая и левая)-Лук и стрела-Панч Ап", "CheerFreestyle" },
                    { new Guid("975a8db3-3898-4be4-8e57-c46cfe69e21d"), "Juniors", "Third", "Складка ноги вместе, складка нови врозь (касаясь грудью, фиксация 3 сек)", "Удержание ноги в положении «Легхолд» (Флажок) любая нога (женщины).", "Переворот вперед-рондат", "Стойка на руках – кувырок вперед.", "Колесо с опорой на одну руку.", "Cheer" },
                    { new Guid("9e10a242-b4a3-4ad6-8d14-e22d121cffd3"), "YoungerChildren", "Third", "Мост из положения лежа и лечь обратно (фиксация 3 сек)", "фиксация положения лодочка на спине, фиксация положения лодочка на животе (по 3 сек)", "Кувырок вперед", "Чир положение рук Хай Ви, Лоу Ви, Ти,", "«Чир прыжок:  «ТАК»»", "Cheer" },
                    { new Guid("a83c5474-51f2-4df5-bcf3-d53ae63e66b2"), "Youth", "Third", "Складка ноги вместе, складка нови врозь (касаясь грудью, фиксация 3 сек)", "Мост из положения стоя и встать с моста.", "Колесо с опорой на две руки.", "Стойка на голове и руках.", "Чир прыжки: «ТойТач», «хеднер»", "Cheer" },
                    { new Guid("afdbb17a-5352-48b4-bbeb-4b61bd2c0355"), "Baby", "Third", "Сед пятки вместе-колени согнуты в стороны («бабочка»).Наклон вперед в,фиксация 3 сек ", "Прогиб назад стоя на коленях,захват руками пяток «Верблюд»", "Релеве по 6 позиции на 2х ногах", "Прыжок Сотэ по 2 позиции", "Положение рук Клэп", null },
                    { new Guid("bb06d451-f848-4d99-af97-c9bf88192542"), "Juniors", "Fourth", "Высокие махи вперед  и в сторону(4 минимум)", "Кувырок вперед, кувырок назад", "Тур пике – (минимум 3 пируэта)", "Чир прыжки «Абстракт» «Си-прыжок»", "Комбинация из  базовых положений рук, минимум 8 положений.", "CheerFreestyle" },
                    { new Guid("bc20becb-e01e-4821-91b6-f7b5cef2a598"), "Youth", "Higher", "Шпагат с захватом ноги", "Элемент чир-акробатики Переворот вперед", "2 разных пируэта-Джазовый или классический на 720 минимум.(2 оборота)-Пируэт на вторую позицию (а ля секонд) минимум 3 оборота", "Комбинация чир-прыжков  в темп ХЕДЛЕР вперед или в сторону+ТОЙ ТАЧ+ТОЙ ТАЧ", "Комбинация базовых положений рук с другими элементами (ЧИР ДАНС) (прыжками, махами, вращениями и т.д.) минимум на 24 счета.", "CheerFreestyle" },
                    { new Guid("c2045d04-37e4-483e-a948-8717e01aea43"), "YoungerChildren", "Second", "Равновесие: удержание прямая нога вперёд на 90 (фиксация 3 сек)", "Складка ноги врозь , складка ноги вместе (касаясь грудью, фиксация 3 сек)", "Мост с колен и встать обратно.", "Чир положение рук Т, ломаное Т", "«Два чир-прыжка подряд: «ТАК», «Стредл»»", "Cheer" },
                    { new Guid("c6557597-ca45-4d84-9103-a9eaea33b811"), "BoysGirls", "Fourth", "Складка ноги вместе, складка нови врозь (касаясь грудью, фиксация 3 сек)", "Мост из положения лежа и лечь обратно (фиксация 3 сек)", "Равновесия: Положение «пассе» на высоких полупальцах (фиксация 3 сек)", "Чир прыжки: «ТАК», «Стредл».", "Базовые положения рук (4 –минимум) Правая диагональ Левая диагональ Ти-позиция Ломаное Т", "CheerFreestyle" },
                    { new Guid("c730cacd-5e2e-4372-8e06-3d7f2d95e01d"), "Baby", "First", "Сед ноги врозь-складка к правой ноге. складка к левой ноге (касаясь грудью, фиксация 3 сек) ", "«Корзиночка» Лежа на животе Захват каждой рукой одноименной  согнутой ноги ", "Равновесие на одной ноге.на всей стопе,вторая нога вытянута вперед на 30 градусов ", "Прыжок «Джампинг Джэк» ", "Положение рук  «Подсвечники». «Ведра»", null },
                    { new Guid("ca56d07b-8aa2-4332-a900-28fa5cbe0e12"), "Youth", "Fourth", "Мост из положения стоя и лечь обратно (фиксация 3 сек)", "Кувырок вперед, кувырок назад.", "Равновесие -ласточка на любую ногу (юниорки).", "Комбинация из ЧИР положений рук, минимум 6.", "Складка ноги врозь", "Cheer" },
                    { new Guid("cb1f5b68-ef41-4c20-8891-00d6cfc4bdac"), "Juniors", "Higher", "Равновесие «Лук-стрела» (женщины).", "Шпагат (один на выбор) (девочки)", "Перекидка назад из положения сидя.", "Рондат-фляк-сальто или Рондат-сальто + рондат-фляк", "Переворот вперед-рондат-фляк.", "Cheer" },
                    { new Guid("d54e0bf6-8995-4023-843b-cabf1132b42d"), "YoungerChildren", "Fourth", "Складка ноги вместе, складка ноги врозь. (касаясь грудью, фиксация 3 сек)", "Стойка на лопатках «Березка»", "Равновесие на всей стопе, положение «пассе» («Либерти»)фиксация 3 сек.", "Чир прыжок «Стредл»", "Базовые положения рук Чистая позиция, Клэп, Хай Ви, Лоу Ви", "CheerFreestyle" },
                    { new Guid("da07248f-63ea-4e57-834f-7f0fd0eefdda"), "YoungerChildren", "First", "чир прыжок «Так»", "Кувырок вперед, кувырок назад.", "Мост из положения стоя  и лечь обратно. Фиксация 3 сек.", "Чир положение рук Лоу Ви,к Хай Ви", "«Березка»", "Cheer" },
                    { new Guid("dc777699-ddd0-450a-b6a9-b96c6c9f7eb4"), "BoysGirls", "Fourth", "Складка ноги вместе, складка нови врозь (касаясь грудью, фиксация 3 сек)", "Мост из положения лежа и лечь обратно (фиксация 3 сек)", "Стойка на предплечьях (березка) (фиксация 3 сек)", "Чир прыжки: «ТАК», «Стредл».", "Кувырок вперед.", "Cheer" },
                    { new Guid("e39c706d-33f4-45ef-8e86-6c92587e4ccc"), "BoysGirls", "Second", "Шпагат (2 вида)", "Кувырок вперед", "Джаз-пируэт на 360(1 оборот) ", "Чир прыжки – ХЕРКИ-«С»джамп", "Комбинация базовых положений рук с другими элементами (прыжками, махами, вращениями и т. д) минимум на 8 счетов", "CheerFreestyle" },
                    { new Guid("f4277582-e7c7-476d-ae7d-080252acc761"), "Juniors", "Fourth", "Мост из положения лежа и лечь обратно (фиксация 3 сек).", "Равновесие «Арабеск» (Ласточка) на любую ногу.", "Колесо с опорой на две руки.", "Стойка на голове и руках (фиксация 3 сек).", "Связка одна восьмерка: Чир положения рук + чир прыжки. Средний темп.", "Cheer" },
                    { new Guid("f4366979-9949-4bb1-a902-dca06b005902"), "Youth", "Second", "Складка ноги вместе, складка нови врозь (касаясь грудью, фиксация 3 сек)", "Удержание ноги в положении «Легхолд» (Флажок) любая нога (юниорки).", "Переворот вперед-рондат.", "Чир-положения рук: Комбинация из минимум 8 положений.", "Чир прыжки: подряд «Так»+«Той Тач»+»Той Тач»", "Cheer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AthleteEntry_EntryId",
                table: "AthleteEntry",
                column: "EntryId");

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
                name: "IX_Contests_Name",
                table: "Contests",
                column: "Name",
                unique: true)
                .Annotation("Relational:Collation", new[] { "case_insensitive" });

            migrationBuilder.CreateIndex(
                name: "IX_Contests_ScheduleFileId",
                table: "Contests",
                column: "ScheduleFileId");

            migrationBuilder.CreateIndex(
                name: "IX_CounterContest_CounterId",
                table: "CounterContest",
                column: "CounterId");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_ContestId",
                table: "Entries",
                column: "ContestId");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_CreatorId",
                table: "Entries",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_TeamId",
                table: "Entries",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalScheduledCell_AthleteId",
                table: "FinalScheduledCell",
                column: "AthleteId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalScheduledCell_GroupBlockId",
                table: "FinalScheduledCell",
                column: "GroupBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupBlocks_ContestId",
                table: "GroupBlocks",
                column: "ContestId");

            migrationBuilder.CreateIndex(
                name: "IX_JudgeContest_JudgeId",
                table: "JudgeContest",
                column: "JudgeId");

            migrationBuilder.CreateIndex(
                name: "IX_PreScheduledCell_AthleteId",
                table: "PreScheduledCell",
                column: "AthleteId");

            migrationBuilder.CreateIndex(
                name: "IX_PreScheduledCell_GroupBlockId",
                table: "PreScheduledCell",
                column: "GroupBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_AthleteId",
                table: "Rating",
                column: "AthleteId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_ContestId",
                table: "Rating",
                column: "ContestId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_GroupBlockId",
                table: "Rating",
                column: "GroupBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_TestResultId",
                table: "Rating",
                column: "TestResultId",
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
                name: "IX_TestResults_AthleteId_ContestId",
                table: "TestResults",
                columns: new[] { "AthleteId", "ContestId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestResults_ContestId",
                table: "TestResults",
                column: "ContestId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResults_JudgeId",
                table: "TestResults",
                column: "JudgeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Login",
                table: "Users",
                column: "Login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AthleteEntry");

            migrationBuilder.DropTable(
                name: "CounterContest");

            migrationBuilder.DropTable(
                name: "FinalScheduledCell");

            migrationBuilder.DropTable(
                name: "JudgeContest");

            migrationBuilder.DropTable(
                name: "PreScheduledCell");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "TestAthleteElement");

            migrationBuilder.DropTable(
                name: "Entries");

            migrationBuilder.DropTable(
                name: "GroupBlocks");

            migrationBuilder.DropTable(
                name: "TestResults");

            migrationBuilder.DropTable(
                name: "Athletes");

            migrationBuilder.DropTable(
                name: "Contests");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "ScheduleFiles");

            migrationBuilder.DropTable(
                name: "Clubs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
