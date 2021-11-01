using UnityEngine;
using System.Collections;


public abstract class ICharacterBuildParam
{
	public ICharacter   NewCharacter = null;		
	public Vector3      SpawnPosition;
	public int          AttrID; 
	public string       AssetName;
	public string       IconSpriteName;
}

public abstract class ICharacterBuilder
{
	public abstract void SetBuildParam( ICharacterBuildParam theParam );
	public abstract void LoadAsset	( int GameObjectID );
	
	public abstract void AddAI		();
	public abstract void SetCharacterAttr();
	public abstract void AddCharacterSystem( PBaseDefenseGame PBDGame );

	public abstract void AddUI();
}

