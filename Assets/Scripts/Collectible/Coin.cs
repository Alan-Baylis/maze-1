using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
	public float RotateSpeed = 5.0f;
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.left * Time.deltaTime * RotateSpeed);
	}

	void OnTriggerEnter(Collider collide) {
		EventController.Event(Events.CoinCollected);
		Destroy(gameObject);
	}
}
