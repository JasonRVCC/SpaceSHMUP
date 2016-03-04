using UnityEngine;
using System.Collections;

public enum WeaponType{
	none,		//default / no weapon
	blaster,	//simple blaster
	spread,		//two shots simultaneously
	phaser,		//shots the move in waves [NI]
	missle,		//homing missles [NI]
	laser,		//damage over time [NI]
	shield		//raise shield level
}

[System.Serializable]
public class WeaponDefinition {
	public WeaponType type = WeaponType.none;
	public string letter;
	public Color color = Color.white;
	public GameObject projectilePrefab;
	public Color projectileColor = Color.white;
	public float damageOnHit = 0;
	public float continuousDamage = 0;
	public float delayBetweenShots = 0;
	public float velocity = 20;
}

public class Weapon : MonoBehaviour {

	//fill in later
}
