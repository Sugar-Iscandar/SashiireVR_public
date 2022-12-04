using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Utilities;

namespace Deliveries
{
    public class SashiireObject : MonoBehaviour, IGrabbable
    {
        FixedJoint fixedJoint;
        Rigidbody myRigidbody;
        FloatingObject thisFloatingSystem;
        Subject<float> collisionForceSubject = new Subject<float>();

        public IObservable<float> OnCollision
        {
            get => collisionForceSubject;
        }

        void Start()
        {
            myRigidbody = GetComponent<Rigidbody>();
            thisFloatingSystem = GetComponent<FloatingObject>();
        }

        public void OnGrabbed(Rigidbody handRigidbody)
        {
            //掴まれた手に追従
            thisFloatingSystem.StopFloating();
            myRigidbody.useGravity = false;
            gameObject.AddComponent<FixedJoint>();
            fixedJoint = GetComponent<FixedJoint>();
            fixedJoint.connectedBody = handRigidbody;
        }

        public void OnReleased()
        {
            //手への追従解除
            Destroy(fixedJoint);
            myRigidbody.useGravity = true;
        }

        void OnCollisionEnter(Collision other)
        {
            float impulse = other.impulse.magnitude;
            Debugger.Log("衝突力＝" + impulse);
            collisionForceSubject.OnNext(impulse);
        }
    }
}
