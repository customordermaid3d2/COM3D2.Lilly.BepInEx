using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    class MaidUtill
    {
        public static string GetMaidFullNale(Maid maid)
        {
            return maid.status.firstName + " , " + maid.status.lastName;
        }
    }
}
