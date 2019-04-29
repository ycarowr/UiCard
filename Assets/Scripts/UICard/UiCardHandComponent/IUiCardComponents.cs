using UnityEngine;

namespace Tools.UI.Card
{
    /// <summary>
    ///     Interface for the used UI Components.
    ///     TODO: To split it in smaller interfaces.
    /// </summary>
    public interface IUiCardComponents
    {
        UiCardParameters CardConfigsParameters { get; }
        Camera MainCamera { get; }
        IUiCardHand Hand { get; }
        SpriteRenderer[] Renderers { get; }
        SpriteRenderer MyRenderer { get; }
        Collider Collider { get; }
        Rigidbody Rigidbody { get; }
        IMouseInput Input { get; }
        GameObject gameObject { get; }
        Transform transform { get; }
        MonoBehaviour MonoBehavior { get; }
    }
}