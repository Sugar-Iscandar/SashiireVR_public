using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Deliveries
{
    public class SashiirePresenter : MonoBehaviour
    {
        [SerializeField] DebugView debugView;

        void Awake()
        {
            Debugger.OnLogOut
                .Subscribe(content =>
                {
                    debugView.SetContentDebugText(content);
                })
                .AddTo(this);
        }
    }
}