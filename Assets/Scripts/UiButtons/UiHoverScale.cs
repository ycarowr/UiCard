using UnityEngine.UI;

namespace Tools.UI.Card
{
    public class UiHoverScale : UiButtonsCardTest
    {
        private Slider MySlider { get; set; }

        protected override void Awake()
        {
            base.Awake();
            MySlider = GetComponent<Slider>();

            MySlider.onValueChanged.AddListener(SetScalePercent);
        }

        protected override void CardSelector_OnHandChanged(UiCardHand[] cards)
        {
            MySlider.onValueChanged.Invoke(MySlider.value);
        }


        private void SetScalePercent(float percent)
        {
            foreach (var card in CardSelector.Cards)
            {
                var hoverState = card.GetComponent<UiCardHover>();
                hoverState.SetHoverScale(percent);
            }
        }
    }
}