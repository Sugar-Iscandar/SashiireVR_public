using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;
using GameSystems;
using UniRx;
using System;

namespace Titles
{
    public class TitleManager : MonoBehaviour
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
                    GoToStory();
                })
                .AddTo(this);
            streamsHandTriggerEvent[1] =  rightHand.OnTriggerButtonPressedEvent
                .Subscribe(_ => 
                {
                    GoToStory();
                })
                .AddTo(this);
                
        }

        void GoToStory()
        {
            //音を出してシーンの切り替えを行う
            foreach (IDisposable disposable in streamsHandTriggerEvent)
                disposable.Dispose();
            SceneChanger.Instance.ChangeScene(Scenes.Darkness);
        }
    }
}
