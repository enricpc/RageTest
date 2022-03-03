using System.Collections.Generic;
using GTANetworkAPI;
using test.DTO;

namespace test.Commands
{
    class PlayerCommands : Script
    {
        [Command("me", "~o~Uso: ~w~/me [accion]", GreedyArg = true)]
        public void meCommand(GTANetworkAPI.Player player, string action)
        {
            action = action.Trim();
            List<GTANetworkAPI.Player> nearPlayers = NAPI.Player.GetPlayersInRadiusOfPlayer(20, player);
            foreach (GTANetworkAPI.Player player1 in nearPlayers)
            {
                player1.SendChatMessage($"~p~{player.Name} {action}");
            }
        }

        [Command("do", "~o~Uso: ~w~/do [descripcion]", GreedyArg = true)]
        public void doCommand(GTANetworkAPI.Player player, string action)
        {
            action = action.Trim();
            List<GTANetworkAPI.Player> nearPlayers = NAPI.Player.GetPlayersInRadiusOfPlayer(20, player);
            foreach (GTANetworkAPI.Player player1 in nearPlayers)
            {
                player1.SendChatMessage($"~g~{player.Name} {action}");
            }
        }

        [Command("coords", "~o~Uso: ~w~/coords")]
        public void coordsCommand(GTANetworkAPI.Player player)
        {
                player.SendChatMessage($"{player.Position.X}, {player.Position.Y}, {player.Position.Z}");
        }

        [Command("tp", "~o~Uso: ~w~/tp [x] [y] [z]", GreedyArg = false)]
        public void tpCommand(GTANetworkAPI.Player player, float x, float y, float z)
        {
            player.SendChatMessage($"estas {x}, {y}, {z}");
            player.Position = new Vector3(x, y, z);

        }

        [Command("revive", "~o~Uso: ~w~/revive")]
        public void reviveCommand(GTANetworkAPI.Player player)
        {
            player.Health = 100;
            player.SendChatMessage("has estat curat");
        }

        [Command("kill", "~o~Uso: ~w~/kill")]
        public void killCommand(GTANetworkAPI.Player player)
        {
            player.Kill();
            player.SendChatMessage("tas muerto");
        }


        [Command("register", "~o~Uso: ~w~/register")]
        public void AccountCmdRegister(GTANetworkAPI.Player player, string username, string password)
        {

            RegisterAccount(player, username, password);
            NAPI.Chat.SendChatMessageToPlayer(player, "~g~Registration successful!");

        }

        [Command("anim"," hace una animacion")]
        public void AnimCmd(GTANetworkAPI.Player player)
        {
            player.PlayAnimation("mp_arresting", "idle", 49);
        }


        [Command("blip", " crear blip", GreedyArg = false)]
        public void BlipCmd(GTANetworkAPI.Player player, float x, float y, float z)
        {
            Utils.createBlipOnPos(x, y, z);
        }

        [Command("bliponpos", " crear blip")]
        public void BlipOnposCmd(GTANetworkAPI.Player player)
        {
            Utils.createBlipOnPos(player.Position.X, player.Position.Y, player.Position.Z);
        }

        [Command("createtp", " crear tp", GreedyArg = true)]
        public void createTpPointCmd(GTANetworkAPI.Player player, string name)
        {
            Utils.createBlipOnPosWhitName(name, player.Position.X, player.Position.Y, player.Position.Z);
            TpPoint tp = new TpPoint()
            {
                owner = player.Name,
                tpcoords = player.Position,
                tpname = name
            };
            MySQL.sqlFunctions.saveTpPoint( tp);
        }
        [Command("deletetp", " elimina tp", GreedyArg = true)]
        public void deleteTpPointCmd(GTANetworkAPI.Player player, string name)
        {
            Utils.createBlipOnPosWhitName(name, player.Position.X, player.Position.Y, player.Position.Z);
            TpPoint tp = new TpPoint()
            {
                owner = player.Name,
                tpcoords = player.Position,
                tpname = name
            };
            MySQL.sqlFunctions.deleteTpPoint(tp);
            Utils.deleteBlipByName(name);
        }
        [Command("ir","ir a  ",GreedyArg =true)]
        public void tpCmd(GTANetworkAPI.Player player, string name)
        {
            TpPoint tpPoint = MySQL.sqlFunctions.getTpPoint(player, name).Result;
            player.Position = tpPoint.tpcoords;
        }



        public static void RegisterAccount(GTANetworkAPI.Player client, string username, string password)
        {

            // create a new Account object
            var account = new Account
            {
                Username = username,
                Password = password
            };


        }


    }
}
