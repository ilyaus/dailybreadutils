using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace DailyBreadUtil
{
    class data_manager
    {
        private SqlConnection connection = null;
        private SqlCommand cmd = null;

        public data_manager()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void ExecuteQuery(string connectionString, string sqlString, ref SqlDataReader data)
        {
            if (connection != null)
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            connection = new SqlConnection(connectionString);
            cmd = new SqlCommand();

            //SqlDataReader data_reader;

            try
            {
                connection.Open();

                cmd.Connection = connection;
                cmd.CommandText = sqlString;

                data = cmd.ExecuteReader();

                cmd.Dispose();

                //return data_reader;
            }
            catch (SqlException ex)
            { }

            //return null;
        }

        public SqlDataReader ExecuteQuery(string connectionString, string sqlString)
        {
            SqlDataReader data = null;

            if (connection != null)
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            connection = new SqlConnection(connectionString);
            cmd = new SqlCommand();

            //SqlDataReader data_reader;

            try
            {
                connection.Open();

                cmd.Connection = connection;
                cmd.CommandText = sqlString;

                data = cmd.ExecuteReader();

                cmd.Dispose();

                //return data_reader;
            }
            catch (SqlException ex)
            { }

            return data;
        }

        public bool ExecuteNonQuery(string connectionString, string sqlString)
        {
            if (connection != null)
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            connection = new SqlConnection(connectionString);
            cmd = new SqlCommand();

            try
            {
                connection.Open();

                cmd.Connection = connection;
                cmd.CommandText = sqlString;

                cmd.ExecuteNonQuery();

                cmd.Dispose();
                connection.Close();
                connection.Dispose();

                //return data_reader;
            }
            catch (SqlException ex)
            { }

            return true;
        }

        public bool Close()
        {
            if (connection != null)
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            return true;
        }
    }
}
