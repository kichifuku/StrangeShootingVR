using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyTarget : MonoBehaviour {

    public GameObject explosionPrefab;
    GameObject playerCamera;
    public int point;

    private void Start()
    {
        playerCamera = GameObject.Find("OVRCameraRig");
        if(gameObject.tag == "enemy")
        {
            point = 150;
        }
        if (gameObject.tag == "200pt") point = 200;
        if (gameObject.tag == "250pt") point = 250;
        if (gameObject.tag == "300pt") point = 300;
        if (gameObject.tag == "350pt") point = 350;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            GameObject prefab = Instantiate(explosionPrefab);
            prefab.transform.position = transform.position;
            prefab.transform.LookAt(playerCamera.transform.position);
            prefab.transform.GetComponentInChildren<Text>().text = point + "pt";

            GameManager.totalPoint += point;

            Debug.Log("hit");
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "floor")
        {
            Destroy(gameObject);
        }

    }
}
