using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

namespace Utilities.View
{
    public class CameraScreenView : MonoBehaviour
    {
        [SerializeField] Image imageBlack;
        [SerializeField] float timeToCompleteFade = 1.0f;
        float alpha;
        Subject<Unit> fadeOutFinishSubject = new Subject<Unit>();
        //フェード終了後にスタートボタンを押せるようにする
        public IObservable<Unit> OnFadeOutFinished { get => fadeOutFinishSubject; }
        Subject<Unit> fadeInFinishSubject = new Subject<Unit>();
        public IObservable<Unit> OnFadeInFinished { get => fadeInFinishSubject; }

        public void Initialize()
        {
            imageBlack.color = Color.black;
            alpha = 1f;
        }

        public IEnumerator FadeOut()
        {
            float currentTime = 0;
            while (currentTime <= timeToCompleteFade)
            {
                alpha = alpha - currentTime / timeToCompleteFade * Time.deltaTime;
                imageBlack.color = new Color(0, 0, 0, alpha);
                yield return null;
                currentTime += Time.deltaTime;
            }
            alpha = 0f;
            imageBlack.color = new Color(0, 0, 0, alpha);
            fadeOutFinishSubject.OnNext(Unit.Default);
        }

        public IEnumerator FadeIn()
        {
            float currentTime = 0;
            while (currentTime <= timeToCompleteFade)
            {
                alpha = currentTime / timeToCompleteFade;
                imageBlack.color = new Color(0, 0, 0, alpha);
                yield return null;
                currentTime += Time.deltaTime;
            }
            alpha = 1f;
            imageBlack.color = new Color(0, 0, 0, alpha);
            fadeInFinishSubject.OnNext(Unit.Default);
        }
    }
}
