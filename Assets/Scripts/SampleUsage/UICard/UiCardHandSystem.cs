using UnityEngine;

namespace Tools.UI.Card
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(IMouseInput))]
    public class UiCardHandSystem : MonoBehaviour, IUiCard
    {
        //--------------------------------------------------------------------------------------------------------------

        #region Components

        SpriteRenderer[] IUiCardComponents.Renderers => MyRenderers;
        SpriteRenderer IUiCardComponents.MyRenderer => MyRenderer;
        Collider IUiCardComponents.Collider => MyCollider;
        Rigidbody IUiCardComponents.Rigidbody => MyRigidbody;
        Transform IUiCardComponents.Transform => MyTransform;
        IMouseInput IUiCardComponents.Input => MyInput;
        IUiCardSelector IUiCardComponents.CardSelector => MyCardSelector;

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
        private UiCardHandFsm CardHandFsm { get; set; }
        private Transform MyTransform { get; set; }
        private Collider MyCollider { get; set; }
        private SpriteRenderer[] MyRenderers { get; set; }
        private SpriteRenderer MyRenderer { get; set; }
        private Rigidbody MyRigidbody { get; set; }
        private IMouseInput MyInput { get; set; }
        private IUiCardSelector MyCardSelector { get; set; }
        public MonoBehaviour MonoBehavior => this;
        public Camera MainCamera => Camera.main;
        public bool IsDragging => CardHandFsm.IsCurrent<UiCardDrag>();
        public bool IsHovering => CardHandFsm.IsCurrent<UiCardHover>();
        public bool IsDisabled => CardHandFsm.IsCurrent<UiCardDisable>();

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
            CardHandFsm.Hover();
        }

        public void Disable()
        {
            CardHandFsm.Disable();
        }

        public void Enable()
        {
            CardHandFsm.Enable();
        }

        public void Play()
        {
            MyCardSelector.PlayCard(this);
        }

        public void Select()
        {
            MyCardSelector.SelectCard(this);
            CardHandFsm.Select();
        }

        public void Unselect()
        {
            CardHandFsm.Unselect();
            MyCardSelector.UnselectCard(this);
        }

        public void Draw()
        {
            CardHandFsm.Draw();
        }

        public void Discard()
        {
            CardHandFsm.Discard();
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
            MyCardSelector = transform.parent.GetComponentInChildren<IUiCardSelector>();
            MyRenderers = GetComponentsInChildren<SpriteRenderer>();
            MyRenderer = GetComponent<SpriteRenderer>();

            //transform
            UiCardScale = new UiCardScale(this);
            UiCardMovement = new UiCardMovement(this);
            UiCardRotation = new UiCardRotation(this);


            //fsm
            CardHandFsm = new UiCardHandFsm(MainCamera, CardConfigsParameters, this);
        }

        private void Update()
        {
            CardHandFsm.Update();
            UiCardMovement.Update();
            UiCardRotation.Update();
            UiCardScale.Update();
        }

        #endregion

        //--------------------------------------------------------------------------------------------------------------
    }
}