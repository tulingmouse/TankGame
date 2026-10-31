using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel :BasePanel<LosePanel>
{
    public CustomGUIButton btnBack;

    public CustomGUIButton btnGoOn;
    // Start is called before the first frame update
    void Start()
    {
        btnBack.clickEvent += ()=>
        {
            //取消暂停
            Time.timeScale = 1;
            //切换场景
            SceneManager.LoadScene("BeginScene");
        };

        btnGoOn.clickEvent += () =>
        {
            //取消暂停
            Time.timeScale = 1;
            //刷新场景 就能让所有内容重置
            SceneManager.LoadScene("GameScene");


        };

        HideMe();
    }

   
}
