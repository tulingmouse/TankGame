using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObj :TankBaseObj
{
    //获取刚体
    private Rigidbody rb;

    private Vector3 moverDir;
    private Vector3 vector;

    private Quaternion rotDelta;

    private void Start()
    {
        //GameDataMgr.Instance.rankData.list.Clear();
        rb = GetComponent<Rigidbody>();
    }
    //记录输入成员变量
    private float vInput;
    private float hInput;
    private float mouseX;

    //当前装备的武器
    public WeaponObj nowWeapon;

    //武器父对象位置
    public Transform weaponPos;

    // Update is called once per frame
    void Update()
    {
        //ws键 控制 前进 
        //知识点  Transfrom位移 Input 轴向检测输入
        //this.transform.Translate(Vector3.forward*moveSpeed*Time.deltaTime*Input.GetAxis("Vertical"));
        vInput = Input.GetAxis("Vertical");

        //ad键 控制 旋转
        //知识点 Transfrom旋转 Input 轴向检测输入
        //this.transform.Rotate(Vector3.up * roundSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));
        hInput = Input.GetAxis("Horizontal");
        //鼠标左右移动控制炮台旋转
        //知识点 Transform旋转  Input 鼠标轴向输入检测
        this.tankHead.transform.Rotate(Vector3.up * Time.deltaTime * headRoundSpeed * Input.GetAxis("Mouse X"));
        mouseX = Input.GetAxis("Mouse X");
        //开火
        //Input
        if(Input.GetMouseButtonDown(0))
        {
            
                        
            this.Fire();
        }
      
    }

    private void FixedUpdate()
    {
        //坦克前进后退
        moverDir = transform.forward * vInput * moveSpeed;
        vector.x = moverDir.x;
        vector.y = rb.velocity.y;
        vector.z = moverDir.z;
        rb.velocity = vector;
        //坦克车身旋转
        rotDelta = Quaternion.Euler(0,hInput*roundSpeed*Time.fixedDeltaTime,0);
        rb.MoveRotation(rb.rotation * rotDelta);
       

    }

    //重写 父类中的 行为 玩家可能会存在特殊处理
    public override void Fire()
    {
        if (nowWeapon != null)
        {
            nowWeapon.Fire();
        }
    }

    public override void Dead()
    {
        Time.timeScale = 0;
        LosePanel.Instance.ShowMe();
        LockMouse.UnLockMouse();
        
        //不用父类死亡 防止摄像机被移除
        //base.Dead();
       
    }

    public override void Wound(TankBaseObj other)
    {
        base.Wound(other);
        //更新主面板血条
        GamePanel.Instance.UpdataHP(this.maxHp, this.hp);
    }

    /// <summary>
    /// 切换武器
    /// </summary>
    /// <param name="weapon"></param>
    public void ChangeWeapon(GameObject weapon)
    {
        if (nowWeapon != null)
        {
            //删除原有武器
            Destroy(nowWeapon.gameObject);
            nowWeapon = null;
        }

        //切换武器
        //创建出武器 设置它的父对象 并且保证缩放没什么问题
        GameObject weaponObj = Instantiate(weapon, weaponPos,false);
        nowWeapon = weaponObj.GetComponent<WeaponObj>();
        nowWeapon.SetFather(this);
    }

}
