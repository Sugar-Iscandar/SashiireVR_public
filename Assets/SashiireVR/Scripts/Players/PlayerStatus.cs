using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players
{
    //Playerの状態を計算・保持・外部へ公開
    public class PlayerStatus : MonoBehaviour
    {
        public Vector3 PlayerCameraPosition
        {
            get => transform.position;
        }
        public Vector3 PlayerCameraForward
        {
            get => transform.forward;
        }

        public Quaternion PlayerCameraRotation
        {
            get => transform.rotation;
        }
    }
}


