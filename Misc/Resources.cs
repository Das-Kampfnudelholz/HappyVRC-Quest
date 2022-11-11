using MelonLoader;
using System;
using System.IO;
using System.Linq;
using UnhollowerRuntimeLib;
using UnhollowerRuntimeLib.XrefScans;
using UnityEngine;
using System.Reflection;
using UnhollowerBaseLib.Attributes;
using Utils;
using System.Net;
using ReMod.Core.Managers;

namespace CyResources
{
    public class Resources
    {

        
        
        //Set your own directory here
        private static string resourcePath = Path.Combine(Environment.CurrentDirectory, "UserData/CRXC/Resources");

        public static Sprite TEMPIcon;
        //This specific name is to set the Tab icon, set it to whatever your tab will be named

        public static Sprite Template;
        //You see how every button has CyResources.Resources.Template??? yeah. those should be
        //individual icons, clone this, look at how TEMPIcon is set up, and give everything its own name
        //never tested this, so if it breaks something, change it all back to CyResources.Resources.Template


        private static Sprite LoadSprite(string sprite)
        {
            return $"{resourcePath}/{sprite}".LoadSpriteFromDisk();
        }

        public static void OnApplicationStart()
        {

            //If you're lazy, you can uncomment this section and it will download my fursona to be your tab icon
            void download()
            {
                string url = "https://i.imgur.com/Ril8U0Q.png";
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(new Uri(url), "UserData/TEMP/Resources/TEMPIcon.png");
                    MelonLogger.Msg("downloaded?");
                }
            }
            MelonLogger.Msg("Initializing resources...");
        	LoadResources();
        }
        public static void LoadResources()
        {
            TEMPIcon = LoadSprite("TEMPIcon.png");
            //Put ur own icons here or ima download my fursona to ur quest
        }
    }
}
