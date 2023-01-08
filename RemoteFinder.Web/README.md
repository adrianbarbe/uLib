About the project:


For DEVs:
How to run project:

-=Migration Create=-
dotnet ef migrations add Initial -c "MainContext" -p "../RemoteFinder.DAL"

-=Migrations apply=-
dotnet ef database update --context "MainContext"

-=Migrations remove=-
dotnet ef migrations remove -c "MainContext" -p "../RemoteFinder.DAL"

-=Create Script=- //--idempotent
dotnet ef migrations script InitialMigration --output ../RemoteFinder.DAL/MigrationSql/migration.sql --context "MainContext"


Set ENV variables:
ASPNETCORE_ENVIRONMENT=Development
DbConnectionString=Host=10.10.10.10;Database=filemanager;Username=postgres;Password=yourpassword

where 10.10.10.10 should be replaced with your DB host address
and yourpassword should be replaced with Db password


For users:
How to use project:
