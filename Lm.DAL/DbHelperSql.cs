using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Lm.CommonLib;

namespace Lm.DAL
{
    /// <summary>
    /// Sql Server������
    /// </summary>
    public class DbHelperSql
    {
        //���ݿ������ַ���
        //private System.Data.SqlClient.SqlDataAdapter adapter = new SqlDataAdapter();
        //#if DEBUG
        //        public static readonly string ConnectionString = "server=.;database=demoDB;uid=sa;pwd=sa2005;";
        //#else
        private static readonly string ConnectionString = Config.PlatformConnectionString(false);
        //#endif
        public DbHelperSql()
        {
            //TODO:���캯��
        }
        #region �ۺϺ���

        /// <summary>
        /// ��ȡ����ĳ�ֶε����ֵ
        /// </summary>
        /// <param name="tableName">����</param>
        /// <param name="fieldName">�ֶ���</param>
        /// <returns>���ֶ��ڱ�������ֵ</returns>
        public static int GetMaxId(string tableName, string fieldName)
        {
            string query = "select max(" + fieldName + ") from " + tableName;
            return ExecuteScalar(CommandType.Text, query, null);
        }

        public static int ExecuteScalar(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteScalar(ConnectionString, cmdType, cmdText, commandParameters);
        }

        public static int ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, cmdType, cmdText, commandParameters);
                object obj = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return ObjectToInt32(obj);
            }
            catch (Exception ex)
            {
                MessageLog.AddErrorLogDbSql("DbHelperSql", "ExecuteScalar:" + ex.Message);
                return -1;
            }
        }

        public static int ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    PrepareCommand(cmd, connection, cmdType, cmdText, commandParameters);
                    object obj = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    return ObjectToInt32(obj);
                }
            }
            catch (Exception ex)
            {
                MessageLog.AddErrorLogDbSql("DbHelperSql", "ExecuteScalar:" + ex.Message);
                return -1;
            }
        }
        #endregion

        #region ����DELETE��UPDATE��INSERT���� ExecuteNonQuery
        /// <summary>
        /// ��������ִ�гɹ�״̬ ��������  
        /// INSERT ��䷵�ص�ǰ�����Զ���������ֵ SELECT @@identity; 
        /// </summary>
        /// <param name="strSql">ִ�����</param>
        /// <returns>ִ��״̬ �ɹ�>0</returns>
        public static int ExecuteNonQuery(string strSql)
        {
            return ExecuteNonQuery(CommandType.Text, strSql, null);
        }
        /// <summary>
        /// ��������ִ�гɹ�״̬ ������
        /// </summary>
        /// <param name="strSql">ִ�����</param>
        /// <param name="cmdParams">����</param>
        /// <returns>ִ��״̬ �ɹ�>0</returns>
        public static int ExecuteNonQuery(string strSql, params SqlParameter[] cmdParams)
        {
            return ExecuteNonQuery(CommandType.Text, strSql, cmdParams);
        }
        /// <summary>
        /// ִ��SQL����
        /// </summary>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdText">�������</param>
        /// <param name="commandParameters">����</param>
        /// <returns></returns>
        private static int ExecuteNonQuery(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteNonQuery(ConnectionString, cmdType, cmdText, commandParameters);
        }

        private static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandTimeout = 60;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    PrepareCommand(cmd, connection, cmdType, cmdText, commandParameters);
                    int num = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return num;
                }
            }
            catch (Exception ex)
            {
                MessageLog.AddErrorLogDbSql("DbHelperSql", "ExecuteNonQuery:" + ex.Message);
                return -1;
            }

        }
        public static int ExecuteNonQuery(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
            catch (Exception ex)
            {
                MessageLog.AddErrorLogDbSql("DbHelperSql", "ExecuteNonQuery:" + ex.Message);
                return -1;
            }
        }
        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
            catch (Exception ex)
            {
                MessageLog.AddErrorLogDbSql("DbHelperSql", "ExecuteNonQuery:" + ex.Message);
                return -1;
            }
        }
        #endregion

        #region ���� DataSet DataTable SqlDataReader
        /// <summary>
        /// ִ�в�ѯ������ѯ�����䵽DataSet
        /// </summary>
        /// <param name="cmdType">�������� ��sql��ѯ����洢����</param>
        /// <param name="cmdText">�����ַ���</param>
        /// <param name="commandParameters">����</param>
        /// <returns>��ѯ���DataSet</returns>
        public static DataSet ExecuteDataSet(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteDataSet(ConnectionString, cmdType, cmdText, commandParameters);
        }

        /// <summary>
        /// ִ�в�ѯ������ѯ�����䵽DataSet
        /// </summary>
        /// <param name="connectionString">�����ַ���</param>
        /// <param name="cmdType">��������</param>
        /// <param name="cmdText">�������</param>
        /// <param name="commandParameters">����</param>
        /// <returns>��ѯ���</returns>
        public static DataSet ExecuteDataSet(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, cmdType, cmdText, commandParameters);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                cmd.Parameters.Clear();
                adapter.Dispose();
                return dataSet;
            }
        }

        public static DataTable ExecuteTable(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, connection, cmdType, cmdText, commandParameters);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Result");
                cmd.Parameters.Clear();
                return dataSet.Tables.Count > 0 ? dataSet.Tables["Result"] : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static DataTable ExecuteTable(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    PrepareCommand(cmd, connection, cmdType, cmdText, commandParameters);
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "Result");
                    cmd.Parameters.Clear();
                    return dataSet.Tables.Count > 0 ? dataSet.Tables["Result"] : null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }

        public static SqlDataReader ExecuteReader(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteReader(ConnectionString, cmdType, cmdText, commandParameters);
        }

        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                return null;
            }
        }
        #endregion

        #region �����洢���� ����
        /// <summary>
        /// ��������ִ�гɹ�״̬ ��������
        /// </summary>
        /// <param name="strSql">ִ�����</param>
        /// <returns>ִ��״̬ �ɹ�>0</returns>
        public static int ExecuteNonQueryBySP(string strSql)
        {
            return ExecuteNonQuery(CommandType.StoredProcedure, strSql, null);
        }
        /// <summary>
        /// ��������ִ�гɹ�״̬ ������
        /// </summary>
        /// <param name="strSql">ִ�����</param>
        /// <param name="cmdParams">����</param>
        /// <returns>ִ��״̬ �ɹ�>0</returns>
        public static int ExecuteNonQueryBySP(string strSql, params SqlParameter[] cmdParams)
        {
            return ExecuteNonQuery(CommandType.StoredProcedure, strSql, cmdParams);
        }

        public static SqlDataReader ExecuteReaderBySP(string storedProcedureName, SqlParameter[] cmdParams, SqlTransaction trans)
        {
            SqlCommand command = new SqlCommand();
            try
            {
                command.Transaction = trans;
                command.CommandText = storedProcedureName;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = trans.Connection;
                command.Parameters.Clear();
                if (cmdParams != null)
                {
                    foreach (SqlParameter parameter in cmdParams)
                    {
                        command.Parameters.Add(parameter);
                    }
                }
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                command.Parameters.Clear();
                return reader;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }
        }

        public static DataTable ExecuteTableBySP(string storedProcedureName, SqlParameter[] cmdParams)
        {
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    command.CommandText = storedProcedureName;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.Clear();

                    if (cmdParams != null)
                    {
                        foreach (SqlParameter parameter in cmdParams)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    adapter.MissingMappingAction = MissingMappingAction.Passthrough;
                    adapter.MissingSchemaAction = MissingSchemaAction.Add;
                    adapter.SelectCommand = command;
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "Result");
                    return dataSet.Tables["Result"];
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
                return null;
            }
        }
        public static DataTable ExecuteTableBySP(string storedProcedureName, SqlParameter[] cmdParams, SqlTransaction trans)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand();
            try
            {
                command.Transaction = trans;
                command.CommandText = storedProcedureName;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = trans.Connection;
                command.Parameters.Clear();
                if (cmdParams != null)
                {
                    foreach (SqlParameter parameter in cmdParams)
                    {
                        command.Parameters.Add(parameter);
                    }
                }
                adapter.MissingMappingAction = MissingMappingAction.Passthrough;
                adapter.MissingSchemaAction = MissingSchemaAction.Add;
                adapter.SelectCommand = command;
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Result");
                return dataSet.Tables["Result"];
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }
        }

        public static bool DeleteDataBySP(string storedProcedureName, SqlParameter[] cmdParams)
        {
            SqlTransaction trans = null;
            SqlCommand command = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                try
                {
                    trans = connection.BeginTransaction();
                    command.CommandText = storedProcedureName;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Transaction = trans;
                    command.Connection = connection;
                    command.Parameters.Clear();
                    if (cmdParams != null)
                    {
                        foreach (SqlParameter parameter in cmdParams)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    command.ExecuteNonQuery();
                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    try
                    {
                        trans.Rollback();
                        Console.WriteLine(ex.StackTrace);
                    }
                    catch (Exception ex1)
                    {
                        Console.WriteLine(ex1.StackTrace);
                    }
                    return false;
                }
            }
        }
        public static bool DeleteDataBySP(string storedProcedureName, SqlParameter[] cmdParams, SqlTransaction trans)
        {
            SqlCommand command = new SqlCommand();
            try
            {
                command.Transaction = trans;
                command.CommandText = storedProcedureName;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = trans.Connection;
                command.Parameters.Clear();
                if (cmdParams != null)
                {
                    foreach (SqlParameter parameter in cmdParams)
                    {
                        command.Parameters.Add(parameter);
                    }
                }
                return command.ExecuteNonQuery() > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static bool UpdateDataBySP(string storedProcedureName, SqlParameter[] cmdParams)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlTransaction trans = null;
                SqlCommand command = new SqlCommand();
                try
                {
                    trans = connection.BeginTransaction();
                    command.CommandText = storedProcedureName;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Transaction = trans;
                    command.Connection = connection;
                    command.Parameters.Clear();
                    if (cmdParams != null)
                    {
                        foreach (SqlParameter parameter in cmdParams)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    command.ExecuteNonQuery();
                    trans.Commit();
                    return true;

                }
                catch (Exception ex)
                {
                    try
                    {
                        trans.Rollback();
                        Console.WriteLine(ex.Message);
                    }
                    catch (Exception ex1)
                    {
                        Console.WriteLine(ex1.Message);
                    }
                    return false;
                }
            }
        }
        public static bool UpdateDataBySP(string storedProcedureName, SqlParameter[] cmdParams, SqlTransaction trans)
        {
            SqlCommand command = new SqlCommand();
            try
            {
                command.Transaction = trans;
                command.CommandText = storedProcedureName;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = trans.Connection;
                command.Parameters.Clear();
                if (cmdParams != null)
                {
                    foreach (SqlParameter parameter in cmdParams)
                    {
                        command.Parameters.Add(parameter);
                    }
                }
                return command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static bool InsertDataBySP(string storedProcedureName, SqlParameter[] cmdParams)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlTransaction trans = null;
                SqlCommand command = new SqlCommand();
                try
                {
                    trans = connection.BeginTransaction();
                    command.CommandText = storedProcedureName;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Transaction = trans;
                    command.Connection = connection;
                    command.Parameters.Clear();
                    if (cmdParams != null)
                    {
                        foreach (SqlParameter parameter in cmdParams)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    command.ExecuteNonQuery();
                    trans.Commit();
                    return true;

                }
                catch (Exception ex)
                {
                    try
                    {
                        trans.Rollback();
                        Console.WriteLine(ex.Message);
                    }
                    catch (Exception ex1)
                    {
                        Console.Write(ex1.Message);
                    }
                    return false;
                }
            }
        }
        public static bool InsertDataBySP(string storedProcedureName, SqlParameter[] cmdParams, SqlTransaction trans)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.Transaction = trans;
                command.CommandText = storedProcedureName;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = trans.Connection;
                command.Parameters.Clear();
                if (cmdParams != null)
                {
                    foreach (SqlParameter parameter in cmdParams)
                    {
                        command.Parameters.Add(parameter);
                    }
                }
                return command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// ����ִ�д�������SQL���
        /// </summary>
        /// <param name="listSql">SQL���</param>
        /// <param name="listParams">����</param>
        /// <returns>�����ɹ�1  ����ʧ��-1</returns>
        public static int ExecuteMoreSQLTran(System.Collections.Generic.List<string> listSql, System.Collections.Generic.List<SqlParameter[]> listParams)
        {
            int result = -1;
            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            {
                sqlConn.Open();
                SqlTransaction sqlTran = sqlConn.BeginTransaction();
                try
                {
                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.Connection = sqlConn;
                        sqlCmd.Transaction = sqlTran;
                        string cmdText = string.Empty;
                        for (int i = 0; i < listSql.Count; i++)
                        {
                            cmdText = listSql[i];
                            SqlParameter[] cmdParams = listParams[i];
                            PrepareCommand(sqlCmd, cmdText, cmdParams);
                            sqlCmd.ExecuteNonQuery();
                            sqlCmd.Parameters.Clear();
                        }
                        sqlTran.Commit();
                        result = 1;
                    }
                }
                catch (Exception ex)
                {
                    sqlTran.Rollback();
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    if (sqlConn.State == ConnectionState.Open)
                        sqlConn.Close();
                }
            }
            return result;
        }
        /// <summary>
        /// ִ�ж���SQL��䣬ʵ�����ݿ�����
        /// </summary>
        /// <param name="sqlList">Sql����ֵ�</param>
        /// <returns>�����ɹ�1  ����ʧ��-1</returns>
        public static int ExecuteMoreSQLTran(System.Collections.Generic.Dictionary<string, SqlParameter[]> sqlList)
        {
            int result = -1;
            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            {
                sqlConn.Open();
                SqlTransaction sqlTran = sqlConn.BeginTransaction();
                using (SqlCommand sqlCmd = new SqlCommand())
                {
                    try
                    {
                        sqlCmd.Connection = sqlConn;
                        sqlCmd.Transaction = sqlTran;

                        string commandText = string.Empty;
                        foreach (var sql in sqlList)
                        {
                            commandText = sql.Key.ToString();
                            SqlParameter[] parameters = (SqlParameter[])sql.Value;
                            PrepareCommand(sqlCmd, commandText, parameters);
                            sqlCmd.ExecuteNonQuery();
                            sqlCmd.Parameters.Clear();
                        }
                        sqlTran.Commit();
                        result = 1;
                    }
                    catch (Exception ex)
                    {
                        sqlTran.Rollback();
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        if (sqlConn.State == ConnectionState.Open)
                            sqlConn.Close();
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// ���ش洢������ֵ�ķ���
        /// </summary>
        /// <param name="sqlStr">�洢����</param>
        /// <param name="cmdParams">���в���</param>
        /// <param name="returnParameter">����ֵ����</param>
        /// <returns></returns>
        public static object GetProcReturnParameters(string sqlStr, SqlParameter[] cmdParams, SqlParameter returnParameter)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 60;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                cmd.Connection = connection;
                cmd.CommandText = sqlStr;
                cmd.CommandTimeout = 60;
                cmd.CommandType = CommandType.StoredProcedure;
                if (cmdParams != null)
                {
                    foreach (SqlParameter parameter in cmdParams)
                    {
                        if (parameter.ParameterName != returnParameter.ParameterName)
                        {
                            cmd.Parameters.Add(parameter);
                        }
                    }
                }
                cmd.Parameters.Add(returnParameter);
                cmd.ExecuteNonQuery();

                object returnobject = returnParameter.Value;
                cmd.Parameters.Clear();
                return returnobject;
            }
        }
        #endregion

        #region ���� Ԥ����򻺴��
        public static int ObjectToInt32(object obj)
        {
            int result = 0;
            if (!(object.Equals(obj, null) || object.Equals(obj, DBNull.Value)))
            {
                int.TryParse(obj.ToString(), out result);
            }
            return result;
        }
        // Hashtable to store cached parameters
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// add parameter array to the cache
        /// </summary>
        /// <param name="cacheKey">Key to the parameter cache</param>
        /// <param name="cmdParms">an array of SqlParamters to be cached</param>
        public static void CacheParameters(string cacheKey, SqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }
        /// <summary>
        /// Retrieve cached parameters
        /// </summary>
        /// <param name="cacheKey">key used to lookup parameters</param>
        /// <returns>Cached SqlParamters array</returns>
        public static SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }

        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandTimeout = 60;
            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parameter in cmdParms)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
        }
        private static void PrepareCommand(SqlCommand sqlCmd, string cmdText, SqlParameter[] cmdParmas)
        {
            sqlCmd.CommandText = cmdText;
            sqlCmd.CommandType = CommandType.Text;
            if (cmdParmas != null && cmdParmas.Length > 0)
            {
                foreach (SqlParameter parm in cmdParmas)
                    sqlCmd.Parameters.Add(parm);
            }
        }
        #endregion

        #region Dynamic-HashTable ExecuteNonQuery
        /// <summary>
        /// ͨ�ø��²��� ��ʱֻ֧�ֵ�����������Ϊ���
        /// </summary>
        /// <param name="ht">key->�����ֶ����ƣ�value->�����ֶ�ֵ</param>
        /// <param name="tableName">�������±���</param>
        /// <param name="whereFiledName">�����ֶ�����</param>
        /// <param name="whereFiledValue">�����ֶ�ֵ</param>
        /// <param name="whereFiledType">�����ֶ����ͣ�1���֡�2�ַ����ַ�����ʱ�䡢GUID��</param>
        /// <returns>ִ��״̬ �ɹ�>0��ʧ�ܷ���0���쳣-1������Ϊ�շ���-2</returns>
        public static int DynamicUpdate(Hashtable ht, string tableName, string whereFiledName, int whereFiledValue, int whereFiledType)
        {
            if (ht == null || string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(whereFiledName))
            {
                return -2;
            }
            StringBuilder sbRet = new StringBuilder("UPDATE [" + tableName + "] SET ");
            foreach (var itemKey in ht.Keys)
            {
                sbRet.Append(itemKey.ToString() + "=@" + itemKey.ToString() + ",");
            }
            sbRet = new StringBuilder(sbRet.ToString().TrimEnd(','));
            switch (whereFiledType)
            {
                case 1:
                    sbRet.AppendFormat(" where {0}={1}", whereFiledName, whereFiledValue);
                    break;
                case 2:
                default:
                    sbRet.AppendFormat(" where {0}='{1}'", whereFiledName, whereFiledValue);
                    break;
            }
            string safeSql = sbRet.ToString();
            SqlParameter[] parameters = new SqlParameter[ht.Keys.Count];
            int index = 0;
            IDictionaryEnumerator ienum = ht.GetEnumerator();
            while (ienum.MoveNext())
            {
                parameters[index] = new SqlParameter("@" + ienum.Key, ienum.Value);
                index++;
            }
            try
            {
                int result = DbHelperSql.ExecuteNonQuery(safeSql, parameters);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }
        /// <summary>
        /// ͨ�ò�������ҷ��� SELECT @@identity;
        /// INSERT ��䷵�ص�ǰ�����Զ���������ֵ SELECT @@identity; 
        /// </summary>
        /// <param name="ht">key->�����ֶ����ƣ�value->�����ֶ�ֵ</param>
        /// <param name="tableName">�����������</param>
        /// <returns>ִ��״̬ �ɹ�>0�ҷ��ص�ǰ�����Զ���������ֵ��ʧ�ܷ���0���쳣-1������Ϊ�շ���-2</returns>
        public static int DynamicInsert(Hashtable ht, string tableName)
        {
            if (ht == null || string.IsNullOrEmpty(tableName))
            {
                return -2;
            }
            #region
            StringBuilder sbRet = new StringBuilder("INSERT INTO [" + tableName + "] (");
            IDictionaryEnumerator ienum = ht.GetEnumerator();
            while (ienum.MoveNext())
            {
                sbRet.Append(ienum.Key.ToString() + ",");
            }
            sbRet = new StringBuilder(sbRet.ToString().TrimEnd(','));
            sbRet.Append(") values (");
            SqlParameter[] parameters = new SqlParameter[ht.Keys.Count];
            int index = 0;
            IDictionaryEnumerator ienum2 = ht.GetEnumerator();
            while (ienum2.MoveNext())
            {
                sbRet.Append("@" + ienum2.Key.ToString() + ",");
                parameters[index] = new SqlParameter("@" + ienum2.Key, ienum2.Value);
                index++;
            }
            sbRet = new StringBuilder(sbRet.ToString().TrimEnd(','));
            sbRet.Append(");SELECT @@identity;");
            string safeSql = sbRet.ToString();
            try
            {
                int result = DbHelperSql.ExecuteScalar(CommandType.Text, safeSql, parameters);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
            #endregion
        }

        #endregion

        #region ���ݿ⡢����ͼ�Ƚṹ
        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="TableName">���ݱ���</param>
        /// <param name="Identifying">�����ֶ���</param>
        /// <returns></returns>
        public static bool BackIdentifying(string TableName, string Identifying)
        {
            string tempstr = "select max(" + Identifying + ") from " + TableName + "";
            object maxid = ExecuteScalar(CommandType.Text, tempstr, null);
            int maxintid = ObjectToInt32(maxid);
            string strSql = "DBCC CHECKIDENT (" + TableName + ",reseed," + maxintid + ")";
            return (ExecuteNonQuery(CommandType.Text, strSql, null) > 0);
        }

        /// <summary>
        /// ��ȡsql server���ݿ�ṹ �������
        /// </summary>
        /// <param name="sType">�ṹ���� Table�� Views��ͼ</param>
        /// <returns></returns>
        public static DataTable GetSchemaTable(string ConnectionString, int iType)
        {
            DataTable dt;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                try
                {
                    dt = conn.GetSchema("TABLES");
                }
                catch
                {
                    dt = null;
                }
                finally
                {
                    conn.Close();
                }
            }

            dt.DefaultView.Sort = "TABLE_NAME asc";
            //TABLE_NAME
            DataTable newtable = new DataTable();
            newtable.Columns.Add("TABLE_NAME");
            newtable.Columns.Add("TABLENAME");

            string[] connect1 = ConnectionString.Split(new string[] { "Initial Catalog=" }, StringSplitOptions.RemoveEmptyEntries);

            string[] connect = connect1[1].Split(new string[] { ";User ID=" }, StringSplitOptions.RemoveEmptyEntries);

            string dataname = connect[0];
            if (iType == 1)
            {
                foreach (DataRow row in dt.Rows)
                {
                    newtable.Rows.Add(dataname + ".dbo." + row["TABLE_NAME"], row["TABLE_NAME"]);
                }
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    newtable.Rows.Add(row["TABLE_NAME"], row["TABLE_NAME"]);
                }
            }
            return newtable;
        }

        public static string GetTablecolumn(string tablename, string columnname)
        {
            string Columtxt = "";
            string strSql = "SELECT value FROM ::fn_listextendedproperty ('MS_Description','user','dbo','table','" + tablename + "','column','" + columnname + "')";
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = strSql;
                cmd.CommandTimeout = 60;
                cmd.CommandType = CommandType.Text;
                Columtxt = (string)cmd.ExecuteScalar();
            }
            return Columtxt;
        }

        /// <summary>
        /// ��ѯ�������ݿ�
        /// </summary>
        /// <returns>���ݿ��б�</returns>
        public static DataTable GetDataList(string connStr, string strSql)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = strSql;
                cmd.CommandTimeout = 60;
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Result");
                adapter.Dispose();
                //cmd.Parameters.Clear();
                if (dataSet.Tables.Count > 0)
                {
                    return dataSet.Tables["Result"];
                }
                return null;
            }
        }

        /// <summary>
        /// ��ѯ���ݿ���ֶε���Ϣ���ֶ������ֶ��������ͣ�
        /// </summary>
        /// <param name="TableName">����</param>
        /// <returns></returns>
        public static DataTable GetTableColumn(string TableName, string ConnStr)
        {
            //sys.extended_properties
            StringBuilder sbSql = new StringBuilder();
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                if (conn.ServerVersion.IndexOf("08.00") > -1)//SQL2000���ݿ�ʹ��
                {
                    sbSql.Append("select c.name as fieldname,t.name fieldtype,m.text as fielddef,p.[value] AS FieldAlias,c.isnullable as IsNotNull,isnull(p.[value],'') AS fieldvalue,");
                    sbSql.Append("COLUMNPROPERTY(c.id,c.name,'PRECISION') as typelength,isnull(COLUMNPROPERTY(c.id,c.name,'Scale'),0) as decimaldigits,isnull(m.text,'') typedefault   from");
                    sbSql.Append(" syscolumns c inner join systypes t on c.xusertype=t.xusertype ");
                    sbSql.Append("left join syscomments m on c.cdefault=m.id left join sysproperties p on c.id=p.id AND p.smallid = c.colid where ");
                    sbSql.AppendFormat("objectproperty(c.id,'IsUserTable')=1 and (object_name(c.id)='{0}') order by c.colorder", TableName);
                }
                else
                {
                    sbSql.Append("select c.name as fieldname,t.name fieldtype,m.text as fielddef,p.[value] AS FieldAlias,c.isnullable as IsNotNull,isnull(p.[value],'') AS fieldvalue,");
                    sbSql.Append("COLUMNPROPERTY(c.id,c.name,'PRECISION') as typelength,isnull(COLUMNPROPERTY(c.id,c.name,'Scale'),0) as decimaldigits ,isnull(m.text,'') typedefault   from");
                    sbSql.Append(" syscolumns c inner join systypes t on c.xusertype=t.xusertype ");
                    sbSql.Append("left join syscomments m on c.cdefault=m.id left join sys.extended_properties p on c.id=p.major_id AND p.minor_id = c.colid where ");
                    sbSql.AppendFormat("objectproperty(c.id,'IsUserTable')=1 and (object_name(c.id)='{0}') order by c.colorder", TableName);
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sbSql.ToString();
                cmd.CommandTimeout = 60;
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Result");
                adapter.Dispose();
                //cmd.Parameters.Clear();
                if (dataSet.Tables.Count > 0)
                {
                    return dataSet.Tables["Result"];
                }
                return null;
            }
        }

        public static DataTable GetTable(string strSql, string ConnStr)
        {
            return ExecuteTable(ConnStr, CommandType.Text, strSql, null);
        }
        #endregion
    }
}
