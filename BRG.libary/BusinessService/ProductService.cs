using BRG.libary.BusinessService.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Reflection;

namespace BRG.libary.BusinessService
{
    public class ProductService : BaseService<ProductService>
    {
        public class ProductInfo
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public byte[] ProductImage { get; set; }
            public Decimal Price { get; set; }
            public int CategoryID { get; set; }
            public string Unit { get; set; }
            public string Content { get; set; }
            public int Amount { get; set; }
         
          
          

            public void CopyValue(ProductInfo info)
            {
                this.ProductID = info.ProductID;
                this.ProductName = info.ProductName;
                this.ProductImage = info.ProductImage;
                this.Price = info.Price;
                this.CategoryID = info.CategoryID;
                this.Unit = info.Unit;
                this.Content = info.Content;
                this.Amount = info.Amount;
            }
        }

        public List<ProductInfo> GetListProduct(SqlConnection connection, string strSearch = null)
        {
            var result = new List<ProductInfo>();
            string strSQL = @"
            SELECT [ProductID]
                    ,[ProductName]
                    ,[CategoryID]
                    ,[Unit]
                    ,[Amount]
                    ,[Price]
                    ,[Content]
                    ,[ProductImage]
                    
            FROM [Product] WHERE 1=1 ";
            using (var command = new SqlCommand(strSQL, connection))
            {
                if (!string.IsNullOrEmpty(strSearch))
                {
                    command.CommandText += "and ProductID like @strSearch or ProductName like @strSearch ";
                    AddSqlParameter(command, "@strSearch", "%" + strSearch + "%", System.Data.SqlDbType.NVarChar);
                }
                WriteLogExecutingCommand(command);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new ProductInfo();
                        item.ProductID = GetDbReaderValue<int>(reader["ProductID"]);
                        item.ProductName = GetDbReaderValue<String>(reader["ProductName"]);
                        item.CategoryID = GetDbReaderValue<int>(reader["CategoryID"]);
                        item.Unit = GetDbReaderValue<String>(reader["Unit"]);
                        item.Amount = GetDbReaderValue<int>(reader["Amount"]);
                        item.Price = GetDbReaderValue<decimal>(reader["Price"]);
                        item.Content = GetDbReaderValue<String>(reader["Content"]);
                        item.ProductImage = GetDbReaderValue<byte[]>(reader["ProductImage"]);
                        result.Add(item);
                    }
                }

            }
            return result;
        }

        public bool InsertProduct(SqlConnection connection, ProductInfo infoInsert)
        {
            string strSQL = @"
            INSERT INTO [Product]
                ([ProductID]
                    ,[ProductName]
                    ,[CategoryID]
                    ,[ProviderID]
                    ,[Unit]
                    ,[Amount]
                    ,[Price]
                    ,[Content]
                    ,[ProductImage]
            VALUES
                (@[ProductID]
                    ,@[ProductName]
                    ,@[CategoryID]
                    ,@[ProviderID]
                    ,@[Unit]
                    ,@[Amount]
                    ,@[Price]
                    ,@[Content]
                    ,@[ProductImage]";
            using (var command = new SqlCommand(strSQL, connection))
            {
                AddSqlParameter(command, "@ProductID", infoInsert.ProductID, System.Data.SqlDbType.Int);
                AddSqlParameter(command, "@ProductName", infoInsert.ProductName, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@CategoryID", infoInsert.CategoryID, System.Data.SqlDbType.Int);
                AddSqlParameter(command, "@Unit", infoInsert.Unit, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@Amount", infoInsert.Amount, System.Data.SqlDbType.Int);
                AddSqlParameter(command, "@Price", infoInsert.Price, System.Data.SqlDbType.Money);
                AddSqlParameter(command, "@ProductImage", infoInsert.ProductImage, System.Data.SqlDbType.Image);

                WriteLogExecutingCommand(command);

                return command.ExecuteNonQuery() > 0;
            }
        }
        public bool DeleteProduct(SqlConnection connection, string ProductID)
        {
            string strSQL = @"
            DELETE [Product] WHERE ProductID = @ProductID ";
            using (var command = new SqlCommand(strSQL, connection))
            {
                AddSqlParameter(command, "@ProductID", ProductID, System.Data.SqlDbType.VarChar);
                WriteLogExecutingCommand(command);

                return command.ExecuteNonQuery() > 0;
            }
        }
        public bool UpdateProduct(SqlConnection connection, ProductInfo infoUpdate)
        {
            string strSQL = @"
            UPDATE [Product]
            SET [ProductName]= @ProductName
                    ,[CategoryID]= @CategoryID
                    ,[Unit]= @Unit
                    ,[Amount]= @Amount
                    ,[Price]= @Price
                    ,[Content]= @Content
                    ,[ProductImage= @ProductImage
            WHERE [ProductID] = @ProductID";

            using (var command = new SqlCommand(strSQL, connection))
            {
                AddSqlParameter(command, "@ProductID", infoUpdate.ProductID, System.Data.SqlDbType.Int);
                AddSqlParameter(command, "@ProductName", infoUpdate.ProductName, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@CategoryID", infoUpdate.CategoryID, System.Data.SqlDbType.Int);
                AddSqlParameter(command, "@Unit", infoUpdate.Unit, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@Amount", infoUpdate.Amount, System.Data.SqlDbType.Int);
                AddSqlParameter(command, "@Price", infoUpdate.Price, System.Data.SqlDbType.Money);
                AddSqlParameter(command, "@ProductImage", infoUpdate.ProductImage, System.Data.SqlDbType.Image);

                WriteLogExecutingCommand(command);
                return command.ExecuteNonQuery() > 0;
            }
        }

        public List<ProductInfo> GetListProductCategory(SqlConnection connection, string CategoryID)
        {
            var result = new List<ProductInfo>();
            string strSQL = @"
            SELECT [ProductID]
                    ,[ProductName]
                    ,[CategoryID]
                    ,[Unit]
                    ,[Amount]
                    ,[Price]
                    ,[Content]
                    ,[ProductImage]
            FROM [Product] WHERE CategoryID = @CategoryID";
            using (var command = new SqlCommand(strSQL, connection))
            {
                AddSqlParameter(command, "@CategoryID", CategoryID, System.Data.SqlDbType.VarChar);
                WriteLogExecutingCommand(command);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new ProductInfo();
                        item.ProductID = GetDbReaderValue<int>(reader["ProductID"]);
                        item.ProductName = GetDbReaderValue<String>(reader["ProductName"]);
                        item.CategoryID = GetDbReaderValue<int>(reader["CategoryID"]);
                        item.Unit = GetDbReaderValue<String>(reader["Unit"]);
                        item.Amount = GetDbReaderValue<int>(reader["Amount"]);
                        item.Price = GetDbReaderValue<decimal>(reader["Price"]);
                        item.Content = GetDbReaderValue<String>(reader["Content"]);
                        item.ProductImage = GetDbReaderValue<byte[]>(reader["ProductImage"]);
                        result.Add(item);
                    }

                }
            }
            return result;
        }
    }
}
