Imports System.IO
Imports System.ComponentModel

Public Class MainForm

    Dim SelectedDrive As DriveInfo
    Dim ConfigFile As INI.IniFile
    Dim Language As String
    Dim LanguageConfig As INI.IniFile

    Dim WithEvents FormatWorker As New BackgroundWorker()
    Dim WithEvents PermissionWorker As New BackgroundWorker()
    Dim WithEvents ProgressTimer As New Timer()

    Delegate Sub UpdateTextStatusDelegate(ByVal statLabel As Label, ByVal stringValue As String)

    Private Sub MainForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Set Window Title and add current version
        Dim VersionString As String = String.Format("{0}.{1}", My.Application.Info.Version.Major, My.Application.Info.Version.Minor)
        Me.Text = "XboxMediaUSB v" + VersionString

        'Load config file if exists
        If File.Exists(My.Computer.FileSystem.CurrentDirectory + "\config.ini") And File.Exists(My.Computer.FileSystem.CurrentDirectory + "\languages\EN.ini") Then
            ConfigFile = New INI.IniFile(My.Computer.FileSystem.CurrentDirectory + "\config.ini") 'Set the general config file

            If Not ConfigFile.ReadValue("Config", "Language") = "" Then 'Check if any language is set in the config file
                Language = ConfigFile.ReadValue("Config", "Language") 'Set the language

                If Not Language = "EN" Then 'If it's not English then change the language
                    LanguageConfig = New INI.IniFile(My.Computer.FileSystem.CurrentDirectory + "\languages\" + Language + ".ini") 'Set the language config file
                    ChangeUILanguage()
                Else
                    LanguageConfig = New INI.IniFile(My.Computer.FileSystem.CurrentDirectory + "\languages\EN.ini") 'Required for dialog strings
                End If

            Else
                ConfigFile.WriteValue("Config", "Language", "EN") 'Set the key if no language is set
                LanguageConfig = New INI.IniFile(My.Computer.FileSystem.CurrentDirectory + "\languages\EN.ini")
            End If

        Else 'Config file and language folder do not exist
            CheckConfigAndLanguage()
            ConfigFile = New INI.IniFile(My.Computer.FileSystem.CurrentDirectory + "\config.ini") 'Set the general config file
            Language = "EN" 'Set the language
            LanguageConfig = New INI.IniFile(My.Computer.FileSystem.CurrentDirectory + "\languages\EN.ini") 'Set language config
        End If

        'List removable and fixed drives in 'Ready' state
        For Each Drive As DriveInfo In DriveInfo.GetDrives()
            If Drive.DriveType = DriveType.Removable And Drive.IsReady Or DriveType.Fixed And Drive.IsReady Then
                DriveList1.Items.Add(Drive.Name + vbTab + Drive.VolumeLabel)
                DriveList2.Items.Add(Drive.Name + vbTab + Drive.VolumeLabel)
            End If
        Next

        'Add all available languages from 'languages' directory to a combobox (in case you want to load a language that has not been added yet)
        For Each Language As String In Directory.GetFiles(My.Computer.FileSystem.CurrentDirectory + "\languages", "*.ini")
            Dim LanguageFileInfo As New FileInfo(Language)
            ToolStripLanguageBox.Items.Add(LanguageFileInfo.Name)
        Next
    End Sub

    Public Sub TextDelegateMethod(ByVal statLabel As Label, ByVal stringValue As String)
        statLabel.Text = stringValue
    End Sub

    Public Sub FormatDrive()
        Dim FormatStartInfo As New ProcessStartInfo()
        FormatStartInfo.FileName = "format.com" 'The real format command, not only 'format'.
        FormatStartInfo.Arguments = "/fs:NTFS /v:XboxMediaUSB /q " + SelectedDrive.Name.Remove(2)
        FormatStartInfo.UseShellExecute = False
        FormatStartInfo.CreateNoWindow = True
        FormatStartInfo.RedirectStandardOutput = True 'Required
        FormatStartInfo.RedirectStandardInput = True

        'Start format without user interaction
        Dim FormatProcess As Process = Process.Start(FormatStartInfo)
        Dim ProcessInputStream As StreamWriter = FormatProcess.StandardInput
        ProcessInputStream.Write(vbCr & vbLf)

        FormatProcess.WaitForExit()
    End Sub

    Private Sub DriveList1_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DriveList1.SelectedValueChanged
        If DriveList1.SelectedItem IsNot Nothing Then
            Try
                SelectedDrive = New DriveInfo(DriveList1.Text.Split(vbTab)(0))
            Catch ex As Exception
                MsgBox(LanguageConfig.ReadValue("Errors", "DriveInformationError") + " " + SelectedDrive.Name)
            End Try
        End If
    End Sub

    Private Sub DriveList2_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DriveList2.SelectedValueChanged
        If DriveList2.SelectedItem IsNot Nothing Then
            Try
                SelectedDrive = New DriveInfo(DriveList2.Text.Split(vbTab)(0))
            Catch ex As Exception
                MsgBox(LanguageConfig.ReadValue("Errors", "DriveInformationError") + " " + SelectedDrive.Name)
            End Try
        End If
    End Sub

    Private Sub FormatWorker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles FormatWorker.DoWork
        Try
            FormatDrive()
        Catch ex As Exception
            MsgBox(LanguageConfig.ReadValue("Errors", "CouldNotFormat") + " " + SelectedDrive.Name, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub FormatWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles FormatWorker.RunWorkerCompleted
        Me.Cursor = Cursors.Default
        ProgressTimer.Stop()

        If StatusLabel.InvokeRequired Then
            StatusLabel.BeginInvoke(New UpdateTextStatusDelegate(AddressOf TextDelegateMethod), SelectedDrive.Name + " " + LanguageConfig.ReadValue("Messages", "FormatSucceeded"))
        Else
            StatusLabel.Text = SelectedDrive.Name + " " + LanguageConfig.ReadValue("Messages", "FormatSucceeded")
        End If

        StatusProgressBar.Visible = False

        If MsgBox(LanguageConfig.ReadValue("Messages", "CreateDefaultFolders"), MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            CreateDirectories()
        End If

        If StatusLabel.InvokeRequired Then
            StatusLabel.BeginInvoke(New UpdateTextStatusDelegate(AddressOf TextDelegateMethod), LanguageConfig.ReadValue("Messages", "SettingPermissions") + " " + SelectedDrive.Name)
        Else
            StatusLabel.Text = LanguageConfig.ReadValue("Messages", "SettingPermissions") + " " + SelectedDrive.Name
        End If

        PermissionWorker.RunWorkerAsync()
    End Sub

    Private Sub PermissionWorker_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles PermissionWorker.DoWork
        Try
            Dim AllAppPKGUser As New Security.Principal.SecurityIdentifier("S-1-15-2-1") 'ALL APPLICATION PACKAGES
            Dim SecRule As New Security.AccessControl.FileSystemAccessRule(AllAppPKGUser, Security.AccessControl.FileSystemRights.FullControl, Security.AccessControl.InheritanceFlags.ContainerInherit Or Security.AccessControl.InheritanceFlags.ObjectInherit, Security.AccessControl.PropagationFlags.None, Security.AccessControl.AccessControlType.Allow)
            Dim CurrentDirPermissions As Security.AccessControl.DirectorySecurity = Directory.GetAccessControl(SelectedDrive.Name)

            CurrentDirPermissions.AddAccessRule(SecRule)
            Directory.SetAccessControl(SelectedDrive.Name, CurrentDirPermissions)
        Catch ex As Exception
            MsgBox(LanguageConfig.ReadValue("Errors", "CouldNotSetPermissions") + " " + SelectedDrive.Name)
        End Try
    End Sub

    Private Sub PermissionWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles PermissionWorker.RunWorkerCompleted
        If StatusLabel.InvokeRequired Then
            StatusLabel.BeginInvoke(New UpdateTextStatusDelegate(AddressOf TextDelegateMethod), "")
        Else
            StatusLabel.Text = ""
        End If

        MsgBox(SelectedDrive.Name + " " + LanguageConfig.ReadValue("Messages", "PreparedforXbox"), MsgBoxStyle.Information)
        StartButton.Enabled = True
    End Sub

    Private Sub StartButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartButton.Click
        If DriveList1.SelectedItem IsNot Nothing Then
            If MsgBox(LanguageConfig.ReadValue("Messages", "FormatConfirmationLine1") + " " + SelectedDrive.Name + " ?" + vbNewLine + _
            LanguageConfig.ReadValue("Messages", "FormatConfirmationLine2"), MsgBoxStyle.YesNo, LanguageConfig.ReadValue("Messages", "FormatConfirmation")) = MsgBoxResult.Yes Then

                'Show status and disable Start button
                StatusLabel.Visible = True
                StatusProgressBar.Visible = True
                StartButton.Enabled = False
                StatusLabel.Text = LanguageConfig.ReadValue("Messages", "FormattingDrive") + " " + SelectedDrive.Name
                Me.Cursor = Cursors.WaitCursor

                'Marquee style is not compatible with Windows 2000 and below
                Select Case System.Environment.OSVersion.Version.Major
                    Case 4 'Windows 95 - NT 4.0
                        StatusProgressBar.Style = ProgressBarStyle.Continuous
                        ProgressTimer.Start()
                    Case 5 'Windows 2000, XP and 2003
                        If System.Environment.OSVersion.Version.Minor = 1 Then 'XP
                            StatusProgressBar.Style = ProgressBarStyle.Marquee
                        Else
                            StatusProgressBar.Style = ProgressBarStyle.Continuous
                            ProgressTimer.Start()
                        End If
                    Case Else 'Newer Windows
                        StatusProgressBar.Style = ProgressBarStyle.Marquee
                End Select

                FormatWorker.RunWorkerAsync()
            End If
        End If
    End Sub

    Private Sub SetPermissionsButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetPermissionsButton.Click
        If DriveList2.SelectedItem IsNot Nothing Then
            PermissionWorker.RunWorkerAsync()
        Else
            MsgBox(LanguageConfig.ReadValue("Errors", "CouldNotSetPermissions"))
        End If
    End Sub

    Private Sub ProgressionTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ProgressTimer.Tick
        'Very simple ProgressBar loop when using Win2K or below
        If StatusProgressBar.Value <> 100 Then
            StatusProgressBar.Value += 1
        Else
            StatusProgressBar.Value = 0
        End If
    End Sub

    Private Sub ChangeUILanguage()
        PrepareGroupBox.Text = LanguageConfig.ReadValue("Interface", "PrepareGroupBox")
        SelectLabel1.Text = LanguageConfig.ReadValue("Interface", "SelectLabel1")
        SelectLabel2.Text = LanguageConfig.ReadValue("Interface", "SelectLabel2")
        WarningLabel.Text = LanguageConfig.ReadValue("Interface", "WarningLabel")
        StartButton.Text = LanguageConfig.ReadValue("Interface", "StartButton")
        SetPermissionGroupBox.Text = LanguageConfig.ReadValue("Interface", "SetPermissionGroupBox")
        SetPermissionsButton.Text = LanguageConfig.ReadValue("Interface", "SetPermissionsButton")
        SettingsToolStripMenuItem.Text = LanguageConfig.ReadValue("Interface", "Settings")
        LanguageToolStripMenuItem.Text = LanguageConfig.ReadValue("Interface", "Language")
    End Sub

    Private Sub CreateDirectories()
        'Create Games folder
        Directory.CreateDirectory(SelectedDrive.Name + "Games")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\Amstrad - GX4000")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\Atari - 2600")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\Atari - 400-800-1200XL")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\Atari - 5200")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\Atari - 7800")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\Atari - Jaguar")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\Atari - Lynx")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\DOS")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\FBNeo - Arcade Games")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\Mattel - Intellivision")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\Microsoft - MSX - MSX2 - MSX2P - MSX Turbo R")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\NEC - PC Engine - TurboGrafx 16")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\NEC - PC Engine CD - TurboGrafx-CD")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\NEC - PC Engine SuperGrafx")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\Nintendo - Family Computer Disk System")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\Nintendo - Game Boy Advance")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\Nintendo - Game Boy Color")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\Nintendo - Game Boy")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\Nintendo - GameCube")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\Nintendo - Nintendo 64")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\Nintendo - Nintendo DS")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\Nintendo - Nintendo Entertainment System")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\Nintendo - Satellaview")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\Nintendo - Super Nintendo Entertainment System")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\Nintendo - Wii")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\Sony - PlayStation")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\Sony - PlayStation 2")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\Sony - PlayStation Portable")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\Sega - 32X")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\Sega - Dreamcast")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\Sega - Game Gear")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\Sega - Master System - Mark III")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\Sega - Mega Drive - Genesis")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\Sega - Mega-CD - Sega CD")
        Directory.CreateDirectory(SelectedDrive.Name + "Games\Sega - Saturn")
        'Create RetroArch folder
        Directory.CreateDirectory(SelectedDrive.Name + "RetroArch")
        Directory.CreateDirectory(SelectedDrive.Name + "RetroArch\assets")
        Directory.CreateDirectory(SelectedDrive.Name + "RetroArch\cheats")
        Directory.CreateDirectory(SelectedDrive.Name + "RetroArch\config")
        Directory.CreateDirectory(SelectedDrive.Name + "RetroArch\info")
        Directory.CreateDirectory(SelectedDrive.Name + "RetroArch\logs")
        Directory.CreateDirectory(SelectedDrive.Name + "RetroArch\overlays")
        Directory.CreateDirectory(SelectedDrive.Name + "RetroArch\playlists")
        Directory.CreateDirectory(SelectedDrive.Name + "RetroArch\saves")
        Directory.CreateDirectory(SelectedDrive.Name + "RetroArch\shaders")
        Directory.CreateDirectory(SelectedDrive.Name + "RetroArch\states")
        Directory.CreateDirectory(SelectedDrive.Name + "RetroArch\system")
        Directory.CreateDirectory(SelectedDrive.Name + "RetroArch\system\dolphin-emu")
        Directory.CreateDirectory(SelectedDrive.Name + "RetroArch\system\pcsx2")
        Directory.CreateDirectory(SelectedDrive.Name + "RetroArch\thumbnails")
        'Create BIOS folder
        Directory.CreateDirectory(SelectedDrive.Name + "BIOS")
    End Sub

    Private Sub CheckConfigAndLanguage()
        'Re-create the config file in case it got deleted
        If Not File.Exists(My.Computer.FileSystem.CurrentDirectory + "\config.ini") Then
            Using ConfigFileWriter As New StreamWriter(My.Computer.FileSystem.CurrentDirectory + "\config.ini", False, System.Text.Encoding.GetEncoding("ISO-8859-1"))
                'System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(System.Text.Encoding.Convert(System.Text.Encoding.UTF8, System.Text.Encoding.GetEncoding("ISO-8859-1"), System.Text.Encoding.UTF8.GetBytes(myString)))
                ConfigFileWriter.WriteLine("[Config]")
                ConfigFileWriter.WriteLine("Language=EN")
            End Using
        End If

        'Re-create the 'languages' folder in case it got deleted
        If Not Directory.Exists(My.Computer.FileSystem.CurrentDirectory + "\languages") Then
            Directory.CreateDirectory(My.Computer.FileSystem.CurrentDirectory + "\languages")
        End If

        'Re-create the default EN.ini in case it got deleted
        If Not File.Exists(My.Computer.FileSystem.CurrentDirectory + "\languages\EN.ini") Then
            Using EnglishFileWriter As New StreamWriter(My.Computer.FileSystem.CurrentDirectory + "\languages\EN.ini", False, System.Text.Encoding.GetEncoding("ISO-8859-1"))
                EnglishFileWriter.WriteLine("[Interface]")
                EnglishFileWriter.WriteLine("PrepareGroupBox=Prepare new a USB drive")
                EnglishFileWriter.WriteLine("SelectLabel1=Select removable USB drive:")
                EnglishFileWriter.WriteLine("SelectLabel2=Select removable USB drive:")
                EnglishFileWriter.WriteLine("WarningLabel=Warning: All Data on this drive will be erased.")
                EnglishFileWriter.WriteLine("StartButton=Start")
                EnglishFileWriter.WriteLine("SetPermissionGroupBox=Only add permission on drive (files will not be deleted)")
                EnglishFileWriter.WriteLine("SetPermissionsButton=Add permission to selected drive")
                EnglishFileWriter.WriteLine("Settings=Settings")
                EnglishFileWriter.WriteLine("Language=Language")
                EnglishFileWriter.WriteLine("")
                EnglishFileWriter.WriteLine("[Messages]")
                EnglishFileWriter.WriteLine("FormatSucceeded=formated!")
                EnglishFileWriter.WriteLine("CreateDefaultFolders=Do you want to create the 'Games', 'BIOS' and 'RetroArch' folder on your new drive?")
                EnglishFileWriter.WriteLine("SettingPermissions=Giving 'ALL APPLICATION PACKAGES' permissions on")
                EnglishFileWriter.WriteLine("PreparedforXbox=is now prepared for your Xbox.")
                EnglishFileWriter.WriteLine("FormatConfirmation=Please confirm")
                EnglishFileWriter.WriteLine("FormatConfirmationLine1=Do you really want to format:")
                EnglishFileWriter.WriteLine("FormatConfirmationLine2=All Data on this drive will be erased.")
                EnglishFileWriter.WriteLine("FormattingDrive=Formatting drive")
                EnglishFileWriter.WriteLine("")
                EnglishFileWriter.WriteLine("[Errors]")
                EnglishFileWriter.WriteLine("DriveInformationError=Error getting drive information for")
                EnglishFileWriter.WriteLine("CouldNotFormat=Could not format drive")
                EnglishFileWriter.WriteLine("CouldNotSetPermissions=Could not set permission for 'ALL APPLICATION PACKAGES' on drive")
                EnglishFileWriter.WriteLine("SelectDriveFirst=Please select a drive from the list first")
            End Using
        End If
    End Sub

    Private Sub DefaultToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DefaultToolStripMenuItem.Click
        'Set language back to English
        ConfigFile.WriteValue("Config", "Language", "EN")
        LanguageConfig = New INI.IniFile(My.Computer.FileSystem.CurrentDirectory + "\languages\EN.ini")
        ChangeUILanguage()
    End Sub

    Private Sub ToolStripLanguageBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripLanguageBox.SelectedIndexChanged
        'Set the selected language from the combobox
        Try
            ConfigFile.WriteValue("Config", "Language", ToolStripLanguageBox.Text)
            LanguageConfig = New INI.IniFile(My.Computer.FileSystem.CurrentDirectory + "\languages\" + ToolStripLanguageBox.Text)
            ChangeUILanguage()
        Catch ex As Exception
            MsgBox("Error setting custom language. Please check your file.", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

End Class