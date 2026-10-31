using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson2 : MonoBehaviour
{
    public Texture2D tex;
    // Start is called before the first frame update
    void Start()
    {
        #region 隐藏鼠标 
         Cursor.visible = true;
        #endregion

        #region 锁定鼠标 
        //None 就是不锁定 
        //Locked 锁定 鼠标会被限制在 屏幕中心点 还会隐藏鼠标  按ESC摆脱编辑模式锁定
        //Confined 限制在窗口范围内
        Cursor.lockState = CursorLockMode.Confined;
        #endregion

        #region 设置鼠标图片
        //参数一：光标图片
        //参数二：偏移位置 相对图片左上角
        //参数三：平台支持的光标模式 (硬件或软件)
        Cursor.SetCursor(tex,Vector2.zero,CursorMode.Auto);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
