﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <remarks>单位数据模板</remarks>
public class UnitModel
{
    /// <summary>
    /// 默认显示类型
    /// </summary>
    public int DefaultViewType;
    /// <summary>
    /// 默认名
    /// </summary>
    public string DefaultName;
    /// <summary>
    /// 默认立绘
    /// </summary>
    public Sprite DefaultFace;
    /// <summary>
    /// 初始攻击力
    /// </summary>
    /// <remarks>决定角色伤害</remarks>
    public int Attack;
    /// <summary>
    /// 初始防御力
    /// </summary>
    /// <remarks>决定角色伤害减免</remarks>
    public int Defence;
    /// <summary>
    /// 初始治愈力
    /// </summary>
    /// <remarks>决定治愈技能治愈量</remarks>
    public int Heal;
    /// <summary>
    /// 初始先手
    /// </summary>
    /// <remarks>决定出手顺序</remarks>
    public int Speed;
    /// <summary>
    /// 初始血量上限
    /// </summary>
    /// <remarks>角色最大血量</remarks>
    public int Blood;
    /// <summary>
    /// 初始行动点等级
    /// </summary>
    public int ActionPoint;
    /// <summary>
    /// 攻击成长曲线
    /// </summary>
    public AnimationCurve AttackCurve;
    /// <summary>
    /// 防御成长曲线
    /// </summary>
    public AnimationCurve DefenceCurve;
    /// <summary>
    /// 治愈力成长曲线
    /// </summary>
    public AnimationCurve HealCurve;
    /// <summary>
    /// 先手成长曲线
    /// </summary>
    public AnimationCurve SpeedCurve;
    /// <summary>
    /// 血量成长曲线
    /// </summary>
    public AnimationCurve BloodCurve;
    /// <summary>
    /// 行动点等级成长曲线
    /// </summary>
    public AnimationCurve ActionPointCurve;
    /// <summary>
    /// 初始卡组
    /// </summary>
    public List<Card> DefaultDeck;
    public string PrivilegeDeckIndex = "Normal";
    public string CoreDeckIndex = "Normal";
}