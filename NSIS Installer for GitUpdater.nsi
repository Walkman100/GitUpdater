; GitUpdater Installer NSIS Script
; get NSIS at http://nsis.sourceforge.net/Download
; As a program that all Power PC users should have, Notepad++ is recommended to edit this file

AddBrandingImage top 20
Icon "My Project\GitUpdater.ico"
Caption "GitUpdater Installer"
Name "GitUpdater"
AutoCloseWindow true
ShowInstDetails show

LicenseBkColor /windows
LicenseData "LICENSE.md"
LicenseForceSelection checkbox "I have read and understand this notice"
LicenseText "Please read the notice below before installing GitUpdater. If you understand the notice, click the checkbox below and click Next."

InstallDir $PROGRAMFILES\DeavmiOSS

OutFile "bin\Release\GitUpdater-Installer.exe"

; Pages

Page license
Page components
Page directory
Page instfiles
UninstPage uninstConfirm
UninstPage instfiles

; Sections

Section "GitUpdater Executable & Uninstaller"
  SectionIn RO
  SetOutPath $INSTDIR
  File "bin\Release\GitUpdater.exe"
  File "bin\Release\GitUpdater.bat"
  File "bin\Release\OpenRepoInBash.bat"
  WriteUninstaller "GitUpdater-Uninst.exe"
SectionEnd

Section "PowerShell files"
  File "bin\Release\OpenRepoInPS.bat"
  File /r "bin\Release\PS"
SectionEnd

Section "GitUpdater Start Menu Shortcuts"
  CreateDirectory "$SMPROGRAMS\DeavmiOSS"
  CreateShortCut "$SMPROGRAMS\DeavmiOSS\GitUpdater.lnk" "$INSTDIR\GitUpdater.exe" "" "" "" "" "" "Start GitUpdater with default options"
  CreateShortCut "$SMPROGRAMS\DeavmiOSS\Uninstall GitUpdater.lnk" "$INSTDIR\GitUpdater-Uninst.exe" "" "" "" "" "" "Uninstall GitUpdater"
  ;Syntax for CreateShortCut: link.lnk target.file [parameters [icon.file [icon_index_number [start_options [keyboard_shortcut [description]]]]]]
SectionEnd

Section "GitUpdater Desktop Shortcut"
  CreateShortCut "$DESKTOP\GitUpdater.lnk" "$INSTDIR\GitUpdater.exe" "" "" "" "" "" "Start GitUpdater with default options"
SectionEnd

Section "GitUpdater Quick Launch Shortcut"
  CreateShortCut "$QUICKLAUNCH\GitUpdater.lnk" "$INSTDIR\GitUpdater.exe" "" "" "" "" "" "Start GitUpdater with default options"
SectionEnd

Section "Example Shortcuts in Start Menu"
  CreateShortCut "$SMPROGRAMS\DeavmiOSS\GitUpdater (Pull All & Exit).lnk" "$INSTDIR\GitUpdater.exe" "-gitcmd=pull -gitwhat=all run exitWhenDone" "" "" "" "" "Start GitUpdater and pull all repos, then exit"
  CreateShortCut "$SMPROGRAMS\DeavmiOSS\GitUpdater (Push All).lnk" "$INSTDIR\GitUpdater.exe" "-gitcmd=push -gitwhat=all run" "" "" "" "" "Start GitUpdater and push all repos"
  CreateShortCut "$SMPROGRAMS\DeavmiOSS\GitUpdater (Pull GitUpdater Repo).lnk" "$INSTDIR\GitUpdater.exe" "-gitcmd=pull -gitwhat=cmdselected -repo=GitUpdater run" "" "" "" "" "Start GitUpdater and pull the GitUpdater repo"
  CreateShortCut "$SMPROGRAMS\DeavmiOSS\GitUpdater (Push GitUpdater Repo & Exit).lnk" "$INSTDIR\GitUpdater.exe" "-gitcmd=push -gitwhat=cmdselected -repo=GitUpdater run exitWhenDone" "" "" "" "" "Start GitUpdater, push the GitUpdater repo, then exit"
  CreateShortCut "$SMPROGRAMS\DeavmiOSS\GitUpdater (Start in Users directory).lnk" "$INSTDIR\GitUpdater.exe" "-dir=C:\Users" "" "" "" "" "Start GitUpdater in Users directory"
SectionEnd

;Section "More apps from DeavmiOSS"
; this should have sub options for available apps, that are downloaded
;SectionEnd

; Uninstaller

Section "Uninstall"
  Delete "$INSTDIR\GitUpdater-Uninst.exe"   ; Remove Application Files
  Delete "$INSTDIR\GitUpdater.exe"
  RMDir $INSTDIR
  
  Delete "$SMPROGRAMS\DeavmiOSS\GitUpdater.lnk"   ; Remove Start Menu Shortcuts & Folder
  Delete "$SMPROGRAMS\DeavmiOSS\Uninstall GitUpdater.lnk"
  RMDir $SMPROGRAMS\DeavmiOSS
  
  Delete "$DESKTOP\GitUpdater.lnk"   ; Remove Desktop Shortcut
  Delete "$QUICKLAUNCH\GitUpdater.lnk"   ; Remove Quick Launch shortcut
SectionEnd

; Functions

Function .onInit
  MessageBox MB_YESNO "This will install GitUpdater. Do you wish to continue?" IDYES gogogo
    Abort
  gogogo:
  SetBrandingImage "[/RESIZETOFIT] 'My Project\git.ico'"
  SetShellVarContext all
  SetAutoClose true
FunctionEnd

Function .onInstSuccess
    MessageBox MB_YESNO "Install Succeeded! Open ReadMe?" IDNO NoReadme
      ExecShell "open" "https://github.com/Walkman100/GitUpdater/blob/master/README.md#gitupdater-"
    NoReadme:
FunctionEnd

; Uninstaller

Function un.onInit
    MessageBox MB_YESNO "This will uninstall GitUpdater. Continue?" IDYES NoAbort
      Abort ; causes uninstaller to quit.
    NoAbort:
    SetShellVarContext all
    SetAutoClose true
FunctionEnd

Function un.onUninstFailed
    MessageBox MB_OK "Uninstall Cancelled"
FunctionEnd

Function un.onUninstSuccess
    MessageBox MB_OK "Uninstall Completed"
FunctionEnd
