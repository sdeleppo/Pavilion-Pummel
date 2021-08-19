using UnityEngine;
using System.Collections;

public class LightShow : MonoBehaviour
{
    public Light lt;
    Color color0 = Color.red;
    Color color1 = Color.blue;
    Color color2 = Color.yellow;
    Color color3 = Color.magenta;
    Color color4 = Color.yellow;
    float duration = 1.0f;
    public bool light1;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float t = Mathf.PingPong(Time.time, duration) / duration;
        if (light1)
            lt.color = Color.Lerp(color0, color3, t);
        else
            lt.color = Color.Lerp(color4, color1, t);
    }
}