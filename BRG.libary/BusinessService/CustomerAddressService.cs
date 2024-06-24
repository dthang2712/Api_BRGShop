using BRG.libary.BusinessService.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRG.libary.BusinessService
{
    public class CustomerAddressService : BaseService<CustomerAddressService>
    {
        public class CustomerAddressInfo
        {
            public string CustomerID { get; set; }
            public string CustomerAddressID { get; set; }
            public string AddressName { get; set; }
            public string PhoneNumber { get; set; }
            public string City { get; set; }
            public string District { get; set; }
            public string Ward { get; set; }
            public string AddressDetail { get; set; }
            public string Default { get; set; }
            public int AutoID { get; set; }

            public void CopyValue(CustomerAddressInfo info)
            {
                this.CustomerID = info.CustomerID;
                this.CustomerAddressID = info.CustomerAddressID;
                this.AddressName = info.AddressName;                
                this.PhoneNumber = info.PhoneNumber;
                this.City = info.City;
                this.District = info.District;
                this.Ward = info.Ward;
                this.AddressDetail = info.AddressDetail;
                this.Default = info.Default;
                this.AutoID = info.AutoID;

            }
        }
        public List<CustomerAddressInfo> GetListCustomerAddress(SqlConnection connection, string strSearch = null)
        {
            var result = new List<CustomerAddressInfo>();
            string strSQL = @"
            SELECT[CustomerID]
                   ,[CustomerAddressID]
                   ,[CategoryName]
                   ,[AddressName]
                   ,[PhoneNumber]
                   ,[City]
                   ,[District]
                   ,[Ward]
                   ,[AddressDetail]
                   ,[Default]
                   ,[AutoID]
            FROM [CustomerAddress] WHERE 1=1 ";

            using (var command = new SqlCommand(strSQL, connection))
            {
                if (!string.IsNullOrEmpty(strSearch))
                {
                    command.CommandText += "and CustomerAddress like @strSearch or CustomerID like @strSearch";
                    AddSqlParameter(command, "@strSearch", "@" + strSearch + "%", System.Data.SqlDbType.NVarChar);
                }
                WriteLogExecutingCommand(command);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new CustomerAddressInfo();
                        item.AutoID = GetDbReaderValue<int>(reader["AutoID"]);
                        item.CustomerID = GetDbReaderValue<string>(reader["CustomerID"]);
                        item.AddressName = GetDbReaderValue<string>(reader["AddressName"]);
                        item.PhoneNumber = GetDbReaderValue<string>(reader["PhoneNumber"]);
                        item.CustomerAddressID = GetDbReaderValue<string>(reader["CustomerAddressID"]);
                        item.City = GetDbReaderValue<string>(reader["City"]);
                        item.District = GetDbReaderValue<string>(reader["District"]);
                        item.Ward = GetDbReaderValue<string>(reader["Ward"]);
                        item.AddressDetail = GetDbReaderValue<string>(reader["AddressDetail"]);
                        item.Default = GetDbReaderValue<string>(reader["Default"]);
                        
                        result.Add(item);
                    }
                }
            }

            return result;
        }
        public bool InsertCustomerAddress(SqlConnection connection, CustomerAddressInfo infoInsert)
        {
            string strSQl = @"
            INSERT INTO  [CustomerAddress]
                ([AutoID]
                   ,[CustomerID]
                   ,[CustomerAddressID]
                   ,[AddressName]
                   ,[PhoneNumber]
                   ,[City]
                   ,[District]
                   ,[Ward]
                   ,[AddressDetail]
                   ,[Default]
            VALUES
                (@AutoID
                ,@CustomerID
                ,@CustomerAddressID
                ,@AddressName
                ,@PhoneNumber
                ,@City
                ,@District
                ,@Ward
                ,@AddressDetail
                ,@Default)";


            using (var command = new SqlCommand(strSQl, connection))
            {
                AddSqlParameter(command, "@CustomerID", infoInsert.CustomerID, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, "@CustomerAddressID", infoInsert.CustomerAddressID, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, "@PhoneNumber", infoInsert.PhoneNumber, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, "@AddressName", infoInsert.AddressName, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@City", infoInsert.City, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@District", infoInsert.District, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@Ward", infoInsert.Ward, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@AddressDetail", infoInsert.AddressDetail, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@Default", infoInsert.Default, System.Data.SqlDbType.Bit);

                WriteLogExecutingCommand(command);

                return command.ExecuteNonQuery() > 0;
            }
        }
        public bool DeleteCustomerAddress(SqlConnection connection, string CustomerID)
        {
            string strSQL = @"
               DELETE [CustomerAddress] WHERE CustomerID = @CustomerID";
            using (var command = new SqlCommand(strSQL, connection))
            {
                AddSqlParameter(command, "@CustomerID", CustomerID, System.Data.SqlDbType.VarChar);
                WriteLogExecutingCommand(command);

                return command.ExecuteNonQuery() > 0;
            }
        }
        public bool UpdateCustomerAddress(SqlConnection connection, CustomerAddressInfo infoUpdate)
        {
            string strSql = @"
               UPDATE [CustomerAddress]
               SET [CategoryName] = @CategoryName
                        ,[AutoID] = @AuttoID
                        ,[AutoID] = @AuttoID
                        ,[AutoID] = @AuttoID
                        ,[AutoID] = @AuttoID
                        ,[AutoID] = @AuttoID
                        ,[AutoID] = @AuttoID
                        ,[AutoID] = @AuttoID
               WHERE [CustomerID] = @CustomerID";
            using (var command = new SqlCommand(strSql, connection))
            {
                AddSqlParameter(command, "@CustomerID", infoUpdate.CustomerID, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, "@CustomerAddressID", infoUpdate.CustomerAddressID, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, "@PhoneNumber", infoUpdate.PhoneNumber, System.Data.SqlDbType.VarChar);
                AddSqlParameter(command, "@AddressName", infoUpdate.AddressName, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@City", infoUpdate.City, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@District", infoUpdate.District, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@Ward", infoUpdate.Ward, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@AddressDetail", infoUpdate.AddressDetail, System.Data.SqlDbType.NVarChar);
                AddSqlParameter(command, "@Default", infoUpdate.Default, System.Data.SqlDbType.Bit);
                WriteLogExecutingCommand(command);
                return command.ExecuteNonQuery() > 0;
            }

        }




    }
}
