using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {
    AudioSource shootTargetSE;

    public GameObject[] targetPrefabs;

    public float shotSpeed;
    public float shotTorque;

    public float amplitude;
    public float torqueSpeed;

    public bool start=false;

    Vector3 startRot;

    bool isRunning;


    private void Start()
    {
        shootTargetSE = GetComponent<AudioSource>();
        startRot = transform.rotation.eulerAngles;
        
    }
   
    public void ShotStart()
    {
        StartCoroutine(Shot());
    }

    private void Update()
    {
        float y = amplitude * Mathf.Sin(Time.time * torqueSpeed);
        Vector3 cahgeRotation = startRot + new Vector3(0, y, 0);
        transform.rotation = Quaternion.Euler(cahgeRotation);
    }

    GameObject SampleTarget()
    {
        GameObject prefab = null;


        int index = Random.Range(0, targetPrefabs.Length);
        prefab = targetPrefabs[index];

        return prefab;
    }



    IEnumerator Shot()
    {
        while (start)
        {
            if (isRunning)
            {
                yield break;
            }
            isRunning = true;
            yield return new WaitForSeconds(Random.Range(10, 15));

            if (start)
            {
                shootTargetSE.Play();
                GameObject target = Instantiate(SampleTarget(), transform.position, Quaternion.identity);

                Rigidbody targetRigidBody = target.GetComponent<Rigidbody>();
                targetRigidBody.AddForce(transform.forward * shotSpeed);
                targetRigidBody.AddTorque(new Vector3(0, shotTorque, 0));
            }
            isRunning = false;
        }
        Debug.Log("croutinFinish");
    }
}
