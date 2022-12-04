using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystems
{
    public enum Scenes
    {
        Title,
        Darkness,
        Sashiire,
        Result
    }

    public class SceneChanger : MonoBehaviour
    {
        static SceneChanger instance;

        public static SceneChanger Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject gameObj = new GameObject("SceneChanger");
                    instance = gameObj.AddComponent<SceneChanger>();
                    DontDestroyOnLoad(gameObj);
                }
                return instance;
            }
        }

        public void ChangeScene(Scenes scene)
        {
            switch (scene)
            {
                case Scenes.Title:
                    SceneManager.LoadScene("TitleScene");
                    break;
                case Scenes.Darkness:
                    SceneManager.LoadScene("DarknessScene");
                    break;
                case Scenes.Sashiire:
                    SceneManager.LoadScene("SashiireScene");
                    break;
                case Scenes.Result:
                    SceneManager.LoadScene("ResultScene");
                    break;
            }
        }
    }
}
