using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health {

	private int health;
	private int baseHealth;


	public Health(int startingHealth) {
		baseHealth = startingHealth;
		health = baseHealth;
	}

	public void dealDamage(int damage) {
		health -= damage;
	}

	void restoreHealth(int healing) {
		health += healing;
	}

	public void refillHealth() {
		health = baseHealth;
	}

	public int getHealth() {
		return health;
	}
}
