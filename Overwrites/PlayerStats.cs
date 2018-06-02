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
            if (RCM.go == null)
            {
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
                this.stat_hunger.NormalValue = 1f;
                this.stat_hunger.stat_targetHunger.NormalValue = 1f;
                this.stat_thirst.NormalValue = 1f;
                this.stat_thirst.stat_targetThirst.NormalValue = 1f;
            }
            base.Update();
        }
    }
}
