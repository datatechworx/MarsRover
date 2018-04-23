Imports System.IO

Public Class Form1

#Region "IMPORT INPUT FILE"

    Private Sub ButtonImport_Click(sender As Object, e As EventArgs) Handles ButtonImport.Click
        ImportFunction()
    End Sub

    Private Sub ImportFunction()
        Try
            TextBoxTravel.Text = String.Empty
            'Read from file
            Dim marsroverinputfile As String = "marsrover.txt"
            If Not IO.File.Exists(marsroverinputfile) Then
                MsgBox("Input file '" & marsroverinputfile & "' could not be found", MsgBoxStyle.Critical, "Invalid input")
                Exit Sub
            End If
            Dim inputinstruction As String = String.Empty
            Using sr As StreamReader = File.OpenText(marsroverinputfile)
                Do While sr.Peek() >= 0
                    inputinstruction = inputinstruction & sr.ReadLine & Environment.NewLine
                Loop
            End Using
            TextBoxTravel.Text = inputinstruction.ToUpper
            'TODO: add input validation rules for marsrover.txt

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

#End Region 'IMPORT INPUT FILE

#Region "RUN"

    Private Sub ButtonRun_Click(sender As Object, e As EventArgs) Handles ButtonRun.Click
        MarsRoverRun()
        Console.WriteLine(TextBoxOutput.Text)
        Console.WriteLine("Complete")
    End Sub

    Private Sub MarsRoverRun()
        Try
            Dim rovercoordinates As String = String.Empty
            Dim marsrover As New rover
            Dim plateaulimit As New limit
            Dim marsdirection As Char = marsrover.direction
            Dim roverdirection As Integer = 0

            Dim roverA As String = String.Empty
            'Dim roverB As String = String.Empty

            TextBoxOutput.Text = String.Empty
            Application.DoEvents()
            'get input from textbox
            Dim travelinstruction As String() = TextBoxTravel.Text.Split(vbNewLine)
            For Each strline As String In travelinstruction
                If strline = vbLf Then Exit Sub
                If String.IsNullOrEmpty(rovercoordinates) Then
                    rovercoordinates = strline
                    Dim limits As String() = strline.Split(New Char() {" "c})
                    'TODO: add coordinate parameters 0,0 to 5,5 bound limits
                    plateaulimit.limitX = limits(0)
                    plateaulimit.limitY = limits(1)

                Else
                    If Not strline.Contains("M") Then 'TODO: input contains numeric value
                        Dim position As String() = strline.Split(New Char() {" "c})
                        marsrover.positionX = position(0)
                        marsrover.positionY = position(1)
                        marsrover.direction = position(2)
                        'convert direction to degrees
                        Select Case marsrover.direction
                            Case "N"
                                roverdirection = 0
                            Case "E"
                                roverdirection = 90
                            Case "S"
                                roverdirection = 180
                            Case "W"
                                roverdirection = 270
                        End Select
                        marsdirection = marsrover.direction
                    Else
                        Dim rovertravel As String = strline
                        For Each c As Char In rovertravel
                            ' Count c
                            Select Case c
                                Case "L"
                                    If roverdirection = 0 Then roverdirection = 360
                                    roverdirection = roverdirection - 90
                                    If roverdirection = 0 Then marsdirection = "N"
                                    If roverdirection = 90 Then marsdirection = "E"
                                    If roverdirection = 180 Then marsdirection = "S"
                                    If roverdirection = 270 Then marsdirection = "W"
                                Case "R"
                                    roverdirection = roverdirection + 90
                                    If roverdirection = 360 Then roverdirection = 0
                                    If roverdirection = 0 Then marsdirection = "N"
                                    If roverdirection = 90 Then marsdirection = "E"
                                    If roverdirection = 180 Then marsdirection = "S"
                                    If roverdirection = 270 Then marsdirection = "W"
                                Case "M"
                                    Select Case marsdirection
                                        Case "N"
                                            marsrover.positionY = marsrover.positionY + 1
                                        Case "E"
                                            marsrover.positionX = marsrover.positionX + 1
                                        Case "W"
                                            marsrover.positionX = marsrover.positionX - 1
                                        Case "S"
                                            marsrover.positionY = marsrover.positionY - 1
                                    End Select

                                    'TODO: test coordinate parameters 0,0 to 5,5 bound limits
                                    If marsrover.positionX < 0 Then
                                        MsgBox("Minimum X coordinate exceed plateau limit")
                                        Exit Sub
                                    End If
                                    If plateaulimit.limitY < 0 Then
                                        MsgBox("Minimum Y coordinate exceed plateau limit")
                                        Exit Sub
                                    End If
                                    If marsrover.positionX > plateaulimit.limitX Then
                                        MsgBox("Maximum X coordinate exceed plateau limit")
                                        Exit Sub
                                    End If
                                    If marsrover.positionY > plateaulimit.limitY Then
                                        MsgBox("Maximum Y coordinate exceed plateau limit")
                                        Exit Sub
                                    End If
                            End Select
                            marsrover.direction = marsdirection
                        Next
                        TextBoxOutput.Text = TextBoxOutput.Text & Environment.NewLine & marsrover.positionX & " " & marsrover.positionY & " " & marsrover.direction

                        'PLATEAU COORDINATES 0,0 - 5,5 
                        If plateaulimit.limitX = 5 And plateaulimit.limitY = 5 Then
                            Try
                                roverbuttonA.Visible = True
                                roverButtonB.Visible = True
                                If String.IsNullOrEmpty(roverA) Then
                                    roverA = "A"
                                    TableLayoutPanel1.SetColumn(roverbuttonA, (marsrover.positionX) - 1)
                                    TableLayoutPanel1.SetRow(roverbuttonA, (4 - marsrover.positionY) + 1)
                                    roverbuttonA.Text = marsrover.direction
                                Else
                                    'roverB = "B"
                                    TableLayoutPanel1.SetColumn(roverButtonB, (marsrover.positionX) - 1)
                                    TableLayoutPanel1.SetRow(roverButtonB, (4 - marsrover.positionY) + 1)
                                    roverButtonB.Text = marsrover.direction
                                End If
                            Catch ex As Exception

                            End Try
                        Else
                            roverbuttonA.Visible = False
                            roverButtonB.Visible = False
                        End If

                        Console.WriteLine()
                        Application.DoEvents()
                    End If
                End If
            Next
            'Console.WriteLine(TextBoxOutput.Text)
            'Console.WriteLine("Complete")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ButtonClose_Click(sender As Object, e As EventArgs) Handles ButtonClose.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBoxTravel.Text = String.Empty
        TextBoxOutput.Text = String.Empty


        TableLayoutPanel1.SetColumn(roverbuttonA, 0)
        TableLayoutPanel1.SetRow(roverbuttonA, 4)
        roverbuttonA.Text = "A"

        TableLayoutPanel1.SetColumn(roverButtonB, 4)
        TableLayoutPanel1.SetRow(roverButtonB, 0)
        roverButtonB.Text = "B"
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub TextBoxTravel_TextChanged(sender As Object, e As EventArgs) Handles TextBoxTravel.TextChanged
        If TextBoxTravel.Text = String.Empty Then
            ButtonRun.Enabled = False
        Else
            ButtonRun.Enabled = True
        End If
    End Sub

#End Region 'RUN

End Class
