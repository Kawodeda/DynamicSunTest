# Тестовое задание DynamicSun

## Приложение для работы с погодными архивами

Написано на ASP.NET Core MVC 7

В качестве СУБД используется: PostgreSQL 16.1

В качестве ORM используется: Entity Framework Core

Для начала работы с БД нужно:
1. Указать вашу строку подключения в appsettings.json под именем "WeatherArchiveDb"
2. Применить к БД все миграции. Это, например, можно сделать командой "Update-Database" в консоли менеджера пакетов
