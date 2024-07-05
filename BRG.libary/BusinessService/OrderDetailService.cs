using BRG.libary.BusinessService.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRG.libary.BusinessService
{
    public class OrderDetailService : BaseService<OrderDetailService>
    {
        public class OrderDetailInfo
        {
            public int OrderID { get; set; }
            public int ProductID { get; set; }
            public int Amount { get; set; }
            public int OrderDetailID { get; set; }

            public void CopyValue(OrderDetailInfo info)
            {
                this.OrderID = info.OrderID;
                this.ProductID = info.ProductID;
                this.Amount = info.Amount;
                this.OrderDetailID = info.OrderDetailID;

            }
        }
        public List<OrderDetailInfo> GetListOrderDetail(SqlConnection connection, string strSearch = null)
        {
            var result = new List<OrderDetailInfo>();
            string strSQL = @"
            SELECT[OrderID]
                   ,[ProductID] 
                   ,[Amount]
                   ,[OrderDetailID]
            FROM [OrderDetail] WHERE 1=1 ";

            using (var command = new SqlCommand(strSQL, connection))
            {
                if (!string.IsNullOrEmpty(strSearch))
                {
                    command.CommandText += "and OrderDetail like @strSearch or ProductID like @strSearch";
                    AddSqlParameter(command, "@strSearch", "@" + strSearch + "%", System.Data.SqlDbType.NVarChar);
                }
                WriteLogExecutingCommand(command);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new OrderDetailInfo();
                        item.OrderDetailID = GetDbReaderValue<int>(reader["OrderDetailID"]);
                        item.ProductID = GetDbReaderValue<int>(reader["ProductID"]);
                        item.Amount = GetDbReaderValue<int>(reader["Amount"]);
                        item.OrderID = GetDbReaderValue<int>(reader["OrderID"]);
                        result.Add(item);
                    }
                }
            }

            return result;
        }
        public bool InsertOrderDetail(SqlConnection connection, OrderDetailInfo infoInsert)
        {
            string strSQl = @"
            INSERT INTO  [OrderDetail]
                ([OrderDetailID]
                ,[ProductID]
                ,[Amount]
                ,[OrderID]
            VALUES
                (@OrderDetailID
                ,@ProductID
                ,@OrderID
                ,@Amount)";


            using (var command = new SqlCommand(strSQl, connection))
            {
                AddSqlParameter(command, "@ProductID", infoInsert.ProductID, System.Data.SqlDbType.Int);
                AddSqlParameter(command, "@OderID", infoInsert.OrderID, System.Data.SqlDbType.Int);
                AddSqlParameter(command, "@Amount", infoInsert.Amount, System.Data.SqlDbType.Int);
                AddSqlParameter(command, "@OrderDetailID", infoInsert.OrderDetailID, System.Data.SqlDbType.Int);

                WriteLogExecutingCommand(command);

                return command.ExecuteNonQuery() > 0;
            }
        }
        public bool DeleteOrderDetail(SqlConnection connection, string ProductID)
        {
            string strSQL = @"
               DELETE [OrderDetail] WHERE ProductID = @ProductID";
            using (var command = new SqlCommand(strSQL, connection))
            {
                AddSqlParameter(command, "@ProductID", ProductID, System.Data.SqlDbType.VarChar);
                WriteLogExecutingCommand(command);

                return command.ExecuteNonQuery() > 0;
            }
        }
        public bool UpdateOrderDetail(SqlConnection connection, OrderDetailInfo infoUpdate)
        {
            string strSql = @"
               UPDATE [OrderDetail]
               SET [Amount] = @Amount
                        ,[OrderDetailID] = @OrderDetailID
                        ,[OrderID] = @OrderID
               WHERE [ProductID] = @ProductID";
            using (var command = new SqlCommand(strSql, connection))
            {
                AddSqlParameter(command, @"ProductID", infoUpdate.ProductID, System.Data.SqlDbType.Int);
                AddSqlParameter(command, @"Amount", infoUpdate.Amount, System.Data.SqlDbType.Int);
                AddSqlParameter(command, @"OrderID", infoUpdate.OrderID, System.Data.SqlDbType.Int);
                AddSqlParameter(command, @"AutoID", infoUpdate.OrderDetailID, System.Data.SqlDbType.Int);
                WriteLogExecutingCommand(command);
                return command.ExecuteNonQuery() > 0;
            }

        }
    }
}

