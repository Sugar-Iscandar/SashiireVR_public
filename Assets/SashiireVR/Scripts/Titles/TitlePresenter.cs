using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.View;
using Titles.View;
using UniRx;
using System;

namespace Titles
{
    public class TitlePresenter : MonoBehaviour
    {
        [SerializeField] TitleManager manager;
        [SerializeField] TitleView titleView;
        [SerializeField] CameraScreenView cameraScreenView;

        void Awake()
        {
            manager.OnTitleSceneStarted
                .Subscribe(_ =>
                {
                    cameraScreenView.Initialize();
                    titleView.Initialize();
                    StartCoroutine(cameraScreenView.FadeOut());
                })
                .AddTo(this);
            cameraScreenView.OnFadeOutFinished
                .Subscribe(_ =>
                {
                    titleView.ActiveStartButton();
                })
                .AddTo(this);
            titleView.OnStartButtonClicked
                .Subscribe(_ =>
                {
                    StartCoroutine(cameraScreenView.FadeIn());
                })
                .AddTo(this);
            cameraScreenView.OnFadeInFinished
                .Subscribe(_ =>
                {
                    manager.GoToStory();
                })
                .AddTo(this);
        }
    }
}
