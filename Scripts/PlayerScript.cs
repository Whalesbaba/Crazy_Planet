using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
	public int maxHealth = 100;
	public int currentHealth;
	public int maxStamina = 100;
	public float currentStamina;
	public int direction;

	public float timerScore = 0;
	public Slider timerSlider;
	public bool stopTimer;

	public CharacterController controller;
										 
	public StarterAssets.ThirdPersonController playerController;  // A reference to our MovePlayer script
	public StarterAssets.StarterAssetsInputs starter;
	public HealthBar healthBar;
	public StaminaBar staminaBar;

	public static bool playerIsAlive;
	public static int enemyKills = 0;

	public bool run;

	public Animator anim;
	private int _animIDDead;
	private int _animIDHurt;

	// Start is called before the first frame update
	void Start()
	{
		Debug.Log("hook");
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);

		currentStamina = maxStamina;
		staminaBar.SetMaxStamina(maxStamina);

		playerIsAlive = true;

		starter = GameObject.Find("PlayerVee").GetComponent<StarterAssets.StarterAssetsInputs>();
		playerController = GameObject.Find("PlayerVee").GetComponent<StarterAssets.ThirdPersonController>();
		if (starter != null)
      {
			Debug.Log("hook1");
      }
		anim = GetComponent<Animator>();
		_animIDDead = Animator.StringToHash("isDeadPlayer");
		_animIDHurt = Animator.StringToHash("Hits");
		anim.SetBool(_animIDDead, false);

		enemyKills = 0;

	}

	// Update is called once per frame
	private void Update()
	{
		if (starter.sprint)
		{
			ReduceStamina(20);

		}
		else { if (currentStamina <= 100) IncreaseStamina(10); }

		run = starter.sprint;

		if (currentStamina <= 1)
		{
			starter.sprint = false;

		}

		//if (Input.GetKeyDown(KeyCode.A))
      {
			//playerController.animeState = 1;
      }
	}

	void TakeDamage(int damage)
	{
		currentHealth -= damage;

		healthBar.SetHealth(currentHealth);
	}
	public void ReduceStamina(int decrease)
	{
		currentStamina -= (float)decrease * Time.deltaTime;
		if (currentStamina < 0) { currentStamina = 0; }
		staminaBar.SetStamina(currentStamina);
	}
	public void IncreaseStamina(int increase)
	{
		currentStamina += increase * Time.deltaTime;
		staminaBar.SetStamina(currentStamina);
	}


   private void OnTriggerEnter(Collider other)
   {
      if (other.tag == "Enemy" || other.tag == "Enemy1")
      {
			TakeDamage(10);
			anim.SetBool(_animIDHurt, true);

			if (currentHealth <= 0)
         {

				playerIsAlive = false;
				anim.SetBool(_animIDDead, true);
			}
      }
   }

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Enemy")
		{

			anim.SetBool(_animIDHurt, false);
		}
	}
   public void SlideTimer()
	{


		timerScore -= 1 * Time.deltaTime;



		if (timerScore <= 0)
		{
			stopTimer = true;
		}
		if (stopTimer == false)
		{

			timerSlider.value = timerScore;

		}

		if (timerScore < 0)
		{
			timerScore = 0;
		}

	}
}
