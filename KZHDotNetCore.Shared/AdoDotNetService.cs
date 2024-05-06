using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

namespace KZHDotNetCore.Shared
{
    public class AdoDotNetService
    {
        private readonly string _connectionString;

        public AdoDotNetService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<M> Query<M>(string query, params AdoDotNetParameter[]? parameters)
        {
            
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);
            if (parameters is not null && parameters.Length > 0)
            {
                // foreach (var item in parameters)
                // {
                //     command.Parameters.AddWithValue(item.Name, item.Value);
                // }
                
                // command.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray());

                var parametersArray = parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray();
                command.Parameters.AddRange(parametersArray);
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            connection.Close();

            string json = JsonConvert.SerializeObject(dataTable); // C# to json
            List<M> list = JsonConvert.DeserializeObject<List<M>>(json)!; // json to C#

            return list;
        }
        
        public M QueryFirstOrDefault<M>(string query, params AdoDotNetParameter[]? parameters)
        {
            
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);
            if (parameters is not null && parameters.Length > 0)
            {
                // foreach (var item in parameters)
                // {
                //     command.Parameters.AddWithValue(item.Name, item.Value);
                // }
                
                // command.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray());

                var parametersArray = parameters.Select(item => new SqlParameter(item.Name, item.Value ?? DBNull.Value)).ToArray();
                command.Parameters.AddRange(parametersArray);
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            connection.Close();

            string json = JsonConvert.SerializeObject(dataTable); // C# to json
            List<M> list = JsonConvert.DeserializeObject<List<M>>(json) ?? new List<M>(); // json to C#

            return list.FirstOrDefault()!;
        }
        
        public int Execute(string query, params AdoDotNetParameter[]? parameters)
        {
            
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);
            if (parameters is not null && parameters.Length > 0)
            {
                // foreach (var item in parameters)
                // {
                //     command.Parameters.AddWithValue(item.Name, item.Value);
                // }
                
                // command.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray());

                var parametersArray = parameters.Select(item => new SqlParameter(item.Name, item.Value ?? DBNull.Value)).ToArray();
                command.Parameters.AddRange(parametersArray);
            }
            // SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            // DataTable dataTable = new DataTable();
            // sqlDataAdapter.Fill(dataTable);

            var result = command.ExecuteNonQuery();

            connection.Close();

            return result;
        }
    }

    public class AdoDotNetParameter
    {
        public AdoDotNetParameter(){}

        public AdoDotNetParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }
        public string Name { get; set; }
        
        public object Value { get; set; }
    }
}