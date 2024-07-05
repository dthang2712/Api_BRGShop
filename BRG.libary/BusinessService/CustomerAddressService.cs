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
        {   public int CustomerAddressID { get; set; }
            public int CustomerID { get; set; }
            public string AddressName { get; set; }
            public string PhoneNumber { get; set; }
            public string City { get; set; }
            public string District { get; set; }
            public string Ward { get; set; }
            public string AddressDetail { get; set; }
            public bool Default { get; set; }

            public void CopyValue(CustomerAddressInfo info)


            {   this.CustomerAddressID = info.CustomerAddressID;
                this.CustomerID = info.CustomerID;  
                this.AddressName = info.AddressName;                
                this.PhoneNumber = info.PhoneNumber;
                this.City = info.City;
                this.District = info.District;
                this.Ward = info.Ward;
                this.AddressDetail = info.AddressDetail;
                this.Default = info.Default;

            }
        }
        public List<CustomerAddressInfo> GetListCustomerAddress(SqlConnection connection, string strSearch = null)
        {
            var result = new List<CustomerAddressInfo>();
            string strSQL = @"
            SELECT[CustomerID]
                   ,[CustomerAddressID]
                   ,[AddressName]
                   ,[PhoneNumber]
                   ,[City]
                   ,[District]
                   ,[Ward]
                   ,[AddressDetail]
                   ,[Default]
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
                        item.CustomerID = GetDbReaderValue<int>(reader["CustomerID"]);
                        item.AddressName = GetDbReaderValue<string>(reader["AddressName"]);
                        item.PhoneNumber = GetDbReaderValue<string>(reader["PhoneNumber"]);
                        item.CustomerAddressID = GetDbReaderValue<int>(reader["CustomerAddressID"]);
                        item.City = GetDbReaderValue<string>(reader["City"]);
                        item.District = GetDbReaderValue<string>(reader["District"]);
                        item.Ward = GetDbReaderValue<string>(reader["Ward"]);
                        item.AddressDetail = GetDbReaderValue<string>(reader["AddressDetail"]);
                        item.Default = GetDbReaderValue<bool>(reader["Default"]);
                        
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
                ([CustomerAddressID]
                   ,[CustomerID]
                   ,[AddressName]
                   ,[PhoneNumber]
                   ,[City]
                   ,[District]
                   ,[Ward]
                   ,[AddressDetail]
                   ,[Default]
            VALUES
                (@CustomerAddressID
                ,@CustomerID
                ,@AddressName
                ,@PhoneNumber
                ,@City
                ,@District
                ,@Ward
                ,@AddressDetail
                ,@Default)";


            using (var command = new SqlCommand(strSQl, connection))
            {
                AddSqlParameter(command, "@CustomerAddressID", infoInsert.CustomerAddressID, System.Data.SqlDbType.Int);
                AddSqlParameter(command, "@CustomerID", infoInsert.CustomerID, System.Data.SqlDbType.Int);
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
               DELETE [CustomerAddress] WHERE CustomerAddressID = @CustomerAddressID";
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
               SET [CustomerID] = @CustomerID
                        ,[PhoneNumber] = @PhoneNumber
                        ,[AddressName] = @AddressName
                        ,[City] = @City
                        ,[District] = @District
                        ,[Ward] = @Ward
                        ,[AddressDetail] = @AddressDetail
                        ,[Default] = @Default
               WHERE [CustomerAddressID] = @CustomerAddressID";
            using (var command = new SqlCommand(strSql, connection))
            {
                AddSqlParameter(command, "@CustomerID", infoUpdate.CustomerID, System.Data.SqlDbType.Int);
                AddSqlParameter(command, "@CustomerAddressID", infoUpdate.CustomerAddressID, System.Data.SqlDbType.Int);
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
