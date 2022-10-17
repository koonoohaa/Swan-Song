using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// ��λ��Ⱦ��
/// </summary>
public class UnitRenderer
{
    Transform _unitRoot;
    Dictionary<Unit, UnitView> _unitViews = new();
    BattleState _battleState;
    public UnitRenderer(BattleState battleState)
    {
        _battleState = battleState;
        _unitRoot = new GameObject("Unit Root").transform;
    }

    /// <summary>
    /// ������λ��ͼ
    /// </summary>
    /// <param name="unit"></param>
    /// <returns></returns>
    public UnitView CreateUnitView(Unit unit)
    {
        var model = Object.Instantiate(UnitDataManager.Instance.UnitViews[unit.UnitData.ViewType],
            _unitRoot);
        var view = model.GetComponent<UnitView>();
        _unitViews.Add(unit, view);
        view.transform.position = _battleState
            .MapRenderer.Grid.CellToWorld(unit.Position.ToVector3Int());

        _battleState.Animator.BindindUnitAnimation(unit, view);
        return view;
    }
    /// <summary>
    /// ѡ�е�λ
    /// </summary>
    /// <param name="units"></param>
    public void SelectUnit(IEnumerable<Unit> units = null)
    {
        if (units != null)
        {
            foreach (var unit in _unitViews)
            {
                if (units.Contains(unit.Key))
                {
                    unit.Value.Select();
                }
                else
                {
                    unit.Value.Unselect();
                }
            }
        }
        else
        {
            foreach (var view in _unitViews.Values)
            {
                view.Unselect();
            }
        }
    }
}