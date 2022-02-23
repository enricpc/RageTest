using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTANetworkAPI;

namespace test.Commands
{
    class AdminCommands:Script
    {
        [Command("veh", "Usage: [vehName", GreedyArg = true)]
        public void CMD_CreateVehicles(GTANetworkAPI.Player player, string car)
        {
            if (Player.Data.ReturnPlayerData(player).AdminLevel < Admin.Rank.Admin_Lead)
            {
                player.SendChatMessage("errro no tines permisos");
                return;
            }
            VehicleHash hash = NAPI.Util.VehicleNameToModel(car);
            if (hash == 0)
            {
                player.SendChatMessage("invalid vehcile");
                return;
            }

            Random rando = new Random();

            Vehicle vehicle = NAPI.Vehicle.CreateVehicle(hash, player.Position.Around(5), player.Rotation.Z, rando.Next(160), rando.Next(160));

            player.SendChatMessage("vehiculo creado");
        }

        [Command("makeadmin", "Usage: [vehName", GreedyArg = false)]
        public void CMD_MakeAdimn (GTANetworkAPI.Player player)
        {
            Player.Data.ReturnPlayerData(player).AdminLevel = Admin.Rank.Admin_Owner;
        }
    }

}
