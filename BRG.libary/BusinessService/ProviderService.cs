using BRG.libary.BusinessService.Common;
using log4net.ObjectRenderer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRG.libary.BusinessService
{
    public class ProviderService : BaseService<ProviderService>
    {
        public class ProviderInfo
        {
            public string ProviderName { get; set; }
            public string ProviderID { get; set; }
            public int AutoID { get; set; }

            public void CopyValue(ProviderInfo info)
            {
                this.AutoID = info.AutoID;
                this.ProviderName = info.ProviderName;
                this.ProviderID = info.ProviderID;
            }
        }

        public List<ProviderInfo> GetListProvider(SqlConnection connection, string strSearch = null )
        {
            var result = new List<ProviderInfo>();
            string strSQL = @"
            SELECT [ProviderID]
                    ,[ProviderName]
                    ,[AutoID]
            FROM [Provider] WHERE 1=1 ";
            using (var command = new SqlCommand(strSQL, connection))
            {
                if (!string.IsNullOrEmpty(strSearch))
                {
                    command.CommandText += "and Provider like @strSearch or ProviderName like @strSearch ";
                    AddSqlParameter(command, "@strSearch", "%"  + strSearch + "%", System.Data.SqlDbType.NVarChar);
                }
                WriteLogExecutingCommand(command);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read()) 
                    {
                        var item = new ProviderInfo();
                        item.ProviderID = GetDbReaderValue<String>(reader["ProviderID"]);
                        item.ProviderName = GetDbReaderValue<String>(reader["ProviderName"]);
                        item.AutoID = GetDbReaderValue<int>(reader["AutoID"]);
                        result.Add(item);
                    }
                }

            }    
            return result;
        }
        public bool InsertProvider(SqlConnection connection, ProviderInfo infoInsert)
        {
            string strSQL = @"
            INSERT INTO [Provider]
                ([ProviderID]
                ,[ProviderName]
                ,[AutoID]
            VALUES
                (@ProviderID
                ,@ProviderName 
                ,AutoID";
            using (var command = new SqlCommand(strSQL, connection))
            {
                AddSqlParameter(command, "@ProviderID", infoInsert.ProviderID, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, "@ProviderName", infoInsert.ProviderName, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@AutoID", infoInsert.AutoID, System.Data.SqlDbType.Int);

                WriteLogExecutingCommand(command);

                return command.ExecuteNonQuery() > 0 ;
            }    
        }
        public bool DeleteProvider(SqlConnection connection, string ProviderID)
        {
            string strSQL = @"
            DELETE [Provider] WHERE ProviderID = @ProviderID ";
            using (var command = new SqlCommand(strSQL, connection))
            {
                AddSqlParameter(command, "@ProviderID", ProviderID, System.Data.SqlDbType.VarChar);
                WriteLogExecutingCommand(command);

                return command.ExecuteNonQuery() > 0;
            }
        }
        public bool UpdateProvider(SqlConnection connection, ProviderInfo infoUpdate) 
        {
            string strSQL = @"
            UPDATE [Provider]
            SET [ProviderName] = @ProviderName         
                ,[AutoID] = @AutoID
            WHERE [ProviderID] = @ProviderID";

            using (var command = new SqlCommand(strSQL,connection))
            {
                AddSqlParameter(command, "@ProviderID", infoUpdate.ProviderID, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, "@ProviderName", infoUpdate.ProviderName, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@AutoID", infoUpdate.AutoID, System.Data.SqlDbType.Int);

                WriteLogExecutingCommand(command);
                return command.ExecuteNonQuery() > 0; 
            }    
        }
    }
    
}
