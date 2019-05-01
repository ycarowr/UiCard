using UnityEngine;

namespace Tools.UI.Card
{
    /// <summary>
    ///     Main components of an UI card.
    /// </summary>
    public interface IUiCardComponents
    {
        Camera MainCamera { get; }
        SpriteRenderer[] Renderers { get; }
        SpriteRenderer Renderer { get; }
        Collider Collider { get; }
        Rigidbody Rigidbody { get; }
        IMouseInput Input { get; }
        MonoBehaviour MonoBehavior { get; }
        GameObject gameObject { get; }
        Transform transform { get; }
    }
}