using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLightController : MonoBehaviour {
	
	void Update () {
        transform.Rotate(0, 250.0f*Time.deltaTime, 0);
	}
}
