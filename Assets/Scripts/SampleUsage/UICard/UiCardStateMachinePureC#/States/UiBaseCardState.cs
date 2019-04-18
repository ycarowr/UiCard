using Patterns.StateMachine;

namespace Tools.UI.Card
{
    /// <summary>
    ///     Base UI Card State.
    /// </summary>
    public abstract class UiBaseCardState : IState
    {
        private const int LayerToRenderNormal = 0;
        private const int LayerToRenderFirst = 1;        
        protected IUiCard Handler { get; }
        protected UiCardParameters Parameters { get; }
        protected BaseStateMachine Fsm { get; }
        public bool IsInitialized { get; }

        //--------------------------------------------------------------------------------------------------------------

        #region Constructor

        protected UiBaseCardState(IUiCard handler, BaseStateMachine fsm,  UiCardParameters parameters)
        {
            Fsm = fsm;
            Handler = handler;
            Parameters = parameters;
            IsInitialized = true;
        }
        
        #endregion

        //--------------------------------------------------------------------------------------------------------------
        
        #region Operations

        /// <summary>
        ///     Renders the textures in the first layer. Each card state is responsible to handle its own layer activity.
        /// </summary>
        protected virtual void MakeRenderFirst()
        {
            for (var i = 0; i < Handler.Renderers.Length; i++)
                Handler.Renderers[i].sortingOrder = LayerToRenderFirst;
        }


        /// <summary>
        ///     Renders the textures in the regular layer. Each card state is responsible to handle its own layer activity.
        /// </summary>
        protected virtual void MakeRenderNormal()
        {
            for (var i = 0; i < Handler.Renderers.Length; i++)
                if(Handler.Renderers[i])
                    Handler.Renderers[i].sortingOrder = LayerToRenderNormal;
        }

        protected void Enable()
        {
            if(Handler.Collider)
                EnableCollision();
            if(Handler.Rigidbody)
                Handler.Rigidbody.Sleep();
            
            MakeRenderNormal();
            NormalColor();
        }

        protected virtual void Disable()
        {
            DisableCollision();
            Handler.Rigidbody.Sleep();
            MakeRenderNormal();
            foreach (var renderer in Handler.Renderers)
            {
                var myColor = renderer.color;
                myColor.a = Parameters.DisabledAlpha;
                renderer.color = myColor;
            }
        }

        protected void DisableCollision()
        {
            Handler.Collider.enabled = false;
        }

        protected void EnableCollision()
        {
            Handler.Collider.enabled = true;
        }

        protected void NormalColor()
        {
            foreach (var renderer in Handler.Renderers)
            {
                if (renderer)
                {
                    var myColor = renderer.color;
                    myColor.a = 1;
                    renderer.color = myColor;
                }
            }
        }
        
        #endregion
        
        //--------------------------------------------------------------------------------------------------------------

        #region FSM

        void IState.OnInitialize()
        {
        }

        public virtual void OnEnterState()
        {
        }

        public virtual void OnExitState()
        {
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void OnNextState(IState next)
        {
        }

        public virtual void OnClear()
        {
        }

        #endregion
        
        //--------------------------------------------------------------------------------------------------------------
      
    }
}