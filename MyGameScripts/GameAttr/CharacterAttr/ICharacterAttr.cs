using UnityEngine;
using System.Collections;

public abstract class ICharacterAttr
{
	protected BaseAttr m_BaseAttr= null; 	
	
	protected int 	 m_NowHP = 0;		
	public ICharacterAttr(){}

	public void SetBaseAttr( BaseAttr BaseAttr )
	{
		m_BaseAttr = BaseAttr;
	}
	public void UnderAttack(ICharacter Attacker)
    {
		m_NowHP -= Attacker.GetAtkValue();
    }
	public BaseAttr GetBaseAttr()
	{
		return m_BaseAttr;
	}
	
	public int GetNowHP()
	{
		return m_NowHP;
	}

	public virtual int GetMaxHP()
	{
		return m_BaseAttr.GetMaxHP();
	}

	public void FullNowHP()
	{
		m_NowHP = GetMaxHP();
	}
	
	public virtual int GetMoveSpeed()
	{
		return m_BaseAttr.GetMoveSpeed();
	}
		
	public virtual string GetAttrName()
	{
		return m_BaseAttr.GetAttrName();
	}

	public virtual void InitAttr()
	{ 
		FullNowHP();
	}

}
