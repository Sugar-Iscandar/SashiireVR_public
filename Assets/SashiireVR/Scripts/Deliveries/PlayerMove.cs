using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UniRx;
using System;

namespace Deliveries
{
    //プレイヤーの「移動」を制御するクラス
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] float speed;
        [SerializeField] float perspectiveChangeValue;
        Vector2 inputValue;
        float cameraAngle;
        CharacterController characterController;
        ReactiveProperty<float> inputMagnitude = new ReactiveProperty<float>();

        public IReadOnlyReactiveProperty<float> InputMagnitude => inputMagnitude;

        void Start()
        {
            characterController = GetComponent<CharacterController>();
        }

        void Update()
        {
            Vector3 directionMove = new Vector3(inputValue.x, 0, inputValue.y);
            cameraAngle = Camera.main.transform.eulerAngles.y;
            directionMove = Quaternion.Euler(0, cameraAngle, 0) * directionMove;
            Vector3 move = directionMove * Time.deltaTime * speed;
            characterController.Move(move);
        }

        //InputSystemにより呼ばれる
        public void MovePlayer(InputAction.CallbackContext context)
        {
            inputValue = context.ReadValue<Vector2>();
            inputMagnitude.Value = inputValue.magnitude;
        }

        //InputSystemにより呼ばれる
        //TODO: スティックが縁に当たった時に実行するようにしたい
        public void ChangePerspective(InputAction.CallbackContext context)
        {
            if (!context.started) return;
            Vector2 input = context.ReadValue<Vector2>();

            if (input.x > 0)
            {
                transform.Rotate(0, perspectiveChangeValue, 0);
            }
            else
            {
                transform.Rotate(0, -perspectiveChangeValue, 0);
            }
        }
    }
}

