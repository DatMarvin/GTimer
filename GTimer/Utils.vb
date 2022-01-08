
Option Explicit On

Imports System.Net.Sockets
Imports System.Threading
Imports System.ServiceProcess
Imports System.Runtime.InteropServices
Imports System.IO
Imports System.IO.Compression
Imports System.Security.Cryptography
Imports System.Text
Imports System.Net.Mail
Imports System.Net



Public Class Utils



    Private Function isFileOpen(filePath As String) As Integer
        Dim file As New FileInfo(filePath)
        Dim stream As FileStream
        Try
            stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None)
            stream.Close()
            Return 0
        Catch ex As Exception
            If TypeOf ex Is IOException AndAlso isFileLocked(ex) Then
                Return 1
            Else
                Return 2
            End If
        End Try
    End Function

    Private Shared Function isFileLocked(exception As Exception) As Boolean
        Dim errorCode As Integer = Marshal.GetHRForException(exception) And ((1 << 16) - 1)
        Return errorCode = 32 OrElse errorCode = 33
    End Function

    Function dateNowFormat() As String
        Return Now.ToShortDateString() & " " & Now.ToShortTimeString() & ":" & Now.Second.ToString().PadLeft(2, "0")
    End Function
    Function isMe() As Boolean
        Return Environment.MachineName = "MARVIN-PC"
    End Function
    Function getByteFormatString(bytes As Long) As String
        Dim kb As Long = CDbl(bytes / 1024)
        If kb < 1 Then Return CStr(bytes) & " B"
        Dim mb As Long = CDbl(kb / 1024)
        If mb < 1 Then Return CStr(Math.Round(kb, 2)) & " KB"
        Dim gb As Long = CDbl(mb / 1024)
        If gb < 1 Then Return CStr(Math.Round(mb, 2)) & " MB"
        Return CStr(Math.Round(gb, 2)) & " GB"
    End Function
    Function getBinaryComponents(ByVal number As Integer) As List(Of Integer)
        Dim res As New List(Of Integer)
        If number <= 0 Then Return New List(Of Integer) From {1}
        Dim highestPot As Integer = Int(Math.Log(number, 2))
        For i = highestPot To 0 Step -1
            If 2 ^ i <= number Then
                res.Add(2 ^ i)
                number -= 2 ^ i
            End If
        Next
        Return res
    End Function

    Function rndIntOrder(ByVal ints() As Integer) As Integer()
        Dim res() As Integer = Nothing
        Dim con() As Integer = Nothing
        Dim rnd As New Random
        Dim r As Integer = rnd.Next(0, ints.Length)
        ExtendArray(res, ints(r))
        ExtendArray(con, r)
        Do
1:          r = rnd.Next(0, ints.Length)
            If con.Contains(r) Then GoTo 1
            ExtendArray(res, ints(r))
            ExtendArray(con, r)
        Loop Until res.Length = ints.Length
        Return res
    End Function
    Function intSequenceToArray(ByVal st As Integer, ByVal en As Integer) As Integer()
        Dim res() As Integer = Nothing
        For i = st To en
            ExtendArray(res, i)
        Next
        Return res
    End Function

    Declare Function URLDownloadToFile Lib "urlmon" Alias "URLDownloadToFileA" (ByVal pCaller As Integer, ByVal szURL As String, ByVal szFileName As String, ByVal dwReserved As Integer, ByVal lpfnCB As Integer) As Long
    Public Function DownloadFile(ByVal URL As String, ByVal LocalFilename As String) As Boolean
        Dim lngRetVal As Long
        lngRetVal = URLDownloadToFile(0, URL, LocalFilename, 0, 0)
        If lngRetVal = 0 Then Return True
        Return False
    End Function

    Function getRandomString(ByVal minlen As Integer, ByVal maxlen As Integer, ByVal dig As Boolean, ByVal low As Boolean, ByVal upp As Boolean) As String
        Dim rnd As New Random
        Dim len As Integer = rnd.Next(minlen, maxlen + 1)
        Dim s As String = ""
        For j = 0 To len - 1
            Dim curr As Integer
            Do
                curr = rnd.Next(48, 123)
            Loop Until curr < 58 And dig Or upp And curr > 64 And curr < 91 Or low And curr > 96
            s &= Chr(curr)
        Next
        Return s
    End Function

    Function ParserDouble(ByVal str As String, Optional ByVal signed As Boolean = True) As Double()
        Dim redbls() As Double = Nothing
        If Not str = "" Then
            For i = 0 To str.Length - 1
                Dim ch As String = str(i)
                Dim curr As String = ""
                Dim n As Integer = 0
                Dim sign As Double = 1.0
                Dim dot As Boolean = False
                If Char.IsDigit(ch) Then
                    If Not i = 0 Then
                        If str(i - 1) = "-" And signed = True Then sign = -1.0
                    End If
                    Do While i + n < str.Length
                        If Char.IsDigit(str(i + n)) Then
                            curr &= str(i + n)
                            n += 1
                        ElseIf str(i + n) = "." And dot = False Then
                            dot = True
                            curr &= ","
                            n += 1
                        Else
                            Exit Do
                        End If
                    Loop
                    curr = CDbl(curr)
                    curr = curr * sign
                    ExtendArray(redbls, curr)
                    i += n
                End If
            Next
        End If
        Return redbls
    End Function
    Function ParserInt(ByVal str As String, Optional ByVal signed As Boolean = True) As Integer()
        Dim reints() As Integer = Nothing
        If Not str = "" Then
            For i = 0 To str.Length - 1
                Dim ch As String = str(i)
                Dim currint As String = ""
                Dim n As Integer = 0
                Dim sign As Integer = 1
                If Char.IsDigit(ch) Then
                    If Not i = 0 Then
                        If str(i - 1) = "-" And signed = True Then sign = -1
                    End If
                    Do While i + n < str.Length
                        If Char.IsDigit(str(i + n)) Then
                            currint &= str(i + n) : n += 1
                        Else : Exit Do
                        End If
                    Loop
                    ExtendArray(reints, currint * sign)
                    i += n
                End If
            Next
        End If
        Return reints
    End Function

    Sub ExtendArray(ByRef ds() As Double, Optional ByVal value As Double = 0)
        If IsNothing(ds) Then
            ReDim ds(0)
            ds(0) = value
        Else
            ReDim Preserve ds(ds.Length)
            ds(ds.Length - 1) = value
        End If
    End Sub
    Sub ExtendArray(ByRef ints() As Integer, Optional ByVal value As Integer = 0)
        If IsNothing(ints) Then
            ReDim ints(0)
            ints(0) = value
        Else
            ReDim Preserve ints(ints.Length)
            ints(ints.Length - 1) = value
        End If
    End Sub
    Sub ExtendArray(ByRef strs() As String, Optional ByVal value As String = "")
        If IsNothing(strs) Then
            ReDim strs(0)
            strs(0) = value
        Else
            ReDim Preserve strs(strs.Length)
            strs(strs.Length - 1) = value
        End If
    End Sub

    Function GetDayDiff(ByVal dt1 As String, ByVal dt2 As String) As Double
        Dim d1 As Date = Date.Parse(dt1)
        Dim d2 As Date = Date.Parse(dt2)
        Return d2.Subtract(d1).TotalDays
    End Function
    Function GetDayDiff(ByVal dt1 As Date, ByVal dt2 As Date) As Double
        Dim diff As Integer = dt2.Subtract(dt1).TotalDays
        Return diff
    End Function
    Function FractionToDateString(ByVal d As Integer, ByVal m As Integer, ByVal y As Integer) As String
        Return CStr(d).PadLeft(2, "0") & "." & CStr(m).PadLeft(2, "0") & "." & CStr(y).PadLeft(4, "0")
    End Function

    Function IntToDayOfWeek(ByVal d As Integer) As String
        Select Case d
            Case 1
                Return "Monday"
            Case 2
                Return "Tuesday"
            Case 3
                Return "Wednesday"
            Case 4
                Return "Thursday"
            Case 5
                Return "Friday"
            Case 6
                Return "Saturday"
            Case 7
                Return "Sunday"
            Case Else
                Return "Invalid Day"
        End Select
    End Function

    Function RndToAlph(ByVal r As Integer, Optional ByVal lower0upper1rndupper2 As Integer = 0) As String
        Dim tempstr As String = ""
        Dim rnd As Random = New Random
        Select Case r
            Case 1 : tempstr = "a" : Case 2 : tempstr = "b" : Case 3 : tempstr = "c" : Case 4 : tempstr = "d" : Case 5 : tempstr = "e" : Case 6 : tempstr = "f" : Case 7 : tempstr = "g" : Case 8 : tempstr = "h" : Case 9 : tempstr = "i" : Case 10 : tempstr = "j" : Case 11 : tempstr = "k" : Case 12 : tempstr = "l" : Case 13 : tempstr = "m" : Case 14 : tempstr = "n" : Case 15 : tempstr = "o" : Case 16 : tempstr = "p" : Case 17 : tempstr = "q" : Case 18 : tempstr = "r" : Case 19 : tempstr = "s" : Case 20 : tempstr = "t" : Case 21 : tempstr = "u" : Case 22 : tempstr = "v" : Case 23 : tempstr = "w" : Case 24 : tempstr = "x" : Case 25 : tempstr = "y" : Case 26 : tempstr = "z"
            Case Else : Return r
        End Select
        If lower0upper1rndupper2 = 1 Then
            tempstr = tempstr.ToUpper
        ElseIf lower0upper1rndupper2 = 2 Then
            Dim rndint As Integer = rnd.Next(0, 2)
            If rndint = 0 Then
                tempstr = tempstr.ToUpper
            End If
        End If
        Return tempstr
    End Function

    Function DirToCount(ByVal dir As Integer, ByVal fnum As Integer, Optional ByVal num As Integer = 1) As Integer
        Select Case dir
            Case 1 : Return -1 * num
            Case 2 : Return 1 * num
            Case 3 : Return -fnum * num
            Case 4 : Return fnum * num
            Case 5 : Return (-fnum - 1) * num
            Case 6 : Return (-fnum + 1) * num
            Case 7 : Return (fnum - 1) * num
            Case 8 : Return (fnum + 1) * num
            Case Else : Return 0
        End Select
    End Function

    Function ReverseDir(ByVal dir As Integer) As Integer
        Select Case dir
            Case 1 : Return 2
            Case 2 : Return 1
            Case 3 : Return 4
            Case 4 : Return 3
            Case 5 : Return 8
            Case 6 : Return 7
            Case 7 : Return 6
            Case 8 : Return 5
            Case Else : Return 0
        End Select
    End Function

    Function dirToFactor(ByVal dir As Integer) As Integer()
        Select Case dir
            Case 1 : Return {-1, 0}
            Case 2 : Return {1, 0}
            Case 3 : Return {0, -1}
            Case 4 : Return {0, 1}
            Case 5 : Return {-1, -1}
            Case 6 : Return {1, -1}
            Case 7 : Return {1, -1}
            Case 8 : Return {1, 1}
            Case Else : Return {0, 0}
        End Select
    End Function

    Function ColorBlackWhite(ByVal count As Integer, ByVal curr As Integer, Optional ByVal inverted As Boolean = False) As Integer()
        If count >= 2 Then
            Dim c As Integer
            If inverted = False Then
                c = (curr - 1) * (255 / (count - 1))
            Else
                c = 255 - ((curr - 1) * (255 / (count - 1)))
            End If
            Return New Integer() {c, c, c}
        End If
        Return Nothing
    End Function


    Function ColorThermal(ByVal count As Integer, ByVal curr As Integer) As Integer()
        Dim r As Integer : Dim g As Integer : Dim b As Integer
        If count >= 2 Then
            If curr <= count / 5 Then
                r = (255 - ((curr - 1) * 255) / Int(count / (5)))
            ElseIf curr > count / 5 And curr <= count / (5 / 3) Then
                r = 0
            ElseIf curr > count / (5 / 3) And curr <= count / (5 / 4) Then
                r = (curr - Int(count / (5 / 3))) * (255 / (Int(count / (5 / 4)) - Int(count / (5 / 3))))
            ElseIf curr > count / (5 / 4) Then
                r = 255
            End If

            If curr <= count / 5 Then
                g = 0
            ElseIf curr > count / 5 And curr <= count / (5 / 2) Then
                g = (curr - Int(count / (5))) * (255 / (Int(count / (5 / 2)) - Int(count / (5))))
            ElseIf curr > count / (5 / 2) And curr <= count / (5 / 4) Then
                g = 255
            ElseIf curr > count / (5 / 4) Then
                g = (255 - ((curr - 1 - Int(count / (5 / 4))) * 255) / Int(count / 5))
            End If

            If curr <= count / (5 / 2) Then
                b = 255
            ElseIf curr > count / (5 / 2) And curr <= count / (5 / 3) Then
                b = (255 - ((curr - 1 - Int(count / (5 / 2))) * 255) / Int(count / (5)))
            ElseIf curr > count / (5 / 3) Then
                b = 0
            End If
            Return New Integer() {r, g, b}
        ElseIf count = 1 Then

            If curr <= -20 Then
                r = 200
            ElseIf curr > -20 And curr <= -10 Then
                r = 200 * ((-10 - curr) / (-10 - -20))
            ElseIf curr > -10 And curr <= 10 Then
                r = 0
            ElseIf curr > 10 And curr <= 20 Then
                r = 255 * ((curr - 10) / (20 - 10))
            ElseIf curr > 20 Then
                r = 255
            End If

            If curr <= -20 Then
                g = 0
            ElseIf curr > -20 And curr <= -10 Then
                g = 0
            ElseIf curr > -10 And curr <= 0 Then
                g = 255 * ((curr - -10) / (0 - -10))
            ElseIf curr > 0 And curr <= 20 Then
                g = 255
            ElseIf curr > 20 And curr <= 30 Then
                g = 255 * ((30 - curr) / (30 - 20))
            ElseIf curr > 30 Then
                g = 0
            End If

            If curr <= 0 Then
                b = 255
            ElseIf curr > 0 And curr <= 10 Then
                b = 255 * ((10 - curr) / (10 - 0))
            ElseIf curr > 10 Then
                b = 0
            End If
            Return New Integer() {r, g, b}
        End If
        Return Nothing
    End Function

    Function IsValid(ByVal curr As Integer, ByVal dir As Integer, ByVal fnum As Integer, Optional ByVal num As Integer = 1) As Boolean
        Select Case dir
            Case 1
                If Int((curr - 1) / fnum) = Int((curr - 1 - num) / fnum) Then
                    Return True
                End If
            Case 2
                If Int((curr - 1) / fnum) = Int((curr - 1 + num) / fnum) Then
                    Return True
                End If
            Case 3
                If CInt(curr - fnum * num) > 0 Then
                    Return True
                End If
            Case 4
                If CInt(curr + fnum * num) <= fnum * fnum Then
                    Return True
                End If
            Case 5
                If CInt(curr - fnum * num) > 0 Then
                    If Int(CInt(curr - 1) / fnum) = Int(CInt(curr - 1 - num) / fnum) Then
                        Return True
                    End If
                End If
            Case 6
                If CInt(curr - fnum * num) > 0 Then
                    If Int(CInt(curr - 1) / fnum) = Int(CInt(curr - 1 + num) / fnum) Then
                        Return True
                    End If
                End If

            Case 7
                If CInt(curr + fnum * num) <= fnum * fnum Then
                    If Int(CInt(curr - 1) / fnum) = Int(CInt(curr - 1 - num) / fnum) Then
                        Return True
                    End If
                End If
            Case 8
                If CInt(curr + fnum * num) <= fnum * fnum Then
                    If Int(CInt(curr - 1) / fnum) = Int(CInt(curr - 1 + num) / fnum) Then
                        Return True
                    End If
                End If
        End Select
        Return False
    End Function
    Function isValidRect(ByVal curr As Integer, ByVal dir As Integer, ByVal fnumx As Integer, ByVal fnumy As Integer, Optional ByVal num As Integer = 1) As Boolean
        Dim tempbln As Boolean = False
        Select Case dir
            Case 1
                If Int((curr - 1) / fnumx) = Int((curr - 1 - num) / fnumx) Then
                    tempbln = True
                End If
            Case 2
                If Int((curr - 1) / fnumx) = Int((curr - 1 + num) / fnumx) Then
                    tempbln = True
                End If
            Case 3
                If CInt(curr - fnumx * num) > 0 Then
                    tempbln = True
                End If
            Case 4
                If CInt(curr + fnumx * num) <= fnumx * fnumy Then
                    tempbln = True
                End If
            Case 5
                If CInt(curr - fnumx * num) > 0 Then
                    If Int(CInt(curr - 1) / fnumx) = Int(CInt(curr - 1 - num) / fnumx) Then
                        tempbln = True
                    End If
                End If
            Case 6
                If CInt(curr - fnumx * num) > 0 Then
                    If Int(CInt(curr - 1) / fnumx) = Int(CInt(curr - 1 + num) / fnumx) Then
                        tempbln = True
                    End If
                End If

            Case 7
                If CInt(curr + fnumx * num) <= fnumx * fnumy Then
                    If Int(CInt(curr - 1) / fnumx) = Int(CInt(curr - 1 - num) / fnumx) Then
                        tempbln = True
                    End If
                End If
            Case 8
                If CInt(curr + fnumx * num) <= fnumx * fnumy Then
                    If Int(CInt(curr - 1) / fnumx) = Int(CInt(curr - 1 + num) / fnumx) Then
                        tempbln = True
                    End If
                End If
        End Select
        Return tempbln
    End Function

    Function IntToMonth(ByVal m As Integer) As String
        Select Case m
            Case 1 : Return "January" : Case 2 : Return "February" : Case 3 : Return "March" : Case 4 : Return "April" : Case 5 : Return "May" : Case 6 : Return "June" : Case 7 : Return "July" : Case 8 : Return "August" : Case 9 : Return "September" : Case 10 : Return "October" : Case 11 : Return "Novermber" : Case 12 : Return "December" : Case Else : Return "Invalid"
        End Select
    End Function

    Function ReverseColor(ByVal r As Integer, ByVal g As Integer, ByVal b As Integer) As Integer
        If r + g + b < 450 And r < 175 And r + g < 225 And b + g < 325 And r <= 175 Then
            Return -16777217
        Else
            Return 0
        End If
    End Function

    Function IntBetweenRange(ByVal curr As Integer, ByVal min As Integer, ByVal max As Integer) As Boolean
        If curr >= min And curr <= max Then
            Return True
        Else : Return False
        End If
    End Function

    Function SortDates(ByVal dt() As String) As String()
        For i = 0 To dt.Length - 1
            If dt(i).Length = 10 Then
                dt(i) = Mid(dt(i), 7, 4) & "." & Mid(dt(i), 4, 2) & "." & Mid(dt(i), 1, 2)
            ElseIf dt(i).Length > 10 Then
                dt(i) = Mid(dt(i), 7, 4) & "." & Mid(dt(i), 4, 2) & "." & Mid(dt(i), 1, 2) & Mid(dt(i), 11)
            End If
        Next
        Array.Sort(dt)
        For i = 0 To dt.Length - 1
            If Not dt(i) = Nothing Then
                If dt(i).Length = 10 Then
                    dt(i) = Mid(dt(i), 9, 2) & "." & Mid(dt(i), 6, 2) & "." & Mid(dt(i), 1, 4)
                ElseIf dt(i).Length > 10 Then
                    dt(i) = Mid(dt(i), 9, 2) & "." & Mid(dt(i), 6, 2) & "." & Mid(dt(i), 1, 4) & Mid(dt(i), 11)
                End If
            End If
        Next
        Return dt
    End Function

    Function SecondsTodhmsString(ByVal s As Integer, Optional defaultIfZero As String = "", Optional padLeft As Boolean = False) As String
        If s <= 0 Then Return defaultIfZero
        Dim d As Integer = Int(Int(Int(s / 60) / 60) / 24)
        Dim h As Integer = Int(Int(s / 60) / 60) Mod 24
        Dim m As Integer = Int(s / 60) Mod 60
        Return IIf(d > 0, CStr(d) & "d ", IIf(padLeft, "   ", "")) &
                IIf(h > 0 Or d > 0, CStr(h).PadLeft(2, "0") & "h ", IIf(padLeft, "    ", "")) &
                IIf(m > 0 Or h > 0 Or d > 0, CStr(m).PadLeft(2, "0") & "m ", IIf(padLeft, "    ", "")) &
                 CStr(Int((s Mod 3600) Mod 60)).PadLeft(2, "0") & "s"
    End Function

    Function SecondsTohmsString(ByVal s As Integer, Optional defaultIfZero As String = "", Optional padLeft As Boolean = False) As String
        If s <= 0 Then Return defaultIfZero
        Dim h As Integer = Int(Int(s / 60) / 60)
        Dim m As Integer = Int(s / 60) Mod 60
        Return IIf(h > 0, CStr(h).PadLeft(2, "0") & "h ", IIf(padLeft, "    ", "")) &
                IIf(m > 0 Or h > 0, CStr(m).PadLeft(2, "0") & "m ", IIf(padLeft, "    ", "")) &
                 CStr(Int((s Mod 3600) Mod 60)).PadLeft(2, "0") & "s"
    End Function

    Function SecondsTomsString(ByVal s As Integer, Optional defaultIfZero As String = "", Optional padLeft As Boolean = False) As String
        If s <= 0 Then Return defaultIfZero
        Dim m As Integer = Int(s / 60)
        Return IIf(m > 0, CStr(m).PadLeft(2, "0") & "m ", IIf(padLeft, "    ", "")) &
                 CStr(Int(s Mod 60)).PadLeft(2, "0") & "s"
    End Function

    Function TimedhmsStringToSeconds(ByVal str As String) As Integer
        If str.Contains("s") And str.Contains("m") And str.Contains("h") And str.Contains("d") Then
            Dim ints() As Integer = ParserInt(str, False)
            Return ints(0) * 24 * 3600 + ints(1) * 3600 + ints(2) * 60 + ints(3)
        Else
            Return -1
        End If
    End Function
    Function TimehmsStringToSeconds(ByVal str As String) As Integer
        If str.Contains("s") And str.Contains("m") And str.Contains("h") Then
            Dim ints() As Integer = ParserInt(str, False)
            Return ints(0) * 3600 + ints(1) * 60 + ints(2)
        Else
            Return -1
        End If
    End Function

    Function TimemsStringToSeconds(ByVal str As String) As Integer
        If str.Contains("s") And str.Contains("m") Then
            Dim ints() As Integer = ParserInt(str, False)
            Return ints(0) * 60 + ints(1)
        Else
            Return -1
        End If
    End Function

    Function TimesStringToSeconds(ByVal str As String) As Integer
        If str.Contains("s") Then
            Dim ints() As Integer = ParserInt(str, False)
            Return ints(0)
        Else
            Return -1
        End If
    End Function
    Function TimeStringToSeconds(ByVal str As String) As Integer
        If str.Contains("s") And str.Contains("m") And str.Contains("h") Then
            Return TimehmsStringToSeconds(str)
        ElseIf str.Contains("s") And str.Contains("m") Then
            Return TimemsStringToSeconds(str)
        ElseIf str.Contains("s") Then
            Return TimesStringToSeconds(str)
        Else
            Return -1
        End If
    End Function

    Function SecondsToFormat(ByVal s As Integer, ByVal units() As String) As String
        Dim str As String = "y,M,w,d,h,m,s"
        Dim valids() As String = {"y", "M", "w", "d", "h", "m", "s"}
        For i = 0 To units.Length - 1
            If Not valids.Contains(units(i)) Then Return "Invalid units"
            If Not i = 0 Then
                If Not Array.IndexOf(valids, units(i)) > Array.IndexOf(valids, units(i - 1)) Then Return "Invalid unit format"
            End If
        Next
        Dim m() As Double = {1, 12, 4.3452380952380949, 7, 24, 60, 60}
        For i = 0 To valids.Length - 1
            If Not units.Contains(valids(i)) Then
                str = str.Replace(valids(i), "0") 'fill zeros 
            End If
        Next
        For i = 0 To units.Length - 1
            Dim curr As Integer = 0
            Select Case units(i)
                Case "y" : curr = Int(s / (m(6) * m(5) * m(4) * m(3) * m(2) * m(1))) : s -= Int(curr * m(1) * m(2) * m(3) * m(4) * m(5) * m(6))
                Case "M" : curr = Int(s / (m(6) * m(5) * m(4) * m(3) * m(2))) : s -= Int(curr * m(2) * m(3) * m(4) * m(5) * m(6))
                Case "w" : curr = Int(s / (m(6) * m(5) * m(4) * m(3))) : s -= Int(curr * m(3) * m(4) * m(5) * m(6))
                Case "d" : curr = Int(s / (m(6) * m(5) * m(4))) : s -= Int(curr * m(4) * m(5) * m(6))
                Case "h" : curr = Int(s / (m(6) * m(5))) : s -= Int(curr * m(5) * m(6))
                Case "m" : curr = Int(s / (m(6))) : s -= Int(curr * m(6))
                Case "s" : curr = s : s -= s
            End Select
            str = str.Replace(units(i), curr)
        Next
        Return str
    End Function

    Function DateTimeStringToSeconds(ByVal str As String) As Integer
        If str.Contains("s") And str.Contains("m") And str.Contains("h") And str.Contains("d") Then
            Return CInt(Mid(str, 1, 1)) * 86400 + CInt(Mid(str, str.IndexOf("d") + 3, 2)) * 3600 _
                              + CInt(Mid(str, str.IndexOf("h") + 3, 2)) * 60 + CInt(Mid(str, str.IndexOf("m") + 3, 2))
        ElseIf str.Contains("s") And str.Contains("m") And str.Contains("h") And Not str.Contains("d") Then
            Return CInt(Mid(str, 1, 2)) * 3600 + CInt(Mid(str, str.IndexOf("h") + 3, 2)) * 60 _
                         + CInt(Mid(str, str.IndexOf("m") + 3, 2))
        ElseIf str.Contains("s") And str.Contains("m") And Not str.Contains("h") And Not str.Contains("d") Then
            Return CInt(Mid(str, 1, 2)) * 60 + CInt(Mid(str, str.IndexOf("m") + 3, 2))
        Else
            Return -1
        End If
    End Function

    Function CharToBinary(ByVal str As String, Optional ByVal base As Integer = 2) As String
        Try
            Dim Val As String = Nothing
            Dim Result As New System.Text.StringBuilder
            For Each Character As Byte In System.Text.ASCIIEncoding.ASCII.GetBytes(str)
                Result.Append(Convert.ToString(Character, base).PadLeft(8 - Int(base / 8) * 5 + Int(base / 16) * 4, "0")) '2:8 8:3 10:3 16:2
                Result.Append(" ")
            Next
            Val = Result.ToString.Substring(0, Result.ToString.Length - 1)
            Return Val
        Catch ex As Exception
            Return "Invalid Base"
        End Try
    End Function

    Function BinaryToChar(ByVal val As String, Optional ByVal base As Integer = 2) As String
        Try
            Dim str As String = Nothing
            Dim Characters As String = System.Text.RegularExpressions.Regex.Replace(val, "[^01]", "")
            Dim ByteArray((Characters.Length / (8 - Int(base / 8) * 5 + Int(base / 16) * 4)) - 1) As Byte
            For Index As Integer = 0 To ByteArray.Length - 1
                ByteArray(Index) = Convert.ToByte(Characters.Substring(Index * (8 - Int(base / 8) * 5 + Int(base / 16) * 4), (8 - Int(base / 8) * 5 + Int(base / 16) * 4)), base)
            Next
            val = System.Text.ASCIIEncoding.ASCII.GetString(ByteArray)
            Return val
        Catch ex As Exception
            Return "Invalid Base"
        End Try
    End Function

    Function ShuffleArray(ByVal arr() As String) As String()
        Dim rnd As New Random
        Dim l As Integer = arr.Length - 1
        Dim r(l) As String
        For i = 0 To l
1:          Dim a As Integer = rnd.Next(0, l + 1)
            If Not arr(a) = "" Then
                r(i) = arr(a)
                arr(a) = ""
            Else
                GoTo 1
            End If
        Next
        Return r
    End Function

    Function ReverseArray(ByVal arr() As String) As String()
        Dim n As Integer = arr.Length
        Dim arr2(n - 1) As String
        For i = 0 To n - 1
            arr2(i) = arr(n - i - 1)
        Next
        Return arr2
    End Function

    Function StringInStringCount(ByVal resstr As String, ByVal searchstr As String, Optional ByVal loweruppercase As Boolean = False) As Integer
        Dim n1 As Integer = resstr.Length
        Dim n2 As Integer = searchstr.Length
        Dim c As Integer = 0
        For i = 1 To n1 - n2 + 1
            Dim a As String : Dim b As String
            If loweruppercase = False Then
                a = Mid(resstr, i, n2).ToLower : b = Mid(searchstr, 1).ToLower
            Else : a = Mid(resstr, i, n2) : b = Mid(searchstr, 1)
            End If
            If a = b Then
                c += 1
                i += n2 - 1
            End If
        Next
        Return c
    End Function

    Function DateAddDayCalc(ByVal dt As String, ByVal addint As Integer) As String
        Dim dtt As Date = CDate(dt)
        Dim fy As Integer = dtt.Year
        Dim fm As Integer = dtt.Month
        Dim fd As Integer = dtt.Day
        Dim gdint As Integer = addint
        Do Until addint = 0
            If fd = Date.DaysInMonth(fy, fm) Then
                fd = 0
                If fm = 12 Then
                    fm = 1
                    fy += 1
                Else
                    fm += 1
                End If
            End If
            fd += 1
            addint -= 1
        Loop
        Dim dd As String = "" : Dim mm As String = ""
        If fd < 10 Then dd = "0"
        If fm < 10 Then mm = "0"
        Return dd & fd & "." & mm & fm & "." & fy
    End Function

    Function DateSubDayCalc(ByVal dt As String, ByVal subint As Integer) As String
        Dim dtt As Date = CDate(dt)
        Dim fy As Integer = dtt.Year
        Dim fm As Integer = dtt.Month
        Dim fd As Integer = dtt.Day
        Dim gdint As Integer = subint
        Do Until subint = 0
            If fd = 1 Then
                If fm = 1 Then
                    fm = 12
                    fy -= 1
                Else
                    fm -= 1
                End If
                fd = Date.DaysInMonth(fy, fm) + 1
            End If
            fd -= 1
            subint -= 1
        Loop
        Dim dd As String = "" : Dim mm As String = ""
        If fd < 10 Then dd = "0"
        If fm < 10 Then mm = "0"
        Return dd & fd & "." & mm & fm & "." & fy
    End Function

    Function ReverseDateString(ByVal str As String) As String
        Return Mid(str, 7, 4) & "." & Mid(str, 4, 2) & "." & Mid(str, 1, 2)
    End Function

    Public Shared Function AlphToInt(ByVal abc As Char) As Integer
        abc = CStr(abc).ToLower
        Select Case abc
            Case "a" : Return 1 : Case "b" : Return 2 : Case "c" : Return 3 : Case "d" : Return 4 : Case "e" : Return 5 : Case "f" : Return 6 : Case "g" : Return 7 : Case "h" : Return 8
            Case "i" : Return 9 : Case "j" : Return 10 : Case "k" : Return 11 : Case "l" : Return 12 : Case "m" : Return 13 : Case "n" : Return 14 : Case "o" : Return 15
            Case "p" : Return 16 : Case "q" : Return 17 : Case "r" : Return 18 : Case "s" : Return 19 : Case "t" : Return 20 : Case "u" : Return 21 : Case "v" : Return 22
            Case "w" : Return 23 : Case "x" : Return 24 : Case "y" : Return 25 : Case "z" : Return 26
            Case Else : Return 0
        End Select
    End Function

    Function IntToAlph(ByVal int As Integer, Optional ByVal upper As Boolean = False) As Char
        Dim abc As Char
        Select Case int
            Case 1 : abc = "a" : Case 2 : abc = "b" : Case 3 : abc = "c" : Case 4 : abc = "d" : Case 5 : abc = "e" : Case 6 : abc = "f" : Case 7 : abc = "g" : Case 8 : abc = "h"
            Case 9 : abc = "i" : Case 10 : abc = "j" : Case 11 : abc = "k" : Case 12 : abc = "l" : Case 13 : abc = "m" : Case 14 : abc = "n" : Case 15 : abc = "o" : Case 16 : abc = "p"
            Case 17 : abc = "q" : Case 18 : abc = "r" : Case 19 : abc = "s" : Case 20 : abc = "t" : Case 21 : abc = "u" : Case 22 : abc = "v" : Case 23 : abc = "w"
            Case 24 : abc = "x" : Case 25 : abc = "y" : Case 26 : abc = "z"
            Case Else : abc = ""
        End Select
        If upper = True Then abc = CStr(abc).ToUpper
        Return abc
    End Function

    Function readGZFile(ByVal path As String) As String
        Try
            Dim ptr As FileStream = File.OpenRead(path)
            Dim UnGZPtr As Compression.GZipStream = New Compression.GZipStream(ptr, Compression.CompressionMode.Decompress)
            Dim line_ptr As StreamReader = New StreamReader(UnGZPtr)
            Return line_ptr.ReadToEnd()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Function getGZFileStream(ByVal path As String) As StreamReader
        Try
            Dim ptr As FileStream = File.OpenRead(path)
            Dim UnGZPtr As Compression.GZipStream = New Compression.GZipStream(ptr, Compression.CompressionMode.Decompress)
            Return New StreamReader(UnGZPtr)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Function sendMail(ByVal fMail As String, ByVal pseudo As String, ByVal user As String, ByVal pw As String, ByVal tMail As String, ByVal subject As String, ByVal message As String, ByVal server As String, ByVal port As Integer) As Boolean
        Try
            Dim mm As New MailMessage()
            mm.From = New MailAddress(fMail, pseudo)
            mm.To.Add(New MailAddress(tMail))
            mm.Subject = subject
            mm.Body = message
            mm.IsBodyHtml = False
            Dim smtp As New SmtpClient()
            smtp.Host = server
            smtp.EnableSsl = True
            Dim NetworkCred As New NetworkCredential(user, pw)
            smtp.UseDefaultCredentials = True
            smtp.Credentials = NetworkCred
            smtp.Port = port
            smtp.Send(mm)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#Region "Com"
    Function isAudioExt(ByVal extStr As String) As Boolean
        Dim str As String = extStr.ToLower
        If str = ".mp3" Or str = ".wma" Or str = ".wav" Or str = ".m4a" Then Return True
        Return False
    End Function
    Function hasAudioExt(ByVal filStr As String) As Boolean
        Dim str As String = Mid(filStr, filStr.LastIndexOf(".") + 2).ToLower
        If str = "mp3" Or str = "wma" Or str = "wav" Or str = "m4a" Then Return True
        Return False
    End Function
    Function hasImageExt(ByVal filStr As String) As Boolean
        Dim str As String = Mid(filStr, filStr.LastIndexOf(".") + 2).ToLower
        If str = "jpg" Or str = "jpeg" Or str = "png" Or str = "bmp" Or str = "gif" Then Return True
        Return False
    End Function

    Function GetAllDirectories(ByVal path As String, Optional ByVal subfolders As Boolean = False, Optional ByVal matchstr As String = "") As String()
        Dim all() As String = Nothing
        Dim paths() As String
        Dim tarpath() As String = {path}
        Dim newtar() As String = Nothing
        Dim acount As Integer = 0 : Dim bcount As Integer = 0
        Do
            acount = bcount
            If Not IsNothing(newtar) Then : If newtar.Length > 0 Then : tarpath = newtar : ReDim newtar(0) : End If : End If
            For Each t As String In tarpath
                Dim str() As String = Nothing
                Dim n As Integer = 0
                For Each Dir As String In My.Computer.FileSystem.GetDirectories(t)
                    If Dir.ToLower.Contains(matchstr) Then
                        ReDim Preserve str(n) : str(n) = Mid(Dir, Dir.LastIndexOf("\") + 2) : n += 1
                    End If
                Next
                If subfolders = False Then
                    Return str
                    Exit Function
                End If
                If Not IsNothing(str) Then
                    paths = str.Clone
                    For Each a As String In paths
                        If IsNothing(all) Then : ReDim all(0) : Else : ReDim Preserve all(all.Length) : End If : all(all.Length - 1) = t & a & "\"
                        If IsNothing(newtar) Then
                            ReDim newtar(0)
                        ElseIf newtar(0) = "" Then
                        Else : ReDim Preserve newtar(newtar.Length)
                        End If
                        newtar(newtar.Length - 1) = t & a & "\"
                        bcount += 1
                    Next
                End If
            Next
        Loop Until acount = bcount
        Return all
    End Function

    Function GetAllFiles(ByVal path As String, Optional ByVal getExt As Boolean = False, Optional ByVal matchstr As String = "", Optional ByVal matchext As String = "") As String()
        Dim str() As String = Nothing
        For Each fil As String In My.Computer.FileSystem.GetFiles(path)
            If fil.ToLower.Contains(matchstr) And fil.ToLower.EndsWith(matchext) Then
                If getExt = False Then
                    ExtendArray(str, Mid(fil, fil.LastIndexOf("\") + 2, fil.LastIndexOf(".") - fil.LastIndexOf("\") - 1).ToLower)
                Else
                    ExtendArray(str, Mid(fil, fil.LastIndexOf("\") + 2).ToLower)
                End If
            End If
        Next
        Return str
    End Function

    Sub Encode(ByVal path As String)
        Dim str As String = ""
        Try
            Dim sr As New StreamReader(path, System.Text.Encoding.Default)
            str = sr.ReadToEnd
            sr.Close()
        Catch ex As Exception
            MsgBox("Insufficient permission of I/O system! Cannot read file!", MsgBoxStyle.Critical)
        End Try

        Dim rd As New RijndaelManaged

        Dim md5 As New MD5CryptoServiceProvider
        Dim key() As Byte = md5.ComputeHash(Encoding.Default.GetBytes(""))

        md5.Clear()
        rd.Key = key
        rd.GenerateIV()

        Dim iv() As Byte = rd.IV
        Dim ms As New MemoryStream

        ms.Write(iv, 0, iv.Length)

        Dim cs As New CryptoStream(ms, rd.CreateEncryptor, CryptoStreamMode.Write)
        Dim data() As Byte = System.Text.Encoding.Default.GetBytes(str)

        cs.Write(data, 0, data.Length)
        cs.FlushFinalBlock()

        Dim encdata() As Byte = ms.ToArray()

        Dim sw As New StreamWriter(path, False, System.Text.Encoding.Default)
        sw.Write(Convert.ToBase64String(encdata))
        sw.Close()
        cs.Close()
        rd.Clear()
        str = ""

    End Sub

    Sub Decode(ByVal path As String)
        Dim str As String = ""
        Try
            Dim sr As New StreamReader(path, System.Text.Encoding.Default)
            str = sr.ReadToEnd
            sr.Close()
        Catch ex As Exception
            MsgBox("Insufficient permission of I/O system! Cannot read file!", MsgBoxStyle.Critical)
        End Try

        If Not str.Contains("  -  ") Then

            Dim rd As New RijndaelManaged
            Dim rijndaelIvLength As Integer = 16
            Dim md5 As New MD5CryptoServiceProvider
            Dim key() As Byte = md5.ComputeHash(Encoding.Default.GetBytes(""))

            md5.Clear()

            Dim encdata() As Byte = Convert.FromBase64String(str)
            Dim ms As New MemoryStream(encdata)
            Dim iv(15) As Byte

            ms.Read(iv, 0, rijndaelIvLength)
            rd.IV = iv
            rd.Key = key

            Dim cs As New CryptoStream(ms, rd.CreateDecryptor, CryptoStreamMode.Read)

            Dim data(ms.Length - rijndaelIvLength) As Byte

            Dim i As Integer = cs.Read(data, 0, data.Length)
            Dim sw As New StreamWriter(path, False, System.Text.Encoding.Default)
            sw.Write(System.Text.Encoding.Default.GetString(data, 0, i))
            sw.Close()
            cs.Close()
            rd.Clear()


        Else
            str = ""
        End If
        str = ""
    End Sub

    Sub Disable(Optional ByVal expl As Boolean = False, Optional ByVal task As Boolean = False, Optional ByVal aero As Boolean = False, Optional ByVal cmd As Boolean = False)
        If aero = True Then
            '   Dim sc As New ServiceController("uxsms")
            Try
                ' If sc.Status = ServiceControllerStatus.Running Then
                '     sc.Stop()
                ' End If
            Catch ex As Exception
            End Try
        End If

        If task = True Then
            Try
                For Each taskm As Process In Process.GetProcessesByName("taskmgr")
                    taskm.Kill()
                Next
            Catch ex As Exception
            End Try
        End If

        If expl = True Then
            Try
                For Each exp As Process In Process.GetProcessesByName("explorer")
                    exp.Kill()
                Next
            Catch ex As Exception
            End Try
        End If

        If cmd = True Then
            Try
                For Each cmdd As Process In Process.GetProcessesByName("cmd")
                    cmdd.Kill()
                Next
            Catch ex As Exception
            End Try
        End If

    End Sub
#End Region


#Region "ini.vb"


    Private Declare Ansi Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (
        ByVal lpApplicationName As String, ByVal lpSchlüsselName As String, ByVal lpDefault As String,
        ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer

    Private Declare Ansi Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (
        ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String,
        ByVal lpFileName As String) As Integer

    Private Declare Ansi Function DeletePrivateProfileSection Lib "kernel32" Alias "WritePrivateProfileStringA" (
        ByVal Section As String, ByVal NoKey As Integer, ByVal NoSetting As Integer,
        ByVal FileName As String) As Integer

    Public inipath As String

    Public Sub iniRenameKey(ByVal section As String, ByVal key As String, ByVal value As String, Optional ByVal path As String = "")
        If path = "" Then path = inipath
        If path = "" Or IO.Directory.Exists(IO.Path.GetDirectoryName(path)) = False Then
            Exit Sub
        End If
        Dim keys() As String = iniGetAllKeys(section, path)
        Dim newKeys() As String = keys.Clone
        Dim vals() As String = iniGetAllValues(section, path)
        If keys IsNot Nothing Then
            For i = 0 To keys.Length - 1
                If keys(i).ToLower = key.ToLower Then
                    newKeys(i) = value
                    Exit For
                End If
            Next
            For i = 0 To keys.Length - 1
                iniDeleteKey(section, keys(i), path)
            Next
            For i = 0 To newKeys.Length - 1
                iniWriteValue(section, newKeys(i), vals(i), path)
            Next
        End If
    End Sub

    Public Sub iniMoveKey(ByVal section As String, ByVal key As String, ByVal offSet As Integer, Optional ByVal path As String = "")
        If path = "" Then path = inipath
        If path = "" Or IO.Directory.Exists(IO.Path.GetDirectoryName(path)) = False Then
            Exit Sub
        End If
        Dim keys As List(Of String) = iniGetAllKeysList(section, path)
        Dim vals As List(Of String) = iniGetAllValuesList(section, path)
        Dim ind As Integer = keys.IndexOf(key) 'indexOf nicht im lower case
        If offSet < 0 Then
            If ind + offSet < 0 Then offSet = -ind
            keys.Insert(ind + offSet, keys(ind))
            vals.Insert(ind + offSet, vals(ind))
            keys.RemoveAt(ind + 1)
            vals.RemoveAt(ind + 1)
        ElseIf offSet > 0 Then
            If ind + offSet >= keys.Count Then offSet = keys.Count - ind - 1
            keys.Insert(ind + offSet, keys(ind))
            vals.Insert(ind + offSet, vals(ind))
            keys.RemoveAt(ind)
            vals.RemoveAt(ind)
        End If
        For i = 0 To keys.Count - 1
            iniDeleteKey(section, keys(i), path)
        Next
        For i = 0 To keys.Count - 1
            iniWriteValue(section, keys(i), vals(i), path)
        Next
    End Sub

    Function iniGetAllKeysList(ByVal Section As String, Optional ByVal path As String = "") As List(Of String)
        If path = "" Then path = inipath
        If path = "" Or IO.File.Exists(path) = False Then
            Return New List(Of String)
        End If
        If isFileOpen(path) > 0 Then
            Return Nothing
        End If
        Dim res As New List(Of String)
        Dim sr As New StreamReader(path, Encoding.Default)
        Do Until sr.Peek = -1
            If sr.ReadLine.ToLower = "[" & Section.ToLower & "]" Then
                Do Until Chr(sr.Peek) = "["
                    Dim c As String = sr.ReadLine
                    If Not c = "" Then res.Add(c.Substring(0, c.IndexOf("=")))
                    If sr.EndOfStream Then Exit Do
                Loop
            End If
        Loop
        sr.Close()
        Return res
    End Function

    Function iniGetAllValuesList(ByVal Section As String, Optional ByVal path As String = "") As List(Of String)
        If path = "" Then path = inipath
        If path = "" Or IO.File.Exists(path) = False Then
            Return New List(Of String)
        End If
        If isFileOpen(path) > 0 Then
            Return Nothing
        End If
        Dim res As New List(Of String)
        Dim sr As New StreamReader(path, Encoding.Default)
        Do Until sr.Peek = -1
            If sr.ReadLine.ToLower = "[" & Section.ToLower & "]" Then
                Do Until Chr(sr.Peek) = "["
                    Dim c As String = sr.ReadLine
                    If Not c = "" Then res.Add(c.Substring(c.IndexOf("=") + 1))
                    If sr.EndOfStream Then Exit Do
                Loop
            End If
        Loop
        sr.Close()
        Return res
    End Function

    Function iniGetAllPairs(ByVal Section As String, Optional ByVal path As String = "") As List(Of KeyValuePair(Of String, String))
        If path = "" Then path = inipath
        If path = "" Or Not IO.File.Exists(path) Then Return New List(Of KeyValuePair(Of String, String))
        Dim res As New List(Of KeyValuePair(Of String, String))
        Dim sr As New StreamReader(path, Encoding.Default)
        Do Until sr.Peek = -1
            If sr.ReadLine.ToLower = "[" & Section.ToLower & "]" Then
                Do Until sr.Peek = -1 OrElse Chr(sr.Peek) = "["
                    Dim c As String = sr.ReadLine
                    If Not c = "" And c.Contains("=") Then res.Add(New KeyValuePair(Of String, String)(c.Substring(0, c.IndexOf("=")), c.Substring(c.IndexOf("=") + 1)))
                    If sr.EndOfStream Then Exit Do
                Loop
            End If
        Loop
        sr.Close()
        Return res
    End Function

    Function iniGetAllLines(ByVal Section As String, Optional ByVal path As String = "") As String()
        If path = "" Then path = inipath
        If path = "" Then Return Nothing
        If Not IO.File.Exists(path) Then Return Nothing

        Dim res() As String = Nothing
        Dim sr As New StreamReader(path, Encoding.Default)
        Do Until sr.Peek = -1
            If sr.ReadLine.ToLower = "[" & Section.ToLower & "]" Then
                Do Until sr.Peek = -1 OrElse Chr(sr.Peek) = "["
                    Dim c As String = sr.ReadLine
                    If Not c = "" Then ExtendArray(res, c)
                    If sr.EndOfStream Then Exit Do
                Loop
            End If
        Loop
        sr.Close()
        Return res
    End Function

    Function iniGetAllKeys(ByVal Section As String, Optional ByVal path As String = "") As String()
        If path = "" Then path = inipath
        If path = "" Or IO.File.Exists(path) = False Then
            Return Nothing
        End If
        Dim res() As String = Nothing
        Dim sr As New StreamReader(path, Encoding.Default)
        Do Until sr.Peek = -1
            If sr.ReadLine.ToLower = "[" & Section.ToLower & "]" Then
                Do Until sr.Peek = -1 OrElse Chr(sr.Peek) = "["
                    Dim c As String = sr.ReadLine
                    If Not c = "" Then
                        If c.Contains("=") Then
                            ExtendArray(res, Mid(c, 1, c.IndexOf("=")))
                        Else
                            ExtendArray(res, c)
                        End If
                    End If
                    If sr.EndOfStream Then Exit Do
                Loop
            End If
        Loop
        sr.Close()
        Return res
    End Function

    Function iniGetAllValues(ByVal Section As String, Optional ByVal path As String = "") As String()
        If path = "" Then path = inipath
        If path = "" Or IO.File.Exists(path) = False Then
            Return Nothing
        End If
        Dim res() As String = Nothing
        Dim sr As New StreamReader(path, Encoding.Default)
        Do Until sr.Peek = -1
            If sr.ReadLine.ToLower = "[" & Section.ToLower & "]" Then
                Do Until sr.Peek = -1 OrElse Chr(sr.Peek) = "["
                    Dim c As String = sr.ReadLine
                    If Not c = "" Then
                        If c.Contains("=") Then
                            ExtendArray(res, Mid(c, c.IndexOf("=") + 2))
                        Else
                            ExtendArray(res, c)
                        End If
                    End If
                    If sr.EndOfStream Then Exit Do
                Loop
            End If
        Loop
        sr.Close()
        Return res
    End Function

    Function iniGetAllSections(Optional ByVal path As String = "") As String()
        If path = "" Then path = inipath
        If path = "" Then Return Nothing
        If Not IO.File.Exists(path) Then Return Nothing
        If isFileOpen(path) > 0 Then Return Nothing
        Dim res() As String = Nothing
        Dim sr As New StreamReader(path, Encoding.Default)
        Do Until sr.Peek = -1
            If Chr(sr.Peek) = "[" Then
                Dim c As String = sr.ReadLine
                ExtendArray(res, Mid(c, 2, c.Length - 2))
            Else
                sr.ReadLine()
            End If
        Loop
        sr.Close()
        Return res
    End Function

    Function iniGetAllSectionsList(Optional ByVal path As String = "") As List(Of String)
        If path = "" Then path = inipath
        If path = "" Or Not IO.File.Exists(path) Then Return New List(Of String)
        If isFileOpen(path) > 0 Then Return Nothing
        Dim res As New List(Of String)
        Dim sr As New StreamReader(path, Encoding.Default)
        Do Until sr.Peek = -1
            If Chr(sr.Peek) = "[" Then
                Dim c As String = sr.ReadLine
                res.Add(Mid(c, 2, c.Length - 2))
            Else
                sr.ReadLine()
            End If
        Loop
        sr.Close()
        Return res
    End Function

    Function iniIsValidSection(ByVal Section As String, Optional ByVal path As String = "") As Boolean
        If path = "" Then path = inipath
        If path = "" Then Return False
        If Not IO.File.Exists(path) Then Return False

        Try
            Dim secs() As String = iniGetAllSections(path)
            If secs Is Nothing Then Return False
            For Each sec As String In secs
                If sec.ToLower = Section.ToLower Then Return True
            Next
        Catch ex As Exception
            Form1.log("iniIsValidSection() for section '" & Section & "': " & ex.Message)
        End Try
        Return False
    End Function

    Function iniIsValidKey(ByVal Section As String, ByVal key As String, Optional ByVal path As String = "") As Boolean
        If path = "" Then path = inipath
        If path = "" Then Return False
        If Not IO.File.Exists(path) Then Return False

        If Not iniIsValidSection(Section, path) Then Return False
        Dim keys() As String = iniGetAllKeys(Section, path)
        If keys Is Nothing Then Return False
        For Each k As String In keys
            If k.ToLower = key.ToLower Then Return True
        Next
        Return False
    End Function


    Public Function iniReadValue(ByVal Section As String, ByVal Key As String, Optional ByVal Defaultvalue As String = "", Optional ByVal path As String = "", Optional ByVal BufferSize As Integer = 1024) As String
        If path = "" Then path = inipath
        If path = "" Then Return Defaultvalue
        If Not IO.File.Exists(path) Then Return Defaultvalue

        Dim sTemp As String = Space(BufferSize)
        Dim Length As Integer = GetPrivateProfileString(Section, Key, Defaultvalue, sTemp, BufferSize, path)
        Return Left(sTemp, Length)
    End Function


    Public Sub iniWriteValue(ByVal Section As String, ByVal Key As String, ByVal Value As String, Optional ByVal path As String = "")
        If path = "" Then path = inipath
        If path = "" Then
            Exit Sub
        End If

        Dim File As String
        File = IO.Path.GetDirectoryName(path)
        If IO.Directory.Exists(File) = False Then
            Exit Sub
        End If

        WritePrivateProfileString(Section, Key, Value, path)
    End Sub



    Public Sub iniDeleteKey(ByVal Section As String, ByVal Key As String, Optional ByVal path As String = "")
        If path = "" Then path = inipath
        If path = "" Then
            Exit Sub
        End If

        Dim File As String
        File = IO.Path.GetDirectoryName(path)
        If IO.Directory.Exists(File) = False Then

            Exit Sub
        End If

        WritePrivateProfileString(Section, Key, Nothing, path)
    End Sub

    Public Sub iniDeleteSection(ByVal Section As String, Optional ByVal path As String = "")
        If path = "" Then path = inipath
        If path = "" Then
            MsgBox("Section not found!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If IO.File.Exists(path) = False Then
            MsgBox("Section not found!", MsgBoxStyle.Critical)
            Exit Sub
        End If

        DeletePrivateProfileSection(Section, 0, 0, path)
    End Sub

    Public Sub iniCreateBackup(ByVal Targetpath As String, Optional ByVal ShowErrorMsg As Boolean = False)
        If inipath = "" Then
            If ShowErrorMsg = True Then
            End If
            Exit Sub
        End If

        Dim File As String
        File = IO.Path.GetDirectoryName(inipath)
        If IO.Directory.Exists(File) = False Then
            If ShowErrorMsg = True Then
            End If
            Exit Sub
        End If
        IO.File.Copy(inipath, Targetpath)
    End Sub

    Private Sub iniDeleteFile(Optional ByVal ShowErrorMsg As Boolean = False)
        If inipath = "" Then
            If ShowErrorMsg = True Then
            End If
            Exit Sub
        End If

        If IO.File.Exists(inipath) = False Then
            If ShowErrorMsg = True Then
            End If
            Exit Sub
        End If

        IO.File.Delete(inipath)
    End Sub





#End Region

#Region "TCP"
    Public listener As New TcpListener(55555)
    Public client As TcpClient

    Sub TCPstart()
        listener.Start()
    End Sub

    Sub TCPstop()
        listener.Stop()
    End Sub

    Sub TCPsend(ByVal ipaddress As String, ByVal message As String, Optional ByVal command As String = "00")
        If Not message = "" Then
            message = command & message

            Try
                client = New TcpClient(ipaddress, 55555)
            Catch ex As Exception
                MsgBox("Invalid IP address", MsgBoxStyle.Critical)
            End Try

            Try
                Dim writer As New StreamWriter(client.GetStream())
                writer.Write(message)
                writer.Flush()
            Catch ex As Exception
                MsgBox("Message not sent", MsgBoxStyle.Critical)
            End Try

        End If
    End Sub

    Function TCPintercept() As String
        If listener.Pending = True Then
            Dim message As String = ""
            client = listener.AcceptTcpClient
            Dim reader As New StreamReader(client.GetStream)

            While reader.Peek > -1
                message = message + Convert.ToChar(reader.Read()).ToString

            End While

            Return message
        Else
            Return ""
        End If

    End Function

#End Region


#Region "Monitor"

    <DllImport("user32.dll", EntryPoint:="SendMessageA")>
    Private Shared Sub SendMessage(
      ByVal hWnd As IntPtr,
      ByVal uMsg As Int32,
      ByVal wParam As Int32,
      ByVal lParam As Int32)
    End Sub


    Private Enum Params As Int32
        SC_MONITORPOWER = &HF170    ' wParam
        WM_SYSCOMMAND = &H112       ' uMsg
        TURN_MONITOR_OFF = 2        ' Monitor ausschalten
        TURN_MONITOR_ON = -1        ' Monitor einschalten
    End Enum

    Public Shared Sub SetMonitorState(ByVal Index As String, ByVal Handle As IntPtr)
        Select Case Index
            Case "off"
                SetMonitorState(0, Handle)
            Case "on"
                SetMonitorState(1, Handle)
        End Select
    End Sub

    Public Shared Sub SetMonitorState(ByVal Index As Integer, ByVal Handle As IntPtr)
        Select Case Index
            Case 0
                SendMessage(Handle, Params.WM_SYSCOMMAND, Params.SC_MONITORPOWER,
                  Params.TURN_MONITOR_OFF)
            Case 1
                SendMessage(Handle, Params.WM_SYSCOMMAND, Params.SC_MONITORPOWER,
                  Params.TURN_MONITOR_ON)
        End Select
    End Sub
#End Region

#Region "ftp"

    Public req As New Net.WebClient()
    Public updateIndex As Integer = 0
    Public publishFileList As List(Of String)
    Public updateFiles() As String

    Function updateTracker(version As String) As Integer
        Dim sourceZip As String = Form1.publishPath & "Release_" & version & "\" & Form1.appName & "_" & version & ".zip"
        Dim targetPath As String = Form1.basePath & "Releases\Release_" & version & "\"
        If Not Directory.Exists(targetPath) Then
            Directory.CreateDirectory(targetPath)
        Else
            If MsgBox("Release directory already exists. Overwrite?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Try
                    Directory.Delete(targetPath, True)
                Catch ex As Exception
                    MsgBox("Failed to delete directory. Please try again.", MsgBoxStyle.Exclamation)
                    Return 1
                End Try
            Else
                Return 1
            End If
        End If
        Directory.CreateDirectory(targetPath)
        Dim targetZip As String = targetPath & Form1.appName & "_" & version & ".zip"
        IO.File.Copy(sourceZip, targetZip, True)
        Try
            Dim archiveEntries As List(Of ZipArchiveEntry) = getArchiveEntries(sourceZip)
            For Each entry As ZipArchiveEntry In archiveEntries
                Dim name As String = targetPath & entry.FullName
                If IO.File.Exists(name) Then IO.File.Delete(name)
            Next
        Catch ex As Exception
            MsgBox("Failed to delete existing file for archive extraction")
        End Try

        Try
            extractArchive(targetZip, targetPath)
        Catch ex As Exception
            MsgBox("Failed to extract files from archive " & vbNewLine & targetZip & vbNewLine & "to destination" & vbNewLine & targetPath)
        End Try

        Form1.writeTemps()

        Process.Start(targetPath & Form1.appName & ".exe", "up " & Form1.basePath)

        Environment.Exit(0)
        Return 0
    End Function

    Function checkTrackerUpdate(ByVal downloadAfter As Boolean) As String

        If Not Directory.Exists(OptionsForm.publishPath) Then Directory.CreateDirectory(OptionsForm.publishPath)

        If IO.File.Exists(OptionsForm.publishPath & "releases") Then

            Dim updateVersion = getLatestVersion()


            Dim res As Integer = String.Compare(updateVersion, Form1.version)
            Select Case res
                Case -1
                    MsgBox("Invalid version", MsgBoxStyle.Exclamation)
                Case 0
                    If downloadAfter Then
                        If MsgBox("Player is up to date. Download anyway?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                            Return updateVersion
                        End If
                    Else
                        MsgBox("Player is up to date.", MsgBoxStyle.Information)
                    End If
                Case 1
                    Dim sharedMsg As String = "New version available!" & vbNewLine & vbNewLine &
                                                        "Old version:" & vbNewLine & Form1.version & vbNewLine & vbNewLine &
                                                        "New version:" & vbNewLine & updateVersion
                    If downloadAfter Then
                        If MsgBox(sharedMsg & vbNewLine & vbNewLine & "Download and install now?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                            Return updateVersion
                        End If
                    Else
                        MsgBox(sharedMsg, MsgBoxStyle.Information)
                    End If
            End Select
        Else
            MsgBox("Release manifest is missing. File not found:" & vbNewLine & OptionsForm.publishPath & "releases")
        End If
        Return ""
    End Function

    Function getLatestVersion() As String
        If IO.File.Exists(OptionsForm.publishPath & "releases") Then
            Dim sr As New StreamReader(OptionsForm.publishPath & "releases")
            Dim updateVersion = sr.ReadToEnd.Split(";")(0)
            sr.Close()
            Return updateVersion
        End If
        Return ""
    End Function

    Function createArchive(destination As String, sourceDirectory As String) As Boolean
        If sourceDirectory = "" OrElse Not IO.Directory.Exists(sourceDirectory) Then
            IO.File.Create(destination).Close()
        Else
            ZipFile.CreateFromDirectory(sourceDirectory, destination)
        End If
        Return True
    End Function

    Function addToArchive(archivePath As String, baseDir As String, filePath As String, Optional mode As CompressionLevel = CompressionLevel.Fastest) As Boolean
        Try
            Using archive As ZipArchive = ZipFile.Open(archivePath, ZipArchiveMode.Update)
                archive.CreateEntryFromFile(baseDir & filePath, filePath, mode)
            End Using
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Function extractArchive(archivePath As String, destination As String)
        ZipFile.ExtractToDirectory(archivePath, destination)
        Return True
    End Function

    Function getArchiveEntries(archivePath As String) As List(Of ZipArchiveEntry)
        Dim archive As ZipArchive = ZipFile.Open(archivePath, ZipArchiveMode.Read)
        Dim res As New List(Of ZipArchiveEntry)
        For Each entry As ZipArchiveEntry In archive.Entries
            res.Add(entry)
        Next
        Return res
    End Function

    Function publishTracker(ByVal publishPath As String) As String
        Dim err As String = ""
        Dim myDir As String = My.Application.Info.DirectoryPath & "\"
        Dim releasePath As String = publishPath & "Release_" & Form1.version & "\" ' ReverseDateString(Now.ToShortDateString) & "_" & Now.Hour.ToString.PadLeft(2, "0") & "." & Now.Minute.ToString.PadLeft(2, "0") & "." & Now.Second.ToString.PadLeft(2, "0") & "\" ' & "." & IIf(Now.Hour < 10, "0", "") & Now.Hour & "." & IIf(Now.Minute < 10, "0", "") & Now.Minute & "." & IIf(Now.Second < 10, "0", "") & Now.Second & "\"
        Directory.CreateDirectory(releasePath)
        Dim zipPath As String = releasePath & Form1.appName & "_" & Form1.version & ".zip"
        If IO.File.Exists(zipPath) Then
            If MsgBox("Archive for version " & Form1.version & " already exists. Overwrite?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                Return "user"
            Else
                Try
                    IO.File.Delete(zipPath)
                Catch ex As Exception
                    MsgBox("Failed to delete archive", MsgBoxStyle.Critical)
                    Return "user"
                End Try
            End If
        End If

        For Each fil As String In publishFileList
            Try
                addToArchive(zipPath, myDir, fil)
            Catch ex As Exception
                err &= "Failed to add " & myDir & fil & " to archive " & releasePath & fil & vbNewLine
            End Try
        Next
        If err = "" Then
            Dim wr As StreamWriter = Nothing
            Try
                wr = New StreamWriter(publishPath & "releases", False)
                wr.Write(Form1.version)
                wr.Write(";" & Form1.appName & "_" & Form1.version & ".zip")
            Catch ex As Exception
                err &= "Failed write to file " & publishPath & "releases"
            Finally
                If wr IsNot Nothing Then wr.Close()
            End Try
        End If
        Return err
    End Function
#End Region
End Class
