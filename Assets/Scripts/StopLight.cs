using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopLight : MonoBehaviour
{
    public static StopLight Instance { get; private set; }

    public float FlashRate = .3f;
    public float FlashTime = 3;

    public Image[] lights;
    public Color[] colors;

    private int lightCounter;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Other Singleton of Spotlight exists, destroy me!");
            Destroy(this);
        }
        else
        {
            Debug.Log("There can only be one!");
            Instance = this;
        }
    }

    private void Start()
    {
        Reset();
    }

    public void Reset()
    {
        lightCounter = 0;

        foreach (var l in lights)
        {
            l.color = Color.black;
        }
    }

    public void UpdateLight()
    {
        lights[lightCounter].color = colors[lightCounter];

        lightCounter++;

        if (lightCounter >= 3)
        {
            //Reset();
            StartCoroutine(FlashForGo());
        }
    }

    IEnumerator FlashForGo()
    {
        var totalTimeFlashing = FlashRate;
        yield return new WaitForSeconds(FlashRate);

        while (totalTimeFlashing < FlashTime)
        {
            foreach (var l in lights)
            {
                l.color = Color.black;
            }
            yield return new WaitForSeconds(FlashRate);
            foreach (var l in lights)
            {
                l.color = Color.green;
            }
            totalTimeFlashing += FlashRate;
            yield return new WaitForSeconds(FlashRate);
            totalTimeFlashing += FlashRate;
        }
        foreach (var l in lights)
        {
            l.gameObject.SetActive(false);
        }
    }
}
