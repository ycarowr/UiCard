using UnityEngine;
using UnityEngine.UI;

namespace Tools.UI.Card
{
    public class UiHandNotify : MonoBehaviour
    {
        private void Awake()
        {
            var cardHand = FindObjectOfType<UiCardHand>();
            var button = GetComponentInChildren<Button>();
            var slider = GetComponentInChildren<Slider>();

            if (button)
                button.onClick.AddListener(cardHand.NotifyPileChange);

            if (slider)
                slider.onValueChanged.AddListener(afloat => { cardHand.NotifyPileChange(); });
        }
    }
}