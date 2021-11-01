using UnityEngine;
using System.Collections;

public class EnemyBuildParam : ICharacterBuildParam
{
	
	public EnemyBuildParam()
	{
	}
}

public class EnemyBuilder : ICharacterBuilder
{
	private EnemyBuildParam m_BuildParam = null;

	public override void SetBuildParam( ICharacterBuildParam theParam )
	{
		m_BuildParam = theParam as EnemyBuildParam;	
	}

	public override void LoadAsset( int GameObjectID )
	{
		IAssetFactory AssetFactory = PBDFactory.GetAssetFactory();
		GameObject EnemyGameObject = AssetFactory.Load( m_BuildParam.NewCharacter.GetAssetName() );
		EnemyGameObject.transform.position = new Vector3(Random.Range(-100, 200), 50, Random.Range(-100, 200));
		EnemyGameObject.gameObject.name = string.Format("Enemy[{0}]",GameObjectID);
		m_BuildParam.NewCharacter.SetGameObject( EnemyGameObject );
	}

	public override void SetCharacterAttr()
	{
		IAttrFactory theAttrFactory = PBDFactory.GetAttrFactory();
		int AttrID = m_BuildParam.NewCharacter.GetAttrID();
		EnemyAttr theEnemyAttr = theAttrFactory.GetEnemyAttr( AttrID );
        m_BuildParam.NewCharacter.SetCharacterAttr( theEnemyAttr );
	}

	public override void AddAI()
	{
		EnemyAI theAI = new EnemyAI(m_BuildParam.NewCharacter);
		m_BuildParam.NewCharacter.SetAI(theAI);
	}

    public override void AddUI()
    {
		HPUI theUI = new HPUI(m_BuildParam.NewCharacter);
		m_BuildParam.NewCharacter.SetUI(theUI);
	}
    public override void AddCharacterSystem( PBaseDefenseGame PBDGame)
	{
		PBDGame.AddEnemy( m_BuildParam.NewCharacter as IEnemy );
	}

}
