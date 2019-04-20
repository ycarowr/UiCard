using UnityEngine.EventSystems;

namespace Tools.UI.Card
{
    /// <summary>
    ///     GameController hand zone.
    /// </summary>
    public class UiZoneHand : UiBaseDropZone
    {
        protected override void OnPointerUp(PointerEventData eventData)
        {
            CardHand?.Unselect();
        }
    }
}