using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;
using UniRx;
using System;
using GameSystems;
using Utilities;

namespace Results
{
    public class ResultManager : MonoBehaviour
    {
        [SerializeField] Hand leftHand;
        [SerializeField] Hand rightHand;
        Utilities.Results result;
        public Utilities.Results Result { get => result; }
        Subject<Unit> resultSceneStartSubject = new Subject<Unit>();
        public IObservable<Unit> OnResultSceneStarted { get => resultSceneStartSubject; }

        void Start()
        {
            resultSceneStartSubject.OnNext(Unit.Default);
            leftHand.Initialize();
            rightHand.Initialize();
            leftHand.IsRayCastActive = false;
            rightHand.IsRayCastActive = false;
            result = ResultRecorder.Instance.Result;
        }

        public void ActivateRay()
        {
            leftHand.IsRayCastActive = true;
            rightHand.IsRayCastActive = true;
        }

        public void ChangeToSashiireScene()
        {
            SceneChanger.Instance.ChangeScene(Scenes.Sashiire);
        }

        public void ChangeToTitleScene()
        {
            SceneChanger.Instance.ChangeScene(Scenes.Title);
        }
    }
}
