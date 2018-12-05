using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour {

    Vector3 startPosition;

    public float amplitude;
    public float moveSpeed;
    float time;

    private void Start()
    {
        startPosition = transform.localPosition;
        time = Random.Range(0, 12);
    }

    void Update()
    {
        time += Time.deltaTime;

        float x = amplitude * Mathf.Sin(time * moveSpeed);
        transform.localPosition = startPosition + new Vector3(x,0,0);


    }
}
