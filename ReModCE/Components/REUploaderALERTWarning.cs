using NEKOClientCore.Core;
using NEKOClientCore.Core.VRChat;
using NEKOClient.Loader;
using System.Net;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine;
using VRC.Core;

namespace NEKOClient.Components
{
    public class REUploaderALERTWarning : ModComponent
    {

        public override void OnPlayerJoined(VRC.Player _Player)
        {
            string url = "https://apiv2.chisdealhd.co.uk/v2/games/api/vrchatclient/nekoclient/risky/records/VitalityPlates/" + _Player.field_Private_APIUser_0.id;
            var _Req = (HttpWebRequest)WebRequest.Create(url);
            using (var res = (HttpWebResponse)_Req.GetResponse())
            using (var stream = res.GetResponseStream())
            using (var Reader = new StreamReader(stream))
            {
                var ReaderValue = Reader.ReadToEnd();

                NEKOClient._Queue.Enqueue(new Action(() =>
                {
                    if (_Player == null)
                        return;
                    if (ReaderValue == "{\"tags\":[]}")
                        return;

                    var _UserPlate = Newtonsoft.Json.JsonConvert.DeserializeObject<RootReuploader>(ReaderValue);

                    for (int i = 0; i < _UserPlate.tags.Count; i++)
                    {

                        if (_UserPlate.tags[i].Text.Equals("<size=32><b><color=red>Known Reuploader</color></b>"))
                        {
                            var popupManager = VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0;
                            
                            popupManager.ShowStandardPopupV2("[REUPLOADER WARNING]", $"WARNING: ({_Player.field_Private_APIUser_0.displayName}) has Joined, This User can RIP Avatars Including Steal assets of your Private Avatars\nTake Extra NOTE!", "Okay",
                            () =>
                            {
                                popupManager.HideCurrentPopup();
                            });

                            //VRCUiManagerEx.Instance.QueueHudMessage($"[REUPLOADER WARNING]\nWARNING: ({_Player.field_Private_APIUser_0.displayName}) has Joined, This User can RIP Avatars Including Steal assets of your Private Avatars\nTake Extra NOTE!", Color.red);
                            ReLogger.Msg("[REUPLOADER WARNING] " + _Player.field_Private_APIUser_0.displayName + " HAS JOINED!\n\nBE CAREFUL FROM THIS USER.");
                        }
                    }


                }));
            }
        }

        public class tags
        {
            public int Id { get; set; }
            public string UserId { get; set; }
            public string Text { get; set; }
        }

        public class RootReuploader
        {
            public List<tags> tags { get; set; }
        }

    }
}
