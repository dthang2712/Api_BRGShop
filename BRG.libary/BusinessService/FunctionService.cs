using BRG.libary.BusinessService.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRG.libary.BusinessService
{
    public class FunctionService : BaseService<FunctionService>
    {
        public class FunctionInfo
        {
            public string FunctionID { get; set; }
            public string FunctionName { get; set; }
            public string Description { get; set; }
            public int AutoID { get; set; }

            public void CopyValue(FunctionInfo info)
            {
                this.FunctionID = info.FunctionID;
                this.FunctionName = info.FunctionName;
                this.Description = info.Description;

                this.AutoID = info.AutoID;

            }
        }
        public List<FunctionInfo> GetListFunction(SqlConnection connection, string strSearch = null)
        {
            var result = new List<FunctionInfo>();
            string strSQL = @"
            SELECT[FunctionID]
                   ,[FunctionName]
                   ,[Description]
                   ,[AutoID]
            FROM [Function] WHERE 1=1 ";

            using (var command = new SqlCommand(strSQL, connection))
            {
                if (!string.IsNullOrEmpty(strSearch))
                {
                    command.CommandText += "and Function like @strSearch or FunctionID like @strSearch";
                    AddSqlParameter(command, "@strSearch", "@" + strSearch + "%", System.Data.SqlDbType.NVarChar);
                }
                WriteLogExecutingCommand(command);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new FunctionInfo();
                        item.AutoID = GetDbReaderValue<int>(reader["AutoID"]);
                        item.FunctionID = GetDbReaderValue<string>(reader["FunctionID"]);
                        item.FunctionName = GetDbReaderValue<string>(reader["FunctionName"]);
                        item.Description = GetDbReaderValue<string>(reader["Description"]);
                        result.Add(item);
                    }
                }
            }

            return result;
        }
        public bool InsertFunction(SqlConnection connection, FunctionInfo infoInsert)
        {
            string strSQl = @"
            INSERT INTO  [Function]
                ([AutoID]
                ,[FunctionID]
                ,[FunctionName]
                ,[Description]
            VALUES
                (@AutoID
                ,@FunctionID
                ,@FunctionName
                ,Description@)";


            using (var command = new SqlCommand(strSQl, connection))
            {
                AddSqlParameter(command, "@FunctionID", infoInsert.FunctionID, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, "@FunctionName", infoInsert.FunctionName, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@Description", infoInsert.Description, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@AutoID", infoInsert.AutoID, System.Data.SqlDbType.Int);

                WriteLogExecutingCommand(command);

                return command.ExecuteNonQuery() > 0;
            }
        }
        public bool DeleteFunction(SqlConnection connection, string FunctionID)
        {
            string strSQL = @"
               DELETE [Function] WHERE FunctionID = @FunctionID";
            using (var command = new SqlCommand(strSQL, connection))
            {
                AddSqlParameter(command, "@FunctionID", FunctionID, System.Data.SqlDbType.VarChar);
                WriteLogExecutingCommand(command);

                return command.ExecuteNonQuery() > 0;
            }
        }
        public bool UpdateFunction(SqlConnection connection, FunctionInfo infoUpdate)
        {
            string strSql = @"
               UPDATE [Function]
               SET [FunctionName] = @FunctionName
                        ,[Description] = @Description
                        ,[AutoID] = @AuttoID
               WHERE [FunctionID] = @FunctionID";
            using (var command = new SqlCommand(strSql, connection))
            {
                AddSqlParameter(command, "@FunctionID", infoUpdate.FunctionID, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, "@FunctionName", infoUpdate.FunctionName, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@Description", infoUpdate.Description, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@AutoID", infoUpdate.AutoID, System.Data.SqlDbType.Int);
                WriteLogExecutingCommand(command);
                return command.ExecuteNonQuery() > 0;
            }

        }




    }
}
