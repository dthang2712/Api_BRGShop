using BRG.libary.BusinessService.Common;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BRG.libary.BusinessService
{
    public class CustomerService : BaseService<CustomerService>
    {

        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public class CustomerInfo
        {
   
            public string Password { get; set; }
            public int? AutoID { get; set; }
            public string FullName { get; set; }
            private DateTime DateOfBirth_Format { get; set; }
            public string DateOfBirth { get; set; }
            public string Sex { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }


            public void CopyValue(CustomerInfo info)
            {
                this.AutoID = info.AutoID;
                this.FullName = info.FullName;
                this.Email = info.Email;
                this.PhoneNumber = info.PhoneNumber;
                this.DateOfBirth_Format = info.DateOfBirth_Format;
                this.Sex = info.Sex;
                this.Password = info.Password;

            }
        }
        public List<CustomerInfo> GetListCustomer(SqlConnection connection, string strSearch = null)
        {
            var result = new List<CustomerInfo>();
            string strSQL = @"
            SELECT[[AutoID]
                   ,[FullName]
                   ,[Email]
                   ,[PhoneNumber]
                   ,[DateOfBirth]
                   ,[Sex]
                   ,[Password]
                   
            FROM [Customer] WHERE 1=1 ";

            using (var command = new SqlCommand(strSQL, connection))
            {
                if (!string.IsNullOrEmpty(strSearch))
                {
                    command.CommandText += "and FullName like @strSearch or PhoneNumber like @strSearch";
                    AddSqlParameter(command, "@strSearch", "@" + strSearch + "%", System.Data.SqlDbType.NVarChar);
                }
                WriteLogExecutingCommand(command);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new CustomerInfo();
                        item.AutoID = GetDbReaderValue<int>(reader["AutoID"]);
                        item.FullName = GetDbReaderValue<string>(reader["FullName"]);
                        item.Email = GetDbReaderValue<string>(reader["Email"]);
                        item.PhoneNumber = GetDbReaderValue<string>(reader["PhoneNumber"]);
                        item.DateOfBirth = GetDbReaderValue<DateTime>(reader["DateOfBirth"]).ToString("d/M/yyyy");
                        item.Sex = GetDbReaderValue<string>(reader["Sex"]);
                        item.Password = GetDbReaderValue<string>(reader["Password"]);
                        result.Add(item);
                    }
                }
            }

            return result;
        }
        public CustomerInfo GetCustomer(SqlConnection connection, string userName)
        {
            var result = new CustomerInfo();
            string strSQL = @"
            SELECT [AutoID]
                   ,[FullName]
                   ,[Email]
                   ,[PhoneNumber]
                   ,[DateOfBirth]
                   ,[Sex]
                   ,[Password]
                   
            FROM [Customer] WHERE PhoneNumber = @PhoneNumber ";

            using (var command = new SqlCommand(strSQL, connection))
            {
                AddSqlParameter(command, "@PhoneNumber", userName, System.Data.SqlDbType.VarChar);


                WriteLogExecutingCommand(command);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new CustomerInfo();
                        result.AutoID = GetDbReaderValue<int?>(reader["AutoID"]);
                        result.FullName = GetDbReaderValue<string>(reader["FullName"]);
                        result.Email = GetDbReaderValue<string>(reader["Email"]);
                        result.PhoneNumber = GetDbReaderValue<string>(reader["PhoneNumber"]);
                        result.DateOfBirth = GetDbReaderValue<DateTime>(reader["DateOfBirth"]).ToString("d/M/yyyy");
                        result.Sex = GetDbReaderValue<string>(reader["Sex"]);
                        result.Password = GetDbReaderValue<string>(reader["Password"]);

                    }
                }
            }

            return result;
        }
        public bool InsertCustomer(SqlConnection connection, CustomerInfo infoInsert)
        {
            string strSQl = @"
            INSERT INTO [Customer]
                ([FullName]
                ,[Email]
                ,[PhoneNumber]
                ,[DateOfBirth]
                ,[Sex]
                ,[Password])
            VALUES
                (@FullName
                ,@Email
                ,@PhoneNumber
                ,@DateOfBirth
                ,@Sex
                ,@Password)";

            _logger.Debug("infoInsert" + JsonConvert.SerializeObject(infoInsert));

            using (var command = new SqlCommand(strSQl, connection))
            {
             
                AddSqlParameter(command, "@FullName", infoInsert.FullName, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@Email", infoInsert.Email, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, "@PhoneNumber", infoInsert.PhoneNumber, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, "@DateOfBirth", DateTime.ParseExact(infoInsert.DateOfBirth, "d/M/yyyy", CultureInfo.InvariantCulture), System.Data.SqlDbType.DateTime);
                AddSqlParameter(command, "@Sex", infoInsert.Sex, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@Password", infoInsert.Password, System.Data.SqlDbType.VarChar);

                WriteLogExecutingCommand(command);

                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool CheckInsertPhoneNumber (SqlConnection connection  , string phoneNumber)
        {
            var result = new CustomerInfo();
            string strSQL = @"
            SELECT [PhoneNumber]

            FROM [Customer] where PhoneNumber = @PhoneNumber ";

            using (var command = new SqlCommand(strSQL, connection))
            {
                AddSqlParameter(command, "@PhoneNumber", phoneNumber, System.Data.SqlDbType.VarChar);
                WriteLogExecutingCommand (command);
                return command.ExecuteReader().HasRows;

            }


        }
        public bool DeleteCustomer(SqlConnection connection, string PhoneNumber)
        {
            string strSQL = @"
               DELETE [Customer] WHERE PhoneNumber = @PhoneNumber";
            using (var command = new SqlCommand(strSQL, connection))
            {
                AddSqlParameter(command, "@PhoneNumber", PhoneNumber, System.Data.SqlDbType.VarChar);
                WriteLogExecutingCommand(command);

                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateCustomer(SqlConnection connection, CustomerInfo infoUpdate)
        {
            string strSql = @"
               UPDATE [Customer]
               SET [AutoID] = @AuttoID                      
                        ,[FullName] = @FullName
                        ,[Email] = @Email
                        ,[DateOfBirth] = @DateOfBirth
                        ,[Sex] = @Sex
                        ,[Password] = @Password
               WHERE [PhoneNumber] = @PhoneNumber";
            using (var command = new SqlCommand(strSql, connection))
            {
               
                AddSqlParameter(command, "@FullName", infoUpdate.FullName, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@Email", infoUpdate.Email, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, "@PhoneNumber", infoUpdate.PhoneNumber, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, "@DateOfBirth", infoUpdate.DateOfBirth, System.Data.SqlDbType.DateTime);
                AddSqlParameter(command, "@Sex", infoUpdate.Sex, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@Password", infoUpdate.Password, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, "@AutoID", infoUpdate.AutoID, System.Data.SqlDbType.Int);
                WriteLogExecutingCommand(command);
                return command.ExecuteNonQuery() > 0;
            }

        }




    }
}
