using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZPosPointer : MonoBehaviour {

    public GameObject[] zombiePrefabs;

    public bool start = false;

    bool isRunning;

    GameObject zombies;

    private void Start()
    {
        zombies = GameObject.Find("Zombies");
    }

    public void GeneZombieStart()
    {
        StartCoroutine(GeneZombie());
    }


    
    GameObject SampleZombie()
    {
        GameObject prefab = null;

        
        int index = Random.Range(0, zombiePrefabs.Length);
        prefab = zombiePrefabs[index];

        return prefab;
    }
    



    IEnumerator GeneZombie()
    {
        while (start)
        {
            if (isRunning)
            {
                yield break;
            }
            isRunning = true;
            yield return new WaitForSeconds(Random.Range(10, 20));

            if (start)
            {
                GameObject zP = Instantiate(SampleZombie(), transform.position, Quaternion.identity);
                zP.transform.parent = zombies.transform;
            }
            isRunning = false;
        }
        Debug.Log("croutinFinish");
    }


    private void OnDrawGizmos()
    {
        Vector3 offset = new Vector3(0, 0.5f, 0);

        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position + offset, 0.5f);
    }
}
