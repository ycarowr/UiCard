using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.UI.Card
{
    public class UiStartEnabler : MonoBehaviour
    {
        public bool IsActive;
        void Start()
        {
            gameObject.SetActive(IsActive);
        }
    }
}