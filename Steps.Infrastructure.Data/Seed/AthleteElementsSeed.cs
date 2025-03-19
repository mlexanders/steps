using Microsoft.EntityFrameworkCore;
using Steps.Domain.Entities;

namespace Steps.Infrastructure.Data.Seed;

public static class AthleteElementsSeed
{
    public static void SeedCheer(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AthleteElements>().HasData(
            new AthleteElements
            {
                Id = new Guid("1da76132-1eb5-48d1-91e6-ea48d977437a"),
                Degree = "Higher",
                Type = "Cheer",
                AgeCategory = "YoungerChildren",
                Element1 = "Равновесие «Арабеск» (Ласточка) на любую ногу",
                Element2 = "Кувырок вперед, кувырок назад",
                Element3 = "Комбинация из 4х любых Чир положения рук.",
                Element4 = "Шпагат (один любой)",
                Element5 = "Рондат"
            },
            new AthleteElements
            {
                Id = new Guid("2aad787d-4290-416f-8c35-a702fa9e7059"),
                Degree = "Higher",
                Type = "Cheer",
                AgeCategory = "BoysGirls",
                Element1 = "Шпагат (два на выбор) (девочки)",
                Element2 = "Скорпион – 1 нога (девочки)",
                Element3 = "Переворот вперед - рондат.",
                Element4 = "Чир прыжки: «Той тач» 2 в темп.",
                Element5 = "Чир-положения рук: связка одна восьмерка (любые положения рук). Средний темп."
            },
            new AthleteElements
            {
                Id = new Guid("2762fed4-0979-445c-9317-b5087116eeb9"),
                Degree = "Higher",
                Type = "Cheer",
                AgeCategory = "Youth",
                Element1 = "Равновесие «Лук-стрела» (юниорки).",
                Element2 = "Переворот вперед через одну руку.",
                Element3 = "Фляк с места.",
                Element4 = "Маховое сальто",
                Element5 = "Связка две восьмерки: Чир-положения рук (любые) +чир прыжки (обязательно минимум 3 подряд). Быстрый темп."
            },
            new AthleteElements
            {
                Id = new Guid("cb1f5b68-ef41-4c20-8891-00d6cfc4bdac"),
                Degree = "Higher",
                Type = "Cheer",
                AgeCategory = "Juniors",
                Element1 = "Равновесие «Лук-стрела» (женщины).",
                Element2 = "Шпагат (один на выбор) (девочки)",
                Element3 = "Перекидка назад из положения сидя.",
                Element4 = "Рондат-фляк-сальто или Рондат-сальто + рондат-фляк",
                Element5 = "Переворот вперед-рондат-фляк."
            },
            new AthleteElements
            {
                Id = new Guid("da07248f-63ea-4e57-834f-7f0fd0eefdda"),
                Degree = "First",
                Type = "Cheer",
                AgeCategory = "YoungerChildren",
                Element1 = "чир прыжок «Так»",
                Element2 = "Кувырок вперед, кувырок назад.",
                Element3 = "Мост из положения стоя  и лечь обратно. Фиксация 3 сек.",
                Element4 = "Чир положение рук Лоу Ви,к Хай Ви",
                Element5 = "«Березка»"
            },
            new AthleteElements
            {
                Id = new Guid("4bd90988-7f23-40dd-8c09-552a61123025"),
                Degree = "First",
                Type = "Cheer",
                AgeCategory = "BoysGirls",
                Element1 = "Перекидка вперед на 2 ноги.",
                Element2 = "Стойка на руках кувырок вперед",
                Element3 = "Чир прыжки: «Той Тач», «Пайк».",
                Element4 = "Шпагат поперечный.",
                Element5 = "Рондат."
            },
            new AthleteElements
            {
                Id = new Guid("63de6d87-776a-4bde-9c0c-af7af59337fc"),
                Degree = "First",
                Type = "Cheer",
                AgeCategory = "Youth",
                Element1 = "Скорпион (юниорки).",
                Element2 = "С разбега: рондат-отскок-рондат.",
                Element3 = "Стойка на руках.",
                Element4 = "Переворот назад.",
                Element5 = "Связка одна восьмерка: Чир-положения рук (любы)+чир прыжки(обязательно минимум 2 подряд). Средний темп."
            },
            new AthleteElements
            {
                Id = new Guid("8613749e-cb44-4716-a82b-3c2cb9619b1d"),
                Degree = "First",
                Type = "Cheer",
                AgeCategory = "Juniors",
                Element1 = "Равновесие «Лук-стрела» (женщины).",
                Element2 = "Рондат-фляк.",
                Element3 = "Той тач + Фляк",
                Element4 = "Маховое сальто",
                Element5 = "Связка три восьмерки (положения рук+чир прыжки (минимум 2 подряд)+махи ногами. Средний темп."
            },
            new AthleteElements
            {
                Id = new Guid("c2045d04-37e4-483e-a948-8717e01aea43"),
                Degree = "Second",
                Type = "Cheer",
                AgeCategory = "YoungerChildren",
                Element1 = "Равновесие: удержание прямая нога вперёд на 90 (фиксация 3 сек)",
                Element2 = "Складка ноги врозь , складка ноги вместе (касаясь грудью, фиксация 3 сек)",
                Element3 = "Мост с колен и встать обратно.",
                Element4 = "Чир положение рук Т, ломаное Т",
                Element5 = "«Два чир-прыжка подряд: «ТАК», «Стредл»»"
            },
            new AthleteElements
            {
                Id = new Guid("399126da-de21-4148-9975-26947f337f58"),
                Degree = "Second",
                Type = "Cheer",
                AgeCategory = "BoysGirls",
                Element1 = "Складка ноги вместе, складка нови врозь (касаясь грудью, фиксация 3 сек)",
                Element2 = "Мост из положения стоя и встать с моста.",
                Element3 = "Колесо с опорой на две руки.",
                Element4 = "Стойка на голове и руках.",
                Element5 = "Чир прыжки: «ТАК», «Той Тач»."
            },
            new AthleteElements
            {
                Id = new Guid("f4366979-9949-4bb1-a902-dca06b005902"),
                Degree = "Second",
                Type = "Cheer",
                AgeCategory = "Youth",
                Element1 = "Складка ноги вместе, складка нови врозь (касаясь грудью, фиксация 3 сек)",
                Element2 = "Удержание ноги в положении «Легхолд» (Флажок) любая нога (юниорки).",
                Element3 = "Переворот вперед-рондат.",
                Element4 = "Чир-положения рук: Комбинация из минимум 8 положений.",
                Element5 = "Чир прыжки: подряд «Так»+«Той Тач»+»Той Тач»"
            },
            new AthleteElements
            {
                Id = new Guid("25ac73a9-3959-492b-ac9f-afb6edc48c66"),
                Degree = "Second",
                Type = "Cheer",
                AgeCategory = "Juniors",
                Element1 = "Складка ноги вместе, складка нови врозь (касаясь грудью, фиксация 3 сек)",
                Element2 = "Скорпион (женщины).",
                Element3 = "С разбега: Вальсет-рондат-180рондат.",
                Element4 = "Фляк с места.",
                Element5 = "Стойка на руках."
            },
            new AthleteElements
            {
                Id = new Guid("9e10a242-b4a3-4ad6-8d14-e22d121cffd3"),
                Degree = "Third",
                Type = "Cheer",
                AgeCategory = "YoungerChildren",
                Element1 = "Мост из положения лежа и лечь обратно (фиксация 3 сек)",
                Element2 = "фиксация положения лодочка на спине, фиксация положения лодочка на животе (по 3 сек)",
                Element3 = "Кувырок вперед",
                Element4 = "Чир положение рук Хай Ви, Лоу Ви, Ти,",
                Element5 = "«Чир прыжок:  «ТАК»»"
            },
            new AthleteElements
            {
                Id = new Guid("85ea24a6-ad0a-45c9-a968-aedd006a42c4"),
                Degree = "Third",
                Type = "Cheer",
                AgeCategory = "BoysGirls",
                Element1 = "Мост из положения стоя и лечь обратно (фиксация 3 сек).",
                Element2 = "Кувырок вперед, кувырок назад",
                Element3 = "Равновесие «Арабеск» (Ласточка) на любую ногу",
                Element4 = "Чир прыжки: «ТАК»+«Стредл». Два подряд.",
                Element5 = "Положения рук: Хай Ви, Лоу Ви, Ти, Правая диагональ, Левая диагональ."
            },
            new AthleteElements
            {
                Id = new Guid("a83c5474-51f2-4df5-bcf3-d53ae63e66b2"),
                Degree = "Third",
                Type = "Cheer",
                AgeCategory = "Youth",
                Element1 = "Складка ноги вместе, складка нови врозь (касаясь грудью, фиксация 3 сек)",
                Element2 = "Мост из положения стоя и встать с моста.",
                Element3 = "Колесо с опорой на две руки.",
                Element4 = "Стойка на голове и руках.",
                Element5 = "Чир прыжки: «ТойТач», «хеднер»"
            },
            new AthleteElements
            {
                Id = new Guid("975a8db3-3898-4be4-8e57-c46cfe69e21d"),
                Degree = "Third",
                Type = "Cheer",
                AgeCategory = "Juniors",
                Element1 = "Складка ноги вместе, складка нови врозь (касаясь грудью, фиксация 3 сек)",
                Element2 = "Удержание ноги в положении «Легхолд» (Флажок) любая нога (женщины).",
                Element3 = "Переворот вперед-рондат",
                Element4 = "Стойка на руках – кувырок вперед.",
                Element5 = "Колесо с опорой на одну руку."
            },
            new AthleteElements
            {
                Id = new Guid("4df14af8-5f5e-400b-be3f-5facd73ce6f7"),
                Degree = "Fourth",
                Type = "Cheer",
                AgeCategory = "YoungerChildren",
                Element1 = "Пресс 10 раз (лежа на спине, ноги согнуты в коленях);",
                Element2 = "Прыжок вокруг себя 180+180 (положение рук свободное);",
                Element3 = "Чир прыжок «Стредл»;",
                Element4 = "Складка нови врозь (касаясь грудью, фиксация 3 сек)",
                Element5 = "Равновесия: «Либерти» (цапелька) на любую ногу (фиксация 3 сек)"
            },
            new AthleteElements
            {
                Id = new Guid("dc777699-ddd0-450a-b6a9-b96c6c9f7eb4"),
                Degree = "Fourth",
                Type = "Cheer",
                AgeCategory = "BoysGirls",
                Element1 = "Складка ноги вместе, складка нови врозь (касаясь грудью, фиксация 3 сек)",
                Element2 = "Мост из положения лежа и лечь обратно (фиксация 3 сек)",
                Element3 = "Стойка на предплечьях (березка) (фиксация 3 сек)",
                Element4 = "Чир прыжки: «ТАК», «Стредл».",
                Element5 = "Кувырок вперед."
            },
            new AthleteElements
            {
                Id = new Guid("ca56d07b-8aa2-4332-a900-28fa5cbe0e12"),
                Degree = "Fourth",
                Type = "Cheer",
                AgeCategory = "Youth",
                Element1 = "Мост из положения стоя и лечь обратно (фиксация 3 сек)",
                Element2 = "Кувырок вперед, кувырок назад.",
                Element3 = "Равновесие -ласточка на любую ногу (юниорки).",
                Element4 = "Комбинация из ЧИР положений рук, минимум 6.",
                Element5 = "Складка ноги врозь"
            },
            new AthleteElements
            {
                Id = new Guid("f4277582-e7c7-476d-ae7d-080252acc761"),
                Degree = "Fourth",
                Type = "Cheer",
                AgeCategory = "Juniors",
                Element1 = "Мост из положения лежа и лечь обратно (фиксация 3 сек).",
                Element2 = "Равновесие «Арабеск» (Ласточка) на любую ногу.",
                Element3 = "Колесо с опорой на две руки.",
                Element4 = "Стойка на голове и руках (фиксация 3 сек).",
                Element5 = "Связка одна восьмерка: Чир положения рук + чир прыжки. Средний темп."
            }
        );
    }
    
    public static void SeedCheerFreestyle(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AthleteElements>().HasData(
            new AthleteElements
            {
                Id = new Guid("3bf3fd78-b8a2-43eb-825c-f607d6b10f81"),
                Degree = "Higher",
                Type = "CheerFreestyle",
                AgeCategory = "YoungerChildren",
                Element1 = "Шпагат (3 вида)",
                Element2 = "Колесо с опорой на две руки",
                Element3 = "Джазовый пируэт на 720(2 оборота)",
                Element4 = "Чир прыжки -ТОЙ ТАЧ",
                Element5 = "Комбинация базовых положений рук с другими элементами (прыжками, махами, вращениями и т.д.) минимум на 16 счетов"
            },
            new AthleteElements
            {
                Id = new Guid("7e200741-9944-442c-a629-6a6b6aae3248"),
                Degree = "Higher",
                Type = "CheerFreestyle",
                AgeCategory = "BoysGirls",
                Element1 = "Шпагат с захватом ноги",
                Element2 = "Колесо с опорой на одну руку ",
                Element3 = "2разных пируэта – -Джазовый или классический на 720,-«Солнышко»",
                Element4 = "Чир-прыжки-ХЕДЛЕР-ТОЙ ТАЧ,-ПАЙК или ДВОЙНАЯ ДЕВЯТКА",
                Element5 = "Комбинация базовых положений рук с другими элементами (ЧИР ДАНС) (прыжками, махами, вращениями и т.д.) минимум на 24 счета"
            },
            new AthleteElements
            {
                Id = new Guid("bc20becb-e01e-4821-91b6-f7b5cef2a598"),
                Degree = "Higher",
                Type = "CheerFreestyle",
                AgeCategory = "Youth",
                Element1 = "Шпагат с захватом ноги",
                Element2 = "Элемент чир-акробатики Переворот вперед",
                Element3 = "2 разных пируэта-Джазовый или классический на 720 минимум.(2 оборота)-Пируэт на вторую позицию (а ля секонд) минимум 3 оборота",
                Element4 = "Комбинация чир-прыжков  в темп ХЕДЛЕР вперед или в сторону+ТОЙ ТАЧ+ТОЙ ТАЧ",
                Element5 = "Комбинация базовых положений рук с другими элементами (ЧИР ДАНС) (прыжками, махами, вращениями и т.д.) минимум на 24 счета."
            },
            new AthleteElements
            {
                Id = new Guid("00123435-0244-42c2-a6be-c0b185c35bed"),
                Degree = "Higher",
                Type = "CheerFreestyle",
                AgeCategory = "Juniors",
                Element1 = "Шпагат с захватом ноги",
                Element2 = "Элементы чир-акробатики ЭРИЭЛ (маховое боковое сальто) стойка на руках",
                Element3 = "3 разных пируэта -Джазовый или классический на 1080 (3 оборота).-Пируэт на вторую позицию (а ля секонд) минимум 4 оборота-Лэг Холд (вперед или  сторону)",
                Element4 = "Комбинация чир- прыжков в темп ХЕДЛЕР вперед или в сторону+ТОЙ ТАЧ+ПАЙК Или СТЭГ-СИТ+ТОЙ ТАЧ+ПАЙК",
                Element5 = "Комбинация базовых положений рук с другими элементами (прыжками, махами, вращениями и т.д.) минимум на 32 счета."
            },
            new AthleteElements
            {
                Id = new Guid("831478e5-0002-49f5-94fe-3db452cc7b81"),
                Degree = "First",
                Type = "CheerFreestyle",
                AgeCategory = "YoungerChildren",
                Element1 = "Шпагат (2 вида)",
                Element2 = "Кувырок вперед",
                Element3 = "Джазовый пируэт на 360",
                Element4 = "Чир прыжки «ХЕРДЛЕР ВПЕРЕД»",
                Element5 = "Комбинация базовых положений рук с другими элементами (прыжками, махами,вращениями и т. д) минимум на 8 счетов "
            },
            new AthleteElements
            {
                Id = new Guid("2d988641-9e29-4d28-822d-1e54f53b014d"),
                Degree = "First",
                Type = "CheerFreestyle",
                AgeCategory = "BoysGirls",
                Element1 = "Шпагат (3 вида)",
                Element2 = "Колесо с опорой на две руки",
                Element3 = "Джаз-пируэт на 720",
                Element4 = "Чир прыжки –-ХЕДЛЕР вперед-ТОЙ ТАЧ",
                Element5 = "Комбинация базовых положений рук с другими элементами  (прыжками, махами, вращениями и т.д.) минимум на 16 счетов"
            },
            new AthleteElements
            {
                Id = new Guid("4c669b05-33d2-47fc-8ed9-3a5cbcc20553"),
                Degree = "First",
                Type = "CheerFreestyle",
                AgeCategory = "Youth",
                Element1 = "Шпагат (3 вида)",
                Element2 = "Колесо с опорой на одну руку",
                Element3 = "Два разных пируэта – -Пируэт аттитюд-«Солнышко»",
                Element4 = "Чир-прыжки-ХЕДЛЕР вперед-ТОЙ ТАЧ,-ПАЙК",
                Element5 = "Комбинация базовых положений рук с другими элементами (ЧИР ДАНС) (прыжками, махами, вращениями и т.д.) минимум на 16 счетов"
            },
            new AthleteElements
            {
                Id = new Guid("20d05371-4141-4cba-b620-002ab98d57f3"),
                Degree = "First",
                Type = "CheerFreestyle",
                AgeCategory = "Juniors",
                Element1 = "Шпагат (3 вида)",
                Element2 = "Сложный элемент чир акробатики с отрывом от пола (на выбор)",
                Element3 = "2 разных пируэта-Джазовый или классический на 720 минимум.-Пируэт на вторую позицию (а ля секонд) минимум 4 оборота",
                Element4 = "Комбинация чир-прыжков  в темп ХЕДЛЕР вперед или в сторону+ТОЙ ТАЧ+ТОЙ ТАЧ",
                Element5 = "Комбинация базовых положений рук с другими элементами (прыжками, махами, вращениями и т.д.) минимум на 24 счета."
            },
            new AthleteElements
            {
                Id = new Guid("8ca7a4f3-8924-4f45-9f28-3e8a94db84ba"),
                Degree = "Second",
                Type = "CheerFreestyle",
                AgeCategory = "YoungerChildren",
                Element1 = "Шпагат (один на выбор)",
                Element2 = "Удержание вытянутой ноги (Флажок)-на любую ногу",
                Element3 = "Повороты на двух ногах с продвижением с удержанием точки «шэне» (минимум 3)",
                Element4 = "Чир прыжки: «ХЕРКИ»",
                Element5 = "Комбинация из 6 базовых положений рук--Кинжалы-Тачдаун-Нижний Тачдаун-Эль (правая и левая)-Лук и стрела-Панч Ап"
            },
            new AthleteElements
            {
                Id = new Guid("e39c706d-33f4-45ef-8e86-6c92587e4ccc"),
                Degree = "Second",
                Type = "CheerFreestyle",
                AgeCategory = "BoysGirls",
                Element1 = "Шпагат (2 вида)",
                Element2 = "Кувырок вперед",
                Element3 = "Джаз-пируэт на 360(1 оборот) ",
                Element4 = "Чир прыжки – ХЕРКИ-«С»джамп",
                Element5 = "Комбинация базовых положений рук с другими элементами (прыжками, махами, вращениями и т. д) минимум на 8 счетов"
            },
            new AthleteElements
            {
                Id = new Guid("87631c44-76f7-486d-84a5-13835c4ff06c"),
                Degree = "Second",
                Type = "CheerFreestyle",
                AgeCategory = "Youth",
                Element1 = "Шпагат (2 вида)",
                Element2 = "Колесо с опорой на две руки",
                Element3 = "Джаз-пируэт на 720",
                Element4 = "Чир прыжки-ХЕДЛЕР ВПЕРЕД-ТОЙ ТАЧ",
                Element5 = "Комбинация базовых положений рук с другими элементами  (прыжками, махами, вращениями и т.д.) минимум на 16 счетов"
            },
            new AthleteElements
            {
                Id = new Guid("03889f6f-9c0c-4deb-9ed1-0f3ea789eee3"),
                Degree = "Second",
                Type = "CheerFreestyle",
                AgeCategory = "Juniors",
                Element1 = "Шпагат (2 вида)",
                Element2 = "Колесо с опорой на одну руку",
                Element3 = "Два разных пируэта-Пируэт аттитюд или арабеск—-«Солнышко",
                Element4 = "Чир-прыжки  -ХЕДЛЕР вперед или  в сторону,---ТОЙ ТАЧ,-ПАЙК",
                Element5 = "Комбинация базовых положений рук с другими элементами (ЧИР ДАНС) (прыжками, махами, вращениями и т.д.) минимум на 16 счетов"
            },
            new AthleteElements
            {
                Id = new Guid("169708ca-247f-46ee-8e32-9bd141c377f9"),
                Degree = "Third",
                Type = "CheerFreestyle",
                AgeCategory = "YoungerChildren",
                Element1 = "Фиксация положения лодочка на спине, фиксация положения лодочка на животе (по 3 сек)",
                Element2 = "Мост из положения лежа и лечь обратно. Фиксация 3 сек",
                Element3 = "Равновесия: Положение «арабеск» на всей стопе(фиксация 3 сек) Поднятая назад нога вытянута и отведена на 45 или 90 градусов. Фиксация 3 сек.",
                Element4 = "Чир прыжки:   «ТАК», «Стредл»",
                Element5 = "Базовые положения рук «Правая диагональ», «Левая диагональ,»»Ти позиция»,» ломаное Т»"
            },
            new AthleteElements
            {
                Id = new Guid("48ba0eea-5f60-4748-8563-ff7f2ea6ab8c"),
                Degree = "Third",
                Type = "CheerFreestyle",
                AgeCategory = "BoysGirls",
                Element1 = "Шпагат (один на выбор)",
                Element2 = "Удержание вытянутой  ноги (Флажок)-на любую ногу",
                Element3 = "Повороты на двух ногах с продвижением с удержанием точки -«шэне» (минимум 3).Фиксировать остановку.",
                Element4 = "Чир прыжки: «Абстракт»",
                Element5 = "Комбинация из  базовых положений рук, минимум 6 положений. Комбинация из 6 базовых положений рук--Кинжалы-Тачдаун-Нижний Тачдаун-Эль (правая и левая)-Лук и стрела-Панч Ап"
            },
            new AthleteElements
            {
                Id = new Guid("8a1c9fbb-eba8-4e67-a24c-ff06b4e279f7"),
                Degree = "Third",
                Type = "CheerFreestyle",
                AgeCategory = "Youth",
                Element1 = "Шпагат (один  на выбор)",
                Element2 = "Высокие махи вперед  и в сторону (4 маха)",
                Element3 = "Тур пике – 3 пируэта.",
                Element4 = "Чир прыжки –– ХЕРКИ-«С»джамп",
                Element5 = "Комбинация из  базовых положений рук, минимум 8 положений."
            },
            new AthleteElements
            {
                Id = new Guid("33bd07ef-9017-4a38-8d8e-92d723ecfb05"),
                Degree = "Third",
                Type = "CheerFreestyle",
                AgeCategory = "Juniors",
                Element1 = "Шпагат (один на выбор)",
                Element2 = "Колесо с опорой на две руки",
                Element3 = "Джазовый пируэт минимум на 720 градусов",
                Element4 = "Чир прыжки-ХЕРКИ,-ХЕДЛЕР вперед,",
                Element5 = "Комбинация базовых положений рук с другими элементами (прыжками, махами, вращениями и т.д) минимум на 8 счетов."
            },
            new AthleteElements
            {
                Id = new Guid("d54e0bf6-8995-4023-843b-cabf1132b42d"),
                Degree = "Fourth",
                Type = "CheerFreestyle",
                AgeCategory = "YoungerChildren",
                Element1 = "Складка ноги вместе, складка ноги врозь. (касаясь грудью, фиксация 3 сек)",
                Element2 = "Стойка на лопатках «Березка»",
                Element3 = "Равновесие на всей стопе, положение «пассе» («Либерти»)фиксация 3 сек.",
                Element4 = "Чир прыжок «Стредл»",
                Element5 = "Базовые положения рук Чистая позиция, Клэп, Хай Ви, Лоу Ви"
            },
            new AthleteElements
            {
                Id = new Guid("c6557597-ca45-4d84-9103-a9eaea33b811"),
                Degree = "Fourth",
                Type = "CheerFreestyle",
                AgeCategory = "BoysGirls",
                Element1 = "Складка ноги вместе, складка нови врозь (касаясь грудью, фиксация 3 сек)",
                Element2 = "Мост из положения лежа и лечь обратно (фиксация 3 сек)",
                Element3 = "Равновесия: Положение «пассе» на высоких полупальцах (фиксация 3 сек)",
                Element4 = "Чир прыжки: «ТАК», «Стредл».",
                Element5 = "Базовые положения рук (4 –минимум) Правая диагональ Левая диагональ Ти-позиция Ломаное Т"
            },
            new AthleteElements
            {
                Id = new Guid("43a080b5-6bca-4ca6-bae8-9aca8e71ab09"),
                Degree = "Fourth",
                Type = "CheerFreestyle",
                AgeCategory = "Youth",
                Element1 = "Складка ноги вместе, складка нови врозь. (касаясь грудью, фиксация 3 сек)",
                Element2 = "Кувырок вперед",
                Element3 = "Повороты на двух ногах с продвижением с удержанием точки -«шэне» (минимум 3)",
                Element4 = "Чир прыжки: «Абстракт»,  «Стредл» ",
                Element5 = "Комбинация из  базовых положений рук, минимум 6 положений.-Кинжалы-Тачдаун-Нижний Тачдаун-Эль (правая и левая)-Лук и стрела-Панч Ап"
            },
            new AthleteElements
            {
                Id = new Guid("bb06d451-f848-4d99-af97-c9bf88192542"),
                Degree = "Fourth",
                Type = "CheerFreestyle",
                AgeCategory = "Juniors",
                Element1 = "Высокие махи вперед  и в сторону(4 минимум)",
                Element2 = "Кувырок вперед, кувырок назад",
                Element3 = "Тур пике – (минимум 3 пируэта)",
                Element4 = "Чир прыжки «Абстракт» «Си-прыжок»",
                Element5 = "Комбинация из  базовых положений рук, минимум 8 положений."
            }
        );
    }
    
    public static void SeedBaby(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AthleteElements>().HasData(
            new AthleteElements
            {
                Id = new Guid("21e9f32a-b76e-42ff-805f-eb7c08fcfb0f"),
                Degree = "Higher",
                AgeCategory = "Baby",
                Element1 = "Складка ноги вместе, складка ноги врозь. (касаясь грудью, фиксация 3 сек) ",
                Element2 = "Упор лежа-прогиб назад,согнуть колени «Рыбка»",
                Element3 = "Релеве на одной ноге.на всей стопе,вторая нога вытянута вперед на 30 градусов ",
                Element4 = "Прыжок «Банана Сплит» ",
                Element5 = "Положение рук Хай Ви ,Лоу Ви "
            },
            new AthleteElements
            {
                Id = new Guid("c730cacd-5e2e-4372-8e06-3d7f2d95e01d"),
                Degree = "First",
                AgeCategory = "Baby",
                Element1 = "Сед ноги врозь-складка к правой ноге. складка к левой ноге (касаясь грудью, фиксация 3 сек) ",
                Element2 = "«Корзиночка» Лежа на животе Захват каждой рукой одноименной  согнутой ноги ",
                Element3 = "Равновесие на одной ноге.на всей стопе,вторая нога вытянута вперед на 30 градусов ",
                Element4 = "Прыжок «Джампинг Джэк» ",
                Element5 = "Положение рук  «Подсвечники». «Ведра»"
            },
            new AthleteElements
            {
                Id = new Guid("2532c449-54a4-4cb0-bec2-7ae7ffb4036e"),
                Degree = "Second",
                AgeCategory = "Baby",
                Element1 = "Складка ноги вместе (касаясь грудью, фиксация 3 сек) ",
                Element2 = "«Полумост» Лежа на спине поднять таз и захватить руками пятки одноименной согнутой ноги ",
                Element3 = "Релеве по 2 позиции на 2х ногах",
                Element4 = "Комбинация прыжков Сотэ по 6 и 2 позиции",
                Element5 = "Положение рук «Руки на бедрах» "
            },
            new AthleteElements
            {
                Id = new Guid("afdbb17a-5352-48b4-bbeb-4b61bd2c0355"),
                Degree = "Third",
                AgeCategory = "Baby",
                Element1 = "Сед пятки вместе-колени согнуты в стороны («бабочка»).Наклон вперед в,фиксация 3 сек ",
                Element2 = "Прогиб назад стоя на коленях,захват руками пяток «Верблюд»",
                Element3 = "Релеве по 6 позиции на 2х ногах",
                Element4 = "Прыжок Сотэ по 2 позиции",
                Element5 = "Положение рук Клэп"
            },
            new AthleteElements
            {
                Id = new Guid("45b91fc5-0858-4c66-aa21-ca2cc220b993"),
                Degree = "Fourth",
                AgeCategory = "Baby",
                Element1 = "Сед на полу,ноги вытянуты- 1.стопы на себя 2-вытянуть носки",
                Element2 = "Удержание прямых ног сидя на полу",
                Element3 = "Положение пассе сидя на полу",
                Element4 = "Прыжок Сотэ по 6 позиции",
                Element5 = "Положение «Клин» (Чистая позиция)"
            }
        );
    }
}