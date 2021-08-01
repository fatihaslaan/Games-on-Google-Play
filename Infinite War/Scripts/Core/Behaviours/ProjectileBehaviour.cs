using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField]
    bool load; //Choose to load projectile on gun, before shooting

    struct Projectile
    {
        public GameObject gameObject;
        public Vector3 enemy;
        public Projectile(GameObject o, Vector3 target)
        {
            gameObject = o;
            enemy = target;
        }
    }

    [SerializeField]
    GameObject projectilePrefab;

    GameObject currentProjectile;
    List<Projectile> firedProjectiles = new List<Projectile>();
    float timer = 0f;

    float projectileSpeed = 1f;

    Vector3 enemy;

    void Start()
    {
        if (load)
        {
            projectileSpeed = 0.3f; //slower for warriors
            Load();
        }
    }

    void Load()
    {
        currentProjectile = Instantiate(projectilePrefab, transform, false);
    }

    void Update()
    {
        foreach (Projectile projectile in firedProjectiles.ToList())
        {
            projectile.gameObject.transform.position = Vector3.MoveTowards(projectile.gameObject.transform.position, projectile.enemy, projectileSpeed);
            if (Vector3.Distance(projectile.gameObject.transform.position, projectile.enemy) < 0.5f) //Projectile is close to enemy we can destroy
            {
                Destroy(projectile.gameObject);
                firedProjectiles.Remove(projectile);
            }
        }
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer < 0)
        {
            timer = 0f;
            Load();
        }
    }

    public void DestroyProjectiles()
    {
        foreach (Projectile projectile in firedProjectiles.ToList())
        {
            Destroy(projectile.gameObject);
        }
        Destroy(currentProjectile);
    }

    public void Fire(Vector3 target, float counter)
    {
        if (load)
            timer = counter / 1.5f; //Load after time
        else
            Load();
        transform.DetachChildren();
        enemy = target;
        firedProjectiles.Add(new Projectile(currentProjectile, enemy));
    }
}