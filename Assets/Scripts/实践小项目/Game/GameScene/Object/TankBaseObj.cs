using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TankBaseObj : MonoBehaviour
{
    //攻击力 防御力 最大当前血量相关
    public int atk;
    public int def;
    public int maxHp;
    public int hp;

    //炮台相关
    public Transform tankHead;


    //移动旋转速度相关
    public float moveSpeed = 10;
    public float roundSpeed = 100;
    public float headRoundSpeed = 100;

    //死亡特效 关联对应预设体 死亡的时候 动态创建出来 设置位置即可
    public GameObject deadEff;

    /// <summary>
    /// 开火抽象方法 
    /// </summary>
    public abstract void Fire();

    /// <summary>
    /// 被别人攻击 造成我自己受伤
    /// </summary>
    /// <param name="other"></param>
    public virtual void Wound(TankBaseObj other)
    {
        int dmg = other.atk - this.def;
        if (dmg <= 0) return;

        //如果伤害大于0 就应该减少血
        this.hp -= dmg;
        //判断 如果血量 <=0 了 就应该死亡
        if (this.hp <= 0)
        {
            this.hp = 0;
            Dead();
        }
    }
    /// <summary>
    /// 死亡行为
    /// </summary>
    public virtual void Dead()
    {
        //对象死亡 在场景上移除该对象 
        Destroy(this.gameObject);
        //死亡的时候 所有坦克对象 都应该播放应该 死亡对象的特效
        if (deadEff != null)
        {
            //实例化对象时 顺便 把位置和角度 都一起设置了
            GameObject effObj = Instantiate(deadEff, this.transform.position, this.transform.rotation);
            //由于该特效对象上 直接关联了 音效 所以我们可以在此处 把音效播放相关也控制了
            AudioSource audioSource = effObj.GetComponent<AudioSource>();
            //根据音乐数据 设置音效 大小 和是否播放
            audioSource.volume = GameDataMgr.Instance.musicData.soundValue;
            audioSource.mute = !GameDataMgr.Instance.musicData.isOpenSound;
            //避免没有勾选 Play on Awake
            audioSource.Play();
        }
    }
    
}
