using UnityEngine;

namespace Tools.UI.Card
{
    [CreateAssetMenu(menuName = "Card Config Parameters")]
    public class UiCardParameters : ScriptableObject
    {
        //--------------------------------------------------------------------------------------------------------------
        public float DisabledAlpha
        {
            get => disabledAlpha;
            set => disabledAlpha = value;
        }

        #region Disable

        [Header("Disable")] [Tooltip("How a card fades when disabled.")] [SerializeField] [Range(0.1f, 1)]
        private float disabledAlpha;

        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region Hover



        public float HoverHeight
        {
            get => hoverHeight;
            set => hoverHeight = value;
        }

        public bool HoverRotation
        {
            get => hoverRotation;
            set => hoverRotation = value;
        }

        public float HoverScale
        {
            get => hoverScale;
            set => hoverScale = value;
        }

        [Header("Hover")] [SerializeField] [Tooltip("How much the card will go upwards when hovered.")] [Range(0, 4)]
        private float hoverHeight;

        [SerializeField] [Tooltip("Whether the hovered card keep its rotation.")]
        private bool hoverRotation;

        [SerializeField] [Tooltip("How much a hovered card scales.")] [Range(0.9f, 2f)]
        private float hoverScale;

        [SerializeField]
        [Range(0, 25)]
        [Tooltip("Speed of a card while it is hovering")]
        private float hoverSpeed;

        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region Bend

        public float Height
        {
            get => height;
            set => height = value;
        }

        public float BentAngle
        {
            get => bentAngle;
            set => bentAngle = value;
        }

        [Header("Bend")] [SerializeField] [Tooltip("Height factor between two cards.")] [Range(0f, 1f)]
        private float height;

        [SerializeField] [Tooltip("Amount of space between the cards on the X axis")] [Range(0f, -5f)]
        private float spacing;

        public float Spacing
        {
            get => spacing;
            set => spacing = -value;
        }

        [SerializeField] [Tooltip("Total angle in degrees the cards will bend.")] [Range(0, 60)]
        private float bentAngle;

        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region Movement


        [Header("Rotation")]
        [SerializeField] [Range(0, 60)] [Tooltip("Speed of a card while it is rotating")]
        private float rotationSpeed;

        [SerializeField]
        [Range(0, 1000)]
        [Tooltip("Speed of a card while it is rotating for player 2")]
        private float rotationSpeedP2;

        [Header("Movement")] [SerializeField] [Range(0, 15)] [Tooltip("Speed of a card while it is moving")]
        private float movementSpeed;

        [Header("Scale")] [SerializeField] [Range(0, 15)] [Tooltip("Speed of a card while it is scaling")]
        private float scaleSpeed;

        public float HoverSpeed { get => hoverSpeed; set => hoverSpeed = value; }
        public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
        public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
        public float ScaleSpeed { get => scaleSpeed; set => scaleSpeed = value;}
        public float RotationSpeedP2 { get => rotationSpeedP2; set => rotationSpeedP2 = value; }

        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region Draw Discard
        [Header("Draw")][SerializeField][Range(0, 1)][Tooltip("Scale when draw the card")]
        private float startSizeWhenDraw;

        public float StartSizeWhenDraw { get => startSizeWhenDraw; set => startSizeWhenDraw = value; }

        //--------------------------------------------------------------------------------------------------------------
        
        [Header("Discard")][SerializeField][Range(0, 1)][Tooltip("Scale when discard the card")]
        private float discardedSize;

        public float DiscardedSize => discardedSize;

        #endregion

        //--------------------------------------------------------------------------------------------------------------

        [Button]
        public void SetDefaults()
        {
            disabledAlpha = 0.5f;

            hoverHeight = 1;
            hoverRotation = false;
            hoverScale = 1.3f;
            hoverSpeed = 15f;

            height = 0.12f;
            spacing = -2;
            bentAngle = 20;

            rotationSpeedP2 = 500;
            rotationSpeed = 20;
            movementSpeed = 4;
            scaleSpeed = 7;

            startSizeWhenDraw = 0.05f;
            discardedSize = 0.5f;
        }
    }
}