Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Menu

Public Class CartItem
    Public Property ProductID As Integer
    Public Property ProductName As String
    Public Property Price As Decimal
    Public Property Quantity As Integer = 1
End Class
Public Class User

    Public Shared User_Login As Boolean = False
    Public Shared Selected_Catalog As String = ""

    Public Shared FullName As String = ""
    Public Shared UserName As String = ""
    Public Shared Email As String = ""
    Public Shared Password As String = ""

    Public Shared Cart As New List(Of CartItem)

End Class
