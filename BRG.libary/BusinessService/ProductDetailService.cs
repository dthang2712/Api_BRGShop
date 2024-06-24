using BRG.libary.BusinessService.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRG.libary.BusinessService
{
    public class ProductDetailService : BaseService<ProductDetailService>
    {
        public class ProductDetailInfo
        {
            public string ProductID { get; set; }
            public string Key { get; set; }
            public string Description { get; set; }
            public int AutoID { get; set; }
            

            public void CopyValue(ProductDetailInfo info)
            {
                this.ProductID = info.ProductID;
                this.Key = info.Key;
                this.Description = info.Description;
                this.AutoID = info.AutoID;
                
            }
        }

        public List<ProductDetailInfo> GetListProductDetail(SqlConnection connection, string strSearch = null)
        {
            var result = new List<ProductDetailInfo>();
            string strSQL = @"
            SELECT [ProductID]
                    ,[Key]
                    ,[Description]
                    ,[AutoID]
                   
            FROM [ProductDetail] WHERE 1=1 ";

            using (var command = new SqlCommand(strSQL, connection))
            {
                if (!String.IsNullOrEmpty(strSearch))
                {
                    command.CommandText += " and ProductDetail like @strSearch or ProductID like @strSearch";
                    AddSqlParameter(command, "@strSearch", "%" + strSearch + "%", System.Data.SqlDbType.NVarChar);
                }

                WriteLogExecutingCommand(command);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new ProductDetailInfo();
                        item.ProductID = GetDbReaderValue<string>(reader["ProductID"]);
                        item.Key = GetDbReaderValue<string>(reader["Key"]);
                        item.Description = GetDbReaderValue<string>(reader["Description"]);
                        item.AutoID = GetDbReaderValue<int>(reader["AutoID"]);
                        
                        result.Add(item);
                    }
                }
            }

            return result;
        }

        public bool InsertProductDetail(SqlConnection connection, ProductDetailInfo infoInsert)
        {
            string strSQL = @"
            INSERT INTO [ProductDetail]
                ([ProductID]
                ,[Key]
                ,[Description]
                ,[AutoID]
               
            VALUES
                (@ProductID
                ,@Key
                ,@Description
                ,@AutoID)";

            using (var command = new SqlCommand(strSQL, connection))
            {
                AddSqlParameter(command, "@ProductID", infoInsert.ProductID, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, "@Key", infoInsert.Key, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@Description", infoInsert.Description, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@AutoID", infoInsert.AutoID, System.Data.SqlDbType.Int);
                
                WriteLogExecutingCommand(command);

                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteProductDetail(SqlConnection connection, string ProductID)
        {
            string strSQL = @"
            DELETE [ProductDetail] WHERE ProductID = @ProductID";

            using (var command = new SqlCommand(strSQL, connection))
            {
                AddSqlParameter(command, "@ProductID", ProductID, System.Data.SqlDbType.VarChar);
                WriteLogExecutingCommand(command);

                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateProductDetail(SqlConnection connection, ProductDetailInfo infoUpdate)
        {
            string strSQL = @"
            UPDATE [ProductDetail]
            SET 
                   [Key] = @Key
                  ,[Description] = @Description
                  ,[AutoID] = AutoID
            WHERE [ProductID] = @ProductID";

            using (var command = new SqlCommand(strSQL, connection))
            {
                AddSqlParameter(command, "@ProductID", infoUpdate.ProductID, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, "@Key", infoUpdate.Key, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@Description", infoUpdate.Description, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@AutoID", infoUpdate.AutoID, System.Data.SqlDbType.Int);

                WriteLogExecutingCommand(command);
                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}
