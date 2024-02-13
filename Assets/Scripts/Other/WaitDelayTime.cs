using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using Cysharp.Threading.Tasks;

public class WaitDelayTime : MonoBehaviour
{
    public async UniTask WaitTime()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(9f)); // ë“ã@èàóù
    }
}
