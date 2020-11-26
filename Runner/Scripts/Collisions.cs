using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    Rigidbody rb;
    Animation anim;
    public AudioSource land;
    public AudioSource fall;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animation>();
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag=="Platform")
        {
            Global.score += Global.jumpleft+1;
            Global.jumpleft=Global.defjump;
            anim.Play("Landing");
            anim.PlayQueued("Running", QueueMode.CompleteOthers);
            Global.fall = false;
            Global.landed=true;
            Global.running=true;
            if(Global.sound)
                land.Play();
        }
        if(col.gameObject.tag=="Bound")
        {
            Global.game = false;
            if (Global.score > Global.highscore)
                Global.highscore = Global.score;
            Global.jumpleft = Global.defjump;
        }
    }

    void Update()
    {        
        if (rb.velocity.y < -5f && !Global.fall)
        {
            anim.Play("Falling");
            Global.fall = true;
            if(Global.sound&&!fall.isPlaying)
                fall.Play();            
        }
        if (rb.velocity.y > 0.5f)
        {
            Global.fall = false;            
        }
        if(!Global.fall||Global.landed)
            fall.Stop();
    }
}
