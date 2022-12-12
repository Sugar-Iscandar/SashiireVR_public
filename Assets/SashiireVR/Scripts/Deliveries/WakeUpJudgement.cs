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
        //起きてしまう衝撃力
        [SerializeField] float gameOverValueOfImpulse;
        //起きてしまう歩行速度
        [SerializeField] [Range(0f, 1.0f)] float gameOverValueOfWalkSpeed;
        //女の子が反応する距離
        [SerializeField] float distanceWitchGirlResponsive;
        [SerializeField] Transform playerTransform;
        [SerializeField] Transform girlTransform;
        float distanceBitweenPlayerAndGirl;
        Subject<Unit> judgementSubject = new Subject<Unit>();
        public IObservable<Unit> OnWakeUp { get => judgementSubject; }

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
