using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyEnemy : MonoBehaviour {

    public GameObject explosionPrefab;
    public GameObject cubePrefab;
    GameObject playerCamera;
    public int point;
    int posPointerCount;

    GameObject posPointer;


    private void Start()
    {
        playerCamera = GameObject.Find("OVRCameraRig");
        if (gameObject.tag == "goblin") point = 150;
        if (gameObject.tag == "orc") point = 100;

        posPointerCount = GameObject.Find("PositionPointers").transform.childCount;
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            

            int index = Random.Range(0, posPointerCount);
            posPointer = GameObject.Find("PositionPointer" + index);

            Debug.Log(index);
            while (posPointer.transform.childCount > 0)
            {
                index = Random.Range(0, posPointerCount);
                posPointer = GameObject.Find("PositionPointer" + index);
                Debug.Log(index);
            }
            

            GameObject exPrefab = Instantiate(explosionPrefab);
            exPrefab.transform.position = transform.position;
            exPrefab.transform.LookAt(playerCamera.transform.position);
            exPrefab.transform.GetComponentInChildren<Text>().text = point + "pt";

            GameManager.totalPoint += point;

            GameObject cubeNewPrefab =Instantiate(cubePrefab, posPointer.transform.position, Quaternion.identity);
            cubeNewPrefab.transform.parent = posPointer.transform;
            cubeNewPrefab.transform.forward = posPointer.transform.forward;
            
            Debug.Log("hit");
            Destroy(gameObject);
        }
        
    }
}
