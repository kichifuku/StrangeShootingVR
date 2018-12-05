using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyExplosion : MonoBehaviour {

    ParticleSystem particle;

	void Start () {
        Destroy(gameObject, 3f);
	}
}
