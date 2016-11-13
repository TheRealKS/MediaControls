Imports System.Runtime.InteropServices
Imports WindowsHookLib
Public Class Form1
    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
    End Function
    Const WM_APPCOMMAND As UInteger = &H319
    Const APPCOMMAND_VOLUME_UP As UInteger = &HA
    Const APPCOMMAND_VOLUME_DOWN As UInteger = &H9
    Const APPCOMMAND_VOLUME_MUTE As UInteger = &H8

    Dim WithEvents gkh As New KeyboardHook
    Private Sub keyDownHandler(sender As Object, e As WindowsHookLib.KeyboardEventArgs) Handles gkh.KeyDown
        If (e.KeyCode = Keys.Pause) Then
            Dim command As UInteger = Convert.ToUInt32("14")
            SendMessage(Me.Handle, WM_APPCOMMAND, &H30292, command * &H10000)
        ElseIf (e.Shift AndAlso (e.KeyCode = Keys.PageUp)) Then
            SendMessage(Me.Handle, WM_APPCOMMAND, &H30292, APPCOMMAND_VOLUME_UP * &H10000)
        ElseIf (e.Shift AndAlso (e.KeyCode = Keys.PageDown)) Then
            SendMessage(Me.Handle, WM_APPCOMMAND, &H30292, APPCOMMAND_VOLUME_DOWN * &H10000)
        ElseIf (e.Shift AndAlso (e.KeyCode = Keys.End)) Then
            Dim command As UInteger = Convert.ToUInt32("11")
            SendMessage(Me.Handle, WM_APPCOMMAND, &H30292, command * &H10000)
        ElseIf (e.Shift AndAlso (e.KeyCode = Keys.Home)) Then
            Dim command As UInteger = Convert.ToUInt32("12")
            SendMessage(Me.Handle, WM_APPCOMMAND, &H30292, command * &H10000)
        End If
    End Sub
    Private Sub FormCloseHandler(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If CheckBox1.Checked = True Then
            NotifyIcon1.Visible = True
            Hide()
            e.Cancel = True
        Else
            Hide()
        End If
    End Sub
    Private Sub FormMinimizeHandler(sender As Object, e As EventArgs) Handles Me.SizeChanged
        If WindowState = FormWindowState.Minimized Then
            If CheckBox1.Checked = True Then
                NotifyIcon1.Visible = True
                Hide()
            Else
                Hide()
            End If
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        gkh.InstallHook()
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Visible = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        NotifyIcon1.Visible = False
        End
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If CheckBox1.Checked = True Then
            NotifyIcon1.Visible = True
            Hide()
        Else
            Hide()
        End If
    End Sub
End Class
