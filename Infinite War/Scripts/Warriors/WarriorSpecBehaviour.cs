using System;

[Serializable]
public class WarriorSpecBehaviour
{
    public IArm[] arms; //Arms of spec

    public float cost = 0f; //Total cost of spec
    public float range = 0f; //Max range of spec
    public float shield = 0f; //Total protection pf spec
    public float weight = 1f; //Total weight of spec (Slows down warrior)

    public GlobalAttributes.Target target = GlobalAttributes.Target.Castle; //Target of spec

    public WarriorSpecBehaviour(IArm[] _arms)
    {
        arms = new IArm[_arms.Length];
        for (int i = 0; i < arms.Length; i++) //Give values to variables related to given arm
        {
            arms[i] = _arms[i];
            if (arms[i].Range() > range)
                range = arms[i].Range();
            weight += arms[i].Weight();
            shield += arms[i].Shield();
            if (arms[i].Target() == GlobalAttributes.Target.Warrior)
                target = arms[i].Target();
            cost += arms[i].Cost();
        }
    }
}