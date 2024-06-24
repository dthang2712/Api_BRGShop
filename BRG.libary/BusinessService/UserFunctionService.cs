using BRG.libary.BusinessService.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRG.libary.BusinessService
{
    public class UserFunctionService : BaseService<UserFunctionService>
    {
        public class UserFunctionInfo
        {
            public string UserID { get; set; }
            public string FunctionID { get; set; }
            public int AutoID { get; set; }

            public void CopyValue(UserFunctionInfo info)
            {
                this.UserID = info.UserID;
                this.FunctionID = info.FunctionID;
                this.AutoID = info.AutoID;

            }
        }
        public List<UserFunctionInfo> GetListUserFunction(SqlConnection connection, string strSearch = null)
        {
            var result = new List<UserFunctionInfo>();
            string strSQL = @"
            SELECT[UserID]
                   ,[FunctionID]
                   ,[AutoID]
            FROM [UserFunction] WHERE 1=1 ";

            using (var command = new SqlCommand(strSQL, connection))
            {
                if (!string.IsNullOrEmpty(strSearch))
                {
                    command.CommandText += "and UserFunction like @strSearch or UserID like @strSearch";
                    AddSqlParameter(command, "@strSearch", "@" + strSearch + "%", System.Data.SqlDbType.NVarChar);
                }
                WriteLogExecutingCommand(command);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new UserFunctionInfo();
                        item.AutoID = GetDbReaderValue<int>(reader["AutoID"]);
                        item.UserID = GetDbReaderValue<string>(reader["UserID"]);
                        item.FunctionID = GetDbReaderValue<string>(reader["FunctionID"]);
                        result.Add(item);
                    }
                }
            }

            return result;
        }
        public bool InsertUserFunction(SqlConnection connection, UserFunctionInfo infoInsert)
        {
            string strSQl = @"
            INSERT INTO  [UserFunction]
                ([AutoID]
                ,[UserID]
                ,[FunctionID]
            VALUES
                (@AutoID
                ,@UserID
                ,@FunctionID)";


            using (var command = new SqlCommand(strSQl, connection))
            {
                AddSqlParameter(command, "@CategoryID", infoInsert.UserID, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, "@CategoryName", infoInsert.FunctionID, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, "@AutoID", infoInsert.AutoID, System.Data.SqlDbType.Int);

                WriteLogExecutingCommand(command);

                return command.ExecuteNonQuery() > 0;
            }
        }
        public bool DeleteUserFunction(SqlConnection connection, string UserID)
        {
            string strSQL = @"
               DELETE [UserFunction] WHERE UserID = @UserID";
            using (var command = new SqlCommand(strSQL, connection))
            {
                AddSqlParameter(command, "@UserID", UserID, System.Data.SqlDbType.VarChar);
                WriteLogExecutingCommand(command);

                return command.ExecuteNonQuery() > 0;
            }
        }
        public bool UpdateUserFunction(SqlConnection connection, UserFunctionInfo infoUpdate)
        {
            string strSql = @"
               UPDATE [UserFunction]
               SET [FunctionID] = @FunctionID
                        ,[AutoID] = @AuttoID
               WHERE [UserID] = @UserID";
            using (var command = new SqlCommand(strSql, connection))
            {
                AddSqlParameter(command, @"UserID", infoUpdate.UserID, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, @"FunctionID", infoUpdate.FunctionID, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, @"AutoID", infoUpdate.AutoID, System.Data.SqlDbType.Int);
                WriteLogExecutingCommand(command);
                return command.ExecuteNonQuery() > 0;
            }

        }




    }
}
