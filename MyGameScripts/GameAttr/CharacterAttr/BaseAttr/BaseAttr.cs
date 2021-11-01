using UnityEngine;
using System.Collections;

public abstract class BaseAttr
{			
	public abstract int 	GetMaxHP();
	public abstract int 	GetMoveSpeed();
	public abstract string 	GetAttrName();

	public virtual void AddMoveSpeed(int AddSpeed)
    {

    }
}

public class CharacterBaseAttr : BaseAttr
{
	private int 	m_MaxHP;		
	private int  	m_MoveSpeed;	
	private string 	m_AttrName;		

	public CharacterBaseAttr(int MaxHP,int MoveSpeed, string AttrName)
	{
		m_MaxHP = MaxHP;
		m_MoveSpeed = MoveSpeed;
		m_AttrName = AttrName;
	}

	public override int GetMaxHP()
	{
		return m_MaxHP;
	}

	public override int GetMoveSpeed()
	{
		return m_MoveSpeed;
	}

	public override string GetAttrName()
	{
		return m_AttrName;
	}
	public override void AddMoveSpeed(int AddSpeed)
	{
		m_MoveSpeed += AddSpeed;
	}
}

public class EnemyBaseAttr : CharacterBaseAttr
{
	public EnemyBaseAttr(int MaxHP,int MoveSpeed, string AttrName):base(MaxHP,MoveSpeed,AttrName)
	{
		
	}

}



