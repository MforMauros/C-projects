﻿using System.Data.SqlClient;

namespace Chapter6C_Exercise;

public class DBHelper
{
    private static SqlConnection? _conn;
    public static SqlConnection? GetConnection()
        {
            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var configuration = configurationBuilder.Build();
            string? url = configuration.GetConnectionString("DefaultConnection");

            try
            {
                _conn = new SqlConnection(url);
                return _conn;
            } catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }  
        }

        public static void CloseConnection()
        {
            if(_conn != null)_conn.Close();
        }

}
