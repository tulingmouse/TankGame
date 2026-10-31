using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lesson1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      

    }

    // Update is called once per frame
    void Update()
    {
        #region 场景切换
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //切换到场景2
            //直接 写代码 切换场景 可能会报错 
            //原因是 没有把该场景加载到场景列表中
            SceneManager.LoadScene("Test2");

            //老版本切换
            //Application.LoadLevel("Test2");
        }
        #endregion


        #region 退出游戏
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //执行这句代码 就会退出游戏 
            //但是 在编辑模式下没有作用
            //一定是发布游戏过后 才有用
            Application.Quit();
        }
        #endregion
    }
}
