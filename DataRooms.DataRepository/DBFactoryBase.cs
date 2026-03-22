using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DataRooms.Entity;
using Microsoft.Extensions.Configuration;
using Module = DataRooms.Entity.Module;

namespace DataRooms.DataRepository
{
    public abstract class DBFactoryBase
    {
        private readonly IConfiguration _config;

        public DBFactoryBase(IConfiguration config)
        {
            _config = config;
            
        }

        internal string DbConnectionString => _config.GetConnectionString("SqlDataConnection");
        internal SqlConnection DbConnection => new SqlConnection(DbConnectionString);

        public virtual async Task<int> DbExecuteAsync(string sql,object[] parameters)
        {
            try
            {
                using(var dbConnection = DbConnection)
                {
                    await dbConnection.OpenAsync();
                    using(var cmd = new SqlCommand(sql, dbConnection))
                    {
                        cmd.CommandTimeout = 0;
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            cmd.Parameters.AddWithValue((i+1).ToString(),parameters[i] ?? DBNull.Value);
                        }
                        var result = await cmd.ExecuteScalarAsync();
                        if (result == null)
                            return 0;
                        else
                        return (int)result;
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        

        public virtual async Task<int> DbExecuteAsync(string sql, Dictionary<string,dynamic> parameters)
        {
            try
            {
                using (var dbConnection = DbConnection)
                {
                    await dbConnection.OpenAsync();
                    using (var cmd = new SqlCommand(sql, dbConnection))
                    {
                        cmd.CommandTimeout = 0;
                        foreach (var key in parameters.Keys)
                        {
                            cmd.Parameters.AddWithValue(key, parameters[key] ?? DBNull.Value);
                        }
                        return await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<int> DbExecuteAsync(string sql)
        {
            try
            {
                using (var dbConnection = DbConnection)
                {
                    await dbConnection.OpenAsync();
                    using (var cmd = new SqlCommand(sql, dbConnection))
                    {
                        cmd.CommandTimeout = 0;
                        return await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> GetWaitingWith(int fileid)
        {
            string waitingWith = string.Empty;
            try
            {
                using (var dbConnection = DbConnection)
                {
                    await dbConnection.OpenAsync();
                    using (var da = new SqlDataAdapter(@"select dbo.fun_WaitingWith("+ fileid + ") as waitingwith", dbConnection))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                waitingWith = Convert.ToString(dr["waitingwith"]);
                            }
                        }
                        return waitingWith;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        

        public async Task<HostDetails> GetHostDetails()
        {
            HostDetails hostDetails = new HostDetails();
            //Module module = new Module();
            try
            {
                using (var dbConnection = DbConnection)
                {
                    await dbConnection.OpenAsync();
                    using (var da = new SqlDataAdapter("select Column1,Column2,Column3 from WebApiInfo", dbConnection))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                hostDetails.HostName = Convert.ToString(dr["Column1"]);
                                hostDetails.IpAddress = Convert.ToString(dr["Column2"]);
                                hostDetails.EmailId = Convert.ToString(dr["Column3"]);
                                //using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                                //{
                                                                      
                                //}
                            }
                        }
                    }

                    //using (var da = new SqlDataAdapter("select Column1,Column2,Column3,Column4,Column5 from LicenseInfo", dbConnection))
                    //{
                    //    DataTable dt = new DataTable();
                    //    da.Fill(dt);
                    //    if (dt != null && dt.Rows.Count > 0)
                    //    {
                    //        foreach (DataRow dr in dt.Rows)
                    //        {
                    //            EncryptionHelper encryptionHelper = new EncryptionHelper();
                    //            module.ModuleName = dr["Column1"] == DBNull.Value ? "" : encryptionHelper.Decrypt(Convert.ToString(dr["Column1"]));
                    //            module.ModuleCount = dr["Column2"] == DBNull.Value ? 0 : Convert.ToInt32(encryptionHelper.Decrypt(Convert.ToString(dr["Column2"])));
                    //            module.LicenseStatus = dr["Column3"] == DBNull.Value ? "" : encryptionHelper.Decrypt(Convert.ToString(dr["Column3"]));
                    //            module.FromDate = dr["Column4"] == DBNull.Value ? "" : encryptionHelper.Decrypt(Convert.ToString(dr["Column4"]));
                    //            module.ToDate = dr["Column5"] == DBNull.Value ? "" : encryptionHelper.Decrypt(Convert.ToString(dr["Column5"]));
                    //        }
                    //    }
                    //}

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return hostDetails;
        }

        //public async Module GetLicensingModule(string hostName,string ipAddess)
        //{
        //    Module module = new Module();
        //    try
        //    {
        //        using (var dbConnection = DbConnection)
        //        {
        //            await dbConnection.OpenAsync();
        //            using (var da = new SqlDataAdapter("select Column1,Column2,Column3,Column4,Column5 from LicenseInfo", dbConnection))
        //            {
        //                DataTable dt = new DataTable();
        //                da.Fill(dt);
        //                if (dt != null && dt.Rows.Count > 0)
        //                {
        //                    foreach (DataRow dr in dt.Rows)
        //                    {
        //                        hostDetails.HostName = Convert.ToString(dr["Column1"]);
        //                        hostDetails.IpAddress = Convert.ToString(dr["Column2"]);
        //                        hostDetails.EmailId = Convert.ToString(dr["Column3"]);
        //                    }
        //                }
        //                return hostDetails;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        public async Task<IEnumerable<T>> GetData<T>(int page,int pageSize,string tableName,string whereCondition,string sort)
        {
            try
            {
                List<T> objects = new List<T>();
                using (var dbConnection = DbConnection)
                {
                    await dbConnection.OpenAsync();
                    //string sql = @"EXEC spGetData " + page + "," + pageSize + "," + "'" + tableName + "'" + "," + "'" + whereCondition + "'" + "," + "'" + sort + "'";
                    string sql = @"spGetData";
                    using (SqlCommand cmd = new SqlCommand(sql, dbConnection))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Page", page);
                        cmd.Parameters.AddWithValue("@PageSize", pageSize);
                        cmd.Parameters.AddWithValue("@TableName", tableName);
                        cmd.Parameters.AddWithValue("@WhereCondition", whereCondition);
                        cmd.Parameters.AddWithValue("@Sort", sort);
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = await cmd.ExecuteReaderAsync();
                        if (reader.HasRows)
                        {
                            //objects = new List<T>();
                            while (await reader.ReadAsync())
                            {
                                T obj = Activator.CreateInstance<T>();
                                PropertyInfo[] p = typeof(T).GetProperties();
                                foreach (PropertyInfo pi in p)
                                {
                                    try
                                    {
                                        if (reader[pi.Name] != System.DBNull.Value)
                                            pi.SetValue(obj, reader[pi.Name], null);
                                    }
                                    catch (System.IndexOutOfRangeException) { }
                                }
                                objects.Add(obj);
                            }
                        }
                        return objects;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task<IEnumerable<T>> DbGetDatawithQueryAsync<T>(string sql)
        {
            try
            {
                List<T> objects = new List<T>();
                using (var dbConnection = DbConnection)
                {
                    await dbConnection.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(sql, dbConnection))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader reader = await cmd.ExecuteReaderAsync();
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                T obj = Activator.CreateInstance<T>();
                                PropertyInfo[] p = typeof(T).GetProperties();
                                foreach (PropertyInfo pi in p)
                                {
                                    try
                                    {
                                        if (reader[pi.Name] != System.DBNull.Value)
                                            pi.SetValue(obj, reader[pi.Name], null);
                                    }
                                    catch (System.IndexOutOfRangeException) { }
                                }
                                objects.Add(obj);
                            }
                        }
                        return objects;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IEnumerable<T> DbGetDatawithQuery<T>(string sql)
        {
            try
            {
                List<T> objects = new List<T>();
                using (var dbConnection = DbConnection)
                {
                    dbConnection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, dbConnection))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                T obj = Activator.CreateInstance<T>();
                                PropertyInfo[] p = typeof(T).GetProperties();
                                foreach (PropertyInfo pi in p)
                                {
                                    try
                                    {
                                        if (reader[pi.Name] != System.DBNull.Value)
                                            pi.SetValue(obj, reader[pi.Name], null);
                                    }
                                    catch (System.IndexOutOfRangeException) { }
                                }
                                objects.Add(obj);
                            }
                        }
                        return objects;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckandCreateDatabase(DBConfigureData dBConfigureData, string scriptPath,string secretKey)
        {
            bool isDatabaseCreationSuccessful = false;
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(@"Server=" + dBConfigureData.DBHostName + ";User Id=" + dBConfigureData.UserId + ";Password=" + dBConfigureData.Password + ";");
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                // Recreate Setup
                string commandText = System.IO.File.ReadAllText(scriptPath);
                IEnumerable<string> commandStrings = Regex.Split(commandText, @"^\s*GO\s*$",
                      RegexOptions.Multiline | RegexOptions.IgnoreCase);
                foreach (string commandString in commandStrings)
                {
                    if (commandString.Trim() != "")
                    {
                        using (var command = new SqlCommand(commandString, connection))
                        {
                            try
                            {
                                command.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                    }
                }

                SqlCommand cmdRole = new SqlCommand("Use DataRoom Insert into RoleMaster(RoleName,IsActive)Values('SuperAdmin','1');Use DataRoom Insert into RoleMaster(RoleName,IsActive)Values('Admin','2');Use DataRoom Insert into RoleMaster(RoleName,IsActive)Values('Initiator','3');Use DataRoom Insert into RoleMaster(RoleName,IsActive)Values('Reviewer','4');Use DataRoom Insert into RoleMaster(RoleName,IsActive)Values('Approver','5');Use DataRoom Insert into RoleMaster(RoleName,IsActive)Values('User','6');", connection);
                cmdRole.ExecuteNonQuery();

                SqlCommand cmdInsertApi = new SqlCommand(@"Use DataRoom Insert into WebApiInfo(Column1,Column2,Column3)Values('" + dBConfigureData.EncryptedDomainName + "','" + dBConfigureData.EncryptedPublicIp + "','" + dBConfigureData.EncryptedEmailId + "')", connection);
                cmdInsertApi.ExecuteNonQuery();
                // Insert Default User with Admin Role
                SqlCommand cmdInsertDefaultUser = new SqlCommand(@"Use DataRoom Insert into [dbo].[User](FullName,Username,Password,EmailId,IsADUser,IsActive,CreatedBy,CreatorName,CreatedOn)Values('Admin','admin','Telangana#468','admin@gmail.com','0','1',1,'Admin',sysdatetime());Use DataRoom Insert into UserRoleMapping(UserId,UserName,RoleId,RoleName,IsActive,CreatedBy,CreatorName,CreatedOn)Values(1,'System Admin',1,'SuperAdmin','1',1,'Admin',sysdatetime());", connection);
                cmdInsertDefaultUser.ExecuteNonQuery();


                isDatabaseCreationSuccessful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                connection.Dispose();
            }
            return isDatabaseCreationSuccessful;
        }
    }
}
