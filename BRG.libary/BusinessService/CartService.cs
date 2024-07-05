using BRG.libary.BusinessService.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace BRG.libary.BusinessService
{
    public class CartService : BaseService<CartService>
    {
        public class CartInfo
        {
            public int CartID { get; set; }
            public int CustomerID { get; set; }
            public int ProductID { get; set; }

            public string ProductName { get; set; }
            public Decimal Price { get; set; }

            public int Amount { get; set; }

            public void CopyValue(CartInfo info)
            {
                this.CustomerID = info.CustomerID;
                this.ProductID = info.ProductID;
                this.CartID = info.CartID;
                this.Amount = info.Amount;
            }
        }
        public List<CartInfo> GetListCart(SqlConnection connection, string strSearch = null)
        {
            var result = new List<CartInfo>();
            string strSQL = @"
            SELECT [CustomerID]
                    ,[ProductID]
                    ,[CartID]
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
                        item.ProductID = GetDbReaderValue<int>(reader["ProductID"]);
                        item.CustomerID = GetDbReaderValue<int>(reader["CustomerID"]);
                        item.CartID = GetDbReaderValue<int>(reader["CartID"]);
                        item.Amount = GetDbReaderValue<int>(reader["Amount"]);

                        result.Add(item);
                    }
                }
            }

            return result;
        }

        public List<CartInfo> GetCartCustomer(SqlConnection connection, int strSearch)
        {

            var result = new List<CartInfo>();
            string strSQL = @"
            SELECT C.CartID,
                C.[CustomerID]
                ,C.[ProductID]
                ,P.ProductName
                ,P.Price
                ,C.[Amount]
 
                FROM [Cart] AS C 
                INNER JOIN Product AS P ON C.ProductID = P.ProductID

                WHERE C.CustomerID = @CustomerID ";
            using (var command = new SqlCommand(strSQL, connection))
            {
                    
                    AddSqlParameter(command, "@CustomerID", strSearch, System.Data.SqlDbType.Int);

                WriteLogExecutingCommand(command);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new CartInfo();
                        item.ProductID = GetDbReaderValue<int>(reader["ProductID"]);
                        item.CustomerID = GetDbReaderValue<int>(reader["CustomerID"]);
                        item.CartID = GetDbReaderValue<int>(reader["CartID"]);
                        item.Amount = GetDbReaderValue<int>(reader["Amount"]);
                        item.ProductName = GetDbReaderValue<string>(reader["ProductName"]);
                        item.Price = GetDbReaderValue<Decimal>(reader["Price"]);

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
                ,[Amount])
                
            VALUES
                (@CustomerID
                ,@ProductID
                ,@Amount)";

            using (var command = new SqlCommand(strSQL, connection))
            {
                AddSqlParameter(command, "@CustomerID", infoInsert.CustomerID, System.Data.SqlDbType.Int);
                AddSqlParameter(command, "@ProductID", infoInsert.ProductID, System.Data.SqlDbType.Int);
                AddSqlParameter(command, "@Amount", infoInsert.Amount, System.Data.SqlDbType.Int);
                

                WriteLogExecutingCommand(command);

                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteCart(SqlConnection connection, string CartID)
        {
            string strSQL = @"
            DELETE [CartID] WHERE CartID = @CartID";

            using (var command = new SqlCommand(strSQL, connection))
            {
                AddSqlParameter(command, "@CartID", CartID, System.Data.SqlDbType.Int);
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
                  
            WHERE [CartID] = @CartID";

            using (var command = new SqlCommand(strSQL, connection))
            {
                AddSqlParameter(command, "@CartID", infoUpdate.CartID, System.Data.SqlDbType.Int);
                AddSqlParameter(command, "@CustomerID", infoUpdate.CustomerID, System.Data.SqlDbType.Int);
                AddSqlParameter(command, "@ProductID", infoUpdate.ProductID, System.Data.SqlDbType.Int);
                AddSqlParameter(command, "@Amount", infoUpdate.Amount, System.Data.SqlDbType.Int);

                WriteLogExecutingCommand(command);
                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}
