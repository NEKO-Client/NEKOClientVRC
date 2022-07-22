﻿using SerpentCore.Core;
using SerpentCore.Core.VRChat;
using Serpent.Loader;
using System.Net;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Serpent.Components
{
    public class MODStaff : ModComponent
    {

        public override void OnPlayerJoined(VRC.Player _Player)
        {
            string url = "https://apiv2.chisdealhd.co.uk/v2/games/api/vrchat/records/VitalityPlates/" + _Player.field_Private_APIUser_0.id;
            var _Req = (HttpWebRequest)WebRequest.Create(url);
            using (var res = (HttpWebResponse)_Req.GetResponse())
            using (var stream = res.GetResponseStream())
            using (var Reader = new StreamReader(stream))
            {
                var ReaderValue = Reader.ReadToEnd();

                Serpent._Queue.Enqueue(new Action(() =>
                {
                    if (_Player == null)
                        return;
                    if (ReaderValue == "{\"modstaff\":[]}")
                        return;

                    var _UserPlate = Newtonsoft.Json.JsonConvert.DeserializeObject<RootModstaff>(ReaderValue);

                    for (int i = 0; i < _UserPlate.modstaff.Count; i++)
                    {
                        if (_UserPlate.modstaff[i].UserId.Equals(_Player.field_Private_APIUser_0.id))
                        {
                            VRCUiManagerEx.Instance.QueueHudMessage($"[STAFF]\nStaff of CHIS MOD MENU ({_Player.field_Private_APIUser_0.displayName}) has Joined", Color.red);
                            ReLogger.Msg("[STAFF] " + _Player.field_Private_APIUser_0.displayName + " HAS JOINED!");
                        }
                    }


                }));
            }
        }

    }


    public class modstaff
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Text { get; set; }
    }

    public class RootModstaff
    {
        public List<modstaff> modstaff { get; set; }
    }
}
