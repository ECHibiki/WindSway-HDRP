using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    //public int application_fps = 30;
    void Start()
    {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = application_fps;
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Text>().text = "FPS: " + 1 / Time.unscaledDeltaTime;
    }
}
