@echo off

rem %1 is folder/repo, %2 is git command (like "pull" or "push"), %5 is commands after that (like -f)
rem %3 is True if it should repeat until success, %4 is True if it should not close when done

rem Remove "rem" from lines below to show all parameters when bat file launches
rem echo Parameters: "%*"
rem echo 1: %1 2: %2 3: %3 4: %4 5: %5

echo Git %2ing repo at location "%1"

cd %1

:Start
git %2 %5

IF ERRORLEVEL==1 (
    goto Repeat
) ELSE (
    goto End
)

:Repeat
IF %3==True (
    echo.
    echo Failed to %2 repo at "%1", trying again...
    goto Start
)

:End
IF %4==True (
    echo.
    echo Press enter to close this window. Unless you specified the "Don't wait for cmd to close before starting next" option, further git commands will not start entil you close this window.
    Pause
)
exit