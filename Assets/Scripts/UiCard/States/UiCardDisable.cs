using UnityEngine;

namespace Tools.UI.Card
{
    /// <summary>
    ///     This state disables the collider of the card.
    /// </summary>
    public class UiCardDisable : UiBaseCardState
    {
        [SerializeField] [Range(0.1f, 1)] private float disabledAlpha;

        public override void OnEnterState()
        {
            Disable();
        }

        /// <summary>
        ///     Disabled state of the card.
        /// </summary>
        private void Disable()
        {
            MyCollider.enabled = false;
            MyRigidbody.Sleep();
            MakeRenderNormal();
            foreach (var renderer in MyRenderers)
            {
                var myColor = renderer.color;
                myColor.a = disabledAlpha;
                renderer.color = myColor;
            }
        }

        public void SetDisabledAlpha(float alpha)
        {
            disabledAlpha = alpha;
        }
    }
}