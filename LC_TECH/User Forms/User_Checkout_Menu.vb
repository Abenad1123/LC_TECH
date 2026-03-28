Public Class User_Checkout_Menu
    Dim amountPaid As Integer = 0
    Dim change As Integer = 0

    Dim totalAmount As Decimal = 0
    Dim totalQuantity As Integer = 0
    Dim subtotal As Decimal = 0
    Private Sub Form_Load_Standard(sender As Object, e As EventArgs) Handles MyBase.Load
        SetPlaceholder()
        Product_DropBox.SelectedIndex = 0
    End Sub

    Private Sub Form_Shown_Standard(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.ActiveControl = Nothing
    End Sub

    Private Sub SearchBar_Text_Enter(sender As Object, e As EventArgs) Handles SearchBar_Text.Enter
        If SearchBar_Text.Text = " Search for products" Then
            SearchBar_Text.Text = ""
            SearchBar_Text.ForeColor = Color.Black
        End If
    End Sub

    Private Sub SearchBar_Text_Leave(sender As Object, e As EventArgs) Handles SearchBar_Text.Leave
        If SearchBar_Text.Text.Trim() = "" Then SetPlaceholder()
    End Sub

    Private Sub SetPlaceholder()
        SearchBar_Text.Text = " Search for products"
        SearchBar_Text.ForeColor = ColorTranslator.FromHtml("#adadad")
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        If Not User.User_Login Then
            OpenForm(Of Login_Menu)(Me)
        Else
            OpenForm(Of User_Page_Menu)(Me)
        End If
    End Sub

    Private Sub PictureBox1_Click_1(sender As Object, e As EventArgs) Handles PictureBox1.Click
        OpenForm(Of Main_Menu)(Me)
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        OpenForm(Of User_Cart_Menu)(Me)
    End Sub

    Private Sub Product_DropBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Product_DropBox.SelectionChangeCommitted
        User.Selected_Catalog = Product_DropBox.SelectedItem.ToString()
        OpenForm(Of Catalog_Select_Menu)(Me)
    End Sub

    '------------ END OF INITIALIZATION ------------
    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshCartUI()
    End Sub

    Public Sub StyleCartGrid(dgv As DataGridView)

        With dgv
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .ReadOnly = True
            .AllowUserToAddRows = False
            .RowHeadersVisible = False
            .BackgroundColor = Color.White
            .BorderStyle = BorderStyle.None
            .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal

            .EnableHeadersVisualStyles = False
            .ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50)
            .ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
            .ColumnHeadersHeight = 35

            .DefaultCellStyle.Font = New Font("Segoe UI", 10)
            .DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 200, 200)
            .DefaultCellStyle.SelectionForeColor = Color.Black

            .AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245)

            .Columns("ProductID").HeaderText = "ID"
            .Columns("ProductName").HeaderText = "Product Name"
            .Columns("Price").HeaderText = "Price"
            .Columns("Quantity").HeaderText = "Qty"
            .Columns("TotalPrice").HeaderText = "Total"

            .Columns("Price").DefaultCellStyle.Format = "₱#,0.00"
            .Columns("TotalPrice").DefaultCellStyle.Format = "₱#,0.00"

            .Columns("ProductID").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Columns("Quantity").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

            .Columns("ProductID").FillWeight = 15
            .Columns("ProductName").FillWeight = 45
            .Columns("Price").FillWeight = 15
            .Columns("Quantity").FillWeight = 15
            .Columns("TotalPrice").FillWeight = 20
        End With

    End Sub

    Public Sub LoadCartToGrid(dgv As DataGridView)

        dgv.DataSource = Nothing

        Dim cartDisplay = User.Cart.Select(Function(item) New With {
            .ProductID = item.ProductID,
            .ProductName = item.ProductName,
            .Price = item.Price,
            .Quantity = item.Quantity,
            .TotalPrice = item.Price * item.Quantity
        }).ToList()

        dgv.DataSource = cartDisplay

    End Sub

    Public Sub LoadCartSummary()
        Dim shippingFee As Decimal = If(subtotal >= 1000, 0, 100)

        For Each item In User.Cart
            totalQuantity += item.Quantity
            subtotal += item.Price * item.Quantity
        Next


        totalAmount = subtotal + shippingFee

        Label6.Text = totalQuantity.ToString()
        Label7.Text = "₱" & subtotal.ToString("N2")
        Label8.Text = "₱" & shippingFee.ToString("N2")
        Label9.Text = "₱" & totalAmount.ToString("N2")

        Label15.Text = GenerateRandomID()
        Label16.Text = "₱" & amountPaid.ToString("N2")
        Label17.Text = "₱" & change.ToString("N2")

    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Public Function GenerateRandomID(Optional length As Integer = 10) As String

        Dim chars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
        Dim rnd As New Random()
        Dim result As String = ""

        For i As Integer = 1 To length
            result &= chars(rnd.Next(0, chars.Length))
        Next

        Return result

    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim amount As Integer = CInt(TextBox1.Text)

        If (amount < totalAmount) Then
            MsgBox("You need to input higher amount than the total")
            Exit Sub
        End If

        If (TextBox2.Text = "") Then
            MsgBox("Enter an address before continuing")
        End If

        MsgBox("Payment success!")

        change = totalAmount - amount

        Label16.Text = "₱" & amountPaid.ToString("N2")
        Label17.Text = "₱" & change.ToString("N2")

        User.Cart.Clear()

        amountPaid = 0
        change = 0
        totalQuantity = 0
        totalAmount = 0
        subtotal = 0
        RefreshCartUI()
    End Sub

    Private Sub RefreshCartUI()

        LoadCartToGrid(DataGridView1)
        StyleCartGrid(DataGridView1)
        LoadCartSummary()

    End Sub


End Class