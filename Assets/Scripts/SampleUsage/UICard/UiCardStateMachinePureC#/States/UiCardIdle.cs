using Patterns.StateMachine;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tools.UI.Card
{
    /// <summary>
    ///     UI Card Idle behavior.
    /// </summary>
    public class UiCardIdle : UiBaseCardState
    {
        private Vector3 DefaultSize { get; set; }
        //--------------------------------------------------------------------------------------------------------------
        
        public UiCardIdle(IUiCard handler, BaseStateMachine fsm, UiCardParameters parameters) : base(handler, fsm, parameters)
        {
            Handler.Input.OnPointerDown += OnPointerDown;
            Handler.Input.OnPointerEnter += OnPointerEnter;
            DefaultSize = Handler.Transform.localScale;
        }
        
        //--------------------------------------------------------------------------------------------------------------

        public override void OnEnterState()
        {
            if (Handler.UiCardMovement.IsOperating)
            {
                DisableCollision();
                Handler.UiCardMovement.OnArrive += Enable;
            }
            else
                Enable();
            
            MakeRenderNormal();
            Handler.transform.localScale = DefaultSize;
        }

        public override void OnExitState()
        {
            Handler.UiCardMovement.OnArrive -= Enable;
        }
        
        //--------------------------------------------------------------------------------------------------------------

        private void OnPointerEnter(PointerEventData obj)
        {
            if (Fsm.IsCurrent(this))
                Handler.Hover();
        }

        private void OnPointerDown(PointerEventData eventData)
        {
            if (Fsm.IsCurrent(this))
                Handler.Select();
        }
        
        //--------------------------------------------------------------------------------------------------------------
    }
}