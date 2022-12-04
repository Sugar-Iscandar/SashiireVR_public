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
        Subject<Unit> buttonClickSubject = new Subject<Unit>();

        public IObservable<Unit> OnTriggerButtonPressedEvent
        {
            get => buttonClickSubject;
        }

        void OnTriggerEnter(Collider other)
        {
            iGrabable = other.gameObject.GetComponent<IGrabbable>();
            Debugger.Log("手のコライダー接触");
            if (iGrabable == null)
            Debugger.Log("iGrabableがnull");
        }

        void OnTriggerExit(Collider other)
        {
            if (iGrabable == null) return;
            iGrabable = null;
            Debugger.Log("手のコライダー接触解除");
        }


        //InputSystemにより呼ばれる
        public void OnGrabButtonPressed(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                iGrabable?.OnGrabbed(GetComponent<Rigidbody>());
                Debugger.Log("掴んだ");
            }
            else if (context.canceled)
            {
                iGrabable?.OnReleased();
                Debugger.Log("離した");
            }
        }

        public void OnTriggerButtonPressed(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            buttonClickSubject.OnNext(Unit.Default);
        }
    }
}

