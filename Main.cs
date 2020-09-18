using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Fnrp.Fivem.Common.Client;

namespace PlayerControl.Client
{
    public class Main : ClientScript
    {
        [Tick]
        internal async Task OnTick()
        {
            int modelHash = Game.PlayerPed.Model.Hash;
            if (this._lastModelHash != modelHash)
            {
                this._lastModelHash = modelHash;
                this._lastHatComponent = -1;
                this._lastGlassesComponent = -1;
                this._lastMaskComponent = -1;
            }
            await BaseScript.Delay(1000);
        }

        [Command("hat")]
        internal void OnHatCommand()
        {
            Ped playerPed = Game.PlayerPed;
            int currentHat = API.GetPedPropIndex(playerPed.Handle, 0);
            int currentTexture = API.GetPedPropIndex(playerPed.Handle, 0);
            if (currentHat > -1)
            {
                this._lastHatComponent = currentHat;
                this._lastHatTexture = currentTexture;
                API.ClearPedProp(playerPed.Handle, 0);
                return;
            }
            if (this._lastHatComponent > -1)
            {
                API.SetPedPropIndex(playerPed.Handle, 0, this._lastHatComponent, this._lastHatTexture, true);
                this._lastHatComponent = -1;
                this._lastHatTexture = -1;
                return;
            }
            Screen.ShowNotification("~r~Player ped has not hat to toggle", true);
        }

		[Command("glasses")]
		internal void OnGlassesCommand()
		{
			Ped playerPed = Game.PlayerPed;
			int currentGlasses = API.GetPedPropIndex(playerPed.Handle, 1);
			int currentTexture = API.GetPedPropTextureIndex(playerPed.Handle, 1);
			if (currentGlasses > -1)
			{
				this._lastGlassesComponent = currentGlasses;
				this._lastGlassesTexture = currentTexture;
				API.ClearPedProp(playerPed.Handle, 1);
				return;
			}
			if (this._lastGlassesComponent > -1)
			{
				API.SetPedPropIndex(playerPed.Handle, 1, this._lastGlassesComponent, this._lastGlassesTexture, true);
				this._lastGlassesComponent = -1;
				this._lastGlassesTexture = -1;
				return;
			}
			Screen.ShowNotification("~r~Player ped has no glasses to toggle.", true);
		}

		[Command("mask")]
		internal void OnMaskCommand()
		{
			Ped playerPed = Game.PlayerPed;
			int currentMask = API.GetPedDrawableVariation(playerPed.Handle, 1);
			int currentTexture = API.GetPedTextureVariation(playerPed.Handle, 1);
			if (currentMask > 0)
			{
				this._lastMaskComponent = currentMask;
				this._lastMaskTexture = currentTexture;
				API.SetPedComponentVariation(playerPed.Handle, 1, 0, 0, 0);
				return;
			}
			if (this._lastMaskComponent > -1)
			{
				API.SetPedComponentVariation(playerPed.Handle, 1, this._lastMaskComponent, this._lastMaskTexture, 0);
				this._lastMaskComponent = -1;
				return;
			}
			Screen.ShowNotification("~r~Player ped has no mask to toggle.", true);
		}

		private int _lastModelHash;

		private int _lastHatComponent;

		private int _lastHatTexture;

		private int _lastGlassesComponent;

		private int _lastGlassesTexture;

		private int _lastMaskComponent;

		private int _lastMaskTexture;
	}
}