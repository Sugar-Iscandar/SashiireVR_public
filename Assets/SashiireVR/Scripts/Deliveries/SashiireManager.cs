using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using GameSystems;

namespace Deliveries
{
    //差し入れシーンを統括する
    public class SashiireManager : MonoBehaviour
    {
        [SerializeField] SashiireObject sashiireObject;
        [SerializeField] WakeUpJudgement wakeUpJudgement;
        [SerializeField] PlayerMove playerMove;
        [SerializeField] GoalMarker goalMarker;
        [SerializeField] SashiireSetChecker sashiireSetChecker;
        IDisposable streamCollisitonJudgement;
        IDisposable streamWalkSpeedJudgement;
        
        void Start()
        {
            streamCollisitonJudgement = sashiireObject.OnCollision
                .Subscribe(impulse => 
                {
                    wakeUpJudgement.JudgWakeUpFromCollisionImpulse(impulse);
                })
                .AddTo(this);

            streamWalkSpeedJudgement = playerMove.InputMagnitude
                .Subscribe(magnitude =>
                {
                    wakeUpJudgement.JudgWakeUpFromWalkSpeed(magnitude);
                })
                .AddTo(this);

            wakeUpJudgement.OnWakeUp
                .First()
                .Subscribe(_ =>
                {
                    WakeUpGirl();
                })
                .AddTo(this);

            goalMarker.OnPlayerGoaled
                .Subscribe(_ =>
                {
                    OnPlayerGoaled();
                })
                .AddTo(this);

            
        }

        void WakeUpGirl()
        {
            Debugger.Log("起きてしまった！");
            streamCollisitonJudgement.Dispose();
            streamWalkSpeedJudgement.Dispose();
        }

        void OnPlayerGoaled()
        {
            if (sashiireSetChecker.IsSashiirePlaced)
            {
                SceneChanger.Instance.ChangeScene(Scenes.Result);
            }
            else
            {
                //諦めるか確認
                Debugger.Log("諦めるんか？");
            }
        }
    }
}


