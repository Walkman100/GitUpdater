; GitUpdater Installer NSIS Script
; get NSIS at http://nsis.sourceforge.net/Download

!define ProgramName "GitUpdater"
Icon "My Project\${ProgramName}.ico"

Name "${ProgramName}"
Caption "${ProgramName} Installer"
XPStyle on
ShowInstDetails show
AutoCloseWindow true

LicenseBkColor /windows
LicenseData "LICENSE.md"
LicenseForceSelection checkbox "I have read and understand this notice"
LicenseText "Please read the notice below before installing ${ProgramName}. If you understand the notice, click the checkbox below and click Next."

InstallDir $PROGRAMFILES\WalkmanOSS
InstallDirRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${ProgramName}" "InstallLocation"
OutFile "bin\Release\${ProgramName}-Installer.exe"

; Pages

Page license
Page components
Page directory
Page instfiles
Page custom postInstallShow postInstallFinish ": Install Complete"
UninstPage uninstConfirm
UninstPage instfiles

; Sections

Section "Executable & Uninstaller"
  SectionIn RO
  SetOutPath $INSTDIR
  File "bin\Release\${ProgramName}.exe"
  File "bin\Release\${ProgramName}.bat"
  File "bin\Release\OpenRepoInBash.bat"
  WriteUninstaller "${ProgramName}-Uninst.exe"
SectionEnd

Section "Add to Windows Programs & Features"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${ProgramName}" "DisplayName" "${ProgramName}"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${ProgramName}" "Publisher" "WalkmanOSS"
  
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${ProgramName}" "DisplayIcon" "$INSTDIR\${ProgramName}.exe"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${ProgramName}" "InstallLocation" "$INSTDIR\"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${ProgramName}" "UninstallString" "$INSTDIR\${ProgramName}-Uninst.exe"
  
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${ProgramName}" "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${ProgramName}" "NoRepair" 1
  
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${ProgramName}" "HelpLink" "https://github.com/Walkman100/${ProgramName}/issues/new"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${ProgramName}" "URLInfoAbout" "https://github.com/Walkman100/${ProgramName}" ; Support Link
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${ProgramName}" "URLUpdateInfo" "https://github.com/Walkman100/${ProgramName}/releases" ; Update Info Link
SectionEnd

Section "PowerShell files"
  File "bin\Release\OpenRepoInPS.bat"
  File /r "bin\Release\PS"
SectionEnd

Section "Remove old files in DeavmiOSS"
  Delete "$PROGRAMFILES\DeavmiOSS\${ProgramName}-Uninst.exe"
  Delete "$PROGRAMFILES\DeavmiOSS\${ProgramName}.exe"
  RMDir "$PROGRAMFILES\DeavmiOSS"
  
  Delete "$SMPROGRAMS\DeavmiOSS\${ProgramName}.lnk"
  Delete "$SMPROGRAMS\DeavmiOSS\Uninstall ${ProgramName}.lnk"
  RMDir "$SMPROGRAMS\DeavmiOSS"

  Delete "$SMPROGRAMS\DeavmiOSS\${ProgramName} (Pull All & Exit).lnk"
  Delete "$SMPROGRAMS\DeavmiOSS\${ProgramName} (Push All).lnk"
  Delete "$SMPROGRAMS\DeavmiOSS\${ProgramName} (Pull ${ProgramName} Repo).lnk"
  Delete "$SMPROGRAMS\DeavmiOSS\${ProgramName} (Push ${ProgramName} Repo & Exit).lnk"
  Delete "$SMPROGRAMS\DeavmiOSS\${ProgramName} (Start in Users directory).lnk"
SectionEnd

Section "Start Menu Shortcuts"
  CreateDirectory "$SMPROGRAMS\WalkmanOSS"
  CreateShortCut "$SMPROGRAMS\WalkmanOSS\${ProgramName}.lnk" "$INSTDIR\${ProgramName}.exe" "" "" "" "" "" "Start ${ProgramName} with default options"
  CreateShortCut "$SMPROGRAMS\WalkmanOSS\Uninstall ${ProgramName}.lnk" "$INSTDIR\${ProgramName}-Uninst.exe" "" "" "" "" "" "Uninstall ${ProgramName}"
  ;Syntax for CreateShortCut: link.lnk target.file [parameters [icon.file [icon_index_number [start_options [keyboard_shortcut [description]]]]]]
SectionEnd

Section "Desktop Shortcut"
  CreateShortCut "$DESKTOP\${ProgramName}.lnk" "$INSTDIR\${ProgramName}.exe" "" "" "" "" "" "Start ${ProgramName} with default options"
SectionEnd

Section "Quick Launch Shortcut"
  CreateShortCut "$QUICKLAUNCH\${ProgramName}.lnk" "$INSTDIR\${ProgramName}.exe" "" "" "" "" "" "Start ${ProgramName} with default options"
SectionEnd

Section "Example Shortcuts in Start Menu"
  CreateShortCut "$SMPROGRAMS\WalkmanOSS\${ProgramName} (Pull All & Exit).lnk" "$INSTDIR\${ProgramName}.exe" "-gitcmd=pull -gitwhat=all run exitWhenDone" "" "" "" "" "Start ${ProgramName} and pull all repos, then exit"
  CreateShortCut "$SMPROGRAMS\WalkmanOSS\${ProgramName} (Push All).lnk" "$INSTDIR\${ProgramName}.exe" "-gitcmd=push -gitwhat=all run" "" "" "" "" "Start ${ProgramName} and push all repos"
  CreateShortCut "$SMPROGRAMS\WalkmanOSS\${ProgramName} (Pull ${ProgramName} Repo).lnk" "$INSTDIR\${ProgramName}.exe" "-gitcmd=pull -gitwhat=cmdselected -repo=${ProgramName} run" "" "" "" "" "Start ${ProgramName} and pull the ${ProgramName} repo"
  CreateShortCut "$SMPROGRAMS\WalkmanOSS\${ProgramName} (Push ${ProgramName} Repo & Exit).lnk" "$INSTDIR\${ProgramName}.exe" "-gitcmd=push -gitwhat=cmdselected -repo=${ProgramName} run exitWhenDone" "" "" "" "" "Start ${ProgramName}, push the ${ProgramName} repo, then exit"
  CreateShortCut "$SMPROGRAMS\WalkmanOSS\${ProgramName} (Start in Users directory).lnk" "$INSTDIR\${ProgramName}.exe" "-dir=C:\Users" "" "" "" "" "Start ${ProgramName} in Users directory"
SectionEnd

; Functions

Function .onInit
  SetShellVarContext all
  SetAutoClose true
FunctionEnd

; Custom Install Complete page

!include nsDialogs.nsh
!include LogicLib.nsh ; For ${IF} logic
Var Dialog
Var Label
Var CheckboxReadme
Var CheckboxReadme_State
Var CheckboxRunProgram
Var CheckboxRunProgram_State

Function postInstallShow
  nsDialogs::Create 1018
  Pop $Dialog
  ${If} $Dialog == error
    Abort
  ${EndIf}
  
  ${NSD_CreateLabel} 0 0 100% 12u "Setup will launch these tasks when you click close:"
  Pop $Label
  
  ${NSD_CreateCheckbox} 10u 30u 100% 10u "&Open Readme"
  Pop $CheckboxReadme
  ${If} $CheckboxReadme_State == ${BST_CHECKED}
    ${NSD_Check} $CheckboxReadme
  ${EndIf}
  
  ${NSD_CreateCheckbox} 10u 50u 100% 10u "&Launch ${ProgramName}"
  Pop $CheckboxRunProgram
  ${If} $CheckboxRunProgram_State == ${BST_CHECKED}
    ${NSD_Check} $CheckboxRunProgram
  ${EndIf}
  
  # alternative for the above ${If}:
  #${NSD_SetState} $Checkbox_State
  nsDialogs::Show
FunctionEnd

Function postInstallFinish
  ${NSD_GetState} $CheckboxReadme $CheckboxReadme_State
  ${NSD_GetState} $CheckboxRunProgram $CheckboxRunProgram_State
  
  ${If} $CheckboxReadme_State == ${BST_CHECKED}
    ExecShell "open" "https://github.com/Walkman100/${ProgramName}/blob/master/README.md#gitupdater-"
  ${EndIf}
  ${If} $CheckboxRunProgram_State == ${BST_CHECKED}
    ExecShell "open" "$INSTDIR\${ProgramName}.exe"
  ${EndIf}
FunctionEnd

; Uninstaller

Section "Uninstall"
  Delete "$INSTDIR\${ProgramName}-Uninst.exe"   ; Remove Application Files
  Delete "$INSTDIR\${ProgramName}.exe"
  RMDir "$INSTDIR"
  
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${ProgramName}" ; Remove Windows Programs & Features integration (uninstall info)
  
  Delete "$SMPROGRAMS\WalkmanOSS\${ProgramName}.lnk"   ; Remove Start Menu Shortcuts & Folder
  Delete "$SMPROGRAMS\WalkmanOSS\Uninstall ${ProgramName}.lnk"
  
  Delete "$SMPROGRAMS\WalkmanOSS\${ProgramName} (Pull All & Exit).lnk"
  Delete "$SMPROGRAMS\WalkmanOSS\${ProgramName} (Push All).lnk"
  Delete "$SMPROGRAMS\WalkmanOSS\${ProgramName} (Pull ${ProgramName} Repo).lnk"
  Delete "$SMPROGRAMS\WalkmanOSS\${ProgramName} (Push ${ProgramName} Repo & Exit).lnk"
  Delete "$SMPROGRAMS\WalkmanOSS\${ProgramName} (Start in Users directory).lnk"
  RMDir "$SMPROGRAMS\WalkmanOSS"
  
  Delete "$DESKTOP\${ProgramName}.lnk"   ; Remove Desktop Shortcut
  Delete "$QUICKLAUNCH\${ProgramName}.lnk"   ; Remove Quick Launch shortcut
  
  ; Remove old files in DeavmiOSS
  Delete "$PROGRAMFILES\DeavmiOSS\${ProgramName}-Uninst.exe"
  Delete "$PROGRAMFILES\DeavmiOSS\${ProgramName}.exe"
  RMDir "$PROGRAMFILES\DeavmiOSS"
  
  Delete "$SMPROGRAMS\DeavmiOSS\${ProgramName}.lnk"
  Delete "$SMPROGRAMS\DeavmiOSS\Uninstall ${ProgramName}.lnk"
  
  Delete "$SMPROGRAMS\DeavmiOSS\${ProgramName} (Pull All & Exit).lnk"
  Delete "$SMPROGRAMS\DeavmiOSS\${ProgramName} (Push All).lnk"
  Delete "$SMPROGRAMS\DeavmiOSS\${ProgramName} (Pull ${ProgramName} Repo).lnk"
  Delete "$SMPROGRAMS\DeavmiOSS\${ProgramName} (Push ${ProgramName} Repo & Exit).lnk"
  Delete "$SMPROGRAMS\DeavmiOSS\${ProgramName} (Start in Users directory).lnk"
  RMDir "$SMPROGRAMS\DeavmiOSS"
SectionEnd

; Uninstaller Functions

Function un.onInit
  SetShellVarContext all
  SetAutoClose true
FunctionEnd

Function un.onUninstFailed
  MessageBox MB_OK "Uninstall Cancelled"
FunctionEnd

Function un.onUninstSuccess
  MessageBox MB_OK "Uninstall Completed"
FunctionEnd
