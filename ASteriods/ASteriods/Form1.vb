Public Class Form1

    'This is the x and y of the players "centre" point where all calculations are based off of
    Dim x As Integer = 50
    Dim y As Integer = 50

    'used to determine tick rate
    Const fps As Decimal = 60

    'Variables for detecting key presses
    Dim wpress As Boolean
    Dim apress As Boolean
    Dim spress As Boolean
    Dim dpress As Boolean

    'misc maths variables
    Dim r As Integer = 0    'Roataion from ghost image
    Dim rv As Integer = 5   'Rotation multiplier
    Dim v As Decimal = 0    'Velocity
    Dim maxv As Decimal = 7 'Max velocity
    Dim length As Integer = 20  'Length of player

    'Drawing stuff
    Dim formGraphics As System.Drawing.Graphics 'Graphics layer?
    Dim playerpen As New System.Drawing.Pen(System.Drawing.Color.Blue, 1)   'Blue pen
    Dim debugpen As New System.Drawing.Pen(System.Drawing.Color.Red, 1) 'Red pen
    Dim debugpen2 As New System.Drawing.Pen(System.Drawing.Color.Green, 1)  'Green pen

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Load graphics
        formGraphics = Me.CreateGraphics()

        'Makes the tickspeed relevant to set fps
        GameTick.Interval = 1 / (fps / 1000)
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        'Getting key presses and storing htem in the respective variables
        Select Case e.KeyCode
            Case Keys.W : wpress = True
            Case Keys.A : apress = True
            Case Keys.S : spress = True
            Case Keys.D : dpress = True
        End Select
    End Sub

    Private Sub Form1_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        'Getting key presses and storing htem in the respective variables
        Select Case e.KeyCode
            Case Keys.W : wpress = False
            Case Keys.A : apress = False
            Case Keys.S : spress = False
            Case Keys.D : dpress = False
        End Select
    End Sub

    Private Sub GameTick_Tick(sender As Object, e As EventArgs) Handles GameTick.Tick
        'Radians stuff
        Dim ToRadians As Double = System.Math.PI / 180
        Dim ToDegrees As Double = 180 / System.Math.PI
        Dim rr As Double
        'Rotation direction
        Dim rdir As Integer

        'Checking w and s
        If wpress = True And spress = True Then
            'Nothing
        ElseIf wpress = True Then
            v = v + 0.1
        ElseIf spress = True Then
            v = v - 0.1
        End If

        'Keeping v in acceptable values
        If v > maxv Then v = maxv
        If v < 0 Then v = 0

        'Checking a and d
        If dpress = True And apress = True Then
            rdir = 0
        ElseIf dpress = True Then
            rdir = -1
        ElseIf apress = True Then
            rdir = 1
        Else
            rdir = 0
        End If

        'Updating rotation value
        r = r + rdir * rv

        'Keeping r in acceptable values
        If r > 360 Then r = r - 360
        If r < 0 Then r = r + 360
        'Making radians r
        rr = r * ToRadians

        'Different points used for drawing
        Dim forward As Point
        Dim back As Point
        Dim centre As Point
        Dim p1 As Point 'These two are used for the left and right point to make the shape
        Dim p2 As Point

        formGraphics.Clear(Color.White)

        'Updating centrepint using v and r
        x = x + v * Math.Cos(90 * ToRadians - rr)
        y = y + v * Math.Sin(90 * ToRadians - rr)

        'Updating labels
        Label5.Text = wpress
        Label6.Text = apress
        Label7.Text = spress
        Label8.Text = dpress
        Label4.Text = r
        Label1.Text = x
        Label2.Text = y

        'Setting the centre
        centre.X = x
        centre.Y = y

        'Setting thetopand bottom points of the shape
        forward.X = x + (0.5 * length * Math.Sin(rr))
        forward.Y = y + (0.5 * length * Math.Cos(rr))
        back.X = x - (0.5 * length * Math.Sin(rr))
        back.Y = y - (0.5 * length * Math.Cos(rr))

        'Setting the two edge points
        p1.X = x + length * Math.Sin(rr + 160 * ToRadians)
        p1.Y = y + length * Math.Cos(rr + 160 * ToRadians)
        p2.X = x + length * Math.Sin(rr + 200 * ToRadians)
        p2.Y = y + length * Math.Cos(rr + 200 * ToRadians)

        'Drawing the shape
        formGraphics.DrawLine(playerpen, forward, p1)
        formGraphics.DrawLine(playerpen, p1, back)
        formGraphics.DrawLine(playerpen, forward, p2)
        formGraphics.DrawLine(playerpen, p2, back)

        'Debug line so i can see the shape if i muck up
        formGraphics.DrawLine(debugpen, forward, back)
    End Sub
End Class
