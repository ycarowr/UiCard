using System;
using UnityEngine;

namespace Extensions
{
    /// <summary>
    ///     Extension methods for UnityEngine.Transform.
    ///     Ref: https://github.com/mminer/unity-extensions/blob/master/TransformExtensions.cs
    /// </summary>
    public static class TransformExtensions
    {
        /// <summary>
        ///     Makes the given game objects children of the transform.
        /// </summary>
        /// <param name="transform">Parent transform.</param>
        /// <param name="children">Game objects to make children.</param>
        public static void AddChildren(this Transform transform, GameObject[] children) =>
            Array.ForEach(children, child => child.transform.parent = transform);

        /// <summary>
        ///     Makes the game objects of given components children of the transform.
        /// </summary>
        /// <param name="transform">Parent transform.</param>
        /// <param name="children">Components of game objects to make children.</param>
        public static void AddChildren(this Transform transform, Component[] children) =>
            Array.ForEach(children, child => child.transform.parent = transform);

        /// <summary>
        ///     Sets the position of a transform's children to zero.
        /// </summary>
        /// <param name="transform">Parent transform.</param>
        /// <param name="recursive">Also reset ancestor positions?</param>
        public static void ResetChildPositions(this Transform transform, bool recursive = false)
        {
            foreach (Transform child in transform)
            {
                child.position = Vector3.zero;

                if (recursive) child.ResetChildPositions(recursive);
            }
        }

        /// <summary>
        ///     Sets the layer of the transform's children.
        /// </summary>
        /// <param name="transform">Parent transform.</param>
        /// <param name="layerName">Name of layer.</param>
        /// <param name="recursive">Also set ancestor layers?</param>
        public static void SetChildLayers(this Transform transform, string layerName, bool recursive = false)
        {
            var layer = LayerMask.NameToLayer(layerName);
            SetChildLayersHelper(transform, layer, recursive);
        }

        static void SetChildLayersHelper(Transform transform, int layer, bool recursive)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.layer = layer;

                if (recursive) SetChildLayersHelper(child, layer, recursive);
            }
        }

        /// <summary>
        ///     Sets the x component of the transform's position.
        /// </summary>
        /// <param name="x">Value of x.</param>
        public static void SetX(this Transform transform, float x) =>
            transform.position = new Vector3(x, transform.position.y, transform.position.z);

        /// <summary>
        ///     Sets the y component of the transform's position.
        /// </summary>
        /// <param name="y">Value of y.</param>
        public static void SetY(this Transform transform, float y) =>
            transform.position = new Vector3(transform.position.x, y, transform.position.z);

        /// <summary>
        ///     Sets the z component of the transform's position.
        /// </summary>
        /// <param name="z">Value of z.</param>
        public static void SetZ(this Transform transform, float z) =>
            transform.position = new Vector3(transform.position.x, transform.position.y, z);

        /// <summary>
        ///     Calculus of the location of this object. Whether it is located at the top or bottom. -1 and 1 respectively.
        /// </summary>
        /// <returns></returns>
        public static int CloserEdge(this Transform transform, Camera camera, int width, int height)
        {
            //edge points according to the screen/camera
            var worldPointTop = camera.ScreenToWorldPoint(new Vector3(width / 2, height));
            var worldPointBot = camera.ScreenToWorldPoint(new Vector3(width / 2, 0));

            //distance from the pivot to the screen edge
            var deltaTop = Vector2.Distance(worldPointTop, transform.position);
            var deltaBottom = Vector2.Distance(worldPointBot, transform.position);

            return deltaBottom <= deltaTop ? 1 : -1;
        }
    }
}