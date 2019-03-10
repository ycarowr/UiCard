using UnityEngine.EventSystems;

namespace Tools.UI.Card
{
    /// <summary>
    ///     Player hand zone.
    /// </summary>
    public class UiZoneHand : UiBaseDropZone
    {
        protected override void OnPointerUp(PointerEventData eventData)
        {
            var selectedCard = CardSelector.SelectedCard;

            if (selectedCard)
                selectedCard.UnselectThisCard();
        }
    }
}