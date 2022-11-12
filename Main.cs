using MelonLoader;
using Oculus.Platform;
using QuestMod;
using ReButtonAPI;
using ReButtonAPI.QuickMenu;
using ReButtonAPI.Wings;
using ReMod.Core.Managers;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transmtn;
using UnhollowerRuntimeLib;
using UnhollowerRuntimeLib;
using UnhollowerRuntimeLib.XrefScans;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using VRC;
using VRC.Core;
using VRC.DataModel;
using VRC.SDKBase;
using VRC.Udon;
using VRC.UI;
using VRC.UI.Core;
using VRC.UI.Elements;
using VRC.UI.Elements.Menus;
using static UnityEngine.UI.Image;
using System.Net;
using UnityEngine.Networking;
using Il2CppSystem;
using Action = System.Action;
using Object = UnityEngine.Object;
using Environment = System.Environment;
using Il2CppSystem.Security.Cryptography;


//IMPORTANT NOTE!!!! YOU NEED TO RENAME THE ENTIRE ReButtonAPI NAMESPACE 
//IF YOU INTEND TO USE THIS WITH ABSOULUTELY ANY OTHER MODS MADE WITH THIS TEMPLATE
//You can do this by right clicking it below
namespace ReButtonAPI
{





        //This ButtonAPI isnt Standalone, its just for HowTo
        public class Main : MelonMod
        {
            //private static string musicloc = System.IO.Path.Combine(Environment.CurrentDirectory, "HappyVRC/Resources/Music");
           // DirectoryInfo di = Directory.CreateDirectory(musicloc);
            public static GameObject UserInterface; //New UI is "Obfuscate" thats why we need first to Grab the GameObject
            private static UiManager _uiManager;
            private static ReMenuButton _testspace;
            private static ReMenuButton _menuButton1;
            private static ReCategoryPage _catapage;
            private static ReWingMenu _leftWing;
            private static ReMenuButton _targetButton1;
            private static ReMenuButton _targetButton2;
            private static ReMenuButton _teleportTargetButton;
            public static ReMirroredWingMenu WingMenu;
            public static ReMirroredWingButton _WingButton;
            public static ReMirroredWingToggle _WingToggle;

        private GameObject loadScreenPrefab;
        private static AudioClip _clip;
        private static readonly MelonPreferences_Category Cla =
    MelonPreferences.CreateCategory("CLA", "Custom Loading Audio");
        private static MelonPreferences_Entry<string> _path =
    Cla.CreateEntry("path", Environment.CurrentDirectory + "/UserData/CustomLoadAudio/music.mp3", "Audio File Path", "The file path the audio will be loaded from.");


        public static int Speed { get; private set; }

            public override void OnUpdate()
            {
                if (Utils.esp.ESP == true)
                {
                    new WaitForSeconds(10f);
                    Utils.esp.espmethod();
                }
            }
            private static HarmonyLib.Harmony Harmony;


            public static void OnUiManagerInit()
            {
               
            }

            public static IEnumerator SetAudio()
            {
            var clip = UnityWebRequest.Get(Environment.CurrentDirectory + "/UserData/CustomLoadAudio/music.mp3");
            clip.SendWebRequest();
            while (!clip.isDone) yield return null;
            if (clip.isHttpError) yield break;
            var audioclip = WebRequestWWW.InternalCreateAudioClipUsingDH(clip.downloadHandler, clip.url, false, false, AudioType.UNKNOWN);


            while (GameObject.Find("UserInterface/LoadingBackground_TealGradient_Music") == null) yield return null;
            var source1 = GameObject.Find("UserInterface/LoadingBackground_TealGradient_Music").transform.Find("LoadingSound").GetComponent<AudioSource>();
            source1.clip = audioclip;
            source1.Play();
            //every load screen after
            while (GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup") == null) yield return null;
            var source2 = GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup").transform.Find("LoadingSound").GetComponent<AudioSource>();
            source2.clip = audioclip;
            source2.Play();








            yield return null;
                yield break;
            }




        public override void OnApplicationStart()
        {
            System.IO.Directory.CreateDirectory(Environment.CurrentDirectory + "/UserData/CustomLoadAudio");
            SetAudio();
            Harmony = HarmonyInstance;
                using (var client = new WebClient())
                {
                    //         client.DownloadFile("http://example.com/file/song/a.mpeg", musicloc);
                }
                ClassInjector.RHelperRegisterTypeInIl2Cpp<EnableDisableListener>();
                MelonCoroutines.Start(WaitForUI());
                IEnumerator WaitForUI()
                {
                while (ReferenceEquals(VRCUiManager.field_Private_Static_VRCUiManager_0, null)) yield return null; // wait till VRCUIManger isnt null
                    foreach (var GameObjects in Resources.FindObjectsOfTypeAll<GameObject>())
                    {
                        if (GameObjects.name.Contains("UserInterface"))
                        {
                            UserInterface = GameObjects;
                        }
                    }

                    while (ReferenceEquals(QuickMenuEx.Instance, null)) yield return null;
                    MenuStart();
                    yield break;
                }
            }
            [HarmonyLib.HarmonyPatch(typeof(Message), "get_IsError")]
            class Patch
            {
                private static int first = 0;
                private static int second = 0;
                public static void Postfix(ref bool __result)
                {
                    if (first < 5)
                    {
                        first++;
                        return;
                    }

                    if (second < 2)
                    {
                        __result = false;
                        second++;
                        if (second == 2)
                            Harmony.UnpatchAll(Harmony.Id);
                    }
                }
            }
            public static void MenuStart()
            {
                MelonLogger.Msg("Welcome to HappyVRC");



                _uiManager = new UiManager("HappyVRC", CyResources.Resources.TEMPIcon);

            VRCPlayer.field_Internal_Static_VRCPlayer_0.gameObject.GetComponent<VRCPlayer>()._player.Method_Internal_get_APIUser_PDM_0().id.
                //---------------------Movement---------------------------------
                _uiManager.MainMenu.AddMenuPage("Movement", "Movement", null);
                var dMovement = _uiManager.MainMenu.GetMenuPage("Movement");
                _testspace = dMovement.AddSpacer(null);
                _menuButton1 = dMovement.AddButton("Force Jump", "Force jump ", () =>
                {
                    Networking.LocalPlayer.SetJumpImpulse(3);
                }, null);
                _menuButton1 = dMovement.AddButton("High Jump", "High jump ", () =>
                {
                    Networking.LocalPlayer.SetJumpImpulse(5);
                    VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = true;
                }, null);
                //---------------------Movement---------------------------------


                //---------------------Visuals---------------------------------
                _uiManager.MainMenu.AddMenuPage("Visuals", "Visuals", null);
                var dVisuals = _uiManager.MainMenu.GetMenuPage("Visuals");
                _testspace = dVisuals.AddSpacer(null);
                _menuButton1 = dVisuals.AddButton("ESP", "ESP", () =>
                {
                    ToggleESP(true);
                }, null);
                //---------------------Visuals--------------------------------------


                //---------------------Crasher---------------------------------
                _uiManager.MainMenu.AddMenuPage("Crasher", "Crasher", null);
                var dCrasher = _uiManager.MainMenu.GetMenuPage("Crasher");
                _testspace = dCrasher.AddSpacer(null);
                _menuButton1 = dCrasher.AddButton("PC Crasher", "PC Crasher", () =>
                {

                }, null);
                //---------------------Crasher--------------------------------------


                //---------------------Cheatos---------------------------------
                string Role(string Target, string Event)
                {
                    string text = null;
                    for (int i = 0; i < 24; i++)
                    {
                        text = "Player Node (" + i.ToString() + ")";
                        if (GameObject.Find("Game Logic/Game Canvas/Game In Progress/Player List/Player List Group/Player Entry (" + i.ToString() + ")/Player Name Text").GetComponent<Text>().text.Equals(Target))
                        {
                            UdonBehaviour component = GameObject.Find(text).GetComponent<UdonBehaviour>();
                            component.SendCustomNetworkEvent(0, Event);
                        }
                    }
                    return text;
                }
                _uiManager.MainMenu.AddMenuPage("Cheats", "Cheats", null);
                var dCheat = _uiManager.MainMenu.GetMenuPage("Cheats");
                 _menuButton1 = dCheat.AddButton("God Mode", "God Mode ", () =>
                {

                  }, null);
            _menuButton1 = dCheat.AddButton("Make Murder", "Make Murder ", () =>
                {
                    Role(APIUser.CurrentUser.displayName, "SyncAssignM");
                }, null);
                _menuButton1 = dCheat.AddButton("Make Bystander", "Make Bystander ", () =>
                {
                    Role(APIUser.CurrentUser.displayName, "SyncAssignB");
                }, null);
                _menuButton1 = dCheat.AddButton("Make Detective", "Make Detective ", () =>
                {
                    Role(APIUser.CurrentUser.displayName, "SyncAssignD");
                }, null);
                _testspace = dCheat.AddSpacer(null);

                _menuButton1 = dCheat.AddButton("Start Game", "Start Game ", () =>
                {
                    GameObject.Find("/Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncCountdown");
                }, null);
                _menuButton1 = dCheat.AddButton("Stop Game", "Stop Game ", () =>
                {
                    GameObject.Find("/Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncAbort");
                }, null);

                _testspace = dCheat.AddSpacer(null);
                _testspace = dCheat.AddSpacer(null);
                 _menuButton1 = dCheat.AddButton("Release Snake", "Release Snake", delegate
                {
                    GameObject.Find("/Game Logic").transform.Find("Snakes/SnakeDispenser").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "DispenseSnake");
                }, null);


            //---------------------Cheatos---------------------------------


            //---------------------Movement---------------------------------
            _uiManager.MainMenu.AddMenuPage("Lovense Connect", "Connect your Lovense toy", null);
            var dERP = _uiManager.MainMenu.GetMenuPage("Lovense");
            _testspace = dERP.AddSpacer(null);
            _menuButton1 = dERP.AddButton("Lovense", "Lovense", () =>
            {
                Networking.LocalPlayer.SetJumpImpulse(3);
            }, null);
            _menuButton1 = dERP.AddButton("High Jump", "High jump ", () =>
            {
                Networking.LocalPlayer.SetJumpImpulse(5);
                VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = true;
            }, null);
            //---------------------Movement---------------------------------




            if (APIUser.CurrentUser.id.Contains("usr_a07dad30-bd86-47c9-b787-6e6fb124c72c"))
                {

                    _uiManager.MainMenu.AddMenuPage("UwU", "UwU", null);
                    var dUwU = _uiManager.MainMenu.GetMenuPage("UwU");
                    _testspace = dUwU.AddSpacer(null);
                    _menuButton1 = dUwU.AddButton("Force Jump", "Force jump ", () =>
                    {
                        Networking.LocalPlayer.SetJumpImpulse(3);
                    }, null);
                    _menuButton1 = dUwU.AddButton("High Jump", "High jump ", () =>
                    {
                        Networking.LocalPlayer.SetJumpImpulse(5);
                        VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = true;
                    }, null);
                    return;
                }

                //Wing stuff
                //A mirrored wing menu
                WingMenu = ReMirroredWingMenu.Create("Demo Menu", "Opens a wing Menu", null);
                //And a left wing
                _leftWing = new ReWingMenu("Left", true);
                //This is a button inside the wing menu
                _WingButton = WingMenu.AddButton("Button1", "This is a demo button", new Action(() =>
                {
                    MelonLogger.Msg("Test");
                }), null);
                //And a wing toggle
                _WingToggle = WingMenu.AddToggle("ESP", "Turns on ESP", ToggleESP, false);



                //This is all selected user stuff, refer to above ig?
                _uiManager.TargetMenu.AddMenuPage("Target Page", "Target page", null);
                var targetPageMenu = _uiManager.MainMenu.GetMenuPage("Target Page");
                var targetMenu = _uiManager.TargetMenu;
                //A target button
                _targetButton1 = targetMenu.AddButton("target Button1", "Yet another demo", () =>
                {
                    //code
                }, null);
                //Example of how to use it
                _teleportTargetButton = targetMenu.AddButton("Teleport", "Teleports to target.", () =>
                {
                    VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0.TeleportTo(IUserExtension.GetVRCPlayer().transform.position, IUserExtension.GetVRCPlayer().transform.rotation);
                }, null);


            }
            public static void ChangeAvatarById(string AvatarId)
            {




                PageAvatar pageAvatar = GameObject.Find("Screens").transform.Find("Avatar").GetComponent<PageAvatar>();
                pageAvatar.ChangeToSelectedAvatar();

            }
            public static void ChangeAvatar(ApiAvatar avatar)
            {
                PageAvatar pageAvatar = GameObject.Find("Screens").transform.Find("Avatar").GetComponent<PageAvatar>();
                pageAvatar.ChangeToSelectedAvatar();
            }
            public static void ToggleESP(bool value)
            {
                if (Utils.esp.ESP == true)
                {
                    Utils.esp.ESP = false;
                    Utils.esp.espmethod();
                }
                else
                   if (Utils.esp.ESP == false)
                {
                    Utils.esp.ESP = true;
                    Utils.esp.espmethod();
                }
            }

        }

        public class SpeedHack
        {
            public static bool isSpeedHack = false;
            public static int iSpeedHackSpeed = 8;
            public static void Toggle()
            {
                isSpeedHack = !isSpeedHack;

            }

            public void Update()
            {
                if (!isSpeedHack) return;
                Networking.LocalPlayer.SetStrafeSpeed(3);
                Networking.LocalPlayer.SetWalkSpeed(4);
                Networking.LocalPlayer.SetRunSpeed(4);
                VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = true;
            }
        }
    }

