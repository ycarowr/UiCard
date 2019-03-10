using Patterns;
using UnityEngine;

namespace Tools.UI.Card
{
    /// <summary>
    ///     Base UI Card State.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(IMouseInput))]
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class UiBaseCardState : StateMB<UiCardHand>
    {
        protected const int LayerToRenderNormal = 0;
        protected const int LayerToRenderFirst = 1;
        protected SpriteRenderer[] MyRenderers { get; set; }
        protected Collider MyCollider { get; set; }
        protected Rigidbody MyRigidbody { get; set; }
        protected Transform MyTransform { get; set; }
        protected IMouseInput MyInput { get; set; }

        public override void OnAwake()
        {
            MyRenderers = GetComponentsInChildren<SpriteRenderer>();
            MyCollider = GetComponent<Collider>();
            MyRigidbody = GetComponent<Rigidbody>();
            MyTransform = GetComponent<Transform>();
            MyInput = GetComponent<IMouseInput>();
        }

        /// <summary>
        ///     Renders the textures in the first layer. Each card state is responsible to handle its own layer activity.
        /// </summary>
        protected virtual void MakeRenderFirst()
        {
            for (var i = 0; i < MyRenderers.Length; i++)
                MyRenderers[i].sortingOrder = LayerToRenderFirst;
        }


        /// <summary>
        ///     Renders the textures in the regular layer. Each card state is responsible to handle its own layer activity.
        /// </summary>
        protected virtual void MakeRenderNormal()
        {
            for (var i = 0; i < MyRenderers.Length; i++)
                MyRenderers[i].sortingOrder = LayerToRenderNormal;
        }

        protected void Enable()
        {
            MyCollider.enabled = true;
            MyRigidbody.Sleep();
            MakeRenderNormal();
            NormalColor();
        }

        protected void NormalColor()
        {
            foreach (var renderer in MyRenderers)
            {
                var myColor = renderer.color;
                myColor.a = 1;
                renderer.color = myColor;
            }
        }
    }
}