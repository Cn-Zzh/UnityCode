using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainHeroCommand : ITrainCommand
{
	public TrainHeroCommand()
	{
	}

	public override void Execute()
	{
		ICharacterFactory Factory = PBDFactory.GetCharacterFactory();
		IHero hero = Factory.CreateHero(1);
		Camera.main.GetComponent<ThirdCameraCon>().target = hero.GetGameObject().transform;

		IAttrFactory AttrFactory = PBDFactory.GetAttrFactory();
		HeroAttr PreAttr = AttrFactory.GetSoldierAttr();
		hero.SetCharacterAttr(PreAttr);
	}
}
