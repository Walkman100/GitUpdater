@echo off

rem %1 is folder/repo, %2 is git command (like "pull" or "push")
rem %3 is True if it should repeat until success, %4 is True if it should not close when done

echo 1: %1 2: %2 3: %3 4: %4

cd %1

:start
git %2

IF ERRORLEVEL==1 (
    IF %3==True goto start
)

IF %4==True (
    Pause
)
exit