using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

namespace Utilities
{
    public class FlashingObject : MonoBehaviour
    {
        [SerializeField] AnimationCurve flashingCurve;
        [SerializeField] float flashingSpeed;
        float originalAlpha;
        float alpha;
        MeshRenderer mesh;
        IDisposable updateStream;

        void Start()
        {
            mesh = GetComponent<MeshRenderer>();
            originalAlpha = mesh.material.color.a;
            Debug.Log(originalAlpha);
            alpha = 0f;
            updateStream = this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    FlashObject();
                });
        }

        void FlashObject()
        {
            alpha = Mathf.PingPong(
                flashingCurve.Evaluate(Time.time * flashingSpeed) * originalAlpha,
                originalAlpha
            );
            Color color = mesh.material.color;
            color.a = alpha;
            mesh.material.color = color;
        }
    }
}
