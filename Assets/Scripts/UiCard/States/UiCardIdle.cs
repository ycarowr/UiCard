using UnityEngine.EventSystems;

namespace Tools.UI.Card
{
    /// <summary>
    ///     UI Card Idle behavior.
    /// </summary>
    public class UiCardIdle : UiBaseCardState
    {
        public override void OnAwake()
        {
            base.OnAwake();
            MyInput.OnPointerDown += OnPointerDown;
            MyInput.OnPointerEnter += OnPointerEnter;
        }

        public override void OnEnterState()
        {
            Enable();
            MakeRenderNormal();
        }

        private void OnPointerEnter(PointerEventData obj)
        {
            if (Fsm.IsCurrent(this))
                Fsm.PushState<UiCardHover>();
        }

        private void OnPointerDown(PointerEventData eventData)
        {
            if (Fsm.IsCurrent(this))
                Fsm.SelectThisCard();
        }

        private void OnDestroy()
        {
            MyInput.OnPointerDown -= OnPointerDown;
        }
    }
}