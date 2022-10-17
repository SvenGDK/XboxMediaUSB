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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.StatusLabel = New System.Windows.Forms.Label
        Me.StatusProgressBar = New System.Windows.Forms.ProgressBar
        Me.StartButton = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.DriveList1 = New System.Windows.Forms.ComboBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.SetPermissionsButton = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.DriveList2 = New System.Windows.Forms.ComboBox
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.StatusLabel)
        Me.GroupBox1.Controls.Add(Me.StatusProgressBar)
        Me.GroupBox1.Controls.Add(Me.StartButton)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.DriveList1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(616, 215)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Prepare new a USB drive"
        '
        'StatusLabel
        '
        Me.StatusLabel.Location = New System.Drawing.Point(16, 152)
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusLabel.Size = New System.Drawing.Size(500, 20)
        Me.StatusLabel.TabIndex = 8
        Me.StatusLabel.Text = "%STATUS%"
        Me.StatusLabel.Visible = False
        '
        'StatusProgressBar
        '
        Me.StatusProgressBar.Location = New System.Drawing.Point(16, 175)
        Me.StatusProgressBar.Name = "StatusProgressBar"
        Me.StatusProgressBar.Size = New System.Drawing.Size(500, 23)
        Me.StatusProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.StatusProgressBar.TabIndex = 7
        Me.StatusProgressBar.Visible = False
        '
        'StartButton
        '
        Me.StartButton.Location = New System.Drawing.Point(524, 66)
        Me.StartButton.Name = "StartButton"
        Me.StartButton.Size = New System.Drawing.Size(87, 29)
        Me.StartButton.TabIndex = 6
        Me.StartButton.Text = "Start"
        Me.StartButton.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(16, 108)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(500, 20)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Warning: All Data on this drive will be erased."
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(14, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(504, 23)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Select removable drive:"
        '
        'DriveList1
        '
        Me.DriveList1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DriveList1.FormattingEnabled = True
        Me.DriveList1.Location = New System.Drawing.Point(16, 66)
        Me.DriveList1.Name = "DriveList1"
        Me.DriveList1.Size = New System.Drawing.Size(500, 28)
        Me.DriveList1.TabIndex = 2
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.SetPermissionsButton)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.DriveList2)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 232)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(616, 145)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Set only permissions on drive (files will not be deleted)"
        '
        'SetPermissionsButton
        '
        Me.SetPermissionsButton.Location = New System.Drawing.Point(465, 100)
        Me.SetPermissionsButton.Name = "SetPermissionsButton"
        Me.SetPermissionsButton.Size = New System.Drawing.Size(146, 29)
        Me.SetPermissionsButton.TabIndex = 9
        Me.SetPermissionsButton.Text = "Set permissions"
        Me.SetPermissionsButton.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(10, 38)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(504, 23)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Select removable drive:"
        '
        'DriveList2
        '
        Me.DriveList2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DriveList2.FormattingEnabled = True
        Me.DriveList2.Location = New System.Drawing.Point(14, 65)
        Me.DriveList2.Name = "DriveList2"
        Me.DriveList2.Size = New System.Drawing.Size(596, 28)
        Me.DriveList2.TabIndex = 7
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(638, 385)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "MainForm"
        Me.Text = "XboxMediaUSB"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents DriveList1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents StartButton As System.Windows.Forms.Button
    Friend WithEvents StatusProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents StatusLabel As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents SetPermissionsButton As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DriveList2 As System.Windows.Forms.ComboBox

End Class
