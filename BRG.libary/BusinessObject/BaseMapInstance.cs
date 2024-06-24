using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BRG.libary.BusinessObject
{
    public abstract class BaseMapInstance<T> where T : BaseMapInstance<T>, new()
    {
        /// <summary>
        /// 	Logger
        /// </summary>
        protected ILog _logger = null;

        private static readonly ILog _internalLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected static Dictionary<string, T> _mapInstance = new Dictionary<string, T>();

        protected static AutoResetEvent _lockExclusiveAccess = new AutoResetEvent(true); //initial unlock

        public BaseMapInstance()
        {
            _logger = LogManager.GetLogger(this.GetType());
        }

        public static T GetInstance(string idObject)
        {
            try
            {
                _lockExclusiveAccess.WaitOne();

                T instance = default(T);
                if (!_mapInstance.TryGetValue(idObject, out instance))
                {
                    instance = new T();
                    _mapInstance.Add(idObject, instance);
                }

                return instance;
            }
            catch (Exception ex)
            {
                _internalLogger.Error(ex.Message, ex);
                throw;
            }
            finally
            {
                _lockExclusiveAccess.Set();
            }
        }
    }
}
