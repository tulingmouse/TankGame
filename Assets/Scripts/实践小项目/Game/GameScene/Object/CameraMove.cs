using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //摄像机 看向的目标
    public Transform targetPlayer;

    private Vector3 pos;
    public float H = 10;
    private void LateUpdate()
    {
        if (targetPlayer == null)
        {
            return;
        }
        //x和z和玩家一样
        pos.x =targetPlayer.position.x;
        pos.z = targetPlayer.position.z;
        //高度通过外部调整
        pos.y = H;
        this.transform.position = pos;
    }

}
