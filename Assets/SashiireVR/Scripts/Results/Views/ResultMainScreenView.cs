using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

namespace Results.View
{
    public class ResultMainScreenView : MonoBehaviour
    {
        [SerializeField] Image imageMain;
        [SerializeField] Sprite[] successImage;
        [SerializeField] Text textMain;
        [SerializeField] float timeToCompleteImageFade = 1.0f;
        [SerializeField] float imageWaitTime = 1.0f;
        float imageAlpha;
        Subject<Unit> thankYouShowFinishSubject = new Subject<Unit>();
        public IObservable<Unit> OnThankYouShowFinished { get => thankYouShowFinishSubject; }

        public void Initialize()
        {
            //各種UIは透明にしておく
            imageMain.color = new Color(1,1,1,0);
            textMain.color = new Color(1,1,1,0);
            imageMain.gameObject.SetActive(false);
            textMain.gameObject.SetActive(false);
            imageAlpha = 0f;
        }
        
        public void ShowImageSlide()
        {
            imageMain.gameObject.SetActive(true);
            //画像をスライドショーのように3枚ほど表示する
            StartCoroutine(ImageSlideShow());
        }

        public void ShowGameOverText()
        {
            textMain.text = "Game Over";
            textMain.color = new Color(1,1,1,1);
            textMain.gameObject.SetActive(true);
        }

        IEnumerator ImageSlideShow()
        {
            for (int i = 0; i < successImage.Length; i++)
            {
                imageMain.sprite = successImage[i];
                yield return FadeIn<Image>(imageMain);
                yield return new WaitForSeconds(imageWaitTime);
                yield return FadeOut<Image>(imageMain);
            }
            Observable.Timer(TimeSpan.FromSeconds(1))
                .Subscribe(_ =>
                {
                    StartCoroutine(ShowThankYouText());
                });
        }

        IEnumerator ShowThankYouText()
        {
            textMain.text = "Thank you for playing";
            textMain.gameObject.SetActive(true);
            yield return FadeIn<Text>(textMain);
            yield return new WaitForSeconds(3.0f);
            yield return FadeOut<Text>(textMain);
            Observable.Timer(TimeSpan.FromSeconds(1))
                .Subscribe(_ =>
                {
                    thankYouShowFinishSubject.OnNext(Unit.Default);
                });
        }

        IEnumerator FadeIn<T>(T obj) where T : Graphic
        {
            float currentTime = 0;
            while (currentTime <= timeToCompleteImageFade)
            {
                imageAlpha = currentTime / timeToCompleteImageFade;
                obj.color = new Color(1, 1, 1, imageAlpha);
                yield return null;
                currentTime += Time.deltaTime;
            }
            imageAlpha = 1f;
            obj.color = new Color(1, 1, 1, imageAlpha);
        }

        IEnumerator FadeOut<T>(T obj) where T : Graphic
        {
            float currentTime = 0;
            while (currentTime <= timeToCompleteImageFade)
            {
                imageAlpha = imageAlpha - currentTime / timeToCompleteImageFade * Time.deltaTime;
                obj.color = new Color(1, 1, 1, imageAlpha);
                yield return null;
                currentTime += Time.deltaTime;
            }
            imageAlpha = 0f;
            obj.color = new Color(1, 1, 1, imageAlpha);
        }
    }
}
