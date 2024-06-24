using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRG.libary.BusinessObject
{
    public static class SystemConfigConnection
    {
        public delegate SqlConnection GetConnectionProto();
        public static GetConnectionProto GetConnection;
    }
}
