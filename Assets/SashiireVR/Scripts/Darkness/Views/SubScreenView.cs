using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using UniRx;
using System;

namespace Darkness.View
{
    public class SubScreenView : MonoBehaviour
    {
        [SerializeField] Text textMain;
        [SerializeField] VrButton buttonNext;
        Text textNextButton;
        [SerializeField] VrButton buttonBack;
        List<string> synopses = new List<string>();
        int pageNum;
        int currentPage;
        Subject<Unit> synopsisFinishSubject = new Subject<Unit>();

        public IObservable<Unit> OnNextButtonClicked
        {
            get => buttonNext.OnButtonClicked;
        }
        public IObservable<Unit> OnBackButtonClicled
        {
            get => buttonBack.OnButtonClicked;
        }
        public IObservable<Unit> OnSynopsisFinished
        {
            get => synopsisFinishSubject;
        }

        public void Initialized()
        {
            textMain.gameObject.SetActive(false);
            pageNum = 0;
            currentPage = 0;
            textNextButton = buttonNext.transform.GetComponentInChildren<Text>();

            buttonNext.OnButtonClicked
                .Subscribe(_ =>
                {
                    GoToNextPage();
                })
                .AddTo(this);
            buttonBack.OnButtonClicked
                .Subscribe(_ =>
                {
                    GoToBackPage();
                })
                .AddTo(this);
            
            InactiveButtons();
        }

        public void SetContentMainText(string content)
        {
            synopses.AddRange(content.Split('；'));
            pageNum = synopses.Count;
            currentPage = 0;
            textMain.text = synopses[currentPage];
        }

        public void ShowMainText()
        {
            textMain.gameObject.SetActive(true);
        }

        public void ActiveButtons()
        {
            BoxCollider nextButtonCollider = buttonNext.gameObject.GetComponent<BoxCollider>();
            BoxCollider backButtonCollider = buttonBack.gameObject.GetComponent<BoxCollider>();
            nextButtonCollider.enabled = true;
            backButtonCollider.enabled = true;
        }

        public void InactiveButtons()
        {
            BoxCollider nextButtonCollider = buttonNext.gameObject.GetComponent<BoxCollider>();
            BoxCollider backButtonCollider = buttonBack.gameObject.GetComponent<BoxCollider>();
            nextButtonCollider.enabled = false;
            backButtonCollider.enabled = false;
        }

        void GoToNextPage()
        {
            //最後のページなら
            if (currentPage == pageNum -1)
            {
                //すべてのあらすじを読み終わった
                synopsisFinishSubject.OnNext(Unit.Default);
                return;
            }
            //最後のページに変わるなら
            else if (currentPage == pageNum -2)
            {
                textNextButton.text = "スタート！";
            }
            currentPage++;
            textMain.text = synopses[currentPage];
        }

        void GoToBackPage()
        {
            if (currentPage == 0) return;
            currentPage--;
            textMain.text = synopses[currentPage];
        }
    }
}
