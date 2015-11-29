using UnityEngine;
using System.Collections;

public class DamageScript : MonoBehaviour {

	// when hitboxes collide with each other
	void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.root != transform.root && col.tag != "Ground" && !col.isTrigger) // if the hitbox is not the same hitbox, and is not a ground collider
        {
            if(!col.transform.GetComponent<PlayerControl>().damage) // get the damage parameter
            {
                col.transform.GetComponent<PlayerControl>().damage = true;

                col.transform.root.GetComponent<Animator>().SetTrigger("Damage"); // setting the trigger to Damage
            }
        }
    }
}
