using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : MonoBehaviour
{
	float speed=0.5f;
    public AudioSource run;
    public AudioSource land;

    Animation anim;
    void Start()
    {
        anim = GetComponentInChildren<Animation>();
        anim.Play("Running");
    }
    void Update()
    {
        Global.playerpos=transform.position.z;
        if (Global.game&&!Global.tut)
        {
            transform.Translate(0, 0, speed);
            if(Global.running&&!run.isPlaying&&Global.sound)
                run.Play();
            else if(!Global.running||Global.fall)
                run.Stop();
        }
    }
}
