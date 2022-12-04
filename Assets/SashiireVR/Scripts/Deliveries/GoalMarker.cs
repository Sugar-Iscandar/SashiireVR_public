using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Deliveries
{
    public class GoalMarker : MonoBehaviour
    {
        Subject<Unit> goalSubject = new Subject<Unit>();

        public IObservable<Unit> OnPlayerGoaled
        {
            get => goalSubject;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag != "Player") return;
            goalSubject.OnNext(Unit.Default);
            this.gameObject.SetActive(false);
        }
    }
}

