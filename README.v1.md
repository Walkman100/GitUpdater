# GitUpdater [![Build status](https://ci.appveyor.com/api/projects/status/72cvetqe8awtp2fn)](https://ci.appveyor.com/project/Walkman100/gitupdater)
A simple application written in VB.NET to update all your repos with one click, or one repeatedly until it works.

[![Gitter](https://badges.gitter.im/Join Chat.svg)](https://gitter.im/Walkman100/Walkman?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

## Compile requirements
See [CompileInstructions.md](https://github.com/Walkman100/WinCompile/blob/master/CompileInstructions.md)

# Command Line
The syntax to launch GitUpdater from the command line is:
```shell
gitupdater.exe [<dir|-dir|/dir|\dir> [parent dir ]<repo> ][<push|pull|(any git command)> [* ]<all|selected|notselected|cmdselected|cmdnotselected>]
```
some examples are:

```shell
gitupdater.exe -dir GitUpdater push cmdselected
gitupdater.exe -dir YTVL pull cmdnotselected
gitupdater.exe pull all
```

Notes:

- If you use either of the `selected` or `notselected` options, it will say that no item has been specified, since those methods use the selection in the GUI. Please use the <i><b>cmd</b>selected</i> and <i><b>cmd</b>notselected</i> methods.
- The way this has been programmed allows you to put multiple commands after each other:
```shell
gitupdater.exe -dir GitUpdater push cmdselected -dir YTVL pull cmdselected
```
- Any git command can be used in place of `push|pull`, and anything after that and before the method (e.g. `cmdselected`) will be ignored. This allows for a command like this:

```shell
gitupdater.exe -dir GitUpdater show ouiocuiygcrdackdacrdi cmdselected
```

This will execute the `git show` command in the `GitUpdater` repo.

# Perform Git commands at a scheduled time
To do this you use an external program to launch GitUpdater with command line args (please see [above](#command-line) for an explanation of them).

An external program that you can use is Windows itself, using the Windows Task Scheduler:

1: Press <kbd>⊞ Win</kbd> & <kbd>R</kbd>

![Run Dialogue](http://walkman100.github.io/Walkman/Images/WindowsProjectsScreenshots/GitUpdater/WinTaskSchdRun.png "Run Dialogue")

2: Type `taskschd.msc` or copy and paste, then press enter

3: In the window that opens, in the pane on the left click on 'Task Scheduler Library' or expand it and click on the folder where you want to make the task.

![Left Pane](http://walkman100.github.io/Walkman/Images/WindowsProjectsScreenshots/GitUpdater/WinTaskSchdLeftPane.png "Left Pane")

4: Click on the option in the pane on the right that reads 'Create Task...'

![Right Pane](http://walkman100.github.io/Walkman/Images/WindowsProjectsScreenshots/GitUpdater/WinTaskSchdRightPane.png "Right Pane")

5: Give the task any name you want. Click on the tab 'Triggers', then the 'New...' button.

![Set a name](http://walkman100.github.io/Walkman/Images/WindowsProjectsScreenshots/GitUpdater/WinTaskSchdName.png "Set a name")
![Click Triggers then New](http://walkman100.github.io/Walkman/Images/WindowsProjectsScreenshots/GitUpdater/WinTaskSchdTriggersNew.png "Click Triggers then New")

6: Set when you want the task to start.

![Set when you want the task to start](http://walkman100.github.io/Walkman/Images/WindowsProjectsScreenshots/GitUpdater/WinTaskSchdTaskStart.png "Set when you want the task to start")

7: Click 'OK', then go to the 'Actions' tab and click 'New...'

![Click Actions then New](http://walkman100.github.io/Walkman/Images/WindowsProjectsScreenshots/GitUpdater/WinTaskSchdActionsNew.png "Click Actions then New")

8: Click 'Browse...' and locate the GitUpdater executable.

![Locate GitUpdater](http://walkman100.github.io/Walkman/Images/WindowsProjectsScreenshots/GitUpdater/WinTaskSchdLocateGitUpdater.png "Locate GitUpdater")

9: In the 'Add arguments (optional):' box add the arguments you want.

![Set the Arguments](http://walkman100.github.io/Walkman/Images/WindowsProjectsScreenshots/GitUpdater/WinTaskSchdArguments.png "Set the Arguments")

10: Click 'OK', 'OK' again, then close the Task Scheduler window.

![Check the line looks correct](http://walkman100.github.io/Walkman/Images/WindowsProjectsScreenshots/GitUpdater/WinTaskSchdDone.png "Check the line looks correct")

## Donate
[Show your support!](http://walkman100.github.io/Walkman/HTML/Donate.html)


[![Bitdeli Badge](https://d2weczhvl823v0.cloudfront.net/Walkman100/gitupdater/trend.png)](https://bitdeli.com/free "Bitdeli Badge")

