using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyZombie : MonoBehaviour {

    public GameObject explosionPrefab;
    public int point=500;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {

            Instantiate(explosionPrefab, transform.position + new Vector3(0, 1.8f, 0), Quaternion.identity);

            GameManager.totalPoint += point;

            Debug.Log("hit");
        }

    }
}
