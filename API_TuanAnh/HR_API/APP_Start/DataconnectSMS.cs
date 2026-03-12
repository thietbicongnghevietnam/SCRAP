using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace HR_API.APP_Start
{
    //public class DataconnectSMS
    //{
    //}
    public class DataconnectSMS
    {
        private static SqlCommand objCommand;
        private static SqlConnection objCnn;
        public static SqlDataAdapter da;
        public static string source = "Data Source=192.168.128.1;Initial Catalog=SMSVersion3;User ID=sa;Password=Psnvdb2013;MultipleActiveResultSets=True;TrustServerCertificate=True;";
        private static SqlConnection con = new SqlConnection(DataconnectSMS.source);

        public static string connection_string = "Data Source=192.168.128.1;Initial Catalog=SMSVersion3;User ID=scan;Password=khong123";

        static DataconnectSMS()
        {
            try
            {
                DataconnectSMS.con.Open();
            }
            catch
            {
            }
        }

        public static DataTable TableWithoutParameter(string storedname)
        {
            DataconnectSMS.open();
            DataconnectSMS.objCommand = new SqlCommand(storedname, DataconnectSMS.objCnn);
            DataconnectSMS.objCommand.CommandTimeout = 50;
            DataconnectSMS.objCommand.CommandType = CommandType.StoredProcedure;
            DataTable dataTable = new DataTable();
            new SqlDataAdapter()
            {
                SelectCommand = DataconnectSMS.objCommand
            }.Fill(dataTable);
            DataconnectSMS.close();
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
            DataconnectSMS.objCnn = new SqlConnection(DataconnectSMS.GetConnectStringFromFile() + "User ID=scan;Password=khong123;");
            if (DataconnectSMS.objCnn.State == ConnectionState.Open)
                return;
            DataconnectSMS.objCnn.Open();
        }

        public static void close()
        {
            try
            {
                if (DataconnectSMS.objCnn.State == ConnectionState.Closed)
                    return;
                DataconnectSMS.objCnn.Close();
            }
            catch
            {
            }
        }

        public static int ExcuteNonStore(string StoreName, string[] ParameterList, object[] objValue)
        {
            SqlConnection connection = new SqlConnection(DataconnectSMS.source);
            int num;
            try
            {
                connection.Open();
                DataconnectSMS.objCommand = new SqlCommand(StoreName, connection);
                DataconnectSMS.objCommand.CommandType = CommandType.StoredProcedure;
                for (int index = 0; index < ParameterList.Length; ++index)
                    DataconnectSMS.objCommand.Parameters.Add(new SqlParameter(ParameterList[index], objValue[index]));
                num = DataconnectSMS.objCommand.ExecuteNonQuery();
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
            SqlConnection connection = new SqlConnection(DataconnectSMS.source);
            int num;
            try
            {
                connection.Open();
                DataconnectSMS.objCommand = new SqlCommand(StoreName, connection);
                DataconnectSMS.objCommand.CommandType = CommandType.StoredProcedure;
                num = DataconnectSMS.objCommand.ExecuteNonQuery();
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

        public static int ExcuteStored_int(string storedname, string[] parameter, object[] objVal)
        {
            SqlConnection connection = new SqlConnection(DataconnectSMS.source);
            int num;
            try
            {
                connection.Open();
                SqlCommand sqlCommand1 = new SqlCommand();
                SqlCommand sqlCommand2 = new SqlCommand(storedname, connection);
                sqlCommand2.CommandTimeout = 120;
                sqlCommand2.CommandType = CommandType.StoredProcedure;
                sqlCommand2.Parameters.Clear();
                for (int index = 0; index < parameter.Length; ++index)
                    sqlCommand2.Parameters.Add(new SqlParameter(parameter[index], objVal[index]));
                num = sqlCommand2.ExecuteNonQuery();
                connection.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return num;
        }

        public static DataTable StoreFillDS(string query_object, CommandType type, params object[] obj)
        {
            SqlConnection connection = new SqlConnection(DataconnectSMS.source);
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
            SqlConnection connection = new SqlConnection(DataconnectSMS.source);
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
                DataconnectSMS.da = new SqlDataAdapter();
                DataconnectSMS.da.SelectCommand = sqlCommand2;
                DataconnectSMS.da.Fill(dataSet2);
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
            SqlConnection connection = new SqlConnection(DataconnectSMS.source);
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
            SqlConnection connection = new SqlConnection(DataconnectSMS.source);
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
            SqlConnection connection = new SqlConnection(DataconnectSMS.source);
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
            SqlConnection connection = new SqlConnection(DataconnectSMS.source);
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
            SqlConnection connection = new SqlConnection(DataconnectSMS.source);
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

        public static int excutenonquerry(string query_object, CommandType type, params object[] obj)
        {
            try
            {
                int data = 0;
                using (SqlConnection conn = new SqlConnection(connection_string))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query_object, conn);
                    cmd.CommandType = type;
                    SqlCommandBuilder.DeriveParameters(cmd);
                    for (int i = 1; i <= obj.Length; i++)
                    {
                        cmd.Parameters[i].Value = obj[i - 1];
                    }
                    data = cmd.ExecuteNonQuery();
                    conn.Close();
                    return data;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public static int ExcuteScara(string StoreName, string[] ParameterList, object[] objValue)
        {
            SqlConnection connection = new SqlConnection(DataconnectSMS.source);
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
