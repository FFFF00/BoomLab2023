using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Text;
using System;

public class GameManager : MonoSingletion<GameManager>
{
    // Start is called before the first frame update
    public string currStage;
    public string nextStage;

    private bool stop = false;
    void Start()
    {
        //非测试时注释掉
        //#if UNITY_EDITOR
        LoadSence();
        //#endif
        AddListener();

    }

    private void AddListener()
    {
        MessageManager.Instance.AddListener(MessageManager.MessageId.PlayerDeadPre, DeathCount);
        MessageManager.Instance.AddListener(MessageManager.MessageId.PlayerDead, ReloadSence);

        MessageManager.Instance.AddListener(MessageManager.MessageId.NextStage, LoadNextSence);
        MessageManager.Instance.AddListener(MessageManager.MessageId.ReplayEnd, ActiveSence);

        MessageManager.Instance.AddListener(MessageManager.MessageId.UnitCreate, RegisterObject);
        //MessageManager.Instance.AddListener(MessageManager.MessageId.UnitDistory, RemoveObject);

        MessageManager.Instance.AddListener(MessageManager.MessageId.IconCollisionBegin, GetIcon);
    }

    public class Data
    {
        public Vector2 pos;
        public float speedVector;
        public float facing4X;
        public float facing4Y;
        public int state;
    }

    public Stack<Data> stack = new Stack<Data>();
    private void FixedUpdate()
    {
        //放在GameManger运行
        if (!stop)
            CoroutineManager.Instance.FixedUpdateCoroutine();
    }

    #region 暂停

    HashSet<string> PauseSet = new HashSet<string>();

    public void Stop()
    {
        stop = true;
    }

    public void Begin()
    {
        stop = false;
    }

    public void Pause(String key)
    {
        PauseSet.Add(key);
        Time.timeScale = 0;
        //Debug.Log("暂停   " + key + "   " + Time.frameCount);
    }

    public void Begin(String key)
    {
        PauseSet.Remove(key);
        if (PauseSet.Count == 0)
        {
            Time.timeScale = 1;
            //Debug.Log("开启   " + key + "   " + Time.frameCount);
        }
    }

    public void PauseReset()
    {
        PauseSet.Clear();
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }

    #endregion

    #region 统计

    private int iconCount;
    private int tempCount;
    private int deathCount = 0;
    public Dictionary<String, int> deathCountDic = new Dictionary<string, int>();
    public Dictionary<String, float> currTimeDic = new Dictionary<string, float>();
    public Dictionary<String, float> totalTimeDic = new Dictionary<string, float>();
    public float totalTime;
    private float currTimer;

    private void GetIcon(Message message)
    {
        tempCount++;
    }

    private void CountClear()
    {
        tempCount = 0;
    }

    public float GetTotalTime()
    {
        return totalTime;
    }

    private void DeathCount(Message message = null)
    {
        currStage = SceneManager.GetActiveScene().name;
        if (!deathCountDic.ContainsKey(currStage))
        {
            deathCountDic.Add(currStage, 1);
        }
        else
        {
            deathCountDic[currStage]++;
        }
    }

    #endregion

    #region 场景
    public AsyncOperation senceLoad;
    public AsyncOperation senceUnload;
    private bool senceLock = true;

    public void ReloadSence(Message message = null)
    {
        currStage = SceneManager.GetActiveScene().name;
        //Debug.Log("reload前场景：" + currStage);
        senceUnload = SceneManager.UnloadSceneAsync(currStage, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        senceLoad = SceneManager.LoadSceneAsync(currStage, LoadSceneMode.Additive);
        senceLoad.completed += CompletLoadScene;
        //重新录制
        //MessageManager.Instance.SendMessage(MessageManager.MessageId.RecordEnd);
        ActiveSence();
    }
    public void ReloadSence()
    {
        ReloadSence(null);
    }

    public void LoadSence()
    {
        senceLoad = SceneManager.LoadSceneAsync(currStage, LoadSceneMode.Additive);
        senceLoad.allowSceneActivation = true;
        senceLoad.completed += CompletLoadScene;
        ActiveSence();
    }

    public void LoadNextSence(Message message)
    {
        deathCount = 0;
        //延迟一帧，触发器碰撞当帧不能立即销毁对象
        StartCoroutine(LoadNextSenceCoroutine(message));
    }

    private IEnumerator LoadNextSenceCoroutine(Message message)
    {
        if (senceLock)
        {
            senceLock = false;
            yield return null;

            //要在卸载场景前完成统计
            currStage = SceneManager.GetActiveScene().name;
            currTimeDic.Add(currStage, Time.time - currTimer);

            totalTime += currTimeDic[currStage];
            if (!totalTimeDic.ContainsKey(currStage))
            {
                totalTimeDic.Add(currStage, Time.time - currTimer);
            }
            else
            {
                totalTimeDic[currStage] += Time.time - currTimer;
            }

            if (!deathCountDic.ContainsKey(currStage))
            {
                deathCountDic.Add(currStage, 0);
            }

            if (message.replay == true)
            {
                senceLoad.allowSceneActivation = false;
                MessageManager.Instance.SendMessage(MessageManager.MessageId.RecordEnd);
                MessageManager.Instance.SendMessage(MessageManager.MessageId.ReplayBegin);
            }
            else
            {
                ActiveSence();
                //重新录制
                MessageManager.Instance.SendMessage(MessageManager.MessageId.RecordEnd);
            }

            //要在卸载场景前完成统计
            //Debug.Log("unload后场景：" + currStage);
            nextStage = message.nextStageName;
            senceUnload = SceneManager.UnloadSceneAsync(currStage, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
            senceLoad = SceneManager.LoadSceneAsync(nextStage, LoadSceneMode.Additive);
            senceLoad.completed += CompletLoadScene;
        }
        //收集到的icon计数
        iconCount += tempCount;
    }

    public void ActiveSence(Message message = null)
    {
        senceLoad.allowSceneActivation = true;
        CoroutineManager.Instance.StopAllCoroutine();
        StopAllCoroutines();
    }

    private void CompletLoadScene(AsyncOperation a)
    {
        currTimer = Time.time;
        senceLock = true;
        //unload当前场景
        Debug.Log("unload前场景：" + currStage);
        if (currStage != nextStage)
        {
            //senceUnload = SceneManager.UnloadSceneAsync(currStage, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
            currStage = nextStage;

            //ObjectRemove4Recording();
            ClearObjectCache();
            //MessageManager.Instance.SendMessage(MessageManager.MessageId.ClockBegin);        
        }

        Scene scene = SceneManager.GetSceneByName(currStage);
        SceneManager.SetActiveScene(scene);
        MessageManager.Instance.SendMessage(MessageManager.MessageId.CompleteLoadStage);
        MessageManager.Instance.SendMessage(MessageManager.MessageId.RecordBegin);
        MessageManager.Instance.SendMessage(MessageManager.MessageId.PlayerReset);

        CountClear();
        GameLogic.Instance.NextLevel();
    }

    #endregion

    #region 管理
    private Dictionary<Vector3Int, Jigsaw> senceGameObjectCacheKV = new ();
    public Dictionary<Vector3Int, Jigsaw> SceneGOCacheKV { get => senceGameObjectCacheKV; }
    private Dictionary<Jigsaw, Vector3Int> senceGameObjectCacheVK = new ();

    public void RegisterObject(Message message)
    {
        Jigsaw jigsaw = message.gameObject.GetComponent<Jigsaw>();
        RegisterObject(jigsaw);
    }

    public void RegisterObject(Jigsaw jigsaw)
    {
        senceGameObjectCacheKV.Remove(jigsaw.CellCoord);
        senceGameObjectCacheKV.Add(jigsaw.CellCoord, jigsaw);
        senceGameObjectCacheVK.Remove(jigsaw);
        senceGameObjectCacheVK.Add(jigsaw, jigsaw.CellCoord);
    }

    public void UpdateObject(Jigsaw jigsaw)
    {
        Vector3Int oldpos = senceGameObjectCacheVK[jigsaw];
        if (oldpos.Equals(jigsaw.CellCoord))
            return;
        Jigsaw target = senceGameObjectCacheKV[jigsaw.CellCoord];
        if (target.fixedPos)
            return;

        target.MoveTo(oldpos);

        GameLogic.Instance.PlayActionTextAndAudio(PlayerAction.moveTile);

        RegisterObject(jigsaw);
        RegisterObject(target);
    }

    public bool CheckLegalTargetPos(Vector3Int targetPos)
    {
        Jigsaw target = senceGameObjectCacheKV[targetPos];
        return !target.fixedPos;
    }

    public void ClearObjectCache()
    {
        senceGameObjectCacheKV.Clear();
        senceGameObjectCacheVK.Clear();
    }
    #endregion

    #region 工具
    public void WriteFile(string fileName, string content = null)
    {
        StreamWriter streamWriter;
        if (IsFileExists(fileName))
        {
            streamWriter = new StreamWriter(new FileStream(fileName, FileMode.Create, FileAccess.Write));
        }
        else
        {
            streamWriter = File.CreateText(fileName);
        }
        streamWriter.Write(content);
        streamWriter.Close();
        //Debug.Log("close() : " + fileName);
    }

    public string ReadFile(string fileName)
    {
        if (!IsFileExists(fileName))
            return "";
        StreamReader streamReader = new StreamReader(File.OpenRead(fileName));
        string content = streamReader.ReadToEnd();
        streamReader.Close();
        return content;
    }

    public void CreateDirectory(string fileName)
    {
        //文件夹存在则返回
        if (IsDirectoryExists(fileName))
            return;
        Directory.CreateDirectory(fileName);
    }

    public bool IsFileExists(string fileName)
    {
        return File.Exists(fileName);
    }

    public bool IsDirectoryExists(string fileName)
    {
        return Directory.Exists(fileName);
    }

    public string CurrDataTimeString()
    {
        int hour = DateTime.Now.Hour;
        int minute = DateTime.Now.Minute;
        int second = DateTime.Now.Second;
        int year = DateTime.Now.Year;
        int month = DateTime.Now.Month;
        int day = DateTime.Now.Day;

        //格式化显示当前时间
        return string.Format("{0:D2}:{1:D2}:{2:D2} " + "{3:D4}/{4:D2}/{5:D2}", hour, minute, second, year, month, day);
    }

    private string Bytes2String(byte[] data)
    {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < data.Length; i++)
        {
            builder.Append(data[i].ToString("x2"));
        }
        return builder.ToString();
    }
    #endregion
}
