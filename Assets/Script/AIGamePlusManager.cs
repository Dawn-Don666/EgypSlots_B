using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class AIGamePlusManager : MonoYoungster<AIGamePlusManager>
{
    //��ȡIOS��������
#if UNITY_IOS
    [DllImport("__Internal")]
    internal extern static void onGameEvent(string eventToken);

    [DllImport("__Internal")]
    internal extern static void onGameLevelChanged(int level);
#endif

    public void SendEvent(string eventToken)
    {
#if UNITY_IOS && !UNITY_EDITOR
        onGameEvent(eventToken);
        print("AIGamePlus ���Ե���ԭ��������� �¼���" + eventToken);
#endif
    }

    public void SendLevelChanged(int level)
    {
#if UNITY_IOS && !UNITY_EDITOR
        onGameLevelChanged(level);
        print($"AIGamePlus ���Ե���ԭ���������ȼ��� {level}");
#endif
    }
}
