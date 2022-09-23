
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;
using VRC.Udon.Common.Interfaces;
namespace Yakuda_Assets.Toggelsystem
{
    public class Toggel_System : UdonSharpBehaviour
    {
        #region Header_Params

        [Header("==========Button======")]
        [SerializeField, Tooltip("Event: Button")]
        public GameObject[] ObjectsON;
        [SerializeField, Tooltip("Event: Button")]
        public GameObject[] ObjectsOFF;

        
        [Header("==========Toggel======")]
        [SerializeField, Tooltip("Events: Toggel or Interact")]
        public GameObject[] Toggelobjects;
        
        [Header("==========Settings======")]


        [SerializeField, Tooltip("interactfunc: use Interact funktion\n 0: Toggel 1: Button 2: both")]
        public int interactfunc;
        public bool Global;
        /*
        Sync 0: no sync
        Sync 1: sync when objects are on
        Sync 2: sync when objects are off
        Sync 3: sync when objects are on if they are not ion it is turned off
        Sync 4: sync when objects are on if they are not off it is turned off
        */
        [SerializeField, Tooltip("Sync 0: no sync\nSync 1: sync when objects are on\nSync 2: sync when objects are off \nSync 3: sync when objects are on if they are not ion it is turned off \nSync 4: sync when objects a+re on if they are not off it is turned off")]
        public int Syncmode;
        #endregion Header_Params
        #region Joined
        public override void OnPlayerJoined(VRCPlayerApi Player)
        {
            if (Networking.IsMaster)
            {
                for (int i = 0; i < ObjectsON.Length; i++)
                {
                    if (Syncmode == 1)
                    {
                        if (ObjectsON[i].activeSelf)
                        {
                            SendCustomNetworkEvent(NetworkEventTarget.All, "NetButtonOn");
                        }
                        else
                        {

                        }
                    }
                    else if (Syncmode == 3)
                    {
                        if (ObjectsON[i].activeSelf)
                        {
                            SendCustomNetworkEvent(NetworkEventTarget.All, "NetButtonOn");
                        }
                        else
                        {
                            SendCustomNetworkEvent(NetworkEventTarget.All, "NetButtonOff");
                        }
                    }


                }
                for (int k = 0; k < ObjectsOFF.Length; k++)
                {
                    if (Syncmode == 2)
                    {
                        if (ObjectsOFF[k].activeSelf)
                        {

                        }
                        else
                        {
                            SendCustomNetworkEvent(NetworkEventTarget.All, "NetButtonOn");
                        }
                    }
                    else if (Syncmode == 4)
                    {
                        if (ObjectsOFF[k].activeSelf)
                        {
                            SendCustomNetworkEvent(NetworkEventTarget.All, "NetButtonOff");
                        }
                        else
                        {
                            SendCustomNetworkEvent(NetworkEventTarget.All, "NetButtonOn");
                        }
                    }

                }
                for (int m = 0; m < Toggelobjects.Length; m++)
                {
                    if (Syncmode == 1)
                    {
                        if (Toggelobjects[m].activeSelf)
                        {
                            SendCustomNetworkEvent(NetworkEventTarget.All, "NetToggelOn");
                        }
                        else
                        {
                            SendCustomNetworkEvent(NetworkEventTarget.All, "NetToggelOff");
                        }
                    }

                }

            }
        }
        #endregion Joined
        #region ButtonsToggel
        public void Button()
        {
            if (Global == true)
            {
                SendCustomNetworkEvent(NetworkEventTarget.All, "NetButtonOn");
            }
            else if (Global == false)
            {
                for (int i = 0; i < ObjectsON.Length; i++)
                {
                    ObjectsON[i].SetActive(true);
                }
                for (int n = 0; n < ObjectsOFF.Length; n++)
                {
                    ObjectsOFF[n].SetActive(false);
                }
            }

        }
        public void Toggel()
        {
            if (Global == true)
            {
                SendCustomNetworkEvent(NetworkEventTarget.All, "NetToggel");
            }
            else if (Global == false)
            {
                for (int i = 0; i < Toggelobjects.Length; i++)
                {
                    Toggelobjects[i].SetActive(!Toggelobjects[i].activeSelf);
                }
            }
        }
        public void Interact()
        {
            if(interactfunc == 0)
            {
                if (Global == true)
                {
                    SendCustomNetworkEvent(NetworkEventTarget.All, "NetToggel");
                }
                else if (Global == false)
                {
                    for (int i = 0; i < Toggelobjects.Length; i++)
                    {
                        Toggelobjects[i].SetActive(!Toggelobjects[i].activeSelf);
                    }
                }
            }
            else if(interactfunc == 1)
            {
                if (Global == true)
                {
                    SendCustomNetworkEvent(NetworkEventTarget.All, "NetButtonOn");
                }
                else if (Global == false)
                {
                    for (int i = 0; i < ObjectsON.Length; i++)
                    {
                        ObjectsON[i].SetActive(true);
                    }
                    for (int n = 0; n < ObjectsOFF.Length; n++)
                    {
                        ObjectsOFF[n].SetActive(false);
                    }
                }
            }
            else if(interactfunc == 2)
            {
                if (Global == true)
                {
                    SendCustomNetworkEvent(NetworkEventTarget.All, "NetButtonOn");
                    SendCustomNetworkEvent(NetworkEventTarget.All, "NetToggel");
                }
                else if (Global == false)
                {
                    for (int i = 0; i < ObjectsON.Length; i++)
                    {
                        ObjectsON[i].SetActive(true);
                    }
                    for (int n = 0; n < ObjectsOFF.Length; n++)
                    {
                        ObjectsOFF[n].SetActive(false);
                    }
                    for (int k = 0; k < Toggelobjects.Length; k++)
                    {
                        Toggelobjects[k].SetActive(!Toggelobjects[k].activeSelf);
                    }
                }
            }

        }
        #endregion ButtonsToggel
        #region Networking
        public void NetToggel()
        {
            for (int i = 0; i < Toggelobjects.Length; i++)
            {
                Toggelobjects[i].SetActive(!Toggelobjects[i].activeSelf);
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
        #endregion Networking
        void Start()
        {

        }
    }
}
