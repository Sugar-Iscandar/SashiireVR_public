using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using UniRx;
using System;

namespace Titles.View
{
    public class TitleView : MonoBehaviour
    {
        [SerializeField] VrButton buttonStart;

        public IObservable<Unit> OnStartButtonClicked
        { 
            get => buttonStart.OnButtonClicked;
        }

        public void Initialize()
        {
            BoxCollider collider = buttonStart.gameObject.GetComponent<BoxCollider>();
            collider.enabled = false;
        }

        public void ActiveStartButton()
        {
            BoxCollider collider = buttonStart.gameObject.GetComponent<BoxCollider>();
            collider.enabled = true;
        }
    }
}
