using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public enum Results
    {
        Success,
        GirlWokeUp,
        GiveUp,
        TimeUp
    }
    public class ResultRecorder : MonoBehaviour
    {
        static ResultRecorder instance;

        public static ResultRecorder Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject gameObj = new GameObject("ResultRecorder");
                    instance = gameObj.AddComponent<ResultRecorder>();
                    DontDestroyOnLoad(gameObj);
                }
                return instance;
            }
        }

        Results result;

        public Results Result
        {
            get => result;
            set => result = value;
        }
    }
}
