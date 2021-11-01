#region  组件
using UnityEngine;

public abstract class ComponentBase
{
    public virtual void Do()//执行的方法
    {

    }

    public abstract string GetButName();//返回对应的名字
}

public class Use : ComponentBase
{
    public override void Do()
    {
        base.Do();

        Debug.Log("使用");
    }
    public override string GetButName()
    {
        return "使用";
    }
}
public class UseMore : ComponentBase
{
    public override void Do()
    {
        base.Do();
        Debug.Log("批量使用");
    }
    public override string GetButName()
    {
        return "批量使用";
    }
}

public class Sell : ComponentBase
{
    public override void Do()
    {
        base.Do();
        Debug.Log("出售物品");
    }

    public override string GetButName()
    {
        return "出售";
    }
}
public class Equip : ComponentBase
{
    public override void Do()
    {
        base.Do();
        Debug.Log("装备装备");
    }

    public override string GetButName()
    {
        return "装备";
    }
}
public class Compound : ComponentBase
{
    public override void Do()
    {
        base.Do();
        Debug.Log("合成");
    }

    public override string GetButName()
    {
        return "合成";
    }
}

public class Share : ComponentBase
{
    public override void Do()
    {
        base.Do();
        Debug.Log("分享");
    }

    public override string GetButName()
    {
        return "分享";
    }
}
#endregion