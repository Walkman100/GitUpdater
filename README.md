# GitUpdater [![Build status](https://ci.appveyor.com/api/projects/status/72cvetqe8awtp2fn)](https://ci.appveyor.com/project/Walkman100/gitupdater)

A simple application written in VB.NET to update all your repos with one click, or one repeatedly until it works.

# Command Line

The syntax to launch GitUpdater from the command line is:
```shell
gitupdater.exe [hideGUI] [exitWhenDone] [-gitcmd=<push|pull|(any git command)>] [-gitwhat=<all|selected|notselected|cmdselected|cmdnotselected>] [-dir=<repos parent folder>] [-repo=<repo name>] [run]
```
some examples are:

`gitupdater.exe -gitcmd=push -gitwhat=cmdselected -repo=GitUpdater run exitWhenDone`

`gitupdater.exe -gitcmd=pull -gitwhat=cmdnotselected -repo=YTVL run`

`gitupdater.exe hideGUI -gitcmd=pull -gitwhat=all run exitWhenDone`

Notes:

- If you use either of the `selected` or `notselected` options, it will say that no item has been specified, since those methods use the selection in the GUI. Please use the <i><b>cmd</b>selected</i> and <i><b>cmd</b>notselected</i> methods.
- The way this has been programmed allows you to put multiple commands after each other:
`gitupdater.exe -gitcmd=push -gitwhat=cmdselected -repo=GitUpdater run -gitcmd=pull -gitwhat=cmdselected -repo=YTVL run`
- Any git command can be used in place of `push|pull`, the flags can be put in any order (but it is recommended to use the specified order), and anything that doesn't begin with one of the predefined flags will be ignored. This allows for a command like this:
`gitupdater.exe -repo=GitUpdater -gitcmd=show ouiocuiygcrdackdacrdi -gitwhat=cmdselected run`
This will execute the `git show` command in the `GitUpdater` repo.
- The `exitWhenDone` flag is only used when the program performs a git operation, so if you use for example `gitupdater.exe exitWhenDone` it will still open the GUI, but after any Git operation it will close.

**Please note that the `run` flag is necessary to run the program, and if it is put before any other parameters they will not be used** (except the `exitWhenDone` and `hideGUI` flags, they can be put anywhere).
- You can launch GitUpdater in a specific repo parent folder:

`gitupdater.exe -dir="C:\Users\Matthew\GitHub"`

If your repos are located in that folder.

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

9: In the 'Add arguments (optional):' box add the arguments you want, e.g. `-gitcmd=pull -gitwhat=all run exitWhenDone` or `-gitcmd=push -gitwhat=cmdselected -repo=GitUpdater run exitWhenDone`

![Set the Arguments](http://walkman100.github.io/Walkman/Images/WindowsProjectsScreenshots/GitUpdater/WinTaskSchdArguments.png "Set the Arguments")

10: Click 'OK', 'OK' again, then close the Task Scheduler window.

![Check the line looks correct](http://walkman100.github.io/Walkman/Images/WindowsProjectsScreenshots/GitUpdater/WinTaskSchdDone.png "Check the line looks correct")

## Donate
[Show your support!](http://walkman100.github.io/Walkman/HTML/Donate.html)
