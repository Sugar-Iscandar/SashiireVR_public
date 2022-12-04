using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Deliveries
{
    //マネージャーからの情報を元に起床タイミングを判定する
    //Playerと女の子の距離を計測する
    public class WakeUpJudgement : MonoBehaviour
    {
        [SerializeField]
        [Header("起きてしまう衝撃力")]
        float gameOverValueOfImpulse;
        [SerializeField] 
        [Range(0f, 1.0f)]
        [Header("起きてしまう歩く速度")] 
        float gameOverValueOfWalkSpeed;
        [SerializeField]
        [Header("女の子が反応する距離")]
        float distanceWitchGirlResponsive;
        [Space(10)]
        [SerializeField] Transform playerTransform;
        [SerializeField] Transform girlTransform;
        float distanceBitweenPlayerAndGirl;
        Subject<Unit> judgementSubject = new Subject<Unit>();

        public IObservable<Unit> OnWakeUp
        {
            get => judgementSubject;
        }

        void Update()
        {
            distanceBitweenPlayerAndGirl = Vector2.Distance(
                new Vector2(
                    playerTransform.position.x,
                    playerTransform.position.z
                ),
                new Vector2(
                    girlTransform.position.x,
                    girlTransform.position.z
                )
            );
        }

        public void JudgWakeUpFromCollisionImpulse(float impulse)
        {
            if (impulse >= gameOverValueOfImpulse)
            {
                judgementSubject.OnNext(Unit.Default);
            }
        }

        public void JudgWakeUpFromWalkSpeed(float magnitude)
        {
            if (distanceBitweenPlayerAndGirl > distanceWitchGirlResponsive) return;

            if (magnitude >= gameOverValueOfWalkSpeed)
            {
                judgementSubject.OnNext(Unit.Default);
            }
        }
    }
}
