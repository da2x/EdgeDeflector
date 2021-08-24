Unicode True

RequestExecutionLevel user

; Installer for EdgeDeflector

BrandingText " "

!define PRODUCT "EdgeDeflector"
!define DESCRIPTION "Open MICROSOFT-EDGE:// links in your default web browser."

!getdllversion "..\bin\Release\${PRODUCT}.exe" VERSION_

VIAddVersionKey "ProductName" "${PRODUCT} Installer"
VIAddVersionKey "FileVersion" "${VERSION_1}.${VERSION_2}.${VERSION_3}.${VERSION_4}"
VIAddVersionKey "FileDescription" "Install ${PRODUCT} ${VERSION_1}.${VERSION_2}.${VERSION_3}.${VERSION_4}."
VIAddVersionKey "LegalCopyright" "Copyright © 2021"

VIFileVersion "${VERSION_1}.${VERSION_2}.${VERSION_3}.${VERSION_4}"
VIProductVersion "${VERSION_1}.${VERSION_2}.${VERSION_3}.${VERSION_4}"

Name "${PRODUCT} Installer"

OutFile "..\bin\Release\${PRODUCT}_install.exe"

; Default installation directory
InstallDir $LOCALAPPDATA\Programs\${PRODUCT}

; Store install dir in the registry
InstallDirRegKey HKCU "SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\${PRODUCT}.exe" "Path"


; Installer pages
Page directory
Page instfiles

; Uninstaller pages
UninstPage uninstConfirm
UninstPage instfiles

Section "Installer"
  SetAutoClose true
  AddSize 8

  ; Set output path to the installation directory.
  SetOutPath $INSTDIR

  ; Install the program
  File "..\bin\Release\${PRODUCT}.exe"

  ; Path registration
  WriteRegStr HKCU "SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\${PRODUCT}.exe" "" "$INSTDIR\${PRODUCT}.exe"
  WriteRegStr HKCU "SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\${PRODUCT}.exe" "Path" "$INSTDIR"
  WriteRegStr HKCU "SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\${PRODUCT}.exe" "SupportedProtocols" "microsoft-edge"
  WriteRegDWORD HKCU "SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\${PRODUCT}.exe" "useURL" 1

  ; Register protocol
  WriteRegStr HKCU "SOFTWARE\Classes\${PRODUCT}.microsoft-edge" "" "URL: MICROSOFT-EDGE"
  WriteRegStr HKCU "SOFTWARE\Classes\${PRODUCT}.microsoft-edge" "URL Protocol" ""
  WriteRegStr HKCU "SOFTWARE\Classes\${PRODUCT}.microsoft-edge\shell\open\command" "" "$INSTDIR\${PRODUCT}.exe %1"

  ; Program class registration
  WriteRegStr HKCU "SOFTWARE\Classes\${PRODUCT}\Application" "ApplicationName" "${PRODUCT}"
  WriteRegStr HKCU "SOFTWARE\Classes\${PRODUCT}\DefaultIcon" "" "$INSTDIR\${PRODUCT}.exe,0"
  WriteRegStr HKCU "SOFTWARE\Classes\${PRODUCT}\shell\open\command" "" "$INSTDIR\${PRODUCT}.exe %1"
  WriteRegStr HKCU "SOFTWARE\Classes\${PRODUCT}\Capabilities" "ApplicationName" "${PRODUCT}"
  WriteRegStr HKCU "SOFTWARE\Classes\${PRODUCT}\Capabilities" "ApplicationIcon" "$INSTDIR\${PRODUCT}.exe,0"
  WriteRegStr HKCU "SOFTWARE\Classes\${PRODUCT}\Capabilities" "ApplicationDescription" "${DESCRIPTION}"
  WriteRegStr HKCU "SOFTWARE\Classes\${PRODUCT}\Capabilities\UrlAssociations" "microsoft-edge" "${PRODUCT}.microsoft-edge"

  ; Application registration
  WriteRegStr HKCU "SOFTWARE\Classes\Applications\${PRODUCT}.exe\DefaultIcon" "" "$INSTDIR\${PRODUCT}.exe,0"

  ; Program registration
  WriteRegStr HKCU "SOFTWARE\RegisteredApplications" "${PRODUCT}" "SOFTWARE\Classes\${PRODUCT}\Capabilities"

  ; Install the uninstaller
  WriteUninstaller "${PRODUCT}_uninstall.exe"

  ; Register the uninstaller
  WriteRegStr HKCU "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT}" "DisplayName" "${PRODUCT}"
  WriteRegStr HKCU "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT}" "DisplayIcon" "$INSTDIR\${PRODUCT}.exe,0"
  WriteRegStr HKCU "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT}" "DisplayVersion" "${VERSION_1}.${VERSION_2}.${VERSION_3}.${VERSION_4}"

  WriteRegStr HKCU "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT}" "InstallLocation" "$INSTDIR"
  WriteRegStr HKCU "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT}" "UninstallString" "$INSTDIR\${PRODUCT}_uninstall.exe"

  WriteRegDWORD HKCU "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT}" "NoModify" 1
  WriteRegDWORD HKCU "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT}" "NoRepair" 1

  ; Estimated installation size
  SectionGetSize 0 $0
  WriteRegDWORD HKCU "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT}" "EstimatedSize" $0
SectionEnd

Function .onInstSuccess
  ; Open manual installation steps
  ExecShell "open" "microsoft-edge:https://www.daniel.priv.no/tools/edgedeflector/post-install.html"
FunctionEnd

;--------------------------------


Section "Uninstall"
  ; Remove program
  Delete "$INSTDIR\${PRODUCT}.exe"

  ; Remove registry keys
  DeleteRegKey HKCU "SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\${PRODUCT}.exe"
  DeleteRegKey HKCU "SOFTWARE\Classes\${PRODUCT}"
  DeleteRegKey HKCU "SOFTWARE\Classes\${PRODUCT}.microsoft-edge"
  DeleteRegKey HKCU "SOFTWARE\Classes\Applications\${PRODUCT}.exe"
  DeleteRegValue HKCU "SOFTWARE\RegisteredApplications" "${PRODUCT}"

  ; Remove uninstaller
  DeleteRegKey HKCU "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT}"
  Delete "$INSTDIR\${PRODUCT}_uninstall.exe"

  RMDir "$INSTDIR"
SectionEnd
