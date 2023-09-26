
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;
using VRC.Udon.Common.Interfaces;


namespace Yakuda.Assets.Toggelsystem
{
    public class New_ToggelSystem : UdonSharpBehaviour
    {
        #region Header_Params
        [Header("==========Yakudas Toggel System======= ", order = 0)]
        [Space(-10, order = 1)]
        [Header("Syncmode 0 : Loca", order = 2)]
        [Space(-10, order = 3)]
        [Header("Symcmode 1 : Sync with player in lobby", order = 4)]
        [Space(10, order = 5)]
        [Header("Symcmode 2 : sync with all", order = 6)]
        [Space(10, order = 7)]



        public int Syncmode;


        [Header("===Object Toggel===", order = 0)]
        [Space(-10, order = 1)]
        [Header("Default Toggel objects", order = 2)]
        public GameObject[] Toggelobjects;

        [Header("on by push toggel")]
        public GameObject[] ObjectsON;
        [Header("off by push toggel")]

        public GameObject[] ObjectsOFF;



        #endregion Header_Params
        #region Trigger

        #region Joined
        public override void OnPlayerJoined(VRCPlayerApi Player)
        {
            if (Networking.IsMaster)
            {
                if (Syncmode == 2)
                {
                    for (int o = 0; o < Toggelobjects.Length; o++)
                    {
                        if (Toggelobjects[o].activeSelf)
                        {
                            SendCustomNetworkEvent(NetworkEventTarget.All, "NetToggelOn");
                        }
                        else
                        {
                            SendCustomNetworkEvent(NetworkEventTarget.All, "NetToggelOff");
                        }
                    }
                    for (int m = 0; m < ObjectsON.Length; m++)
                    {
                        if (ObjectsOFF[m].activeSelf)
                        {
                            SendCustomNetworkEvent(NetworkEventTarget.All, "NetButtonOff");
                        }
                        else
                        {
                            SendCustomNetworkEvent(NetworkEventTarget.All, "NetButtonOn");
                        }
                    }
                }
            }
        }
        #endregion Joined

        public void Interact()
        {
            if (Syncmode == 0)//Local
            {
                for (int i = 0; i < Toggelobjects.Length; i++)
                {
                    Toggelobjects[i].SetActive(!Toggelobjects[i].activeSelf);
                }
                for (int i = 0; i < ObjectsON.Length; i++)
                {
                    ObjectsON[i].SetActive(true);
                }
                for (int n = 0; n < ObjectsOFF.Length; n++)
                {
                    ObjectsOFF[n].SetActive(false);
                }

            }
            else if (Syncmode == 1)
            {
                SendCustomNetworkEvent(NetworkEventTarget.All, "NetToggel");
            }
            else if (Syncmode == 2)
            {
                SendCustomNetworkEvent(NetworkEventTarget.All, "NetToggel");
            }
        }
        #endregion Trigger
        #region Networking
        public void NetToggel()
        {
            for (int p = 0; p < Toggelobjects.Length; p++)
            {
                Toggelobjects[p].SetActive(!Toggelobjects[p].activeSelf);
            }
            for (int m = 0; m < ObjectsON.Length; m++)
            {
                ObjectsON[m].SetActive(true);
            }
            for (int l = 0; l < ObjectsOFF.Length; l++)
            {
                ObjectsOFF[l].SetActive(false);
            }
        }
        public void NetToggelOn()
        {
            for (int i = 0; i < Toggelobjects.Length; i++)
            {
                Toggelobjects[i].SetActive(true);
            }
        }
        public void NetToggelOff()
        {
            for (int i = 0; i < Toggelobjects.Length; i++)
            {
                Toggelobjects[i].SetActive(false);
            }
        }
        public void NetButtonOff()
        {
            for (int i = 0; i < ObjectsON.Length; i++)
            {
                ObjectsON[i].SetActive(false);
            }
            for (int k = 0; k < ObjectsOFF.Length; k++)
            {
                ObjectsOFF[k].SetActive(true);
            }
        }

        public void NetButtonOn()
        {
            for (int i = 0; i < ObjectsON.Length; i++)
            {
                ObjectsON[i].SetActive(true);
            }
            for (int i = 0; i < ObjectsOFF.Length; i++)
            {
                ObjectsOFF[i].SetActive(false);
            }
        }
        #endregion Networking
        void Start()
        {

        }

    }
}
