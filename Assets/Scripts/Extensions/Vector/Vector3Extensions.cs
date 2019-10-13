using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    /// <summary>
    ///     Vectors extensions.
    ///     Refs:
    ///     1. https://gist.github.com/omgwtfgames/f917ca28581761b8100f
    ///     2. https://github.com/mminer/unity-extensions/blob/master/Vector3Extensions.cs
    /// </summary>
    public static class VectorExtensions
    {
        /// <summary>
        ///     Finds the position closest to the given one.
        /// </summary>
        /// <param name="position">World position.</param>
        /// <param name="otherPositions">Other world positions.</param>
        /// <returns>Closest position.</returns>
        public static Vector3 GetClosest(this Vector3 position, IEnumerable<Vector3> otherPositions)
        {
            var closest = Vector3.zero;
            var shortestDistance = Mathf.Infinity;

            foreach (var otherPosition in otherPositions)
            {
                var distance = (position - otherPosition).sqrMagnitude;

                if (distance < shortestDistance)
                {
                    closest = otherPosition;
                    shortestDistance = distance;
                }
            }

            return closest;
        }


        public static Vector2 XY(this Vector3 v) => new Vector2(v.x, v.y);

        public static Vector3 WithX(this Vector3 v, float x) => new Vector3(x, v.y, v.z);

        public static Vector3 WithY(this Vector3 v, float y) => new Vector3(v.x, y, v.z);

        public static Vector3 WithZ(this Vector3 v, float z) => new Vector3(v.x, v.y, z);

        public static Vector2 WithX(this Vector2 v, float x) => new Vector2(x, v.y);

        public static Vector2 WithY(this Vector2 v, float y) => new Vector2(v.x, y);

        public static Vector3 WithZ(this Vector2 v, float z) => new Vector3(v.x, v.y, z);

        public static Vector3 WithAddX(this Vector3 v, float x) => new Vector3(v.x + x, v.y, v.z);

        public static Vector3 WithAddY(this Vector3 v, float y) => new Vector3(v.x, v.y + y, v.z);

        public static Vector3 WithAddZ(this Vector3 v, float z) => new Vector3(v.x, v.y, v.z + z);

        public static Vector2 WithAddX(this Vector2 v, float x) => new Vector2(v.x + x, v.y);

        public static Vector2 WithAddY(this Vector2 v, float y) => new Vector2(v.x, v.y + y);

        // axisDirection - unit vector in direction of an axis (eg, defines a line that passes through zero)
        // point - the point to find nearest on line for
        public static Vector3 NearestPointOnAxis(this Vector3 axisDirection, Vector3 point, bool isNormalized = false)
        {
            if (!isNormalized)
                axisDirection.Normalize();
            var d = Vector3.Dot(point, axisDirection);
            return axisDirection * d;
        }

        // lineDirection - unit vector in direction of line
        // pointOnLine - a point on the line (allowing us to define an actual line in space)
        // point - the point to find nearest on line for
        public static Vector3 NearestPointOnLine(
            this Vector3 lineDirection, Vector3 point, Vector3 pointOnLine, bool isNormalized = false)
        {
            if (!isNormalized)
                lineDirection.Normalize();
            var d = Vector3.Dot(point - pointOnLine, lineDirection);
            return pointOnLine + lineDirection * d;
        }
    }
}