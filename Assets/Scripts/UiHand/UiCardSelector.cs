using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.UI.Card
{
    /// <summary>
    ///     Card Selector holds a register of UI cards of a player.
    /// </summary>
    public class UiCardSelector : MonoBehaviour
    {
        //UI cards of the player
        public List<UiCardHand> Cards { get; private set; }

        //UI Card currently selected by the player
        public UiCardHand SelectedCard { get; private set; }

        /// <summary>
        ///     Event raised when add or remove a card.
        /// </summary>
        public event Action<UiCardHand[]> OnHandChanged = hand => { };

        /// <summary>
        ///     Event raised when a card is selected.
        /// </summary>
        public event Action<UiCardHand> OnCardSelected = card => { };


        private void Awake()
        {
            //initialize register
            Cards = new List<UiCardHand>();

            Clear();
        }

        #region Operations

        /// <summary>
        ///     Select UI Card implementation.
        /// </summary>
        /// <param name="card"></param>
        public void SelectCard(UiCardHand card)
        {
            SelectedCard = card ?? throw new ArgumentNullException("Null is not a valid argument.");

            //disable all cards
            DisableCards();

            //push drag card state for the selected card
            SelectedCard.PushState<UiCardDrag>();

            NotifyCardSelected();
        }

        /// <summary>
        ///     Play UI Card implementation.
        /// </summary>
        /// <param name="card"></param>
        public void PlayCard(UiCardHand card)
        {
            if (card == null)
                throw new ArgumentNullException("Null is not a valid argument.");

            Destroy(card.gameObject);
            RemoveCard(card);
            EnableCards();
        }

        /// <summary>
        ///     Unselect UI Card implementation.
        /// </summary>
        /// <param name="card"></param>
        public void UnselectCard(UiCardHand card)
        {
            if (card == null)
                throw new ArgumentNullException("Null is not a valid argument.");

            EnableCards();
            NotifyHandChange();
        }

        /// <summary>
        ///     Add an UI Card to the player hand.
        /// </summary>
        /// <param name="card"></param>
        public void AddCard(UiCardHand card)
        {
            if (card == null)
                throw new ArgumentNullException("Null is not a valid argument.");

            Cards.Add(card);
            card.transform.SetParent(transform);
            card.PushState<UiCardIdle>();
            NotifyHandChange();
        }


        /// <summary>
        ///     Remove an UI Card to the player hand.
        /// </summary>
        /// <param name="card"></param>
        public void RemoveCard(UiCardHand card)
        {
            if (card == null)
                throw new ArgumentNullException("Null is not a valid argument.");

            Cards.Remove(card);

            NotifyHandChange();
        }

        #endregion


        #region Extra

        /// <summary>
        ///     Disable input for all cards.
        /// </summary>
        public void DisableCards()
        {
            foreach (var otherCard in Cards)
                otherCard.PushState<UiCardDisable>();
        }

        /// <summary>
        ///     Enable input for all cards.
        /// </summary>
        public void EnableCards()
        {
            foreach (var otherCard in Cards)
                otherCard.PushState<UiCardIdle>();
        }

        [Button]
        private void Clear()
        {
            var childCards = GetComponentsInChildren<UiCardHand>();
            foreach (var uiCardHand in childCards)
                Destroy(uiCardHand.gameObject);

            Cards.Clear();
        }

        [Button]
        private void NotifyHandChange()
        {
            OnHandChanged.Invoke(Cards.ToArray());
        }

        [Button]
        private void NotifyCardSelected()
        {
            OnCardSelected.Invoke(SelectedCard);
        }

        #endregion
    }
}