using BRG.libary.BusinessService.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRG.libary.BusinessService
{
    public class UserService : BaseService<UserService>
    {
        public class UserInfo
        {
            public int UserID { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string FullName { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string Sex { get; set; }
            public string PhoneNumber { get; set; }

            public void CopyValue(UserInfo info)
            {
                this.UserID = info.UserID;
                this.UserName = info.UserName;
                this.Password = info.Password;
                this.FullName = info.FullName;
                this.DateOfBirth = info.DateOfBirth;
                this.Sex = info.Sex;
                this.PhoneNumber = info.PhoneNumber;

            }
        }
        public List<UserInfo> GetListUser(SqlConnection connection, string strSearch = null)
        {
            var result = new List<UserInfo>();
            string strSQL = @"
            SELECT[UserID]
                   ,[UserName]
                   ,[Password]
                   ,[FullName]
                   ,[DateOfBirth]
                   ,[Sex]
                   ,[PhoneNumber]
            FROM [User] WHERE 1=1 ";

            using (var command = new SqlCommand(strSQL, connection))
            {
                if (!string.IsNullOrEmpty(strSearch))
                {
                    command.CommandText += "and User like @strSearch or UserID like @strSearch";
                    AddSqlParameter(command, "@strSearch", "@" + strSearch + "%", System.Data.SqlDbType.NVarChar);
                }
                WriteLogExecutingCommand(command);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new UserInfo();
                        item.UserID = GetDbReaderValue<int>(reader["UserID"]);
                        item.UserName = GetDbReaderValue<string>(reader["UserName"]);
                        item.Password = GetDbReaderValue<string>(reader["Password"]);
                        item.FullName = GetDbReaderValue<string>(reader["FullName"]);
                        item.DateOfBirth = GetDbReaderValue<DateTime>(reader["DateOfBirth"]);
                        item.Sex = GetDbReaderValue<string>(reader["Sex"]);
                        item.PhoneNumber = GetDbReaderValue<string>(reader["PhoneNumber"]);
                        result.Add(item);
                    }
                }
            }

            return result;
        }
        public bool InsertUser(SqlConnection connection, UserInfo infoInsert)
        {
            string strSQl = @"
            INSERT INTO  [User]
                ([UserID]
                ,[UserName]
                ,[Password]
                ,[FullName]
                ,[DateOfBirth]
                ,[Sex]
                ,[PhoneNumber]
            VALUES
                (@UserID
                ,@UserName
                ,@Password
                ,@FullName
                ,@Sex
                ,@DateOfBirth
                ,@PhoneNumber)";


            using (var command = new SqlCommand(strSQl, connection))
            {
                AddSqlParameter(command, "@UserID", infoInsert.UserID, System.Data.SqlDbType.Int);
                AddSqlParameter(command, "@UserName", infoInsert.UserName, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@Password", infoInsert.Password, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, "@FullName", infoInsert.FullName, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@Sex", infoInsert.Sex, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, "@DateOfBirth", infoInsert.DateOfBirth, System.Data.SqlDbType.Date);
                AddSqlParameter(command, "@PhoneNumber", infoInsert.PhoneNumber, System.Data.SqlDbType.VarChar);

                WriteLogExecutingCommand(command);

                return command.ExecuteNonQuery() > 0;
            }
        }
        public bool DeleteUser(SqlConnection connection, string UserID)
        {
            string strSQL = @"
               DELETE [User] WHERE UserID = @UserID";
            using (var command = new SqlCommand(strSQL, connection))
            {
                AddSqlParameter(command, "@UserID", UserID, System.Data.SqlDbType.VarChar);
                WriteLogExecutingCommand(command);

                return command.ExecuteNonQuery() > 0;
            }
        }
        public bool UpdateUser(SqlConnection connection, UserInfo infoUpdate)
        {
            string strSql = @"
               UPDATE [User]
               SET [UserName] = @UserName
                        ,[Password] = @Password
                        ,[FullName] = @FullName
                        ,[Sex] = @Sex
                        ,[DateOfBirth] = @DateOfBirth
                        ,[PhoneNumber] = @PhoneNumber
               WHERE [UserID] = @UserID";
            using (var command = new SqlCommand(strSql, connection))
            {
                AddSqlParameter(command, "@UserID", infoUpdate.UserID, System.Data.SqlDbType.Int);
                AddSqlParameter(command, "@UserName", infoUpdate.UserName, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@Password", infoUpdate.Password, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, "@FullName", infoUpdate.FullName, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@Sex", infoUpdate.Sex, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, "@DateOfBirth", infoUpdate.DateOfBirth, System.Data.SqlDbType.Date);
                AddSqlParameter(command, "@PhoneNumber", infoUpdate.PhoneNumber, System.Data.SqlDbType.VarChar);
                WriteLogExecutingCommand(command);
                return command.ExecuteNonQuery() > 0;
            }

        }




    }
}
