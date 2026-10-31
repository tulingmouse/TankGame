using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// PlayerPrefs数据管理类 统一管理数据的存储和读取
/// </summary>
public class PlayerPrefsDataMgr
{
    private static PlayerPrefsDataMgr instance = new PlayerPrefsDataMgr();

    public static PlayerPrefsDataMgr Instance
    {
        get
        {
            return instance;
        }
    }

    private PlayerPrefsDataMgr() { }

    /// <summary>
    /// 存储数据
    /// </summary>
    /// <param name="data">数据对象</param>
    /// <param name="keyName">数据对象的唯一key</param>
    public void SaveData(object data, string keyName)
    {
        //如果Type 得到传入数据对象的所有的 字段
        //然后结合 PlayerPrefs来进行存储
        #region 获取传入数据的所有字段
        Type dataType = data.GetType();
        //得到所有的字段
        FieldInfo[] infos = dataType.GetFields();

        #endregion

        #region 自己定义一个key的规则 进行数据存储
        //我们存储都是通过 PlayerPrefs来进行存储的
        //保证key的唯一性 我们就需要自己定一个key的规则

        //我们自己定一个规则 
        // keyName_数据类型_字段类型_字段名
        string saveKeyName = "";
        FieldInfo info;
        for (int i = 0; i < infos.Length; i++)
        {
            //对每一个字段  进行数据存储
            //得到具体的字段信息 
            info = infos[i];
            //通过FieldInfo可以直接获取到 字段的类型 和字段的名字
            //字段的类型 info.FeildType.Name;
            //字段的名字 info.Name;
            //根据定的key的规则 来进行key的生成
            //Player1_PlayerInfo_Int32_age
            saveKeyName = keyName + "_" + dataType.Name + "_" + info.FieldType.Name + "_" + info.Name;

            //得到符合规则的key 
            //接下来通过PlayerPrefs来进行存储
            //如何获取值
            //info.GetValue(data);
            //封装一个专门存储值的方法
            SaveValue(info.GetValue(data), saveKeyName);

        }
        #endregion

        #region 遍历这些字段 进行数据存储

        #endregion
    }

    private void SaveValue(object value, string keyName)
    {
        //直接通过PlayerPrefs来进行存储
        //根据数据的不同 来决定使用哪一个API来进行存储
        //PlayerPrefs只支持3种类型存储
        //判断数据类型是什么类型 如何调用具体的方法来存储
        Type fieldType = value.GetType();

        //类型判断
        //是不是int
        if (fieldType == typeof(int))
        {
            PlayerPrefs.SetInt(keyName, (int)value);
        }
        else if (fieldType == typeof(float))
        {
            PlayerPrefs.SetFloat(keyName, (float)value);
        }
        else if (fieldType == typeof(string))
        {
            PlayerPrefs.SetString(keyName, value.ToString());
        }
        else if (fieldType == typeof(bool))
        {
            //自己定一个存储规则
            PlayerPrefs.SetInt(keyName, (bool)value ? 1 : 0);
        }
        //如何判断 泛型类的类型呢
        //通过反射 判断 父子关系
        //这相当于判断 字段是不是Ilist的子类
        else if (typeof(IList).IsAssignableFrom(fieldType))
        {
            //父类装子类
            IList list = value as IList;
            //先存储数量 
            PlayerPrefs.SetInt(keyName, list.Count);
            int index = 0;
            foreach (object obj in list)
            {
                //存储具体的值
                SaveValue(obj, keyName + index);
                ++index;
            }
        }
        //判断是不是 Dictionary类型 通过其父类
        else if (typeof(IDictionary).IsAssignableFrom(fieldType))
        {
            //父类装 子类
            IDictionary dic = value as IDictionary;
            //先存字典长度
            PlayerPrefs.SetInt(keyName, dic.Count);
            //用于区分key的值
            int index = 0;
            foreach (object key in dic.Keys)
            {
                //存储值
                SaveValue(key, keyName +"_key_"+ index);
                SaveValue(dic[key], keyName + "_value_" + index);
                index++;
            }
        }

        //基础类型都不是 那么可能就是自定义类型
        else
        {
            SaveData(value, keyName);
        }

        PlayerPrefs.Save();
    }
    /// <summary>
    /// 读取数据 
    /// </summary>
    /// <param name="type">想要读取数据的 数据类型</param>
    /// <param name="keyName">数据对象的唯一Key 自己控制</param>
    /// <returns></returns>
    public object LoadData(Type type, string keyName)
    {
        //不用object对象 传入 而使用 Type传入
        //主要是节约外部的一行代码
        //假设要读取一个 Player类型的数据 如果是object 就必须在外部new一个对象再传入
        //写Type 就只用传类型进来 Type typeof(Player)  在内部再动态创建一个对象返回出来
        //达到了 让你在外部 少写一行代码的作用

        //根据传入的类型 和keyName
        //依据你存储数据时  key的拼接规则 来进行数据的获取赋值 返回出去

        //根据传入的type 创建一个对象 用于存储数据
        object data = Activator.CreateInstance(type);
        //要往这个new出来的对象中存储数据 填充数据
        //得到所有字段
        FieldInfo[] infos = type.GetFields();

        string loadKeyName = "";
        FieldInfo info;
        for (int i = 0;i<infos.Length;i++)
        {
            info = infos[i];
            //规则一定是和 存储时一模一样的 这样才能找到对应数据
            loadKeyName = keyName + "_" + type.Name + "_" + info.FieldType.Name + "_" + info.Name;


            //有key 就可以结合PlayerPrefs来读取数据 
            //填充到data中
            info.SetValue(data, LoadValue(info.FieldType,loadKeyName));

        }

        return data;
    }

    /// <summary>
    /// 得到单个数据的方法
    /// </summary>
    /// <param name="fieldType">字段类型用于判断用哪个API读取</param>
    /// <param name="KeyName">用于获取具体数据</param>
    /// <returns></returns>
    private object LoadValue(Type fieldType,string keyName)
    {
        //根据 字段类型 来判断 用哪个API来读取
        if (fieldType == typeof(int))
        {
           return PlayerPrefs.GetInt(keyName);
        }
        else if (fieldType == typeof(float))
        {
          return  PlayerPrefs.GetFloat(keyName);
        }
        else if (fieldType == typeof(string))
        {
           return PlayerPrefs.GetString(keyName);
        }
        else if (fieldType == typeof(bool))
        {
            //自己定一个存储规则
             return PlayerPrefs.GetInt(keyName)==1?true:false;
        }
        else if(typeof(IList).IsAssignableFrom(fieldType))
        {
            //得到长度
            int count = PlayerPrefs.GetInt(keyName, 0);
            //实例化 List对象 来进行赋值
            //用来反射中双A Activator进行快速实例化List对象
            IList list = Activator.CreateInstance(fieldType)as IList;
            for (int i = 0; i < count; i++)
            {
                //目的是要得到泛型中的类型 
                list.Add(LoadValue(fieldType.GetGenericArguments()[0],keyName+i));
            }
            return list;
        }
        else if(typeof(IDictionary).IsAssignableFrom(fieldType))
        {
            //得到字典的长度
            int count = PlayerPrefs.GetInt(keyName, 0);
            //实例化一个字典对象 用父类装子类
            IDictionary dic = Activator.CreateInstance(fieldType) as IDictionary;
            //得到泛型类型
            Type[] kvType =fieldType.GetGenericArguments();
            for (int i = 0; i < count; i++)
            {
                dic.Add(LoadValue(kvType[0], keyName + "_key_" + i),
                       LoadValue(kvType[1], keyName + "_value_" + i));
            }
            return dic;

        }
        else
        {
            return LoadData(fieldType, keyName);
        }
    }


}
