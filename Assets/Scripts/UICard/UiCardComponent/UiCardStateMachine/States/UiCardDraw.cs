using Patterns.StateMachine;
using UnityEngine;

namespace Tools.UI.Card
{
    /// <summary>
    ///     This state draw the collider of the card.
    /// </summary>
    public class UiCardDraw : UiBaseCardState
    {
        public UiCardDraw(IUiCard handler, BaseStateMachine fsm, UiCardParameters parameters) : base(handler, fsm,
            parameters)
        {
        }

        Vector3 StartScale { get; set; }

        //--------------------------------------------------------------------------------------------------------------

        #region Operations

        public override void OnEnterState()
        {
            CachePreviousValue();
            DisableCollision();
            SetScale();
            Handler.Movement.OnFinishMotion += GoToIdle;
        }

        public override void OnExitState() => Handler.Movement.OnFinishMotion -= GoToIdle;

        void GoToIdle() => Handler.Enable();

        void CachePreviousValue()
        {
            StartScale = Handler.transform.localScale;
            Handler.transform.localScale *= Parameters.StartSizeWhenDraw;
        }

        void SetScale() => Handler.ScaleTo(StartScale, Parameters.ScaleSpeed);

        #endregion
    }
}