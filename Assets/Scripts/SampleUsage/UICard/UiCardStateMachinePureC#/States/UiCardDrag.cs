using System;
using Extensions;
using Patterns.StateMachine;
using UnityEngine;

namespace Tools.UI.Card
{
    public class UiCardDrag : UiBaseCardState
    {
        //--------------------------------------------------------------------------------------------------------------
        
        private Vector3 StartEuler { get; set; }
        private Camera MyCamera { get; }
        
        public UiCardDrag(IUiCard handler, Camera camera, BaseStateMachine fsm, UiCardParameters parameters) : base(handler, fsm, parameters)
        {
            MyCamera = camera;
        }
        
        //--------------------------------------------------------------------------------------------------------------
        
        #region Operations

        public override void OnUpdate()
        {
            AddMovement();
        }

        public override void OnEnterState()
        {
            //cache old values
            StartEuler = Handler.Transform.eulerAngles;

            Handler.RotateTo(Vector3.zero, Parameters.RotationSpeed);
            MakeRenderFirst();
            NormalColor();
        }

        public override void OnExitState()
        {
            //reset position and rotation
            if (Handler.Transform)
            {
                Handler.RotateTo(StartEuler, Parameters.RotationSpeed);
                MakeRenderNormal();
            }
        }
        
        #endregion
        
        //--------------------------------------------------------------------------------------------------------------
        

        private Vector3 WorldPoint()
        {
            var mousePosition = Handler.Input.MousePosition;
            var worldPoint = MyCamera.ScreenToWorldPoint(mousePosition);
            return worldPoint;
        }

        private void AddMovement()
        {
            var myZ = Handler.Transform.position.z;
            Handler.Transform.position = WorldPoint().WithZ(myZ);
        }

        private void AddTorque()
        {
            //TODO: Add Torque to the Card.

            throw new NotImplementedException();
        }
        
        //--------------------------------------------------------------------------------------------------------------
        
    }
}