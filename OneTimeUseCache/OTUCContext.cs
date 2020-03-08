using System;
using System.Collections.Generic;
using System.Linq;

namespace OneTimeUseCache
{
    public class OTUCContext<T> where T : OneTimeUseObject
    {
        /// <summary>
        /// Items who lived more than this time span will withdraw from cache
        /// </summary>
        public static TimeSpan LifeTime { get; set; } = new TimeSpan(0, 2, 0);

        List<T> _objects = new List<T>();

        static OTUCContext<T> _current;
        public static OTUCContext<T> Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new OTUCContext<T>();
                }
                return _current;
            }
        }

        static object _locker = new object();

        // methods
        OTUCContext()
        {

        }



        public void Add(T obj)
        {
            lock (_locker)
            {
                _objects.RemoveAll(p => p.Key == obj.Key);
                _objects.Add(obj);
            }
        }

        public T Get(string key)
        {
            lock (_locker)
            {
                var foundedObj = _objects.SingleOrDefault(p => p.Key == key);
                if (foundedObj == null)
                {
                    return null;
                }
                else if (foundedObj.Created.Add(LifeTime) < DateTime.Now)
                {
                    _objects.Remove(foundedObj);
                    return null;
                }
                else
                {
                    return foundedObj;
                }
            }

        }


    }
}
