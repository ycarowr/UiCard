using UnityEngine;

namespace Extensions
{
    /// <summary>
    ///     Extension methods for UnityEngine.Component.
    ///     Ref: https://github.com/mminer/unity-extensions/blob/master/ComponentExtensions.cs
    /// </summary>
    public static class ComponentExtensions
    {
        /// <summary>
        ///     Attaches a component to the given component's game object.
        /// </summary>
        /// <param name="component">Component.</param>
        /// <returns>Newly attached component.</returns>
        public static T AddComponent<T>(this Component component) where T : Component =>
            component.gameObject.AddComponent<T>();

        /// <summary>
        ///     Gets a component attached to the given component's game object.
        ///     If one isn't found, a new one is attached and returned.
        /// </summary>
        /// <param name="component">Component.</param>
        /// <returns>Previously or newly attached component.</returns>
        public static T GetOrAddComponent<T>(this Component component) where T : Component =>
            component.GetComponent<T>() ?? component.AddComponent<T>();

        /// <summary>
        ///     Checks whether a component's game object has a component of type T attached.
        /// </summary>
        /// <param name="component">Component.</param>
        /// <returns>True when component is attached.</returns>
        public static bool HasComponent<T>(this Component component) where T : Component =>
            component.GetComponent<T>() != null;
    }
}