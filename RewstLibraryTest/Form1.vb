Imports System.Net.Http
Imports System.Net.Http.Json

Public Class Form1

    Const VAULT As String = "https://akv-2-isuztref6n4i4ee.vault.azure.net/"

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load


    End Sub

    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Using hook As New Rewst.Webhook(TextBox1.Text, VAULT, TextBox2.Text)
            Dim rulelist As RuleCollection = Await hook.CallAsync(Of RuleCollection)(HttpMethod.Get)
            For Each r In rulelist.Rules
                RichTextBox1.AppendText(r.Name)
                RichTextBox1.AppendText(vbCrLf)
            Next
        End Using
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Using hook As New Rewst.Webhook(TextBox1.Text, VAULT, TextBox2.Text)
            Dim d As New Doohicky
            d.Name = "Doohicky"
            d.Id = 123
            d.Created = Now

            Dim result = hook.Call(Of Doohicky, Hashtable)(HttpMethod.Post, d)
            For Each key In result.Keys
                RichTextBox1.AppendText($"{key}={result(key)}")
                RichTextBox1.AppendText(vbCrLf)
            Next

        End Using
    End Sub
End Class

Public Class Doohicky
    Public Property Name As String
    Public Property Id As Integer
    Public Property Created As Date
End Class

Public Class RuleCollection
    Public Property Rules As New List(Of Rule)
End Class

Public Class Rule
    Public Property Name As String
    Public Property Triggers As New List(Of ValueComparison)
    Public Property Conditions As New List(Of ValueComparison)
    Public Property Actions As New List(Of RuleAction)
End Class

Public Class ValueComparison
    Public Source As String
    Public Compare As String
    Public Value As String
End Class

Public Class RuleAction
    Public Type As String
    Public Args As New List(Of String)
End Class