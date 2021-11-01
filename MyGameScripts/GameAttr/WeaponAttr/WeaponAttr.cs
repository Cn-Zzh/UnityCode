using UnityEngine;
using System.Collections;

public class WeaponAttr
{
	public int 		Atk 	{get; private set;} 

	public string AttrName { get; private set; }

	public WeaponAttr(int AtkValue,string AttrName)
	{
		this.Atk = AtkValue;
		this.AttrName = AttrName;
	}
}
