﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameToolKit;
/// <summary>
/// 关卡选择状态
/// </summary>
public class SelectLevelState : GameState
{
    protected internal override void OnEnter()
    {
        ServiceFactory.Instance.GetService<PanelManager>()
            .OpenPanel("TreeMapView");
    }

    protected internal override void OnExit()
    {
        ServiceFactory.Instance.GetService<PanelManager>()
            .ClosePanel("TreeMapView");
    }

    protected internal override void OnUpdata()
    {

    }

    /// <summary>
    /// 选择前往的节点
    /// </summary>
    /// <param name="id">节点id</param>
    public void SelectNode(int id)
    {
        var gm = ServiceFactory.Instance.GetService<GameManager>();
        var map = gm.GameData.TreeMap;
        var node = map.FindNode(id);
        map.CurrentId = id;
        switch (node.PlaceType)
        {
            case PlaceType.NormalBattle:
                gm.SetStatus<BattleState>();
                break;
            case PlaceType.AdvancedBattle:
                break;
            case PlaceType.BossBattle:
                break;
            case PlaceType.Start:
                break;
            case PlaceType.BonFire:
                break;
        }
    }
}