public class AlienController : WarriorController
{
    public override int Id()
    {
        return 1;
    }

    public override int EnemyId()
    {
        return 0;
    }

    public override float Health()
    {
        return WarriorType().Health();
    }
    
    public override WarriorBehaviour WarriorType()
    {
        return new AlienBehaviour();
    }
}