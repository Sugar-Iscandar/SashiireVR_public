using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;

namespace Girls
{
    public class FaceController : MonoBehaviour
    {
        VRMBlendShapeProxy blendShapeProxy;
        bool callOnce;

        void Start()
        {
            callOnce = false;
        }

        //ゲーム開始時は寝せておく
        //Update開始以降じゃないとBlendShapeが取得できないらしい
        void Update()
        {
            if (!callOnce)
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
                callOnce = true;
            }
        }
    }
}

