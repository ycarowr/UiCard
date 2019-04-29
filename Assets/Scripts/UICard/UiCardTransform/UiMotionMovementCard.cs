using UnityEngine;

namespace Tools.UI.Card
{
    public class UiMotionMovementCard : UiMotionBaseCard
    {
        public UiMotionMovementCard(IUiCard handler) : base(handler)
        {
        }

        private bool WithZ { get; set; }

        public override void Execute(Vector3 position, float speed, float delay, bool withZ)
        {
            WithZ = withZ;
            base.Execute(position, speed, delay, withZ);
        }

        protected override void OnMotionEnds()
        {
            WithZ = false;
            IsOperating = false;
            var target = Target;
            target.z = Handler.transform.position.z;
            Handler.transform.position = target;
            base.OnMotionEnds();
        }

        protected override void KeepMotion()
        {
            var current = Handler.transform.position;
            var amount = Speed * Time.deltaTime;
            var delta = Vector3.Lerp(current, Target, amount);
            if (!WithZ)
                delta.z = Handler.transform.position.z;
            Handler.transform.position = delta;
        }

        protected override bool CheckFinalState()
        {
            var distance = Target - Handler.transform.position;
            if (!WithZ)
                distance.z = 0;
            return distance.magnitude <= Threshold;
        }
    }
}