using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace test.DTO
{
   public class TpPoint
    {
        public int tpid { get; set; }
        public string tpname { get; set; }
        public Vector3 tpcoords { get; set; }
        public string owner { get; set; }
    }
}
