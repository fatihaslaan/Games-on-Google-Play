using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    Manager manager;

    void Start()
    {
        manager=Manager.GetInstance();
    }

    void Update()
    {
        Vector3 direction = Vector3.forward * manager.joystick.Vertical + Vector3.right * manager.joystick.Horizontal; //Joystick Control
        transform.position += direction * speed * Time.deltaTime;

        if(transform.position.y<-4) //Player Fell
            manager.ChangeScene(false);
    }
}