using System;
using System.Collections.Generic;
using System.Text;

namespace OneTimeUseCache
{
    public abstract class OneTimeUseObject
    {
        public abstract string Key { get; set; }
        public DateTime Created { get; private set; }

        public OneTimeUseObject()
        {
            this.Created = DateTime.Now;
        }
    }
}
