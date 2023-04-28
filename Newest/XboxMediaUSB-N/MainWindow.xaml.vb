Imports System.ComponentModel
Imports System.IO
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Security.AccessControl
Imports System.Security.Principal
Imports System.Text

Class MainWindow

    'Store selected drive infos
    Dim SelectedDrive As DriveInfo = Nothing

    'Config and language settings
    Dim ConfigFile As INI.IniFile = Nothing
    Dim CurrentLanguage As String = ""
    Dim LanguageConfig As INI.IniFile = Nothing

    'BackgroundWorkers
    Dim WithEvents FormatWorker As New BackgroundWorker()
    Dim WithEvents PermissionWorker As New BackgroundWorker()

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        'Set Window Title and add current version
        Dim VersionString As String = String.Format("{0}.{1}", My.Application.Info.Version.Major, My.Application.Info.Version.Minor)
        Title = "XboxMediaUSB v" + VersionString

        'Add all available languages from 'languages' directory to the LanguagesComboBox (in case you want to load a language that has not been added yet)
        For Each Language As String In Directory.GetFiles(My.Computer.FileSystem.CurrentDirectory + "\languages", "*.ini")
            Dim LanguageName As String = Path.GetFileNameWithoutExtension(Language)
            LanguagesComboBox.Items.Add(LanguageName)
        Next

        'Load config and language file if exists
        If File.Exists(My.Computer.FileSystem.CurrentDirectory + "\config.ini") And File.Exists(My.Computer.FileSystem.CurrentDirectory + "\languages\EN.ini") Then

            'Set the general config file
            ConfigFile = New INI.IniFile(My.Computer.FileSystem.CurrentDirectory + "\config.ini")

            'Check if any language is set in the config file
            If Not ConfigFile.ReadValue("Config", "Language") = "" Then

                'Get and set the language
                CurrentLanguage = ConfigFile.ReadValue("Config", "Language")
                If Not CurrentLanguage = "EN" Then
                    'If it's not English then change the language
                    LanguageConfig = New INI.IniFile(My.Computer.FileSystem.CurrentDirectory + "\languages\" + CurrentLanguage + ".ini")
                    ChangeUILanguage()
                Else
                    'Set to English - Required for dialog strings
                    LanguageConfig = New INI.IniFile(My.Computer.FileSystem.CurrentDirectory + "\languages\EN.ini")
                End If

            Else
                'Set the key if no language is set
                ConfigFile.WriteValue("Config", "Language", "EN")
                LanguageConfig = New INI.IniFile(My.Computer.FileSystem.CurrentDirectory + "\languages\EN.ini")
            End If

        Else 'Config file and language folder do not exist
            CheckConfigAndLanguage()
            ConfigFile = New INI.IniFile(My.Computer.FileSystem.CurrentDirectory + "\config.ini") 'Set the general config file
            CurrentLanguage = "EN" 'Set the language
            LanguageConfig = New INI.IniFile(My.Computer.FileSystem.CurrentDirectory + "\languages\EN.ini") 'Set language config
            ChangeUILanguage() 'Load
        End If

        'Set the language in LanguagesComboBox
        LanguagesComboBox.SelectedItem = CurrentLanguage

        'List removable and fixed drives in 'Ready' state
        For Each Drive As DriveInfo In DriveInfo.GetDrives()
            If Drive.DriveType = DriveType.Removable And Drive.IsReady Or Drive.DriveType = DriveType.Fixed And Drive.IsReady Then
                If Not Drive.Name = "C:\" Then 'Do not add C:\
                    DriveList1.Items.Add(Drive.Name + vbTab + Drive.VolumeLabel)
                    DriveList2.Items.Add(Drive.Name + vbTab + Drive.VolumeLabel)
                End If
            End If
        Next

    End Sub

    Private Sub LanguagesComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles LanguagesComboBox.SelectionChanged
        'Set the selected language from the combobox
        If LanguagesComboBox.SelectedItem IsNot Nothing And e.AddedItems(0) IsNot Nothing Then
            Try
                Dim NewLanguage As String = e.AddedItems(0).ToString
                ConfigFile.WriteValue("Config", "Language", NewLanguage)
                LanguageConfig = New INI.IniFile(My.Computer.FileSystem.CurrentDirectory + "\languages\" + NewLanguage + ".ini")
                ChangeUILanguage()
            Catch ex As Exception
                MsgBox("Error setting custom language. Please check your file.", MsgBoxStyle.Critical, "Error")
            End Try
        End If
    End Sub

#Region "Drive Selection"

    Private Sub DriveList1_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles DriveList1.SelectionChanged
        If DriveList1.SelectedItem IsNot Nothing And e.AddedItems(0) IsNot Nothing Then
            Try
                Dim SelectedDriveLetter As String = e.AddedItems(0).ToString().Split(CChar(vbTab))(0)
                SelectedDrive = New DriveInfo(SelectedDriveLetter)
            Catch ex As Exception
                MsgBox(LanguageConfig.ReadValue("Errors", "DriveInformationError"))
            End Try
        End If
    End Sub

    Private Sub DriveList2_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles DriveList2.SelectionChanged
        If DriveList2.SelectedItem IsNot Nothing And e.AddedItems(0) IsNot Nothing Then
            Try
                Dim SelectedDriveLetter As String = e.AddedItems(0).ToString().Split(CChar(vbTab))(0)
                SelectedDrive = New DriveInfo(SelectedDriveLetter)
            Catch ex As Exception
                MsgBox(LanguageConfig.ReadValue("Errors", "DriveInformationError"))
            End Try
        End If
    End Sub

#End Region

#Region "Subs"

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
        'Check if SelectedDrive is actually set
        If SelectedDrive IsNot Nothing Then
            'Double check if the name contains "\" and still exists
            If SelectedDrive.Name.EndsWith("\") And Directory.Exists(SelectedDrive.Name) Then

                If Not File.Exists(SelectedDrive.Name + "autorun.inf") Then
                    'Create an autorun file on the USB
                    Using AutorunWriter As New StreamWriter(SelectedDrive.Name + "autorun.inf", False, Encoding.UTF8)
                        AutorunWriter.WriteLine("[autorun]")
                        AutorunWriter.WriteLine("Label=XboxMediaUSBv2")
                        AutorunWriter.WriteLine("Icon=xbox.ico")
                    End Using

                    'Hide the autorun
                    File.SetAttributes(SelectedDrive.Name + "autorun.inf", FileAttributes.Hidden)
                Else
                    MsgBox("Autorun already exists.", MsgBoxStyle.Information)
                End If

                If Not File.Exists(SelectedDrive.Name + "xbox.ico") Then
                    'Save the icon to the USB
                    If Assembly.GetExecutingAssembly().GetManifestResourceStream("XboxMediaUSB.xbox.ico") IsNot Nothing Then
                        Using IconFile As FileStream = File.Create(SelectedDrive.Name + "xbox.ico")
                            Assembly.GetExecutingAssembly().GetManifestResourceStream("XboxMediaUSB.xbox.ico").CopyTo(IconFile)
                        End Using
                        File.SetAttributes(SelectedDrive.Name + "xbox.ico", FileAttributes.Hidden)
                    Else
                        If File.Exists(My.Computer.FileSystem.CurrentDirectory + "\xbox.ico") Then
                            File.Copy(My.Computer.FileSystem.CurrentDirectory + "\xbox.ico", SelectedDrive.Name + "xbox.ico", True)
                        End If
                        File.SetAttributes(SelectedDrive.Name + "xbox.ico", FileAttributes.Hidden)
                    End If
                Else
                    MsgBox("Icon already exists.", MsgBoxStyle.Information)
                End If

            End If
        End If
    End Sub

    Private Sub CheckConfigAndLanguage()
        'Re-create the config file in case it got deleted
        If Not File.Exists(My.Computer.FileSystem.CurrentDirectory + "\config.ini") Then
            Using ConfigFileWriter As New StreamWriter(My.Computer.FileSystem.CurrentDirectory + "\config.ini", False, Encoding.GetEncoding("ISO-8859-1"))
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
            Using EnglishFileWriter As New StreamWriter(My.Computer.FileSystem.CurrentDirectory + "\languages\EN.ini", False, Encoding.GetEncoding("ISO-8859-1"))
                EnglishFileWriter.WriteLine("[Interface]")
                EnglishFileWriter.WriteLine("Welcome=Welcome to XboxMediaUSB !")
                EnglishFileWriter.WriteLine("PrepareGroupBox=Prepare a new USB drive")
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

#End Region

#Region "Button Actions"

    Private Sub FormatUSBButton_Click(sender As Object, e As RoutedEventArgs) Handles FormatUSBButton.Click
        If DriveList1.SelectedItem IsNot Nothing And SelectedDrive IsNot Nothing Then
            If MsgBox(LanguageConfig.ReadValue("Messages", "FormatConfirmationLine1") + " " + SelectedDrive.Name + " ?" + vbNewLine +
            LanguageConfig.ReadValue("Messages", "FormatConfirmationLine2"),
                      MsgBoxStyle.YesNo, LanguageConfig.ReadValue("Messages", "FormatConfirmation")) = MsgBoxResult.Yes Then

                If Dispatcher.CheckAccess() = False Then
                    Dispatcher.BeginInvoke(Sub()
                                               MainWin.Height = 470

                                               'Show status and disable Start button
                                               StatusTextBlock.Visibility = Visibility.Visible
                                               StatusProgressBar.Visibility = Visibility.Visible

                                               FormatUSBButton.IsEnabled = False
                                               AddPermissionsButton.IsEnabled = False
                                               SetUSBInfosButton.IsEnabled = False

                                               StatusTextBlock.Text = LanguageConfig.ReadValue("Messages", "FormattingDrive") + " " + SelectedDrive.Name

                                               Cursor = Cursors.Wait
                                               StatusProgressBar.IsIndeterminate = True
                                           End Sub)
                Else
                    MainWin.Height = 470

                    'Show status and disable Start button
                    StatusTextBlock.Visibility = Visibility.Visible
                    StatusProgressBar.Visibility = Visibility.Visible

                    FormatUSBButton.IsEnabled = False
                    AddPermissionsButton.IsEnabled = False
                    SetUSBInfosButton.IsEnabled = False

                    StatusTextBlock.Text = LanguageConfig.ReadValue("Messages", "FormattingDrive") + " " + SelectedDrive.Name

                    Cursor = Cursors.Wait
                    StatusProgressBar.IsIndeterminate = True
                End If

                FormatWorker.RunWorkerAsync()
            End If
        Else
            MsgBox("Please select a drive first", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub AddPermissionsButton_Click(sender As Object, e As RoutedEventArgs) Handles AddPermissionsButton.Click
        If DriveList2.SelectedItem IsNot Nothing And SelectedDrive IsNot Nothing Then
            PermissionWorker.RunWorkerAsync()
        Else
            MsgBox("Please select a drive first", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub SetUSBInfosButton_Click(sender As Object, e As RoutedEventArgs) Handles SetUSBInfosButton.Click
        If DriveList2.SelectedItem IsNot Nothing And SelectedDrive IsNot Nothing Then
            CreateUSBAutorun()
        Else
            MsgBox("Please select a drive first", MsgBoxStyle.Exclamation)
        End If
    End Sub

#End Region

#Region "BackgroundWorker Events"

    Private Sub PermissionWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles PermissionWorker.DoWork
        Try
            Dim AllApplicationPackagesUser As New SecurityIdentifier("S-1-15-2-1")
            Dim CurrentDirPermissions As DirectorySecurity = Directory.GetAccessControl(SelectedDrive.Name)
            Dim SecurityRule As New FileSystemAccessRule(AllApplicationPackagesUser,
                                                         FileSystemRights.FullControl,
                                                         InheritanceFlags.ContainerInherit Or InheritanceFlags.ObjectInherit,
                                                         PropagationFlags.None,
                                                         AccessControlType.Allow)
            CurrentDirPermissions.AddAccessRule(SecurityRule)

            If Dispatcher.CheckAccess() = False Then
                Dispatcher.BeginInvoke(Sub() Directory.SetAccessControl(SelectedDrive.Name, CurrentDirPermissions))
            Else
                Directory.SetAccessControl(SelectedDrive.Name, CurrentDirPermissions)
            End If

        Catch ex As Exception
            MsgBox(LanguageConfig.ReadValue("Errors", "CouldNotSetPermissions") + " " + SelectedDrive.Name)
        End Try
    End Sub

    Private Sub PermissionWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles PermissionWorker.RunWorkerCompleted
        If StatusTextBlock.Dispatcher.CheckAccess() = False Then
            StatusTextBlock.Dispatcher.BeginInvoke(Sub() StatusTextBlock.Text = "")
        Else
            StatusTextBlock.Text = ""
        End If

        If MainWin.Dispatcher.CheckAccess() = False Then
            MainWin.Dispatcher.BeginInvoke(Sub()
                                               If MainWin.Height = 470 Then
                                                   MainWin.Height = 435
                                               End If
                                           End Sub)
        Else
            If MainWin.Height = 470 Then
                MainWin.Height = 435
            End If
        End If

        If Dispatcher.CheckAccess() = False Then
            Dispatcher.BeginInvoke(Sub()
                                       FormatUSBButton.IsEnabled = True
                                       AddPermissionsButton.IsEnabled = True
                                       SetUSBInfosButton.IsEnabled = True
                                       Cursor = Cursors.Arrow
                                   End Sub)
        Else
            FormatUSBButton.IsEnabled = True
            AddPermissionsButton.IsEnabled = True
            SetUSBInfosButton.IsEnabled = True
            Cursor = Cursors.Arrow
        End If

        MsgBox(SelectedDrive.Name + " " + LanguageConfig.ReadValue("Messages", "PreparedforXbox"), MsgBoxStyle.Information)
    End Sub

    Private Sub FormatWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles FormatWorker.DoWork
        Try
            'Set format process start infos
            Dim FormatStartInfo As New ProcessStartInfo With {
            .FileName = "format.com",
            .Arguments = SelectedDrive.Name.Remove(2) + " /fs:NTFS /v:XboxMediaUSBv2 /q", .UseShellExecute = False, .CreateNoWindow = True, .RedirectStandardOutput = True, .RedirectStandardInput = True}

            'Start format
            Using FormatProcess As New Process With {.StartInfo = FormatStartInfo}
                FormatProcess.Start()

                'Confirm the format process
                Dim ProcessInputStream As StreamWriter = FormatProcess.StandardInput
                ProcessInputStream.Write(vbCr & vbLf)

                'Wait until done
                FormatProcess.WaitForExit()
            End Using

        Catch ex As Exception
            MsgBox(LanguageConfig.ReadValue("Errors", "CouldNotFormat") + " " + SelectedDrive.Name, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub FormatWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles FormatWorker.RunWorkerCompleted

        'Format succeeded message
        If StatusTextBlock.Dispatcher.CheckAccess() = False Then
            StatusTextBlock.Dispatcher.BeginInvoke(Sub() StatusTextBlock.Text = SelectedDrive.Name + " " + LanguageConfig.ReadValue("Messages", "FormatSucceeded"))
        Else
            StatusTextBlock.Text = SelectedDrive.Name + " " + LanguageConfig.ReadValue("Messages", "FormatSucceeded")
        End If

        'Hide format progressbar
        If StatusProgressBar.Dispatcher.CheckAccess() = False Then
            StatusProgressBar.Dispatcher.BeginInvoke(Sub() StatusProgressBar.Visibility = Visibility.Hidden)
        Else
            StatusProgressBar.Visibility = Visibility.Hidden
        End If

        'Create folders if CreateDefaultFoldersCheckBox is checked
        If CreateDefaultFoldersCheckBox.IsChecked Then
            If StatusTextBlock.Dispatcher.CheckAccess() = False Then
                StatusTextBlock.Dispatcher.BeginInvoke(Sub() StatusTextBlock.Text = "Creating directories")
            Else
                StatusTextBlock.Text = "Creating directories"
            End If
            CreateDirectories()
        End If

        'Create autorun with icon if SetUSBIconCheckBox is checked
        If SetUSBIconCheckBox.IsChecked Then
            If StatusTextBlock.Dispatcher.CheckAccess() = False Then
                StatusTextBlock.Dispatcher.BeginInvoke(Sub() StatusTextBlock.Text = "Creating autorun")
            Else
                StatusTextBlock.Text = "Creating autorun"
            End If
            CreateUSBAutorun()
        End If

        'Set drive permissions
        If StatusTextBlock.Dispatcher.CheckAccess() = False Then
            StatusTextBlock.Dispatcher.BeginInvoke(Sub() StatusTextBlock.Text = LanguageConfig.ReadValue("Messages", "SettingPermissions") + " " + SelectedDrive.Name)
        Else
            StatusTextBlock.Text = LanguageConfig.ReadValue("Messages", "SettingPermissions") + " " + SelectedDrive.Name
        End If

        PermissionWorker.RunWorkerAsync()
    End Sub

#End Region

End Class

Namespace INI
    Public Class IniFile
        Public IniPath As String

        <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> Private Shared Function WritePrivateProfileString(lpAppName As String, lpKeyName As String, lpString As String, lpFileName As String) As Boolean
        End Function

        <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> Private Shared Function GetPrivateProfileString(lpAppName As String, lpKeyName As String, lpDefault As String, lpReturnedString As StringBuilder, nSize As Integer, lpFileName As String) As Integer
        End Function

        Public Sub New(INIPathValue As String)
            IniPath = INIPathValue
        End Sub

        Public Sub WriteValue(Section As String, Key As String, Value As String)
            WritePrivateProfileString(Section, Key, Value, IniPath)
        End Sub

        Public Function ReadValue(Section As String, Key As String) As String
            Dim res As Integer
            Dim sb As New StringBuilder(255)
            res = GetPrivateProfileString(Section, Key, "", sb, sb.Capacity, IniPath)
            Return sb.ToString()
        End Function
    End Class
End Namespace