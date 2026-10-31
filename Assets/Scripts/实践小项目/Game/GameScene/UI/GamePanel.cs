using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel :BasePanel<GamePanel>
{
    //获取控件 关联场景上的 控件对象 之后好控制

    //分数
    public CustomGUILabel labScore;
    //时间
    public CustomGUILabel labTime;
    //退出按钮
    public CustomGUIButton btnQuit;
    //设置按钮
    public CustomGUIButton btnSetting;
    //血量图
    public CustomGUITexture texHP;

    //血条控件的宽
    public float hpw = 350;

    //用于记录该玩家的当前分数
    [HideInInspector]
    public int nowScore;
    [HideInInspector]
    public float nowTime = 0;
    private int time;

    // Start is called before the first frame update
    void Start()
    {
        //监听界面上的一些控件操作事件   
        btnSetting.clickEvent += () =>
        {
             
             //打开设置面板
             SettingPanel.Instance.ShowMe();

            //改变时间 缩放值 为0就是时间 停止
            Time.timeScale = 0;


        };

        btnQuit.clickEvent += () => 
        {
            //返回游戏界面 
            //弹出 确定退出的按钮'
            QuitPanel.Instance.ShowMe();
            //改变时间 缩放值 为0就是时间 停止
            Time.timeScale = 0;
        };

       

    }

    // Update is called once per frame
    void Update()
    {
        //通过帧间隔时间进行累加
        nowTime += Time.deltaTime;

        //把秒转换为时分秒
        time = (int)nowTime;
        labTime.content.text = "";
        //得到几个小时 
        if (time / 3600 > 0)
        {
            labTime.content.text += time / 3600 + "时";
        }
        if (time % 3600 / 60 > 0 || labTime.content.text != "")
        {
            labTime.content.text += time % 3600 / 60 + "分";
        }
        labTime.content.text += time % 60 + "秒";
    }
    
    /// <summary>
    /// 提供给外部的加分方法
    /// </summary>
    /// <param name="score"></param>
    public void AddScore(int  score)
    {
        nowScore += score;

        labScore.content.text = nowScore.ToString();
    }    
    /// <summary>
    /// 更新血条的方法
    /// </summary>
    /// <param name="maxHP"></param>
    /// <param name="HP"></param>
    public void UpdataHP(int maxHP,int HP)
    {
        texHP.guiPos.width = (float)HP/maxHP *hpw;
    }

}
