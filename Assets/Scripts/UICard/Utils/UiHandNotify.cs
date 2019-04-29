using UnityEngine;
using UnityEngine.UI;

namespace Tools.UI.Card
{
    /// <summary>
    ///     Notifies the hand when the UI component is changed.
    /// </summary>
    public class UiHandNotify : MonoBehaviour
    {
        private void Awake()
        {
            var cardHands = FindObjectsOfType<UiCardHand>();
            var button = GetComponentInChildren<Button>();
            var slider = GetComponentInChildren<Slider>();

            foreach (var hand in cardHands)
            {
                if (button)
                    button.onClick.AddListener(hand.NotifyPileChange);

                if (slider)
                    slider.onValueChanged.AddListener(afloat => { hand.NotifyPileChange(); });
            }
        }
    }
}