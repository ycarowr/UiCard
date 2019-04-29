using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.UI.Card
{
    /// <summary>
    ///     Enables or Disables a gameobject on Start.
    /// </summary>
    public class UiStartEnabler : MonoBehaviour
    {
        public bool IsActive;

        void Start()
        {
            gameObject.SetActive(IsActive);
        }
    }
}