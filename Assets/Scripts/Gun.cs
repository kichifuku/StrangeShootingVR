using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    private AudioSource shotSound;

    [SerializeField]
    private Transform _RightHandAnchor;

    [SerializeField]
    private Transform _LeftHandAnchor;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float bulletPower = 500f;

    private void Start()
    {
        shotSound = GetComponent<AudioSource>();
    }

    private Transform Pointer
    {
        get
        {
            var controller = OVRInput.GetActiveController();

            if(controller == OVRInput.Controller.RTrackedRemote)
            {
                return _RightHandAnchor;
            }else if(controller == OVRInput.Controller.LTrackedRemote)
            {
                return _LeftHandAnchor;
            }
            return null;
        }
    }

    private void Shot(Transform pointer)
    {
        shotSound.Play();
        var bulletInstance = GameObject.Instantiate(bulletPrefab, pointer.position, pointer.rotation) as GameObject;
        bulletInstance.GetComponent<Rigidbody>().AddForce(bulletInstance.transform.forward * bulletPower);
    }


    void Update() {
        var pointer = Pointer;
        if (pointer == null)
        {
            return;
        }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            Shot(pointer);
        }

        
	}
}
