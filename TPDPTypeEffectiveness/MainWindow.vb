Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions

Public Class MainWindow

    Private _puppetList As New List(Of Puppet)
    Private _maxValue As Integer
    Private _typeList As New List(Of Type)
    Private _typeChartList As New List(Of TypeChart)
    Private _sorted As Boolean = False
    Private _currentPuppetForm As PuppetForm
    Private _currentPuppetSubForm As PuppetForm
    Private _abilityList As List(Of Ability)
    Private _extendedPuppetsSourceCode As String = Nothing
    Private _fanCharaPuppetList As New List(Of String)

#Region "Functions for initial load"

    Private Function DownloadSource(url As String) As String

        Try

            Using client As New WebClient()
                Return client.DownloadString(url)
            End Using

        Catch

            MessageBox.Show(String.Concat($"An error occured while requesting {url}. It is possible that the wiki is offline. If the error still occurs after some time, please contact @kawaii_shadowii on Discord."),
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
            Environment.Exit(0)

        End Try

    End Function

    Private Sub LoadPuppets(sourceCode As String, rowsSelector As String)

        Dim doc As New HtmlAgilityPack.HtmlDocument
        doc.LoadHtml(sourceCode)

        Dim rows = doc.DocumentNode.SelectNodes(rowsSelector)

        If rows Is Nothing Then
            MessageBox.Show("An error occured while loading the rows from the puppet table. Please contact @kawaii_shadowii on Discord.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Environment.Exit(0)
        End If

        rows.RemoveAt(0)
        rows.RemoveAt(UBound(rows.ToArray()))

        Dim subFormCounter As Integer = 1

        For Each row In rows

            Dim columns = row.SelectNodes("td")

            Dim formName = RemoveHTMLTagsAndNewlines(columns(1).InnerText)
            Dim puppetFormName = formName.Split(" "c)
            Dim puppetForm = puppetFormName.FirstOrDefault()
            Dim puppetName = puppetFormName.LastOrDefault().TrimEnd("*"c)

            Dim listPuppet = _puppetList.Where(Function(x) x.Name.Equals(puppetName)).FirstOrDefault()

            If listPuppet IsNot Nothing Then

                Dim listPuppetForm = listPuppet.Forms.Where(Function(x) x.Name.Equals(puppetForm)).FirstOrDefault()

                If listPuppetForm IsNot Nothing Then
                    If listPuppetForm.SubForms Is Nothing Then listPuppetForm.SubForms = New List(Of PuppetForm)
                    listPuppetForm.SubForms.Add(CreatePuppetForm(String.Concat("Sub Form ", subFormCounter), columns))
                    subFormCounter += 1
                Else
                    listPuppet.Forms.Add(CreatePuppetForm(puppetForm, columns))
                    subFormCounter = 1
                End If

            Else

                Dim puppet As New Puppet With {
                    .Name = puppetName,
                    .Forms = New List(Of PuppetForm)
                }

                puppet.Forms.Add(CreatePuppetForm(puppetForm, columns))

                _puppetList.Add(puppet)

            End If

        Next

    End Sub

    Private Sub LoadExtendedPuppets(sourceCode As String, rowsSelector As String, isFanChara As Boolean)

        Dim doc As New HtmlAgilityPack.HtmlDocument
        doc.LoadHtml(sourceCode)

        Dim rows = doc.DocumentNode.SelectNodes(rowsSelector)

        If rows Is Nothing Then
            MessageBox.Show("An error occured while loading the rows from the extended puppet table. Please contact @kawaii_shadowii on Discord.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Environment.Exit(0)
        End If

        rows.RemoveAt(0)
        rows.RemoveAt(UBound(rows.ToArray()))

        For Each row In rows

            Dim tableHeader = row.SelectSingleNode("th")

            If tableHeader IsNot Nothing Then

                Dim puppetName As String = RemoveHTMLTagsAndNewlines(tableHeader.InnerText)

                _puppetList.Add(New Puppet With {
                    .Name = puppetName,
                    .Forms = New List(Of PuppetForm)
                })

                If isFanChara Then _fanCharaPuppetList.Add(puppetName)

            End If

            Dim columns = row.SelectNodes("td")

            Dim puppetForm = columns(1).InnerText
            _puppetList.LastOrDefault().Forms.Add(CreatePuppetForm(puppetForm, columns))

        Next

    End Sub

    Private Sub SetMaxValue()

        Dim values As New List(Of Integer)

        values.Add(_puppetList.Select(Function(x) x.Forms.Select(Function(y) y.MaxHP).Max()).Max())
        values.Add(_puppetList.Select(Function(x) x.Forms.Select(Function(y) y.MaxFoAtk).Max()).Max())
        values.Add(_puppetList.Select(Function(x) x.Forms.Select(Function(y) y.MaxFoDef).Max()).Max())
        values.Add(_puppetList.Select(Function(x) x.Forms.Select(Function(y) y.MaxSpAtk).Max()).Max())
        values.Add(_puppetList.Select(Function(x) x.Forms.Select(Function(y) y.MaxSpDef).Max()).Max())
        values.Add(_puppetList.Select(Function(x) x.Forms.Select(Function(y) y.MaxSpd).Max()).Max())

        _maxValue = values.Max(Function(x) x)

    End Sub

    Private Sub LoadTypesAndTypeChart(sourceCode As String, rowsSelector As String)

        Dim doc As New HtmlAgilityPack.HtmlDocument
        doc.LoadHtml(sourceCode)

        Dim rows = doc.DocumentNode.SelectNodes(rowsSelector)

        If rows Is Nothing Then
            MessageBox.Show("An error occured while loading the rows from the type chart. Please contact @kawaii_shadowii on Discord.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Environment.Exit(0)
        End If

        rows.RemoveAt(0)

        Dim rowPos As Integer = 0

        For Each row In rows

            Dim type = row.SelectSingleNode("td")
            Dim typeStyles = type.Attributes.AttributesWithName("style").FirstOrDefault().Value.Split(";"c)
            Dim typeBackColor = typeStyles(1).Substring(typeStyles(1).LastIndexOf("#"c), 7)
            Dim typeColor = typeStyles(2).Substring(typeStyles(2).LastIndexOf("#"c), 7)

            _typeList.Add(New Type With {
                .Name = RemoveHTMLTagsAndNewlines(type.InnerText),
                .Pos = rowPos,
                .BackColor = typeBackColor,
                .Color = typeColor
            })

            Dim columnPos As Integer = 0

            For Each column In row.SelectNodes("td").Skip(1)

                Dim value As Double = 1

                Select Case RemoveHTMLTagsAndNewlines(column.InnerText)
                    Case "O"
                        value = 2
                    Case String.Empty
                        value = 1
                    Case "X"
                        value = 0
                    Case Else
                        value = 0.5
                End Select

                _typeChartList.Add(New TypeChart With {
                    .X = columnPos,
                    .Y = rowPos,
                    .Value = value
                })

                columnPos += 1

            Next

            rowPos += 1

        Next

    End Sub

    Private Sub LoadPuppetsIntoComboBox()

        If Not _sorted Then
            cmb_CharacterSelect.DataSource = _puppetList.Select(Function(x) x.Name).ToList()
        Else
            cmb_CharacterSelect.DataSource = _puppetList.Select(Function(x) x.Name).OrderBy(Function(x) x).ToList()
        End If

    End Sub

#End Region

#Region "Event Functions"

    Private Sub MainWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim puppetsSourceCode As String = DownloadSource("https://tpdp.miraheze.org/wiki/Puppetdex")
        LoadPuppets(puppetsSourceCode, "//html/body/div/div/div[@class='mw-content-container']/main[@id='content']/div[@id='bodyContent']/div[@id='mw-content-text']/div/div/section/article[@data-title='SoD 1.103']/table/tbody/tr")

        _extendedPuppetsSourceCode = DownloadSource("https://tpdp.miraheze.org/wiki/Mod:Mod_Puppetdex")
        LoadExtendedPuppets(_extendedPuppetsSourceCode, "//html/body/div/div/div[@class='mw-content-container']/main[@id='content']/div[@id='bodyContent']/div[@id='mw-content-text']/div/div/section/article[@data-title='Shard of Dreams - Extended -']/table/tbody/tr", False)

        SetMaxValue()

        Dim typesSourceCode As String = DownloadSource("https://tpdp.miraheze.org/wiki/Type_Chart")
        LoadTypesAndTypeChart(typesSourceCode, "//html/body/div/div/div[@class='mw-content-container']/main[@id='content']/div[@id='bodyContent']/div[@id='mw-content-text']/div/div/section/article[@data-title='SoD 1.103']/table[@class='wikitable'][1]/tbody/tr")

        _abilityList = Ability.LoadAbilities()

        LoadPuppetsIntoComboBox()

    End Sub

    Private Sub cmb_CharacterSelect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_CharacterSelect.SelectedIndexChanged
        ChangePupetInfo()
    End Sub

    Private Sub ChangePupetInfo()

        Dim formInitial As String = Nothing
        Dim puppetName As String

        If cmb_CharacterSelect.Text.Contains("_"c) Then

            puppetName = cmb_CharacterSelect.Text.Remove(0, cmb_CharacterSelect.Text.IndexOf("_"c) + 1)
            formInitial = cmb_CharacterSelect.Text.Substring(0, cmb_CharacterSelect.Text.IndexOf("_"c)).ToUpper()

        Else
            puppetName = cmb_CharacterSelect.Text
        End If

        Dim puppet = _puppetList.Where(Function(x) x.Name.ToLower().Equals(puppetName.ToLower())).FirstOrDefault()
        If puppet Is Nothing Then Exit Sub

        btn_Form1.Text = puppet.Forms(0).Name
        btn_Form2.Text = puppet.Forms(1).Name
        btn_Form3.Text = puppet.Forms(2).Name
        btn_Form4.Text = puppet.Forms(3).Name

        Dim button As Button = Nothing
        If Not String.IsNullOrWhiteSpace(formInitial) Then button = Me.Controls.OfType(Of Button).Where(Function(x) x.Name.StartsWith("btn_Form") AndAlso x.Text.StartsWith(formInitial)).FirstOrDefault()

        cmb_CharacterSelect.Text = puppetName

        If button IsNot Nothing Then
            FormButtonClick(button)
        Else
            FormButtonClick(btn_Form1)
        End If

    End Sub

    Private Sub FormButtonClickEvent(sender As Object, e As EventArgs) Handles btn_Form1.Click, btn_Form2.Click, btn_Form3.Click, btn_Form4.Click
        FormButtonClick(sender)
    End Sub

    Private Sub FormButtonClick(sender As Object)

        If Not _puppetList.Select(Function(x) x.Name.ToLower()).Contains(cmb_CharacterSelect.Text.ToLower()) Then Exit Sub

        ChangeButtonEnabledState(sender, "btn_Form")
        ChangeButtonEnabledState(btn_StatsBase, "btn_Stats")
        UpdateStatsAndTypes(sender)
        ChangeDynamicButtonEnabledState(sender, "dynAbilitySubFormBtn")

    End Sub

    Private Sub StatsButtonClickEvent(sender As Object, e As EventArgs) Handles btn_StatsBase.Click, btn_Stats50Min.Click, btn_Stats50Max.Click
        ChangeButtonEnabledState(sender, "btn_Stats")
        UpdateStats(DirectCast(sender, Button).Text, If(_currentPuppetSubForm, _currentPuppetForm))
    End Sub

    Private Sub ChangeButtonEnabledState(sender As Object, buttonPrefix As String)

        For Each control In Me.Controls

            If TypeOf control Is Button AndAlso DirectCast(control, Button).Name.StartsWith(buttonPrefix) AndAlso Not control Is sender Then
                DirectCast(control, Button).Enabled = True
            End If

        Next

        DirectCast(sender, Button).Enabled = False

    End Sub

    Private Sub ChangeDynamicButtonEnabledState(sender As Object, buttonPrefix As String)
        Dim button = Me.Controls.Find(DirectCast(sender, Button).Name, True).FirstOrDefault()
        If button IsNot Nothing Then button.Enabled = False
    End Sub

    Private Sub UpdateStatsAndTypes(sender As Object)

        Dim formName = DirectCast(sender, Button).Text

        Dim selectedForm As PuppetForm = Nothing

        If formName.StartsWith("Sub Form ") Then
            _currentPuppetSubForm = _currentPuppetForm.SubForms.Where(Function(x) x.Name.Equals(formName)).FirstOrDefault()
            If _currentPuppetSubForm.SubForms Is Nothing Then _currentPuppetSubForm.SubForms = _currentPuppetForm.SubForms
            selectedForm = _currentPuppetSubForm
        Else
            _currentPuppetSubForm = Nothing
            _currentPuppetForm = _puppetList.Where(Function(x) x.Name.ToLower().Equals(cmb_CharacterSelect.Text.ToLower())).FirstOrDefault().Forms.Where(Function(x) x.Name.Equals(formName)).FirstOrDefault()
            selectedForm = _currentPuppetForm
        End If

        Dim type1 = _typeList.Where(Function(x) x.Name.Equals(selectedForm.Type1)).FirstOrDefault()
        lbl_Type1.Text = selectedForm.Type1
        SetTypeLabelColor(type1, lbl_Type1)

        Dim type2 = _typeList.Where(Function(x) x.Name.Equals(selectedForm.Type2)).FirstOrDefault()
        lbl_Type2.Text = selectedForm.Type2
        SetTypeLabelColor(type2, lbl_Type2)

        UpdateStats(btn_StatsBase.Text, selectedForm)
        lbl_CostValue.Text = selectedForm.Cost

        lbl_Ability1.Text = String.Concat("Ability 1: ", selectedForm.Ability1)
        lbl_Ability2.Text = String.Concat("Ability 2: ", selectedForm.Ability2)

        RemoveControls("dynTypeLbl_")
        RemoveControls("dynAbilityTypeLbl_")
        RemoveControls("dynAbilitySubFormBtn_")

        UpdateTypeEffectiveness(type1, True)
        UpdateTypeEffectiveness(type2, False)

        AddTypeLabels()
        AddAbilityTypeLabelsAndButtons(selectedForm.Ability1, 12, 343, type1, type2, selectedForm.SubForms)
        AddAbilityTypeLabelsAndButtons(selectedForm.Ability2, 238, 343, type1, type2, selectedForm.SubForms)

    End Sub

    Private Sub UpdateStats(text As String, puppetForm As PuppetForm)

        Select Case text
            Case btn_StatsBase.Text
                UpdateStats(puppetForm.BaseHP, puppetForm.BaseFoAtk, puppetForm.BaseFoDef, puppetForm.BaseSpAtk, puppetForm.BaseSpDef, puppetForm.BaseSpd)
            Case btn_Stats50Min.Text
                UpdateStats(puppetForm.MinHP, puppetForm.MinFoAtk, puppetForm.MinFoDef, puppetForm.MinSpAtk, puppetForm.MinSpDef, puppetForm.MinSpd)
            Case btn_Stats50Max.Text
                UpdateStats(puppetForm.MaxHP, puppetForm.MaxFoAtk, puppetForm.MaxFoDef, puppetForm.MaxSpAtk, puppetForm.MaxSpDef, puppetForm.MaxSpd)
        End Select

    End Sub

    Private Sub UpdateStats(hp As Integer, foAtk As Integer, foDef As Integer, spAtk As Integer, spDef As Integer, spd As Double)

        lbl_HPValue.Text = hp
        lbl_FoAtkValue.Text = foAtk
        lbl_FoDefValue.Text = foDef
        lbl_SpAtkValue.Text = spAtk
        lbl_SpDefValue.Text = spDef
        lbl_SpdValue.Text = spd

        lbl_HPBG.Width = 242 / _maxValue * hp
        lbl_FoAtkBG.Width = 242 / _maxValue * foAtk
        lbl_FoDefBG.Width = 242 / _maxValue * foDef
        lbl_SpAtkBG.Width = 242 / _maxValue * spAtk
        lbl_SpDefBG.Width = 242 / _maxValue * spDef
        lbl_SpdBG.Width = 242 / _maxValue * spd

    End Sub

    Private Sub RemoveControls(prefix As String)

        Dim controlList As New List(Of Object)

        For Each control In Me.Controls
            If TypeOf control Is Label AndAlso DirectCast(control, Label).Name.Contains(prefix) Then controlList.Add(control)
            If TypeOf control Is Button AndAlso DirectCast(control, Button).Name.Contains(prefix) Then controlList.Add(control)
        Next

        For Each control In controlList
            Me.Controls.Remove(control)
        Next

    End Sub

    Private Sub AddTypeLabels()

        Dim column As Integer = 0
        Dim rowSpace As Integer = 0

        For Each type In _typeList.Where(Function(x) x.Effectiveness <> 1).OrderByDescending(Function(x) x.Effectiveness)

            AddLabelToForm(type.Name, type.Effectiveness, "dynTypeLbl_", 12 + (113 * column), 200 + rowSpace, type)

            column += 1

            If column Mod 4 = 0 Then

                column = 0
                rowSpace += 28

            End If

        Next

    End Sub

    Private Sub AddAbilityTypeLabelsAndButtons(abilityName As String, xPos As Integer, yPos As Integer, type1 As Type, type2 As Type, subForms As List(Of PuppetForm))

        If abilityName.Equals("Affinity Twist") Then

            Dim reversedTypeChart = New List(Of TypeChart)

            For Each typeChart In _typeChartList
                reversedTypeChart.Add(New TypeChart With {
                    .X = typeChart.X,
                    .Y = typeChart.Y,
                    .Value = typeChart.Value
                })
            Next

            For Each typeChart In reversedTypeChart

                Select Case typeChart.Value
                    Case 0
                        typeChart.Value = 1
                    Case Else
                        typeChart.Value = 1 / typeChart.Value
                End Select

            Next

            Dim typeList As New List(Of Type)
            UpdateTypeEffectiveness(type1, True, reversedTypeChart, typeList)
            UpdateTypeEffectiveness(type2, False, reversedTypeChart, typeList)

            Dim column As Integer = 0
            Dim rowSpace As Integer = 0

            For Each type In typeList.Where(Function(x) x.Effectiveness <> _typeList.Where(Function(y) y.Name.Equals(x.Name)).FirstOrDefault().Effectiveness).OrderByDescending(Function(x) x.Effectiveness)

                AddLabelToForm(type.Name, type.Effectiveness, "dynAbilityTypeLbl_", xPos + (113 * column), yPos + rowSpace, type)

                column += 1

                If column Mod 2 = 0 Then

                    column = 0
                    rowSpace += 28

                End If

            Next

        Else

            Dim ability = _abilityList.Where(Function(x) x.Name.Equals(abilityName)).FirstOrDefault()

            If ability Is Nothing Then Exit Sub

            If ability.Effectivenesses IsNot Nothing Then

                For Each effectiveness In ability.Effectivenesses
                    Dim typeEffectiveness = _typeList.Where(Function(x) x.Name.Equals(effectiveness.TypeName)).FirstOrDefault().Effectiveness
                    effectiveness.CalculatedEffectiveness = typeEffectiveness * effectiveness.Effectiveness
                Next

                Dim column As Integer = 0
                Dim rowSpace As Integer = 0

                For Each effectiveness In ability.Effectivenesses.OrderByDescending(Function(x) x.CalculatedEffectiveness)

                    Dim type = _typeList.Where(Function(x) x.Name.Equals(effectiveness.TypeName)).FirstOrDefault()
                    AddLabelToForm(effectiveness.TypeName, effectiveness.CalculatedEffectiveness, "dynAbilityTypeLbl_", xPos + (113 * column), yPos + rowSpace, type)

                    column += 1

                    If column Mod 2 = 0 Then

                        column = 0
                        rowSpace += 28

                    End If

                Next

            End If

            If ability.ShowSubForms Then

                Dim column As Integer = 0
                Dim rowSpace As Integer = 0

                For Each subForm In subForms

                    AddButtonToForm(subForm.Name, "dynAbilitySubFormBtn_", xPos + (113 * column), yPos + rowSpace)

                    column += 1

                    If column Mod 2 = 0 Then

                        column = 0
                        rowSpace += 28

                    End If

                Next

            End If

        End If

    End Sub

    Private Sub UpdateTypeEffectiveness(defendingType As Type, resetEffectiveness As Boolean)

        If defendingType Is Nothing OrElse defendingType.Name.Equals("None") Then Exit Sub

        For Each typeChart In _typeChartList.Where(Function(x) x.X = defendingType.Pos)

            Dim attackingType = _typeList.Where(Function(x) x.Pos = typeChart.Y).FirstOrDefault()

            If resetEffectiveness Then
                attackingType.Effectiveness = typeChart.Value
            Else
                attackingType.Effectiveness *= typeChart.Value
            End If

        Next

    End Sub

    Private Sub UpdateTypeEffectiveness(defendingType As Type, resetEffectiveness As Boolean, reversedTypeChart As List(Of TypeChart), ByRef typeList As List(Of Type))

        If defendingType Is Nothing OrElse defendingType.Name.Equals("None") Then Exit Sub

        For Each typeChart In reversedTypeChart.Where(Function(x) x.X = defendingType.Pos)

            Dim attackingType = _typeList.Where(Function(x) x.Pos = typeChart.Y).FirstOrDefault()
            Dim newAttackingType = typeList.Where(Function(x) x.Name.Equals(attackingType.Name)).FirstOrDefault()

            If newAttackingType Is Nothing Then

                newAttackingType = New Type With {
                    .Name = attackingType.Name,
                    .Pos = attackingType.Pos,
                    .BackColor = attackingType.BackColor,
                    .Color = attackingType.Color
                }

                typeList.Add(newAttackingType)

            End If

            If resetEffectiveness Then
                newAttackingType.Effectiveness = typeChart.Value
            Else
                newAttackingType.Effectiveness *= typeChart.Value
            End If

        Next

    End Sub

    Private Sub btn_Order_Click(sender As Object, e As EventArgs) Handles btn_Order.Click
        ChangePuppetsOrder()
    End Sub

    Private Sub ChangePuppetsOrder()

        If _sorted Then
            cmb_CharacterSelect.DataSource = _puppetList.Select(Function(x) x.Name).ToList()
            _sorted = False
        Else
            cmb_CharacterSelect.DataSource = _puppetList.Select(Function(x) x.Name).OrderBy(Function(x) x).ToList()
            _sorted = True
        End If

    End Sub

    Private Sub btn_FanCharacters_Click(sender As Object, e As EventArgs) Handles btn_FanCharacters.Click

        If btn_FanCharacters.Text.Equals("Add FanChara") Then

            Dim addToList As Boolean = False
            If _fanCharaPuppetList.Count = 0 Then addToList = True

            LoadExtendedPuppets(_extendedPuppetsSourceCode, "//html/body/div/div/div[@class='mw-content-container']/main[@id='content']/div[@id='bodyContent']/div[@id='mw-content-text']/div/div/section/article[@data-title='Shard of Dreams - Extended - FanChara -']/table/tbody/tr", addToList)
            btn_FanCharacters.Text = "No FanChara"

        Else

            For Each fanCharaPuppet In _fanCharaPuppetList
                _puppetList.Remove(_puppetList.Where(Function(x) x.Name.Equals(fanCharaPuppet)).FirstOrDefault())
            Next

            btn_FanCharacters.Text = "Add FanChara"

        End If

        LoadPuppetsIntoComboBox()

    End Sub

    Private Sub MainWindow_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub KeyDownEvent(sender As Object, e As KeyEventArgs) Handles cmb_CharacterSelect.KeyDown, btn_Order.KeyDown, btn_Form4.KeyDown, btn_Form3.KeyDown, btn_Form2.KeyDown, btn_Form1.KeyDown

        If e.KeyCode = Keys.Escape Then Me.ActiveControl = Nothing

        If sender Is cmb_CharacterSelect Then
            If e.KeyCode <> Keys.Up AndAlso e.KeyCode <> Keys.Down AndAlso cmb_CharacterSelect.DroppedDown Then cmb_CharacterSelect.DroppedDown = False
            If e.KeyCode = Keys.Enter Then ChangePupetInfo()
        End If

    End Sub

#End Region

#Region "Support Functions"

    Private Function CreatePuppetForm(name As String, columns As HtmlAgilityPack.HtmlNodeCollection) As PuppetForm

        Return New PuppetForm With {
            .Name = RemoveHTMLTagsAndNewlines(name),
            .Type1 = RemoveHTMLTagsAndNewlines(columns(2).InnerText),
            .Type2 = RemoveHTMLTagsAndNewlines(columns(3).InnerText),
            .BaseHP = columns(4).InnerText,
            .BaseFoAtk = columns(5).InnerText,
            .BaseFoDef = columns(6).InnerText,
            .BaseSpAtk = columns(7).InnerText,
            .BaseSpDef = columns(8).InnerText,
            .BaseSpd = columns(9).InnerText,
            .Cost = columns(11).InnerText,
            .MinHP = CalculateStat(True, columns(4).InnerText, 0D, 0D, 50D),
            .MinFoAtk = CalculateStat(False, columns(5).InnerText, 0D, 0D, 50D),
            .MinFoDef = CalculateStat(False, columns(6).InnerText, 0D, 0D, 50D),
            .MinSpAtk = CalculateStat(False, columns(7).InnerText, 0D, 0D, 50D),
            .MinSpDef = CalculateStat(False, columns(8).InnerText, 0D, 0D, 50D),
            .MinSpd = CalculateStat(False, columns(9).InnerText, 0D, 0D, 50D),
            .MaxHP = CalculateStat(True, columns(4).InnerText, 15D, 64D, 50D),
            .MaxFoAtk = CalculateStat(False, columns(5).InnerText, 15D, 64D, 50D, 1.1D),
            .MaxFoDef = CalculateStat(False, columns(6).InnerText, 15D, 64D, 50D, 1.1D),
            .MaxSpAtk = CalculateStat(False, columns(7).InnerText, 15D, 64D, 50D, 1.1D),
            .MaxSpDef = CalculateStat(False, columns(8).InnerText, 15D, 64D, 50D, 1.1D),
            .MaxSpd = CalculateStat(False, columns(9).InnerText, 15D, 64D, 50D, 1.1D),
            .Ability1 = RemoveHTMLTagsAndNewlines(columns(12).InnerText),
            .Ability2 = RemoveHTMLTagsAndNewlines(columns(13).InnerText)
        }

    End Function

    Private Function CalculateStat(isHP As Boolean, baseStat As Decimal, rank As Decimal, ev As Decimal, level As Decimal, Optional emblem As Decimal = 1) As Integer

        If isHP Then
            Return Math.Floor((((2D * (baseStat + rank) + ev) / 100D) + 1D) * level + 10D)
        Else
            Return Math.Floor(Math.Floor((2D * (baseStat + rank) + ev) / 100D * level + 5D) * emblem)
        End If

    End Function

    Private Sub AddLabelToForm(typeName As String, effectiveness As Double, labelPrefix As String, xPos As Integer, yPos As Integer, type As Type)

        Dim label As New Label
        label.Text = String.Concat(typeName, " x ", effectiveness)
        label.Font = lbl_Type1.Font
        label.Name = String.Concat(labelPrefix, typeName)
        label.AutoSize = False
        label.Size = New Size(100, 23)
        label.Location = New Point(xPos, yPos)
        label.TextAlign = ContentAlignment.MiddleCenter
        SetTypeLabelColor(type, label)

        Me.Controls.Add(label)

    End Sub

    Private Sub SetTypeLabelColor(type As Type, label As Label)

        If type Is Nothing OrElse type.Name.Equals("None") Then
            label.BackColor = Color.Transparent
            label.ForeColor = Color.Black
        Else
            label.BackColor = ColorTranslator.FromHtml(type.BackColor)
            label.ForeColor = ColorTranslator.FromHtml(type.Color)
        End If

    End Sub

    Private Sub AddButtonToForm(text As String, labelPrefix As String, xPos As Integer, yPos As Integer)

        Dim button As New Button
        button.Text = text
        button.Font = btn_Form1.Font
        button.FlatStyle = btn_Form1.FlatStyle
        button.Name = String.Concat(labelPrefix, text)
        button.AutoSize = False
        button.Size = New Size(100, 23)
        button.Location = New Point(xPos, yPos)
        button.TextAlign = ContentAlignment.MiddleCenter

        AddHandler button.Click, AddressOf FormButtonClickEvent

        Me.Controls.Add(button)

    End Sub

    Private Function RemoveHTMLTagsAndNewlines(str As String) As String
        Return Regex.Replace(str, "<.*?>", String.Empty).Replace(vbCrLf, Nothing).Replace(vbCr, Nothing).Replace(vbLf, Nothing)
    End Function

#End Region

End Class
