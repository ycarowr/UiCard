using UnityEngine;

namespace Tools.UI.Card
{
    public interface IUiCardComponents
    {
        UiCardParameters CardConfigsParameters { get; }
        Camera MainCamera { get; }
        IUiCardSelector CardSelector { get; }
        SpriteRenderer[] Renderers { get; }
        SpriteRenderer MyRenderer { get; }
        Collider Collider { get; }
        Rigidbody Rigidbody { get; }
        Transform Transform { get; }
        IMouseInput Input { get; }
        GameObject gameObject { get; }
        Transform transform { get; }
        UiCardMovement UiCardMovement { get; }
        MonoBehaviour MonoBehavior { get; }
    }
}