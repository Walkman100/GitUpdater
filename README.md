# GitUpdater [![Build status](https://ci.appveyor.com/api/projects/status/72cvetqe8awtp2fn)](https://ci.appveyor.com/project/Walkman100/gitupdater)
A simple application written in VB.Net to update all your repos with one click, or one repeatedly until it works.

[![Gitter](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/Walkman100/Walkman?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

## Compile requirements
See [CompileInstructions.md](https://github.com/Walkman100/gists/blob/master/CompileInstructions.md)

## Download
Get the latest version [here](https://github.com/Walkman100/GitUpdater/releases), and the latest build from commit
[here](https://ci.appveyor.com/project/Walkman100/GitUpdater/build/artifacts)
(note that these builds are built for the Debug config and so are not optimised)

# Command Line
The syntax to launch GitUpdater from the command line is:
```cmd
gitupdater.exe [hideGUI] [exitWhenDone] [-gitcmd=<push|pull|(any git command)>] [-gitwhat=<all|selected|notselected|cmdselected|cmdnotselected>] [-dir=<repos parent folder>] [-repo=<repo name>] [run]
```
some examples are:

```cmd
gitupdater.exe -gitcmd=push -gitwhat=cmdselected -repo=GitUpdater run exitWhenDone
gitupdater.exe -gitcmd=pull -gitwhat=cmdnotselected -repo=YTVL run
gitupdater.exe hideGUI -gitcmd=pull -gitwhat=all run exitWhenDone
```

Notes:

- If you use either of the `selected` or `notselected` options, it will say that no item has been specified, since those methods use the selection in the GUI. Please use the <i><b>cmd</b>selected</i> and <i><b>cmd</b>notselected</i> methods.
- The way this has been programmed allows you to put multiple commands after each other:
```cmd
gitupdater.exe -gitcmd=push -gitwhat=cmdselected -repo=GitUpdater run -gitcmd=pull -gitwhat=cmdselected -repo=YTVL run
```
- Any git command can be used in place of `push|pull`, the flags can be put in any order (but it is recommended to use the specified order), and anything that doesn't begin with one of the predefined flags will be ignored. This allows for a command like this:

```cmd
gitupdater.exe -repo=GitUpdater -gitcmd=show ouiocuiygcrdackdacrdi -gitwhat=cmdselected run
```

This will execute the `git show` command in the `GitUpdater` repo.
- The `exitWhenDone` flag is only used when the program performs a git operation, so if you use for example `gitupdater.exe exitWhenDone` it will still open the GUI, but after any Git operation it will close.

**Please note that the `run` flag is necessary to run the program, and if it is put before any other parameters they will not be used** (except the `exitWhenDone` and `hideGUI` flags, they can be put anywhere).
- You can launch GitUpdater in a specific repo parent folder:

```cmd
gitupdater.exe -dir="C:\Users\Matthew\GitHub"
```

If your repos are located in that folder.

# Perform Git commands at a scheduled time
To do this you use an external program to launch GitUpdater with command line args (please see [above](#command-line) for an explanation of them).

An external program that you can use is Windows itself, using the Windows Task Scheduler:

1: Press <kbd>⊞ Win</kbd> & <kbd>R</kbd>

[![Run Dialog][Run Dialog]][Run Dialog]

  [Run Dialog]: http://walkman100.github.io/images/Screenshots/My_Projects/GitUpdater/WinTaskSchdRun.png

2: Type `taskschd.msc` or copy and paste, then press enter

3: In the window that opens, in the pane on the left click on 'Task Scheduler Library' or expand it and click on the folder where you want to make the task.

[![Left Pane][Left Pane]][Left Pane]

  [Left Pane]: http://walkman100.github.io/images/Screenshots/My_Projects/GitUpdater/WinTaskSchdLeftPane.png

4: Click on the option in the pane on the right that reads 'Create Task...'

[![Right Pane][Right Pane]][Right Pane]

  [Right Pane]: http://walkman100.github.io/images/Screenshots/My_Projects/GitUpdater/WinTaskSchdRightPane.png

5: Give the task any name you want. Click on the tab 'Triggers', then the 'New...' button.

[![Set a name][Set a name]][Set a name]

[![Click Triggers then New][New Trigger]][New Trigger]

  [Set a name]: http://walkman100.github.io/images/Screenshots/My_Projects/GitUpdater/WinTaskSchdName.png
  [New Trigger]: http://walkman100.github.io/images/Screenshots/My_Projects/GitUpdater/WinTaskSchdTriggersNew.png

6: Set when you want the task to start.

[![Set when you want the task to start][task start]][task start]

  [task start]: http://walkman100.github.io/images/Screenshots/My_Projects/GitUpdater/WinTaskSchdTaskStart.png

7: Click 'OK', then go to the 'Actions' tab and click 'New...'

[![Click Actions then New][new action]][new action]

  [new action]: http://walkman100.github.io/images/Screenshots/My_Projects/GitUpdater/WinTaskSchdActionsNew.png

8: Click 'Browse...' and locate the GitUpdater executable.

[![Locate GitUpdater][locate exe]][locate exe]

  [locate exe]: http://walkman100.github.io/images/Screenshots/My_Projects/GitUpdater/WinTaskSchdLocateGitUpdater.png

9: In the 'Add arguments (optional):' box add the arguments you want, e.g.

```cmd
-gitcmd=pull -gitwhat=all run exitWhenDone
-gitcmd=push -gitwhat=cmdselected -repo=GitUpdater run exitWhenDone
```

[![Set the Arguments][arguments]][arguments]

  [arguments]: http://walkman100.github.io/images/Screenshots/My_Projects/GitUpdater/WinTaskSchdArguments.png

10: Click 'OK', 'OK' again, then close the Task Scheduler window.

[![Check the line looks correct][check]][check]

  [check]: http://walkman100.github.io/images/Screenshots/My_Projects/GitUpdater/WinTaskSchdDone.png

## Donate
[Show your support!](http://walkman100.github.io/donate)
