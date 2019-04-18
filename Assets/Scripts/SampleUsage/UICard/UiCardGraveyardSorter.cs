using System;
using System.Linq;
using UnityEngine;

namespace Tools.UI.Card
{
    [RequireComponent(typeof(UiCardGraveyard))]
    public class UiCardGraveyardSorter : MonoBehaviour
    {
        [SerializeField] private UiCardParameters parameters;
        [SerializeField] [Tooltip("World point where the graveyard is positioned")]
        private Transform graveyardPosition;
        //--------------------------------------------------------------------------------------------------------------

        private IUiCardPile CardGraveyard { get; set; }
        
        //--------------------------------------------------------------------------------------------------------------

        private void Awake()
        {
            CardGraveyard = GetComponent<UiCardGraveyard>();
            CardGraveyard.OnPileChanged += Sort;
        }
        
        //--------------------------------------------------------------------------------------------------------------

        public void Sort(IUiCard[] cards)
        {
            if (cards == null)
                throw new ArgumentException("Can't sort a card list null");

            var lastPos = cards.Length - 1;
            var lastCard = cards[lastPos];
            var gravPos = graveyardPosition.position + new Vector3(0, 0, -5);
            var backGravPos = graveyardPosition.position;

            //move last
            lastCard.MoveTo(gravPos, parameters.MovementSpeed);

            //move others
            for (int i = 0; i < cards.Length -1; i++)
            {
                var card = cards[i];
                card.MoveTo(backGravPos, parameters.MovementSpeed);
            }            
        }
        
        //--------------------------------------------------------------------------------------------------------------
    }
}