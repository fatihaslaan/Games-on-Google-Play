using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float maxRange;

    float speed = 10f;
    float screenSize;
    float screenHeight;

    void Start()
    {
        screenSize = Screen.width / 2;
        screenHeight = Screen.height / 5;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.y > screenHeight && Input.mousePosition.y < Screen.height - screenHeight) //Determine touchable area
            {
                if (transform.position.x < maxRange && Input.mousePosition.x > screenSize)
                    transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
                if (transform.position.x > -maxRange && Input.mousePosition.x < screenSize)
                    transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
            }
        }
    }
}