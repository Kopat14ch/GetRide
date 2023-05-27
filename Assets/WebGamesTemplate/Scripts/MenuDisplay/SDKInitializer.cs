using System;
using System.Collections;
using Agava.YandexGames;
using UnityEngine;

public class SDKInitializer : MonoBehaviour
{
    public event Action Initialized;

    private IEnumerator Start()
    {
        yield return YandexGamesSdk.Initialize(OnYandexSDKInitialize);
    }

    private void OnYandexSDKInitialize()
    {
        Initialized?.Invoke();
    }
}
