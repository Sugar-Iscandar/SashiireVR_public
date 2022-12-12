using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;
using UniRx;
using System;
using GameSystems;

namespace Darkness
{
    public class DarknessManager : MonoBehaviour
    {
        [SerializeField] Hand leftHand;
        [SerializeField] Hand rightHand;
        Subject<string> darknessStartSubject = new Subject<string>();

        public IObservable<string> OnDarknessStarted
        {
            get => darknessStartSubject;
        }

        void Start()
        {
            darknessStartSubject.OnNext(LoadSynopsisText());
            leftHand.Initialize();
            rightHand.Initialize();
            leftHand.IsRayCastActive = true;
            rightHand.IsRayCastActive = true;
        }

        string LoadSynopsisText()
        {
            TextAsset textAsset = new TextAsset();
            textAsset = Resources.Load("SynopsisText", typeof(TextAsset)) as TextAsset;
            return textAsset.text;
        }

        public void ChangeToSashiireScene()
        {
            SceneChanger.Instance.ChangeScene(Scenes.Sashiire);
        }
    }
}
