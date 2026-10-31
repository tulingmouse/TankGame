using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGUIToggleGroup : MonoBehaviour
{
    public CustomGUIToggle[] toggles;
    //记录上一次为true的taggle
    private CustomGUIToggle frontTurTog;
    private void Start()
    {
        if (toggles.Length == 0)
        {
            return;
        }
        //通过遍历 为多个 多选框 添加事件监听
        //在函数中 做处理
        //当一个为true时 另外的变为false
        for (int i = 0; i < toggles.Length; i++)
        {
            CustomGUIToggle toggle = toggles[i];
            toggle.changeValue += (value) =>
            {
                //当传入的 value 是true时 需要把另外两个 变成false
                if (value)
                {
                    //意味着另外的要变成false
                    for (int j = 0; j < toggles.Length; j++)
                    {
                        //闭包 toggle是上个函数中申明的变量 
                        //改变了它的生命周期1
                        if (toggles[j] != toggle)
                        {
                            toggles[j].isSel = false;
                        }
                    }
                    frontTurTog = toggle;

                }
                //来判断当前变成 false的这个toggle是不是上一次为true
                //如果是 就不应该让它变为false
                else if(toggle == frontTurTog) 
                {
                    toggle.isSel = true;
                }
            };

        }
    }
}
