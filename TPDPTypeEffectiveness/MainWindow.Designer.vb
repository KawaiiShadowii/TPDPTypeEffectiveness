<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainWindow
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainWindow))
        Me.cmb_CharacterSelect = New System.Windows.Forms.ComboBox()
        Me.btn_Form1 = New System.Windows.Forms.Button()
        Me.btn_Form2 = New System.Windows.Forms.Button()
        Me.btn_Form3 = New System.Windows.Forms.Button()
        Me.btn_Form4 = New System.Windows.Forms.Button()
        Me.lbl_Type1 = New System.Windows.Forms.Label()
        Me.lbl_Type2 = New System.Windows.Forms.Label()
        Me.lbl_HP = New System.Windows.Forms.Label()
        Me.lbl_FoAtk = New System.Windows.Forms.Label()
        Me.lbl_FoDef = New System.Windows.Forms.Label()
        Me.lbl_SpAtk = New System.Windows.Forms.Label()
        Me.lbl_SpDef = New System.Windows.Forms.Label()
        Me.lbl_Spd = New System.Windows.Forms.Label()
        Me.lbl_Cost = New System.Windows.Forms.Label()
        Me.lbl_HPBG = New System.Windows.Forms.Label()
        Me.lbl_FoAtkBG = New System.Windows.Forms.Label()
        Me.lbl_FoDefBG = New System.Windows.Forms.Label()
        Me.lbl_SpAtkBG = New System.Windows.Forms.Label()
        Me.lbl_SpDefBG = New System.Windows.Forms.Label()
        Me.lbl_SpdBG = New System.Windows.Forms.Label()
        Me.lbl_CostValue = New System.Windows.Forms.Label()
        Me.lbl_HPValue = New System.Windows.Forms.Label()
        Me.lbl_FoAtkValue = New System.Windows.Forms.Label()
        Me.lbl_FoDefValue = New System.Windows.Forms.Label()
        Me.lbl_SpAtkValue = New System.Windows.Forms.Label()
        Me.lbl_SpDefValue = New System.Windows.Forms.Label()
        Me.lbl_SpdValue = New System.Windows.Forms.Label()
        Me.btn_Order = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cmb_CharacterSelect
        '
        Me.cmb_CharacterSelect.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmb_CharacterSelect.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmb_CharacterSelect.FormattingEnabled = True
        Me.cmb_CharacterSelect.IntegralHeight = False
        Me.cmb_CharacterSelect.Location = New System.Drawing.Point(12, 12)
        Me.cmb_CharacterSelect.Name = "cmb_CharacterSelect"
        Me.cmb_CharacterSelect.Size = New System.Drawing.Size(326, 21)
        Me.cmb_CharacterSelect.TabIndex = 0
        '
        'btn_Form1
        '
        Me.btn_Form1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Form1.Location = New System.Drawing.Point(12, 39)
        Me.btn_Form1.Name = "btn_Form1"
        Me.btn_Form1.Size = New System.Drawing.Size(100, 23)
        Me.btn_Form1.TabIndex = 2
        Me.btn_Form1.Text = "Form 1"
        Me.btn_Form1.UseVisualStyleBackColor = True
        '
        'btn_Form2
        '
        Me.btn_Form2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Form2.Location = New System.Drawing.Point(125, 39)
        Me.btn_Form2.Name = "btn_Form2"
        Me.btn_Form2.Size = New System.Drawing.Size(100, 23)
        Me.btn_Form2.TabIndex = 3
        Me.btn_Form2.Text = "Form 2"
        Me.btn_Form2.UseVisualStyleBackColor = True
        '
        'btn_Form3
        '
        Me.btn_Form3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Form3.Location = New System.Drawing.Point(238, 39)
        Me.btn_Form3.Name = "btn_Form3"
        Me.btn_Form3.Size = New System.Drawing.Size(100, 23)
        Me.btn_Form3.TabIndex = 4
        Me.btn_Form3.Text = "Form 3"
        Me.btn_Form3.UseVisualStyleBackColor = True
        '
        'btn_Form4
        '
        Me.btn_Form4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Form4.Location = New System.Drawing.Point(351, 39)
        Me.btn_Form4.Name = "btn_Form4"
        Me.btn_Form4.Size = New System.Drawing.Size(100, 23)
        Me.btn_Form4.TabIndex = 5
        Me.btn_Form4.Text = "Form 4"
        Me.btn_Form4.UseVisualStyleBackColor = True
        '
        'lbl_Type1
        '
        Me.lbl_Type1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Type1.Location = New System.Drawing.Point(125, 70)
        Me.lbl_Type1.Name = "lbl_Type1"
        Me.lbl_Type1.Size = New System.Drawing.Size(100, 23)
        Me.lbl_Type1.TabIndex = 5
        Me.lbl_Type1.Text = "Type 1"
        Me.lbl_Type1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_Type2
        '
        Me.lbl_Type2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Type2.Location = New System.Drawing.Point(238, 70)
        Me.lbl_Type2.Name = "lbl_Type2"
        Me.lbl_Type2.Size = New System.Drawing.Size(100, 23)
        Me.lbl_Type2.TabIndex = 6
        Me.lbl_Type2.Text = "Type 2"
        Me.lbl_Type2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_HP
        '
        Me.lbl_HP.AutoSize = True
        Me.lbl_HP.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_HP.Location = New System.Drawing.Point(13, 103)
        Me.lbl_HP.Name = "lbl_HP"
        Me.lbl_HP.Size = New System.Drawing.Size(24, 13)
        Me.lbl_HP.TabIndex = 7
        Me.lbl_HP.Text = "HP"
        '
        'lbl_FoAtk
        '
        Me.lbl_FoAtk.AutoSize = True
        Me.lbl_FoAtk.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_FoAtk.Location = New System.Drawing.Point(13, 116)
        Me.lbl_FoAtk.Name = "lbl_FoAtk"
        Me.lbl_FoAtk.Size = New System.Drawing.Size(44, 13)
        Me.lbl_FoAtk.TabIndex = 8
        Me.lbl_FoAtk.Text = "Fo.Atk"
        '
        'lbl_FoDef
        '
        Me.lbl_FoDef.AutoSize = True
        Me.lbl_FoDef.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_FoDef.Location = New System.Drawing.Point(13, 129)
        Me.lbl_FoDef.Name = "lbl_FoDef"
        Me.lbl_FoDef.Size = New System.Drawing.Size(45, 13)
        Me.lbl_FoDef.TabIndex = 9
        Me.lbl_FoDef.Text = "Fo.Def"
        '
        'lbl_SpAtk
        '
        Me.lbl_SpAtk.AutoSize = True
        Me.lbl_SpAtk.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_SpAtk.Location = New System.Drawing.Point(13, 142)
        Me.lbl_SpAtk.Name = "lbl_SpAtk"
        Me.lbl_SpAtk.Size = New System.Drawing.Size(45, 13)
        Me.lbl_SpAtk.TabIndex = 10
        Me.lbl_SpAtk.Text = "Sp.Atk"
        '
        'lbl_SpDef
        '
        Me.lbl_SpDef.AutoSize = True
        Me.lbl_SpDef.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_SpDef.Location = New System.Drawing.Point(13, 155)
        Me.lbl_SpDef.Name = "lbl_SpDef"
        Me.lbl_SpDef.Size = New System.Drawing.Size(46, 13)
        Me.lbl_SpDef.TabIndex = 11
        Me.lbl_SpDef.Text = "Sp.Def"
        '
        'lbl_Spd
        '
        Me.lbl_Spd.AutoSize = True
        Me.lbl_Spd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Spd.Location = New System.Drawing.Point(13, 168)
        Me.lbl_Spd.Name = "lbl_Spd"
        Me.lbl_Spd.Size = New System.Drawing.Size(29, 13)
        Me.lbl_Spd.TabIndex = 12
        Me.lbl_Spd.Text = "Spd"
        '
        'lbl_Cost
        '
        Me.lbl_Cost.AutoSize = True
        Me.lbl_Cost.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Cost.Location = New System.Drawing.Point(13, 181)
        Me.lbl_Cost.Name = "lbl_Cost"
        Me.lbl_Cost.Size = New System.Drawing.Size(32, 13)
        Me.lbl_Cost.TabIndex = 13
        Me.lbl_Cost.Text = "Cost"
        '
        'lbl_HPBG
        '
        Me.lbl_HPBG.BackColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.lbl_HPBG.Location = New System.Drawing.Point(96, 103)
        Me.lbl_HPBG.Name = "lbl_HPBG"
        Me.lbl_HPBG.Size = New System.Drawing.Size(356, 13)
        Me.lbl_HPBG.TabIndex = 14
        '
        'lbl_FoAtkBG
        '
        Me.lbl_FoAtkBG.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.lbl_FoAtkBG.Location = New System.Drawing.Point(96, 116)
        Me.lbl_FoAtkBG.Name = "lbl_FoAtkBG"
        Me.lbl_FoAtkBG.Size = New System.Drawing.Size(356, 13)
        Me.lbl_FoAtkBG.TabIndex = 15
        '
        'lbl_FoDefBG
        '
        Me.lbl_FoDefBG.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.lbl_FoDefBG.Location = New System.Drawing.Point(96, 129)
        Me.lbl_FoDefBG.Name = "lbl_FoDefBG"
        Me.lbl_FoDefBG.Size = New System.Drawing.Size(356, 13)
        Me.lbl_FoDefBG.TabIndex = 16
        '
        'lbl_SpAtkBG
        '
        Me.lbl_SpAtkBG.BackColor = System.Drawing.Color.FromArgb(CType(CType(153, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lbl_SpAtkBG.Location = New System.Drawing.Point(96, 142)
        Me.lbl_SpAtkBG.Name = "lbl_SpAtkBG"
        Me.lbl_SpAtkBG.Size = New System.Drawing.Size(356, 13)
        Me.lbl_SpAtkBG.TabIndex = 17
        '
        'lbl_SpDefBG
        '
        Me.lbl_SpDefBG.BackColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lbl_SpDefBG.Location = New System.Drawing.Point(96, 155)
        Me.lbl_SpDefBG.Name = "lbl_SpDefBG"
        Me.lbl_SpDefBG.Size = New System.Drawing.Size(356, 13)
        Me.lbl_SpDefBG.TabIndex = 18
        '
        'lbl_SpdBG
        '
        Me.lbl_SpdBG.BackColor = System.Drawing.Color.FromArgb(CType(CType(153, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.lbl_SpdBG.Location = New System.Drawing.Point(96, 168)
        Me.lbl_SpdBG.Name = "lbl_SpdBG"
        Me.lbl_SpdBG.Size = New System.Drawing.Size(356, 13)
        Me.lbl_SpdBG.TabIndex = 19
        '
        'lbl_CostValue
        '
        Me.lbl_CostValue.BackColor = System.Drawing.SystemColors.Control
        Me.lbl_CostValue.Location = New System.Drawing.Point(65, 181)
        Me.lbl_CostValue.Name = "lbl_CostValue"
        Me.lbl_CostValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lbl_CostValue.Size = New System.Drawing.Size(25, 13)
        Me.lbl_CostValue.TabIndex = 20
        Me.lbl_CostValue.Text = "999"
        '
        'lbl_HPValue
        '
        Me.lbl_HPValue.BackColor = System.Drawing.SystemColors.Control
        Me.lbl_HPValue.Location = New System.Drawing.Point(65, 103)
        Me.lbl_HPValue.Name = "lbl_HPValue"
        Me.lbl_HPValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lbl_HPValue.Size = New System.Drawing.Size(25, 13)
        Me.lbl_HPValue.TabIndex = 21
        Me.lbl_HPValue.Text = "999"
        '
        'lbl_FoAtkValue
        '
        Me.lbl_FoAtkValue.BackColor = System.Drawing.SystemColors.Control
        Me.lbl_FoAtkValue.Location = New System.Drawing.Point(65, 116)
        Me.lbl_FoAtkValue.Name = "lbl_FoAtkValue"
        Me.lbl_FoAtkValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lbl_FoAtkValue.Size = New System.Drawing.Size(25, 13)
        Me.lbl_FoAtkValue.TabIndex = 22
        Me.lbl_FoAtkValue.Text = "999"
        '
        'lbl_FoDefValue
        '
        Me.lbl_FoDefValue.BackColor = System.Drawing.SystemColors.Control
        Me.lbl_FoDefValue.Location = New System.Drawing.Point(65, 129)
        Me.lbl_FoDefValue.Name = "lbl_FoDefValue"
        Me.lbl_FoDefValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lbl_FoDefValue.Size = New System.Drawing.Size(25, 13)
        Me.lbl_FoDefValue.TabIndex = 23
        Me.lbl_FoDefValue.Text = "999"
        '
        'lbl_SpAtkValue
        '
        Me.lbl_SpAtkValue.BackColor = System.Drawing.SystemColors.Control
        Me.lbl_SpAtkValue.Location = New System.Drawing.Point(65, 142)
        Me.lbl_SpAtkValue.Name = "lbl_SpAtkValue"
        Me.lbl_SpAtkValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lbl_SpAtkValue.Size = New System.Drawing.Size(25, 13)
        Me.lbl_SpAtkValue.TabIndex = 24
        Me.lbl_SpAtkValue.Text = "999"
        '
        'lbl_SpDefValue
        '
        Me.lbl_SpDefValue.BackColor = System.Drawing.SystemColors.Control
        Me.lbl_SpDefValue.Location = New System.Drawing.Point(65, 155)
        Me.lbl_SpDefValue.Name = "lbl_SpDefValue"
        Me.lbl_SpDefValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lbl_SpDefValue.Size = New System.Drawing.Size(25, 13)
        Me.lbl_SpDefValue.TabIndex = 25
        Me.lbl_SpDefValue.Text = "999"
        '
        'lbl_SpdValue
        '
        Me.lbl_SpdValue.BackColor = System.Drawing.SystemColors.Control
        Me.lbl_SpdValue.Location = New System.Drawing.Point(65, 168)
        Me.lbl_SpdValue.Name = "lbl_SpdValue"
        Me.lbl_SpdValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lbl_SpdValue.Size = New System.Drawing.Size(25, 13)
        Me.lbl_SpdValue.TabIndex = 26
        Me.lbl_SpdValue.Text = "999"
        '
        'btn_Order
        '
        Me.btn_Order.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Order.Location = New System.Drawing.Point(351, 10)
        Me.btn_Order.Name = "btn_Order"
        Me.btn_Order.Size = New System.Drawing.Size(100, 23)
        Me.btn_Order.TabIndex = 1
        Me.btn_Order.Text = "Change Order"
        Me.btn_Order.UseVisualStyleBackColor = True
        '
        'MainWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(464, 321)
        Me.Controls.Add(Me.btn_Order)
        Me.Controls.Add(Me.lbl_SpdValue)
        Me.Controls.Add(Me.lbl_SpDefValue)
        Me.Controls.Add(Me.lbl_SpAtkValue)
        Me.Controls.Add(Me.lbl_FoDefValue)
        Me.Controls.Add(Me.lbl_FoAtkValue)
        Me.Controls.Add(Me.lbl_HPValue)
        Me.Controls.Add(Me.lbl_CostValue)
        Me.Controls.Add(Me.lbl_SpdBG)
        Me.Controls.Add(Me.lbl_SpDefBG)
        Me.Controls.Add(Me.lbl_SpAtkBG)
        Me.Controls.Add(Me.lbl_FoDefBG)
        Me.Controls.Add(Me.lbl_FoAtkBG)
        Me.Controls.Add(Me.lbl_HPBG)
        Me.Controls.Add(Me.lbl_Cost)
        Me.Controls.Add(Me.lbl_Spd)
        Me.Controls.Add(Me.lbl_SpDef)
        Me.Controls.Add(Me.lbl_SpAtk)
        Me.Controls.Add(Me.lbl_FoDef)
        Me.Controls.Add(Me.lbl_FoAtk)
        Me.Controls.Add(Me.lbl_HP)
        Me.Controls.Add(Me.lbl_Type2)
        Me.Controls.Add(Me.lbl_Type1)
        Me.Controls.Add(Me.btn_Form4)
        Me.Controls.Add(Me.btn_Form3)
        Me.Controls.Add(Me.btn_Form2)
        Me.Controls.Add(Me.btn_Form1)
        Me.Controls.Add(Me.cmb_CharacterSelect)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(480, 360)
        Me.MinimumSize = New System.Drawing.Size(480, 360)
        Me.Name = "MainWindow"
        Me.Text = "TPDP Type Effectiveness"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cmb_CharacterSelect As ComboBox
    Friend WithEvents btn_Form1 As Button
    Friend WithEvents btn_Form2 As Button
    Friend WithEvents btn_Form3 As Button
    Friend WithEvents btn_Form4 As Button
    Friend WithEvents lbl_Type1 As Label
    Friend WithEvents lbl_Type2 As Label
    Friend WithEvents lbl_HP As Label
    Friend WithEvents lbl_FoAtk As Label
    Friend WithEvents lbl_FoDef As Label
    Friend WithEvents lbl_SpAtk As Label
    Friend WithEvents lbl_SpDef As Label
    Friend WithEvents lbl_Spd As Label
    Friend WithEvents lbl_Cost As Label
    Friend WithEvents lbl_HPBG As Label
    Friend WithEvents lbl_FoAtkBG As Label
    Friend WithEvents lbl_FoDefBG As Label
    Friend WithEvents lbl_SpAtkBG As Label
    Friend WithEvents lbl_SpDefBG As Label
    Friend WithEvents lbl_SpdBG As Label
    Friend WithEvents lbl_CostValue As Label
    Friend WithEvents lbl_HPValue As Label
    Friend WithEvents lbl_FoAtkValue As Label
    Friend WithEvents lbl_FoDefValue As Label
    Friend WithEvents lbl_SpAtkValue As Label
    Friend WithEvents lbl_SpDefValue As Label
    Friend WithEvents lbl_SpdValue As Label
    Friend WithEvents btn_Order As Button
End Class
