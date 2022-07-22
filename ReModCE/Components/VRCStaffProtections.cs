using SerpentCore.Core;
using SerpentCore.Core.VRChat;
using Serpent.Loader;

namespace Serpent.Components
{
    public class VRCStaffProtections : ModComponent
    {

        public override void OnPlayerJoined(VRC.Player _Player)
        {
            if (_Player.field_Private_APIUser_0.hasModerationPowers || _Player.field_Private_APIUser_0.hasSuperPowers || _Player.field_Private_APIUser_0.hasScriptingAccess)
            {

                var popupManager = VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0;
                popupManager.ShowStandardPopupV2("WARNING!", $"VRC Moderator has just joined!\n\nUsername: ({_Player.field_Private_APIUser_0.displayName})\n\nLeave if you want or be brave and stay.", "Okay",
                () =>
                {
                    popupManager.HideCurrentPopup();
                });

                //VRCUiManagerEx.Instance.QueueHudMessage($"[VRCHAT MODERATOR TEAM]\nWATCH OUT, ({_Player.field_Private_APIUser_0.displayName}) has Joined, Leave or Stay. I have Disabled your Cheats for keep you Safe (IF ENABLED)", Color.red);
                ReLogger.Msg("[VRCHAT MODERATOR TEAM] WATCH OUT, (" + _Player.field_Private_APIUser_0.displayName + ") HAS JOINED!");
            }
        }
    }
}