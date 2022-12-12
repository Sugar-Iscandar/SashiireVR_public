using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.View;
using UniRx;
using System;
using Results.View;

namespace Results
{
    public class ResultPresenter : MonoBehaviour
    {
        [SerializeField] ResultManager manager;
        [SerializeField] ResultMainScreenView mainView;
        [SerializeField] ResultSubScreenView subView;
        [SerializeField] CameraScreenView cameraScreenView;

        void Awake()
        {
            manager.OnResultSceneStarted
                .Subscribe(_ =>
                {
                    mainView.Initialize();
                    subView.Initialize();
                    cameraScreenView.Initialize();
                    StartCoroutine(cameraScreenView.FadeOut());
                })
                .AddTo(this);
            cameraScreenView.OnFadeOutFinished
                .First()
                .Subscribe(_ =>
                {
                    if (manager.Result == Utilities.Results.Success)
                    {
                        mainView.ShowImageSlide();
                    }
                    else
                    {
                        mainView.ShowGameOverText();
                        subView.ShowContent(manager.Result);
                        manager.ActivateRay();
                    }
                });
            mainView.OnThankYouShowFinished
                .Subscribe(_ =>
                {
                    StartCoroutine(cameraScreenView.FadeIn());
                    cameraScreenView.OnFadeInFinished
                        .Subscribe(_ =>
                        {
                            manager.ChangeToTitleScene();
                        })
                        .AddTo(this);
                })
                .AddTo(this);
            subView.OnGoTitleButtonClicked
                .Subscribe(_ =>
                {
                    StartCoroutine(cameraScreenView.FadeIn());
                    cameraScreenView.OnFadeInFinished
                        .Subscribe(_ =>
                        {
                            manager.ChangeToTitleScene();
                        })
                        .AddTo(this);
                })
                .AddTo(this);
            subView.OnRetryButtonClicked
                .Subscribe(_ =>
                {
                    StartCoroutine(cameraScreenView.FadeIn());
                    cameraScreenView.OnFadeInFinished
                        .Subscribe(_ =>
                        {
                            manager.ChangeToSashiireScene();
                        })
                        .AddTo(this);
                })
                .AddTo(this);

        }
    }
}
