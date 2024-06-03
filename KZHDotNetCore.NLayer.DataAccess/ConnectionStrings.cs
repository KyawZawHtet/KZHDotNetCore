using System.Data.SqlClient;

namespace KZHDotNetCore.NLayer.DataAccess;

internal static class ConnectionStrings
{
    public static readonly SqlConnectionStringBuilder SqlConnectionStringBuilder = new SqlConnectionStringBuilder()
    {
        DataSource = "DESKTOP-QMAAE64", // Server Name
        InitialCatalog = "KZHDotNetCore", // Database Name
        UserID = "sa",
        Password = "Kyaw279!",
        TrustServerCertificate = true
    };
}