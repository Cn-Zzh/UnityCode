using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleState : ISceneState
{
	public BattleState(SceneStateController Controller):base(Controller)
	{
		this.StateName = "GameState";
	}

	public override void StateBegin()
	{
		PBaseDefenseGame.Instance.Initinal();
	}

	public override void StateEnd()
	{
		PBaseDefenseGame.Instance.Release();
	}
			
	public override void StateUpdate()
	{	
		PBaseDefenseGame.Instance.Update();
		if( PBaseDefenseGame.Instance.ThisGameIsOver())
			m_Controller.SetState(new MainMenuState(m_Controller), "MainMenuScene" );
	}
}
