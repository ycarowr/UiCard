using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.UI.Card
{
    //------------------------------------------------------------------------------------------------------------------

    /// <summary>
    ///     Pile of cards. Add or Remove cards and be notified when changes happen.
    /// </summary>
    public abstract class UiCardPile : MonoBehaviour, IUiCardPile
    {
        //--------------------------------------------------------------------------------------------------------------

        #region Properties

        /// <summary>
        ///     List with all cards.
        /// </summary>
        public List<IUiCard> Cards { get; private set; }

        /// <summary>
        ///     Event raised when add or remove a card.
        /// </summary>
        private event Action<IUiCard[]> onPileChanged = hand => { };

        public Action<IUiCard[]> OnPileChanged
        {
            get => onPileChanged;
            set => onPileChanged = value;
        }

        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region Operations

        /// <summary>
        ///     Add a card to the pile.
        /// </summary>
        /// <param name="card"></param>
        public virtual void AddCard(IUiCard card)
        {
            if (card == null)
                throw new ArgumentNullException("Null is not a valid argument.");

            Cards.Add(card);
            card.transform.SetParent(transform);
            NotifyPileChange();

            card.Draw();
        }


        /// <summary>
        ///     Remove a card from the pile.
        /// </summary>
        /// <param name="card"></param>
        public virtual void RemoveCard(IUiCard card)
        {
            if (card == null)
                throw new ArgumentNullException("Null is not a valid argument.");

            Cards.Remove(card);

            NotifyPileChange();
        }

        /// <summary>
        ///     Clear all the pile.
        /// </summary>
        [Button]
        protected virtual void Clear()
        {
            var childCards = GetComponentsInChildren<IUiCard>();
            foreach (var uiCardHand in childCards)
                Destroy(uiCardHand.gameObject);

            Cards.Clear();
        }

        /// <summary>
        ///     Notify all listeners of this pile that some change has been made.
        /// </summary>
        [Button]
        public void NotifyPileChange()
        {
            onPileChanged?.Invoke(Cards.ToArray());
        }

        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region Unitycallbacks

        protected virtual void Awake()
        {
            //initialize register
            Cards = new List<IUiCard>();

            Clear();
        }

        #endregion

        //--------------------------------------------------------------------------------------------------------------
    }
}