using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;
using UniRx;
using UniRx.Triggers;

namespace Girls
{
    public class FaceController : MonoBehaviour
    {
        VRMBlendShapeProxy blendShapeProxy;

        void Start()
        {
            //ゲーム開始時は寝せておく
            //Update開始以降じゃないとBlendShapeが取得できないらしい
            this.UpdateAsObservable()
                .FirstOrDefault()
                .Subscribe(_ =>
                {
                    blendShapeProxy = GetComponent<VRMBlendShapeProxy>();
                    blendShapeProxy.AccumulateValue(
                        BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink),
                        0.967f
                    );
                    blendShapeProxy.AccumulateValue(
                        BlendShapeKey.CreateFromPreset(BlendShapePreset.Fun),
                        0.204f
                    );
                    blendShapeProxy.Apply();
                });
        }

        public void ChangeFaceExpressionOnWakeUp()
        {
            StartCoroutine(ChangeFace());
        }

        IEnumerator ChangeFace()
        {
            //表情を「あくび」っぽく
            blendShapeProxy.AccumulateValue(
                BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink),
                0f
            );
            blendShapeProxy.AccumulateValue(
                BlendShapeKey.CreateFromPreset(BlendShapePreset.Fun),
                0f
            );
            blendShapeProxy.AccumulateValue(
                BlendShapeKey.CreateFromPreset(BlendShapePreset.Sorrow),
                1.0f
            );
            blendShapeProxy.AccumulateValue(
                BlendShapeKey.CreateFromPreset(BlendShapePreset.Joy),
                0.615f
            );
            blendShapeProxy.Apply();

            yield return new WaitForSeconds(3.0f);

            //通常の表情へ
            blendShapeProxy.AccumulateValue(
                BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink),
                0f
            );
            blendShapeProxy.AccumulateValue(
                BlendShapeKey.CreateFromPreset(BlendShapePreset.Fun),
                0f
            );
            blendShapeProxy.AccumulateValue(
                BlendShapeKey.CreateFromPreset(BlendShapePreset.Sorrow),
                0f
            );
            blendShapeProxy.AccumulateValue(
                BlendShapeKey.CreateFromPreset(BlendShapePreset.Joy),
                0f
            );
            blendShapeProxy.Apply();
        }
    }
}

