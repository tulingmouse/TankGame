using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObj : MonoBehaviour
{
    //移动速度
    public float moveSpeed = 50;

    //谁发射的我
    public TankBaseObj fatherObj;

    //特效对象
    public GameObject effobj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward*moveSpeed*Time.deltaTime);
    }

    //和别人碰撞触发时
    private void OnTriggerEnter(Collider other)
    {
        //子弹射击到立方体 或者不同阵营的对象也爆炸
        if (other.CompareTag("Cube")
            ||other.CompareTag("Player")&&fatherObj.CompareTag("Monster")
            || other.CompareTag("Monster") && fatherObj.CompareTag("Player"))
        {

            //判断是否受伤
            //得到碰撞到的对象身上 是否有 坦克相关脚本 我们用李氏替换 原则
            //通过父类去获取
            TankBaseObj obj = other.GetComponent<TankBaseObj>();
            if (obj != null)
            {
                obj.Wound(fatherObj);
            }


            //当子弹销毁时 可以创建一个 爆炸特效
            if(effobj!=null)
            {
                GameObject eff = Instantiate(effobj,this.transform.position,this.transform.rotation);
                //改音效的音量和开启状态
                AudioSource audioSource = eff.GetComponent<AudioSource>();
                audioSource.volume = GameDataMgr.Instance.musicData.soundValue;
                audioSource.mute = !GameDataMgr.Instance.musicData.isOpenSound;

            }

            Destroy(gameObject);
        }
    }

    //设置拥有者
    public void SetFather(TankBaseObj obj)
    {
        fatherObj = obj;
    }

}
