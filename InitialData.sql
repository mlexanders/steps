-- Очистка таблиц (в правильном порядке с учетом внешних ключей)
TRUNCATE TABLE "Athletes" CASCADE;
TRUNCATE TABLE "Clubs" CASCADE;
TRUNCATE TABLE "Teams" CASCADE;
TRUNCATE TABLE "Users" CASCADE;

-- Создание пользователей
INSERT INTO "Users" ("Id", "Login", "Role", "PasswordHash")
VALUES 
    ('11111111-1111-1111-1111-111111111111'::uuid, 'string', 1, 'AQAAAAIAAYagAAAAEEdzCVhvWFrBPdv7nwR/oOU8QlNNiDA1BzGY/y/Xb6HSjy1vEtoOAWfsYWIjxZ04YA=='),
    ('22222222-2222-2222-2222-222222222222'::uuid, 'counter', 2, 'AQAAAAIAAYagAAAAEEdzCVhvWFrBPdv7nwR/oOU8QlNNiDA1BzGY/y/Xb6HSjy1vEtoOAWfsYWIjxZ04YA=='),
    ('33333333-3333-3333-3333-333333333333'::uuid, 'judge1', 3, 'AQAAAAIAAYagAAAAEEdzCVhvWFrBPdv7nwR/oOU8QlNNiDA1BzGY/y/Xb6HSjy1vEtoOAWfsYWIjxZ04YA=='),
    ('44444444-4444-4444-4444-444444444444'::uuid, 'judge2', 3, 'AQAAAAIAAYagAAAAEEdzCVhvWFrBPdv7nwR/oOU8QlNNiDA1BzGY/y/Xb6HSjy1vEtoOAWfsYWIjxZ04YA=='),
    ('55555555-5555-5555-5555-555555555555'::uuid, 'judge3', 3, 'AQAAAAIAAYagAAAAEEdzCVhvWFrBPdv7nwR/oOU8QlNNiDA1BzGY/y/Xb6HSjy1vEtoOAWfsYWIjxZ04YA==');

-- Создание клубов
INSERT INTO "Clubs" ("Id", "Name", "OwnerId")
VALUES 
    ('aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa'::uuid, 'Чирлидинг Клуб "Старт"', '11111111-1111-1111-1111-111111111111'::uuid),
    ('bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb'::uuid, 'Чирлидинг Клуб "Победа"', '11111111-1111-1111-1111-111111111111'::uuid),
    ('cccccccc-cccc-cccc-cccc-cccccccccccc'::uuid, 'Чирлидинг Клуб "Олимп"', '11111111-1111-1111-1111-111111111111'::uuid),
    ('dddddddd-dddd-dddd-dddd-dddddddddddd'::uuid, 'Чирлидинг Клуб "Юность"', '11111111-1111-1111-1111-111111111111'::uuid),
    ('eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee'::uuid, 'Чирлидинг Клуб "Спорт"', '11111111-1111-1111-1111-111111111111'::uuid);

-- Создание команд
INSERT INTO "Teams" ("Id", "Name", "Address", "ClubId", "OwnerId")
VALUES 
    -- Команды клуба "Старт"
    ('aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa'::uuid, 'Старт-1', 'ул. Спортивная, 1', 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa'::uuid, '11111111-1111-1111-1111-111111111111'::uuid),
    ('aaaaaaaa-2222-2222-2222-aaaaaaaaaaaa'::uuid, 'Старт-2', 'ул. Спортивная, 1', 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa'::uuid, '11111111-1111-1111-1111-111111111111'::uuid),
    ('aaaaaaaa-3333-3333-3333-aaaaaaaaaaaa'::uuid, 'Старт-3', 'ул. Спортивная, 1', 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa'::uuid, '11111111-1111-1111-1111-111111111111'::uuid),
    
    -- Команды клуба "Победа"
    ('bbbbbbbb-1111-1111-1111-bbbbbbbbbbbb'::uuid, 'Победа-1', 'ул. Победы, 10', 'bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb'::uuid, '11111111-1111-1111-1111-111111111111'::uuid),
    ('bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb'::uuid, 'Победа-2', 'ул. Победы, 10', 'bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb'::uuid, '11111111-1111-1111-1111-111111111111'::uuid),
    
    -- Команды клуба "Олимп"
    ('cccccccc-1111-1111-1111-cccccccccccc'::uuid, 'Олимп-1', 'ул. Олимпийская, 5', 'cccccccc-cccc-cccc-cccc-cccccccccccc'::uuid, '11111111-1111-1111-1111-111111111111'::uuid),
    ('cccccccc-2222-2222-2222-cccccccccccc'::uuid, 'Олимп-2', 'ул. Олимпийская, 5', 'cccccccc-cccc-cccc-cccc-cccccccccccc'::uuid, '11111111-1111-1111-1111-111111111111'::uuid),
    ('cccccccc-3333-3333-3333-cccccccccccc'::uuid, 'Олимп-3', 'ул. Олимпийская, 5', 'cccccccc-cccc-cccc-cccc-cccccccccccc'::uuid, '11111111-1111-1111-1111-111111111111'::uuid),
    
    -- Команды клуба "Юность"
    ('dddddddd-1111-1111-1111-dddddddddddd'::uuid, 'Юность-1', 'ул. Молодежная, 15', 'dddddddd-dddd-dddd-dddd-dddddddddddd'::uuid, '11111111-1111-1111-1111-111111111111'::uuid),
    ('dddddddd-2222-2222-2222-dddddddddddd'::uuid, 'Юность-2', 'ул. Молодежная, 15', 'dddddddd-dddd-dddd-dddd-dddddddddddd'::uuid, '11111111-1111-1111-1111-111111111111'::uuid),
    
    -- Команды клуба "Спорт"
    ('eeeeeeee-1111-1111-1111-eeeeeeeeeeee'::uuid, 'Спорт-1', 'ул. Физкультурная, 20', 'eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee'::uuid, '11111111-1111-1111-1111-111111111111'::uuid),
    ('eeeeeeee-2222-2222-2222-eeeeeeeeeeee'::uuid, 'Спорт-2', 'ул. Физкультурная, 20', 'eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee'::uuid, '11111111-1111-1111-1111-111111111111'::uuid),
    ('eeeeeeee-3333-3333-3333-eeeeeeeeeeee'::uuid, 'Спорт-3', 'ул. Физкультурная, 20', 'eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee'::uuid, '11111111-1111-1111-1111-111111111111'::uuid);

-- Создание спортсменов
INSERT INTO "Athletes" ("Id", "FullName", "BirthDate", "AgeCategory", "Degree", "AthleteType", "TeamId")
VALUES 
    (gen_random_uuid(), 'Иванов Иван Иванович', '2010-01-01', 0, 0, 0, 'aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa'::uuid),
    (gen_random_uuid(), 'Петров Петр Петрович', '2011-02-02', 1, 1, 0, 'aaaaaaaa-1111-1111-1111-aaaaaaaaaaaa'::uuid),
    
    (gen_random_uuid(), 'Александров Александр Александрович', '2013-04-04', 3, 3, 0, 'aaaaaaaa-2222-2222-2222-aaaaaaaaaaaa'::uuid),
    (gen_random_uuid(), 'Николаев Николай Николаевич', '2014-05-05', 4, 4, 0, 'aaaaaaaa-2222-2222-2222-aaaaaaaaaaaa'::uuid),
    (gen_random_uuid(), 'Смирнов Дмитрий Андреевич', '2010-06-06', 0, 0, 0, 'aaaaaaaa-2222-2222-2222-aaaaaaaaaaaa'::uuid),
    
    (gen_random_uuid(), 'Козлов Артем Сергеевич', '2011-07-07', 1, 1, 0, 'aaaaaaaa-3333-3333-3333-aaaaaaaaaaaa'::uuid),
    (gen_random_uuid(), 'Попов Егор Владимирович', '2012-08-08', 2, 2, 0, 'aaaaaaaa-3333-3333-3333-aaaaaaaaaaaa'::uuid),
    (gen_random_uuid(), 'Васильев Василий Васильевич', '2010-09-09', 0, 0, 0, 'aaaaaaaa-3333-3333-3333-aaaaaaaaaaaa'::uuid),
    
    (gen_random_uuid(), 'Кузнецов Михаил Александрович', '2011-10-10', 1, 1, 0, 'bbbbbbbb-1111-1111-1111-bbbbbbbbbbbb'::uuid),
    (gen_random_uuid(), 'Соколов Илья Дмитриевич', '2012-11-11', 2, 2, 0, 'bbbbbbbb-1111-1111-1111-bbbbbbbbbbbb'::uuid),
    (gen_random_uuid(), 'Новиков Даниил Сергеевич', '2013-12-12', 3, 3, 0, 'bbbbbbbb-1111-1111-1111-bbbbbbbbbbbb'::uuid),
    
    (gen_random_uuid(), 'Морозов Кирилл Андреевич', '2014-01-13', 4, 4, 0, 'bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb'::uuid),
    (gen_random_uuid(), 'Волков Матвей Владимирович', '2010-02-14', 0, 0, 0, 'bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb'::uuid),
    (gen_random_uuid(), 'Алексеев Тимофей Александрович', '2011-03-15', 1, 1, 0, 'bbbbbbbb-2222-2222-2222-bbbbbbbbbbbb'::uuid),
    
    (gen_random_uuid(), 'Лебедев Марк Дмитриевич', '2012-04-16', 2, 2, 0, 'cccccccc-1111-1111-1111-cccccccccccc'::uuid),
    (gen_random_uuid(), 'Семенов Семен Семенович', '2010-05-17', 0, 0, 0, 'cccccccc-1111-1111-1111-cccccccccccc'::uuid),
    (gen_random_uuid(), 'Голубев Андрей Сергеевич', '2011-06-18', 1, 1, 0, 'cccccccc-1111-1111-1111-cccccccccccc'::uuid),
    
    (gen_random_uuid(), 'Виноградов Денис Александрович', '2012-07-19', 2, 2, 0, 'cccccccc-2222-2222-2222-cccccccccccc'::uuid),
    (gen_random_uuid(), 'Богданов Иван Владимирович', '2013-08-20', 3, 3, 0, 'cccccccc-2222-2222-2222-cccccccccccc'::uuid),
    (gen_random_uuid(), 'Воробьев Максим Андреевич', '2014-09-21', 4, 4, 0, 'cccccccc-2222-2222-2222-cccccccccccc'::uuid),
    
    (gen_random_uuid(), 'Федоров Роман Дмитриевич', '2010-10-22', 0, 0, 0, 'cccccccc-3333-3333-3333-cccccccccccc'::uuid),
    (gen_random_uuid(), 'Комиссаров Арсений Александрович', '2011-11-23', 1, 1, 0, 'cccccccc-3333-3333-3333-cccccccccccc'::uuid),
    (gen_random_uuid(), 'Дмитриев Лев Сергеевич', '2012-12-24', 2, 2, 0, 'cccccccc-3333-3333-3333-cccccccccccc'::uuid);
