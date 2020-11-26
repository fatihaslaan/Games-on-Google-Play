using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jump : MonoBehaviour
{
    //public GameObject player;
    public AudioSource jumps;
    public Image flashcooldownimg;
    float flashcooldown=0f;
    Rigidbody playerrb;
    float forwardforce=100;
    float upforce=900;
    float horizantalforce = 700;
    Animation anim;
    // Start is called before the first frame update
    void Start()
    {
        playerrb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animation>();
    }

    void Update()
    {
        if(flashcooldown>0)
            flashcooldown-=Time.deltaTime;
        flashcooldownimg.fillAmount=(flashcooldown/5f);
    }

	public void jump(bool right)
	{
        if (Global.jumpleft <= 0||!Global.game)
            return;
        Global.jumpleft--;
		if(right)
		{
            playerrb.AddForce(new Vector3(horizantalforce,upforce,forwardforce));
		}
		else
		{
            playerrb.AddForce(new Vector3(-horizantalforce, upforce, forwardforce));
		}
        if ((playerrb.velocity.y > -0.1f && playerrb.velocity.y < 0.1f))
        {
            anim.Stop("Running");
            anim.Play("Jumping");
        }
        Global.running=false;
        if(Global.sound)
            jumps.Play();
	}
	
    public void flash()
    {
        if(flashcooldown<=0)
            flashcooldown=5f;
        else
            return;
        playerrb.transform.position=new Vector3(playerrb.transform.position.x,playerrb.transform.position.y,playerrb.transform.position.z+50);
    }
}
