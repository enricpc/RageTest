using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Utils
    {

        public static void createBlip()
        {
            NAPI.Blip.CreateBlip(1, new Vector3(0, 0, 0), 1, 46);
        }
        public static void createBlipOnPos(double x, double y, double z)
        {
            NAPI.Blip.CreateBlip(1, new Vector3(x, y, z), 1, 46);
        }
    }
}
