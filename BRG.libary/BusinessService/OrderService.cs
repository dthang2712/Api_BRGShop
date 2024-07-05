using BRG.libary.BusinessService.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRG.libary.BusinessService
{
    public class OrderService : BaseService<OrderService>
    {
        public class OrderInfo
        {
            public int OrderID { get; set; }
            public int CustomerID { get; set; }
            public int CustomerAddressID { get; set; }
            public string Note { get; set; }
            public decimal TotalPrice { get; set; }
            public int Status { get; set; }
            public int UserID { get; set; }

            public void CopyValue(OrderInfo info)
            {
                this.OrderID = info.OrderID;
                this.CustomerID = info.CustomerID;
                this.CustomerAddressID = info.CustomerAddressID;
                this.Note = info.Note;
                this.TotalPrice = info.TotalPrice;
                this.Status = info.Status;
                this.UserID = info.UserID;

            }
        }
        public List<OrderInfo> GetListOrder(SqlConnection connection, string strSearch = null)
        {
            var result = new List<OrderInfo>();
            string strSQL = @"
            SELECT[OrderID]
                   ,[CustomerID]
                   ,[CustomerAddressID]
                   ,[Note]
                   ,[TotalPrice]
                   ,[Status]
                   ,[UserID]
            FROM [Order] WHERE 1=1 ";

            using (var command = new SqlCommand(strSQL, connection))
            {
                if (!string.IsNullOrEmpty(strSearch))
                {
                    command.CommandText += "and Order like @strSearch or OrderID like @strSearch";
                    AddSqlParameter(command, "@strSearch", "@" + strSearch + "%", System.Data.SqlDbType.NVarChar);
                }
                WriteLogExecutingCommand(command);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new OrderInfo();
                        item.OrderID = GetDbReaderValue<int>(reader["OrderID"]);
                        item.CustomerID = GetDbReaderValue<int>(reader["CustomerID"]);
                        item.CustomerAddressID = GetDbReaderValue<int>(reader["CustomerAddressID"]);
                        item.Note = GetDbReaderValue<string>(reader["Note"]);
                        item.TotalPrice = GetDbReaderValue<Decimal>(reader["TotalPrice"]);
                        item.Status = GetDbReaderValue<int>(reader["Status"]);
                        item.UserID = GetDbReaderValue<int>(reader["UserID"]);                       
                        result.Add(item);
                    }
                }
            }

            return result;
        }
        public bool InsertOrder(SqlConnection connection, OrderInfo infoInsert)
        {
            string strSQl = @"
            INSERT INTO  [Order]
                ([OrderID]
                ,[CustomerID]
                ,[CustomerAddressID]
                ,[Note]
                ,[TotalPrice]
                ,[Status]
                ,[UserID]
            VALUES
                (@OrderID
                ,@CustomerID
                ,@CustomerAddressID
                ,@Note
                ,@TotalPrice
                ,@Status
                ,@UserID)";


            using (var command = new SqlCommand(strSQl, connection))
            {
                AddSqlParameter(command, "@OrderID", infoInsert.OrderID, System.Data.SqlDbType.Int);
                AddSqlParameter(command, "@CustomerID", infoInsert.CustomerID, System.Data.SqlDbType.Int);
                AddSqlParameter(command, "@CustomerAddressID", infoInsert.CustomerAddressID, System.Data.SqlDbType.Int);
                AddSqlParameter(command, "@Note", infoInsert.Note, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, "@TotalPrice", infoInsert.TotalPrice, System.Data.SqlDbType.Money);
                AddSqlParameter(command, "@Status", infoInsert.Status, System.Data.SqlDbType.Int);
                AddSqlParameter(command, "@UserID", infoInsert.UserID, System.Data.SqlDbType.Int);

                WriteLogExecutingCommand(command);

                return command.ExecuteNonQuery() > 0;
            }
        }
        public bool DeleteOrder(SqlConnection connection, string OrderID)
        {
            string strSQL = @"
               DELETE [Order] WHERE OrderID = @OrderID";
            using (var command = new SqlCommand(strSQL, connection))
            {
                AddSqlParameter(command, "@OrderID", OrderID, System.Data.SqlDbType.VarChar);
                WriteLogExecutingCommand(command);

                return command.ExecuteNonQuery() > 0;
            }
        }
        public bool UpdateOrder(SqlConnection connection, OrderInfo infoUpdate)
        {
            string strSql = @"
               UPDATE [Order]
               SET [CustomerID] = @CustomerID
                        ,[CustomerAddressID] = @CustomerAddressID
                        ,[Note] = @Note
                        ,[TotalPrice] = @TotalPrice
                        ,[Status] = @Status
                        ,[UserID] =@UserID
                        
               WHERE [OrderID] = @OrderID";
            using (var command = new SqlCommand(strSql, connection))
            {
                AddSqlParameter(command, @"CustomerID", infoUpdate.CustomerID, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, @"Note", infoUpdate.Note, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, @"TotalPrice", infoUpdate.TotalPrice, System.Data.SqlDbType.Money);
                AddSqlParameter(command, @"Status", infoUpdate.Status, System.Data.SqlDbType.Int);
                AddSqlParameter(command, @"CustomerAddressID", infoUpdate.CustomerAddressID, System.Data.SqlDbType.Int);
                AddSqlParameter(command, @"UserID", infoUpdate.UserID, System.Data.SqlDbType.Int);
                WriteLogExecutingCommand(command);
                return command.ExecuteNonQuery() > 0;
            }

        }




    }
}
