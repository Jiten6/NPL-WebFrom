using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using Microsoft.Win32;

namespace NewPortal2023.ESS
{
    /* ------------------------------------------------------      
     Purpose                 : This is used to write database connectivity,store procedure execution related methods,
                               parameter collection created from BAL. These methods are accessed from middle tier. 
     Author                  : Shashikant
     Date Written            : 15-Oct-2008  
     Comments                : Include third tier(Database Access Layer) In4DAL.Dll file into Bin/Debug folder for accessing database connectivity logic.
     Change History          : 
     ------------------------------------------------------ */

    /* Below is the code of user defined datatypes used at the time of setting parameter type*/

    public enum DBUtilDBType
    {
        Boolean = SqlDbType.Bit,
        Byte = SqlDbType.TinyInt,
        Date = SqlDbType.SmallDateTime,
        DateTime = SqlDbType.DateTime,
        Varchar = SqlDbType.VarChar,
        Char = SqlDbType.Char,
        Decimal = SqlDbType.Decimal,
        Numeric = SqlDbType.Float,
        Integer = SqlDbType.Int,
        Guid = SqlDbType.UniqueIdentifier,
        Text = SqlDbType.Text,
        Image = SqlDbType.Image,
        Blob = SqlDbType.VarBinary
    }

    /* Below is the code of user defined datatypes used at the time of setting parameter direction*/

    public enum DBUtilDirection
    {
        In = ParameterDirection.Input,
        Out = ParameterDirection.Output,
    }

    /* Below is the main code of database interaction*/

    public class DBUtility
    {
        public Hashtable mhtParamCollection;
        public Hashtable mhtParamOutPut;
        //private Database mdbTransDB; 
        private IDbTransaction mObjTransaction;
        private IDbConnection mdbCnnConnection;
        public DBUtility()
        {
            mhtParamCollection = new Hashtable();
            mhtParamOutPut = new Hashtable();
        }


        /*Below is the code used in such applications where transaction is started in code rather than store procedure.
         *It is basicaly code level transaction handling.
         *In our case we will not use this method. It is written for above purpose*/

        public void StartTransaction()
        {
            try
            {
                if (mdbCnnConnection == null)
                {
                    mdbCnnConnection = getConnection();
                    mObjTransaction = mdbCnnConnection.BeginTransaction();
                }
            }
            catch (Exception Ex)
            {
                //ErrorHandler objERR = new ErrorHandler();
                //objERR.WriteError(DateTime.Now +  ", DAL.DBUtility-StartTransaction(): " +  Ex.Message.ToString() , Environment.UserName);
                throw Ex;
            }

        }

        /*Below is the code used in such applications where transaction is rollbacked in code rather than store procedure.
         *It is basicaly code level transaction handling.
         *In our case we will not use this method. It is written for above purpose*/

        public void RollBackTransaction()
        {
            try
            {
                if (mObjTransaction != null)
                {
                    mObjTransaction.Rollback();
                }
                if (mdbCnnConnection != null)
                {
                    if (mdbCnnConnection.State == ConnectionState.Open)
                    {
                        mdbCnnConnection.Close();
                    }
                }
            }
            catch (Exception Ex)
            {
                //ErrorHandler objERR = new ErrorHandler();
                //objERR.WriteError(DateTime.Now +  ", DAL.DBUtility-RollBackTransaction(): " +  Ex.Message.ToString() , Environment.UserName);
                throw Ex;
            }
            finally
            {
                mObjTransaction.Dispose();
                if (mdbCnnConnection.State == ConnectionState.Open)
                {
                    mdbCnnConnection.Close();
                }
            }

        }

        /*Below is the code used in such applications where transaction is commited in code rather than store procedure.
         *It is basicaly code level transaction handling.
         *In our case we will not use this method. It is written for above purpose*/

        public void CommitTransaction()
        {
            try
            {
                if (mdbCnnConnection != null)
                {
                    mObjTransaction.Commit();
                    mdbCnnConnection.Close();
                }
            }
            catch (Exception Ex)
            {
                //ErrorHandler objERR = new ErrorHandler();
                //objERR.WriteError(DateTime.Now +  ", DAL.DBUtility-CommitTransaction(): " +  Ex.Message.ToString() , Environment.UserName);
                throw Ex;
            }
            finally
            {
                mObjTransaction.Dispose();
                if (mdbCnnConnection.State == ConnectionState.Open)
                {
                    mdbCnnConnection.Close();
                }
            }
        }

        /* Below is the code used for storing parameters in hash table for temporary purpose unless store procedure executes.
         * This method is initiated from BAL at the time of store procedure execution. 
         * This method also stores the out parameter values in hash table for further user in BAL.*/

        public void AddParameters(string psParamName, DBUtilDBType puType, DBUtilDirection puDirection, int piSize, string psValue)
        {
            try
            {
                UtilParams objParams = new UtilParams();
                objParams.ParamName = psParamName;
                objParams.ParamType = (SqlDbType)puType;
                objParams.Direction = (ParameterDirection)puDirection;
                if (puDirection == DBUtilDirection.Out)
                {
                    mhtParamOutPut.Add(psParamName, "");
                }
                objParams.ParamSize = piSize;
                objParams.ParamValue = psValue;
                // mhtParamCollection.Clear();
                mhtParamCollection.Add(psParamName, objParams);
                //objParams.ParamName = "";
            }
            catch
            {
                throw;
            }
        }

        /* Below is the code used for storing parameters in hash table for temporary purpose unless store procedure executes.
         * This method is initiated from BAL at the time of store procedure execution and specialy written for storing Images. 
         * This method also stores the out parameter values in hash table for further user in BAL.*/

        public void AddParametersImage(string psParamName, DBUtilDBType puType, DBUtilDirection puDirection, int piSize, byte[] psValue)
        {
            try
            {
                UtilParams objParams = new UtilParams();
                objParams.ParamName = psParamName;
                objParams.ParamType = (SqlDbType)puType;
                objParams.Direction = (ParameterDirection)puDirection;
                if (puDirection == DBUtilDirection.Out)
                {
                    mhtParamOutPut.Add(psParamName, "");
                }
                objParams.ParamSize = piSize;
                objParams.ParamByte = psValue;
                mhtParamCollection.Add(psParamName, objParams);
            }
            catch
            {
                throw;
            }
        }

        /* Below is the code used for returing value from store procedure where query is written for getting one perticular datavalue*/

        public string Execute_QueryString(string psName)
        {
            string return_string;
            SqlConnection objConnection = null;
            SqlCommand cmd = null;
            try
            {
                //objConnection = getConnection();
                //cmd = new OracleCommand(psName, objConnection);
                objConnection = getConnection();
                cmd = new SqlCommand(psName, objConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                return return_string = Convert.ToString(cmd.ExecuteScalar());
            }
            catch (Exception Ex)
            {
                //ErrorHandler objERR = new ErrorHandler();
                /////objERR.WriteError(DateTime.Now +  ", DAL.DBUtility-Execute_StoreProc(): " +  Ex.Message.ToString() + " " + Environment.UserName, Environment.UserName);
                throw;
            }
            finally
            {

                if (objConnection.State == ConnectionState.Open)
                    objConnection.Close();

            }
        }


        /* Below is the code used for executing non return value based store procedure.*/

        public bool Execute_StoreProc_Transactional(string psStoreProcName)
        {
            string sOutValue;
            UtilParams objParams;
            SqlParameter objSqlParams;
            SqlCommand objCommand = null;
            try
            {
                objCommand = new SqlCommand(psStoreProcName, (SqlConnection)mdbCnnConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandTimeout = 0;
                objCommand.Transaction = (SqlTransaction)mObjTransaction;
                IDictionaryEnumerator en = mhtParamCollection.GetEnumerator();
                while (en.MoveNext())
                {
                    objParams = (UtilParams)en.Value;
                    objSqlParams = new SqlParameter();
                    objSqlParams.ParameterName = objParams.ParamName;
                    objSqlParams.SqlDbType = (SqlDbType)objParams.ParamType;
                    objSqlParams.Direction = objParams.Direction;
                    objSqlParams.Size = objParams.ParamSize;
                    objSqlParams.Value = objParams.ParamValue;
                    objCommand.Parameters.Add(objSqlParams);
                }
                objCommand.ExecuteNonQuery();
                en = mhtParamCollection.GetEnumerator();
                while (en.MoveNext())
                {
                    objParams = (UtilParams)en.Value;
                    if (objParams.Direction == (ParameterDirection)DBUtilDirection.Out)
                    {
                        sOutValue = objCommand.Parameters[en.Key.ToString()].Value.ToString();
                        mhtParamOutPut[en.Key.ToString()] = sOutValue;
                    }
                }

                return true;
            }
            catch (Exception Ex)
            {
                RollBackTransaction();
                //ErrorHandler objERR = new ErrorHandler();
                //objERR.WriteError(DateTime.Now +  ", DAL.DBUtility-Execute_StoreProc(): " +  Ex.Message.ToString() + " " + Environment.UserName, Environment.UserName);
                throw Ex;
            }
            finally
            {
                objCommand.Dispose();
            }
        }

        /* Below is the code used for returing value from store procedure where query is written for getting one perticular datavalue*/

        public String Execute_StoreProc_ScalerValue(string psStoreProcName)
        {
            //DataSet dsResult = new DataSet();
            UtilParams objParams;
            SqlConnection objConnection = null;
            SqlParameter objSqlParams = null;
            SqlCommand objCommand = null;
            String returnvalue = "";

            try
            {
                objConnection = getConnection();
                objCommand = new SqlCommand(psStoreProcName, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandTimeout = 0;

                IDictionaryEnumerator en = mhtParamCollection.GetEnumerator();

                while (en.MoveNext())
                {
                    objParams = (UtilParams)en.Value;
                    objSqlParams = new SqlParameter();
                    objSqlParams.ParameterName = objParams.ParamName;
                    objSqlParams.SqlDbType = (SqlDbType)objParams.ParamType;
                    objSqlParams.Direction = objParams.Direction;
                    if (objParams.Direction == ParameterDirection.Output)
                    {
                        objSqlParams.Size = objParams.ParamSize;

                    }

                    objSqlParams.Value = objParams.ParamValue;
                    objCommand.Parameters.Add(objSqlParams);
                }
                returnvalue = objCommand.ExecuteScalar().ToString();
                // objSqlDataAdapter.Fill(dsResult);
                return returnvalue;
            }
            catch (Exception Ex)
            {
                //ErrorHandler objERR = new ErrorHandler();
                //objERR.WriteError(DateTime.Now +  ", DAL.DBUtility-Execute_StoreProc_DataSet(): " +  Ex.Message.ToString() + " " + Environment.UserName, Environment.UserName);
                throw Ex;
            }
            finally
            {
                objCommand.Dispose();
                //objSqlDataAdapter.Dispose();
                if (objConnection.State == ConnectionState.Open)
                    objConnection.Close();

            }

        }

        /* Below is the code used for getting connection with database at the of executing store procedure*/

        private SqlConnection getConnection()
        {
            try
            {
                string sConnectString = Read_Ini_File();
                SqlConnection conObjConnect = new SqlConnection(sConnectString);
                conObjConnect.Open();
                return conObjConnect;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string Read_Ini_File()
        {

            //local variables
            string FilePath;
            string rDSN;

            //Convert Physical path of aspx to ini file path
            FilePath = HttpContext.Current.Request.PhysicalApplicationPath + @"\BIN\DATASTRING.INI";

            rDSN = "";

            // Open the ini file
            FileInfo config_file = new FileInfo(FilePath);
            if (config_file.Exists)
            {
                // read configuration values
                using (StreamReader stream_reader = new StreamReader(config_file.FullName))
                {
                    //local variables
                    string section = "[]";
                    string line;
                    string ServerName;

                    // get server name to find the client 
                    ServerName = HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToUpper();
                    ServerName = "[" + ServerName + "]";

                    // the file is reached.
                    while ((line = stream_reader.ReadLine()) != null)
                    {
                        // set the current section name
                        if (line.StartsWith("[") && line.EndsWith("]") && line != section)
                        {
                            section = line.ToUpper();
                        }
                        // read connection string in second iteration
                        if (section == "[ESS]" && section != line.ToUpper())
                        {

                            rDSN = line.ToString().Replace("DSN=Provider=sqloledb;", "").Trim();
                            break;
                        }

                    }
                }
            }
            return rDSN;
        }
        /* Below is the code used for getting connection status at the of executing store procedure*/
        public bool getConnectionStatus()
        {
            try
            {
                // TO BE COMMENTED BEFORE UPLOAD
                //string sConnectString = "workstation id=S-CC22BLIJ4EWW9;packet size=4096;integrated security=SSPI;data source=S-CC22BLIJ4EWW9;persist security info=False;initial catalog=JewelryPot;";

                //string sConnectString=ConfigurationSettings.AppSettings["ConnectionString"].ToString();

                string sConnectString;
                sConnectString = "server=" + RegValue(Microsoft.Win32.RegistryHive.LocalMachine, "SOFTWARE\\EIP", "DBSERVER");
                sConnectString += ";uid=" + RegValue(Microsoft.Win32.RegistryHive.LocalMachine, "SOFTWARE\\EIP", "LOGIN");
                sConnectString += ";pwd=" + RegValue(Microsoft.Win32.RegistryHive.LocalMachine, "SOFTWARE\\EIP", "PASSWORD");
                sConnectString += ";database=" + RegValue(Microsoft.Win32.RegistryHive.LocalMachine, "SOFTWARE\\EIP", "DB");

                SqlConnection conObjConnect = new SqlConnection(sConnectString);
                conObjConnect.Open();
                //return conObjConnect;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public static string RegValue(RegistryHive Hive, string Key, string ValueName) // ERROR: Unsupported modifier : Ref, Optional string ErrInfo) 
        {
            RegistryKey objParent = null;
            RegistryKey objSubkey = null;
            string sAns;
            switch (Hive)
            {
                case RegistryHive.ClassesRoot:
                    objParent = Registry.ClassesRoot;
                    break;
                case RegistryHive.CurrentConfig:
                    objParent = Registry.CurrentConfig;
                    break;
                case RegistryHive.CurrentUser:
                    objParent = Registry.CurrentUser;
                    break;
                case RegistryHive.DynData:
                    objParent = Registry.DynData;
                    break;
                case RegistryHive.LocalMachine:
                    objParent = Registry.LocalMachine;
                    break;
                case RegistryHive.PerformanceData:
                    objParent = Registry.PerformanceData;
                    break;
                case RegistryHive.Users:
                    objParent = Registry.Users;
                    break;
            }
            objSubkey = objParent.OpenSubKey(Key);
            if (objSubkey != null)
            {
                sAns = Convert.ToString(objSubkey.GetValue(ValueName));
            }
            else
            {
                sAns = "";
            }
            return sAns;
        }
        /* Below is the code used for returing single/multiple records from store procedure and storing it to dataset in BAL.*/

        public DataSet Execute_StoreProc_DataSet(string psStoreProcName)
        {
            DataSet dsResult = new DataSet();
            UtilParams objParams;
            SqlConnection objConnection = null;
            SqlParameter objSqlParams = null;
            SqlCommand objCommand = null;
            SqlDataAdapter objSqlDataAdapter = null;

            try
            {
                objConnection = getConnection();
                objCommand = new SqlCommand(psStoreProcName, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandTimeout = 0;

                IDictionaryEnumerator en = mhtParamCollection.GetEnumerator();

                while (en.MoveNext())
                {
                    objParams = (UtilParams)en.Value;
                    objSqlParams = new SqlParameter();
                    objSqlParams.ParameterName = objParams.ParamName;
                    objSqlParams.SqlDbType = (SqlDbType)objParams.ParamType;
                    objSqlParams.Direction = objParams.Direction;
                    if (objParams.Direction == ParameterDirection.Output)
                    {
                        objSqlParams.Size = objParams.ParamSize;

                    }

                    objSqlParams.Value = objParams.ParamValue;
                    objCommand.Parameters.Add(objSqlParams);
                }
                objSqlDataAdapter = new SqlDataAdapter(objCommand);
                objSqlDataAdapter.Fill(dsResult);
                return dsResult;
            }
            catch (Exception Ex)
            {
                //ErrorHandler objERR = new ErrorHandler();
                //objERR.WriteError(DateTime.Now +  ", DAL.DBUtility-Execute_StoreProc_DataSet(): " +  Ex.Message.ToString() + " " + Environment.UserName, Environment.UserName);
                throw Ex;
            }
            finally
            {
                objCommand.Dispose();
                objSqlDataAdapter.Dispose();
                if (objConnection.State == ConnectionState.Open)
                    objConnection.Close();

            }

        }

        /* Below is the code used for returing single/multiple image records from store procedure and storing it to dataset in BAL.*/

        public DataSet Execute_StoreProc_DataSetImage(string psStoreProcName)
        {
            DataSet dsResult = new DataSet();
            UtilParams objParams;
            SqlConnection objConnection = null;
            SqlParameter objSqlParams = null;
            SqlCommand objCommand = null;
            SqlDataAdapter objSqlDataAdapter = null;

            try
            {
                objConnection = getConnection();
                objCommand = new SqlCommand(psStoreProcName, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandTimeout = 0;
                IDictionaryEnumerator en = mhtParamCollection.GetEnumerator();

                while (en.MoveNext())
                {
                    objParams = (UtilParams)en.Value;
                    objSqlParams = new SqlParameter();
                    objSqlParams.ParameterName = objParams.ParamName;
                    objSqlParams.SqlDbType = (SqlDbType)objParams.ParamType;
                    objSqlParams.Direction = objParams.Direction;
                    if (objParams.Direction == ParameterDirection.Output)
                    {
                        objSqlParams.Size = objParams.ParamSize;

                    }

                    objSqlParams.Value = objParams.ParamByte;
                    objCommand.Parameters.Add(objSqlParams);
                }
                objSqlDataAdapter = new SqlDataAdapter(objCommand);
                objSqlDataAdapter.Fill(dsResult);
                return dsResult;
            }
            catch (Exception Ex)
            {
                //ErrorHandler objERR = new ErrorHandler();
                //objERR.WriteError(DateTime.Now +  ", DAL.DBUtility-Execute_StoreProc_DataSet(): " +  Ex.Message.ToString() + " " + Environment.UserName, Environment.UserName);
                throw Ex;
            }
            finally
            {
                objCommand.Dispose();
                objSqlDataAdapter.Dispose();
                if (objConnection.State == ConnectionState.Open)
                    objConnection.Close();

            }

        }

        /* Below is the code used for executing non return value based store procedure and out out parameter values to hash table.*/

        public bool Execute_StoreProc(string psStoreProcName)
        {
            string sOutValue;
            UtilParams objParams;
            SqlConnection objConnection = null;
            SqlParameter objSqlParams = null;
            SqlCommand objCommand = null;

            try
            {
                objConnection = getConnection();
                objCommand = new SqlCommand(psStoreProcName, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandTimeout = 0;
                IDictionaryEnumerator en = mhtParamCollection.GetEnumerator();
                while (en.MoveNext())
                {
                    objParams = (UtilParams)en.Value;
                    objSqlParams = new SqlParameter();
                    objSqlParams.ParameterName = objParams.ParamName;
                    objSqlParams.SqlDbType = (SqlDbType)objParams.ParamType;
                    objSqlParams.Direction = objParams.Direction;
                    objSqlParams.Size = objParams.ParamSize;
                    objSqlParams.Value = objParams.ParamValue;
                    objCommand.Parameters.Add(objSqlParams);
                }
                objCommand.ExecuteNonQuery();

                en = mhtParamCollection.GetEnumerator();
                while (en.MoveNext())
                {
                    objParams = (UtilParams)en.Value;
                    if (objParams.Direction == (ParameterDirection)DBUtilDirection.Out)
                    {
                        sOutValue = objCommand.Parameters[en.Key.ToString()].Value.ToString();
                        mhtParamOutPut[en.Key.ToString()] = sOutValue;
                    }
                }
                if (objConnection != null && objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
                return true;
            }
            catch (Exception Ex)
            {
                //	ErrorHandler objERR = new ErrorHandler();
                ///objERR.WriteError(DateTime.Now +  ", DAL.DBUtility-Execute_StoreProc(): " +  Ex.Message.ToString() + " " + Environment.UserName, Environment.UserName);
                throw Ex;
            }
            finally
            {
                objCommand.Dispose();
                if (objConnection.State == ConnectionState.Open)
                    objConnection.Close();

            }
        }

        /* Below is the code used for executing non return value based store procedure and out out parameter values to hash table.*/

        public bool Execute_StoreProcImage(string psStoreProcName)
        {
            string sOutValue;
            UtilParams objParams;
            SqlConnection objConnection = null;
            SqlParameter objSqlParams = null;
            SqlCommand objCommand = null;

            try
            {
                objConnection = getConnection();
                objCommand = new SqlCommand(psStoreProcName, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandTimeout = 0;
                IDictionaryEnumerator en = mhtParamCollection.GetEnumerator();
                while (en.MoveNext())
                {
                    objParams = (UtilParams)en.Value;
                    objSqlParams = new SqlParameter();
                    objSqlParams.ParameterName = objParams.ParamName;
                    objSqlParams.SqlDbType = (SqlDbType)objParams.ParamType;
                    objSqlParams.Direction = objParams.Direction;
                    objSqlParams.Size = objParams.ParamSize;
                    objSqlParams.Value = objParams.ParamByte;
                    objCommand.Parameters.Add(objSqlParams);
                }
                objCommand.ExecuteNonQuery();

                en = mhtParamCollection.GetEnumerator();
                while (en.MoveNext())
                {
                    objParams = (UtilParams)en.Value;
                    if (objParams.Direction == (ParameterDirection)DBUtilDirection.Out)
                    {
                        sOutValue = objCommand.Parameters[en.Key.ToString()].Value.ToString();
                        mhtParamOutPut[en.Key.ToString()] = sOutValue;
                    }
                }
                if (objConnection != null && objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
                return true;
            }
            catch (Exception Ex)
            {
                //	ErrorHandler objERR = new ErrorHandler();
                ///objERR.WriteError(DateTime.Now +  ", DAL.DBUtility-Execute_StoreProc(): " +  Ex.Message.ToString() + " " + Environment.UserName, Environment.UserName);
                throw Ex;
            }
            finally
            {
                objCommand.Dispose();
                if (objConnection.State == ConnectionState.Open)
                    objConnection.Close();

            }
        }

        /* Below is the code used getting output parameter values from hash table after execution of store procedure into calling BAL method.*/

        public string getOutputParameterValue(string psOutputParamName)
        {
            try
            {
                return mhtParamOutPut[psOutputParamName].ToString();
            }
            catch (Exception Ex)
            {
                //ErrorHandler objERR = new ErrorHandler();
                //objERR.WriteError(DateTime.Now +  ", DAL.DBUtility-getOutputParameterValue(): " +  Ex.Message.ToString() + " " + Environment.UserName, Environment.UserName);
                throw Ex;
            }
        }

        /* Below is the code used clearing parameter values from hash table after execution of store procedure in calling BAL method.*/

        public void ClearTransactionalParams()
        {
            mhtParamCollection.Clear();
            mhtParamOutPut.Clear();
        }

    }

    /* Below is the code of creating parameter properties required at the time of adding parameters for store procedure.*/

    public class UtilParams
    {
        string msParamName;
        SqlDbType msParamType;
        ParameterDirection msDirection;
        int miParamSize;
        byte[] byParamByte;
        string miParamValue;

        public byte[] ParamByte
        {
            get { return byParamByte; }
            set { byParamByte = value; }
        }
        public string ParamName
        {
            get { return msParamName; }
            set { msParamName = value; }
        }
        public SqlDbType ParamType
        {
            get { return msParamType; }
            set { msParamType = value; }
        }
        public ParameterDirection Direction
        {
            get { return msDirection; }
            set { msDirection = value; }
        }
        public int ParamSize
        {
            get { return miParamSize; }
            set { miParamSize = value; }
        }

        public string ParamValue
        {
            get { return miParamValue; }
            set { miParamValue = value; }
        }
    }
}
