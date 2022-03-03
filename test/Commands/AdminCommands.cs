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

           NAPI.Vehicle.CreateVehicle(hash, player.Position.Around(5), player.Rotation.Z, rando.Next(160), rando.Next(160));

            player.SendChatMessage("vehiculo creado");
        }

        [Command("weapon", "Usage: [wepName", GreedyArg = true)]
        public void CMD_GiveWeapon(GTANetworkAPI.Player player, string weapon)
        {
            if (Player.Data.ReturnPlayerData(player).AdminLevel < Admin.Rank.Admin_Lead)
            {
                player.SendChatMessage("errro no tines permisos");
                return;
            }
            WeaponHash hash = NAPI.Util.WeaponNameToModel(weapon);
            if (hash == 0)
            {
                player.SendChatMessage("invalid weapon");
                return;
            }

            NAPI.Player.GivePlayerWeapon(player, hash, 100);

            player.SendChatMessage("arma creada");
            var a = Player.Data.ReturnWeapons(player);
            a.Add(hash);
            Player.Data.SetWeapons(player,a);
        }

        [Command("delweapon")]
        public void CMD_DelWeapon(GTANetworkAPI.Player player)
        {
            if (Player.Data.ReturnPlayerData(player).AdminLevel < Admin.Rank.Admin_Lead)
            {
                player.SendChatMessage("errro no tines permisos");
                return;
            }
            NAPI.Player.RemoveAllPlayerWeapons(player);


            player.SendChatMessage("armaseliminadas");
            Player.Data.SetWeapons(player, new List<WeaponHash>());
        }

        [Command("createhospital", "crea un punto de curacion", GreedyArg = true)]
        public void CMD_CreateHospital(GTANetworkAPI.Player player)
        {
            if (Player.Data.ReturnPlayerData(player).AdminLevel < Admin.Rank.Admin_Lead)
            {
                player.SendChatMessage("errro no tines permisos");
                return;
            }
            NAPI.Marker.CreateMarker(MarkerType.UpsideDownCone, player.Position.Add(new Vector3(0, 0, 1)),new Vector3(), new Vector3(), 1.0f, new Color(0, 255, 0, 255));
            NAPI.TextLabel.CreateTextLabel("Entra a curarte", player.Position, 1.0f, 1.0f, 0, new Color(0, 255, 0, 255), true);
            ColShape col = NAPI.ColShape.CreateCylinderColShape(player.Position, 1.0f, 1.0f);
            Utils.hospitals.Add(col);
            Utils.createBlipHospitalOnPos(player.Position);
        }


        [Command("createmetaldetector", "crea detector de metales", GreedyArg = true)]
        public void CMD_CreateMetalDetector(GTANetworkAPI.Player player)
        {
            if (Player.Data.ReturnPlayerData(player).AdminLevel < Admin.Rank.Admin_Lead)
            {
                player.SendChatMessage("errro no tines permisos");
                return;
            }
          var a=  NAPI.Util.GetHashKey("ch_prop_ch_metal_detector_01a");
            NAPI.Object.CreateObject(a,new Vector3( player.Position.X,player.Position.Y,player.Position.Z-1), new Vector3());
            ColShape col = NAPI.ColShape.CreateCylinderColShape(player.Position, 1.0f, 1.0f);
            Utils.metalDetectors.Add(col);
        }


        [Command("makeadmin", "Usage: [vehName", GreedyArg = false)]
        public void CMD_MakeAdimn (GTANetworkAPI.Player player)
        {
            Player.Data.ReturnPlayerData(player).AdminLevel = Admin.Rank.Admin_Owner;
        }
    }

}
