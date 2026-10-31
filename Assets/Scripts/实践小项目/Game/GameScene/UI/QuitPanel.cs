using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitPanel :BasePanel<QuitPanel>
{
    public CustomGUIButton btnQuit;
    public CustomGUIButton btnGoOn;
    public CustomGUIButton btnClose;


    // Start is called before the first frame update
    void Start()
    {

        btnQuit.clickEvent += () =>
        {
            //回到主界面
            SceneManager.LoadScene("BeginScene");
        };

        //继续游戏和 XX都是关闭当前面板
        btnGoOn.clickEvent += () =>
        {
            HideMe();
        };

        btnClose.clickEvent += () =>
        {
            HideMe();

        };

        HideMe();
            
    }

    public override void HideMe()
    {
        
        base.HideMe();
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
