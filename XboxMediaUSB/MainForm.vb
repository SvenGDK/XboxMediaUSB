Imports System.IO

Public Class MainForm

    Dim SelectedDrive As IO.DriveInfo

    Dim WithEvents FormatWorker As New System.ComponentModel.BackgroundWorker()
    Dim WithEvents PermissionWorker As New System.ComponentModel.BackgroundWorker()

    Delegate Sub UpdateTextStatusDelegate(ByVal statLabel As Label, ByVal stringValue As String)

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        For Each Drive As DriveInfo In DriveInfo.GetDrives()
            If Drive.DriveType = DriveType.Removable And Drive.IsReady Or DriveType.Fixed And Drive.IsReady Then
                DriveList1.Items.Add(Drive.Name + vbTab + Drive.VolumeLabel)
                DriveList2.Items.Add(Drive.Name + vbTab + Drive.VolumeLabel)
            End If
        Next
    End Sub

    Public Sub TextDelegateMethod(ByVal statLabel As Label, ByVal stringValue As String)
        statLabel.Text = stringValue
    End Sub

    Public Sub FormatDrive()
        Dim FormatStartInfo As New ProcessStartInfo()
        FormatStartInfo.FileName = "format.com"
        FormatStartInfo.Arguments = "/fs:NTFS /v:XboxMediaUSB /q " + SelectedDrive.Name.Remove(2)
        FormatStartInfo.UseShellExecute = False
        FormatStartInfo.CreateNoWindow = True
        FormatStartInfo.RedirectStandardOutput = True
        FormatStartInfo.RedirectStandardInput = True

        Dim FormatProcess As Process = Process.Start(FormatStartInfo)
        Dim processInputStream As StreamWriter = FormatProcess.StandardInput
        processInputStream.Write(vbCr & vbLf)

        FormatProcess.WaitForExit()
    End Sub

    Private Sub ComboBox1_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DriveList1.SelectedValueChanged
        If DriveList1.SelectedItem IsNot Nothing Then
            Try
                SelectedDrive = New DriveInfo(DriveList1.Text.Split(vbTab)(0))
            Catch ex As Exception
                MsgBox("Error while getting Drive information for " + SelectedDrive.Name)
            End Try
        End If
    End Sub

    Private Sub DriveList2_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DriveList2.SelectedValueChanged
        If DriveList2.SelectedItem IsNot Nothing Then
            Try
                SelectedDrive = New DriveInfo(DriveList2.Text.Split(vbTab)(0))
            Catch ex As Exception
                MsgBox("Error while getting Drive information for " + SelectedDrive.Name)
            End Try
        End If
    End Sub

    Private Sub FormatWorker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles FormatWorker.DoWork
        Try
            FormatDrive()
        Catch ex As Exception
            MsgBox("Could not format " + SelectedDrive.Name)
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FormatWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles FormatWorker.RunWorkerCompleted
        Me.Cursor = Cursors.Default

        If StatusLabel.InvokeRequired Then
            StatusLabel.BeginInvoke(New UpdateTextStatusDelegate(AddressOf TextDelegateMethod), SelectedDrive.Name + " formated!")
        Else
            StatusLabel.Text = SelectedDrive.Name + " formated!"
        End If

        StatusProgressBar.Visible = False

        If MsgBox("Do you want to create the 'Games' and 'RetroArch' folders on your new drive?", MsgBoxStyle.YesNo, "Directories") = MsgBoxResult.Yes Then
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

            Directory.CreateDirectory(SelectedDrive.Name + "RetroArch")
            Directory.CreateDirectory(SelectedDrive.Name + "RetroArch\assets")
            Directory.CreateDirectory(SelectedDrive.Name + "RetroArch\cheats")
            Directory.CreateDirectory(SelectedDrive.Name + "RetroArch\configs")
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
        End If

        If StatusLabel.InvokeRequired Then
            StatusLabel.BeginInvoke(New UpdateTextStatusDelegate(AddressOf TextDelegateMethod), "Setting permissions on " + SelectedDrive.Name)
        Else
            StatusLabel.Text = "Setting permissions on " + SelectedDrive.Name
        End If

        PermissionWorker.RunWorkerAsync()
    End Sub

    Private Sub PermissionWorker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles PermissionWorker.DoWork
        Try
            Dim AllAppPKGUser As New Security.Principal.SecurityIdentifier("S-1-15-2-1") 'ALL APPLICATION PACKAGES
            Dim SecRule As New Security.AccessControl.FileSystemAccessRule(AllAppPKGUser, Security.AccessControl.FileSystemRights.FullControl, Security.AccessControl.InheritanceFlags.ContainerInherit Or Security.AccessControl.InheritanceFlags.ObjectInherit, Security.AccessControl.PropagationFlags.None, Security.AccessControl.AccessControlType.Allow)
            Dim CurrentDirPermissions As Security.AccessControl.DirectorySecurity = Directory.GetAccessControl(SelectedDrive.Name)

            CurrentDirPermissions.AddAccessRule(SecRule)
            Directory.SetAccessControl(SelectedDrive.Name, CurrentDirPermissions)
        Catch ex As Exception
            MsgBox("Could not set permissions on " + SelectedDrive.Name)
        End Try
    End Sub

    Private Sub PermissionWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles PermissionWorker.RunWorkerCompleted
        MsgBox(SelectedDrive.Name + " is now prepared to use with your Xbox.", MsgBoxStyle.Information)
        StartButton.Enabled = True
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartButton.Click
        If DriveList1.SelectedItem IsNot Nothing Then
            If MsgBox("Do you really want to format " + SelectedDrive.Name + " ? " + vbNewLine + _
            "All exisisting data on this drive will be erased.", MsgBoxStyle.YesNo, "Please confirm") = MsgBoxResult.Yes Then

                StatusLabel.Visible = True
                StatusProgressBar.Visible = True
                StartButton.Enabled = False
                StatusLabel.Text = "Formatting drive " + SelectedDrive.Name
                Me.Cursor = Cursors.WaitCursor

                FormatWorker.RunWorkerAsync()
            End If
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetPermissionsButton.Click
        If DriveList2.SelectedItem IsNot Nothing Then
            PermissionWorker.RunWorkerAsync()
        Else
            MsgBox("Please select a drive first.")
        End If
    End Sub

End Class
