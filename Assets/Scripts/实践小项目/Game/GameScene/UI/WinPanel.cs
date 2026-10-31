using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : BasePanel<WinPanel>
{
    //关联控件 
    public CustomGUIInput inputInfo;
    public CustomGUIButton btnSure;

    // Start is called before the first frame update
    void Start()
    {
        HideMe();
        btnSure.clickEvent += () =>
        {

            //把数据记录到排行榜 并且回到主场景
            GameDataMgr.Instance.AddRankInfo(inputInfo.content.text, GamePanel.Instance.nowScore, GamePanel.Instance.nowTime);

            //接着返回 我们的开始界面即可
            Time.timeScale = 1;
            SceneManager.LoadScene("BeginScene");
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
