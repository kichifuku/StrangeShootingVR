using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour {

    public AudioSource shotSE;
    public GameObject bulletPrefab;
    public float bulletPower = 200;

    private void Start()
    {
        shotSE = GetComponent<AudioSource>();
    }


    void Update () {
        float y = Input.GetAxis("Horizontal");
        float x = Input.GetAxis("Vertical");

        transform.Rotate(x * 0.3f, 0, 0, Space.Self);
        transform.Rotate(0, y*0.3f, 0,Space.World);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShotDebug();
        }
    }

    private void ShotDebug()
    {

        shotSE.Play();
        var bulletInstance = GameObject.Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
        bulletInstance.GetComponent<Rigidbody>().AddForce(transform.forward * bulletPower);
        Destroy(bulletInstance, 5f);
    }
}
