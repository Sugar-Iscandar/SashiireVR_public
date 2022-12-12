using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using UniRx;
using System;

namespace Deliveries.View
{
    public class PlayerDialogView : MonoBehaviour
    {
        [SerializeField] float distanceToPlayer = 0.5f;
        [SerializeField] GameObject prefabCanvasPlayerDialog;
        GameObject canvasPlayerDialog;
        GameObject panel;
        Text textMessage;
        VrButton yesButton;
        VrButton noButton;

        //Dispose処理に使用するため公開
        public GameObject CanvasPlayerDialog
        {
            get => canvasPlayerDialog;
        }
        
        public IObservable<Unit> OnYesButtonClicked
        {
            get => yesButton.OnButtonClicked;
        }

        public IObservable<Unit> OnNoButtonClicked
        {
            get => noButton.OnButtonClicked;
        }

        public void ShowPlayerDialog(Vector3 playerPosition, Vector3 playerForward, Quaternion playerRotation)
        {
            //Playerの向いている方向でダイアログ表示
            Vector3 showPosition = playerPosition + playerForward * distanceToPlayer;
            Quaternion showRotation = Quaternion.Euler(0, playerRotation.eulerAngles.y, 0);
            canvasPlayerDialog = Instantiate(prefabCanvasPlayerDialog, showPosition, showRotation);
            
            //各インスタンスを取得
            GetAllChildren();
        }

        public void HidePlayerDialog()
        {
            Destroy(canvasPlayerDialog);
        }

        public void SetContentMessageText(string content)
        {
            textMessage.text = content;
        }

        void GetAllChildren()
        {
            /*
            panel = canvasPlayerDialog.transform.Find("Panel").gameObject;
            textMessage = canvasPlayerDialog.transform.Find("Panel/Text_Message").gameObject.GetComponent<Text>();
            yesButton = canvasPlayerDialog.transform.Find("Panel/VRButton_Yes").gameObject.GetComponent<VrButton>();
            noButton = canvasPlayerDialog.transform.Find("Panel/VRButton_No").gameObject.GetComponent<VrButton>();
            */

            //こっちとどっちが早いかそのうち検証したい

            Transform[] children = canvasPlayerDialog.transform.GetComponentsInChildren<Transform>();
            Debugger.Log("children count = " + children.Length);
            foreach (Transform child in children)
            {
                if (child.gameObject == this.gameObject) continue;

                switch (child.name)
                {
                    case "Panel":
                        panel = child.gameObject;
                        break;
                    case "Text_Message":
                        textMessage = child.gameObject.GetComponent<Text>();
                        break;
                    case "VRButton_Yes":
                        yesButton = child.gameObject.GetComponent<VrButton>();
                        break;
                    case "VRButton_No":
                        noButton = child.gameObject.GetComponent<VrButton>();
                        break;
                }
            }
        }
    }
}
