using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedScript : MonoBehaviour {

	public float speedBoost;
	public float maxSpeedDuringBoost;

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
				AddSpeedBoostErl (ErlMan);
			}

			IsaManager IsaMan = Isa.GetComponent<IsaManager> ();
			if (IsaMan != null)
			{
				AddSpeedBoostIsa (IsaMan);
			}

			Erl.AddComponent<SpeedTimer> ();
			Isa.AddComponent<SpeedTimer> ();

			Destroy (gameObject, 0.0F);
		}
	}

	void AddSpeedBoostErl (ErlManager Erl)
	{
		Erl.moveSpeed = FindSpeedBoost(Erl.moveSpeed);
	}

	void AddSpeedBoostIsa (IsaManager Isa)
	{
		Isa.maxSpeed = FindSpeedBoost (Isa.maxSpeed);
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
