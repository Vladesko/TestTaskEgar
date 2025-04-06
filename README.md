# TestTaskEgar

## Первая задача

Необходимо реализовать ASP.NET.Core приложение, которое работает с некоторой сущностью ( далее - Сущность ).
___

+ Версия: **.NET 8**.
+ База данных: **PostgreSQL**.
+ ORM: **EntityFramework Core** с использованием *FluentAPI*
+ Логгирование с использованием библиотеки: **Serilog**
+ Дополнительно:
   + Используется "Чистая архитектура".

___

В данном случае я эту сущность поместил в слой **Domian** и дал название ей **SomeEntity**.

В репозитории есть папка с **docker-compose** файлами. Необходимо поднять в докере БД.
Использовал подход с миграциями.

Также в БД при запуске проекта будет создаваться таблица с логами, куда будут записываться логи.

___

### Первый метод
___

Метод **GET**, который возвращает массив сущностей.
Сущности хранятся в БД уже отсортированные по полю **Code**, соответственно мы отбираем определенные сущности, которые входят в указанный в виде параметров метода диапазон.

Сущность, которую мы возвращаем, такая же, как и в БД, что не есть хорошо, но это маленький проект и тут нет ничего сильно критичного. В продакшене конечно так делать не стоит.

___

### Второй метод

___ 

Сохраняет сущности в БД. Перед сохранением новых сущностей, он удаляет старые сущности. 

На вход в параметры принимает DTO, в котором параметр **Code** это тип данных *string* из-за чего приходится делать лишний парсинг. Если спарсить не удалось, то дефолтное значение для code будет **0**.

Еще было бы неплохо валидировать сразу DTO с использованием библиотек **FluentValidation** и **AutoValidation**. Однако это маленькое тестовое и не хотелось и я посчитал ненужным делать данные функционал. Ограничился минимальной валидацией, написанной руками.

___

### Итог

___

В целом задача небольшая и интересная, (если бы все такие были).

Тут не было применено лучших практик архитектурных решений. Можно было бы также прикрутить тесты, версионирование и так далее, но это же тестовое :) Много времени тратить не хочется.

Возможно название метод или переменных стоило бы изменить, где-то что-то улучшить, но как я уже сказал выше, это было бы намного больше усилий и времени.

___

## Вторая задача

В этой же Базе Данных можно открыть Query Tool и прописать следующие SQL - команды
+ Для создания первой таблицы
``` SQL
CREATE TABLE "Clients"
(
	"Id" bigint PRIMARY KEY,
	"ClientName" varchar(250)
)
```

+ Для создания второй таблицы

```sql
 CREATE TABLE "ClientContacts"
(
	"Id" bigint PRIMARY KEY,
	"ClientId" bigint REFERENCES "Clients"("Id"),
	"ContactType" varchar(255), 
	"ContactValue" varchar(255) 
)
```
____
### Первая задача

____

> 1.	Написать запрос, который возвращает наименование клиентов и кол-во контактов клиентов

**Ответ**

``` sql
SELECT "Clients"."ClientName", COUNT("ClientContacts"."ClientId")
FROM "Clients"
INNER JOIN "ClientContacts" ON "Clients"."Id" = "ClientContacts"."ClientId"
GROUP BY "ClientName"
```
____

### Вторая задача

____

> 2.	Написать запрос, который возвращает список клиентов, у которых есть *более 2* контактов

**Ответ**

``` sql
SELECT "Clients"."ClientName", COUNT("ClientContacts"."ClientId")
FROM "Clients"
INNER JOIN "ClientContacts" ON "Clients"."Id" = "ClientContacts"."ClientId"
GROUP BY "ClientName"
HAVING COUNT("ClientContacts"."ClientId") > 2
```

___

## Третья задача

Проверка знаний оконных функий.

Дана таблица:
Dates

Написать запрос, который возвращает интервалы для одинаковых Id. Например, есть такой набор данных:

| Id | Dt         |
|----|------------|
| 1  | 01.01.2021 |
| 1  | 10.01.2021 |
| 1  | 30.01.2021 |
| 2  | 15.01.2021 |
| 2  | 30.01.2021 |

Результатом выполнения запроса должен быть такой набор данных:

| Id | Sd         | Ed         |
|----|------------|------------|
| 1  | 01.01.2021 | 10.01.2021 |
| 1  | 10.01.2021 | 30.01.2021 |
| 2  | 15.01.2021 | 30.01.2021 |

**Ответ**

``` sql
SELECT "Id", "Sd", "Ed"
FROM (
    SELECT "Id", "Dt" AS "Sd", 
           LEAD("Dt") OVER(PARTITION BY "Id" ORDER BY "Id") AS "Ed"
    FROM "Dates"
) subquery
WHERE "Ed" IS NOT NULL;
```

Вместо звездочки я указываю параметры, потому что так лучше для производительности в будущем.
 


