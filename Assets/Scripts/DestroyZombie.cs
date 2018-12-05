using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyZombie : MonoBehaviour {

    public GameObject explosionPrefab;
    //public GameObject zombiePrefab;
    //GameObject playerCamera;
    public int point=500;


    private void Start()
    {
        //playerCamera = GameObject.Find("OVRCameraRig");
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            /*
            GameObject exPrefab = Instantiate(explosionPrefab);
            exPrefab.transform.position = transform.position+new Vector3(0,1.8f,0);
            exPrefab.transform.LookAt(playerCamera.transform.position);
            exPrefab.transform.GetComponentInChildren<Text>().text = point + "pt";
            */

            Instantiate(explosionPrefab, transform.position + new Vector3(0, 1.8f, 0), Quaternion.identity);

            GameManager.totalPoint += point;

            Debug.Log("hit");
        }

    }
}
