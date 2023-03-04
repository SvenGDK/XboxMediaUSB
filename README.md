# XboxMediaUSB

<img width="426" alt="xboxmediausb2" src="https://user-images.githubusercontent.com/84620/222885481-ab39175d-e2be-4192-83ca-c645fd7f0a66.png">

This tool is a simple Windows GUI for @Gamr13 USB script.

v1 runs on Windows 98 with .NET Framework 2 installed up to Windows 11 (ARM is also supported in v1.3-2 or higher).</br>
v2 runs on modern Windows systems (including ARM64) with .NET Framework 4.8

It is possible to add new content to your Xbox USB drive from other operating systems</br>
like MacOS or Linux AFTER it has been formatted with XboxMediaUSB on a Windows system,</br>
you only need a NTFS driver which can write data (MacOS primarily).</br>
The new content will be recognized without any issues.

### v1 & v2 comes with 2 options:
#### 1. Format an external USB drive to NTFS and add the permissions for ALL APPLICATION PACKAGES.

###### Optional: Creates Games, BIOS and RetroArch folders with the structure listed below.
```
Games
Games\Amstrad - GX4000
Games\Atari - 2600
Games\Atari - 400-800-1200XL
Games\Atari - 5200
Games\Atari - 7800
Games\Atari - Jaguar
Games\Atari - Lynx
Games\DOS
Games\FBNeo - Arcade Games
Games\Mattel - Intellivision
Games\Microsoft - MSX - MSX2 - MSX2P - MSX Turbo R
Games\NEC - PC Engine - TurboGrafx 16
Games\NEC - PC Engine CD - TurboGrafx-CD
Games\NEC - PC Engine SuperGrafx
Games\Nintendo - Family Computer Disk System
Games\Nintendo - Game Boy Advance
Games\Nintendo - Game Boy Color
Games\Nintendo - Game Boy
Games\Nintendo - GameCube
Games\Nintendo - Nintendo 64
Games\Nintendo - Nintendo DS
Games\Nintendo - Nintendo Entertainment System
Games\Nintendo - Satellaview
Games\Nintendo - Super Nintendo Entertainment System
Games\Nintendo - Wii
Games\Sony - PlayStation
Games\Sony - PlayStation 2
Games\Sony - PlayStation Portable
Games\Sega - 32X
Games\Sega - Dreamcast
Games\Sega - Game Gear
Games\Sega - Master System - Mark III
Games\Sega - Mega Drive - Genesis
Games\Sega - Mega-CD - Sega CD
Games\Sega - Saturn

RetroArch
RetroArch\assets
RetroArch\cheats
RetroArch\config
RetroArch\info
RetroArch\logs
RetroArch\overlays
RetroArch\playlists
RetroArch\saves
RetroArch\shaders
RetroArch\states
RetroArch\system
RetroArch\system\dolphin-emu
RetroArch\system\pcsx2
RetroArch\thumbnails

BIOS
```

###### Optional: Adds an Xbox icon and volume label (XboxMediaUSB v2) to your USB drive.

#### 2. Add permissions for ALL APPLICATION PACKAGES on an existing external USB drive
> This option will ONLY add **ALL APPLICATION PACKAGES** to your external drive and does not delete any data.<br/>
> Use this when you already have set up your external drive for your Xbox.
