using System;
using System.Collections.Generic;
using System.Linq;
using GTANetworkAPI;
using MySql.Data.MySqlClient;
using test.DTO;

namespace test
{
    public class Main:Script
    {
       public static List<TpPoint> tpPoints;

        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {

         consolelog("Gm activo");
            MySQL.MySQL.InitConnection();
            Utils.blipList = new List<Blip>();
            Utils.hospitals = new List<ColShape>();
            Utils.metalDetectors = new List<ColShape>();
        }


        [ServerEvent(Event.PlayerConnected)]
        public void onPlayerConnect(GTANetworkAPI.Player player)
        {
            player.Name = "Enric";
            NAPI.Chat.SendChatMessageToAll($"{player.Name} se ha conectado al servidor");
             tpPoints =  MySQL.sqlFunctions.getTpPoints(player).Result;
            foreach(TpPoint tp in tpPoints)
            {
                Utils.createBlipOnPosWhitName(tp.tpname, tp.tpcoords);
            }

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

        [ServerEvent(Event.PlayerEnterColshape)]
        public void OnEnterColShape(ColShape shape, GTANetworkAPI.Player player)
        {
            foreach(ColShape colShape in Utils.hospitals)
            {
                if (shape.Equals(colShape))
                {
                    player.Health = 100;
                    player.SendChatMessage("has estat curat");
                }
            }
            foreach(ColShape colShape in Utils.metalDetectors)
            {
                if (shape.Equals(colShape))
                {
                    var weapons = Player.Data.ReturnWeapons(player);

                    if (weapons.Any())
                    {
                        customDo(player, "El detector sonaria");
                    }
                    else
                    {
                        customDo(player, "El detector NO sonaria");
                    }
                }
            }
        }

        public static void customDo(GTANetworkAPI.Player player,string message)
        { 
            List<GTANetworkAPI.Player> nearPlayers = NAPI.Player.GetPlayersInRadiusOfPlayer(20, player);
            foreach (GTANetworkAPI.Player player1 in nearPlayers)
            {
                player1.SendChatMessage("~g~"+message);
            }
        }
        

        public static void consolelog(string message)
        {
            NAPI.Util.ConsoleOutput("[LOG]"+message);
        }
    }
}
