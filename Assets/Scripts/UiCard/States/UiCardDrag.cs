using System;
using Extensions;
using UnityEngine;

namespace Tools.UI.Card
{
    public class UiCardDrag : UiBaseCardState
    {
        private Vector3 StartPosition { get; set; }
        private Quaternion StartRotation { get; set; }
        private Camera MyCamera { get; set; }

        public override void OnAwake()
        {
            base.OnAwake();
            MyCamera = Camera.main;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            AddMovement();
        }

        public override void OnEnterState()
        {
            //cache old values
            StartPosition = MyTransform.position;
            StartRotation = MyTransform.rotation;

            MyTransform.localRotation = Quaternion.identity;
            MakeRenderFirst();
            NormalColor();
        }

        public override void OnExitState()
        {
            //reset position and rotation
            MyTransform.position = StartPosition;
            MyTransform.rotation = StartRotation;
            MakeRenderNormal();
        }

        private Vector3 WorldPoint()
        {
            var mousePosition = MyInput.MousePosition;
            var worldPoint = MyCamera.ScreenToWorldPoint(mousePosition);
            return worldPoint;
        }

        private void AddMovement()
        {
            var myZ = MyTransform.position.z;
            MyTransform.position = WorldPoint().WithZ(myZ);
        }

        private void AddTorque()
        {
            //TODO: Add Torque to the Card.

            throw new NotImplementedException();
        }
    }
}