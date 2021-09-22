using UnityEngine;

public class EndPointTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            Manager.GetInstance().ChangeScene(true); //Level Passed
        }
    }
}