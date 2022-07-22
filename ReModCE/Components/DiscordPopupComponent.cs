using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using NEKOClient.Core;
using NEKOClientCore.Core;
using NEKOClientCore.Core.VRChat;
using UnityEngine;
using VRC.Core;

namespace NEKOClient.Components
{
    internal sealed class DiscordPopupComponent : ModComponent
    {
        private bool _fired;
        private ConfigValue<bool> HadDiscordPopup;

        public DiscordPopupComponent()
        {
            HadDiscordPopup = new ConfigValue<bool>(nameof(HadDiscordPopup), false);
        }

        public override void OnAvatarIsReady(VRCPlayer vrcPlayer)
        {
            if (vrcPlayer != VRCPlayer.field_Internal_Static_VRCPlayer_0)
                return;

            if (_fired || HadDiscordPopup)
                return;

            MelonCoroutines.Start(ShowDiscordPopup());
        }

        private IEnumerator ShowDiscordPopup()
        {
            yield return new WaitForSeconds(3f);

            while (APIUser.CurrentUser == null)
                yield return null;

            _fired = true;

            var popupManager = VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0;
            popupManager.ShowStandardPopupV2("NEKOClient Discord", $"Hi there {APIUser.CurrentUser.displayName},\nWelcome to NEKO Client where Its Modified REMOD Client with Loads Custom Intergrations.\nMore Welcome Join our Discord Report bugs and Issues.", "Join!",
                () =>
                {
                    Process.Start("https://discord.gg/aBJX59jEaH");
                    popupManager.HideCurrentPopup();
                });
            HadDiscordPopup.SetValue(true);
        }
    }
}
