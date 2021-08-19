using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	//Handling
	public float health = 100;
	public float knockbackAmount = 100f;
	//Components
	public GameObject highscores;
	public GameObject hud;
	private Rigidbody r;
	private CharacterController c;
	private PlayerController p;
	public GameObject damageFlash;
	private Animator anim;
	public Rigidbody weapons;
    public AudioClip deathJingle;
    public GameObject aliveMusic;
	//Internal
	bool knockingBack;
    bool dead;

	void Awake()
	{
		r = GetComponent<Rigidbody>();
		c = GetComponent<CharacterController>();
		p = GetComponent<PlayerController>();
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		if (health >100)
		{
			health = 100;
		}
	}

	void Damage(float amount)
	{
        if (health <= 0 && !dead)
        {
            print("The player has died.");
            highscores.SetActive(true);
            hud.SetActive(false);
            aliveMusic.SetActive(false);
            p.enabled = false;//stop controlling
            p.tag = "Untagged";//untag
            weapons.isKinematic = false;//detach weapons
            weapons.GetComponent<Collider>().isTrigger = false;//throw weapons
            weapons.transform.parent = null;//throw weapons
            //slow down time
            anim.SetBool("Dead", true);//you die
            highscores.GetComponent<AudioSource>().clip = deathJingle;//Set the death song
            highscores.GetComponent<AudioSource>().Play();
            highscores.GetComponent<AudioSource>().loop = false;
            dead = true;
        }

		if (!knockingBack)
		{
			{
				StartCoroutine(KnockBack( 0.4f, 100));
				StartCoroutine("DamageFlash");
				health -= amount;
			}

		}
	}

	public IEnumerator KnockBack(float duration, float amt)
	{
		knockingBack = true;//mark knockback
		c.enabled = false;//turn off c
		p.canMove = false;//turn off p
		r.isKinematic = false;//turn on r
		r.AddForce (-transform.forward * amt);//knockback
		yield return new WaitForSeconds(duration);//wait a half second
		c.enabled = true;//turn on c
		p.canMove = true;//turn on p
		r.isKinematic = true;//turn on r
		knockingBack = false;//mark knockback
	}

	IEnumerator DamageFlash()
	{
		damageFlash.SetActive(true);
		yield return new WaitForSeconds(0.1f);
		damageFlash.SetActive(false);
	}

}
