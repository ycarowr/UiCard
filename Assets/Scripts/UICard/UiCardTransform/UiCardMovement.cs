using System.Collections;
using UnityEngine;

namespace Tools.UI.Card
{
    public class UiCardMovement : UiCardBaseTransform
    {
        public UiCardMovement(IUiCard handler) : base(handler)
        {
        }

        public override void Execute(Vector3 position, float speed, float delay)
        {
            Speed = speed;
            Target = position;

            if (delay == 0)
                IsOperating = true;
            else
                Handler.MonoBehavior.StartCoroutine(AllowScale(delay));
        }

        protected override void OnMotionEnds()
        {
            IsOperating = false;
            Handler.transform.position = Target;
            base.OnMotionEnds();
        }

        protected override void KeepMotion()
        {
            var current = Handler.transform.position;
            var amount = Speed * Time.deltaTime;
            Handler.transform.position = Vector3.Lerp(current, Target, amount);
        }

        protected override bool CheckFinalState()
        {
            var distance = Target - Handler.transform.position;
            return distance.magnitude <= Threshold;
        }

        private IEnumerator AllowScale(float delay)
        {
            yield return new WaitForSeconds(delay);
            IsOperating = true;
        }
    }
}