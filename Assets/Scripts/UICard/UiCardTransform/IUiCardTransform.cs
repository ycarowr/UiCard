using UnityEngine;

namespace Tools.UI.Card
{
    /// <summary>
    ///     Interface for simple Transform operations.
    /// </summary>
    public interface IUiCardTransform
    {
        /// <summary>
        ///     Movement module.
        /// </summary>
        UiMotionBaseCard Movement { get; }

        /// <summary>
        ///     Rotation module.
        /// </summary>
        UiMotionBaseCard Rotation { get; }

        /// <summary>
        ///     Scale module.
        /// </summary>
        UiMotionBaseCard Scale { get; }

        /// <summary>
        ///     Move in the 3d space using only the X and Y axis.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="speed"></param>
        /// <param name="delay"></param>
        void MoveTo(Vector3 position, float speed, float delay = 0);

        /// <summary>
        ///     Move in the 3d space.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="speed"></param>
        /// <param name="delay"></param>
        void MoveToWithZ(Vector3 position, float speed, float delay = 0);

        /// <summary>
        ///     Rotate in the 3d space.
        /// </summary>
        /// <param name="euler"></param>
        /// <param name="speed"></param>
        void RotateTo(Vector3 euler, float speed);

        /// <summary>
        ///     Scale in the 3d space.
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="speed"></param>
        /// <param name="delay"></param>
        void ScaleTo(Vector3 scale, float speed, float delay = 0);
    }
}