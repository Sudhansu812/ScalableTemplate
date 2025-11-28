@echo off
SET MIGRATION_NAME=%1

IF "%MIGRATION_NAME%"=="" (
	ECHO Migration name not provided.
	ECHO Usage: add-migration.bat [MigrationName]
	EXIT /B 1
)

ECHO Starting Migration: %MIGRATION_NAME%...

dotnet ef migrations add %MIGRATION_NAME% ^
	--project src\ScalableApplication.Infrastructure ^
	--startup-project src\ScalableApplication.API ^
	--output-dir Persistence\Migrations
