namespace FileSearchByIndex.Core.Services
{
    public class BaseService<T> where T : class
    {
        protected log4net.ILog _log = log4net.LogManager.GetLogger(typeof(T));
    }
}
