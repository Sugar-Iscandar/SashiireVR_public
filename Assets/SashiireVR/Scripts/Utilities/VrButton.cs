using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

namespace Utilities
{
    public class VrButton : MonoBehaviour, IClickable
    {
        Image image;
        Color originalColor;
        Subject<Unit> buttonClickSubject = new Subject<Unit>();
        public IObservable<Unit> OnButtonClicked
        {
            get => buttonClickSubject;
        }

        void Awake()
        {
            image = GetComponent<Image>();
            originalColor = image.color;
        }
        
        public void OnAimed()
        {
            image.color = Color.green;
        }

        public void OnDeflected()
        {
            if (image == null) return;
            image.color = originalColor;
        }

        public void OnClicked()
        {
            buttonClickSubject.OnNext(Unit.Default);
        }
    }
}


