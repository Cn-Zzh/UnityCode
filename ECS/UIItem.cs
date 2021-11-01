using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ShowType
{
    bag,
    shop
}

public enum ComponentType
{
    use,
    sell,
    share,
    compound,
    usemore,
    equip
}
public class UIItem
{
    public Dictionary<ShowType, Action> dic;
    public Item item;
    public Text text;
    public Text numText;

    public UIItem(string name,Transform parent)
    {
        dic = new Dictionary<ShowType, Action>()
        {
            { ShowType.bag,()=>{Tips.Get().Show(item); } },
            { ShowType.shop,()=>{ } }
        };
        GameObject game = GameObject.Instantiate(Resources.Load<GameObject>("ECS/" + name), parent, false);
        game.GetComponent<Button>().onClick.AddListener(()=> { dic[item.show].Invoke(); });
        text = game.transform.Find("Text").GetComponent<Text>();
        numText = game.transform.Find("numText").GetComponent<Text>();
    }
    public void SetItem(DataItem data,ShowType type)
    {
        SetItem(Item.CreateEntityByID(data.cif.id, type));
    }

    public void SetItem(Item item)
    {
        this.item = item;
        text.text = item.data.cif.name;
        numText.text = item.data.cif.num.ToString();
    }
}
