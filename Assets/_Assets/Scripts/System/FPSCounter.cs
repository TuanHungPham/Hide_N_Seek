using System;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _fpsCounterText;
    private float pollingTime = 1f;
    private float time;
    private int frameCount;

    private void Awake()
    {
        _fpsCounterText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        ShowFPS();
    }

    private void ShowFPS()
    {
        time += Time.deltaTime;

        frameCount++;

        if (time >= pollingTime)
        {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            _fpsCounterText.text = string.Format($"Fps: {frameRate}");

            time -= pollingTime;
            frameCount = 0;
        }
    }
}