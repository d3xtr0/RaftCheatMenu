﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RaftCheatMenu.Overwrites
{
    class PersonControllerOv : PersonController
    {
        protected float basenormalSpeed;
        protected float basesprintSpeed;
        protected float baseswimSpeed;
        protected float basegravity;
        protected bool LastFlyMode;

        protected void BaseValues()
        {
            this.basenormalSpeed = this.normalSpeed;
            this.basesprintSpeed = this.sprintSpeed;
            this.baseswimSpeed = this.swimSpeed;
            this.basegravity = this.gravity;
        }

        protected override void Start()
        {
            this.BaseValues();
            base.Start();
        }

        protected override void Update()
        {
            this.normalSpeed = this.basenormalSpeed * RCM.Cheat.SpeedMultiplier;
            this.sprintSpeed = this.basesprintSpeed * RCM.Cheat.SpeedMultiplier;
            this.swimSpeed = this.baseswimSpeed * RCM.Cheat.SpeedMultiplier;

            base.Update();

            if (RCM.Cheat.FlyMode)
            {
                this.gravity = 0f;
                this.controllerType = ControllerType.Ground;

                bool button = MyInput.GetButton("LeftControl");
                bool sprint = MyInput.GetButton("Sprint");
                bool button2 = MyInput.GetButton("Jump");

                float updown = 0f;
                if (button2)
                {
                    updown = 1f;
                }
                if (button)
                {
                    updown = -1f;
                }

                Vector3 lhs = new Vector3(MyInput.GetAxis("Strafe"), updown, MyInput.GetAxis("Walk"));
                this.moveDirection = base.transform.right * lhs.x + base.transform.up * lhs.y + base.transform.forward * lhs.z;
                this.moveDirection *= sprint ? sprintSpeed : normalSpeed;
                this.controller.Move(moveDirection * Time.deltaTime);

                LastFlyMode = true;
                return;
            }
            if (LastFlyMode)
            {
                this.gravity = this.basegravity;
                LastFlyMode = false;
            }
        }

        protected override void WaterControll()
        {
            if (RCM.Cheat.FlyMode) return;
            base.WaterControll();
        }

        protected override void GroundControll()
        {
            if (RCM.Cheat.FlyMode) return;
            base.GroundControll();
        }
    }
}
