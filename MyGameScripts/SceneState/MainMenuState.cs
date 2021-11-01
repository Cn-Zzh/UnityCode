using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuState : ISceneState
{
	public MainMenuState(SceneStateController Controller):base(Controller)
	{
		this.StateName = "MainMenuState";
	}

	public override void StateBegin()
	{
		Button tmpBtn = UITool.GetUIComponent<Button>("StartGameBtn");
		if(tmpBtn!=null)
			tmpBtn.onClick.AddListener( ()=> OnStartGameBtnClick(tmpBtn) );
	}
			
	private void OnStartGameBtnClick(Button theButton)
	{
		m_Controller.SetState(new BattleState(m_Controller), "GameScene" );
	}

}
