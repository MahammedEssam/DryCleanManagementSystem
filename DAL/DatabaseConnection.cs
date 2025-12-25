using Microsoft.Extensions.Configuration;
using System;
using System.IO;

public static class DatabaseConnection
{
    private readonly static string _connectionString;

    public static DatabaseConnection()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        var configuration = builder.Build();
        _connectionString = configuration.GetConnectionString("DryCleanDB");

        if (string.IsNullOrEmpty(_connectionString))
            throw new Exception("Connection string is empty! تحقق من appsettings.json");
    }

    // ترجع Connection String بدل SqlConnection
    public static string ConnectionString()
    {
        return _connectionString;
    }
}
