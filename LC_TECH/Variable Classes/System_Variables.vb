Public Class Basic
    Public Shared Catalogs() As String = {"All", "Peripherals", "Components", "Accessories", "Laptops", "Pre Built PCs"}

    Public Shared Brands()() As String = {
        New String() {"ASUS", "ViewSonic", "Gamdias", "LC"},
        New String() {"Cooler Master", "Tecware", "NZXT", "DeepCool", "Intel", "AMD", "ASUS", "MSI", "Gigabyte", "ASUS", "NVIDIA", "Kingston", "Corsair", "G.Skill", "TeamGroup"}
    }

    Public Shared conString As String = "Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=LC_TECH;Integrated Security=True;"
    ' Public Shared conString As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\LC_TECH.mdf;Integrated Security=True;"

    Public Shared Open_admin_login As Integer = 0
End Class