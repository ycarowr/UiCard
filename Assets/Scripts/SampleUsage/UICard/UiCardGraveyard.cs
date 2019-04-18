using System;
using UnityEngine;

namespace Tools.UI.Card
{
    //------------------------------------------------------------------------------------------------------------------

    /// <summary>
    ///     Card Graveyard holds a register of UI cards that were played by the player.
    /// </summary>
    [RequireComponent(typeof(UiCardSelector))]
    public class UiCardGraveyard : UiCardPile
    {
        [SerializeField] [Tooltip("World point where the graveyard is positioned")]
        private Transform graveyardPosition;
        
        //--------------------------------------------------------------------------------------------------------------
        
        private UiCardSelector CardSelector { get; set; }


        //--------------------------------------------------------------------------------------------------------------
        
        #region Unitycallbacks
        
        protected override void Awake()
        {
            base.Awake();
            CardSelector = GetComponent<UiCardSelector>();
            CardSelector.OnCardPlayed += AddCard;
        }
        
        #endregion
        
        //--------------------------------------------------------------------------------------------------------------

        #region Operations

        /// <summary>
        ///     Add an UI Card to the player hand.
        /// </summary>
        /// <param name="card"></param>
        public override void AddCard(IUiCard card)
        {
            if (card == null)
                throw new ArgumentNullException("Null is not a valid argument.");

            Cards.Add(card);
            card.transform.SetParent(graveyardPosition);
            card.Discard();
            NotifyPileChange();
        }


        /// <summary>
        ///     Remove an UI Card from the player graveyard.
        /// </summary>
        /// <param name="card"></param>
        public override void RemoveCard(IUiCard card)
        {
            if (card == null)
                throw new ArgumentNullException("Null is not a valid argument.");

            Cards.Remove(card);
            NotifyPileChange();
        }

        #endregion
        
        //--------------------------------------------------------------------------------------------------------------
    }
}