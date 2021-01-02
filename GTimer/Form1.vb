Imports System.IO
Imports System.IO.Compression

Public Class Form1

    Public dll As New Utils
    Public iniPath As String = AppDomain.CurrentDomain.BaseDirectory & "\gtimer.ini"
    Public basePath As String = AppDomain.CurrentDomain.BaseDirectory

    Dim appName = "GTimer"

    Dim games As List(Of Game)
    Public lastOptionsState As OptionsForm.optionState

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Application.CommandLineArgs.Count > 0 Then
            Dim para As String = My.Application.CommandLineArgs(0)
            If para.StartsWith("up") Then
                install()
            End If
        End If

        dll.inipath = iniPath

        games = New List(Of Game)
        Dim secs As List(Of String) = dll.iniGetAllSectionsList()
        secs.Remove("Config")
        For i = 0 To secs.Count - 1
            games.Add(New Game(i, secs(i)))
        Next
        updateLabels(True)

        Try
            My.Computer.Registry.LocalMachine.CreateSubKey("Software\Microsoft\Windows\CurrentVersion\Run").SetValue("GTimer", basePath & appName & ".exe")
        Catch ex As Exception

        End Try

        tracker.Start()
        tempWriter.Start()
    End Sub



    Private Sub tracker_Tick(sender As Object, e As EventArgs) Handles tracker.Tick
        For Each game In games
            game.trackerUpdate()
        Next
    End Sub

    Sub updateLabels(reload As Boolean)
        For Each game In games
            game.updatePanel(reload)
        Next
    End Sub

    Private Sub tempWriter_Tick(sender As Object, e As EventArgs) Handles tempWriter.Tick
        updateLabels(True)
    End Sub

    Private Sub optionButton_Click(sender As Object, e As EventArgs) Handles optionButton.Click
        OptionsForm.ShowDialog()
    End Sub

    Sub install()
        killProc("GTimer", True)
        Dim currPath As String = My.Application.Info.DirectoryPath
        Dim copyPath As String = ""
        For i = 1 To My.Application.CommandLineArgs.Count - 1
            copyPath &= My.Application.CommandLineArgs(i) & IIf(i = My.Application.CommandLineArgs.Count - 1, "", " ")
        Next
        MsgBox("Starting Installation...")
1:      Dim fils() As String = Nothing
        Try
            Dim sr As New StreamReader(currPath.Substring(0, currPath.LastIndexOf("\")) & "\releases")
            fils = sr.ReadToEnd().Split(";")
            sr.Close()
            For i = 0 To fils.Length - 1
                fils(i) = fils(i).Replace(";", "")
            Next
        Catch ex As Exception
            If MsgBox("Reading release manifest failed." & vbNewLine & vbNewLine &
                      currPath.Substring(0, currPath.LastIndexOf("\")) & "\releases" &
                      vbNewLine & vbNewLine & "Try again?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation) = MsgBoxResult.Yes Then
                GoTo 1
            Else
                Environment.Exit(0)
            End If
        End Try

        If fils IsNot Nothing Then
            Dim archiveEntries As New List(Of List(Of ZipArchiveEntry))
            For i = 0 To fils.Length - 1
                If CStr(currPath & "\" & fils(i)).EndsWith(".zip") Then
                    archiveEntries.Add(getArchiveEntries(currPath & "\" & fils(i)))
                End If
            Next

            Dim fileList As New List(Of String)
            For Each archive In archiveEntries
                For Each entry In archive
                    fileList.Add(entry.FullName)
                Next
            Next

            For Each fil As String In fileList
                File.Delete(copyPath & "\" & fil)
                File.Copy(currPath & "\" & fil, copyPath & "\" & fil)
            Next
            Try
                Dim wr As New StreamWriter(copyPath & "\version", False)
                wr.Write(currPath.Substring(currPath.LastIndexOf("\") + 8))
                wr.Close()
            Catch ex As Exception
            End Try
            Process.Start(copyPath & "\GTimer.exe")
            Environment.Exit(0)
        Else
            MsgBox("Release manifest is corrupted.")
        End If

    End Sub
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

    Sub killProc(ByVal name As String, Optional excludeOwn As Boolean = False)
        Try
            For Each p As Process In Process.GetProcessesByName(name)
                If Not p.Id = Process.GetCurrentProcess().Id Or Not excludeOwn Then
                    p.Kill()
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub
End Class
