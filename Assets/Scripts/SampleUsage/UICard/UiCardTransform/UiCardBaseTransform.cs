using System;
using System.Collections;
using Patterns.StateMachine;
using UnityEngine;

namespace Tools.UI.Card
{
    public abstract class UiCardBaseTransform
    {
        public Action OnArrive = () => { };

        public bool IsOperating { get; protected set; }
        protected virtual float Threshold => 0.1f;
        protected Vector3 Target { get; set; }
        protected IUiCard Handler { get; }
        protected float Speed { get; set; }

        //--------------------------------------------------------------------------------------------------------------
        
        protected UiCardBaseTransform(IUiCard handler)
        {
            Handler = handler;
        }
        
        //--------------------------------------------------------------------------------------------------------------
        
        public void Update()
        {
            if (!IsOperating)
                return;
            
            if (CheckFinalState())
                Finish();
            else
                KeepExecution();
        }


        protected abstract bool CheckFinalState();
        protected abstract void Finish();
        protected abstract void KeepExecution();
        public abstract void Execute(Vector3 vector, float speed, float delay = 0);
        public virtual void StopMotion()
        {
            IsOperating = false;
        }
    }
}