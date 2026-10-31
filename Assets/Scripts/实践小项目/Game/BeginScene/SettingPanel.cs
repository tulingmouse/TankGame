using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingPanel :BasePanel<SettingPanel>
{
    //申明成员变量 关联控件
    public CustomGUISlider sliderMusic;
    public CustomGUISlider sliderSound;

    public CustomGUIToggle toggleMusic;
    public CustomGUIToggle toggleSound;

    public CustomGUIButton buttonClose;

    // Start is called before the first frame update
    void Start()
    {

        //监听对应的事件 处理逻辑 
        sliderMusic.changeValue += (value) =>
        {
            //处理音乐变化
            GameDataMgr.Instance.ChangeBkValue(value);
        };
        sliderSound.changeValue += (value) =>
        {
            //处理音效变化
            GameDataMgr.Instance.ChangeSoundValue(value);

        };
        toggleMusic.changeValue += (value) =>
        {
            //处理音乐开关
            GameDataMgr.Instance.OpenOrCloseBKMusic(value);

        };
        toggleSound.changeValue += (value) =>
        {
            //处理音效开关
            GameDataMgr.Instance.OpenOrCloseSound(value);

        };

        buttonClose.clickEvent += () =>
        {
            //处理隐藏自己
            HideMe();
            //判断当前所在的场景 应该如何判断
           
            if (SceneManager.GetActiveScene().name=="BeginScene")
            {
                BeginPanel.Instance.ShowMe();
            }
        };
        HideMe();

    }

    public void UpdataPanelInfo()
    {
        //我们面板上的信息都是根据 音效数据 更新的 
        MusicData data = GameDataMgr.Instance.musicData;

        //设置面板内容
        sliderMusic.nowValue = data.bkValue;
        sliderSound.nowValue = data.soundValue;

        toggleMusic.isSel = data.isOpenBK;
        toggleSound.isSel = data.isOpenSound;

    }
    public override void HideMe()
    {

        base.HideMe();
        Time.timeScale = 1;
    }

    public override void ShowMe()
    {
        base.ShowMe();
        //每次显示面板时 顺便把 面板上的内容也更新了
        UpdataPanelInfo();
    }
}
