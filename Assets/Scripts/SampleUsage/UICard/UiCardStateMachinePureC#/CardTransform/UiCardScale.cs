using System.Collections;
using UnityEngine;

namespace Tools.UI.Card
{
    public class UiCardScale : UiCardBaseTransform
    {  
        //--------------------------------------------------------------------------------------------------------------
        
        public UiCardScale(IUiCard handler) : base (handler)
        {
        }

        //--------------------------------------------------------------------------------------------------------------

        protected override bool CheckFinalState()
        {
            var delta = Target - Handler.transform.localScale;
            return delta.magnitude > Threshold;
        }
                
        public override void Execute(Vector3 scale, float speed)
        {
            Speed = speed;
            Target = scale;
            Handler.MonoBehavior.StartCoroutine(AllowScale(0.01f));
        }
        
        //--------------------------------------------------------------------------------------------------------------
        
        #region private
        
        protected override void Finish()
        {
            Handler.transform.localScale = Target;
            IsOperating = false;
        }

        protected override void KeepExecution()
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
        
        #endregion
        
        //--------------------------------------------------------------------------------------------------------------
    }
}