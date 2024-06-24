using BRG.libary.BusinessService.Common;
using System.Data.SqlClient;

namespace BRG.libary.BusinessService
{
    public class CategoryService : BaseService<CategoryService>
    {
        public class CategoryInfo
        {
            public string CategoryName { get; set; }
            public string CatergoryID { get; set; }
            public int AutoID { get; set; }

            public void CopyValue(CategoryInfo info)
            {
                this.CategoryName = info.CategoryName;
                this.CatergoryID = info.CatergoryID;
                this.AutoID = info.AutoID;

            }
        }
        public List<CategoryInfo> GetListCategory(SqlConnection connection, string strSearch = null)
        {
            var result = new List<CategoryInfo>();
            string strSQL = @"
            SELECT[CategoryID]
                   ,[CategoryName]
                   ,[AutoID]
            FROM [Category] WHERE 1=1 ";

            using (var command = new SqlCommand(strSQL, connection))
            {
                if (!string.IsNullOrEmpty(strSearch))
                {
                    command.CommandText += "and Category like @strSearch or CategoryName like @strSearch";
                    AddSqlParameter(command, "@strSearch", "@" + strSearch + "%", System.Data.SqlDbType.NVarChar);
                }
                WriteLogExecutingCommand(command);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new CategoryInfo();
                        item.AutoID = GetDbReaderValue<int>(reader["AutoID"]);
                        item.CategoryName = GetDbReaderValue<string>(reader["CategoryName"]);
                        item.CatergoryID = GetDbReaderValue<string>(reader["CategoryID"]);
                        result.Add(item);
                    }
                }
            }

            return result;
        }
        public bool InsertCategory(SqlConnection connection, CategoryInfo infoInsert) 
        {
            string strSQl = @"
            INSERT INTO  [Category]
                ([AutoID]
                ,[CatogoryID]
                ,[CatogoryName]
            VALUES
                (@AutoID
                ,@CatogoryID
                ,@CatogoryName)";


            using (var command = new SqlCommand(strSQl, connection))
            {
                AddSqlParameter(command, "@CategoryID", infoInsert.CatergoryID ,System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, "@CategoryName", infoInsert.CategoryName, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@AutoID", infoInsert.AutoID, System.Data.SqlDbType.Int);

                WriteLogExecutingCommand(command);

                return command.ExecuteNonQuery() > 0;
            }
        }
        public bool DeleteCategory(SqlConnection connection, string CategoryID)
        {
            string strSQL = @"
               DELETE [Category] WHERE CategoryID = @CategoryID";
            using (var command = new SqlCommand(strSQL, connection))
            {
                AddSqlParameter(command, "@CategoryID", CategoryID, System.Data.SqlDbType.VarChar);
                WriteLogExecutingCommand(command);

                return command.ExecuteNonQuery() > 0;
            }
        }
        public bool UpdateCategory(SqlConnection connection, CategoryInfo infoUpdate) 
        {
            string strSql = @"
               UPDATE [Category]
               SET [CategoryName] = @CategoryName
                        ,[AutoID] = @AuttoID
               WHERE [CategoryID] = @CategoryID";
            using (var command = new SqlCommand(strSql, connection))
            {
                AddSqlParameter(command, @"CategoryID" ,infoUpdate.CatergoryID,System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, @"CategoryName", infoUpdate.CategoryName, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, @"AutoID", infoUpdate.AutoID, System.Data.SqlDbType.Int);
                WriteLogExecutingCommand(command);
                return command.ExecuteNonQuery() > 0; 
            } 
                
        }




    }
}



