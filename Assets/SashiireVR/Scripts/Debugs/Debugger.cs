using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;
using UniRx;

public static class Debugger
{
    //PlayerSettingsにて定義
#if DEBUG_MODE
    const string ConditionalString = "DEBUG_MODE";
#else
    const string ConditionalString = "UNITY_EDITOR";
#endif

    static Subject<string> logOutSubject = new Subject<string>();
    public static IObservable<string> OnLogOut
    {
        get => logOutSubject;
    }

    [Conditional(ConditionalString)]
    public static void Log(object message)
    {
        UnityEngine.Debug.Log(message);
        logOutSubject.OnNext(message.ToString());
    }
}

