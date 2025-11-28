@echo off
echo Updating database...

dotnet ef database update ^
	--project src\ScalableApplication.Infrastructure ^
	--startup-project src\ScalableApplication.API

echo Database update completed.