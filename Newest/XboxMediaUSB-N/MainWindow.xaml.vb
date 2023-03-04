Imports System.ComponentModel
Imports System.IO
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Text

Class MainWindow

    Dim SelectedDrive As DriveInfo
    Dim ConfigFile As INI.IniFile
    Dim CurrentLanguage As String
    Dim LanguageConfig As INI.IniFile

    Dim WithEvents FormatWorker As New BackgroundWorker()
    Dim WithEvents PermissionWorker As New BackgroundWorker()

    Delegate Sub UpdateTextStatusDelegate(StatTextBlock As TextBlock, StringValue As String)

    Public Sub TextDelegateMethod(StatTextBlock As TextBlock, StringValue As String)
        StatusTextBlock.Text = StringValue
    End Sub

    Public Sub FormatDrive()
        Dim FormatStartInfo As New ProcessStartInfo With {
            .FileName = "format.com",
            .Arguments = "/fs:NTFS /v:XboxMediaUSBv2 /q " + SelectedDrive.Name.Remove(2),
            .UseShellExecute = False,
            .CreateNoWindow = True,
            .RedirectStandardOutput = True, 'Required
            .RedirectStandardInput = True
        }

        'Start format without user interaction
        Dim FormatProcess As Process = Process.Start(FormatStartInfo)
        Dim ProcessInputStream As StreamWriter = FormatProcess.StandardInput
        ProcessInputStream.Write(vbCr & vbLf)

        FormatProcess.WaitForExit()
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

    Private Sub CreateUSBAutorun()

        'Create an autorun file on the USB
        Using AutorunWriter As New StreamWriter(SelectedDrive.Name + "autorun.inf")
            AutorunWriter.WriteLine("[autorun]")
            AutorunWriter.WriteLine("Label=XboxMediaUSB v2")
            AutorunWriter.WriteLine("Icon=xbox.ico")
        End Using

        'Save the icon to the USB
        Using IconFile As FileStream = File.Create(SelectedDrive.Name + "xbox.ico")
            Assembly.GetExecutingAssembly().GetManifestResourceStream("XboxMediaUSB.xbox.ico").CopyTo(IconFile)
        End Using

        'Hide the files
        File.SetAttributes(SelectedDrive.Name + "autorun.inf", FileAttributes.Hidden)
        File.SetAttributes(SelectedDrive.Name + "xbox.ico", FileAttributes.Hidden)
    End Sub

    Private Sub CheckConfigAndLanguage()
        'Re-create the config file in case it got deleted
        If Not File.Exists(My.Computer.FileSystem.CurrentDirectory + "\config.ini") Then
            Using ConfigFileWriter As New StreamWriter(My.Computer.FileSystem.CurrentDirectory + "\config.ini", False, Encoding.GetEncoding("ISO-8859-1"))
                ConfigFileWriter.WriteLine("[Config]")
                ConfigFileWriter.WriteLine("Language=EN.ini")
            End Using
        End If

        'Re-create the 'languages' folder in case it got deleted
        If Not Directory.Exists(My.Computer.FileSystem.CurrentDirectory + "\languages") Then
            Directory.CreateDirectory(My.Computer.FileSystem.CurrentDirectory + "\languages")
        End If

        'Re-create the default EN.ini in case it got deleted
        If Not File.Exists(My.Computer.FileSystem.CurrentDirectory + "\languages\EN.ini") Then
            Using EnglishFileWriter As New StreamWriter(My.Computer.FileSystem.CurrentDirectory + "\languages\EN.ini", False, Encoding.GetEncoding("ISO-8859-1"))
                EnglishFileWriter.WriteLine("[Interface]")
                EnglishFileWriter.WriteLine("Welcome=Welcome to XboxMediaUSB !")
                EnglishFileWriter.WriteLine("PrepareGroupBox=Prepare new a USB drive")
                EnglishFileWriter.WriteLine("SelectLabel1=Select a removable USB drive from the list :")
                EnglishFileWriter.WriteLine("SelectLabel2=Select a removable USB drive from the list :")
                EnglishFileWriter.WriteLine("WarningLabel=Warning: All Data on this drive will be erased.")
                EnglishFileWriter.WriteLine("StartButton=Format USB")
                EnglishFileWriter.WriteLine("SetPermissionGroupBox=Modify an exisiting USB drive")
                EnglishFileWriter.WriteLine("SetPermissionsButton=Add permissions to drive")
                EnglishFileWriter.WriteLine("Options=Options :")
                EnglishFileWriter.WriteLine("CreateDefaultFolders=Create default folders")
                EnglishFileWriter.WriteLine("SetUSBIcon=Set USB drive icon")
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
                EnglishFileWriter.WriteLine("DriveInformationError=Error getting drive information")
                EnglishFileWriter.WriteLine("CouldNotFormat=Could not format drive")
                EnglishFileWriter.WriteLine("CouldNotSetPermissions=Could not set permission for 'ALL APPLICATION PACKAGES' on drive")
                EnglishFileWriter.WriteLine("SelectDriveFirst=Please select a drive from the list first")
            End Using
        End If
    End Sub

    Private Sub ChangeUILanguage()
        'UI
        WelcomeTextBlock.Text = LanguageConfig.ReadValue("Interface", "Welcome")
        PrepareNewTextBlock.Text = LanguageConfig.ReadValue("Interface", "PrepareGroupBox")
        SelectUSBFromListTextBlock1.Text = LanguageConfig.ReadValue("Interface", "SelectLabel1")
        SelectUSBFromListTextBlock2.Text = LanguageConfig.ReadValue("Interface", "SelectLabel2")
        WarningTextBlock.Text = LanguageConfig.ReadValue("Interface", "WarningLabel")
        FormatUSBButton.Content = LanguageConfig.ReadValue("Interface", "StartButton")
        AddPermissionsTextBlock.Text = LanguageConfig.ReadValue("Interface", "SetPermissionGroupBox")
        AddPermissionsButton.Content = LanguageConfig.ReadValue("Interface", "SetPermissionsButton")
        SetUSBInfosButton.Content = LanguageConfig.ReadValue("Interface", "SetUSBIcon")

        'Options
        OptionsTextBlock.Text = LanguageConfig.ReadValue("Interface", "Options")
        CreateDefaultFoldersCheckBox.Content = LanguageConfig.ReadValue("Interface", "CreateDefaultFolders")
        SetUSBIconCheckBox.Content = LanguageConfig.ReadValue("Interface", "SetUSBIcon")

        'Menu
        SettingsMenuItem.Header = LanguageConfig.ReadValue("Interface", "Settings")
        LanguagesMenuItem.Header = LanguageConfig.ReadValue("Interface", "Language")
    End Sub

    Private Sub FormatUSBButton_Click(sender As Object, e As RoutedEventArgs) Handles FormatUSBButton.Click

        If DriveList1.SelectedItem IsNot Nothing Then
            If MsgBox(LanguageConfig.ReadValue("Messages", "FormatConfirmationLine1") + " " + SelectedDrive.Name + " ?" + vbNewLine +
            LanguageConfig.ReadValue("Messages", "FormatConfirmationLine2"), MsgBoxStyle.YesNo, LanguageConfig.ReadValue("Messages", "FormatConfirmation")) = MsgBoxResult.Yes Then

                MainWin.Height = 470

                'Show status and disable Start button
                StatusTextBlock.Visibility = Visibility.Visible
                StatusProgressBar.Visibility = Visibility.Visible

                FormatUSBButton.IsEnabled = False
                AddPermissionsButton.IsEnabled = False

                StatusTextBlock.Text = LanguageConfig.ReadValue("Messages", "FormattingDrive") + " " + SelectedDrive.Name

                Cursor = Cursors.Wait
                StatusProgressBar.IsIndeterminate = True

                FormatWorker.RunWorkerAsync()
            End If
        End If

    End Sub

    Private Sub AddPermissionsButton_Click(sender As Object, e As RoutedEventArgs) Handles AddPermissionsButton.Click
        If DriveList2.SelectedItem IsNot Nothing Then
            PermissionWorker.RunWorkerAsync()
        Else
            MsgBox(LanguageConfig.ReadValue("Errors", "CouldNotSetPermissions"))
        End If
    End Sub

    Private Sub DriveList1_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles DriveList1.SelectionChanged
        If DriveList1.SelectedItem IsNot Nothing Then
            Try
                Dim SelectedDriveItem As String = e.AddedItems(0).ToString
                SelectedDrive = New DriveInfo(SelectedDriveItem.Split(vbTab)(0))
            Catch ex As Exception
                MsgBox(LanguageConfig.ReadValue("Errors", "DriveInformationError"))
            End Try
        End If
    End Sub

    Private Sub DriveList2_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles DriveList2.SelectionChanged
        If DriveList2.SelectedItem IsNot Nothing Then
            Try
                Dim SelectedDriveItem As String = e.AddedItems(0).ToString
                SelectedDrive = New DriveInfo(SelectedDriveItem.Split(vbTab)(0))
            Catch ex As Exception
                MsgBox(LanguageConfig.ReadValue("Errors", "DriveInformationError"))
            End Try
        End If
    End Sub

    Private Sub PermissionWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles PermissionWorker.DoWork
        Try
            Dim AllApplicationPackagesUser As New Security.Principal.SecurityIdentifier("S-1-15-2-1")
            Dim SecurityRule As New Security.AccessControl.FileSystemAccessRule(AllApplicationPackagesUser,
                                                                                Security.AccessControl.FileSystemRights.FullControl,
                                                                                Security.AccessControl.InheritanceFlags.ContainerInherit Or Security.AccessControl.InheritanceFlags.ObjectInherit,
                                                                                Security.AccessControl.PropagationFlags.None,
                                                                                Security.AccessControl.AccessControlType.Allow)
            Dim CurrentDirPermissions As Security.AccessControl.DirectorySecurity = Directory.GetAccessControl(SelectedDrive.Name)
            CurrentDirPermissions.AddAccessRule(SecurityRule)

            Directory.SetAccessControl(SelectedDrive.Name, CurrentDirPermissions)
        Catch ex As Exception
            MsgBox(LanguageConfig.ReadValue("Errors", "CouldNotSetPermissions") + " " + SelectedDrive.Name)
        End Try
    End Sub

    Private Sub PermissionWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles PermissionWorker.RunWorkerCompleted
        If StatusTextBlock.Dispatcher.CheckAccess() = False Then
            StatusTextBlock.Dispatcher.BeginInvoke(New UpdateTextStatusDelegate(AddressOf TextDelegateMethod), "")
        Else
            StatusTextBlock.Text = ""
        End If

        If MainWin.Height = 470 Then
            MainWin.Height = 435
        End If

        MsgBox(SelectedDrive.Name + " " + LanguageConfig.ReadValue("Messages", "PreparedforXbox"), MsgBoxStyle.Information)
        FormatUSBButton.IsEnabled = True
    End Sub

    Private Sub FormatWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles FormatWorker.DoWork
        Try
            FormatDrive()
        Catch ex As Exception
            MsgBox(LanguageConfig.ReadValue("Errors", "CouldNotFormat") + " " + SelectedDrive.Name, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub FormatWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles FormatWorker.RunWorkerCompleted
        Cursor = Cursors.Arrow

        If StatusTextBlock.Dispatcher.CheckAccess() = False Then
            StatusTextBlock.Dispatcher.BeginInvoke(New UpdateTextStatusDelegate(AddressOf TextDelegateMethod), SelectedDrive.Name + " " + LanguageConfig.ReadValue("Messages", "FormatSucceeded"))
        Else
            StatusTextBlock.Text = SelectedDrive.Name + " " + LanguageConfig.ReadValue("Messages", "FormatSucceeded")
        End If

        StatusProgressBar.Visibility = Visibility.Hidden

        If CreateDefaultFoldersCheckBox.IsChecked Then
            CreateDirectories()
        End If

        If SetUSBIconCheckBox.IsChecked Then
            CreateUSBAutorun()
        End If

        If StatusTextBlock.Dispatcher.CheckAccess() = False Then
            StatusTextBlock.Dispatcher.BeginInvoke(New UpdateTextStatusDelegate(AddressOf TextDelegateMethod), LanguageConfig.ReadValue("Messages", "SettingPermissions") + " " + SelectedDrive.Name)
        Else
            StatusTextBlock.Text = LanguageConfig.ReadValue("Messages", "SettingPermissions") + " " + SelectedDrive.Name
        End If

        PermissionWorker.RunWorkerAsync()
    End Sub

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        'Set Window Title and add current version
        Dim VersionString As String = String.Format("{0}.{1}", My.Application.Info.Version.Major, My.Application.Info.Version.Minor)
        Title = "XboxMediaUSB v" + VersionString

        'Load config file if exists
        If File.Exists(My.Computer.FileSystem.CurrentDirectory + "\config.ini") And File.Exists(My.Computer.FileSystem.CurrentDirectory + "\languages\EN.ini") Then
            ConfigFile = New INI.IniFile(My.Computer.FileSystem.CurrentDirectory + "\config.ini") 'Set the general config file

            If Not ConfigFile.ReadValue("Config", "Language") = "" Then 'Check if any language is set in the config file
                CurrentLanguage = ConfigFile.ReadValue("Config", "Language") 'Set the language

                If Not CurrentLanguage = "EN.ini" Then 'If it's not English then change the language
                    LanguageConfig = New INI.IniFile(My.Computer.FileSystem.CurrentDirectory + "\languages\" + CurrentLanguage) 'Set the language config file
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
            CurrentLanguage = "EN" 'Set the language
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
            LanguagesComboBox.Items.Add(LanguageFileInfo.Name)
        Next
    End Sub

    Private Sub LanguagesComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles LanguagesComboBox.SelectionChanged
        'Set the selected language from the combobox
        If LanguagesComboBox.SelectedItem IsNot Nothing Then
            Try
                Dim NewLanguage As String = e.AddedItems(0).ToString
                ConfigFile.WriteValue("Config", "Language", NewLanguage)
                LanguageConfig = New INI.IniFile(My.Computer.FileSystem.CurrentDirectory + "\languages\" + NewLanguage)
                ChangeUILanguage()
            Catch ex As Exception
                MsgBox("Error setting custom language. Please check your file.", MsgBoxStyle.Critical, "Error")
            End Try
        End If
    End Sub

    Private Sub SetUSBInfosButton_Click(sender As Object, e As RoutedEventArgs) Handles SetUSBInfosButton.Click
        CreateUSBAutorun()
    End Sub

End Class

Namespace INI

    Public Class IniFile
        Public IniPath As String

        <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> Private Shared Function WritePrivateProfileString(ByVal lpAppName As String,
                        ByVal lpKeyName As String,
                        ByVal lpString As String,
                        ByVal lpFileName As String) As Boolean
        End Function

        <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> Private Shared Function GetPrivateProfileString(ByVal lpAppName As String,
                        ByVal lpKeyName As String,
                        ByVal lpDefault As String,
                        ByVal lpReturnedString As StringBuilder,
                        ByVal nSize As Integer,
                        ByVal lpFileName As String) As Integer
        End Function

        Public Sub New(ByVal INIPathValue As String)
            IniPath = INIPathValue
        End Sub

        Public Sub WriteValue(ByVal Section As String, ByVal Key As String, ByVal Value As String)
            WritePrivateProfileString(Section, Key, Value, IniPath)
        End Sub

        Public Function ReadValue(ByVal Section As String, ByVal Key As String) As String
            Dim res As Integer
            Dim sb As New StringBuilder(255)
            res = GetPrivateProfileString(Section, Key, "", sb, sb.Capacity, IniPath)
            Return sb.ToString()
        End Function
    End Class

End Namespace