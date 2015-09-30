using UnityEngine;
using System.Collections;

/// <summary>
/// 整数型时间计数器
/// </summary>
public class JTTimeTask : MonoBehaviour {
    float runFloatTime;
    int runIntTime;
    System.Action<int> callback;
    bool isStop;
    protected int step = 1;

    /*///////////////////////test///////////////////////
    JTTimeTask task;
    public void Awake()
    {
        task = JTTimeTask.Init(OnTime, 3);
        task.start();
    }

    void OnTime(int time)
    {
        Debug.LogError(time);
    }
    ///////////////////////////////////////////////*/

    /// <summary>
    /// 步长默认为step = 1时间的计时器，一秒下发一次
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static JTTimeTask Init(System.Action<int> time) {
        return Init(time, 1);
    }

    /// <summary>
    /// 步长为step时间的计时器，step秒下发一次
    /// </summary>
    /// <param name="time"></param>
    /// <param name="step"></param>
    /// <returns></returns>
    public static JTTimeTask Init(System.Action<int> time, int step)
    {
        if (step <= 0) {
            Debug.LogError("need step > 0");
            return null;
        }
        GameObject go = new GameObject("TimeTask");
        JTTimeTask task = go.AddComponent<JTTimeTask>();
        task.onBegin(time);
        task.step = step;
        return task;
    }

    public void begin() {
        runFloatTime = 0;
        runIntTime = 0;
        flag = -1;
        isStop = false;
    }

    void onBegin(System.Action<int> time)
    {
        runFloatTime = 0;
        runIntTime = 0;
        flag = -1;
        callback = time;
    }

    void Update()
    {
        TimeTask();
    }

    /// <summary>
    /// 设置计时器步长
    /// </summary>
    /// <param name="step"></param>
    public void SetStep(int step) {
        if (step <= 0)
        {
            Debug.LogError("need step > 0");
            return;
        }
        this.step = step;
    }

    int flag = -1;
    void TimeTask()
    {
        if (isStop) return;
        runFloatTime += Time.deltaTime;
        runIntTime = ((int)runFloatTime);
        if (callback != null && runIntTime % step ==0 && runIntTime != flag)
        {
            callback(runIntTime / step);
            flag = runIntTime;
        }
    }

    public void stop() {
        isStop = true;
        runFloatTime = 0;
        runIntTime = 0;
        flag = -1;
    }

    public void Clear() {
        Destroy(gameObject);
    }
}
