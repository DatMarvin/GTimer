Imports System.Windows.Forms.DataVisualization
Imports System.Windows.Forms.DataVisualization.Charting


Public Class ChartForm

    ReadOnly Property dll() As Utils
        Get
            Return Form1.dll
        End Get
    End Property

    Private Sub ChartForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        initChart()
    End Sub

    Public Sub initChart()
        chart.Series.Clear()
        chart.ChartAreas.Clear()
        chart.ChartAreas.Add(New ChartArea("main"))
    End Sub

    Private Sub Chart1_Click(sender As Object, e As EventArgs) Handles chart.Click

    End Sub

    Private Sub chartIt()
        initChart()
        '   chart.BackColor = Color.Black
        chart.BackSecondaryColor = Color.Red
        chart.BorderlineColor = Color.Gold
        chart.ForeColor = Color.White

        For Each user In Form1.users
            If user.name = ComboBox1.Text Then
                Dim firstDate As Date = user.getFirstLogEntry()
                Dim startDate As Date = Form1.startDate.Date

                If dll.GetDayDiff(startDate, firstDate) > 0 Then
                    startDate = firstDate
                End If

                Dim gameTimeMax As New List(Of Integer)
                Dim gameEarliest As New List(Of String)
                Dim gameLatest As New List(Of String)
                Dim sharedDates As New HashSet(Of String)
                For Each game In user.games

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

    Private Sub Button1_Click(sender As Object, e As EventArgs)



    End Sub

    Sub setCustomXLabels(times As List(Of String), series As Series)


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        initChart()
    End Sub

    Private Sub diagramButton_Click(sender As Object, e As EventArgs) Handles diagramButton.Click
        chartIt()
    End Sub
End Class