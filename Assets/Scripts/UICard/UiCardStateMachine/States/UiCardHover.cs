using Extensions;
using Patterns.StateMachine;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tools.UI.Card
{
    public class UiCardHover : UiBaseCardState
    {
        //--------------------------------------------------------------------------------------------------------------

        public UiCardHover(IUiCard handler, BaseStateMachine fsm, UiCardParameters parameters) : base(handler, fsm,
            parameters)
        {
        }

        //--------------------------------------------------------------------------------------------------------------

        #region Operations

        public override void OnEnterState()
        {
            MakeRenderFirst();
            SubscribeInput();
            CachePreviousValues();
            SetScale();
            SetPosition();
            SetRotation();
        }

        public override void OnExitState()
        {
            ResetValues();
            UnsubscribeInput();
            DisableCollision();
        }

        #endregion

        //--------------------------------------------------------------------------------------------------------------

        private void OnPointerExit(PointerEventData obj)
        {
            if (Fsm.IsCurrent(this))
                Handler.Enable();
        }

        private void OnPointerDown(PointerEventData eventData)
        {
            if (Fsm.IsCurrent(this))
                Handler.Select();
        }

        //--------------------------------------------------------------------------------------------------------------

        private void ResetValues()
        {
            var rotationSpeed = Handler.IsPlayer ? Parameters.RotationSpeed : Parameters.RotationSpeedP2;
            Handler.RotateTo(StartEuler, rotationSpeed);
            Handler.MoveTo(StartPosition, Parameters.HoverSpeed);
            Handler.ScaleTo(StartScale, Parameters.ScaleSpeed);
        }

        private void SetRotation()
        {
            if (Parameters.HoverRotation)
                return;

            var speed = Handler.IsPlayer ? Parameters.RotationSpeed : Parameters.RotationSpeedP2;

            Handler.RotateTo(Vector3.zero, speed);
        }

        /// <summary>
        ///     View Math.
        /// </summary>
        private void SetPosition()
        {
            var camera = Handler.MainCamera;
            var halfCardHeight = new Vector3(0, Handler.MyRenderer.bounds.size.y / 2);
            var bottomEdge = Handler.MainCamera.ScreenToWorldPoint(Vector3.zero);
            var topEdge = Handler.MainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height));
            var edgeFactor = Handler.transform.CloserEdge(camera, Screen.width, Screen.height);
            var myEdge = edgeFactor == 1 ? bottomEdge : topEdge;
            var edgeY = new Vector3(0, myEdge.y);
            var currentPosWithoutY = new Vector3(Handler.transform.position.x, 0, Handler.transform.position.z);
            var hoverHeightParameter = new Vector3(0, Parameters.HoverHeight);
            var final = currentPosWithoutY + edgeY + (halfCardHeight + hoverHeightParameter) * edgeFactor;
            Handler.MoveTo(final, Parameters.HoverSpeed);
        }

        private void SetScale()
        {
            var currentScale = Handler.transform.localScale;
            var finalScale = currentScale * Parameters.HoverScale;
            Handler.ScaleTo(finalScale, Parameters.ScaleSpeed);
        }

        private void CachePreviousValues()
        {
            StartPosition = Handler.transform.position;
            StartEuler = Handler.transform.eulerAngles;
            StartScale = Handler.transform.localScale;
        }

        private void SubscribeInput()
        {
            Handler.Input.OnPointerExit += OnPointerExit;
            Handler.Input.OnPointerDown += OnPointerDown;
        }

        private void UnsubscribeInput()
        {
            Handler.Input.OnPointerExit -= OnPointerExit;
            Handler.Input.OnPointerDown -= OnPointerDown;
        }

        private void CalcEdge()
        {

        }

        //--------------------------------------------------------------------------------------------------------------

        #region Properties

        private Vector3 StartPosition { get; set; }
        private Vector3 StartEuler { get; set; }
        private Vector3 StartScale { get; set; }

        #endregion

        //--------------------------------------------------------------------------------------------------------------
    }
}