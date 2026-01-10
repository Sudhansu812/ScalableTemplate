@echo off
REM Sets environment variables from env\.env for the current session

REM Resolve the script directory to handle relative paths
setlocal enabledelayedexpansion
set "SCRIPT_DIR=%~dp0"
set "ENV_FILE=%SCRIPT_DIR%..\env\.env"

if not exist "%ENV_FILE%" (
    echo [ERROR] .env file not found at "%ENV_FILE%"
    exit /b 1
)

for /f "usebackq tokens=* delims=" %%A in ("%ENV_FILE%") do (
    set "LINE=%%A"
    REM Skip empty lines and lines starting with #
    if not "!LINE!"=="" if not "!LINE:~0,1!"=="#" (
        for /f "tokens=1,* delims==" %%B in ("!LINE!") do (
            set "VAR=%%B"
            set "VAL=%%C"
            REM Remove surrounding quotes if present
            if defined VAL (
                if "!VAL:~0,1!"=="\"" set "VAL=!VAL:~1,-1!"
                if "!VAL:~0,1!"=="'" set "VAL=!VAL:~1,-1!"
            )
            setx "!VAR!" "!VAL!" >nul
            set "!VAR!=!VAL!"
        )
    )
)

echo [INFO] Environment variables set for the user profile.
endlocal