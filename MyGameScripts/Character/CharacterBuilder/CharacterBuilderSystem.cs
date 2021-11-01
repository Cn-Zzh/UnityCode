using UnityEngine;
using System.Collections.Generic;

public class CharacterBuilderSystem : IGameSystem
{
	private int m_GameObjectID = 0;

	public CharacterBuilderSystem(PBaseDefenseGame PBDGame):base(PBDGame)
	{}

	public override void Initialize()
	{}

	public override void Update()
	{}


	public void Construct(ICharacterBuilder theBuilder)
	{
		theBuilder.LoadAsset( ++m_GameObjectID );
		theBuilder.SetCharacterAttr();
		theBuilder.AddAI();
		theBuilder.AddUI();
		theBuilder.AddCharacterSystem( m_PBDGame);
	}
}
