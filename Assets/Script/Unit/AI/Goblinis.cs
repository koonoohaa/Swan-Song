using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 哥布林力士
/// 对一个角色及其周围八格内的角色造成100%力量值的伤害，攻击时搜索最近距离的敌人,
/// 移动至敌人身前进行攻击,攻击后停留在角色周围(5x5的格子内随机)。
/// </summary>
public class Goblinis : Unit
{
    public Goblinis(Vector2Int pos) : base(new UnitData()
    {
        Name = "Goblinis",//哥林布力士
        BloodMax = 100,//最大血量
        Blood = 100,//初始血量为最大血量
        Attack = 20,//攻击力
        Defence = 4,//防御力
        Speed = 4,//先攻权重
    }
, pos)
    {
    }

    /// <summary>
    /// 行动
    /// </summary>
    protected override void Decide()
    {
        //获得玩家对象
        List<Player> players = GameManager.Instance.GetState<BattleState>().PlayerList.ToList();
        //得到要攻击的对象
        Player player = getAttackPlayer(players);
        //攻击对象
        attackPlayer(players,player);
        //撤退
        retreat(player.Position);
    }

    /// <summary>
    /// 根据玩家距离哥布林力士的距离，选择合适的攻击对象
    /// </summary>
    /// <param name="players">所有玩家</param>
    /// <returns></returns>
    public Player getAttackPlayer(List<Player> players)
    {
        int num = -1;//记录距离最短的玩家的号码
        int i = 0;
        double minDis = int.MaxValue;//设初值为最大值

        foreach (Player p in players)
        {
            double dis = Math.Pow(Math.Abs(p.Position.x - this.Position.x), 2.0) + Math.Pow(Math.Abs(p.Position.y - this.Position.y), 2.0);
            if (dis < minDis && p.ActionStatus == ActionStatus.Running)
            {
                minDis = dis;
                num = i;
            }
            i++;
        }
        return players[num];
    }

    /// <summary>
    ///  进行范围攻击
    /// </summary>
    /// <param name="player"></param>
    public void attackPlayer(List<Player> players, Player player)
    {
        //移动
        MoveclosePlayerPos(player.Position);
        for (int i = -1; i <= 1; ++i)
        {
            for (int j = -1; j <= 1; ++j)
            {
                int index = hasPlayer(players, new Vector2Int(player.Position.x + i, player.Position.y + j));
                if (index != -1)//如果该位置有角色进行攻击
                {
                    (players[index] as IHurtable).Hurt(this.UnitData.Attack, HurtType.FromUnit, this);
                }
            }
        }
    }
    /// <summary>
    /// 判断是否有玩家在这个位置上
    /// </summary>
    /// <param name="players"></param>
    /// <param name="pos"></param>
    /// <returns></returns>
    public int hasPlayer(List<Player> players, Vector2Int pos)
    {
        int i = 0;
        foreach (Player player in players)
        {
            if (player.Position == pos)
            {
                return i;
            }
            i++;
        }
        return -1;
    }
    /// <summary>
    /// 靠近玩家
    /// </summary>
    /// <param name="playerPos">玩家位置</param>
    /// <returns></returns>
    public void MoveclosePlayerPos(Vector2Int playerPos)
    {
        //获取可以移动的位置
        List<Vector2Int> moveablePos = GetMoveArea().ToList();
        Vector2Int pos = playerPos;
        bool flag = false;//是否找到可靠近的位置
        //玩家附近有八个位置，找到一个可降落的位置
        for (int i = -1; i <= 1 && !flag; ++i)
        {
            for (int j = -1; j <= 1 && !flag; ++j)
            {
                pos = new Vector2Int(playerPos.x + i, playerPos.y + j);

                foreach (Vector2Int ps in moveablePos)
                {
                    if (pos == ps)
                    {
                        flag = true;
                        break;
                    }
                }
            }
        }
        //移动到玩家附近
        Move(pos);
    }

    /// <summary>
    /// 撤退到玩家附近5*5格子内
    /// </summary>
    /// <param name="playerPos">被攻击的玩家的位置</param>
    public void retreat(Vector2Int playerPos)
    {
        //获取可以移动的位置
        List<Vector2Int> moveablePos = GetMoveArea().ToList();
        Vector2Int pos = playerPos;
        bool flag = false;//是否找到可靠近的位置
        //玩家附近有八个位置，找到一个可降落的位置
        for (int i = -2; i <= 2 && !flag; ++i)
        {
            for (int j = -2; j <= 2 && !flag; ++j)
            {
                pos = new Vector2Int(playerPos.x + i, playerPos.y + j);

                foreach (Vector2Int ps in moveablePos)
                {
                    //判断该位置是否可撤退
                    if (pos == ps)
                    {
                        flag = true;
                        break;
                    }
                }
            }
        }
        Move(pos);
    }
}
