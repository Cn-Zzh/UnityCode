using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCfg
{
    public int id;
    public string name;
    public string icon;
    public int num;

    public int isUse;
    public int isSell;
    public int isShare;
    public int isUseMore;
    public int isEquip;
    public int isCompound;
}

public class DataItem
{
    public DataCfg cif;

    public DataItem(int id)
    {
        cif = new DataCfg();
        cif.id = id;
        cif.name = id.ToString();
        cif.icon = id.ToString();
        switch (id)
        {
            case 1:
                cif.isUse = 1;
                cif.isSell = 1;
                cif.isShare = 1;
                cif.isUseMore = 0;
                cif.isEquip = 0;
                cif.isCompound = 0;
                cif.num = 5;
                break;
            case 2:
                cif.isUse = 1;
                cif.isSell = 1;
                cif.isShare = 1;
                cif.isUseMore = 1;
                cif.isEquip = 0;
                cif.isCompound = 0;
                cif.num = 8;
                break;
            case 3:
                cif.isUse = 0;
                cif.isSell = 1;
                cif.isShare = 1;
                cif.isUseMore = 0;
                cif.isEquip = 1;
                cif.isCompound = 0;
                cif.num = 1;
                break;
            case 4:
                cif.isUse = 0;
                cif.isSell = 1;
                cif.isShare = 1;
                cif.isUseMore = 0;
                cif.isEquip = 0;
                cif.isCompound = 1;
                cif.num = 4;
                break;
            default:
                break;
        }
    }

    public DataItem GetItemDataById(int id)
    {
        DataItem data = new DataItem(id);
        return data;
    }
}
