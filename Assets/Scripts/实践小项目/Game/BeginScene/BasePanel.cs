using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class BasePanel<T> : MonoBehaviour where T:class
{

    //两个关键的静态成员

    //私有的静态成员变量 (私有)
    private static T instance;
    //公共的静态成员属性或者方法 (获取)
    public static T Instance => instance;

    private void Awake()
    {
        //在Awake中初始化的原因是 
        //我们的面板脚本 在场景上 肯定只会挂载一次 
        //那么我们可以在这个脚本的生命周期函数的Awake中
        //直接记录场景上 唯一的这个脚本 
        instance = this as T;
    }


    public virtual void ShowMe()
    {
        Cursor.visible = true;
        this.gameObject .SetActive(true);
    }

    public virtual void HideMe()
    {
        this.gameObject.SetActive(false);
    }

}
