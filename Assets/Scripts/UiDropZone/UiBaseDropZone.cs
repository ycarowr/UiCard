using UnityEngine;
using UnityEngine.EventSystems;

namespace Tools.UI.Card
{
    /// <summary>
    ///     Base zones where the user can drop a UI Card.
    /// </summary>
    [RequireComponent(typeof(IMouseInput))]
    public abstract class UiBaseDropZone : MonoBehaviour
    {
        protected UiCardSelector CardSelector { get; set; }
        protected IMouseInput Input { get; set; }

        protected virtual void Awake()
        {
            CardSelector = GetComponentInParent<UiCardSelector>();
            Input = GetComponent<IMouseInput>();
            Input.OnPointerUp += OnPointerUp;
        }

        protected virtual void OnPointerUp(PointerEventData eventData)
        {
        }
    }
}