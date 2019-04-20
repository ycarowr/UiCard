using System;
using System.Collections;
using Patterns.StateMachine;
using UnityEngine;

namespace Tools.UI.Card
{
    public class UiCardMovement : UiCardBaseTransform
    {        
        public UiCardMovement(IUiCard handler):base(handler)
        {
        }
        
        
        //--------------------------------------------------------------------------------------------------------------
        
        public override void Execute(Vector3 position, float speed, float delay)
        {
            Speed = speed;
            Target = position;

            if (delay == 0)
                IsOperating = true;
            else
                Handler.MonoBehavior.StartCoroutine(AllowScale(delay));
        }

        //--------------------------------------------------------------------------------------------------------------

        protected override void Finish()
        {
            IsOperating = false;
            Handler.transform.position = Target;
            OnArrive?.Invoke();
        }

        protected override void KeepExecution()
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

        //--------------------------------------------------------------------------------------------------------------
    }
}