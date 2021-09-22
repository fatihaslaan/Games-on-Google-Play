using UnityEngine;

public class BouncyObstacle : Obstacle
{
    public override void OnCollisionEnter(Collision c) //Addforce to player on collision
    {
        float force = 250;
        if (c.gameObject.tag == "Player")
        {
            Vector3 dir = transform.position - c.transform.position;
            dir.Normalize();
            c.gameObject.GetComponent<Rigidbody>().AddForce(dir * force);
        }
    }
}