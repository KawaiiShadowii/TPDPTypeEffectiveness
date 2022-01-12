Imports System.IO
Imports System.Text.RegularExpressions

Public Class MainWindow

    Private _puppetList As New List(Of Puppet)
    Private _maxValue As Integer
    Private _types As New List(Of Type)
    Private _typeChart As New List(Of Typechart)
    Private _sorted As Boolean = False

    Private Sub MainWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim puppetsSourceCode As String = DownloadSource("http://tpdpwiki.net/wiki/Puppetdex")
        LoadPuppets(puppetsSourceCode, "//html/body/div[@id='content']/div[@id='bodyContent']/div[@id='mw-content-text']/div/div/div[@title='SoD 1.103']/table/tbody/tr")

        Dim extendedPuppetsSourceCode As String = DownloadSource("http://en.tpdpwiki.net/wiki/Mod:Mod_Puppetdex")
        LoadExtendedPuppets(extendedPuppetsSourceCode, "//html/body/div[@id='content']/div[@id='bodyContent']/div[@id='mw-content-text']/div/div/div[@title='Shard of Dreams - Extended -']/table/tbody/tr")

        SetMaxValue()

        Dim typesSourceCode As String = DownloadSource("http://tpdpwiki.net/wiki/Type_Chart")
        LoadTypesAndTypeChart(typesSourceCode, "//html/body/div[@id='content']/div[@id='bodyContent']/div[@id='mw-content-text']/div/div/div[@title='SoD 1.013']/table[@class='wikitable floatleft']/tbody/tr")

        LoadPuppetsIntoComboBox()

    End Sub

    Private Function DownloadSource(url As String) As String

        Using process As New Process()

            Dim processStartInfo As New ProcessStartInfo("curl", url)
            processStartInfo.UseShellExecute = False
            processStartInfo.RedirectStandardOutput = True
            processStartInfo.CreateNoWindow = True
            process.StartInfo = processStartInfo
            process.Start()

            Using reader As StreamReader = process.StandardOutput
                Return reader.ReadToEnd()
            End Using

        End Using

    End Function

    Private Sub LoadPuppets(sourceCode As String, rowsSelector As String)

        Dim doc As New HtmlAgilityPack.HtmlDocument
        doc.LoadHtml(sourceCode)

        Dim rows = doc.DocumentNode.SelectNodes(rowsSelector)
        rows.RemoveAt(0)
        rows.RemoveAt(UBound(rows.ToArray()))

        For Each row In rows

            Dim columns = row.SelectNodes("td")

            Dim formName = RemoveHTMLTagsAndNewlines(columns(1).InnerText)
            Dim puppetFormName = formName.Split(" "c)
            Dim puppetForm = puppetFormName.FirstOrDefault()
            Dim puppetName = puppetFormName.LastOrDefault()

            If _puppetList.Any(Function(x) x.Name.Equals(puppetName)) Then

                _puppetList.Where(Function(x) x.Name.Equals(puppetName)).FirstOrDefault().Forms.Add(CreatePuppetForm(puppetForm, columns))

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

    Private Sub LoadExtendedPuppets(sourceCode As String, rowsSelector As String)

        Dim doc As New HtmlAgilityPack.HtmlDocument
        doc.LoadHtml(sourceCode)

        Dim rows = doc.DocumentNode.SelectNodes(rowsSelector)
        rows.RemoveAt(0)
        rows.RemoveAt(UBound(rows.ToArray()))

        For Each row In rows

            Dim tableHeader = row.SelectSingleNode("th")

            If tableHeader IsNot Nothing Then

                _puppetList.Add(New Puppet With {
                    .Name = RemoveHTMLTagsAndNewlines(tableHeader.InnerText),
                    .Forms = New List(Of PuppetForm)
                })

            End If

            Dim columns = row.SelectNodes("td")

            Dim puppetForm = columns(1).InnerText
            _puppetList.LastOrDefault().Forms.Add(CreatePuppetForm(puppetForm, columns))

        Next

    End Sub

    Private Sub SetMaxValue()

        Dim values As New List(Of Integer)

        values.Add(_puppetList.Select(Function(x) x.Forms.Select(Function(y) y.HP).Max()).Max())
        values.Add(_puppetList.Select(Function(x) x.Forms.Select(Function(y) y.FoAtk).Max()).Max())
        values.Add(_puppetList.Select(Function(x) x.Forms.Select(Function(y) y.FoDef).Max()).Max())
        values.Add(_puppetList.Select(Function(x) x.Forms.Select(Function(y) y.SpAtk).Max()).Max())
        values.Add(_puppetList.Select(Function(x) x.Forms.Select(Function(y) y.SpDef).Max()).Max())
        values.Add(_puppetList.Select(Function(x) x.Forms.Select(Function(y) y.Spd).Max()).Max())
        values.Add(_puppetList.Select(Function(x) x.Forms.Select(Function(y) y.Cost).Max()).Max())

        _maxValue = values.Max(Function(x) x)

    End Sub

    Private Sub LoadTypesAndTypeChart(sourceCode As String, rowsSelector As String)

        Dim doc As New HtmlAgilityPack.HtmlDocument
        doc.LoadHtml(sourceCode)

        Dim rows = doc.DocumentNode.SelectNodes(rowsSelector)
        rows.RemoveAt(0)

        Dim rowPos As Integer = 0
        Dim columnPos As Integer = 0

        For Each row In rows

            Dim type = row.SelectSingleNode("th")
            Dim typeStyles = type.Attributes.AttributesWithName("style").FirstOrDefault().Value.Split(";"c)
            Dim typeBackColor = typeStyles(0).Substring(typeStyles(0).LastIndexOf("#"c), 7)
            Dim typeColor = typeStyles(1).Substring(typeStyles(1).LastIndexOf("#"c), 7)

            _types.Add(New Type With {
                .Name = RemoveHTMLTagsAndNewlines(type.InnerText),
                .Pos = rowPos,
                .BackColor = typeBackColor,
                .Color = typeColor
            })

            columnPos = 0

            For Each column In row.SelectNodes("td")

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

                _typeChart.Add(New Typechart With {
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
        cmb_CharacterSelect.DataSource = _puppetList.Select(Function(x) x.Name).ToList()
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

    Private Function CreatePuppetForm(name As String, columns As HtmlAgilityPack.HtmlNodeCollection) As PuppetForm

        Return New PuppetForm With {
            .Name = RemoveHTMLTagsAndNewlines(name),
            .Type1 = RemoveHTMLTagsAndNewlines(columns(2).InnerText),
            .Type2 = RemoveHTMLTagsAndNewlines(columns(3).InnerText),
            .HP = columns(4).InnerText,
            .FoAtk = columns(5).InnerText,
            .FoDef = columns(6).InnerText,
            .SpAtk = columns(7).InnerText,
            .SpDef = columns(8).InnerText,
            .Spd = columns(9).InnerText,
            .Cost = columns(11).InnerText
        }

    End Function

    Private Sub cmb_CharacterSelect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_CharacterSelect.SelectedIndexChanged

        Dim puppet = _puppetList.Where(Function(x) x.Name.Equals(cmb_CharacterSelect.Text)).FirstOrDefault()

        btn_Form1.Text = puppet.Forms(0).Name
        btn_Form2.Text = puppet.Forms(1).Name
        btn_Form3.Text = puppet.Forms(2).Name
        btn_Form4.Text = puppet.Forms(3).Name

        btn_Form1.Enabled = False
        btn_Form2.Enabled = True
        btn_Form3.Enabled = True
        btn_Form4.Enabled = True

        btn_Form1_Click(btn_Form1, e)

    End Sub

    Private Sub btn_Form1_Click(sender As Object, e As EventArgs) Handles btn_Form1.Click, btn_Form2.Click, btn_Form3.Click, btn_Form4.Click
        ChangeButtonEnabledState(sender)
        UpdateValues(sender)
    End Sub

    Private Sub ChangeButtonEnabledState(sender As Object)

        For Each control In Me.Controls

            If TypeOf control Is Button AndAlso Not control Is sender Then
                DirectCast(control, Button).Enabled = True
            End If

        Next

        DirectCast(sender, Button).Enabled = False

    End Sub

    Private Sub UpdateValues(sender As Object)

        Dim formName = DirectCast(sender, Button).Text
        Dim puppetForm = _puppetList.Where(Function(x) x.Name.Equals(cmb_CharacterSelect.Text)).FirstOrDefault().Forms.Where(Function(x) x.Name.Equals(formName)).FirstOrDefault()

        Dim type1 = _types.Where(Function(x) x.Name.Equals(puppetForm.Type1)).FirstOrDefault()
        lbl_Type1.Text = puppetForm.Type1
        SetPuppetLabelColor(type1, lbl_Type1)

        Dim type2 = _types.Where(Function(x) x.Name.Equals(puppetForm.Type2)).FirstOrDefault()
        lbl_Type2.Text = puppetForm.Type2
        SetPuppetLabelColor(type2, lbl_Type2)

        lbl_HPValue.Text = puppetForm.HP
        lbl_FoAtkValue.Text = puppetForm.FoAtk
        lbl_FoDefValue.Text = puppetForm.FoDef
        lbl_SpAtkValue.Text = puppetForm.SpAtk
        lbl_SpDefValue.Text = puppetForm.SpDef
        lbl_SpdValue.Text = puppetForm.Spd
        lbl_CostValue.Text = puppetForm.Cost

        lbl_HPBG.Width = 356 / _maxValue * puppetForm.HP
        lbl_FoAtkBG.Width = 356 / _maxValue * puppetForm.FoAtk
        lbl_FoDefBG.Width = 356 / _maxValue * puppetForm.FoDef
        lbl_SpAtkBG.Width = 356 / _maxValue * puppetForm.SpAtk
        lbl_SpDefBG.Width = 356 / _maxValue * puppetForm.SpDef
        lbl_SpdBG.Width = 356 / _maxValue * puppetForm.Spd

        RemoveTypeLabels()

        UpdateTypeEffectiveness(type1, True)
        UpdateTypeEffectiveness(type2, False)

        AddTypeLabels()

    End Sub

    Private Sub SetPuppetLabelColor(type As Type, label As Label)

        If type Is Nothing OrElse type.Name.Equals("None") Then
            label.BackColor = Color.Transparent
            label.ForeColor = Color.Black
        Else
            label.BackColor = ColorTranslator.FromHtml(type.BackColor)
            label.ForeColor = ColorTranslator.FromHtml(type.Color)
        End If

    End Sub

    Private Sub UpdateTypeEffectiveness(defendingType As Type, resetEffectiveness As Boolean)

        If defendingType Is Nothing OrElse defendingType.Name.Equals("None") Then Exit Sub

        For Each typeChart In _typeChart.Where(Function(x) x.X = defendingType.Pos)

            Dim attackingType = _types.Where(Function(x) x.Pos = typeChart.Y).FirstOrDefault()

            If resetEffectiveness Then
                attackingType.Effectiveness = typeChart.Value
            Else
                attackingType.Effectiveness *= typeChart.Value
            End If

        Next

    End Sub

    Private Sub RemoveTypeLabels()

        For Each type In _types.Where(Function(x) x.Effectiveness <> 1).OrderByDescending(Function(x) x.Effectiveness)
            Dim control = Me.Controls.Find(String.Concat("dynLbl_", type.Name), True).FirstOrDefault()
            Me.Controls.Remove(control)
        Next

    End Sub

    Private Sub AddTypeLabels()

        Dim column As Integer = 0
        Dim rowSpace As Integer = 0

        For Each type In _types.Where(Function(x) x.Effectiveness <> 1).OrderByDescending(Function(x) x.Effectiveness)

            Dim label As New Label
            label.Text = String.Concat(type.Name, " x ", type.Effectiveness)
            label.Font = lbl_Type1.Font
            label.Name = String.Concat("dynLbl_", type.Name)
            label.AutoSize = False
            label.Size = New Size(100, 23)
            label.Location = New Point(12 + (113 * column), 200 + rowSpace)
            label.TextAlign = ContentAlignment.MiddleCenter
            label.BackColor = ColorTranslator.FromHtml(type.BackColor)
            label.ForeColor = ColorTranslator.FromHtml(type.Color)

            Me.Controls.Add(label)

            column += 1

            If column Mod 4 = 0 Then

                column = 0
                rowSpace += 28

            End If

        Next

    End Sub

    Private Sub btn_Order_Click(sender As Object, e As EventArgs) Handles btn_Order.Click
        ChangePuppetsOrder()
    End Sub

    Private Sub MainWindow_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub KeyDownEvent(sender As Object, e As KeyEventArgs) Handles cmb_CharacterSelect.KeyDown, btn_Order.KeyDown, btn_Form4.KeyDown, btn_Form3.KeyDown, btn_Form2.KeyDown, btn_Form1.KeyDown
        If e.KeyCode = Keys.Escape Then Me.ActiveControl = Nothing
    End Sub

    Private Function RemoveHTMLTagsAndNewlines(str As String) As String
        Return Regex.Replace(str, "<.*?>", String.Empty).Replace(vbCrLf, Nothing).Replace(vbCr, Nothing).Replace(vbLf, Nothing)
    End Function

End Class
