using UnityEngine;
using System.Collections;

public class HeroAttr : ICharacterAttr
{
	protected int 	m_SoldierLv; 
	protected int	m_AddMaxHP; 

	public HeroAttr()
	{}

	public void SetSoldierAttr(BaseAttr BaseAttr)
	{
		base.SetBaseAttr( BaseAttr );

		m_SoldierLv = 1;
		m_AddMaxHP = 0;
	}
	
	public void SetSoldierLv(int Lv)
	{
		m_SoldierLv = Lv;
	}

	public int GetSoldierLv()
	{
		return m_SoldierLv ;
	}

	public override int GetMaxHP()
	{
		return base.GetMaxHP() + m_AddMaxHP;
	}
	public void AddMoveSpeed(int AddSpeed)
    {
		m_BaseAttr.AddMoveSpeed(AddSpeed);
    }
	public void AddMaxHP(int AddMaxHP)
	{
		m_AddMaxHP = AddMaxHP;
	}



}
