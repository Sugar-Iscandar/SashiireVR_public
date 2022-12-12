using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

namespace Players
{
    public class HandRay : MonoBehaviour
    {
        [SerializeField] float distanceRayReaches = 1.0f;
        IClickable iClickable = null;
        IClickable previousIClickabke = null;
        LineRenderer lineRenderer;
        bool isRayCastActive = false;
        ReactiveProperty<bool> isAimed = new ReactiveProperty<bool>();

        public bool IsRayCastActive
        {
            get => isRayCastActive;
            set
            {
                if (value == true)
                {
                    lineRenderer.enabled = true;
                    isRayCastActive = true;
                }
                else
                {
                    lineRenderer.enabled = false;
                    isRayCastActive = false;
                }
            }
        }

        public void Initialize()
        {
            lineRenderer = GetComponent<LineRenderer>();

            this.UpdateAsObservable()
                .Where(_ => isRayCastActive)
                .Subscribe(_ =>
                {
                    UpdateRaycast();
                });

            isAimed.Skip(1).Subscribe(flag =>
            {
                if (flag)
                {
                    iClickable.OnAimed();
                    Debugger.Log("ボタンを狙った");
                }
                else
                {
                    previousIClickabke.OnDeflected();
                    Debugger.Log("ボタンを狙うのやめた");
                }
            })
            .AddTo(this);
            
        }

        void UpdateRaycast()
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, -transform.up);
            
            lineRenderer.SetPosition(0, ray.origin);
            //Rayを飛ばして何かにヒットしたら
            if (Physics.Raycast(ray, out hit, distanceRayReaches))
            {
                lineRenderer.SetPosition(1, hit.point);

                //そのオブジェクトはClickableを持っているか
                iClickable = hit.collider.gameObject.GetComponent<IClickable>();
                //持っていたら
                if (iClickable != null)
                {
                    isAimed.Value = true;
                    previousIClickabke = iClickable;
                }
                //持っていなかったら
                else
                {
                    isAimed.Value = false;
                }
            }
            else
            {
                lineRenderer.SetPosition(1, ray.origin + (ray.direction * distanceRayReaches));
                iClickable = null;
                isAimed.Value = false;
            }
        }

        public void ClickButton()
        {
            if (iClickable == null) return;
            iClickable.OnClicked();
        }
    }
}

