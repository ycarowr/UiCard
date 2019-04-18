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
        
        public override void Execute(Vector3 position, float speed)
        {
            Speed = speed;
            Target = position;
            Handler.MonoBehavior.StartCoroutine(AllowMovement(0.01f));
        }
                
        //--------------------------------------------------------------------------------------------------------------
        
        protected override void Finish()
        {
            Handler.transform.position = Target;
            IsOperating = false;
            OnArrive?.Invoke();
        }

        protected override void KeepExecution()
        {
            var current = Handler.transform.position;
            var amount = Speed * Time.deltaTime;
            Handler.transform.position = Vector3.Lerp(current, Target, amount);
        }

        private IEnumerator AllowMovement(float time)
        {
            yield return new WaitForSeconds(time);
            IsOperating = true;    
        }

        protected override bool CheckFinalState()
        {
            var distance = Target - Handler.transform.position;
            return distance.magnitude > Threshold;
        }

        //--------------------------------------------------------------------------------------------------------------
    }
}