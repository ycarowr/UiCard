using UnityEngine.UI;

namespace Tools.UI.Card
{
    public class UiHoverRotation : UiButtonsCardTest
    {
        private Toggle MyToggle { get; set; }

        protected override void Awake()
        {
            base.Awake();
            MyToggle = GetComponent<Toggle>();

            MyToggle.onValueChanged.AddListener(SetKeepRotation);
        }

        protected override void CardSelector_OnHandChanged(UiCardHand[] cards)
        {
            MyToggle.onValueChanged.Invoke(MyToggle.isOn);
        }

        private void SetKeepRotation(bool keepRotation)
        {
            foreach (var card in CardSelector.Cards)
            {
                var hoverState = card.GetComponent<UiCardHover>();
                hoverState.SetKeepRotation(keepRotation);
            }
        }
    }
}