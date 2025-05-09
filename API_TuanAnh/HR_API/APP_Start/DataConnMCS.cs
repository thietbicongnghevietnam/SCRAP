using System.Data;
using System.Reflection;
using System;
using System.IO;
using System.Data.SqlClient;


namespace HR_API.APP_Start
{
    public class DataConnMCS
    {
        private static SqlCommand? objCommand;
        private static SqlConnection? objCnn;
        public static SqlDataAdapter? da;
        public static string source = "Data Source=192.168.128.130;Initial Catalog=SWMS_DEV;User ID=sa;Password=Psnvdb2013;MultipleActiveResultSets=True;TrustServerCertificate=True;";
        private static SqlConnection con = new SqlConnection(DataConnMCS.source);

        static DataConnMCS()
        {
            try
            {
                DataConnMCS.con.Open();
            }
            catch
            {
            }
        }

        public static DataTable TableWithoutParameter(string storedname)
        {
            DataConnMCS.open();
            DataConnMCS.objCommand = new SqlCommand(storedname, DataConnMCS.objCnn);
            DataConnMCS.objCommand.CommandTimeout = 50;
            DataConnMCS.objCommand.CommandType = CommandType.StoredProcedure;
            DataTable dataTable = new DataTable();
            new SqlDataAdapter()
            {
                SelectCommand = DataConnMCS.objCommand
            }.Fill(dataTable);
            DataConnMCS.close();
            return dataTable;
        }

        private static string GetConnectStringFromFile()
        {
            try
            {
                using (StreamReader streamReader = new StreamReader(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName.ToString() + "\\scnn.ini"))
                    return streamReader.ReadToEnd();
            }
            catch
            {
                return "";
            }
        }

        public static void open()
        {
            DataConnMCS.objCnn = new SqlConnection(DataConnMCS.GetConnectStringFromFile() + "User ID=sa;Password=Psnvdb2013;");
            if (DataConnMCS.objCnn.State == ConnectionState.Open)
                return;
            DataConnMCS.objCnn.Open();
        }

        public static void close()
        {
            try
            {
                if (DataConnMCS.objCnn.State == ConnectionState.Closed)
                    return;
                DataConnMCS.objCnn.Close();
            }
            catch
            {
            }
        }

        public static int ExcuteNonStore(string StoreName, string[] ParameterList, object[] objValue)
        {
            SqlConnection connection = new SqlConnection(DataConnMCS.source);
            int num;
            try
            {
                connection.Open();
                DataConnMCS.objCommand = new SqlCommand(StoreName, connection);
                DataConnMCS.objCommand.CommandType = CommandType.StoredProcedure;
                for (int index = 0; index < ParameterList.Length; ++index)
                    DataConnMCS.objCommand.Parameters.Add(new SqlParameter(ParameterList[index], objValue[index]));
                num = DataConnMCS.objCommand.ExecuteNonQuery();
                connection.Dispose();
                connection.Close();
            }
            catch
            {
                connection.Dispose();
                connection.Close();
                num = 0;
            }
            return num;
        }

        public static int ExcuteNonStore(string StoreName)
        {
            SqlConnection connection = new SqlConnection(DataConnMCS.source);
            int num;
            try
            {
                connection.Open();
                DataConnMCS.objCommand = new SqlCommand(StoreName, connection);
                DataConnMCS.objCommand.CommandType = CommandType.StoredProcedure;
                num = DataConnMCS.objCommand.ExecuteNonQuery();
                connection.Dispose();
                connection.Close();
            }
            catch
            {
                connection.Dispose();
                connection.Close();
                num = 0;
            }
            return num;
        }

        public static DataTable StoreFillDS(string query_object, CommandType type, params object[] obj)
        {
            SqlConnection connection = new SqlConnection(DataConnMCS.source);
            DataTable dataTable = new DataTable();
            try
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(query_object, connection);
                sqlCommand.CommandType = type;
                SqlCommandBuilder.DeriveParameters(sqlCommand);
                for (int index = 1; index <= obj.Length; ++index)
                    sqlCommand.Parameters[index].Value = obj[index - 1];
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
                connection.Dispose();
                connection.Close();
                return dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                connection.Dispose();
                connection.Close();
                return dataTable;
            }
        }

        public static DataSet TablesWithParameter(
          string storedname,
          string[] parameter,
          object[] objVal)
        {
            SqlConnection connection = new SqlConnection(DataConnMCS.source);
            DataSet dataSet1 = new DataSet();
            try
            {
                connection.Open();
                DataSet dataSet2 = new DataSet();
                SqlCommand sqlCommand1 = new SqlCommand();
                SqlCommand sqlCommand2 = new SqlCommand(storedname, connection);
                sqlCommand2.CommandType = CommandType.StoredProcedure;
                for (int index = 0; index < parameter.Length; ++index)
                    sqlCommand2.Parameters.Add(new SqlParameter(parameter[index], objVal[index]));
                DataConnMCS.da = new SqlDataAdapter();
                DataConnMCS.da.SelectCommand = sqlCommand2;
                DataConnMCS.da.Fill(dataSet2);
                connection.Dispose();
                connection.Close();
                return dataSet2;
            }
            catch (Exception ex)
            {
                connection.Dispose();
                connection.Close();
                return dataSet1;
            }
        }

        public static int Execute_NonSQL(string sql)
        {
            SqlConnection connection = new SqlConnection(DataConnMCS.source);
            int num = 0;
            try
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand sqlCommand = new SqlCommand(sql, connection, transaction);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = sql;
                num = sqlCommand.ExecuteNonQuery();
                transaction.Commit();
                connection.Dispose();
                connection.Close();
                return num;
            }
            catch (Exception ex)
            {
                connection.Dispose();
                connection.Close();
                return num;
            }
        }

        public static bool ExcuteStored_bool(string storedname, string[] parameter, object[] objVal)
        {
            int num = 0;
            SqlConnection connection = new SqlConnection(DataConnMCS.source);
            try
            {
                connection.Open();
                SqlCommand sqlCommand1 = new SqlCommand();
                SqlCommand sqlCommand2 = new SqlCommand(storedname, connection);
                sqlCommand2.CommandType = CommandType.StoredProcedure;
                sqlCommand2.CommandTimeout = 0;
                sqlCommand2.Parameters.Clear();
                for (int index = 0; index < parameter.Length; ++index)
                    sqlCommand2.Parameters.Add(new SqlParameter(parameter[index], objVal[index]));
                num = sqlCommand2.ExecuteNonQuery();
                connection.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Dispose();
                connection.Close();
            }
            return num > 0;
        }

        public static DataTable TableWithParameter(
          string storedname,
          string[] parameter,
          object[] objVal)
        {
            SqlConnection connection = new SqlConnection(DataConnMCS.source);
            DataTable dataTable = new DataTable();
            try
            {
                connection.Open();
                SqlCommand sqlCommand1 = new SqlCommand();
                SqlCommand sqlCommand2 = new SqlCommand(storedname, connection);
                sqlCommand2.CommandType = CommandType.StoredProcedure;
                for (int index = 0; index < parameter.Length; ++index)
                    sqlCommand2.Parameters.Add(new SqlParameter(parameter[index], objVal[index]));
                new SqlDataAdapter() { SelectCommand = sqlCommand2 }.Fill(dataTable);
            }
            catch (Exception ex)
            {
                connection.Dispose();
                connection.Close();
                throw ex;
            }
            connection.Dispose();
            connection.Close();
            return dataTable;
        }

        public static DataTable SelectStore(string strStoreName, string[] strPara, object[] objValue)
        {
            SqlConnection connection = new SqlConnection(DataConnMCS.source);
            DataTable dataTable = new DataTable();
            try
            {
                connection.Open();
                SqlCommand sqlCommand1 = new SqlCommand();
                SqlCommand sqlCommand2 = new SqlCommand(strStoreName, connection);
                sqlCommand2.CommandTimeout = 60;
                sqlCommand2.CommandType = CommandType.StoredProcedure;
                for (int index = 0; index < strPara.Length; ++index)
                    sqlCommand2.Parameters.Add(new SqlParameter(strPara[index], objValue[index]));
                new SqlDataAdapter() { SelectCommand = sqlCommand2 }.Fill(dataTable);
            }
            catch (Exception ex)
            {
                dataTable = (DataTable)null;
            }
            finally
            {
                connection.Dispose();
                connection.Close();
            }
            return dataTable;
        }

        public static string GetExcuteScalar_string(
          string storedname,
          string[] parameter,
          object[] objVal)
        {
            SqlConnection connection = new SqlConnection(DataConnMCS.source);
            object obj;
            try
            {
                connection.Open();
                SqlCommand sqlCommand1 = new SqlCommand();
                SqlCommand sqlCommand2 = new SqlCommand(storedname, connection);
                sqlCommand2.CommandType = CommandType.StoredProcedure;
                for (int index = 0; index < parameter.Length; ++index)
                    sqlCommand2.Parameters.Add(new SqlParameter(parameter[index], objVal[index]));
                obj = sqlCommand2.ExecuteScalar();
                connection.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Dispose();
                connection.Close();
                obj = (object)"0";
            }
            return Convert.ToString(obj);
        }

        public static int ExcuteScara(string StoreName, string[] ParameterList, object[] objValue)
        {
            SqlConnection connection = new SqlConnection(DataConnMCS.source);
            int num;
            try
            {
                connection.Open();
                SqlCommand sqlCommand1 = new SqlCommand();
                SqlCommand sqlCommand2 = new SqlCommand(StoreName, connection);
                sqlCommand2.CommandType = CommandType.StoredProcedure;
                for (int index = 0; index < ParameterList.Length; ++index)
                    sqlCommand2.Parameters.Add(new SqlParameter(ParameterList[index], objValue[index]));
                num = int.Parse(sqlCommand2.ExecuteScalar().ToString());
                connection.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Dispose();
                connection.Close();
                num = -1;
            }
            return num;
        }
    }
}
