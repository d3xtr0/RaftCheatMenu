using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LlockhamIndustries.Misc;
using ModAPI;
using ModAPI.Attributes;
using RaftCheatMenu.Utils;
using UnityEngine;
using Input = ModAPI.Input;

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

        private CanvasHelper canvas;

        public static class Cheat
        {
            public static bool GodMode = false;
            public static float moveAcceleration = 0.2f;
            public static float moveSpeed = 8f;
            public static float jumpAcceleration = 1f;
        }
        private void OnGUI()
        {
            if (visible)
            {
                GUI.skin = Interface.Skin;
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

                Y = MenuRect.yMin + ToolbarHeight + Padding;

                switch (CurrentTab)
                {
                    case 0: // Cheats
                        {
                            Y -= Padding;
                            ScrollPosition = GUI.BeginScrollView(new Rect(MenuRect.xMin, Y, Width, Height - ToolbarHeight - 5), ScrollPosition, new Rect(0, 0, Width - 24, Y));
                            Y = Padding;

                            UI.AddCheckBox(ref Cheat.GodMode, "God Mode");

                            GUI.EndScrollView();
                            break;
                        }
                    case 1: //Environment
                        {
                            break;
                        }
                    case 2: //Player
                        {
                            Y -= Padding;
                            ScrollPosition = GUI.BeginScrollView(new Rect(MenuRect.xMin, Y, Width, Height - ToolbarHeight - 5), ScrollPosition, new Rect(0, 0, Width - 24, Y));
                            Y = Padding;
                            /*base.GetComponent<FirstPersonCharacterController>().*/
                            UI.AddSlider(ref Cheat.moveAcceleration, 0, 100, "Move Acceleration");
                            UI.AddSlider(ref Cheat.moveSpeed, 0, 100, "Move Speed");
                            UI.AddSlider(ref Cheat.jumpAcceleration, 0, 100, "Jump Acceleration");

                            GUI.EndScrollView();
                            break;
                        }
                    case 3: //Spawn
                        {
                            break;
                        }
                    case 4: //Inventory
                        {
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
                            break;
                        }
                }
            }
        }

        private void Update()
        {
            // if clicked button
            if (Input.GetButtonDown("menu"))
            {
                ModAPI.Console.Write("menu");
                // show cursor
                if (visible)
                {
                    //canvas.CloseMenu(MenuType.PauseMenu);
                    Helper.SetCursorVisibleAndLockState(false, CursorLockMode.Locked);
                }
                else
                {
                    //canvas.OpenMenu(MenuType.PauseMenu, true);
                    Helper.SetCursorVisibleAndLockState(true, CursorLockMode.None);
                }
                // toggle menu
                visible = !visible;
            }
        }

        public static void IncreaseY(float step = StepY)
        {
            Y += step;
        }
    }
}
