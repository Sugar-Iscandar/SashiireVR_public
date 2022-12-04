using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

namespace Utilities
{
    public class FloatingObject : MonoBehaviour
    {
        [SerializeField] AnimationCurve floatingCurve;
        [SerializeField] float floatingRange;
        [SerializeField] float floatingSpeed;
        Vector3 currentPosition;
        IDisposable updateStream;

        void Start()
        {
            currentPosition = transform.position;

            updateStream = this.UpdateAsObservable()
                .Subscribe(_ => 
                {
                    FloatObject();
                });
        }

        void FloatObject()
        {
            transform.position = new Vector3(
                transform.position.x,
                currentPosition.y + Mathf.PingPong(
                    floatingCurve.Evaluate(Time.time * floatingSpeed),
                    floatingRange
                ),
                transform.position.z
            );
        }

        public void StopFloating()
        {
            updateStream.Dispose();
        }
    }
}
