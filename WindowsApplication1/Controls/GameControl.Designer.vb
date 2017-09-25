Namespace Controls
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class GameControl
        Inherits System.Windows.Forms.UserControl

        'UserControl overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GameControl))
        Me.ToolTip = New MetroFramework.Components.MetroToolTip()
        Me.BorderPanel1 = New NHLGames.Controls.BorderPanel()
        Me.picAway = New System.Windows.Forms.PictureBox()
        Me.vs = New MetroFramework.Controls.MetroLabel()
        Me.lblHomeScore = New MetroFramework.Controls.MetroLabel()
        Me.lblAwayScore = New MetroFramework.Controls.MetroLabel()
        Me.live2 = New System.Windows.Forms.PictureBox()
        Me.live1 = New System.Windows.Forms.PictureBox()
        Me.lblAwayTeam = New MetroFramework.Controls.MetroLabel()
        Me.picHome = New System.Windows.Forms.PictureBox()
        Me.lblHomeTeam = New MetroFramework.Controls.MetroLabel()
        Me.lblPeriod = New MetroFramework.Controls.MetroLabel()
        Me.lblTime = New MetroFramework.Controls.MetroLabel()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.lnkHome = New System.Windows.Forms.Button()
        Me.lnkAway = New System.Windows.Forms.Button()
        Me.lnkNational = New System.Windows.Forms.Button()
        Me.lnkFrench = New System.Windows.Forms.Button()
        Me.lnkThree = New System.Windows.Forms.Button()
        Me.lnkSix = New System.Windows.Forms.Button()
        Me.lnkEnd1 = New System.Windows.Forms.Button()
        Me.lnkEnd2 = New System.Windows.Forms.Button()
        Me.lnkRef = New System.Windows.Forms.Button()
        Me.lblNotInSeason = New MetroFramework.Controls.MetroLabel()
        Me.lblStreamStatus = New MetroFramework.Controls.MetroLabel()
        Me.BorderPanel1.SuspendLayout
        CType(Me.picAway,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.live2,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.live1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.picHome,System.ComponentModel.ISupportInitialize).BeginInit
        Me.FlowLayoutPanel1.SuspendLayout
        Me.SuspendLayout
        '
        'ToolTip
        '
        Me.ToolTip.Style = MetroFramework.MetroColorStyle.[Default]
        Me.ToolTip.StyleManager = Nothing
        Me.ToolTip.Theme = MetroFramework.MetroThemeStyle.Light
        '
        'BorderPanel1
        '
        Me.BorderPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.BorderPanel1.BorderColour = System.Drawing.Color.LightGray
        Me.BorderPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.BorderPanel1.Controls.Add(Me.picAway)
        Me.BorderPanel1.Controls.Add(Me.vs)
        Me.BorderPanel1.Controls.Add(Me.lblHomeScore)
        Me.BorderPanel1.Controls.Add(Me.lblAwayScore)
        Me.BorderPanel1.Controls.Add(Me.live2)
        Me.BorderPanel1.Controls.Add(Me.live1)
        Me.BorderPanel1.Controls.Add(Me.lblAwayTeam)
        Me.BorderPanel1.Controls.Add(Me.picHome)
        Me.BorderPanel1.Controls.Add(Me.lblHomeTeam)
        Me.BorderPanel1.Controls.Add(Me.lblPeriod)
        Me.BorderPanel1.Controls.Add(Me.lblTime)
        Me.BorderPanel1.Controls.Add(Me.FlowLayoutPanel1)
        Me.BorderPanel1.Controls.Add(Me.lblNotInSeason)
        Me.BorderPanel1.Controls.Add(Me.lblStreamStatus)
        Me.BorderPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BorderPanel1.Location = New System.Drawing.Point(3, 3)
        Me.BorderPanel1.Name = "BorderPanel1"
        Me.BorderPanel1.Size = New System.Drawing.Size(312, 151)
        Me.BorderPanel1.TabIndex = 9
        '
        'picAway
        '
        Me.picAway.BackgroundImage = CType(resources.GetObject("picAway.BackgroundImage"),System.Drawing.Image)
        Me.picAway.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.picAway.Location = New System.Drawing.Point(31, 23)
        Me.picAway.Name = "picAway"
        Me.picAway.Size = New System.Drawing.Size(60, 50)
        Me.picAway.TabIndex = 0
        Me.picAway.TabStop = false
        '
        'vs
        '
        Me.vs.AutoSize = true
        Me.vs.BackColor = System.Drawing.Color.Transparent
        Me.vs.FontWeight = MetroFramework.MetroLabelWeight.Bold
        Me.vs.Location = New System.Drawing.Point(142, 54)
        Me.vs.Name = "vs"
        Me.vs.Size = New System.Drawing.Size(26, 19)
        Me.vs.TabIndex = 25
        Me.vs.Text = "VS"
        Me.vs.UseCustomBackColor = true
        '
        'lblHomeScore
        '
        Me.lblHomeScore.AutoSize = true
        Me.lblHomeScore.BackColor = System.Drawing.Color.Transparent
        Me.lblHomeScore.FontSize = MetroFramework.MetroLabelSize.Tall
        Me.lblHomeScore.FontWeight = MetroFramework.MetroLabelWeight.Bold
        Me.lblHomeScore.Location = New System.Drawing.Point(185, 51)
        Me.lblHomeScore.Name = "lblHomeScore"
        Me.lblHomeScore.Size = New System.Drawing.Size(22, 25)
        Me.lblHomeScore.TabIndex = 24
        Me.lblHomeScore.Text = "0"
        Me.lblHomeScore.UseCustomBackColor = true
        '
        'lblAwayScore
        '
        Me.lblAwayScore.AutoSize = true
        Me.lblAwayScore.BackColor = System.Drawing.Color.Transparent
        Me.lblAwayScore.FontSize = MetroFramework.MetroLabelSize.Tall
        Me.lblAwayScore.FontWeight = MetroFramework.MetroLabelWeight.Bold
        Me.lblAwayScore.Location = New System.Drawing.Point(105, 51)
        Me.lblAwayScore.Name = "lblAwayScore"
        Me.lblAwayScore.Size = New System.Drawing.Size(22, 25)
        Me.lblAwayScore.TabIndex = 23
        Me.lblAwayScore.Text = "0"
        Me.lblAwayScore.UseCustomBackColor = true
        '
        'live2
        '
        Me.live2.BackgroundImage = CType(resources.GetObject("live2.BackgroundImage"),System.Drawing.Image)
        Me.live2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.live2.Location = New System.Drawing.Point(287, 3)
        Me.live2.Name = "live2"
        Me.live2.Size = New System.Drawing.Size(19, 18)
        Me.live2.TabIndex = 15
        Me.live2.TabStop = false
        '
        'live1
        '
        Me.live1.BackgroundImage = CType(resources.GetObject("live1.BackgroundImage"),System.Drawing.Image)
        Me.live1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.live1.Location = New System.Drawing.Point(3, 3)
        Me.live1.Name = "live1"
        Me.live1.Size = New System.Drawing.Size(19, 18)
        Me.live1.TabIndex = 14
        Me.live1.TabStop = false
        '
        'lblAwayTeam
        '
        Me.lblAwayTeam.BackColor = System.Drawing.Color.Transparent
        Me.lblAwayTeam.Location = New System.Drawing.Point(31, 75)
        Me.lblAwayTeam.Name = "lblAwayTeam"
        Me.lblAwayTeam.Size = New System.Drawing.Size(60, 22)
        Me.lblAwayTeam.TabIndex = 4
        Me.lblAwayTeam.Text = "Away"
        Me.lblAwayTeam.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblAwayTeam.UseCustomBackColor = true
        '
        'picHome
        '
        Me.picHome.BackgroundImage = CType(resources.GetObject("picHome.BackgroundImage"),System.Drawing.Image)
        Me.picHome.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.picHome.Location = New System.Drawing.Point(220, 23)
        Me.picHome.Name = "picHome"
        Me.picHome.Size = New System.Drawing.Size(60, 50)
        Me.picHome.TabIndex = 1
        Me.picHome.TabStop = false
        '
        'lblHomeTeam
        '
        Me.lblHomeTeam.BackColor = System.Drawing.Color.Transparent
        Me.lblHomeTeam.Location = New System.Drawing.Point(220, 75)
        Me.lblHomeTeam.Name = "lblHomeTeam"
        Me.lblHomeTeam.Size = New System.Drawing.Size(60, 22)
        Me.lblHomeTeam.TabIndex = 4
        Me.lblHomeTeam.Text = "Home"
        Me.lblHomeTeam.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblHomeTeam.UseCustomBackColor = true
        '
        'lblPeriod
        '
        Me.lblPeriod.BackColor = System.Drawing.Color.Transparent
        Me.lblPeriod.FontSize = MetroFramework.MetroLabelSize.Small
        Me.lblPeriod.Location = New System.Drawing.Point(97, 3)
        Me.lblPeriod.Name = "lblPeriod"
        Me.lblPeriod.Size = New System.Drawing.Size(117, 28)
        Me.lblPeriod.TabIndex = 11
        Me.lblPeriod.Text = "Period"
        Me.lblPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblPeriod.UseCustomBackColor = true
        '
        'lblTime
        '
        Me.lblTime.BackColor = System.Drawing.Color.Transparent
        Me.lblTime.Location = New System.Drawing.Point(97, 31)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(117, 21)
        Me.lblTime.TabIndex = 8
        Me.lblTime.Text = "7:30 PM"
        Me.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblTime.UseCustomBackColor = true
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.FlowLayoutPanel1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FlowLayoutPanel1.Controls.Add(Me.lnkHome)
        Me.FlowLayoutPanel1.Controls.Add(Me.lnkAway)
        Me.FlowLayoutPanel1.Controls.Add(Me.lnkNational)
        Me.FlowLayoutPanel1.Controls.Add(Me.lnkFrench)
        Me.FlowLayoutPanel1.Controls.Add(Me.lnkThree)
        Me.FlowLayoutPanel1.Controls.Add(Me.lnkSix)
        Me.FlowLayoutPanel1.Controls.Add(Me.lnkEnd1)
        Me.FlowLayoutPanel1.Controls.Add(Me.lnkEnd2)
        Me.FlowLayoutPanel1.Controls.Add(Me.lnkRef)
        Me.FlowLayoutPanel1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(2, 108)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(305, 38)
        Me.FlowLayoutPanel1.TabIndex = 10
        Me.FlowLayoutPanel1.WrapContents = false
        '
        'lnkHome
        '
        Me.lnkHome.BackgroundImage = CType(resources.GetObject("lnkHome.BackgroundImage"),System.Drawing.Image)
        Me.lnkHome.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.lnkHome.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.lnkHome.FlatAppearance.BorderSize = 0
        Me.lnkHome.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer))
        Me.lnkHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lnkHome.Location = New System.Drawing.Point(4, 6)
        Me.lnkHome.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.lnkHome.Name = "lnkHome"
        Me.lnkHome.Size = New System.Drawing.Size(26, 26)
        Me.lnkHome.TabIndex = 29
        Me.lnkHome.UseVisualStyleBackColor = true
        Me.lnkHome.Visible = false
        '
        'lnkAway
        '
        Me.lnkAway.BackgroundImage = CType(resources.GetObject("lnkAway.BackgroundImage"),System.Drawing.Image)
        Me.lnkAway.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.lnkAway.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.lnkAway.FlatAppearance.BorderSize = 0
        Me.lnkAway.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer))
        Me.lnkAway.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lnkAway.Location = New System.Drawing.Point(38, 6)
        Me.lnkAway.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.lnkAway.Name = "lnkAway"
        Me.lnkAway.Size = New System.Drawing.Size(26, 26)
        Me.lnkAway.TabIndex = 28
        Me.lnkAway.UseVisualStyleBackColor = true
        Me.lnkAway.Visible = false
        '
        'lnkNational
        '
        Me.lnkNational.BackgroundImage = CType(resources.GetObject("lnkNational.BackgroundImage"),System.Drawing.Image)
        Me.lnkNational.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.lnkNational.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.lnkNational.FlatAppearance.BorderSize = 0
        Me.lnkNational.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer))
        Me.lnkNational.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lnkNational.Location = New System.Drawing.Point(72, 6)
        Me.lnkNational.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.lnkNational.Name = "lnkNational"
        Me.lnkNational.Size = New System.Drawing.Size(26, 26)
        Me.lnkNational.TabIndex = 29
        Me.lnkNational.UseVisualStyleBackColor = true
        Me.lnkNational.Visible = false
        '
        'lnkFrench
        '
        Me.lnkFrench.BackgroundImage = CType(resources.GetObject("lnkFrench.BackgroundImage"),System.Drawing.Image)
        Me.lnkFrench.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.lnkFrench.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.lnkFrench.FlatAppearance.BorderSize = 0
        Me.lnkFrench.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer))
        Me.lnkFrench.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lnkFrench.Location = New System.Drawing.Point(106, 6)
        Me.lnkFrench.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.lnkFrench.Name = "lnkFrench"
        Me.lnkFrench.Size = New System.Drawing.Size(26, 26)
        Me.lnkFrench.TabIndex = 29
        Me.lnkFrench.UseVisualStyleBackColor = true
        Me.lnkFrench.Visible = false
        '
        'lnkThree
        '
        Me.lnkThree.BackgroundImage = Global.NHLGames.My.Resources.Resources.threec
        Me.lnkThree.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.lnkThree.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.lnkThree.FlatAppearance.BorderSize = 0
        Me.lnkThree.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer))
        Me.lnkThree.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lnkThree.Location = New System.Drawing.Point(140, 6)
        Me.lnkThree.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.lnkThree.Name = "lnkThree"
        Me.lnkThree.Size = New System.Drawing.Size(26, 26)
        Me.lnkThree.TabIndex = 29
        Me.lnkThree.UseVisualStyleBackColor = true
        Me.lnkThree.Visible = false
        '
        'lnkSix
        '
        Me.lnkSix.BackgroundImage = Global.NHLGames.My.Resources.Resources.sixc
        Me.lnkSix.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.lnkSix.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.lnkSix.FlatAppearance.BorderSize = 0
        Me.lnkSix.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer))
        Me.lnkSix.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lnkSix.Location = New System.Drawing.Point(174, 6)
        Me.lnkSix.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.lnkSix.Name = "lnkSix"
        Me.lnkSix.Size = New System.Drawing.Size(26, 26)
        Me.lnkSix.TabIndex = 29
        Me.lnkSix.UseVisualStyleBackColor = true
        Me.lnkSix.Visible = false
        '
        'lnkEnd1
        '
        Me.lnkEnd1.BackgroundImage = Global.NHLGames.My.Resources.Resources.endz1
        Me.lnkEnd1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.lnkEnd1.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.lnkEnd1.FlatAppearance.BorderSize = 0
        Me.lnkEnd1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer))
        Me.lnkEnd1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lnkEnd1.Location = New System.Drawing.Point(208, 6)
        Me.lnkEnd1.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.lnkEnd1.Name = "lnkEnd1"
        Me.lnkEnd1.Size = New System.Drawing.Size(26, 26)
        Me.lnkEnd1.TabIndex = 29
        Me.lnkEnd1.UseVisualStyleBackColor = true
        Me.lnkEnd1.Visible = false
        '
        'lnkEnd2
        '
        Me.lnkEnd2.BackgroundImage = Global.NHLGames.My.Resources.Resources.endz2
        Me.lnkEnd2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.lnkEnd2.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.lnkEnd2.FlatAppearance.BorderSize = 0
        Me.lnkEnd2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer))
        Me.lnkEnd2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lnkEnd2.Location = New System.Drawing.Point(242, 6)
        Me.lnkEnd2.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.lnkEnd2.Name = "lnkEnd2"
        Me.lnkEnd2.Size = New System.Drawing.Size(26, 26)
        Me.lnkEnd2.TabIndex = 29
        Me.lnkEnd2.UseVisualStyleBackColor = true
        Me.lnkEnd2.Visible = false
        '
        'lnkRef
        '
        Me.lnkRef.BackgroundImage = Global.NHLGames.My.Resources.Resources.refc
        Me.lnkRef.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.lnkRef.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.lnkRef.FlatAppearance.BorderSize = 0
        Me.lnkRef.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(224,Byte),Integer))
        Me.lnkRef.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lnkRef.Location = New System.Drawing.Point(276, 6)
        Me.lnkRef.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.lnkRef.Name = "lnkRef"
        Me.lnkRef.Size = New System.Drawing.Size(26, 26)
        Me.lnkRef.TabIndex = 29
        Me.lnkRef.UseVisualStyleBackColor = true
        Me.lnkRef.Visible = false
        '
        'lblNotInSeason
        '
        Me.lblNotInSeason.BackColor = System.Drawing.Color.Transparent
        Me.lblNotInSeason.FontSize = MetroFramework.MetroLabelSize.Small
        Me.lblNotInSeason.Location = New System.Drawing.Point(3, 76)
        Me.lblNotInSeason.Name = "lblNotInSeason"
        Me.lblNotInSeason.Size = New System.Drawing.Size(303, 22)
        Me.lblNotInSeason.TabIndex = 13
        Me.lblNotInSeason.Text = "NotInSeason"
        Me.lblNotInSeason.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblNotInSeason.UseCustomBackColor = true
        '
        'lblStreamStatus
        '
        Me.lblStreamStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblStreamStatus.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblStreamStatus.FontWeight = MetroFramework.MetroLabelWeight.Regular
        Me.lblStreamStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64,Byte),Integer), CType(CType(64,Byte),Integer), CType(CType(64,Byte),Integer))
        Me.lblStreamStatus.Location = New System.Drawing.Point(2, 108)
        Me.lblStreamStatus.Name = "lblStreamStatus"
        Me.lblStreamStatus.Size = New System.Drawing.Size(306, 38)
        Me.lblStreamStatus.TabIndex = 27
        Me.lblStreamStatus.Text = "Select a stream to watch"
        Me.lblStreamStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblStreamStatus.UseCustomBackColor = true
        Me.lblStreamStatus.UseCustomForeColor = true
        '
        'GameControl
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.BorderPanel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.Name = "GameControl"
        Me.Padding = New System.Windows.Forms.Padding(3)
        Me.Size = New System.Drawing.Size(318, 157)
        Me.BorderPanel1.ResumeLayout(false)
        Me.BorderPanel1.PerformLayout
        CType(Me.picAway,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.live2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.live1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.picHome,System.ComponentModel.ISupportInitialize).EndInit
        Me.FlowLayoutPanel1.ResumeLayout(false)
        Me.ResumeLayout(false)

End Sub

        Friend WithEvents picAway As PictureBox
        Friend WithEvents lblAwayTeam As MetroFramework.Controls.MetroLabel
        Friend WithEvents lblHomeTeam As MetroFramework.Controls.MetroLabel
        Friend WithEvents lblTime As MetroFramework.Controls.MetroLabel
        Friend WithEvents BorderPanel1 As BorderPanel
        Friend WithEvents ToolTip As MetroFramework.Components.MetroToolTip
        Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
        Friend WithEvents lblPeriod As MetroFramework.Controls.MetroLabel
        Friend WithEvents lblNotInSeason As MetroFramework.Controls.MetroLabel
        Friend WithEvents picHome As PictureBox
        Friend WithEvents live1 As PictureBox
        Friend WithEvents live2 As PictureBox
        Friend WithEvents vs As MetroFramework.Controls.MetroLabel
        Friend WithEvents lblHomeScore As MetroFramework.Controls.MetroLabel
        Friend WithEvents lblAwayScore As MetroFramework.Controls.MetroLabel
        Friend WithEvents lblStreamStatus As MetroFramework.Controls.MetroLabel
        Friend WithEvents lnkHome As Button
        Friend WithEvents lnkAway As Button
        Friend WithEvents lnkNational As Button
        Friend WithEvents lnkFrench As Button
        Friend WithEvents lnkThree As Button
        Friend WithEvents lnkSix As Button
        Friend WithEvents lnkEnd1 As Button
        Friend WithEvents lnkEnd2 As Button
        Friend WithEvents lnkRef As Button
    End Class
End Namespace