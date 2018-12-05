using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour {

    public GameObject exPrefab;

    GameManager gameManager;

    void Start () {

        gameManager = FindObjectOfType<GameManager>();
	}
	
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        gameManager.GameStart();

        Instantiate(exPrefab, gameObject.transform.position+new Vector3(0,3,0), Quaternion.identity);
        Destroy(gameObject);
    }
}
