using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RustBuster2016.API;
using UnityEngine;

namespace PluginDePruebas
{
    public class Mainclass__ : RustBusterPlugin
    {
        public override string Name { get { return "PluginDePruebas"; } }
        public override string Author { get { return " by salva/juli"; } }
        public override Version Version { get { return new Version("1.0"); } }
        public static GameObject PluginLoader_;
        public static Mainclass__ Instance;
        public override void Initialize()
        {
            Instance = this;
            if (this.IsConnectedToAServer)
            {
                Hooks.OnRustBusterClientPluginsLoaded += Hooks_OnRustBusterClientPluginsLoaded;
            }
        }
        private void Hooks_OnRustBusterClientPluginsLoaded()
        {
            PluginLoader_ = new GameObject();
            PluginLoader_.AddComponent<WaitForPlayer>();
            UnityEngine.Object.DontDestroyOnLoad(PluginLoader_);
        }
        public override void DeInitialize()
        {
            Hooks.OnRustBusterClientPluginsLoaded -= Hooks_OnRustBusterClientPluginsLoaded;
            if (PluginLoader_ != null) UnityEngine.Object.DestroyImmediate(PluginLoader_);
        }
        public class WaitForPlayer : MonoBehaviour
        {
            public static bool IsPlayerReady = false;
            public void Update()
            {
                if (Controllable.localPlayerControllableExists)
                {
                    try
                    {
                        if (!IsPlayerReady)
                        {
                            PlayerClient.GetLocalPlayer().gameObject.AddComponent<Holaaaa>();
                            IsPlayerReady = true;
                        }
                    }
                    catch
                    {
                        IsPlayerReady = false;
                    }
                }
            }
            public void OnDisable()
            {
                IsPlayerReady = false;
            }
        }
        public class Holaaaa : MonoBehaviour
        {
            public bool ActivarGUI = false;
            public void Start()
            {
                ActivarGUI = false;
                Debug.Log("Funcionando************************************************************");
            }
            [RPC]
            public void TestDeString(string text)
            {
                Debug.Log("RECIBIDO");
                ConsoleSystem.Print("RECIBIDO" + text);

                if (text == "fuck")
                {
                    ActivarGUI = true;
                }
            }
            public void OnGUI()
            {
                if (ActivarGUI)
                {
                    GUI.Label(new Rect(100, 100, 100, 100), "MAGIA BABY!!!!");
                }
            }
        }
    }
}