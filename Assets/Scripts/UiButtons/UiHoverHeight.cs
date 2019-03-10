using UnityEngine.UI;

namespace Tools.UI.Card
{
    public class UiHoverHeight : UiButtonsCardTest
    {
        private Slider MySlider { get; set; }

        protected override void Awake()
        {
            base.Awake();
            MySlider = GetComponent<Slider>();

            MySlider.onValueChanged.AddListener(SetHoverHeight);
        }

        protected override void CardSelector_OnHandChanged(UiCardHand[] cards)
        {
            MySlider.onValueChanged.Invoke(MySlider.value);
        }

        private void SetHoverHeight(float height)
        {
            foreach (var card in CardSelector.Cards)
            {
                var hoverState = card.GetComponent<UiCardHover>();
                hoverState.SetYOffset(height);
            }
        }
    }
}