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

        void OnPointerExit(PointerEventData obj)
        {
            if (Fsm.IsCurrent(this))
                Handler.Enable();
        }

        void OnPointerDown(PointerEventData eventData)
        {
            if (Fsm.IsCurrent(this))
                Handler.Select();
        }

        //--------------------------------------------------------------------------------------------------------------

        void ResetValues()
        {
            var rotationSpeed = Handler.IsPlayer ? Parameters.RotationSpeed : Parameters.RotationSpeedP2;
            Handler.RotateTo(StartEuler, rotationSpeed);
            Handler.MoveTo(StartPosition, Parameters.HoverSpeed);
            Handler.ScaleTo(StartScale, Parameters.ScaleSpeed);
        }

        void SetRotation()
        {
            if (Parameters.HoverRotation)
                return;

            var speed = Handler.IsPlayer ? Parameters.RotationSpeed : Parameters.RotationSpeedP2;

            Handler.RotateTo(Vector3.zero, speed);
        }

        /// <summary>
        ///     View Math.
        /// </summary>
        void SetPosition()
        {
            var camera = Handler.MainCamera;
            var halfCardHeight = new Vector3(0, Handler.Renderer.bounds.size.y / 2);
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

        void SetScale()
        {
            var currentScale = Handler.transform.localScale;
            var finalScale = currentScale * Parameters.HoverScale;
            Handler.ScaleTo(finalScale, Parameters.ScaleSpeed);
        }

        void CachePreviousValues()
        {
            StartPosition = Handler.transform.position;
            StartEuler = Handler.transform.eulerAngles;
            StartScale = Handler.transform.localScale;
        }

        void SubscribeInput()
        {
            Handler.Input.OnPointerExit += OnPointerExit;
            Handler.Input.OnPointerDown += OnPointerDown;
        }

        void UnsubscribeInput()
        {
            Handler.Input.OnPointerExit -= OnPointerExit;
            Handler.Input.OnPointerDown -= OnPointerDown;
        }

        void CalcEdge()
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

        #region Properties

        Vector3 StartPosition { get; set; }
        Vector3 StartEuler { get; set; }
        Vector3 StartScale { get; set; }

        #endregion

        //--------------------------------------------------------------------------------------------------------------
    }
}