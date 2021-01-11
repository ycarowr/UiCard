using System;
using Extensions;
using UnityEngine;

namespace Tools.UI.Card
{
    /// <summary>
    ///     Class responsible to bend the cards in the player hand.
    /// </summary>
    [RequireComponent(typeof(IUiPlayerHand))]
    public class UiPlayerHandBender : MonoBehaviour
    {
        //--------------------------------------------------------------------------------------------------------------

        #region Unitycallbacks

        void Awake()
        {
            PlayerHand = GetComponent<IUiPlayerHand>();
            CardRenderer = CardPrefab.GetComponentsInChildren<SpriteRenderer>()[0];
            PlayerHand.OnPileChanged += Bend;
        }

        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region Fields and Properties

        [SerializeField] UiCardParameters parameters;

        [SerializeField] [Tooltip("The Card Prefab")]
        GameObject CardPrefab;

        [SerializeField] [Tooltip("Transform used as anchor to position the cards.")]
        Transform pivot;

        SpriteRenderer CardRenderer { get; set; }
        float CardWidth => CardRenderer.bounds.size.x;
        IUiPlayerHand PlayerHand { get; set; }

        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region Operations

        void Bend(IUiCard[] cards)
        {
            if (cards == null)
                throw new ArgumentException("Can't bend a card list null");

            var fullAngle = -parameters.BentAngle;
            var anglePerCard = fullAngle / cards.Length;
            var firstAngle = CalcFirstAngle(fullAngle);
            var handWidth = CalcHandWidth(cards.Length);

            var pivotLocationFactor = pivot.CloserEdge(Camera.main, Screen.width, Screen.height);

            //calc first position of the offset on X axis
            var offsetX = pivot.position.x - handWidth / 2;

            for (var i = 0; i < cards.Length; i++)
            {
                var card = cards[i];

                //set card Z angle
                var angleTwist = (firstAngle + i * anglePerCard) * pivotLocationFactor;

                //calc x position
                var xPos = offsetX + CardWidth / 2;

                //calc y position
                var yDistance = Mathf.Abs(angleTwist) * parameters.Height;
                var yPos = pivot.position.y - yDistance * pivotLocationFactor;

                //set position
                if (!card.IsDragging && !card.IsHovering)
                {
                    var zAxisRot = pivotLocationFactor == 1 ? 0 : 180;
                    var rotation = new Vector3(0, 0, angleTwist - zAxisRot);
                    var position = new Vector3(xPos, yPos, card.transform.position.z);

                    var rotSpeed = card.IsPlayer ? parameters.RotationSpeed : parameters.RotationSpeedP2;
                    card.RotateTo(rotation, rotSpeed);
                    card.MoveTo(position, parameters.MovementSpeed);
                }

                //increment offset
                offsetX += CardWidth + parameters.Spacing;
            }
        }

        /// <summary>
        ///     Calculus of the angle of the first card.
        /// </summary>
        /// <param name="fullAngle"></param>
        /// <returns></returns>
        static float CalcFirstAngle(float fullAngle)
        {
            var magicMathFactor = 0.1f;
            return -(fullAngle / 2) + fullAngle * magicMathFactor;
        }

        /// <summary>
        ///     Calculus of the width of the player's hand.
        /// </summary>
        /// <param name="quantityOfCards"></param>
        /// <returns></returns>
        float CalcHandWidth(int quantityOfCards)
        {
            var widthCards = quantityOfCards * CardWidth;
            var widthSpacing = (quantityOfCards - 1) * parameters.Spacing;
            return widthCards + widthSpacing;
        }

        #endregion

        //--------------------------------------------------------------------------------------------------------------
    }
}