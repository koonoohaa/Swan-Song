using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
/// <summary>
/// 影袭怪
/// 对一个角色造成100%力量值的伤害，如果连续攻击同一角色，
/// 从第二次开始，每次造成150%力量值的伤害
/// （比如第一回合打了角色A，第二回合还打的角色A，那伤害会提升，中间转移过目标后要重新计算)，
/// 优先攻击血量百分比最低的敌人，移动至角色身前进行攻击，攻击后返回原位置。
/// </summary>
public class ShaAttkMonster : Unit
{
    /// <summary>
    /// 上一回合伤害的角色
    /// </summary>
    public Player hurtedPlayer
    {
        get;
        private set;
    }
    public ShaAttkMonster(Vector2Int pos) : base(new UnitData()
    {
        Name = "ShaAttkMonster",//史莱姆
        BloodMax = 80,//最大血量
        Blood = 80,//初始血量为最大血量
        Attack = 10,//攻击力
        Defence = 4,//防御力
        Speed = 2,//先攻权重
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
        attackPlayer(player);
        //撤退
        retreat(player.Position);
    }

    /// <summary>
    /// 根据血量，选择合适的攻击对象
    /// </summary>
    /// <param name="players">所有玩家</param>
    /// <returns></returns>
    public Player getAttackPlayer(List<Player> players)
    {
        int num = -1;//记录血量比最少的玩家的号码
        int i = 0;
        double minbloodPercent = int.MaxValue;//设初值为最大值

        foreach (Player p in players)
        {
            double bloodPercent = (double)p.UnitData.Blood / p.UnitData.BloodMax;
            if (bloodPercent < minbloodPercent && p.ActionStatus == ActionStatus.Running)
            {
                minbloodPercent = bloodPercent;
                num = i;
            }
            i++;
        }
        return players[num];
    }
    /// <summary>
    /// 移动到要攻击玩家附近
    /// 对一个角色造成100%力量值的伤害，如果连续攻击同一角色，
    /// 从第二次开始，每次造成150%力量值的伤害
    /// </summary>
    /// <param name="player">要攻击的玩家</param>
    public void attackPlayer(Player player)
    {
        //移动
        MoveclosePlayerPos(player.Position);
        //得到当前回合数
        int roundNumber = GameManager.Instance.GetState<BattleState>().RoundNumber;
        //不是第一回合，影袭怪已经攻击过，判断当前回合攻击玩家是否是上一轮攻击过的对象
        if (roundNumber != 1 && this.hurtedPlayer == player)
        {
            //150%伤害
            (player as IHurtable).Hurt((int)(this.UnitData.Attack*1.5), HurtType.FromUnit, this);
        }
        else//是第一回合或两次攻击对象不一样
        {
            //100%伤害
            (player as IHurtable).Hurt(this.UnitData.Attack, HurtType.FromUnit, this);
        }
        this.hurtedPlayer = player;
       
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
