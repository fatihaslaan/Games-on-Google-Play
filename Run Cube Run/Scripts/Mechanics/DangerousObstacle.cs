using UnityEngine;

public class DangerousObstacle : Obstacle
{
    public override void OnCollisionEnter(Collision c) //Game over on collision
    {
        if (c.gameObject.tag == "Player")
        {
            Manager.GetInstance().ChangeScene(false);
        }
    }
}