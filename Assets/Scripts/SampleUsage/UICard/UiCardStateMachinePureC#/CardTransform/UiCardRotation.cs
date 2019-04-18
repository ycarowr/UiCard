using UnityEngine;

namespace Tools.UI.Card
{
    public class UiCardRotation : UiCardBaseTransform
    {
        public UiCardRotation(IUiCard handler) : base(handler)
        {
        }

        //--------------------------------------------------------------------------------------------------------------

        public override void Execute(Vector3 euler, float speed)
        {
            IsOperating = true;
            Target = euler;
        }

        //--------------------------------------------------------------------------------------------------------------

        protected override void Finish()
        {
            Handler.transform.eulerAngles = Target;
            IsOperating = false;
            OnArrive?.Invoke();
        }

        protected override void KeepExecution()
        {
            var current = Handler.transform.rotation;
            var amount = Speed * Time.deltaTime;
            var rotation = Quaternion.Euler(Target);
            Handler.transform.rotation = Quaternion.RotateTowards(current, rotation, amount);
        }

        protected override bool CheckFinalState()
        {
            var distance = Target - Handler.transform.eulerAngles;
            return (distance.magnitude <= Threshold || (int)distance.magnitude == 360);
        }

        //--------------------------------------------------------------------------------------------------------------
    }
}