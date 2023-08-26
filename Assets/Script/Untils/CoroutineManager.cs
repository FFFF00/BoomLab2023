using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager
{
    private static CoroutineManager _instance = null;
    public static CoroutineManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new CoroutineManager();
            }
            return _instance;
        }
    }

    private LinkedList<IEnumerator> coroutineList = new LinkedList<IEnumerator>();

    public void StartCoroutine(IEnumerator ie)
    {
        coroutineList.AddLast(ie);
    }

    public void StopCoroutine(IEnumerator ie)
    {      
        if (ie != null) {
            coroutineList.Remove(ie);
        }
    }

    public void StopAllCoroutine()
    {
        coroutineList.Clear();
    }

    public double count = 0;
    public void FixedUpdateCoroutine()
    {
        count ++;
        var node = coroutineList.First;
        //Debug.Log("cor + " + count);
        while (node != null)
        {
            IEnumerator ie = node.Value;
            bool ret = true;
            //Debug.Log("cor + " + ie.Current + (ie.Current is IWait) + count);
            if (ie.Current is IWait)
            {
                //if(((WaitForSeconds)ie.Current).log){
                //Debug.Log("tick " + ((WaitForSeconds)ie.Current).name + " COR:" + count + " frame" + Time.frameCount);
                //}
                IWait wait = (IWait)ie.Current;
                //检测等待条件，条件满足，跳到迭代器的下一元素 （IEnumerator方法里的下一个yield）
                if (wait.Tick())
                {
                    ret = ie.MoveNext();
                }
            }
            else
            {
                //Debug.Log("tock " + " COR:" + count + " frame" + Time.frameCount);
                ret = ie.MoveNext();
            }
            //下一个迭代器
            //Debug.Log("next " + node.Next + coroutineList.Count + " COR:" + count + " frame" + Time.frameCount);
            var originNode = node;
            node = node.Next;
            //迭代器没有下一个元素了，删除迭代器（IEnumerator方法执行结束）
            if (!ret)
            {
                //Debug.Log("remove  " + ((WaitForSeconds)ie.Current).name + " COR:" + count + " frame" + Time.frameCount);
                coroutineList.Remove(originNode.Value);
            }
        }
    }
}
