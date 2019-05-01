using System;
using UnityEngine;

namespace Tools.UI.Card
{
    [RequireComponent(typeof(UiCardGraveyard))]
    public class UiCardGraveyardSorter : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("World point where the graveyard is positioned")]
        private Transform graveyardPosition;

        [SerializeField] private UiCardParameters parameters;

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
            lastCard.MoveToWithZ(gravPos, parameters.MovementSpeed);

            //move others
            for (var i = 0; i < cards.Length - 1; i++)
            {
                var card = cards[i];
                card.MoveToWithZ(backGravPos, parameters.MovementSpeed);
            }
        }

        //--------------------------------------------------------------------------------------------------------------
    }
}