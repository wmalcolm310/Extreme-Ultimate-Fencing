using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    public int PlayerNumber = 0; 
    Transform enemy;

    Rigidbody2D rig2d;
    Animator anim;

    float horizontal;
    float vertical;
    public float maxSpeed = 10;
    bool move;
    Vector3 movement;

    public float attackRate = 0.3f;
    bool[] attack = new bool[1]; // array of attacks, have one attack for now
    float[] attacktimer = new float[1]; // time of attack animation
    int[] timesPressed = new int[1]; // times attack button is pressed

    public bool damage;
    public float noDamage = 1;
    float noDamageTimer;
    
	// Use this for initialization
	void Start () {
        rig2d = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        // find the characters in the game
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach(GameObject pl in players) // for each player
        {
            if(pl.transform != this.transform) // if not the enemy
            {
                enemy = pl.transform; // transform the next player into the enemy
            }
        }
	}

    void Update()
    {
        AttackInput();
        ScaleCheck();
        Damage();
        UpdateAnimator();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal" + PlayerNumber.ToString()); // movement, input axes: horizontal1 and horizontal2
        vertical = Input.GetAxis("Vertical"); // for jumps
        Vector3 movement = new Vector3(horizontal, 0, 0); // initializing horizontal variable

        if (attack[0]) // first attack
        {
            rig2d.velocity = Vector3.zero; // stopping movement during an attack
            movement = Vector3.zero; // stopping movement during an attack
        }
        else
            rig2d.AddForce(movement * maxSpeed); // move

            
        
    }
    void ScaleCheck()
    {
        if (transform.position.x > enemy.position.x) // if player is to the left
            transform.localScale = new Vector3(-1, 1, 1); // reverse the position when enemy is on the other side
        else
            transform.localScale = Vector3.one; // have the same position
    }
    void AttackInput()
    {
        if(Input.GetButtonDown("Attack1" + PlayerNumber.ToString())) // when attack button is pressed
        {
            attack[0] = true; // attack animation is played
            attacktimer[0] = 0; // attack time
            timesPressed[0]++; // number of times pressed
            rig2d.velocity = Vector3.zero; // stop movement
            movement = Vector3.zero; // stop movement

        }
        if (attack[0]) 
        {
            attacktimer[0] += Time.deltaTime;
            rig2d.velocity = Vector3.zero; // stop movement
            movement = Vector3.zero; // stop movement


            if (attacktimer[0] > attackRate || timesPressed[0] >= 2) // stop when higher than attack rate, or pressed it twice in succession
            {
                attacktimer[0] = 0;
                attack[0] = false;
                timesPressed[0] = 0;

            }
        }
       
        

     }
    void Damage()
    {
        if (damage) // if damage trigger is activated
        {
            noDamageTimer += Time.deltaTime; // damage is added

            if (noDamageTimer > noDamage) // resets 
            {
                damage = false;
                noDamageTimer = 0;
            }
        }
    }
    void UpdateAnimator ()
    {
        anim.SetFloat("Movement", Mathf.Abs(horizontal)); // sets the float to the movement parameter
        anim.SetBool("Attack", attack[0]); // sets the bool to the Attack parameter
        
    }


    
    
	
}
