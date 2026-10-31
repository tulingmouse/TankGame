using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CustomGUIRoot : MonoBehaviour
{
    //用于存储 子对象 所有GUI控件的容器
    private CustomGUIControl[] allControls;
    void Start()
    {

        allControls = this.GetComponentsInChildren<CustomGUIControl>();
    }
    //在这统一绘制子对象控件的内容 
    private void OnGUI()
    {
        //通过每一次绘制之前 得到所有子对象控件的  父类脚本
        //这句代码浪费 性能 因为 每次 GUI都会来获取所有的 控件对应的脚本 
        //编辑状态下才一直执行
        //if (!Application.isPlaying)
        //{
            allControls = this.GetComponentsInChildren<CustomGUIControl>();
        //}
        //遍历控件 让其绘制
        for (int i = 0; i < allControls.Length; i++)
        {
            allControls[i].DrawGUI();
        }
    }

}
