using System;
using UnityEngine;

namespace Tools.UI.Card
{
    [RequireComponent(typeof(UiCardSelector))]
    public class UiCardSorter : MonoBehaviour
    {
        private const int OffsetZ = -1;
        private UiCardSelector CardSelector { get; set; }

        private void Awake()
        {
            CardSelector = GetComponent<UiCardSelector>();
            CardSelector.OnHandChanged += Sort;
        }

        public void Sort(UiCardHand[] cards)
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
    }
}