public class AlienDesigner : WarriorDesigner
{
    public override WarriorBehaviour WarriorType()
    {
        return new AlienBehaviour();
    }

    public override int Id()
    {
        return 1;
    }
}