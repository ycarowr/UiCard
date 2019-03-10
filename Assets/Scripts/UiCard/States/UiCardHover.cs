using UnityEngine;
using UnityEngine.EventSystems;

namespace Tools.UI.Card
{
    public class UiCardHover : UiBaseCardState
    {
        [SerializeField] [Tooltip("How much the card will go upwards when hovered.")] [Range(0, 2)]
        private float hoverYOffset;

        [SerializeField] [Tooltip("Whether the hovered card keep its rotation.")]
        private bool keepRotation;

        [SerializeField] [Tooltip("How much a hovered card scales.")] [Range(0.9f, 2f)]
        private float percentScale;

        private Vector3 StartPosition { get; set; }
        private Quaternion StartRotation { get; set; }
        private Vector3 StartScale { get; set; }


        public override void OnEnterState()
        {
            MakeRenderFirst();

            MyInput.OnPointerExit += OnPointerExit;
            MyInput.OnPointerDown += OnPointerDown;

            //cache old values
            StartPosition = MyTransform.position;
            StartRotation = MyTransform.rotation;
            StartScale = MyTransform.localScale;

            MyTransform.localPosition += new Vector3(0, hoverYOffset, 0);
            MyTransform.localScale *= percentScale;

            if (!keepRotation)
                MyTransform.localRotation = Quaternion.identity;
        }

        public override void OnExitState()
        {
            MyInput.OnPointerExit -= OnPointerExit;
            MyInput.OnPointerDown -= OnPointerDown;

            //reset position and rotation
            MyTransform.rotation = StartRotation;
            MyTransform.position = StartPosition;
            MyTransform.localScale = StartScale;
        }

        private void OnPointerExit(PointerEventData obj)
        {
            if (Fsm.IsCurrent(this))
                Fsm.PushState<UiCardIdle>();
        }

        private void OnPointerDown(PointerEventData eventData)
        {
            if (Fsm.IsCurrent(this))
                Fsm.SelectThisCard();
        }

        public void OnDestroy()
        {
            MyInput.OnPointerExit -= OnPointerExit;
            MyInput.OnPointerDown -= OnPointerDown;
        }


        #region Public 

        public void SetYOffset(float offset)
        {
            hoverYOffset = offset;
        }

        public void SetKeepRotation(bool keep)
        {
            keepRotation = keep;
        }

        public void SetHoverScale(float percent)
        {
            percentScale = percent;
        }

        #endregion
    }
}