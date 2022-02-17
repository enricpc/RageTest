using System;
using System.Linq;
using GTANetworkAPI;


namespace test
{
    public class Main:Script
    {
        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {

         consolelog("Gm activo");

        }


        [ServerEvent(Event.PlayerConnected)]
        public void onPlayerConnect(GTANetworkAPI.Player player)
        {
            player.Name = "Enric";
            NAPI.Chat.SendChatMessageToAll($"{player.Name} se ha conectado al servidor");

            if (player.HasData(Player.Data.DataIdentificer))
                player.ResetData(Player.Data.DataIdentificer);
            player.SetData<Player.Data>(Player.Data.DataIdentificer,new Player.Data());
        }


        [ServerEvent(Event.PlayerSpawn)]
        public void onPlayerSpawn(GTANetworkAPI.Player player)
        {
            if (Player.Data.ReturnFirstSpawnValue(player))
            {
                NAPI.Chat.SendChatMessageToAll($"{player.Name} ha espawneado");
                Player.Data.SetFirstSpawnValue(player, false);
            }
            else
            {
                NAPI.Chat.SendChatMessageToAll($"{player.Name} ha respawneado");
            }
        }

        public static void consolelog(string message)
        {
            NAPI.Util.ConsoleOutput(message);
        }
    }
}
