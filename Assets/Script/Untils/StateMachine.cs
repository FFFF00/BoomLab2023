using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    private Dictionary<int, stateStart> startTable = new Dictionary<int, stateStart>();
    private Dictionary<int, stateEnd> endTable = new Dictionary<int, stateEnd>();
    private Dictionary<int, stateInput> inputTable = new Dictionary<int, stateInput>();
    private Dictionary<int, stateUpdate> updateTable = new Dictionary<int, stateUpdate>();
    private Dictionary<int, stateCoroutine> coroutineTable = new Dictionary<int, stateCoroutine>();
    //public Dictionary<int, stateInvoke> invokeTable = new Dictionary<int, stateInvoke>();
    //public Dictionary<int, float> invokeTimeTable = new Dictionary<int, float>();

    public delegate void stateStart();
    public delegate void stateEnd();
    public delegate int stateInput();
    public delegate void stateUpdate();
    public delegate IEnumerator stateCoroutine();
    public delegate void stateChangeCheck();
    //public delegate void stateInvoke();

    private int currStat;
    private int preStat;
    private IEnumerator enumerator;
    //public IEnumerator preEnumerator;

    //public float invokeTimer;

    public void SetCallbacks(int state, stateStart start, stateEnd end, stateInput input,  stateUpdate update, stateCoroutine coroutine, stateChangeCheck check = null)
    {
        startTable.Add(state, start);
        endTable.Add(state, end);
        inputTable.Add(state, input);
        updateTable.Add(state, update);
        coroutineTable.Add(state, coroutine);
        //invokeTable.Add(state, invoke);
        //invokeTimeTable.Add(state, time);
    }

    stateInput input;
    stateUpdate update;
    stateEnd end;
    stateStart start;
    stateCoroutine coroutine;

    //必须在FixedUpdate中调用
    public void FixedRun()
    {
        //stateInvoke invoke;
        //状态发生改变
        checkStatChange();
        //必须在end后，invoke前
        ////////////////可能发生状态改变的部分///////////////
        //根据输入判断状态
        if (inputTable.TryGetValue(currStat, out input))
        {
            if (input != null)
            {
                currStat = input();
            }
        }
        /////////////////////////////////////////////////////////
        //状态发生改变
        checkStatChange();
        ////////////////可能发生状态改变的部分////////////////
        //逻辑部分，可能直接修改状态机状态
        if (updateTable.TryGetValue(currStat, out update))
        {
            update?.Invoke();
        }
        //////////////////////////////////////////////////////////
        //Debug.Log("上一帧状态:" + preStat);
        //Debug.Log("当前状态:" + currStat);
        /////////////////////////////////////////////////////////
        //状态发生改变
        checkStatChange();
    }

    public void checkStatChange()
    {
        if (preStat != currStat)
        {
            if (endTable.TryGetValue(preStat, out end))
            {
                //在调用下一个方法之前，必须更新preStat，否则可能出现死循环
                preStat = currStat;
                end?.Invoke();
            }
            if (startTable.TryGetValue(currStat, out start))
            {
                //在调用下一个方法之前，必须更新preStat，否则可能出现死循环
                preStat = currStat;
                start?.Invoke();
            }
            //停掉上一个状态的协程 开启本状态的协程 
            CoroutineManager.Instance.StopCoroutine(enumerator);
            if (coroutineTable.TryGetValue(currStat, out coroutine))
            {
                if (coroutine != null)
                {
                    //Debug.Log("协程开启:" + currStat +" "+ preStat);
                    enumerator = coroutine.Invoke();
                    CoroutineManager.Instance.StartCoroutine(enumerator);
                }
            }
            preStat = currStat;
            //invokeTimeTable.TryGetValue(currStat, out invokeTimer);
        }
    }

    public int getCurrStat()
    {
        return currStat;
    }

    public void setCurrStat(int state)
    {
        currStat = state;
        checkStatChange();
    }
}
