using UnityEngine;
using System.Collections;

public enum ENUM_Weapon
{
	Null 	= 0,
	Spear 	= 1,
	Knife	= 2,	
	Sword	= 3,	
	Max	,
}


public abstract class IWeapon
{
	protected ENUM_Weapon m_emWeaponType = ENUM_Weapon.Null;

	
	protected int		   m_AtkPlusValue = 0;		  	
	protected WeaponAttr m_WeaponAttr = null;		  	
	protected ICharacter  m_WeaponOwner = null;
	protected GameObject m_WeaponGameobject = null;
                              
	
	public IWeapon(){}
	
	public ENUM_Weapon GetWeaponType()
	{
		return  m_emWeaponType;
	}

	public void SetOwner( ICharacter Owner )
	{
		m_WeaponOwner = Owner;
	}

	public void SetWeaponAttr(WeaponAttr theWeaponAttr)
	{
        m_WeaponAttr = theWeaponAttr;
		m_WeaponGameobject = Resources.Load<GameObject>(m_WeaponAttr.AttrName);
	}

	public WeaponAttr GetWeaponAttr()
    {
		return m_WeaponAttr;
    }

	public int GetAtkValue()
	{
		return m_WeaponAttr.Atk ;
	}
	public GameObject GetGameObject()
    {
		return m_WeaponGameobject;
    }
	





}

