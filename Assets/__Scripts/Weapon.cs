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

	static public Transform PROJECTILE_ANCHOR;

	public bool _________________;

	[SerializeField]
	private WeaponType _type = WeaponType.blaster;
	public WeaponDefinition def;
	public GameObject collar;
	public float lastShot;

	void Start()
	{
		collar = transform.Find ("Collar").gameObject;
		SetType (_type);

		if (PROJECTILE_ANCHOR == null) 
		{
			GameObject go = new GameObject("_Projectile_Anchor");
			PROJECTILE_ANCHOR = go.transform;
		}

		GameObject parentGO = transform.parent.gameObject;
		if (parentGO.tag == "Hero") 
		{ Hero.S.fireDelegate += Fire;}
	}

	public WeaponType type
	{
		get{ return(_type);}
		set{ SetType(value);}
	}

	public void SetType(WeaponType wt)
	{
		_type = wt;
		if (type == WeaponType.none) {
			this.gameObject.SetActive (false);
			return;
		} 
		else 
		{ this.gameObject.SetActive(true);}

		def = Main.GetWeaponDefinition (_type);
		collar.renderer.material.color = def.color;
		lastShot = 0;  //can always fire immediately after the type is set
	}

	public void Fire()
	{
		if (!gameObject.activeInHierarchy) 
		{ return;}
		if(Time.time - lastShot < def.delayBetweenShots)
		{ return;}

		Projectile p;
		switch (type) 
		{
		case WeaponType.blaster:
			p = MakeProjectile();
			p.rigidbody.velocity = Vector3.up * def.velocity;
			break;

		case WeaponType.spread:
			p = MakeProjectile();
			p.rigidbody.velocity = Vector3.up * def.velocity;
			p = MakeProjectile();
			p.rigidbody.velocity = new Vector3( -.2f, 0.9f, 0f) * def.velocity;
			p = MakeProjectile();
			p.rigidbody.velocity = new Vector3( .2f, 0.9f, 0f) * def.velocity;
			break;
		}
	}

	public Projectile MakeProjectile()
	{
		GameObject go = Instantiate (def.projectilePrefab) as GameObject;
		if (transform.parent.gameObject.tag == "Hero") {
			go.tag = "ProjectileHero";
			go.layer = LayerMask.NameToLayer ("ProjectileHero");
		} 
		else 
		{
			go.tag = "ProjectileEnemy";
			go.layer = LayerMask.NameToLayer ("ProjectileEnemy");
		}
		go.transform.position = collar.transform.position;
		go.transform.parent = PROJECTILE_ANCHOR;
		Projectile p = go.GetComponent<Projectile> ();
		p.type = type;
		lastShot = Time.time;
		return(p);
	}
}
