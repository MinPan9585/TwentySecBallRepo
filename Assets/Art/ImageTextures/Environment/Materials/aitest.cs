using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine;
using UnityEngine.UI;

public class ImageSwing : MonoBehaviour
{
    public float amplitude = 30f;  // 摆动的幅度
    public float frequency = 1f;   // 摆动的频率

    private RectTransform rectTransform;
    private float elapsedTime;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        float angle = Mathf.Sin(elapsedTime * frequency) * amplitude;
        rectTransform.rotation = Quaternion.Euler(0, 0, angle);
    }
}