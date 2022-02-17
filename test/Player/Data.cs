using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.Player
{
    class Data
    {
        public const string DataIdentificer = "PlayerData_Identifier";

        public int AdminLevel;
        private int Cash;
        public bool FirsSpawned;

        public Data()
        {
            this.AdminLevel = 0;
            this.Cash = 0;
            this.FirsSpawned = true;
        }

        public static bool ReturnFirstSpawnValue(GTANetworkAPI.Player player)
        {
            return player.GetData<Data>(DataIdentificer).FirsSpawned;
        }

        public static void SetFirstSpawnValue(GTANetworkAPI.Player player, bool value)
        {
             player.GetData<Data>(DataIdentificer).FirsSpawned=value;
        }
        public static int ReturnCashValue(GTANetworkAPI.Player player)
        {
            return player.GetData<Data>(DataIdentificer).Cash;
        }

        public static void SetCashValue(GTANetworkAPI.Player player, int value)
        {
            player.GetData<Data>(DataIdentificer).Cash = value;
        }
        public static int ReturnAdminLevelValue(GTANetworkAPI.Player player)
        {
            return player.GetData<Data>(DataIdentificer).AdminLevel;
        }

        public static void ReturnAdminLevelValue(GTANetworkAPI.Player player, int value)
        {
            player.GetData<Data>(DataIdentificer).AdminLevel = value;
        }
    }
}
