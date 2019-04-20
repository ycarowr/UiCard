using Patterns.StateMachine;
using UnityEngine;

namespace Tools.UI.Card
{
    /// <summary>
    ///     State when a cards has been discarded.
    /// </summary>
    public class UiCardDiscard : UiBaseCardState
    {
        private Vector3 StartScale { get; set; }
        
        public UiCardDiscard(IUiCard handler, BaseStateMachine fsm, UiCardParameters parameters) : base(handler, fsm, parameters)
        {
        }
        
        //--------------------------------------------------------------------------------------------------------------
        
        #region Operations

        public override void OnEnterState()
        {
            Disable();
            SetScale();
            SetRotation();
        }

        private void SetScale()
        {
            var finalScale = Handler.transform.localScale * Parameters.DiscardedSize;
            Handler.ScaleTo(finalScale, Parameters.ScaleSpeed);            
        }

        private void SetRotation()
        {
            Handler.RotateTo(Vector3.zero, Parameters.RotationSpeed);
        }

        #endregion
    }
}