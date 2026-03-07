Public Class Basic
    Public Shared Catalogs() As String = {"All", "Peripherals", "Components", "Accessories", "Laptops", "Pre Built PCs"}

    Public Shared Brands()() As String = {
        New String() {"ASUS", "ViewSonic", "Gamdias", "LC"},
        New String() {"Cooler Master", "Tecware", "NZXT", "DeepCool", "Intel", "AMD", "ASUS", "MSI", "Gigabyte", "ASUS", "NVIDIA", "Kingston", "Corsair", "G.Skill", "TeamGroup"}
    }

    Public Shared UserConnectionString As String = "Data Source=CORE;Initial Catalog=LC_USER;Integrated Security=True"
    Public Shared ProductsInfoConnectionString As String = "Data Source=CORE;Initial Catalog=LC_PRODUCTS;Integrated Security=True"
End Class