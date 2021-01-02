Public Class GamePanel

    ReadOnly Property dll() As Utils
        Get
            Return Form1.dll
        End Get
    End Property

    Dim game As Game
    Dim label As Label
    Dim pic As PictureBox
    Dim siz As Integer = 150
    Dim baseTop As Integer = 100
    Dim baseLeft As Integer = 100
    Dim gap As Integer = 50

    Public Sub New(game As Game)
        Me.game = game
    End Sub

    Sub init()
        label = New Label()
        pic = New PictureBox()
        pic.BackgroundImage = Image.FromFile(Form1.basePath & game.logoPath)
        pic.BackgroundImageLayout = ImageLayout.Stretch
        pic.Size = New Size(siz, siz)
        pic.Location = New Point(baseLeft + game.id * (siz + gap), baseTop)
        Form1.Controls.Add(pic)
        label.Font = New Font("Georgia", 16, FontStyle.Regular)
        label.Location = New Point(baseLeft + game.id * (siz + gap), baseTop + siz + 10)
        label.ForeColor = Color.White
        label.AutoSize = True
        Form1.Controls.Add(label)

        update()
    End Sub

    Sub update(Optional time As Long = 0)
        label.Text = dll.SecondsTodhmsString(time)
    End Sub
End Class
