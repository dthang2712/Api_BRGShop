using BRG.libary.BusinessService.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRG.libary.BusinessService
{
    public class CartService : BaseService<CartService>
    {
        public class CartInfo
        {
            public int AutoID { get; set; }
            public string CustomerID { get; set; }
            public string ProductID { get; set; }
            public int Amount { get; set; }

            public void CopyValue(CartInfo info)
            {
                this.CustomerID = info.CustomerID;
                this.ProductID = info.ProductID;
                this.AutoID = info.AutoID;
                this.Amount = info.Amount;
            }
        }
        public List<CartInfo> GetListCart(SqlConnection connection, string strSearch = null)
        {
            var result = new List<CartInfo>();
            string strSQL = @"
            SELECT [CustomerID]
                    ,[ProductID]
                    ,[AutoID]
                    ,[Amount]
 
            FROM [Cart] WHERE 1=1 ";

            using (var command = new SqlCommand(strSQL, connection))
            {
                if (!String.IsNullOrEmpty(strSearch))
                {
                    command.CommandText += " and CustomerID like @strSearch or ProductID like @strSearch";
                    AddSqlParameter(command, "@strSearch", "%" + strSearch + "%", System.Data.SqlDbType.NVarChar);
                }

                WriteLogExecutingCommand(command);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new CartInfo();
                        item.ProductID = GetDbReaderValue<string>(reader["ProductID"]);
                        item.CustomerID = GetDbReaderValue<string>(reader["CustomerID"]);
                        item.AutoID = GetDbReaderValue<int>(reader["AutoID"]);
                        item.Amount = GetDbReaderValue<int>(reader["Amount"]);
                  
                        result.Add(item);
                    }
                }
            }

            return result;
        }

        public bool InsertCart(SqlConnection connection, CartInfo infoInsert)
        {
            string strSQL = @"
            INSERT INTO [Cart]
                ([CustomerID]
                ,[ProductID]
                ,[AutoID]
                ,[Amount]
                
            VALUES
                (@CustomerID
                ,@ProductID
                ,@AutoID
                ,@Amount";

            using (var command = new SqlCommand(strSQL, connection))
            {
                AddSqlParameter(command, "@CustomerID", infoInsert.CustomerID, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, "@ProductID", infoInsert.ProductID, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@AutoID", infoInsert.AutoID, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@Amount", infoInsert.Amount, System.Data.SqlDbType.VarChar);
                

                WriteLogExecutingCommand(command);

                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteCart(SqlConnection connection, string AutoID)
        {
            string strSQL = @"
            DELETE [AutoID] WHERE AutoID = @AutoID";

            using (var command = new SqlCommand(strSQL, connection))
            {
                AddSqlParameter(command, "@AutoID", AutoID, System.Data.SqlDbType.Int);
                WriteLogExecutingCommand(command);

                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateCart(SqlConnection connection, CartInfo infoUpdate)
        {
            string strSQL = @"
            UPDATE [Cart]
            SET [CustomerID] = @Customer
                  ,[ProductID] = @ProductID
                  ,[Amount] = @Amount
                  
            WHERE [AutoID] = @AutoID";

            using (var command = new SqlCommand(strSQL, connection))
            {
                AddSqlParameter(command, "@AutoID", infoUpdate.AutoID, System.Data.SqlDbType.Int);
                AddSqlParameter(command, "@CustomerID", infoUpdate.CustomerID, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, "@ProductID", infoUpdate.ProductID, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, "@Amount", infoUpdate.Amount, System.Data.SqlDbType.Int);

                WriteLogExecutingCommand(command);
                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}
