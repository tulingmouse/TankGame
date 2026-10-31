using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

public class LockMouse : MonoBehaviour
{
    //检测点击后的时间累加
    private bool isAddTime;

    private float nowTime;

    // Start is called before the first frame update
    void Start()
    {
        lockedMouse();
        isAddTime = false;
    }

    // Update is called once per frame
    void Update()
    {
     
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnLockMouse();
        }
       
        if(Time.timeScale ==1&&Cursor.lockState==CursorLockMode.None)
        {

            if (Input.GetMouseButtonDown(0))
            {
                isAddTime=true;
            }
        }
        if(isAddTime)
        {
            nowTime += Time.deltaTime;
        }
        if (nowTime>0.3)
        {
            if (Time.timeScale==1)
            {
                lockedMouse();
            }
            nowTime = 0;
            isAddTime = false;
        }
       
    }

    public static void lockedMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public static void  UnLockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
