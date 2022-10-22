<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.PrepareGroupBox = New System.Windows.Forms.GroupBox
        Me.StatusLabel = New System.Windows.Forms.Label
        Me.StatusProgressBar = New System.Windows.Forms.ProgressBar
        Me.StartButton = New System.Windows.Forms.Button
        Me.WarningLabel = New System.Windows.Forms.Label
        Me.SelectLabel1 = New System.Windows.Forms.Label
        Me.DriveList1 = New System.Windows.Forms.ComboBox
        Me.SetPermissionGroupBox = New System.Windows.Forms.GroupBox
        Me.SetPermissionsButton = New System.Windows.Forms.Button
        Me.SelectLabel2 = New System.Windows.Forms.Label
        Me.DriveList2 = New System.Windows.Forms.ComboBox
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LanguageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DefaultToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripLanguageBox = New System.Windows.Forms.ToolStripComboBox
        Me.PrepareGroupBox.SuspendLayout()
        Me.SetPermissionGroupBox.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PrepareGroupBox
        '
        Me.PrepareGroupBox.Controls.Add(Me.StatusLabel)
        Me.PrepareGroupBox.Controls.Add(Me.StatusProgressBar)
        Me.PrepareGroupBox.Controls.Add(Me.StartButton)
        Me.PrepareGroupBox.Controls.Add(Me.WarningLabel)
        Me.PrepareGroupBox.Controls.Add(Me.SelectLabel1)
        Me.PrepareGroupBox.Controls.Add(Me.DriveList1)
        Me.PrepareGroupBox.Location = New System.Drawing.Point(12, 47)
        Me.PrepareGroupBox.Name = "PrepareGroupBox"
        Me.PrepareGroupBox.Size = New System.Drawing.Size(654, 215)
        Me.PrepareGroupBox.TabIndex = 2
        Me.PrepareGroupBox.TabStop = False
        Me.PrepareGroupBox.Text = "Prepare new a USB drive"
        '
        'StatusLabel
        '
        Me.StatusLabel.Location = New System.Drawing.Point(16, 152)
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusLabel.Size = New System.Drawing.Size(632, 20)
        Me.StatusLabel.TabIndex = 8
        Me.StatusLabel.Text = "%STATUS%"
        Me.StatusLabel.Visible = False
        '
        'StatusProgressBar
        '
        Me.StatusProgressBar.Location = New System.Drawing.Point(16, 175)
        Me.StatusProgressBar.Name = "StatusProgressBar"
        Me.StatusProgressBar.Size = New System.Drawing.Size(632, 23)
        Me.StatusProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.StatusProgressBar.TabIndex = 7
        Me.StatusProgressBar.Visible = False
        '
        'StartButton
        '
        Me.StartButton.Location = New System.Drawing.Point(509, 66)
        Me.StartButton.Name = "StartButton"
        Me.StartButton.Size = New System.Drawing.Size(139, 35)
        Me.StartButton.TabIndex = 6
        Me.StartButton.Text = "Start"
        Me.StartButton.UseVisualStyleBackColor = True
        '
        'WarningLabel
        '
        Me.WarningLabel.Location = New System.Drawing.Point(16, 108)
        Me.WarningLabel.Name = "WarningLabel"
        Me.WarningLabel.Size = New System.Drawing.Size(632, 20)
        Me.WarningLabel.TabIndex = 5
        Me.WarningLabel.Text = "Warning: All Data on this drive will be erased."
        '
        'SelectLabel1
        '
        Me.SelectLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.SelectLabel1.Location = New System.Drawing.Point(14, 40)
        Me.SelectLabel1.Name = "SelectLabel1"
        Me.SelectLabel1.Size = New System.Drawing.Size(634, 23)
        Me.SelectLabel1.TabIndex = 4
        Me.SelectLabel1.Text = "Select removable USB drive:"
        '
        'DriveList1
        '
        Me.DriveList1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DriveList1.FormattingEnabled = True
        Me.DriveList1.Location = New System.Drawing.Point(16, 66)
        Me.DriveList1.Name = "DriveList1"
        Me.DriveList1.Size = New System.Drawing.Size(487, 28)
        Me.DriveList1.TabIndex = 2
        '
        'SetPermissionGroupBox
        '
        Me.SetPermissionGroupBox.Controls.Add(Me.SetPermissionsButton)
        Me.SetPermissionGroupBox.Controls.Add(Me.SelectLabel2)
        Me.SetPermissionGroupBox.Controls.Add(Me.DriveList2)
        Me.SetPermissionGroupBox.Location = New System.Drawing.Point(12, 267)
        Me.SetPermissionGroupBox.Name = "SetPermissionGroupBox"
        Me.SetPermissionGroupBox.Size = New System.Drawing.Size(654, 145)
        Me.SetPermissionGroupBox.TabIndex = 3
        Me.SetPermissionGroupBox.TabStop = False
        Me.SetPermissionGroupBox.Text = "Only add permission on drive (files will not be deleted)"
        '
        'SetPermissionsButton
        '
        Me.SetPermissionsButton.Location = New System.Drawing.Point(348, 95)
        Me.SetPermissionsButton.Name = "SetPermissionsButton"
        Me.SetPermissionsButton.Size = New System.Drawing.Size(300, 35)
        Me.SetPermissionsButton.TabIndex = 9
        Me.SetPermissionsButton.Text = "Add permission to selected drive"
        Me.SetPermissionsButton.UseVisualStyleBackColor = True
        '
        'SelectLabel2
        '
        Me.SelectLabel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.SelectLabel2.Location = New System.Drawing.Point(12, 32)
        Me.SelectLabel2.Name = "SelectLabel2"
        Me.SelectLabel2.Size = New System.Drawing.Size(636, 23)
        Me.SelectLabel2.TabIndex = 8
        Me.SelectLabel2.Text = "Select removable USB drive:"
        '
        'DriveList2
        '
        Me.DriveList2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DriveList2.FormattingEnabled = True
        Me.DriveList2.Location = New System.Drawing.Point(16, 59)
        Me.DriveList2.Name = "DriveList2"
        Me.DriveList2.Size = New System.Drawing.Size(632, 28)
        Me.DriveList2.TabIndex = 7
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SettingsToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(678, 33)
        Me.MenuStrip1.TabIndex = 4
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LanguageToolStripMenuItem})
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(88, 29)
        Me.SettingsToolStripMenuItem.Text = "Settings"
        '
        'LanguageToolStripMenuItem
        '
        Me.LanguageToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DefaultToolStripMenuItem, Me.ToolStripLanguageBox})
        Me.LanguageToolStripMenuItem.Name = "LanguageToolStripMenuItem"
        Me.LanguageToolStripMenuItem.Size = New System.Drawing.Size(161, 30)
        Me.LanguageToolStripMenuItem.Text = "Language"
        '
        'DefaultToolStripMenuItem
        '
        Me.DefaultToolStripMenuItem.Name = "DefaultToolStripMenuItem"
        Me.DefaultToolStripMenuItem.Size = New System.Drawing.Size(181, 30)
        Me.DefaultToolStripMenuItem.Text = "Default"
        '
        'ToolStripLanguageBox
        '
        Me.ToolStripLanguageBox.Name = "ToolStripLanguageBox"
        Me.ToolStripLanguageBox.Size = New System.Drawing.Size(121, 33)
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(678, 419)
        Me.Controls.Add(Me.SetPermissionGroupBox)
        Me.Controls.Add(Me.PrepareGroupBox)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(700, 475)
        Me.Name = "MainForm"
        Me.Text = "XboxMediaUSB"
        Me.PrepareGroupBox.ResumeLayout(False)
        Me.SetPermissionGroupBox.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PrepareGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents DriveList1 As System.Windows.Forms.ComboBox
    Friend WithEvents SelectLabel1 As System.Windows.Forms.Label
    Friend WithEvents WarningLabel As System.Windows.Forms.Label
    Friend WithEvents StartButton As System.Windows.Forms.Button
    Friend WithEvents StatusProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents StatusLabel As System.Windows.Forms.Label
    Friend WithEvents SetPermissionGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents SetPermissionsButton As System.Windows.Forms.Button
    Friend WithEvents SelectLabel2 As System.Windows.Forms.Label
    Friend WithEvents DriveList2 As System.Windows.Forms.ComboBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents SettingsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LanguageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DefaultToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripLanguageBox As System.Windows.Forms.ToolStripComboBox

End Class
