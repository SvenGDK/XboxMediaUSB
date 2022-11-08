# XboxMediaUSB

<img width="256" alt="w11" src="https://user-images.githubusercontent.com/84620/196041768-b9ecbc36-1c31-4abf-ad04-efb86bb41f78.png"> <img width="256" alt="w2k" src="https://user-images.githubusercontent.com/84620/196042058-62b5c622-3ffd-46af-9fc3-ac62d31155c5.png">

This tool is a simple Windows GUI for @Gamr13 USB script.

It runs on Windows 98 with .NET Framework 2 installed up to Windows 11, ARM is also supported.

It is possible to add new content to your Xbox USB drive from other operating systems like MacOS or Linux AFTER it has been formatted with XboxMediaUSB on a Windows system, you only need a NTFS driver which can write data. The new content will be recognized without any issues.

### Version 1 comes with 2 options:
#### 1. Format external USB drive to NTFS and set permissions for ALL APPLICATION PACKAGES.

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

#### 2. Set permissions for ALL APPLICATION PACKAGES on existing external USB drive
> This option will ONLY add **ALL APPLICATION PACKAGES** to your external drive.<br/>
> Use this when you already have set up your external drive for your Xbox.
