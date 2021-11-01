using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUI
{
    protected ICharacter Owner = null;
    protected ICharacterAttr attr = null;
    Slider hp = null;
    int nowHp = 0;
    int maxHp = 0;
    float time = 0;

    public HPUI(ICharacter character)
    {
        Owner = character;
        hp = PBDFactory.GetAssetFactory().Load("Hp").GetComponent<Slider>();
        hp.transform.SetParent(UITool.FindUIGameObject("Canvas").transform,false);
        attr = Owner.GetCharacterAttr();
        nowHp = attr.GetMaxHP();
        maxHp = attr.GetMaxHP();
        hp.gameObject.SetActive(false);
    }
    public void Change(int HP,int maxHP)
    {
        time = 5;
        hp.gameObject.SetActive(true);
        //Debug.Log(attr.GetNowHP() + "        " + attr.GetMaxHP());
        nowHp = HP;
        maxHp = maxHP;
        hp.value = HP * 1.0f / maxHP;
        if(hp.value == 0)
        {
            GameObject.Destroy(hp.gameObject);
        }
    }
    public void Update()
    {
        if (time <= 0)
            return;
        time -= Time.deltaTime;
        Vector3 v3 = Owner.GetPosition() - Camera.main.transform.position - new Vector3(0, Owner.GetPosition().y - Camera.main.transform.position.y, 0);
        float Face = Vector3.Dot(v3, Camera.main.transform.forward) / v3.magnitude / Camera.main.transform.forward.magnitude;
        if (Face < 0.2)
        {
            hp.gameObject.SetActive(false);
        }
        else
        {
            hp.gameObject.SetActive(true);
        }
        hp.transform.position = Camera.main.WorldToScreenPoint(Owner.GetPosition() + new Vector3(0, 2, 0));
        hp.value = nowHp * 1.0f / maxHp;
        if(time <= 0)
        {
            hp.gameObject.SetActive(false);
        }
    }
}
