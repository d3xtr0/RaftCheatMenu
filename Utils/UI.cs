using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RaftCheatMenu.Utils
{
    public static class UI
    {
        public static bool AddCheckBox(ref bool updateValue, string label)
        {
            updateValue = GUI.Toggle(new Rect(20, RCM.Y, RCM.MenuRect.width - 50, 30), updateValue, label);
            RCM.IncreaseY();
            return updateValue;
        }

        public static void AddLabel(string text, float x = 20, float width = 150)
        {
            GUI.Label(new Rect(x, RCM.Y, width, 30), text, RCM.labelStyle);
            RCM.IncreaseY();
        }

        public static float AddSlider(ref float updateValue, float minValue, float maxValue, string text, float x = 160, float width = 210, float height = 30)
        {
            AddLabel(text, 20, 150);
            RCM.IncreaseY(-30);
            updateValue = GUI.HorizontalSlider(new Rect(x, RCM.Y + 3f, width, height), updateValue, minValue, maxValue);
            RCM.IncreaseY();
            return updateValue;
        }
        /*
        public static void AddRadioButtons(ref int updateValue, string[] texts, int xCount, float x = Padding, float width = 250, float height = 20, bool increaseY = false, bool autoAlign = true)
        {
            updateValue = GUI.SelectionGrid(new Rect((autoAlign ? MenuRect.xMin : 0) + Padding + x, Y, width, height), updateValue, texts, xCount, GUI.skin.GetStyle("Button"));
            RCM.IncreaseY(increaseY);
        }

        public static void AddLabel(string text, float x = Padding, float width = 150, float height = 20, bool increaseY = false, bool autoAlign = true)
        {
            GUI.Label(new Rect((autoAlign ? MenuRect.xMin : 0) + Padding + x, Y, width, height), text, LabelStyle);
            RCM.IncreaseY(increaseY);
        }


        public static bool AddCheckBox(ref bool updateValue, string text, float x = 160, float width = 20, float height = 30, bool increaseY = false, bool autoAlign = true)
        {
            updateValue = GUI.Toggle(new Rect((autoAlign ? MenuRect.xMin : 0) + Padding + x, Y, width, height), updateValue, text);
            RCM.IncreaseY(increaseY);
            return updateValue;
        }

        public static float AddSlider(ref float updateValue, float minValue, float maxValue, float x = 160, float width = 210, float height = 30, bool increaseY = false, bool autoAlign = true)
        {
            updateValue = GUI.HorizontalSlider(new Rect((autoAlign ? MenuRect.xMin : 0) + Padding + x, Y + 3f, width, height), updateValue, minValue, maxValue);
            RCM.IncreaseY(increaseY);
            return updateValue;
        }

        public static bool AddButton(string text, float x = Padding, float width = 180, float height = 20, bool increaseY = false, bool autoAlign = true)
        {
            var result = GUI.Button(new Rect((autoAlign ? MenuRect.xMin : 0) + Padding + x, Y, width, height), text);
            RCM.IncreaseY(increaseY);
            return result;
        }
        
        public static bool AddStatSlider(
            string text,
            ref float updateValueActive,
            ref float updateValueInactive,
            float min,
            float max,
            ref bool updateFixValue,
            float checkBoxPosition,
            bool autoAlign = true)
        {
            AddLabel(text);
            if (updateFixValue)
            {
                if (updateValueActive < 0)
                {
                    updateValueActive = updateValueInactive;
                }
                AddSlider(ref updateValueActive, min, max, width: 160);
            }
            else
            {
                updateValueActive = -1;
                AddSlider(ref updateValueInactive, min, max, width: 160);
            }
            AddLabel(Mathf.RoundToInt(updateFixValue ? updateValueActive : updateValueInactive).ToString(), checkBoxPosition - 30, 40);
            AddCheckBox(ref updateFixValue, checkBoxPosition, height: 20);
            return updateFixValue;
        }
        */

    }
}
