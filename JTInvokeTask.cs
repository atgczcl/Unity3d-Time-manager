using UnityEngine;
using System.Collections;

/// <summary>
/// 重复定时器默认无限重复
/// </summary>
public class JTInvokeTask : MonoBehaviour {
    float time = 1;
    float delayTime;
    System.Action callback;
    bool isRepeat;
    int repeatCount = -1;

    /// <summary>
    /// 延迟定时调用
    /// </summary>
    /// <param name="time"></param>
    /// <param name="isBeginNow"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public static JTInvokeTask InitOnce(float time, bool isBeginNow, System.Action callback)
    {
        return InitRepeat(time, 0, false, 1, isBeginNow, callback);
    }

    /// <summary>
    /// 指定重复次数的调用
    /// </summary>
    /// <param name="time"></param>
    /// <param name="repeadDelayTime"></param>
    /// <param name="repeatCount"></param>
    /// <param name="isBeginNow"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public static JTInvokeTask InitCount(float time, float repeadDelayTime, int repeatCount, bool isBeginNow, System.Action callback)
    {
        return InitRepeat(time, repeadDelayTime, true, repeatCount, isBeginNow, callback);
    }

    /// <summary>
    /// repeatCount = -1 无限重复
    /// </summary>
    /// <param name="time"></param>
    /// <param name="repeadDelayTime"></param>
    /// <param name="isRepeat"></param>
    /// <param name="repeatCount"></param>
    /// <param name="isBeginNow"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public static JTInvokeTask InitRepeat(float time, float repeadDelayTime, bool isRepeat, int repeatCount, bool isBeginNow, System.Action callback)
    {

        if (repeatCount == 0)
        {
            Debug.LogError("repeat count is 0! can't begin!");
            return null;
        }

        GameObject go = new GameObject("JTInvokeTask");
        JTInvokeTask task = go.AddComponent<JTInvokeTask>();
        task.callback = callback;
        task.time = time;
        task.delayTime = repeadDelayTime;
        task.isRepeat = isRepeat;
        task.repeatCount = repeatCount;
        if (isBeginNow) task.OnBegin();
        return task;
    }

    public void begin() {
        OnBegin();
    }

    void OnBegin() {
        if (IsInvoking("OnInvoke")) CancelInvoke("OnInvoke");

        if (isRepeat)
        {
            InvokeRepeating("OnInvoke", delayTime, time);
        }
        else {
            Invoke("OnInvoke", time);
        }
    }

    void OnInvoke() {
        if (callback != null) {
            callback();
        }
        if (repeatCount >= 0) { 
            repeatCount--;
            if (repeatCount <= 0) Clear();
        }
    }

    /// <summary>
    /// 暂停，之后可以继续使用
    /// </summary>
    public void Stop() {
        CancelInvoke("OnInvoke");
    }

    /// <summary>
    /// 直接清除，不能再次使用
    /// </summary>
    public void Clear() {
        Destroy(gameObject);
    }
}
