using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UniRx;
using System;

namespace Players
{
    public class Hand : MonoBehaviour
    {
        public enum LeftRight
        {
            Left,
            Right
        }
        [SerializeField] LeftRight leftOrRight;
        IGrabbable iGrabable = null;
        HandRay handRay;
        Subject<Unit> buttonClickSubject = new Subject<Unit>();

        public bool IsRayCastActive
        {
            get => handRay.IsRayCastActive;
            set => handRay.IsRayCastActive = value;
        }

        public IObservable<Unit> OnTriggerButtonPressedEvent
        {
            get => buttonClickSubject;
        }

        public void Initialize()
        {
            handRay = GetComponent<HandRay>();
            handRay.Initialize();
            handRay.IsRayCastActive = false;
        }

        void OnTriggerEnter(Collider other)
        {
            iGrabable = other.gameObject.GetComponent<IGrabbable>();
        }

        void OnTriggerExit(Collider other)
        {
            if (iGrabable == null) return;
            iGrabable = null;
        }

        //InputSystemにより呼ばれる
        public void OnGrabButtonPressed(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                iGrabable?.OnGrabbed(GetComponent<Rigidbody>());
            }
            else if (context.canceled)
            {
                iGrabable?.OnReleased();
            }
        }

        public void OnTriggerButtonPressed(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            buttonClickSubject.OnNext(Unit.Default);
            handRay.ClickButton();
        }
    }
}

