using UnityEngine;
using UnityEngine.Assertions;

namespace Tools.UI.Card
{
    public class UiButtonsCardTest : MonoBehaviour
    {
        [SerializeField] protected UiCardHand CardSelector;

        protected virtual void Awake()
        {
            Assert.IsNotNull(CardSelector);

            CardSelector.OnPileChanged += CardSelector_OnHandChanged;
        }

        protected virtual void CardSelector_OnHandChanged(IUiCard[] cards)
        {
        }
    }
}