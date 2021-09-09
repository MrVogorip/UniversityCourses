using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Wrapper
{
    public class DBWrapper
    {
        private SqlConnection oSqlCon;
        private SqlCommand oSqlCom;
        private SqlDataAdapter oSqlDtAdptr;
        private SqlConnectionStringBuilder oConStringBuilder;
        public void Procedure( string sqlExp, string fullName, string nameOperation, int cost, string date)
        {
            string sqlExpression = sqlExp;
            if (oSqlCon.State != ConnectionState.Open)
            {
                oSqlCon.Open();
            }
            SqlCommand command = new SqlCommand(sqlExpression, oSqlCon);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter name = new SqlParameter
            {
                ParameterName = "@name",
                Value = fullName
            };
            command.Parameters.Add(name);
            SqlParameter operation = new SqlParameter
            {
                ParameterName = "@operation",
                Value = nameOperation
            };
            command.Parameters.Add(operation);
            SqlParameter value = new SqlParameter
            {
                ParameterName = "@cost",
                Value = cost
            };
            command.Parameters.Add(value);
            SqlParameter time = new SqlParameter
            {
                ParameterName = "@time",
                Value = date
            };
            command.Parameters.Add(time);
            var result = command.ExecuteScalar();
        }
    
        public List<List<string>> ExecuteQuery(string SqlCommandAsString)
        {
            var result = new List<List<string>>();
            try
            {
                if (oSqlCon.State != ConnectionState.Open)
                {
                    oSqlCon.Open();
                }
                var reader = new SqlCommand(SqlCommandAsString, oSqlCon).ExecuteReader();
                int row = 0;

                while (reader.Read())
                {
                    var currentRow = new List<string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        currentRow.Add(reader[i].ToString());
                    }
                    result.Add(currentRow);
                    row++;
                }
                reader.Close();
                return result;
            }
            catch (Exception ex)
            {
                return new List<List<string>>() { new List<string>() { ex.Message.ToString() } };
            }
        }

        public DBWrapper(string sqlInstanceName, string dbName)
        {
            oConStringBuilder = new SqlConnectionStringBuilder();
            oConStringBuilder.DataSource = sqlInstanceName;
            oConStringBuilder.InitialCatalog = dbName;
            oConStringBuilder.IntegratedSecurity = true;
            oSqlCon = new SqlConnection();
            oSqlCon.ConnectionString = oConStringBuilder.ConnectionString;
        }

        public DBWrapper(string sqlInstanceName, string dbName, string dbUserName = "", string dbPass = "")
        {
            oConStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = sqlInstanceName,
                InitialCatalog = dbName,
                UserID = dbUserName,
                Password = dbPass
            };
            oSqlCon = new SqlConnection();
            oSqlCon.ConnectionString = oConStringBuilder.ConnectionString;
        }

        public DataTable FillDataSet(string SqlSelectCommandAsString)
        {
            try
            {
                if (oSqlCon.State != ConnectionState.Open)
                {
                    oSqlCon.Open();
                }
                var ds = new DataTable();
                using (oSqlCom = new SqlCommand())
                {
                    oSqlCom.Connection = oSqlCon;
                    oSqlCom.CommandText = SqlSelectCommandAsString;
                    using (oSqlDtAdptr = new SqlDataAdapter())
                    {
                        oSqlDtAdptr.SelectCommand = oSqlCom;
                        oSqlDtAdptr.Fill(ds);
                        return ds;
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                if (oSqlCon.State == ConnectionState.Open)
                {
                    oSqlCon.Close();
                }
            }
        }
    }
}
