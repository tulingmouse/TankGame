using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_PropType
{
    //加属性的类型
    Atk,
    Def,
    MaxHp,
    HP,
    
}

public class PropReward : MonoBehaviour
{
    public E_PropType type = E_PropType.Atk;

    //默认添加值 获取道具后
    public int changeValue = 2;

    //得到特效
    public GameObject getEff;

    private void OnTriggerEnter(Collider other)
    {
        //玩家才能获取属性奖励
        if(other.CompareTag("Player"))
        {
            //得到对应的玩家脚本
            PlayerObj player = other.GetComponent<PlayerObj>();
            //根据类型来加属性
            switch (type)
            {
                case E_PropType.Atk:
                    player.atk += changeValue;
                    break;
                case E_PropType.Def:
                    player.def += changeValue;
                    break;
                case E_PropType.MaxHp:
                    player.maxHp += changeValue;
                    //更新血条
                    GamePanel.Instance.UpdataHP(player.maxHp, player.hp);

                    break;
                case E_PropType.HP:
                    player.hp += changeValue;
                    //不能超过上限
                    if (player.hp > player.maxHp)
                    {
                        player.hp = player.maxHp;
                    }
                    //更新血条
                    GamePanel.Instance.UpdataHP(player.maxHp, player.hp);
                    break;
             
            }
            //播放一个奖励特效
            GameObject eff = Instantiate(getEff, this.transform.position, this.transform.rotation);
            //控制 获取音效
            AudioSource audioSource = eff.GetComponent<AudioSource>();
            audioSource.volume = GameDataMgr.Instance.musicData.soundValue;
            audioSource.mute = !GameDataMgr.Instance.musicData.isOpenSound;

            Destroy(this.gameObject);
        
        }
    }

}
