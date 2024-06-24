using BRG.libary.BusinessService.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRG.libary.BusinessService
{
    public class ProductImageService : BaseService<ProductImageService>
    {
        public class ProductImageInfo
        {
            public string ProductID { get; set; }
            public Byte Image { get; set; }
            public int AutoID { get; set; }

            public void CopyValue(ProductImageInfo info)
            {
                this.ProductID = info.ProductID;
                this.Image = info.Image;
                this.AutoID = info.AutoID;

            }
        }
        public List<ProductImageInfo> GetListProductImage(SqlConnection connection, string strSearch = null)
        {
            var result = new List<ProductImageInfo>();
            string strSQL = @"
            SELECT[ProductID]
                   ,[Image]
                   ,[AutoID]
            FROM [ProductImage] WHERE 1=1 ";

            using (var command = new SqlCommand(strSQL, connection))
            {
                if (!string.IsNullOrEmpty(strSearch))
                {
                    command.CommandText += "and ProductImage like @strSearch or ProductID like @strSearch";
                    AddSqlParameter(command, "@strSearch", "@" + strSearch + "%", System.Data.SqlDbType.NVarChar);
                }
                WriteLogExecutingCommand(command);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new ProductImageInfo();
                        item.AutoID = GetDbReaderValue<int>(reader["AutoID"]);
                        item.Image = GetDbReaderValue<byte>(reader["Image"]);
                        item.ProductID = GetDbReaderValue<string>(reader["ProductID"]);
                        result.Add(item);
                    }
                }
            }

            return result;
        }
        public bool InsertProductImage(SqlConnection connection, ProductImageInfo infoInsert)
        {
            string strSQl = @"
            INSERT INTO  [ProductImage]
                ([AutoID]
                ,[ProductID]
                ,[Image]
            VALUES
                (@AutoID
                ,@ProductID
                ,@Image)";


            using (var command = new SqlCommand(strSQl, connection))
            {
                AddSqlParameter(command, "@ProductID", infoInsert.ProductID, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, "@Image", infoInsert.Image, System.Data.SqlDbType.Image);
                AddSqlParameter(command, "@AutoID", infoInsert.AutoID, System.Data.SqlDbType.Int);

                WriteLogExecutingCommand(command);

                return command.ExecuteNonQuery() > 0;
            }
        }
        public bool DeleteProductImage(SqlConnection connection, string ProductID)
        {
            string strSQL = @"
               DELETE [ProductImage] WHERE ProductID = @ProductID";
            using (var command = new SqlCommand(strSQL, connection))
            {
                AddSqlParameter(command, "@ProductID", ProductID, System.Data.SqlDbType.VarChar);
                WriteLogExecutingCommand(command);

                return command.ExecuteNonQuery() > 0;
            }
        }
        public bool UpdateProductImage(SqlConnection connection, ProductImageInfo infoUpdate)
        {
            string strSql = @"
               UPDATE [ProductImage]
               SET [Image] = @Image
                        ,[AutoID] = @AuttoID
               WHERE [ProductID] = @ProductID";
            using (var command = new SqlCommand(strSql, connection))
            {
                AddSqlParameter(command, @"ProductID", infoUpdate.ProductID, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, @"Image", infoUpdate.Image, System.Data.SqlDbType.Image);
                AddSqlParameter(command, @"AutoID", infoUpdate.AutoID, System.Data.SqlDbType.Int);
                WriteLogExecutingCommand(command);
                return command.ExecuteNonQuery() > 0;
            }

        }




    }
}
