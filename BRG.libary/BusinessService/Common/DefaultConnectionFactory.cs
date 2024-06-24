using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRG.libary.BusinessService.Common
{
    public static class DefaultConnectionFactory
    {
        public static SqlConnectionFactory BRGShop
        {
            get
            {
                return SqlConnectionFactory.GetInstance("BRGShop");
            }
        }
    }
}
