using BRG.libary.BusinessService.Common;
using System.Data.SqlClient;

namespace BRG.libary.BusinessService
{
    public class CategoryService : BaseService<CategoryService>
    {
        public class CategoryInfo
        {
            public string CategoryName { get; set; }
            public int CategoryID { get; set; }

            public void CopyValue(CategoryInfo info)
            {
                this.CategoryName = info.CategoryName;
                this.CategoryID = info.CategoryID;

            }
        }
        public List<CategoryInfo> GetListCategory(SqlConnection connection, string strSearch = null)
        {
            var result = new List<CategoryInfo>();
            string strSQL = @"
            SELECT [CategoryID]
                   ,[CategoryName]
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
                        item.CategoryName = GetDbReaderValue<string>(reader["CategoryName"]);
                        item.CategoryID = GetDbReaderValue<int>(reader["CategoryID"]);
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
                ([CategoryID]
                ,[CatogoryName]
            VALUES
                (@CategoryID
                ,@CategoryName)";


            using (var command = new SqlCommand(strSQl, connection))
            {
                AddSqlParameter(command, "@CategoryID", infoInsert.CategoryID ,System.Data.SqlDbType.Int);
                AddSqlParameter(command, "@CategoryName", infoInsert.CategoryName, System.Data.SqlDbType.NVarChar);

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
               WHERE [CategoryID] = @CategoryID";
            using (var command = new SqlCommand(strSql, connection))
            {
                AddSqlParameter(command, @"CategoryID" ,infoUpdate.CategoryID,System.Data.SqlDbType.Int);
                AddSqlParameter(command, @"CategoryName", infoUpdate.CategoryName, System.Data.SqlDbType.NVarChar);
                WriteLogExecutingCommand(command);
                return command.ExecuteNonQuery() > 0; 
            } 
                
        }




    }
}



