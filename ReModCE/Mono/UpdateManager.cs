using NEKOClient.Loader;
using System;
using UnityEngine;

namespace NEKOClient.Mono
{
    internal class UpdateManager : MonoBehaviour
    {
        public UpdateManager(IntPtr ptr) : base(ptr)
        {

        }

        void Start()
        {
            ReLogger.Msg("Starting Update manager");
        }

        void LateUpdate()
        {
            try { if (VRC.Player.prop_Player_0.gameObject == null) return; } catch { return; }

            try
            {
                if (NEKOClient._Queue.Count != 0)
                {
                    for (int i = 0; i < NEKOClient._Queue.Count; i++)
                    {
                        NEKOClient._Queue.ToArray()[i].Invoke();
                        NEKOClient._Queue.Dequeue();
                    }
                }
            }
            catch { }
        }
    }
}
