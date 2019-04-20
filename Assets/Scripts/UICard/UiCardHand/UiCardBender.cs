using System;
using UnityEngine;

namespace Tools.UI.Card
{
    /// <summary>
    ///     Class responsible to bend the cards in the player hand.
    /// </summary>
    [RequireComponent(typeof(UiCardHand))]
    public class UiCardBender : MonoBehaviour
    {
        //--------------------------------------------------------------------------------------------------------------

        #region Unitycallbacks

        private void Awake()
        {
            CardHand = GetComponent<UiCardHand>();
            CardRenderer = CardPrefab.GetComponent<SpriteRenderer>();
            CardHand.OnPileChanged += Bend;
        }

        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region Fields and Properties

        [SerializeField] private UiCardParameters parameters;

        [SerializeField] [Tooltip("The Card Prefab")]
        private UiCardHandComponent CardPrefab;

        [SerializeField] [Tooltip("Transform used as anchor to position the cards.")]
        private Transform pivot;

        private SpriteRenderer CardRenderer { get; set; }
        private float CardWidth => CardRenderer.bounds.size.x;
        private UiCardHand CardHand { get; set; }

        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region Operations

        private void Bend(IUiCard[] cards)
        {
            if (cards == null)
                throw new ArgumentException("Can't bend a card list null");

            var fullAngle = -parameters.BentAngle;
            var anglePerCard = fullAngle / cards.Length;
            var firstAngle = CalcFirstAngle(fullAngle);
            var handWidth = CalcHandWidth(cards.Length);

            //calc first position of the offset on X axis
            var offsetX = pivot.position.x - handWidth / 2;

            for (var i = 0; i < cards.Length; i++)
            {
                var card = cards[i];

                //set card Z angle
                var angleTwist = firstAngle + i * anglePerCard;

                //calc x position
                var xPos = offsetX + CardWidth / 2;

                //calc y position
                var yDistance = Mathf.Abs(angleTwist) * parameters.Height;
                var yPos = pivot.position.y - yDistance;

                //set position
                if (!card.IsDragging && !card.IsHovering)
                {
                    var rotation = new Vector3(0, 0, angleTwist);
                    var position = new Vector3(xPos, yPos, card.transform.position.z);

                    card.RotateTo(rotation, parameters.RotationSpeed);
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
        private static float CalcFirstAngle(float fullAngle)
        {
            var magicMathFactor = 0.1f;
            return -(fullAngle / 2) + fullAngle * magicMathFactor;
        }

        /// <summary>
        ///     Calculus of the width of the player's hand.
        /// </summary>
        /// <param name="quantityOfCards"></param>
        /// <returns></returns>
        private float CalcHandWidth(int quantityOfCards)
        {
            var widthCards = quantityOfCards * CardWidth;
            var widthSpacing = (quantityOfCards - 1) * parameters.Spacing;
            return widthCards + widthSpacing;
        }

        #endregion

        //--------------------------------------------------------------------------------------------------------------
    }
}