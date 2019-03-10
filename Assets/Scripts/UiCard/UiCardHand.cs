using Patterns;

namespace Tools.UI.Card
{
    /// <summary>
    ///     State Machine that holds all states of a UI Card.
    /// </summary>
    public class UiCardHand : StateMachineMB<UiCardHand>
    {
        private UiCardSelector CardSelector { get; set; }

        protected override void Start()
        {
            base.Start();
            CardSelector = GetComponentInParent<UiCardSelector>();
        }

        /// <summary>
        ///     Notifies the card selector that a card has been selected.
        /// </summary>
        public void SelectThisCard()
        {
            CardSelector.SelectCard(this);
        }

        /// <summary>
        ///     Notifies the card selector that a card has been unselected.
        /// </summary>
        public void UnselectThisCard()
        {
            CardSelector.UnselectCard(this);
        }

        /// <summary>
        ///     Notifies the card selector that a card has been played.
        /// </summary>
        public void PlayThisCard()
        {
            CardSelector.PlayCard(this);
        }
    }
}