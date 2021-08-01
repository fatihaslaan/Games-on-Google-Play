using UnityEngine;
using UnityEngine.UI;

public abstract class BaseController : MonoBehaviour
{
    public float health;
    public Image healthBar;

    Transform canvas;

    Camera camera;

    public virtual void Awake()
    {
        camera=Camera.main;
        health = MaxHealth();
        canvas=transform.Find("Canvas");
        healthBar = canvas.GetChild(0).GetChild(0).GetComponent<Image>();
        InvokeRepeating("HealthRotation", 0.0f, 0.1f);
    }

    void HealthRotation() //Rotates canvas of warriors all the time to make them looking to camera 
    {
        canvas.transform.LookAt(camera.transform);
    }

    public abstract int Id();

    public abstract int EnemyId();
    
    public abstract Vector3 Location();

    public abstract float Health();

    public virtual float MaxHealth()
    {
        return Health();
    }

    public virtual void RecievedDamage(float damage)
    {
        health -= damage;
        healthBar.fillAmount = health / MaxHealth();
    }
}