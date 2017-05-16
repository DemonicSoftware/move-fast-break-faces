using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
   public int HP = 5;
   public bool isEnemy = true;

   // Use this for initialization
   void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
   public void Damage(int damageCount)
   {
      HP -= damageCount;

      if (HP <= 0)
      {
         // Dead!
         Destroy(gameObject);
      }
   }

   void OnTriggerEnter2D(Collider2D otherCollider)
   {
      // Is this a shot?
      Shot shot = otherCollider.gameObject.GetComponent<Shot>();
      Hand hand = otherCollider.gameObject.GetComponent<Hand>();
      if (shot != null)
      {
         // Avoid friendly fire
         if (shot.isEnemy != isEnemy)
         {
            Damage(shot.damage);

            // Destroy the shot
            Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
         }
      }
        if (hand != null)
        {
            // Avoid friendly fire
            if (hand.isEnemy != isEnemy)
            {
                Damage(hand.damage);

                // Destroy the shot
                Destroy(hand.gameObject); // Remember to always target the game object, otherwise you will just remove the script
            }
        }
    }
}
