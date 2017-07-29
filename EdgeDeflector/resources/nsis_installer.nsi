; Installer for EdgeDeflector

;--------------------------------

!define PRODUCT "EdgeDeflector"

Name "${PRODUCT} Installer"

OutFile "..\bin\Release\${PRODUCT}_install.exe"

; Default installation directory
InstallDir $PROGRAMFILES\${PRODUCT}

; Store install dir in the registry
InstallDirRegKey HKLM "Software\${PRODUCT}" "Install_Dir"

; Request application privileges for UAC
RequestExecutionLevel admin


;--------------------------------

; Installer pages
Page directory
Page instfiles

; Uninstaller pages
UninstPage uninstConfirm
UninstPage instfiles

;--------------------------------


Section "Installer"

  ; Set output path to the installation directory.
  SetOutPath $INSTDIR

  ; Put file there
  File ..\bin\Release\EdgeDeflector.exe

  ; Self-registeres to the registry
  ExecWait '$INSTDIR\${PRODUCT}.exe'

  ; Write the installation path to the registry
  WriteRegStr HKLM SOFTWARE\${PRODUCT} "Install_Dir" "$INSTDIR"
  
  ; Write the uninstall keys to the registry
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT}" "DisplayName" "${PRODUCT}"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT}" "UninstallString" '"$INSTDIR\${PRODUCT}_uninstall.exe"'
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT}" "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT}" "NoRepair" 1
  WriteUninstaller "${PRODUCT}_uninstall.exe"

  ExecShell "open" "microsoft-edge:https://github.com/da2x/EdgeDeflector/wiki/Thank-you-for-installing-EdgeDeflector"

SectionEnd

;--------------------------------


Section "Uninstall"

  ; Remove registry keys
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT}"
  DeleteRegKey HKLM SOFTWARE\${PRODUCT}

  ; Remove files and uninstaller
  Delete $INSTDIR\${PRODUCT}.exe
  Delete $INSTDIR\${PRODUCT}_uninstall.exe

SectionEnd
