using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Darkness.View;
using UniRx;
using System;
using Utilities.View;

namespace Darkness
{
    public class DarknessPresenter : MonoBehaviour
    {
        [SerializeField] DarknessManager manager;
        [SerializeField] MainScreenView mainView;
        [SerializeField] SubScreenView subView;
        [SerializeField] CameraScreenView cameraScreenView;

        void Awake()
        {
            manager.OnDarknessStarted
                .Subscribe(text =>
                {
                    mainView.Initialized();
                    subView.Initialized();
                    cameraScreenView.Initialize();
                    subView.SetContentMainText(text);
                    subView.ShowMainText();
                    StartCoroutine(cameraScreenView.FadeOut());
                })
                .AddTo(this);
            cameraScreenView.OnFadeOutFinished
                .Subscribe(_ =>
                {
                    subView.ActiveButtons();
                })
                .AddTo(this);
            subView.OnSynopsisFinished
                .Subscribe(_ =>
                {
                    StartCoroutine(cameraScreenView.FadeIn());
                })
                .AddTo(this);
            cameraScreenView.OnFadeInFinished
                .Subscribe(_ =>
                {
                    manager.ChangeToSashiireScene();
                })
                .AddTo(this);
        }
    }
}
