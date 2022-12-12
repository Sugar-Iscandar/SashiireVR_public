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
        Subject<Unit> titleStartSubject = new Subject<Unit>();
        public IObservable<Unit> OnTitleSceneStarted { get => titleStartSubject; }
        public IObservable<Unit> OnLeftHandTriggerButtonPressed
        { 
            get => leftHand.OnTriggerButtonPressedEvent; 
        }
        public IObservable<Unit> OnRightHandTriggerButtonPressed
        {
            get => rightHand.OnTriggerButtonPressedEvent;
        }

        void Start()
        {
            titleStartSubject.OnNext(Unit.Default);
            leftHand.Initialize();
            rightHand.Initialize();
            leftHand.IsRayCastActive = true;
            rightHand.IsRayCastActive = true;
        }

        public void GoToStory()
        {
            Debugger.Log("シーン遷移開始");
            SceneChanger.Instance.ChangeScene(Scenes.Darkness);
        }
    }
}
