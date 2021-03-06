using GTANetworkAPI;
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

        public Admin.Rank AdminLevel;
        public int Cash;
        public bool FirsSpawned;
        public int Level;
        public string Playername;
        public int Age;
        public List<WeaponHash> weapons;

        public Data()
        {
            this.AdminLevel = Admin.Rank.Admin_None;
            this.Cash = 0;
            this.FirsSpawned = true;
            this.Level = 0;
            this.Playername = "";
            this.Age = 1;
            this.weapons = new List<WeaponHash>();
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
        public static Admin.Rank ReturnAdminLevelValue(GTANetworkAPI.Player player)
        {
            return player.GetData<Data>(DataIdentificer).AdminLevel;
        }

        public static void setAdminLevelValue(GTANetworkAPI.Player player, Admin.Rank value)
        {
            player.GetData<Data>(DataIdentificer).AdminLevel = value;
        }

        public static int ReturnLevelValue(GTANetworkAPI.Player player)
        {
            return player.GetData<Data>(DataIdentificer).Level;
        }

        public static void SetLevelValue(GTANetworkAPI.Player player, int value)
        {
            player.GetData<Data>(DataIdentificer).Level = value;
        }

        public static Data ReturnPlayerData(GTANetworkAPI.Player player)
        {
            if (!player.HasData(DataIdentificer))
                player.SetData(DataIdentificer, new Data());
            return player.GetData<Data>(DataIdentificer);
        }
        public static List<WeaponHash> ReturnWeapons(GTANetworkAPI.Player player)
        {
            return player.GetData<Data>(DataIdentificer).weapons;
        }

        public static void SetWeapons(GTANetworkAPI.Player player, List<WeaponHash> value)
        {
            player.GetData<Data>(DataIdentificer).weapons = value;
        }
    }
}
