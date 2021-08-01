public class HumanDesigner : WarriorDesigner
{
    public override WarriorBehaviour WarriorType()
    {
        return new HumanBehaviour();
    }

    public override int Id()
    {
        return 0;
    }
}