using Extensions;
using UnityEngine;

namespace Tools.UI.Card
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(IMouseInput))]
    public class UiCardHandComponent : MonoBehaviour, IUiCard
    {
        //--------------------------------------------------------------------------------------------------------------

        #region Components

        SpriteRenderer[] IUiCardComponents.Renderers => MyRenderers;
        SpriteRenderer IUiCardComponents.MyRenderer => MyRenderer;
        Collider IUiCardComponents.Collider => MyCollider;
        Rigidbody IUiCardComponents.Rigidbody => MyRigidbody;
        Transform IUiCardComponents.Transform => MyTransform;
        IMouseInput IUiCardComponents.Input => MyInput;
        IUiCardHand IUiCardComponents.CardSelector => Hand;

        #endregion

        #region Transform

        public UiCardMovement UiCardMovement { get; private set; }
        public UiCardRotation UiCardRotation { get; private set; }
        public UiCardScale UiCardScale { get; private set; }

        #endregion

        #region Properties

        public string Name => gameObject.name;
        public UiCardParameters CardConfigsParameters => cardConfigsParameters;
        [SerializeField] public UiCardParameters cardConfigsParameters;
        private UiCardHandFsm Fsm { get; set; }
        private Transform MyTransform { get; set; }
        private Collider MyCollider { get; set; }
        private SpriteRenderer[] MyRenderers { get; set; }
        private SpriteRenderer MyRenderer { get; set; }
        private Rigidbody MyRigidbody { get; set; }
        private IMouseInput MyInput { get; set; }
        private IUiCardHand Hand { get; set; }
        public MonoBehaviour MonoBehavior => this;
        public Camera MainCamera => Camera.main;
        public bool IsDragging => Fsm.IsCurrent<UiCardDrag>();
        public bool IsHovering => Fsm.IsCurrent<UiCardHover>();
        public bool IsDisabled => Fsm.IsCurrent<UiCardDisable>();
        public bool IsPlayer => transform.CloserEdge(MainCamera, Screen.width, Screen.height) == 1;

        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region Transform

        public void RotateTo(Vector3 rotation, float speed)
        {
            UiCardRotation.Execute(rotation, speed);
        }

        public void MoveTo(Vector3 position, float speed, float delay)
        {
            UiCardMovement.Execute(position, speed, delay);
        }

        public void ScaleTo(Vector3 scale, float speed, float delay)
        {
            UiCardScale.Execute(scale, speed, delay);
        }

        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region Operations

        public void Hover()
        {
            Fsm.Hover();
        }

        public void Disable()
        {
            Fsm.Disable();
        }

        public void Enable()
        {
            Fsm.Enable();
        }

        public void Select()
        {
            if (!IsPlayer)
                return;

            Hand.SelectCard(this);
            Fsm.Select();
        }

        public void Unselect()
        { 
            Fsm.Unselect();
        }

        public void Draw()
        {
            Fsm.Draw();
        }

        public void Discard()
        {
            Fsm.Discard();
        }

        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region Unity Callbacks

        private void Awake()
        {
            //components
            MyTransform = transform;
            MyCollider = GetComponent<Collider>();
            MyRigidbody = GetComponent<Rigidbody>();
            MyInput = GetComponent<IMouseInput>();
            Hand = transform.parent.GetComponentInChildren<IUiCardHand>();
            MyRenderers = GetComponentsInChildren<SpriteRenderer>();
            MyRenderer = GetComponent<SpriteRenderer>();

            //transform
            UiCardScale = new UiCardScale(this);
            UiCardMovement = new UiCardMovement(this);
            UiCardRotation = new UiCardRotation(this);


            //fsm
            Fsm = new UiCardHandFsm(MainCamera, CardConfigsParameters, this);
        }

        private void Update()
        {
            Fsm.Update();
            UiCardMovement.Update();
            UiCardRotation.Update();
            UiCardScale.Update();
        }

        #endregion

        //--------------------------------------------------------------------------------------------------------------
    }
}