using BRG.libary.BusinessService.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRG.libary.BusinessService
{
    public class ProductDiscountService : BaseService<ProductDiscountService>
    {
        public class ProductDiscountInfo
        {
            public string ProductID { get; set; }
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
            public int AutoID { get; set; }
            public int Discount { get; set; }
            public Decimal PriceDiscount { get; set; }

            public void CopyValue(ProductDiscountInfo info)
            {
                this.ProductID = info.ProductID;
                this.DateFrom = info.DateFrom;
                this.AutoID = info.AutoID;  
                this.DateTo = info.DateTo;
                this.Discount = info.Discount;
                this.PriceDiscount = info.PriceDiscount;

            }
        }
        public List<ProductDiscountInfo> GetListProductDiscount(SqlConnection connection, string strSearch = null)
        {
            var result = new List<ProductDiscountInfo>();
            string strSQL = @"
            SELECT[ProductID]
                   ,[DateFrom]
                   ,[AutoID]
                   ,[DateTo]
                   ,[Discount]
                   ,[PriceDiscount]
               
            FROM [ProductDiscount] WHERE 1=1 ";

            using (var command = new SqlCommand(strSQL, connection))
            {
                if (!string.IsNullOrEmpty(strSearch))
                {
                    command.CommandText += "and ProductDiscount like @strSearch or ProductID like @strSearch"; //
                    AddSqlParameter(command, "@strSearch", "@" + strSearch + "%", System.Data.SqlDbType.NVarChar);
                }
                WriteLogExecutingCommand(command);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new ProductDiscountInfo();
                        item.AutoID = GetDbReaderValue<int>(reader["AutoID"]);
                        item.ProductID = GetDbReaderValue<string>(reader["ProductID"]);
                        item.DateFrom = GetDbReaderValue<DateTime>(reader["DateFrom"]);
                        item.DateTo = GetDbReaderValue<DateTime>(reader["DateTo"]);
                        item.Discount = GetDbReaderValue<int>(reader["Discount"]);
                        item.PriceDiscount = GetDbReaderValue<decimal>(reader["PriceDiscount"]);
                        
                        result.Add(item);
                    }
                }
            }

            return result;
        }
        public bool InsertProductDiscount(SqlConnection connection, ProductDiscountInfo infoInsert)
        {
            string strSQl = @"
            INSERT INTO  [ProductDiscount]
                ([AutoID]
                ,[ProductID]
                ,[DateFrom]
                ,[DateTo]
                ,[Discount]
                ,[PriceDiscount]
            VALUES
                (@AutoID
                ,@ProductID
                ,@DateFrom
                ,@DateTo
                ,@Discount
                ,@PriceDiscount)";


            using (var command = new SqlCommand(strSQl, connection))
            {
                AddSqlParameter(command, "@ProductID", infoInsert.ProductID, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, "@DateFrom", infoInsert.DateFrom, System.Data.SqlDbType.DateTime);
                AddSqlParameter(command, "@DateTo", infoInsert.DateTo, System.Data.SqlDbType.DateTime);
                AddSqlParameter(command, "@Discount", infoInsert.Discount, System.Data.SqlDbType.Int);
                AddSqlParameter(command, "@PriceDiscount", infoInsert.PriceDiscount, System.Data.SqlDbType.Money);
                AddSqlParameter(command, "@AutoID", infoInsert.AutoID, System.Data.SqlDbType.Int);

                WriteLogExecutingCommand(command);

                return command.ExecuteNonQuery() > 0;
            }
        }
        public bool DeleteProductDiscount(SqlConnection connection, string ProductID) //// 
        {
            string strSQL = @"
               DELETE [ProductDiscount] WHERE ProductID = @ProductID";
            using (var command = new SqlCommand(strSQL, connection))
            {
                AddSqlParameter(command, "@ProductID", ProductID, System.Data.SqlDbType.VarChar);
                WriteLogExecutingCommand(command);

                return command.ExecuteNonQuery() > 0;
            }
        }
        public bool UpdateProductDiscount(SqlConnection connection, ProductDiscountInfo infoUpdate)
        {
            string strSql = @"
               UPDATE [ProductDiscount]
               SET       [DateFrom] = @DateFrom
                        ,[DateTo] = @DateTo
                        ,[Discount] = @Discount
                        ,[PriceDiscount] = @PriceDiscount
                        ,[AutoID] = @AuttoID
                            
               WHERE [ProductID] = @ProductID";
            using (var command = new SqlCommand(strSql, connection))
            {
                AddSqlParameter(command, "@ProductID", infoUpdate.ProductID, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, "@DateFrom", infoUpdate.DateFrom, System.Data.SqlDbType.DateTime);
                AddSqlParameter(command, "@DateTo", infoUpdate.DateTo, System.Data.SqlDbType.DateTime);
                AddSqlParameter(command, "@Discount", infoUpdate.Discount, System.Data.SqlDbType.Int);
                AddSqlParameter(command, "@PriceDiscount", infoUpdate.PriceDiscount, System.Data.SqlDbType.Money);
                AddSqlParameter(command, "@AutoID", infoUpdate.AutoID, System.Data.SqlDbType.Int);
                WriteLogExecutingCommand(command);
                return command.ExecuteNonQuery() > 0;
            }

        }




    }
}
