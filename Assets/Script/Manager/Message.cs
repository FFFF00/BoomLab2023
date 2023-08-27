using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message
{
    public string nextStageName { set; get; }
    public bool replay { set; get; }
    public int taskId { set; get; }
    public int memeId { set; get; }
    public int num { set; get; }

    public float f { set; get; }
    public Vector2 pos { set; get; }
    public Vector2 pos2 { set; get; }
    public Vector2 pos3 { set; get; }
    public GameObject gameObject { set; get; }
}
