@echo off

rem %1 is folder/repo, %2 is git command (like "pull" or "push")
rem %3 is True if it should repeat until success, %4 is True if it should not close when done
rem %5 is True if it should log output, %6 is where to log output

rem Remove "rem" from lines below to show all parameters when bat file launches
rem echo Parameters: "%*"
rem echo 1: %1 2: %2 3: %3 4: %4 5: %5 6: %6

rem Setting GUI elements and going to the Repo directory
color 0A
title Git %~2ing repo at location "%~1" [GitUpdater]
echo Git %~2ing repo at location "%~1"...
cd /d %~1
:start

rem Check if Logging must be enabled
if %~5==True goto log

rem -----------------------------------------------------
rem                 Code without logging
rem -----------------------------------------------------

rem Run the Git command
git %~2

rem If it doesn't fail, go to the end of no logging code
if Not ERRORLEVEL==1 goto end

rem If it should retry if it fails go to start
if %~3==True (
    echo.
    echo Failed to %~2 repo at "%~1", trying again...
    timeout 10 /NOBREAK
    goto start
)

:end

rem If window mustn't be closed when done, pause
if %~4==True (
    echo.
    echo Press enter to close this window. Unless you specified the "Don't wait for cmd to close before starting next" option, further git commands will not start until you close this window.
    pause
) else (
    exit
)

rem -----------------------------------------------------
rem                   Code with logging
rem -----------------------------------------------------

:log

rem Run the Git command
@echo [%date% %time%] Git %~2ing repo at location "%~1" >> %~6
git %~2 >> %~6

rem If it doesn't fail, go to the end of code with logging
if Not ERRORLEVEL==1 goto logend

rem If it should retry if it fails go to logging start
if %~3==True (
    @echo [%date% %time%] Failed to %~2 repo at "%~1", trying again... >> %~6
    echo.
    echo Failed to %~2 repo at "%~1", trying again...
    timeout 10 /NOBREAK
    goto log
)
@echo [%date% %time%] Failed to %~2 repo at "%~1", retry disabled. >> %~6

:logend

rem If window mustn't be closed when done, pause
if %~4==True (
    @echo [%date% %time%] Git %~2ing repo at location "%~1" complete. Don't close CMD window when done was enabled. >> %~6
    @echo [%date% %time%] Unless you specified the "Don't wait for cmd to close before starting next" option, further git commands will not start until the CMD window is closed. >> %~6
    @echo [%date% %time%] Waiting for user intervention... >> %~6
    echo.
    echo Press enter to close this window. Unless you specified the "Don't wait for cmd to close before starting next" option, further git commands will not start until you press enter to close this window.
    pause
    @echo [%date% %time%] Received user intervention. >> %~6
    @echo. >> %~6
) else (
@echo [%date% %time%] Git %~2ing repo at location "%~1" complete. >> %~6
@echo. >> %~6
exit
)