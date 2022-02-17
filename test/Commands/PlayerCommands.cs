using System.Collections.Generic;
using GTANetworkAPI;

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

        [Command("coords", "~o~Uso: ~w~/coords")]
        public void coordsCommand(GTANetworkAPI.Player player)
        {
                player.SendChatMessage($"{player.Position.X}, {player.Position.Y}, {player.Position.Z}");
        }

        [Command("tp", "~o~Uso: ~w~/tp [x] [y] [z]", GreedyArg = false)]
        public void tpCommand(GTANetworkAPI.Player player, float x, float y, float z)
        {
            player.SendChatMessage($"hola {x}, {y}, {z}");
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


        [Command("blip", " crear blip")]
        public void BlipCmd(GTANetworkAPI.Player player, float x, float y, float z)
        {
            Utils.createBlipOnPos(x, y, z);
        }

        [Command("bliponpos", " crear blip")]
        public void BlipOnposCmd(GTANetworkAPI.Player player)
        {
            Utils.createBlipOnPos(player.Position.X, player.Position.Y, player.Position.Z);
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
