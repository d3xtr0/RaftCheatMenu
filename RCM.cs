using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModAPI;
using ModAPI.Attributes;
using RaftCheatMenu.Utils;
using UnityEngine;

namespace RaftCheatMenu
{
    class RCM : MonoBehaviour
    {
        public static GameObject go = null;
        private bool visible;
        public static GUIStyle labelStyle;
        public static Rect MenuRect { get; set; }
        public static float Y { get; set; }
        public static bool IsOpen { get; set; }
        public static int CurrentTab { get; set; }
        public static float Width = 550;
        public const float Height = 500;
        public const float ToolbarHeight = 30;
        public const float Padding = 10;
        public const float StepY = 30;
        public static Vector2 ScrollPosition = Vector2.zero;
        private float scroller;

        public static Player _Player;
        public static Network_Player _Network_Player;
        public static AzureSkyController _AzureSkyController;
        public static WeatherManager _WeatherManager;
        public static Raft _Raft;

        public static class Cheat
        {
            public static bool GodMode = false;
            public static bool FlyMode = false;
            public static float SpeedMultiplier = 1f;
            public static float moveAcceleration = 0.2f;
            public static float moveSpeed = 8f;
            public static float jumpAcceleration = 1f;
            public static bool fixTimeScale = false;
            public static WeatherConnection[] _WeatherConnection;
        }

        private void OnGUI()
        {
            if (visible)
            {
                GUI.skin = Gui.Skin;
                Width = Mathf.Clamp(Camera.main.pixelWidth / 2, 700, Camera.main.pixelWidth);
                MenuRect = new Rect(10, 10, Mathf.Clamp(Camera.main.pixelWidth / 2, 700, Camera.main.pixelWidth), Height);

                // apply label style if not existing
                if (labelStyle == null)
                {
                    labelStyle = new GUIStyle(GUI.skin.label);
                    labelStyle.fontSize = 12;
                }

                GUI.Box(MenuRect, "", GUI.skin.window);

                CurrentTab = GUI.Toolbar(new Rect(MenuRect.xMin, MenuRect.yMin, MenuRect.width, ToolbarHeight), CurrentTab,
                    new GUIContent[]
                    {
                        new GUIContent("Cheats"),
                        new GUIContent("Environment"),
                        new GUIContent("Player"),
                        new GUIContent("Spawn"),
                        new GUIContent("Inventory"),
                        new GUIContent("Coop"),
                        new GUIContent("Dev"),
                        new GUIContent("Game")
                    },
                    GUI.skin.GetStyle("Tabs"));

                Y = MenuRect.yMin + ToolbarHeight; //+ Padding

                switch (CurrentTab)
                {
                    case 0: // Cheats
                    {
                        ScrollPosition = GUI.BeginScrollView(new Rect(MenuRect.xMin, Y, Width, Height - ToolbarHeight - 5), ScrollPosition, new Rect(0, 0, Width - 24, this.scroller));
                        Y = Padding;

                        UI.AddCheckBox(ref Cheat.GodMode, "God Mode");

                        if (UI.AddButton("Set Anchor"))
                        {
                            _Raft.AddAnchor(_Raft.transform, false, null);
                        }
                        IncreaseY(-30);
                        if (UI.AddButton("Remove Anchor", 220))
                        {
                            _Raft.RemoveAnchor(_Raft.transform);
                        }

                        this.scroller = Y + 15;
                        GUI.EndScrollView();
                        break;
                    }
                    case 1: //Environment
                    {
                        ScrollPosition = GUI.BeginScrollView(new Rect(MenuRect.xMin, Y, Width, Height - ToolbarHeight - 5), ScrollPosition, new Rect(0, 0, Width - 24, this.scroller));
                        Y = Padding;

                        UI.AddLabel("Time");
                        UI.AddCheckBox(ref Cheat.fixTimeScale, "Pause");
                        if (_AzureSkyController)
                        {
                            if (UI.AddButton("+1h"))
                            {
                                float at = _AzureSkyController.Azure_Timeline;
                                at += 1f;
                                if (at >= 24f)
                                {
                                    at -= 24f;
                                }
                                _AzureSkyController.AzureSetTime(at, _AzureSkyController.Azure_DayCycle);
                            }
                            IncreaseY(-30);
                            if (UI.AddButton("-1h", 220))
                            {
                                float at = _AzureSkyController.Azure_Timeline;
                                at -= 1f;
                                if (at <= 0f)
                                {
                                    at += 24f;
                                }
                                _AzureSkyController.AzureSetTime(at, _AzureSkyController.Azure_DayCycle);
                            }

                            UI.AddSlider(ref _AzureSkyController.Azure_FogDistance, 50, 5000, "Fog Distance");
                        }

                        UI.AddLabel("Weather");
                        foreach (var weather in Cheat._WeatherConnection)
                        {
                            if (UI.AddButton(weather.weatherObject.name))
                            {
                                _WeatherManager.SetWeather(weather.weatherObject.name, true);
                            }
                        }

                        this.scroller = Y + 15;
                        GUI.EndScrollView();
                        break;
                    }
                    case 2: //Player
                    {
                        ScrollPosition = GUI.BeginScrollView(new Rect(MenuRect.xMin, Y, Width, Height - ToolbarHeight - 5), ScrollPosition, new Rect(0, 0, Width - 24, this.scroller));
                        Y = Padding;

                        UI.AddSlider(ref Cheat.SpeedMultiplier, 1, 20, "Player Speed");

                        if (UI.AddButton("Add Durability to HotSlot", 20, 300))
                        {
                            _Network_Player.Inventory.RemoveDurabillityFromHotSlot(-100);
                        }

                        this.scroller = Y + 15;
                        GUI.EndScrollView();
                        break;
                    }
                    case 3: //Spawn
                    {
                        break;
                    }
                    case 4: //Inventory
                    {
                        ScrollPosition = GUI.BeginScrollView(new Rect(MenuRect.xMin, Y, Width, Height - ToolbarHeight - 5), ScrollPosition, new Rect(0, 0, Width - 24, this.scroller));
                        Y = Padding;

                        var items = ItemManager.GetAllItems();
                        foreach (Item_Base current in items)
                        {
                            UnityEngine.GUI.Label(new Rect(20f, Y, 270f, 20f), current.UniqueName + " [" + current.UniqueIndex + "]", labelStyle);
                            if (UnityEngine.GUI.Button(new Rect(290f, Y, 40f, 20f), "+1"))
                            {
                                _Network_Player.Inventory.AddItem(current.UniqueName, 1);
                            }
                            if (UnityEngine.GUI.Button(new Rect(340f, Y, 40f, 20f), "+20"))
                            {
                                _Network_Player.Inventory.AddItem(current.UniqueName, 20);
                            }
                            Y += 30f;
                        }

                        this.scroller = Y + 15;
                        GUI.EndScrollView();
                        break;
                    }
                    case 5: //Coop
                    {
                        break;
                    }
                    case 6: //Dev
                    {
                        break;
                    }
                    case 7: //Game
                    {
                        ScrollPosition = GUI.BeginScrollView(new Rect(MenuRect.xMin, Y, Width, Height - ToolbarHeight - 5), ScrollPosition, new Rect(0, 0, Width - 24, this.scroller));
                        Y = Padding;

                        if (UI.AddButton("Go to Raft", 20, 300))
                        {
                            _Player.transform.position = _Raft.transform.position + (Vector3.up * 2);
                        }

                        this.scroller = Y + 15;
                        GUI.EndScrollView();
                        break;
                    }
                }
            }
        }

        private void Update()
        {
            if (_Player == null)
            {
                _Player = ComponentManager<Player>.Value;
            }
            if (_Network_Player == null)
            {
                _Network_Player = ComponentManager<Network_Player>.Value;
            }
            if (_AzureSkyController == null)
            {
                _AzureSkyController = UnityEngine.Object.FindObjectOfType<AzureSkyController>();
            }
            if (_WeatherManager == null)
            {
                _WeatherManager = UnityEngine.Object.FindObjectOfType<WeatherManager>();
            }
            if (_Raft == null)
            {
                _Raft = ComponentManager<Raft>.Value;
            }

            if (Cheat.fixTimeScale)
            {
                Time.timeScale = 0f;
            }
            else
            {
                if (!visible) Time.timeScale = 1f;
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.F3))
            {
                ModAPI.Log.Write("Flying");
                Cheat.FlyMode = !Cheat.FlyMode;
            }
            // if clicked button
            if (UnityEngine.Input.GetKeyDown(KeyCode.F1))
            {
                ModAPI.Log.Write("Menü");
                if (visible)
                {
                    // menu is closed
                    _Player.SetMouseLookScripts(true);
                    Helper.SetCursorVisibleAndLockState(false, CursorLockMode.Locked);
                    Time.timeScale = 1f;
                }
                else
                {
                    // menu is open
                    _Player.SetMouseLookScripts(false);
                    Helper.SetCursorVisibleAndLockState(true, CursorLockMode.None);
                    Time.timeScale = 0f;
                }
                visible = !visible;
            }
        }

        public static void IncreaseY(float step = StepY)
        {
            Y += step;
        }
    }
}
