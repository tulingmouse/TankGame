using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginPanel : BasePanel<BeginPanel>
{
    //首先申明公共的成员变量 来关联各个控件
    public CustomGUIButton btnBegin;
    public CustomGUIButton btnSetting;
    public CustomGUIButton btnQuit;
    public CustomGUIButton btnRank;
    // Start is called before the first frame update
    void Start()
    {
        //目的是方便 控制坦克的头部转向 所以锁定鼠标在窗口内 避免鼠标出去不好控制
        Cursor.lockState = CursorLockMode.Confined;

        //监听一次按钮点击过后要做什么
        btnBegin.clickEvent += () =>
        {
            //切换场景
            Cursor.visible = false;
            SceneManager.LoadScene("GameScene");

        };

        btnSetting.clickEvent += () =>
        {
            //设置界面
            SettingPanel.Instance?.ShowMe();
            HideMe();
        };

        btnQuit.clickEvent += () =>
        {
            //退出游戏
            Application.Quit();
        };

        btnRank.clickEvent += () =>
        {
            //打开排行榜面板 
            RankPanel.Instance.ShowMe();
            HideMe();
        };

    }

    // Update is called once per frame
    void Update()
    {

    }
}
