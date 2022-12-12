using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using GameSystems;
using Players;
using Utilities;
using Girls;

namespace Deliveries
{
    //差し入れシーンを統括する
    public class SashiireManager : MonoBehaviour
    {
        [SerializeField] Hand leftHand;
        [SerializeField] Hand rightHand;
        [SerializeField] SashiireObject sashiireObject;
        [SerializeField] WakeUpJudgement wakeUpJudgement;
        [SerializeField] PlayerStatus playerStatus;
        [SerializeField] PlayerMove playerMove;
        [SerializeField] GoalMarker goalMarker;
        [SerializeField] SashiireSetChecker sashiireSetChecker;
        [SerializeField] InstructionManager instructionManager;
        [SerializeField] GirlAnimationController girlAnimationController;
        [SerializeField] FaceController faceController;
        Subject<Unit> sashiireSceneStartSubject = new Subject<Unit>();
        public IObservable<Unit> OnSashiireSceneStarted { get => sashiireSceneStartSubject; }
        Subject<string> playerGiveUpGoalSubject = new Subject<string>();
        public IObservable<string> OnPlayerGiveUpGoaledEvent { get => playerGiveUpGoalSubject; }
        Subject<Unit> playerGoalSubject = new Subject<Unit>();
        public IObservable<Unit> OnPlayerGoaledEvent { get => playerGoalSubject; }
        Subject<Unit> girlWokeUpSubject = new Subject<Unit>();
        public IObservable<Unit> OnGirlWokeUp { get => girlWokeUpSubject; }
        IDisposable streamCollisitonJudgement;
        IDisposable streamWalkSpeedJudgement;

        public Vector3 PlayerCameraPosition { get => playerStatus.PlayerCameraPosition; }
        public Vector3 PlayerCameraForward { get => playerStatus.PlayerCameraForward; }
        public Quaternion PlayerCameraRotation { get => playerStatus.PlayerCameraRotation; }
        
        void Start()
        {
            //初めて差し入れを掴んだら
            sashiireObject.OnFirstGrabbed
                .First()
                .Subscribe(_ =>
                {
                    GameStart();
                });
            //差し入れの衝突力をチェック
            streamCollisitonJudgement = sashiireObject.OnCollision
                .Subscribe(impulse => 
                {
                    wakeUpJudgement.JudgWakeUpFromCollisionImpulse(impulse);
                })
                .AddTo(this);
            //プレイヤーの移動速度をチェック
            streamWalkSpeedJudgement = playerMove.InputMagnitude
                .Subscribe(magnitude =>
                {
                    wakeUpJudgement.JudgWakeUpFromWalkSpeed(magnitude);
                })
                .AddTo(this);
            //起きてしまうイベントが発生したら
            wakeUpJudgement.OnWakeUp
                .First()
                .Subscribe(_ =>
                {
                    WakeUpGirl();
                })
                .AddTo(this);
            //プレイヤーのゴールを検出したら
            goalMarker.OnPlayerGoaled
                .Subscribe(_ =>
                {
                    OnPlayerGoaled();
                })
                .AddTo(this);

            leftHand.Initialize();
            rightHand.Initialize();
            instructionManager.Initialize();
            girlAnimationController.Initialize();
            sashiireSceneStartSubject.OnNext(Unit.Default);
        }

        void GameStart()
        {
            instructionManager.FinishRightControllerInstruction();
            instructionManager.ShowAllowInstruction();

            //差し入れが定位置に置かれたら
            sashiireSetChecker.OnSashiireSet
                .First()
                .Subscribe(_ =>
                {
                    instructionManager.FinishAllowInstruction();
                });
        }

        void WakeUpGirl()
        {
            streamCollisitonJudgement.Dispose();
            streamWalkSpeedJudgement.Dispose();
            girlAnimationController.OnWakeUp();
            faceController.ChangeFaceExpressionOnWakeUp();
            ResultRecorder.Instance.Result = Utilities.Results.GirlWokeUp;
            Observable.Timer(TimeSpan.FromSeconds(7))
                .Subscribe(_ =>
                {
                    girlWokeUpSubject.OnNext(Unit.Default);
                });
        }

        void OnPlayerGoaled()
        {
            //差し入れが置かれていた場合はゴール
            if (sashiireSetChecker.IsSashiirePlaced)
            {
                ResultRecorder.Instance.Result = Utilities.Results.Success;
                playerGoalSubject.OnNext(Unit.Default);
            }
            else
            {
                //確認ダイアログを表示
                playerGiveUpGoalSubject.OnNext(
                    "差し入れが置かれていない！\n置くのを諦めますか？"
                );
                leftHand.IsRayCastActive = true;
                rightHand.IsRayCastActive = true;
            }
        }

        public void PlayerGiveUp()
        {
            ResultRecorder.Instance.Result = Utilities.Results.GiveUp;
        }

        //ダイアログを閉じたとき、Rayを消す
        public void ContinueGame()
        {
            leftHand.IsRayCastActive = false;
            rightHand.IsRayCastActive = false;
        }

        public void ChangeToResultScene()
        {
            SceneChanger.Instance.ChangeScene(Scenes.Result);
        }
    }
}


