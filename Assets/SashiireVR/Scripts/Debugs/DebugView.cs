using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugView : MonoBehaviour
{
    [SerializeField] Text textDebug;
    int debugCount = 0;

    void Awake()
    {
        debugCount = 0;
    }

    public void SetContentDebugText(string content)
    {
        textDebug.text = string.Format("No.{0}: {1}", debugCount, content);
        debugCount++;
    }
}


