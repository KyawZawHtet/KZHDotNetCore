using System.Data.SqlClient;
// ReSharper disable All

namespace KZHDotNetCore.ConsoleApp;

internal static class ConnectionStrings
{
    public static SqlConnectionStringBuilder SqlConnectionStringBuilder = new SqlConnectionStringBuilder()
    {
        DataSource = "DESKTOP-QMAAE64", // Server Name
        InitialCatalog = "KZHDotNetCore", // Database Name
        UserID = "sa",
        Password = "Kyaw279!",
        TrustServerCertificate = true
    };
}