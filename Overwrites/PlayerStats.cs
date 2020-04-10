using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RaftCheatMenu.Overwrites
{
    class PlayerStatsOv : PlayerStats
    {

        protected override void Start()
        {
            ModAPI.Log.Write("Start");
            if (RCM.go == null)
            {
                ModAPI.Log.Write("Adding RCM");
                RCM.go = new GameObject("__RCM__");
                RCM.go.AddComponent<RCM>();
            }
            base.Start();
        }

        protected override void Update()
        {
            if (RCM.Cheat.GodMode)
            {
                this.stat_health.NormalValue = 1f;
                /*
                this.canvas.hungerSlider.SetValue(this.stat_hunger.Normal.Max);
                this.canvas.hungerSlider.SetTargetValue(this.stat_hunger.Normal.stat_targetHunger.Max);
                */
                this.stat_hunger.Normal.Value = 1f;
                this.stat_hunger.Normal.NormalValue = 1f;
                this.stat_thirst.NormalValue = 1f;
                this.stat_thirst.stat_targetThirst.NormalValue = 1f;
                this.stat_oxygen.NormalValue = 1f;
            }
            base.Update();
        }
    }
}
