using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BKMusic : MonoBehaviour
{
    private static BKMusic instance;

    public static BKMusic Instance => instance;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        //得到自己依附 的游戏对象的 音源脚本
        audioSource = this.GetComponent<AudioSource>();
        //初始化时 把大小和开关 根据数据 进行设置
        ChangeValue(GameDataMgr.Instance.musicData.bkValue);
        ChangeOpen(GameDataMgr.Instance.musicData.isOpenBK);
    }

    /// <summary>
    /// 改变背景音乐大小
    /// </summary>
    /// <param name="value"></param>
    public void ChangeValue(float value)
    {
        audioSource.volume = value;
    }

    /// <summary>
    /// 开关背景音乐
    /// </summary>
    /// <param name="isOpen"></param>
    public void ChangeOpen(bool isOpen)
    {
        //如果开启就是 不静音
        //没有开启就是静音
        audioSource.mute = !isOpen;
    }
}
