using System;
using UnityEngine;

namespace Tools.UI.Card
{
    [RequireComponent(typeof(UiCardHand))]
    public class UiCardHandSorter : MonoBehaviour
    {
        //--------------------------------------------------------------------------------------------------------------

        private const int OffsetZ = -1;
        private IUiCardPile CardHand { get; set; }

        //--------------------------------------------------------------------------------------------------------------

        private void Awake()
        {
            CardHand = GetComponent<UiCardHand>();
            CardHand.OnPileChanged += Sort;
        }

        //--------------------------------------------------------------------------------------------------------------

        public void Sort(IUiCard[] cards)
        {
            if (cards == null)
                throw new ArgumentException("Can't sort a card list null");

            var layerZ = 0;
            foreach (var card in cards)
            {
                var localCardPosition = card.transform.localPosition;
                localCardPosition.z = layerZ;
                card.transform.localPosition = localCardPosition;
                layerZ += OffsetZ;
            }
        }

        //--------------------------------------------------------------------------------------------------------------
    }
}