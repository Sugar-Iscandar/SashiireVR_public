using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using UniRx;
using System;

namespace Results.View
{
    public class ResultSubScreenView : MonoBehaviour
    {
        [SerializeField] Text textMain;
        [SerializeField] VrButton buttonGoTitle;
        [SerializeField] VrButton buttonRetry;
        public IObservable<Unit> OnGoTitleButtonClicked { get => buttonGoTitle.OnButtonClicked; }
        public IObservable<Unit> OnRetryButtonClicked { get => buttonRetry.OnButtonClicked; }

        public void Initialize()
        {
            textMain.gameObject.SetActive(false);
            buttonGoTitle.gameObject.SetActive(false);
            buttonRetry.gameObject.SetActive(false);
        }

        public void ShowContent(Utilities.Results result)
        {
            if (result == Utilities.Results.GirlWokeUp)
            {
                textMain.text = "やつはちゃんを起こしてしまった！\n颯爽と立ち去る計画は失敗だ…";
                textMain.gameObject.SetActive(true);
                buttonGoTitle.gameObject.SetActive(true);
                buttonRetry.gameObject.SetActive(true);
            }
            else if (result == Utilities.Results.GiveUp)
            {
                textMain.text = "やつはちゃんに差し入れを渡すのを諦めた。\n絶好の機会だったのに…";
                textMain.gameObject.SetActive(true);
                buttonGoTitle.gameObject.SetActive(true);
                buttonRetry.gameObject.SetActive(true);
            }
        }
    }
}

