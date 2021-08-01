public class HumanController : WarriorController
{
    public override int Id()
    {
        return 0;
    }

    public override int EnemyId()
    {
        return 1;
    }

    public override float Health()
    {
        return WarriorType().Health();
    }
    
    public override WarriorBehaviour WarriorType()
    {
        return new HumanBehaviour();
    }
}