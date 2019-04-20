using System.Collections;
using UnityEngine;

namespace Tools.UI.Card
{
    public class UiCardScale : UiCardBaseTransform
    {
        //--------------------------------------------------------------------------------------------------------------

        public UiCardScale(IUiCard handler) : base(handler)
        {
        }

        //--------------------------------------------------------------------------------------------------------------

        protected override bool CheckFinalState()
        {
            var delta = Target - Handler.transform.localScale;
            return delta.magnitude <= Threshold;
        }

        public override void Execute(Vector3 scale, float speed, float delay)
        {
            Speed = speed;
            Target = scale;
            if (delay == 0)
                IsOperating = true;
            else
                Handler.MonoBehavior.StartCoroutine(AllowScale(delay));
        }

        protected override void OnMotionEnds()
        {
            Handler.transform.localScale = Target;
            IsOperating = false;
        }

        protected override void KeepMotion()
        {
            var current = Handler.transform.localScale;
            var amount = Time.deltaTime * Speed;
            Handler.transform.localScale = Vector3.Lerp(current, Target, amount);
        }

        private IEnumerator AllowScale(float delay)
        {
            yield return new WaitForSeconds(delay);
            IsOperating = true;
        }
    }
}