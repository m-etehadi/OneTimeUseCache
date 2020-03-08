using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{
    public class MyOTUO : OneTimeUseCache.OneTimeUseObject
    {
        public override string Key { get; set; }

        public string SomeOtherValues { get; set; }

        
    }
}
