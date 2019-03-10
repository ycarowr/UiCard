using UnityEngine.EventSystems;

namespace Tools.UI.Card
{
    /// <summary>
    ///     Battlefield Zone.
    /// </summary>
    public class UiZoneBattleField : UiBaseDropZone
    {
        protected override void OnPointerUp(PointerEventData eventData)
        {
            var cardSelected = CardSelector.SelectedCard;

            if (cardSelected)
                cardSelected.PlayThisCard();
        }
    }
}