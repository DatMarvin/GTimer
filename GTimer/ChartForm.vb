Imports System.Windows.Forms.DataVisualization
Imports System.Windows.Forms.DataVisualization.Charting


Public Class ChartForm

    ReadOnly Property dll() As Utils
        Get
            Return Form1.dll
        End Get
    End Property

    Private Sub ChartForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Sub initChart()
        chart.Series.Clear()
        chart.ChartAreas.Clear()
        chart.ChartAreas.Add(New ChartArea("main"))


    End Sub

    Public Sub plot()
        initChart()
        loadConfig()
        chartIt()
        Me.Show()
        BringToFront()
    End Sub

    Public Sub plot(config As ChartConfig)
        initChart()
        loadConfig(config.user, config.games)
        chartIt()
        Me.Show()
        BringToFront()
    End Sub


    Public Sub loadConfig(selectedUser As User, selectedGames As List(Of Game))
        userCombo.Items.Clear()
        For Each u As User In Form1.users
            userCombo.Items.Add(u)
        Next
        userCombo.SelectedItem = selectedUser

        gameList.Items.Clear()
        For Each game As Game In User.getMe().games
            gameList.Items.Add(game)
        Next
        selectGames(selectedGames, True)
    End Sub
    Public Sub loadConfig()
        userCombo.Items.Clear()
        For Each user As User In Form1.users
            userCombo.Items.Add(user)
        Next
        userCombo.SelectedItem = User.getMe()

        gameList.Items.Clear()
        For Each game As Game In User.getMe().games
            gameList.Items.Add(game)
        Next
        selectGames(True)
    End Sub

    Private Sub Chart1_Click(sender As Object, e As EventArgs) Handles chart.Click

    End Sub
    Private Sub diagramButton_Click(sender As Object, e As EventArgs) Handles diagramButton.Click
        chartIt()
    End Sub

    Private Sub chartIt()
        initChart()
        '   chart.BackColor = Color.Black
        chart.BackSecondaryColor = Color.Red
        chart.BorderlineColor = Color.Gold
        chart.ForeColor = Color.White

        For Each user In Form1.users
            If user.name = userCombo.Text Then
                Dim firstDate As Date = user.getFirstLogEntry()
                Dim startDate As Date = Form1.startDate.Date

                If dll.GetDayDiff(startDate, firstDate) > 0 Then
                    startDate = firstDate
                End If

                Dim gameTimeMax As New List(Of Integer)
                Dim gameEarliest As New List(Of String)
                Dim gameLatest As New List(Of String)
                Dim sharedDates As New HashSet(Of String)
                For Each game As Game In gameList.Items

                    If Not isGameSelected(game) Then Continue For
                    If Not game.include Then Continue For
                    Dim times = game.getAllTimeKeys(user.iniPath)
                    Dim vals = game.getAllTimeValues(user.iniPath)


                    Dim series As New Series()
                    series.ChartType = SeriesChartType.Line
                    series.LegendText = game.name
                    For i = times.Count - 1 To 0 Step -1
                        Dim xDate As Date = Date.Parse(times(i))
                        Dim diff1 As Integer = dll.GetDayDiff(startDate, xDate)
                        Dim diff2 As Integer = dll.GetDayDiff(Form1.endDate.Date, xDate)
                        If diff1 < 0 OrElse diff2 > 0 Then
                            times.RemoveAt(i)
                            vals.RemoveAt(i)
                        End If
                    Next
                    For i = 0 To dll.GetDayDiff(startDate, Form1.endDate.Date)
                        If times.Count <= i OrElse Not dll.GetDayDiff(startDate.AddDays(i).Date, Date.Parse(times(i))) = 0 Then
                            times.Insert(i, startDate.AddDays(i).Date.ToShortDateString())
                            vals.Insert(i, 0)
                        End If
                    Next

                    Dim cumTime As Integer = 0
                    If radNewTime.Checked Then
                        For i = 0 To times.Count - 1
                            Dim xDate As Date = Date.Parse(times(i))
                            series.Points.Add(New DataPoint(xDate.Subtract(startDate).TotalDays, vals(i)))
                        Next
                    ElseIf radCumTime.Checked Then
                        For i = 0 To times.Count - 1
                            Dim xDate As Date = Date.Parse(times(i))
                            series.Points.Add(New DataPoint(xDate.Subtract(startDate).TotalDays, cumTime + vals(i)))
                            cumTime += vals(i)
                        Next
                    End If

                    gameEarliest.Add(times.Min(Function(t)
                                                   Return Integer.Parse(t.Substring(6) & t.Substring(3, 2) & t.Substring(0, 2))
                                               End Function))
                    gameLatest.Add(times.Max(Function(t)
                                                 Return Integer.Parse(t.Substring(6) & t.Substring(3, 2) & t.Substring(0, 2))
                                             End Function))

                    For i = 0 To times.Count - 1
                        sharedDates.Add(times(i))
                    Next

                    If radNewTime.Checked Then
                        gameTimeMax.Add(vals.Max())
                    ElseIf radCumTime.Checked Then
                        gameTimeMax.Add(cumTime)
                    End If



                    series.BorderWidth = 2
                    chart.ChartAreas(0).AxisX.LabelStyle.Angle = 0

                    chart.Series.Add(series)

                Next

                Dim tickInterval As Integer = 7
                If sharedDates.Count <= 10 Then tickInterval = 5
                If sharedDates.Count <= 7 Then tickInterval = 3
                Dim tickStepDates As Integer = Math.Max(1, sharedDates.Count / tickInterval)
                For i = 0 To sharedDates.Count - 1
                    Dim xDate As Date = Date.Parse(sharedDates(i))

                    Dim coverPrev As Integer
                    Dim coverPast As Integer
                    If i = 0 Then
                        coverPrev = dll.GetDayDiff(startDate, xDate)
                    Else
                        coverPrev = dll.GetDayDiff(Date.Parse(sharedDates(i - 1)), xDate)
                    End If
                    If i = sharedDates.Count - 1 Then
                        coverPast = dll.GetDayDiff(xDate, Form1.endDate.Date)
                    Else
                        coverPast = dll.GetDayDiff(xDate, Date.Parse(sharedDates(i + 1)))
                    End If


                    If i = 0 Or i Mod tickStepDates = 0 Then
                        chart.ChartAreas(0).AxisX.CustomLabels.Add(i - coverPrev - tickStepDates / 2, i + coverPast + tickStepDates / 2, sharedDates(i)).GridTicks = GridTickTypes.TickMark
                    End If

                Next

                If gameTimeMax.Count = 0 Then Continue For
                Dim gameTimeMaxMax As Integer = gameTimeMax.Max()
                Dim tickStep As Integer = Math.Max(1, gameTimeMaxMax / 10)
                If tickStep >= 60 * 50 Then
                    tickStep = Int(tickStep / 60) * 60
                End If
                If tickStep >= 60 * 10 Then
                    tickStep = Int(tickStep / (60 * 5)) * 60 * 5
                End If
                If tickStep >= 60 * 15 Then
                    tickStep = Int(tickStep / (60 * 15)) * 60 * 15
                End If
                For i = 0 To gameTimeMaxMax Step tickStep
                    chart.ChartAreas(0).AxisY.CustomLabels.Add(i - tickStep / 2, i + tickStep / 2, dll.SecondsTodhmsString(i)).GridTicks = GridTickTypes.TickMark
                Next

            End If
        Next
    End Sub



    Private Sub gameSelectCheck_CheckedChanged(sender As Object, e As EventArgs) Handles gameSelectCheck.CheckedChanged

    End Sub

    Private Sub selectGames(toState As Boolean)
        For i = 0 To gameList.Items.Count - 1
            gameList.SetItemChecked(i, toState)
        Next
        gameSelectCheck.Checked = toState
    End Sub
    Private Sub selectGames(selectedGames As List(Of Game), toState As Boolean)
        Dim oneUnselected As Boolean = False
        Dim oneSelected As Boolean = False
        For i = 0 To gameList.Items.Count - 1
            For j = 0 To selectedGames.Count - 1
                If selectedGames(j).user.id = gameList.Items(i).user.id And selectedGames(j).id = gameList.Items(i).id Then
                    gameList.SetItemChecked(i, toState)
                    oneSelected = True
                    Exit For
                End If
                If j = selectedGames.Count - 1 Then
                    oneUnselected = True
                End If
            Next
        Next
        If toState And Not oneUnselected Or Not toState And Not oneSelected Then
            gameSelectCheck.Checked = toState
        End If
    End Sub
    Private Sub gameSelectCheck_Click(sender As Object, e As EventArgs) Handles gameSelectCheck.Click
        selectGames(gameSelectCheck.Checked)
    End Sub

    Function isGameSelected(game As Game) As Boolean
        For i = 0 To gameList.Items.Count - 1
            If gameList.Items(i).user.name = game.user.name And gameList.Items(i).id = game.id Then
                Return gameList.GetItemChecked(i)
            End If
        Next
        Return False
    End Function

    Private Sub chart_Move(sender As Object, e As EventArgs) Handles chart.Move

    End Sub

    Private Sub chart_MouseMove(sender As Object, e As MouseEventArgs) Handles chart.MouseMove
        If chart.ChartAreas.Count > 0 And chart.Series.Count > 0 Then
            Dim x = chart.ChartAreas(0).InnerPlotPosition.X



            Dim rf = chart.ChartAreas(0).InnerPlotPosition.ToRectangleF


            Dim px = (e.X - rf.X) * chart.Series(0).Points.Count / rf.Width
            ' Text = e.X & " - " & rf.X & " -- " & px
        End If

    End Sub

    Private Sub chart_MouseDown(sender As Object, e As MouseEventArgs) Handles chart.MouseDown
        Dim res As HitTestResult = chart.HitTest(e.X, e.Y)
        If res.Series IsNot Nothing Then
            Dim x = res.Series.Points(res.PointIndex).XValue
            Dim y = res.Series.Points(res.PointIndex).YValues(0)
            Dim firstDate As Date = Form1.startDate.Date
            Dim currDate As Date = firstDate.AddDays(res.PointIndex)
            Text = res.Series.LegendText & ": " & x & " " & y & " dt: " & currDate & " - val: " & dll.SecondsTohmsString(y)
        End If

    End Sub

    Public Structure ChartConfig

        Public user As User
        Public games As List(Of Game)
        Public plotMode As String



    End Structure
End Class