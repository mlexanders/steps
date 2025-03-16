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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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