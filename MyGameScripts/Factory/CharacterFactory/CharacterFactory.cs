using UnityEngine;
using System.Collections;

public class CharacterFactory : ICharacterFactory
{
	private CharacterBuilderSystem m_BuilderDirector = new CharacterBuilderSystem( PBaseDefenseGame.Instance );

	public override IHero CreateHero(int Lv)
	{
		SoldierBuildParam SoldierParam = new SoldierBuildParam();

		SoldierParam.NewCharacter = new IHero();

		if ( SoldierParam.NewCharacter == null)
			return null;

		SoldierParam.Lv		  = Lv;

		SoldierBuilder theSoldierBuilder = new SoldierBuilder();
		theSoldierBuilder.SetBuildParam( SoldierParam ); 
		
		m_BuilderDirector.Construct( theSoldierBuilder );
		return SoldierParam.NewCharacter  as IHero;
	}
	public override IEnemy CreateEnemy( ENUM_Enemy emEnemy)
	{
		EnemyBuildParam EnemyParam = new EnemyBuildParam();

		switch( emEnemy)
		{
		case ENUM_Enemy.Chest:
			EnemyParam.NewCharacter = new EnemyChest();
			break;
		case ENUM_Enemy.Chushou:
			EnemyParam.NewCharacter = new EnemyChushou();
			break;
		case ENUM_Enemy.Slime:
			EnemyParam.NewCharacter = new EnemySlime();
			break;
		case ENUM_Enemy.Tortoise:
			EnemyParam.NewCharacter = new EnemyTortoise();
			break;
		default:
		Debug.LogWarning("無法建立["+emEnemy+"]");
		return null;
		}

		if( EnemyParam.NewCharacter == null)
			return null;

				
		EnemyBuilder theEnemyBuilder = new EnemyBuilder();
		theEnemyBuilder.SetBuildParam( EnemyParam ); 
		
		m_BuilderDirector.Construct( theEnemyBuilder );
		return EnemyParam.NewCharacter  as IEnemy;
	}

}


