using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tools.UI.Card
{
    [RequireComponent(typeof(IMouseInput))]
    public class UiCardDiscardClick : MonoBehaviour
    {
        private UiCardUtils Utils { get; set; }
        private IMouseInput Input { get; set; }
        private void Awake()
        {
            Utils = transform.parent.GetComponentInChildren<UiCardUtils>();
            Input = GetComponent<IMouseInput>();
            Input.OnPointerClick += PlayRandom;
        }

        private void PlayRandom(PointerEventData obj)
        {
            Utils.PlayCard();
        }
    }
}