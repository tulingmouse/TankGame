using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankPanel :BasePanel<RankPanel>
{
    //关联public 的控件对象 
    public CustomGUIButton btnClose;


    private List<CustomGUILabel> labPM=new List<CustomGUILabel>();
    private List<CustomGUILabel> labName = new List<CustomGUILabel>();
    private List<CustomGUILabel> labScore = new List<CustomGUILabel>();
    private List<CustomGUILabel> labTime = new List<CustomGUILabel>();



    private void Start()
    {   //找到子对象
        for(int i = 1; i <= 10; i++)
        {
            labName.Add(this.transform.Find("Name/LabelName" + i).GetComponent<CustomGUILabel>());
            labScore.Add(this.transform.Find("Score/LabelScore" + i).GetComponent<CustomGUILabel>());
            labTime.Add(this.transform.Find("Time/LabelTime" + i).GetComponent<CustomGUILabel>());
        }
        //处理事件监听逻辑
        btnClose.clickEvent += () =>
        {
            HideMe();
            BeginPanel.Instance.ShowMe();

        };

       
        HideMe();
    }

    public override void ShowMe()
    {
        base.ShowMe();
        UpdatePanelInfo();
    }

    public void UpdatePanelInfo()
    {
        //根据排行榜数据 更新面板
        //获取 GameDataMgr中的排行榜列表 用于在这里更新
        //得数据
        List<RankInfo> list = GameDataMgr.Instance.rankData.list;

        //根据列表 更新面板数据
        for (int i = 0; i < list.Count; i++)
        {
            //名字
            labName[i].content.text = list[i].name;
            //分数 
            labScore[i].content.text = list[i].score.ToString();
            //时间 存储的时间是s
            //把秒数 转换为 时 分 秒
            int time = (int)list[i].time;
            labTime[i].content.text = "";
            //得到几个小时 
            if (time/3600>0)
            {
                labTime[i].content.text += time / 3600 + "时";
            }
            if (time % 3600 / 60 > 0 || labTime[i].content.text != "")
            {
                labTime[i].content.text += time % 3600 / 60 + "分";
            }
            labTime[i].content.text += time % 60 + "秒";
        }
    }

}
