1. Создание миграций БД
dotnet ef migrations add Initial --context IdentityContext --project BeautyStore.Identity --startup-project BeautyStore
dotnet ef migrations add Initial --context Context --project BeautyStore.DAL --startup-project BeautyStore

2. Применение
dotnet ef database update --context IdentityContext --project BeautyStore.Identity --startup-project BeautyStore
dotnet ef database update --context Context --project BeautyStore.DAL --startup-project BeautyStore

3. Запустить скрипты из папки Start Helper