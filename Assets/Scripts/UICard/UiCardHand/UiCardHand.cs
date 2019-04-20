using System;

namespace Tools.UI.Card
{
    //------------------------------------------------------------------------------------------------------------------

    /// <summary>
    ///     Card Selector holds a register of UI cards of a player.
    /// </summary>
    public class UiCardHand : UiCardPile, IUiCardHand
    {
        //--------------------------------------------------------------------------------------------------------------

        #region Properties

        //UI Card currently selected by the player
        public IUiCard SelectedCard { get; private set; }

        /// <summary>
        ///     Event raised when a card is selected.
        /// </summary>
        public event Action<IUiCard> OnCardSelected = card => { };

        /// <summary>
        ///     Event raised when a card is played.
        /// </summary>
        public event Action<IUiCard> OnCardPlayed = card => { };

        Action<IUiCard> IUiCardHand.OnCardPlayed { get => OnCardPlayed; set => OnCardPlayed = value; }

        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region Operations

        /// <summary>
        ///     Select UI Card implementation.
        /// </summary>
        /// <param name="card"></param>
        public void SelectCard(IUiCard card)
        {
            SelectedCard = card ?? throw new ArgumentNullException("Null is not a valid argument.");

            //disable all cards
            DisableCards();
            NotifyCardSelected();
        }

        /// <summary>
        ///     Play card selected implementation.
        /// </summary>
        /// <param name="card"></param>
        public void PlaySelected()
        {
            if (SelectedCard == null)
                return;

            PlayCard(SelectedCard);
        }

        /// <summary>
        ///     Play UI Card implementation.
        /// </summary>
        /// <param name="card"></param>
        public void PlayCard(IUiCard card)
        {
            if (card == null)
                throw new ArgumentNullException("Null is not a valid argument.");

            SelectedCard = null;
            RemoveCard(card);
            OnCardPlayed?.Invoke(card);
            EnableCards();
            NotifyPileChange();
        }

        /// <summary>
        ///     Unselect UI Card implementation.
        /// </summary>
        /// <param name="card"></param>
        public void UnselectCard(IUiCard card)
        {
            if (card == null)
                return;

            SelectedCard = null;
            card.Unselect();
            NotifyPileChange();
            EnableCards();
        }

        public void Unselect()
        {
            UnselectCard(SelectedCard);
        }

        /// <summary>
        ///     Disable input for all cards.
        /// </summary>
        public void DisableCards()
        {
            foreach (var otherCard in Cards)
                otherCard.Disable();
        }

        /// <summary>
        ///     Enable input for all cards.
        /// </summary>
        public void EnableCards()
        {
            foreach (var otherCard in Cards)
                otherCard.Enable();
        }

        [Button]
        private void NotifyCardSelected()
        {
            OnCardSelected?.Invoke(SelectedCard);
        }

        #endregion

        //--------------------------------------------------------------------------------------------------------------
    }
}