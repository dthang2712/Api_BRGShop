using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRG.libary.BusinessObject
{
    public abstract class BaseSingleton<T> where T : BaseSingleton<T>, new()
    {
        /// <summary>
        /// 	Logger
        /// </summary>
        protected ILog _logger = null;

        private static T _instance = null;

        public BaseSingleton()
        {
            _logger = LogManager.GetLogger(this.GetType());
        }

        public static T GetInstance()
        {
            if (_instance == null)
            {
                _instance = new T();
            }

            return _instance;
        }
    }
}
