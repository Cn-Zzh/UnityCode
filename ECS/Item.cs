using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public ShowType show = ShowType.bag;
    public DataItem data;
    public Dictionary<ComponentType, ComponentBase> dic = new Dictionary<ComponentType, ComponentBase>();

    static public Item CreateEntityByID(int id, ShowType type = ShowType.bag)
    {
        Item item = new Item();
        item.CreateByID(id, type);
        return item;
    }

    public void CreateByID(int id, ShowType type = ShowType.bag)
    {
        data = new DataItem(id);
        InjectAction(type);
    }

    public void InjectAction(ShowType type)
    {
        show = type;
        if (data.cif.isCompound == 1)
        {
            Compound sell = new Compound();
            dic.Add(ComponentType.compound, sell);
        }
        if(data.cif.isUse == 1)
        {
            Use use = new Use();
            dic.Add(ComponentType.use, use);
        }
        if (data.cif.isEquip == 1)
        {
            Equip equip = new Equip();
            dic.Add(ComponentType.equip, equip);
        }
        if (data.cif.isUseMore == 1)
        {
            UseMore usemore = new UseMore();
            dic.Add(ComponentType.usemore, usemore);
        }
        if (data.cif.isShare == 1)
        {
            Share share = new Share();
            dic.Add(ComponentType.share, share);
        }
        if (data.cif.isSell == 1)
        {
            Sell sell = new Sell();
            dic.Add(ComponentType.sell, sell);
        }
    }
}
