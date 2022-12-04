using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;
using UniRx;
using System;
using GameSystems;

namespace Results
{
    public class ResultManager : MonoBehaviour
    {
        [SerializeField] Hand leftHand;
        [SerializeField] Hand rightHand;
        IDisposable[] streamsHandTriggerEvent = new IDisposable[2];

        void Start()
        {
            //左右の手のボタン入力イベントを購読する
            streamsHandTriggerEvent[0] = leftHand.OnTriggerButtonPressedEvent
                .Subscribe(_ => 
                {
                    ChangeToSashiireScene();
                })
                .AddTo(this);
            streamsHandTriggerEvent[1] = rightHand.OnTriggerButtonPressedEvent
                .Subscribe(_ => 
                {
                    ChangeToSashiireScene();
                })
                .AddTo(this);
                
        }

        void ChangeToSashiireScene()
        {
            foreach (IDisposable disposable in streamsHandTriggerEvent)
                disposable.Dispose();
            SceneChanger.Instance.ChangeScene(Scenes.Title);
        }
    }
}
