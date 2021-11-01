using UnityEngine;
using System.Collections;

// 建立Soldier時所需的參數
public class SoldierBuildParam : ICharacterBuildParam
{
	public int 			Lv = 1;
	public SoldierBuildParam()
	{
	}
}

// Soldier各部位的建立
public class SoldierBuilder : ICharacterBuilder
{
	private SoldierBuildParam m_BuildParam = null;

	public override void SetBuildParam( ICharacterBuildParam theParam )
	{
		m_BuildParam = theParam as SoldierBuildParam;	
	}
	
	public override void LoadAsset( int GameObjectID )
	{
		IAssetFactory AssetFactory = PBDFactory.GetAssetFactory();
		GameObject SoldierGameObject = AssetFactory.Load( m_BuildParam.NewCharacter.GetAssetName() );
		SoldierGameObject.gameObject.name = "Hero";
		m_BuildParam.NewCharacter.SetGameObject( SoldierGameObject );
	}

	public override void SetCharacterAttr()
	{
		IAttrFactory theAttrFactory = PBDFactory.GetAttrFactory();
		int AttrID = m_BuildParam.NewCharacter.GetAttrID();
		HeroAttr theSoldierAttr = theAttrFactory.GetSoldierAttr(); 


		theSoldierAttr.SetSoldierLv( m_BuildParam.Lv );
		
		m_BuildParam.NewCharacter.SetCharacterAttr( theSoldierAttr );
	}

	public override void AddAI()
	{
		SoldierAI theAI = new SoldierAI(m_BuildParam.NewCharacter);
		m_BuildParam.NewCharacter.SetAI(theAI);
	}

	public override void AddUI()
	{
		HPUI theUI = new HPUI(m_BuildParam.NewCharacter);
		m_BuildParam.NewCharacter.SetUI(theUI);
	}

	public override void AddCharacterSystem( PBaseDefenseGame PBDGame )
	{
		PBDGame.SetHero(m_BuildParam.NewCharacter as IHero);
	}
}