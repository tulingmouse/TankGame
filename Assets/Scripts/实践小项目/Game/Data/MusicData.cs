using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音效数据类 用于存储音乐设置相关的信息 
/// </summary>
public class MusicData
{
    //背景音乐是否开启
    public bool isOpenBK;
    //音效是否开启 
    public bool isOpenSound;

    //背景音乐大小
    public float bkValue;

    //音效音量
    public float soundValue;

    //加一个是否第一次加载数据的标识
    public bool notFirst;
}
