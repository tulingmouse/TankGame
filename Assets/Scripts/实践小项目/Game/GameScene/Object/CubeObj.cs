using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeObj : MonoBehaviour
{
    //奖励 预设体关联
    public GameObject[] rewardObjects;

    //死亡特效 预设体关联 
    public GameObject deadEff;

    private void OnTriggerEnter(Collider other)
    {
        //打到自己的子弹 应该销毁 
        //把箱子的tag 改成cube 之前子弹逻辑当中就已经处理过 打中cube销毁自己的逻辑

        //打到自己 应该处理 随机创建奖励的逻辑 

        //随机一个数 来获取奖励
        int rangeInt = Random.Range(0, 100);
        //50%的概率 创建一个奖励
        if(rangeInt<50)
        {
            //随机创建一个 奖励预设体 在当前位置 
            rangeInt = Random.Range(0, rewardObjects.Length);
            //在箱子当前位置 创建即可
            Instantiate(rewardObjects[rangeInt],this.transform.position,this.transform.rotation);

        }

        GameObject effObj =Instantiate(deadEff,this.transform.position,this.transform.rotation);
        //控制 获取音效
        AudioSource audioSource = effObj.GetComponent<AudioSource>();
        audioSource.volume = GameDataMgr.Instance.musicData.soundValue;
        audioSource.mute = !GameDataMgr.Instance.musicData.isOpenSound;

        Destroy(this.gameObject);
    }
}
