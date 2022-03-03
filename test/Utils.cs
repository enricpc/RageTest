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
        public static List<Blip> blipList;
        public static List<ColShape> hospitals;
        public static List<ColShape> metalDetectors;
        public static void createBlip()
        {
            Blip blip = NAPI.Blip.CreateBlip(1, new Vector3(0, 0, 0), 1, 46);
            blipList.Add(blip);
        }
        public static void createBlipOnPos(double x, double y, double z)
        {
            Blip blip = NAPI.Blip.CreateBlip(1, new Vector3(x, y, z), 1, 46);
            blipList.Add(blip);
        }
        public static Blip createBlipOnPosWhitName(string name,double x, double y, double z)
        {
           Blip blip= NAPI.Blip.CreateBlip(1, new Vector3(x, y, z), 1, 46);
            blip.Name = name;
            blipList.Add(blip);
            return blip;
        }
        public static Blip  createBlipOnPosWhitName(string name, Vector3 pos)
        {
            Blip blip = NAPI.Blip.CreateBlip(1, pos, 1, 46);
            blip.Name = name;
            blipList.Add(blip);
            return blip;
        }
        public static void deleteBlipByName (string name)
        {
            
            var blip = NAPI.Pools.GetAllBlips().Where(nblip => nblip.Name == name);
        foreach(var a in blip)
            {
                a.Dimension = 3;
            }
           

        }
        public static Blip createBlipHospitalOnPos(Vector3 pos)
        {
            Blip blip = NAPI.Blip.CreateBlip(80, pos, 1, 2, "Hospital");
            blipList.Add(blip);
            return blip;
        }
    }
}
