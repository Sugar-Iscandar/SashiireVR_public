using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

namespace Deliveries
{
    public class SashiireSetChecker : MonoBehaviour
    {
        [SerializeField] float holdTime = 3.0f;
        bool isSashiireTouched = false;
        bool isSashiirePlaced = false;
        float actualHoldTime = 0f;
        IDisposable updateStream;

        public bool IsSashiirePlaced
        {
            get => isSashiirePlaced;
        }
        
        void Start()
        {
            updateStream = this.UpdateAsObservable()
                .Where(_ => isSashiireTouched)
                .Subscribe(_ => 
                {
                    CheckSashiirePlaced();
                });
        }

        void CheckSashiirePlaced()
        {
            actualHoldTime += Time.deltaTime;

            if (actualHoldTime >= holdTime)
            {
                isSashiirePlaced = true;
                updateStream.Dispose();
            }
        }

        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Sashiire"))
            {
                isSashiireTouched = true;
            }
        }

        void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag("Sashiire"))
            {
                isSashiireTouched = false;
                actualHoldTime = 0f;
                if (isSashiirePlaced)
                {
                    isSashiirePlaced = false;
                    Start();
                }
            }
        }

    }
}
