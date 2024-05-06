using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace KZHDotNetCore.Shared;

public class DapperService
{
    private readonly string _connectionString;
    public DapperService(string connectionString)
    {
        _connectionString = connectionString;
    }
    public List<T> Query<T>(string query, object? param = null)
    {
        using IDbConnection dbConnection = new SqlConnection(_connectionString);

        // if (param != null)
        // {
        //     var list = dbConnection.Query<T>(query, param).ToList();
        // }
        // else
        // {
        //     var list = dbConnection.Query<T>(query).ToList();
        // }
        
        var list = dbConnection.Query<T>(query, param).ToList();
        return list;
    }
    
    public T QueryFirstOrDefault<T>(string query, object? param = null)
    {
        using IDbConnection dbConnection = new SqlConnection(_connectionString);

        // if (param != null)
        // {
        //     var list = dbConnection.Query<T>(query, param).ToList();
        // }
        // else
        // {
        //     var list = dbConnection.Query<T>(query).ToList();
        // }
        
        var list = dbConnection.Query<T>(query, param).FirstOrDefault();
        return list!;
    }

    public int Execute(string query, object? param = null)
    {
        using IDbConnection dbConnection = new SqlConnection(_connectionString);
        var result = dbConnection.Execute(query, param);
        return result;
    }
}