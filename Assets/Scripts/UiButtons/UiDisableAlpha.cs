using UnityEngine.UI;

namespace Tools.UI.Card
{
    public class UiDisableAlpha : UiButtonsCardTest
    {
        private Slider MySlider { get; set; }

        protected override void Awake()
        {
            base.Awake();
            MySlider = GetComponent<Slider>();

            MySlider.onValueChanged.AddListener(SetDisableAlpha);
        }

        protected override void CardSelector_OnHandChanged(UiCardHand[] cards)
        {
            MySlider.onValueChanged.Invoke(MySlider.value);
        }

        public void SetDisableAlpha(float alpha)
        {
            foreach (var card in CardSelector.Cards)
            {
                var disableState = card.GetComponent<UiCardDisable>();
                disableState.SetDisabledAlpha(alpha);
            }
        }
    }
}