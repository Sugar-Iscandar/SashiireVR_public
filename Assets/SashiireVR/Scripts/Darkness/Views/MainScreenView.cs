using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Darkness.View
{
    public class MainScreenView : MonoBehaviour
    {
        [SerializeField] Image imageMain;

        public void Initialized()
        {
            imageMain.gameObject.SetActive(true);
        }

        public void ShowImage()
        {
            imageMain.gameObject.SetActive(true);
        }

        public void HideImage()
        {
            imageMain.gameObject.SetActive(false);
        }
    }
}
