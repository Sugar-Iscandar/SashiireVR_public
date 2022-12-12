using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Deliveries.View;
using Utilities.View;

namespace Deliveries
{
    public class SashiirePresenter : MonoBehaviour
    {
        [SerializeField] SashiireManager manager;
        [SerializeField] DebugView debugView;
        [SerializeField] PlayerDialogView playerDialogView;
        [SerializeField] CameraScreenView cameraScreenView;

        void Awake()
        {
            manager.OnSashiireSceneStarted
                .Delay(System.TimeSpan.FromSeconds(2))
                .Subscribe(_ =>
                {
                    cameraScreenView.Initialize();
                    StartCoroutine(cameraScreenView.FadeOut());
                })
                .AddTo(this);

            manager.OnPlayerGiveUpGoaledEvent
                .Subscribe(content =>
                {
                    //ダイアログを生成
                    playerDialogView.ShowPlayerDialog(
                        manager.PlayerCameraPosition,
                        manager.PlayerCameraForward,
                        manager.PlayerCameraRotation
                    );

                    playerDialogView.SetContentMessageText(content);

                    playerDialogView.OnYesButtonClicked
                        .Subscribe(_ =>
                        {
                            manager.PlayerGiveUp();
                            StartCoroutine(cameraScreenView.FadeIn());
                        })
                        .AddTo(playerDialogView.CanvasPlayerDialog);

                    playerDialogView.OnNoButtonClicked
                        .Subscribe(_ =>
                        {
                            playerDialogView.HidePlayerDialog();
                            manager.ContinueGame();
                        })
                        .AddTo(playerDialogView.CanvasPlayerDialog);
                })
                .AddTo(this);

            manager.OnPlayerGoaledEvent
                .Subscribe(_ =>
                {
                    StartCoroutine(cameraScreenView.FadeIn());
                })
                .AddTo(this);

            manager.OnGirlWokeUp
                .Subscribe(_ =>
                {
                    StartCoroutine(cameraScreenView.FadeIn());
                })
                .AddTo(this);
            
            cameraScreenView.OnFadeInFinished
                .Subscribe(_ =>
                {
                    manager.ChangeToResultScene();
                })
                .AddTo(this);

            Debugger.OnLogOut
                .Subscribe(content =>
                {
                    debugView.SetContentDebugText(content);
                })
                .AddTo(this);
        }
    }
}