using UnityEngine;
using System.Collections;
using System;
using System.Runtime.CompilerServices;

public class JTInvoke : MonoBehaviour {
    public float delayTime;
    public float repeatTime;

    private Action callback;
    private Action<int> reverseCallBack;
    private Action reverseOnComplete;
    private int reverseCount;

    /// <summary>
    /// 添加简易定时器脚本
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public static JTInvoke Get(GameObject go)
    {
        if (go == null)
        {
            Log.Error("go is null!");
        }
        JTInvoke item = go.GetComponent<JTInvoke>();
        if (item == null)
        {
            item = go.AddComponent<JTInvoke>();
        }
        return item;
    }

    /// <summary>
    /// 添加一个一次性延迟执行定时器
    /// </summary>
    /// <param name="delayTime"></param>
    /// <param name="callback"></param>
    public void StartTime(float delayTime, Action callback)
    {
        InvokeStart(delayTime, callback);
    }

    /// <summary>
    /// 倒计时,一秒一次
    /// </summary>
    /// <param name="delayTime"></param>
    /// <param name="callback"></param>
    public void StartReverseTime(int count, Action<int> callback, Action onComplete = null, float delayTime = 0, float repeatRate = 1)
    {
        InvokeReverseStart(delayTime, count, repeatRate, callback, onComplete);
    }

    /// <summary>
    /// 添加一个重复定时器
    /// </summary>
    /// <param name="delayTime"></param>
    /// <param name="repeatTime"></param>
    /// <param name="callback"></param>
    public void StartRepeatTime(float delayTime, float repeatTime, Action callback)
    {
        InvokeRepeatStart(delayTime, repeatTime, callback);
    }

    /// <summary>
    /// 添加一个重复定时器
    /// </summary>
    /// <param name="delayTime"></param>
    /// <param name="repeatTime"></param>
    /// <param name="callback"></param>
    public void InvokeStart(float delayTime, Action callback)
    {
        this.callback = callback;
        if (IsInvoking("InvokeCallBack")) {
            CancelInvoke("InvokeCallBack");
        }
        Invoke("InvokeCallBack", delayTime);
    }

    public void InvokeReverseStart(float delayTime, int count, float repeatRate, Action<int> callback, Action onComplete)
    {
        this.reverseCallBack = callback;
        this.reverseOnComplete = onComplete;
        this.reverseCount = count;
        if (IsInvoking("InvokeReverseCallBack"))
        {
            CancelInvoke("InvokeReverseCallBack");
        }
        InvokeRepeating("InvokeReverseCallBack", delayTime, repeatRate);
    }

    public void InvokeRepeatStart(float delayTime, float repeatTime, Action callback)
    {
        this.callback = callback;
        InvokeRepeating("InvokeCallBack", delayTime, repeatTime);
    }

    private void InvokeCallBack(){
        if (callback == null) return;
        callback();
    }

    private void InvokeReverseCallBack()
    {
        if(reverseCallBack == null)return;
        if (reverseCount < 0)
        {
            if (reverseOnComplete != null)
            {
                reverseOnComplete.Invoke();
            }
            reverseCount = 0;
            reverseCallBack = null;
            reverseOnComplete = null;
            CancelInvoke("InvokeReverseCallBack");
        }
        else
        {
            reverseCallBack.Invoke(reverseCount--);
        }
    }

    /// <summary>
    /// 指定函数名字取消
    /// </summary>
    /// <param name="callAction"></param>
    public void CancelTime(Action callAction)
    {
        CancelInvoke(callAction.ToString());
        callback = null;
    }

    /// <summary>
    /// 取消所有定时器
    /// </summary>
    public void CancelTime()
    {
        CancelInvoke();
    }
}
