# TimerManagerForUnity3d
tips: TimerManagerForUnity3d is Singleton Pattern，if you use it end, please call Clear() ;
you can use it like:
        1.JTInvokeTask base on mono Invoke func
        JTInvokeTask.InitOnce(3, true, () =>
        {
            Debug.Log("InitOnce=" + (count));
        });
        
        JTInvokeTask.InitCount(1, 2, 10, true, () =>
        {
            Debug.Log("InitCount=" + (count++));

        });
        JTInvokeTask.InitRepeat....
        
        2.JTTimeTask base on mono  Update func; It is a repeat by custom time Integer;
        JTTimeTask timeTask = JTTimeTask.Init(OnTime, 2);
        timeTask.begin();//begin
        timeTask.stop();//no delate just stop
        timeTask.Clear();//delete game object
        
        
