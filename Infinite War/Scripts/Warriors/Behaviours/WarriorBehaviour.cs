using System.Collections.Generic;

public interface WarriorBehaviour
{
    string Name();

    float Health();

    List<IArm> Arms();

    CastleController EnemyCastle();

    int NumberOfArms();

    float SpeedMultiplier();

    float AttackSpeedMultiplier();

    float DamageMultiplier();

    float SightDistance();

    bool ExplosionOnDeath();
}