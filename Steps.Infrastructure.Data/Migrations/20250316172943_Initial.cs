using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Steps.Infrastructure.Data.Migrations
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
                name: "AthleteElements",
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
                    table.PrimaryKey("PK_AthleteElements", x => x.Id);
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
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contests", x => x.Id);
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
                    AthleteElementsId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Athletes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Athletes_AthleteElements_AthleteElementsId",
                        column: x => x.AthleteElementsId,
                        principalTable: "AthleteElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "ScheduledCell",
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
                    table.PrimaryKey("PK_ScheduledCell", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduledCell_Athletes_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Athletes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduledCell_GroupBlocks_GroupBlockId",
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

            migrationBuilder.InsertData(
                table: "AthleteElements",
                columns: new[] { "Id", "AgeCategory", "Degree", "Element1", "Element2", "Element3", "Element4", "Element5", "Type" },
                values: new object[,]
                {
                    { new Guid("025fc91b-27ff-474f-bc1d-823a716fe7fc"), "YoungerChildren", "Second", "Равновесие: удержание прямая нога вперёд на 90 (фиксация 3 сек)", "Складка ноги врозь , складка ноги вместе (касаясь грудью, фиксация 3 сек)", "Мост с колен и встать обратно.", "Чир положение рук Т, ломаное Т", "«Два чир-прыжка подряд: «ТАК», «Стредл»»", "Cheer" },
                    { new Guid("0501c262-c743-4dc8-8073-861af0730f43"), "Baby", "Second", "Складка ноги вместе (касаясь грудью, фиксация 3 сек) ", "«Полумост» Лежа на спине поднять таз и захватить руками пятки одноименной согнутой ноги ", "Релеве по 2 позиции на 2х ногах", "Комбинация прыжков Сотэ по 6 и 2 позиции", "Положение рук «Руки на бедрах» ", null },
                    { new Guid("0f51b6ec-b247-4fae-b607-657a0b6ce962"), "Youth", "First", "Скорпион (юниорки).", "С разбега: рондат-отскок-рондат.", "Стойка на руках.", "Переворот назад.", "Связка одна восьмерка: Чир-положения рук (любы)+чир прыжки(обязательно минимум 2 подряд). Средний темп.", "Cheer" },
                    { new Guid("12e63ed0-9161-4985-a496-7a1295223d9f"), "Juniors", "First", "Шпагат (3 вида)", "Сложный элемент чир акробатики с отрывом от пола (на выбор)", "2 разных пируэта-Джазовый или классический на 720 минимум.-Пируэт на вторую позицию (а ля секонд) минимум 4 оборота", "Комбинация чир-прыжков  в темп ХЕДЛЕР вперед или в сторону+ТОЙ ТАЧ+ТОЙ ТАЧ", "Комбинация базовых положений рук с другими элементами (прыжками, махами, вращениями и т.д.) минимум на 24 счета.", "CheerFreestyle" },
                    { new Guid("1563a4af-5b1d-480e-b390-c5cc45edd212"), "Juniors", "Higher", "Шпагат с захватом ноги", "Элементы чир-акробатики ЭРИЭЛ (маховое боковое сальто) стойка на руках", "3 разных пируэта -Джазовый или классический на 1080 (3 оборота).-Пируэт на вторую позицию (а ля секонд) минимум 4 оборота-Лэг Холд (вперед или  сторону)", "Комбинация чир- прыжков в темп ХЕДЛЕР вперед или в сторону+ТОЙ ТАЧ+ПАЙК Или СТЭГ-СИТ+ТОЙ ТАЧ+ПАЙК", "Комбинация базовых положений рук с другими элементами (прыжками, махами, вращениями и т.д.) минимум на 32 счета.", "CheerFreestyle" },
                    { new Guid("25ab9c96-d04e-46ee-b865-2f2aec575525"), "BoysGirls", "Second", "Шпагат (2 вида)", "Кувырок вперед", "Джаз-пируэт на 360(1 оборот) ", "Чир прыжки – ХЕРКИ-«С»джамп", "Комбинация базовых положений рук с другими элементами (прыжками, махами, вращениями и т. д) минимум на 8 счетов", "CheerFreestyle" },
                    { new Guid("2f99ddbf-08f5-4001-a2e5-6511f1855aab"), "Youth", "Third", "Шпагат (один  на выбор)", "Высокие махи вперед  и в сторону (4 маха)", "Тур пике – 3 пируэта.", "Чир прыжки –– ХЕРКИ-«С»джамп", "Комбинация из  базовых положений рук, минимум 8 положений.", "CheerFreestyle" },
                    { new Guid("3b9b55bb-a96c-4aa3-bbea-f34ed0c224c3"), "Juniors", "Second", "Складка ноги вместе, складка нови врозь (касаясь грудью, фиксация 3 сек)", "Скорпион (женщины).", "С разбега: Вальсет-рондат-180рондат.", "Фляк с места.", "Стойка на руках.", "Cheer" },
                    { new Guid("421a2115-6ef8-41a0-bd61-534fba72630d"), "YoungerChildren", "Third", "Фиксация положения лодочка на спине, фиксация положения лодочка на животе (по 3 сек)", "Мост из положения лежа и лечь обратно. Фиксация 3 сек", "Равновесия: Положение «арабеск» на всей стопе(фиксация 3 сек) Поднятая назад нога вытянута и отведена на 45 или 90 градусов. Фиксация 3 сек.", "Чир прыжки:   «ТАК», «Стредл»", "Базовые положения рук «Правая диагональ», «Левая диагональ,»»Ти позиция»,» ломаное Т»", "CheerFreestyle" },
                    { new Guid("45c2dcb4-42af-4240-b984-9ca1a01fcc0c"), "YoungerChildren", "Higher", "Шпагат (3 вида)", "Колесо с опорой на две руки", "Джазовый пируэт на 720(2 оборота)", "Чир прыжки -ТОЙ ТАЧ", "Комбинация базовых положений рук с другими элементами (прыжками, махами, вращениями и т.д.) минимум на 16 счетов", "CheerFreestyle" },
                    { new Guid("4a3929a1-6462-4da2-9744-d92f4702031f"), "Juniors", "Higher", "Равновесие «Лук-стрела» (женщины).", "Шпагат (один на выбор) (девочки)", "Перекидка назад из положения сидя.", "Рондат-фляк-сальто или Рондат-сальто + рондат-фляк", "Переворот вперед-рондат-фляк.", "Cheer" },
                    { new Guid("5083591d-35a5-4531-8143-37a5e0a28eda"), "BoysGirls", "Higher", "Шпагат (два на выбор) (девочки)", "Скорпион – 1 нога (девочки)", "Переворот вперед - рондат.", "Чир прыжки: «Той тач» 2 в темп.", "Чир-положения рук: связка одна восьмерка (любые положения рук). Средний темп.", "Cheer" },
                    { new Guid("5183ad88-dde3-4410-bac9-e59bb98b114d"), "Baby", "Fourth", "Сед на полу,ноги вытянуты- 1.стопы на себя 2-вытянуть носки", "Удержание прямых ног сидя на полу", "Положение пассе сидя на полу", "Прыжок Сотэ по 6 позиции", "Положение «Клин» (Чистая позиция)", null },
                    { new Guid("5cea5f56-c86a-44e4-90dc-af315d3c6aac"), "Juniors", "Fourth", "Высокие махи вперед  и в сторону(4 минимум)", "Кувырок вперед, кувырок назад", "Тур пике – (минимум 3 пируэта)", "Чир прыжки «Абстракт» «Си-прыжок»", "Комбинация из  базовых положений рук, минимум 8 положений.", "CheerFreestyle" },
                    { new Guid("5fdfe987-96d8-45b3-a7f9-e6a4835cfc4f"), "Juniors", "Third", "Шпагат (один на выбор)", "Колесо с опорой на две руки", "Джазовый пируэт минимум на 720 градусов", "Чир прыжки-ХЕРКИ,-ХЕДЛЕР вперед,", "Комбинация базовых положений рук с другими элементами (прыжками, махами, вращениями и т.д) минимум на 8 счетов.", "CheerFreestyle" },
                    { new Guid("60a9c78c-3944-44fb-9a54-f9eecb766d57"), "Juniors", "Third", "Складка ноги вместе, складка нови врозь (касаясь грудью, фиксация 3 сек)", "Удержание ноги в положении «Легхолд» (Флажок) любая нога (женщины).", "Переворот вперед-рондат", "Стойка на руках – кувырок вперед.", "Колесо с опорой на одну руку.", "Cheer" },
                    { new Guid("612b0438-7f37-4fce-9bfa-c119dfe0d4f5"), "BoysGirls", "Fourth", "Складка ноги вместе, складка нови врозь (касаясь грудью, фиксация 3 сек)", "Мост из положения лежа и лечь обратно (фиксация 3 сек)", "Равновесия: Положение «пассе» на высоких полупальцах (фиксация 3 сек)", "Чир прыжки: «ТАК», «Стредл».", "Базовые положения рук (4 –минимум) Правая диагональ Левая диагональ Ти-позиция Ломаное Т", "CheerFreestyle" },
                    { new Guid("6e9bfde8-f6c8-4807-8c23-796a8bbfd6f8"), "Baby", "Third", "Сед пятки вместе-колени согнуты в стороны («бабочка»).Наклон вперед в,фиксация 3 сек ", "Прогиб назад стоя на коленях,захват руками пяток «Верблюд»", "Релеве по 6 позиции на 2х ногах", "Прыжок Сотэ по 2 позиции", "Положение рук Клэп", null },
                    { new Guid("7c5c603b-1fda-40dd-adc4-2440d4938aa8"), "YoungerChildren", "Fourth", "Складка ноги вместе, складка ноги врозь. (касаясь грудью, фиксация 3 сек)", "Стойка на лопатках «Березка»", "Равновесие на всей стопе, положение «пассе» («Либерти»)фиксация 3 сек.", "Чир прыжок «Стредл»", "Базовые положения рук Чистая позиция, Клэп, Хай Ви, Лоу Ви", "CheerFreestyle" },
                    { new Guid("89c11e72-1c14-4e1b-bd27-ea2bdcb3f157"), "Youth", "First", "Шпагат (3 вида)", "Колесо с опорой на одну руку", "Два разных пируэта – -Пируэт аттитюд-«Солнышко»", "Чир-прыжки-ХЕДЛЕР вперед-ТОЙ ТАЧ,-ПАЙК", "Комбинация базовых положений рук с другими элементами (ЧИР ДАНС) (прыжками, махами, вращениями и т.д.) минимум на 16 счетов", "CheerFreestyle" },
                    { new Guid("96e4056b-447c-4f0b-b026-eb9c4b222f95"), "YoungerChildren", "Higher", "Равновесие «Арабеск» (Ласточка) на любую ногу", "Кувырок вперед, кувырок назад", "Комбинация из 4х любых Чир положения рук.", "Шпагат (один любой)", "Рондат", "Cheer" },
                    { new Guid("9768683d-59b0-43e6-a734-586af4707035"), "Baby", "Higher", "Складка ноги вместе, складка ноги врозь. (касаясь грудью, фиксация 3 сек) ", "Упор лежа-прогиб назад,согнуть колени «Рыбка»", "Релеве на одной ноге.на всей стопе,вторая нога вытянута вперед на 30 градусов ", "Прыжок «Банана Сплит» ", "Положение рук Хай Ви ,Лоу Ви ", null },
                    { new Guid("9f1d2d1b-02d7-4242-8354-70471710ebd9"), "Youth", "Higher", "Равновесие «Лук-стрела» (юниорки).", "Переворот вперед через одну руку.", "Фляк с места.", "Маховое сальто", "Связка две восьмерки: Чир-положения рук (любые) +чир прыжки (обязательно минимум 3 подряд). Быстрый темп.", "Cheer" },
                    { new Guid("a5e9ce09-bf41-4e93-9418-581fffcfa507"), "YoungerChildren", "First", "чир прыжок «Так»", "Кувырок вперед, кувырок назад.", "Мост из положения стоя  и лечь обратно. Фиксация 3 сек.", "Чир положение рук Лоу Ви,к Хай Ви", "«Березка»", "Cheer" },
                    { new Guid("a743350f-f1b0-4238-94e7-4c6718a0355e"), "BoysGirls", "First", "Перекидка вперед на 2 ноги.", "Стойка на руках кувырок вперед", "Чир прыжки: «Той Тач», «Пайк».", "Шпагат поперечный.", "Рондат.", "Cheer" },
                    { new Guid("a760ef27-25a8-41c0-a827-3e5441d27d3b"), "Juniors", "Fourth", "Мост из положения лежа и лечь обратно (фиксация 3 сек).", "Равновесие «Арабеск» (Ласточка) на любую ногу.", "Колесо с опорой на две руки.", "Стойка на голове и руках (фиксация 3 сек).", "Связка одна восьмерка: Чир положения рук + чир прыжки. Средний темп.", "Cheer" },
                    { new Guid("abd562cf-2734-480b-8e71-a1ebe491f45e"), "YoungerChildren", "Third", "Мост из положения лежа и лечь обратно (фиксация 3 сек)", "фиксация положения лодочка на спине, фиксация положения лодочка на животе (по 3 сек)", "Кувырок вперед", "Чир положение рук Хай Ви, Лоу Ви, Ти,", "«Чир прыжок:  «ТАК»»", "Cheer" },
                    { new Guid("b155d139-5c0c-4fe0-8a0b-8c1a94b226cd"), "Baby", "First", "Сед ноги врозь-складка к правой ноге. складка к левой ноге (касаясь грудью, фиксация 3 сек) ", "«Корзиночка» Лежа на животе Захват каждой рукой одноименной  согнутой ноги ", "Равновесие на одной ноге.на всей стопе,вторая нога вытянута вперед на 30 градусов ", "Прыжок «Джампинг Джэк» ", "Положение рук  «Подсвечники». «Ведра»", null },
                    { new Guid("b2b37324-7a23-464d-bc76-22606e9c4b81"), "Youth", "Second", "Складка ноги вместе, складка нови врозь (касаясь грудью, фиксация 3 сек)", "Удержание ноги в положении «Легхолд» (Флажок) любая нога (юниорки).", "Переворот вперед-рондат.", "Чир-положения рук: Комбинация из минимум 8 положений.", "Чир прыжки: подряд «Так»+«Той Тач»+»Той Тач»", "Cheer" },
                    { new Guid("b35e6105-356d-4015-aa53-14313a7a583d"), "Youth", "Higher", "Шпагат с захватом ноги", "Элемент чир-акробатики Переворот вперед", "2 разных пируэта-Джазовый или классический на 720 минимум.(2 оборота)-Пируэт на вторую позицию (а ля секонд) минимум 3 оборота", "Комбинация чир-прыжков  в темп ХЕДЛЕР вперед или в сторону+ТОЙ ТАЧ+ТОЙ ТАЧ", "Комбинация базовых положений рук с другими элементами (ЧИР ДАНС) (прыжками, махами, вращениями и т.д.) минимум на 24 счета.", "CheerFreestyle" },
                    { new Guid("b3eee10b-c3c4-4c2b-b197-7c0980738025"), "Youth", "Second", "Шпагат (2 вида)", "Колесо с опорой на две руки", "Джаз-пируэт на 720", "Чир прыжки-ХЕДЛЕР ВПЕРЕД-ТОЙ ТАЧ", "Комбинация базовых положений рук с другими элементами  (прыжками, махами, вращениями и т.д.) минимум на 16 счетов", "CheerFreestyle" },
                    { new Guid("b710091d-86db-4012-acf6-aa87da9bbb79"), "Youth", "Fourth", "Складка ноги вместе, складка нови врозь. (касаясь грудью, фиксация 3 сек)", "Кувырок вперед", "Повороты на двух ногах с продвижением с удержанием точки -«шэне» (минимум 3)", "Чир прыжки: «Абстракт»,  «Стредл» ", "Комбинация из  базовых положений рук, минимум 6 положений.-Кинжалы-Тачдаун-Нижний Тачдаун-Эль (правая и левая)-Лук и стрела-Панч Ап", "CheerFreestyle" },
                    { new Guid("bb966ad8-1049-49d6-889a-16c19f3d8eb1"), "YoungerChildren", "Second", "Шпагат (один на выбор)", "Удержание вытянутой ноги (Флажок)-на любую ногу", "Повороты на двух ногах с продвижением с удержанием точки «шэне» (минимум 3)", "Чир прыжки: «ХЕРКИ»", "Комбинация из 6 базовых положений рук--Кинжалы-Тачдаун-Нижний Тачдаун-Эль (правая и левая)-Лук и стрела-Панч Ап", "CheerFreestyle" },
                    { new Guid("c096b900-85b3-4059-8b8f-ecac2faf7ff9"), "BoysGirls", "Third", "Мост из положения стоя и лечь обратно (фиксация 3 сек).", "Кувырок вперед, кувырок назад", "Равновесие «Арабеск» (Ласточка) на любую ногу", "Чир прыжки: «ТАК»+«Стредл». Два подряд.", "Положения рук: Хай Ви, Лоу Ви, Ти, Правая диагональ, Левая диагональ.", "Cheer" },
                    { new Guid("c1abc1ec-6525-4bfa-89e9-a78cc8d85952"), "Juniors", "Second", "Шпагат (2 вида)", "Колесо с опорой на одну руку", "Два разных пируэта-Пируэт аттитюд или арабеск—-«Солнышко", "Чир-прыжки  -ХЕДЛЕР вперед или  в сторону,---ТОЙ ТАЧ,-ПАЙК", "Комбинация базовых положений рук с другими элементами (ЧИР ДАНС) (прыжками, махами, вращениями и т.д.) минимум на 16 счетов", "CheerFreestyle" },
                    { new Guid("ca8e56ec-5977-4b69-a8f5-c5bcf2aabe33"), "YoungerChildren", "First", "Шпагат (2 вида)", "Кувырок вперед", "Джазовый пируэт на 360", "Чир прыжки «ХЕРДЛЕР ВПЕРЕД»", "Комбинация базовых положений рук с другими элементами (прыжками, махами,вращениями и т. д) минимум на 8 счетов ", "CheerFreestyle" },
                    { new Guid("cb9b85de-f4ba-4868-aeee-379761df592a"), "BoysGirls", "First", "Шпагат (3 вида)", "Колесо с опорой на две руки", "Джаз-пируэт на 720", "Чир прыжки –-ХЕДЛЕР вперед-ТОЙ ТАЧ", "Комбинация базовых положений рук с другими элементами  (прыжками, махами, вращениями и т.д.) минимум на 16 счетов", "CheerFreestyle" },
                    { new Guid("ccb085f8-afcb-4fd5-9a9a-057965537677"), "BoysGirls", "Second", "Складка ноги вместе, складка нови врозь (касаясь грудью, фиксация 3 сек)", "Мост из положения стоя и встать с моста.", "Колесо с опорой на две руки.", "Стойка на голове и руках.", "Чир прыжки: «ТАК», «Той Тач».", "Cheer" },
                    { new Guid("d346e586-66c4-49af-9237-dff934a0e1c9"), "Youth", "Fourth", "Мост из положения стоя и лечь обратно (фиксация 3 сек)", "Кувырок вперед, кувырок назад.", "Равновесие -ласточка на любую ногу (юниорки).", "Комбинация из ЧИР положений рук, минимум 6.", "Складка ноги врозь", "Cheer" },
                    { new Guid("d3cc718b-a89b-4e8c-a24b-e6e3de1fc02c"), "YoungerChildren", "Fourth", "Пресс 10 раз (лежа на спине, ноги согнуты в коленях);", "Прыжок вокруг себя 180+180 (положение рук свободное);", "Чир прыжок «Стредл»;", "Складка нови врозь (касаясь грудью, фиксация 3 сек)", "Равновесия: «Либерти» (цапелька) на любую ногу (фиксация 3 сек)", "Cheer" },
                    { new Guid("d6500514-1552-4e4d-94ed-46a92e605faa"), "Juniors", "First", "Равновесие «Лук-стрела» (женщины).", "Рондат-фляк.", "Той тач + Фляк", "Маховое сальто", "Связка три восьмерки (положения рук+чир прыжки (минимум 2 подряд)+махи ногами. Средний темп.", "Cheer" },
                    { new Guid("e859172b-d3e5-4dc2-9ee1-d34a355c94cb"), "BoysGirls", "Higher", "Шпагат с захватом ноги", "Колесо с опорой на одну руку ", "2разных пируэта – -Джазовый или классический на 720,-«Солнышко»", "Чир-прыжки-ХЕДЛЕР-ТОЙ ТАЧ,-ПАЙК или ДВОЙНАЯ ДЕВЯТКА", "Комбинация базовых положений рук с другими элементами (ЧИР ДАНС) (прыжками, махами, вращениями и т.д.) минимум на 24 счета", "CheerFreestyle" },
                    { new Guid("ef1a9dbd-4ffe-4efc-b3d7-758874537b53"), "BoysGirls", "Third", "Шпагат (один на выбор)", "Удержание вытянутой  ноги (Флажок)-на любую ногу", "Повороты на двух ногах с продвижением с удержанием точки -«шэне» (минимум 3).Фиксировать остановку.", "Чир прыжки: «Абстракт»", "Комбинация из  базовых положений рук, минимум 6 положений. Комбинация из 6 базовых положений рук--Кинжалы-Тачдаун-Нижний Тачдаун-Эль (правая и левая)-Лук и стрела-Панч Ап", "CheerFreestyle" },
                    { new Guid("f73735eb-8988-46a5-9d12-3231c5e4bd7a"), "Youth", "Third", "Складка ноги вместе, складка нови врозь (касаясь грудью, фиксация 3 сек)", "Мост из положения стоя и встать с моста.", "Колесо с опорой на две руки.", "Стойка на голове и руках.", "Чир прыжки: «ТойТач», «хеднер»", "Cheer" },
                    { new Guid("fb55a802-3c33-4c85-9500-98df715368d2"), "BoysGirls", "Fourth", "Складка ноги вместе, складка нови врозь (касаясь грудью, фиксация 3 сек)", "Мост из положения лежа и лечь обратно (фиксация 3 сек)", "Стойка на предплечьях (березка) (фиксация 3 сек)", "Чир прыжки: «ТАК», «Стредл».", "Кувырок вперед.", "Cheer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AthleteEntry_EntryId",
                table: "AthleteEntry",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_Athletes_AthleteElementsId",
                table: "Athletes",
                column: "AthleteElementsId",
                unique: true);

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
                name: "IX_GroupBlocks_ContestId",
                table: "GroupBlocks",
                column: "ContestId");

            migrationBuilder.CreateIndex(
                name: "IX_JudgeContest_JudgeId",
                table: "JudgeContest",
                column: "JudgeId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledCell_AthleteId",
                table: "ScheduledCell",
                column: "AthleteId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledCell_GroupBlockId",
                table: "ScheduledCell",
                column: "GroupBlockId");

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
                name: "IX_TestResults_AthleteId",
                table: "TestResults",
                column: "AthleteId");

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
                name: "JudgeContest");

            migrationBuilder.DropTable(
                name: "ScheduledCell");

            migrationBuilder.DropTable(
                name: "TestResults");

            migrationBuilder.DropTable(
                name: "Entries");

            migrationBuilder.DropTable(
                name: "GroupBlocks");

            migrationBuilder.DropTable(
                name: "Athletes");

            migrationBuilder.DropTable(
                name: "Contests");

            migrationBuilder.DropTable(
                name: "AthleteElements");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Clubs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
