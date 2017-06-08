using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedScript : MonoBehaviour {

	public float speedBoost;
	public float maxSpeedDuringBoost;

	private float erlOldSpeed;
	private float isaOldSpeed;

	private GameObject Erl;
	private GameObject Isa;

	// Use this for initialization
	void Start () {
		Erl = GameObject.Find ("Erl");
		Isa = GameObject.Find ("Isa");
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.name == "Isa" || other.name == "Erl")
		{
			ErlManager ErlMan = Erl.GetComponent<ErlManager> ();
			if (ErlMan != null)
			{
				erlOldSpeed = AddSpeedBoostErl (ErlMan);
			}

			IsaManager IsaMan = Isa.GetComponent<IsaManager> ();
			if (IsaMan != null)
			{
				isaOldSpeed = AddSpeedBoostIsa (IsaMan);
			}


//			SphereCollider sc = gameObject.AddComponent("SphereCollider") as SphereCollider;
//			SphereCollider sc = gameObject.AddComponent(typeof(SphereCollider)) as SphereCollider;
//			SphereCollider sc = gameObject.AddComponent<SphereCollider>() as SphereCollider;

			SpeedTimer erlTimer = Erl.AddComponent<SpeedTimer> () as SpeedTimer;
			SpeedTimer isaTimer = Isa.AddComponent<SpeedTimer> () as SpeedTimer;

			erlTimer.oldSpeed = erlOldSpeed;
			isaTimer.oldSpeed = isaOldSpeed;

			erlTimer.whose = "Erl";
			isaTimer.whose = "Isa";

			Destroy (gameObject, 0.0F);
		}
	}

	float AddSpeedBoostErl (ErlManager Erl)
	{
		float oldSpeed = Erl.moveSpeed;
		Erl.moveSpeed = FindSpeedBoost(Erl.moveSpeed);
		return oldSpeed;
	}

	float AddSpeedBoostIsa (IsaManager Isa)
	{
		float oldSpeed = Isa.maxSpeed;
		Isa.maxSpeed = FindSpeedBoost (Isa.maxSpeed);
		return oldSpeed;
	}

	float FindSpeedBoost (float initSpeed)
	{
		float newSpeed = initSpeed + speedBoost;

		if (newSpeed > maxSpeedDuringBoost)
		{
			newSpeed = maxSpeedDuringBoost;
		}

		return newSpeed;
	}

}
