GitUpdater
==========

A simple application written in VB.NET to update all your repos with one click, or one repeatedly until it works.

Command Line
============

The syntax to launch GitUpdater from the command line is:
```shell
gitupdater.exe [<dir|-dir|/dir|\dir> [parent dir ]<repo> ][<push|pull|(any git command)> [* ]<all|selected|notselected|cmdselected|cmdnotselected>]
```
some examples are:

`gitupdater.exe -dir GitUpdater push cmdselected`

`gitupdater.exe -dir YTVL pull cmdnotselected`

`gitupdater.exe pull all`

Note that if you use either of the `selected` or `notselected` options, it will say that no item has been specified, since those methods use the selection in the GUI. Please use the <i><b>cmd</b>selected</i> and <i><b>cmd</b>notselected</i> methods.

Note also that the way this has been programmed allows you to put multiple commands after each other:
`gitupdater.exe -dir GitUpdater push cmdselected -dir YTVL pull cmdselected`

Also note that any git command can be used in place of `push|pull`, and anything after that and before the method (e.g. `cmdselected`) will be ignored. This allows for a command like this:

`gitupdater.exe -dir GitUpdater show ouiocuiygcrdackdacrdi cmdselected`

This will execute the `git show` command in the `GitUpdater` repo.

Perform Git commands at a scheduled time
========================================

To do this you use an external program to launch GitUpdater with command line args (please see above for an explanation of them).

An external program that you can use is Windows itself, using the Windows Task Scheduler:

1. Press <kbd>Windows Key</kbd> & <kbd>R</kbd>

2. Type `taskschd.msc` or copy and paste, then press enter

3. In the window that opens, in the pane on the left click on 'Task Scheduler Library' or expand it and click on the folder where you want to make the task.

4. Click on the option in the pane on the right that reads 'Create Task...'

5. Give the task any name you want. Click on the tab 'Triggers', then the 'New...' button.

6. Set when you want the task to start.

7. Click 'OK', then go to the 'Actions' tab and click 'New...'

8. Click 'Browse...' and locate the GitUpdater executable.

9. In the 'Add arguments (optional):' box add the arguments you want, e.g. `pull all` or `-dir GitUpdater push cmdselected`

10. Click 'OK', 'OK' again, then close the Task Scheduler window.

Screenshots coming soon!
