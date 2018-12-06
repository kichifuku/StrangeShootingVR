using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour {

    public Transform player;

    public float tranceDist = 10.0f;
    public float attackDist = 1.0f;
    NavMeshAgent nav;

    Animator animator;
    public AudioSource audioSource;
    public AudioClip attackVoice;
    public AudioSource dethAudio;

    bool once = true;

    Coroutine sc;

    CapsuleCollider capColl;

	
	void Start () {
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        sc = StartCoroutine(CheckDist());

        player = GameObject.FindWithTag("Player").transform;

        capColl = GetComponent<CapsuleCollider>();
        capColl.enabled = true;

    }
	
	IEnumerator CheckDist()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);

            float dist = Vector3.Distance(player.position, transform.position);
            if (dist < tranceDist)
            {
                nav.SetDestination(player.position);
                nav.isStopped = false;

                animator.SetBool("attack", false);
                animator.SetBool("walk", true);

                if (dist < attackDist)
                {
                    nav.isStopped = true;
                    animator.SetBool("attack", true);

                    audioSource.clip = attackVoice;
                    if (once)
                    {
                        audioSource.Play();
                        once = false;
                    }
                
                }
            }
            else
            {
                nav.isStopped = true;
                animator.SetBool("walk", false);
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "bullet")
        {
            deth();
        }
    }

    public void deth()
    {
            capColl.enabled = false;
            nav.isStopped = true;
            StopCoroutine(sc);
            animator.SetTrigger("damage");
            Destroy(gameObject, 3.0f);
        audioSource.Stop();
        dethAudio.Play();
    }
}
