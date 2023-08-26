using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 按秒等待
/// </summary>
public class WaitForSeconds : IWait
{
    float timer = 0f;
    public bool log = false;

    public string name = "common";
    
    public WaitForSeconds(float seconds)
    {
        timer = seconds;
    }


    public WaitForSeconds(float seconds, string str, bool b)
    {
        timer = seconds;
        log = b;
        name = str;
    }

    public bool Tick()
    {
        timer -= Time.fixedDeltaTime;
        if (log)
        {
            Debug.Log("计时：" + timer + "    " + Time.frameCount);
        }
        return timer <= 0;
    }
}
