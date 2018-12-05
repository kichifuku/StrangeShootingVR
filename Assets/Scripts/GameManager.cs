using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Shooter[] shooters;
    public GameObject[] positionPointers;
    public GameObject[] zPosPointers;

    public static int totalPoint;
    int bestScore = 0;
    public float setTimeLmit = 60;
    float timeLimit;
    int minute;
    int secound;
    public Text score;
    public AudioSource whistle;
    bool once = false;
    public GameObject redLamp;

    public AudioSource finshSound2;
    public AudioSource moveSE;
    

    public GameObject restartBillboard;

    public AudioSource bgm1;
    public AudioSource bgm2;

    public GameObject zombiePrefab;

    public float setSecondTimeLimit = 120;
    float secondTimeLimit;
    bool once2 = false;

    public GameObject zombies;

    public Animator stageAnimator;

    public GameObject waterShowers;

    public GameObject dust;

    public GameObject[] zombiePrefabs;

    
    private void Awake()
    {
        // 【Unite Tokyo 2018】Oculusで作るスタンドアローン・モバイルVRコンテンツ
        // https://www.slideshare.net/UnityTechnologiesJapan002/unite-tokyo-2018oculusvr-96453609/UnityTechnologiesJapan002/unite-tokyo-2018oculusvr-96453609
        // 上記スライドを参考にした設定

        // アイバッファ解像度 scale 1.0 == 1024x1024
        UnityEngine.XR.XRSettings.eyeTextureResolutionScale = 1.25f;

        // 外枠側のレンダリング設定。>>HIGH　外側の解像度が減っていく
        //OVRManager.tiledMultiResLevel = OVRManager.TiledMultiResLevel.LMSLow;

        // 72Hzモード（フレームレートは上がるが綺麗）
        OVRManager.display.displayFrequency = 72.0f;
    }
    


    void Start()
    {
        //whistle = GetComponent<AudioSource>();
    }



    void Update()
    {
        if (timeLimit > 0)
        {
            timeLimit -= Time.deltaTime;
        }
        else if (once)
        {
            whistle.Play();
            GameFinish();
            once = false;
        }
        minute = (int)timeLimit / 60;
        secound = (int)timeLimit - 60 * minute;


        score.text = $"{minute:D2}:{secound:D2}\n{totalPoint}pt";

        if (secondTimeLimit > 0)
        {
            secondTimeLimit -= Time.deltaTime;
        }else if (once2)
        {
            //whistle.Play();
            finshSound2.Play();
            secondGameFinish();
            once2 = false;
        }
    }

    public void GameStart()
    {
        Debug.Log("satart");
        foreach (Shooter s in shooters)
        {
            s.enabled = true;
            s.start = true;
            s.ShotStart();

            timeLimit = setTimeLmit;
            once = true;
            totalPoint = 0;
        }

        foreach (GameObject posPoint in positionPointers)
        {
            posPoint.SetActive(true);
        }
    }

    public void GameFinish()
    {
        Debug.Log("finish");
        foreach (Shooter s in shooters)
        {
            s.enabled = false;
            s.start = false;
        }

        foreach (GameObject posPoint in positionPointers)
        {
            posPoint.SetActive(false);
        }

        Invoke("SecondStageStart", 2.0f);

    }

    void makeRep()
    {
        if (totalPoint > bestScore)
        {
            bestScore = totalPoint;
        }

        GameObject rePre = Instantiate(restartBillboard, new Vector3(0, -0.5f, 6), Quaternion.Euler(0, 0, 90));
        rePre.GetComponentInChildren<Text>().text = $"Total Score  {totalPoint}pt\nBest Score  {bestScore}pt\nこの看板を撃って\nリスタート";
    }

    void SecondStageStart()
    {
        
        redLamp.gameObject.SetActive(true);

        stageAnimator.SetBool("move", true);
        moveSE.Play();


        Invoke("StopRedLamp", 7.0f);

        foreach (GameObject zPosPointer in zPosPointers)
        {
            ZPosPointer zpp = zPosPointer.GetComponent<ZPosPointer>();

            zpp.enabled = true;
            zpp.start = true;
            zpp.GeneZombieStart();

            
            secondTimeLimit = setSecondTimeLimit;
            once2 = true;
            
        }

        Invoke("GenerateZombies", 7.0f);

        bgm1.Stop();
        bgm2.Play();

        dust.SetActive(true);
    }

    void secondGameFinish()
    {
        
        foreach (GameObject zPosPointer in zPosPointers)
        {
            ZPosPointer zpp = zPosPointer.GetComponent<ZPosPointer>();

            zpp.enabled = false;
            zpp.start = false;

        }

        
        waterShowers.SetActive(true);

        Invoke("AllZombiesDeth", 0.5f);

        

        Invoke("Reposit", 4f);

        dust.SetActive(false);
        //Invoke("makeRep",11f);

    }


    void StopRedLamp()
    {
        redLamp.SetActive(false);
    }

    void GenerateZombies()
    {
        /*
        foreach (GameObject zp in zPosPointers)
        {
            GameObject zP = Instantiate(zombiePrefab, zp.transform.position, Quaternion.identity);
            zP.transform.parent = zombies.transform;
        }
        */

        for(int i= 0; i< zPosPointers.Length; i++)
        {
            GameObject zPre = Instantiate(zombiePrefabs[i % zombiePrefabs.Length], zPosPointers[i].transform.position, Quaternion.identity);
            zPre.transform.parent = zombies.transform;
        }
    }

    void Reposit()
    {
        waterShowers.SetActive(false);
        stageAnimator.SetBool("move", false);
        moveSE.Play();
        bgm1.Play();
        bgm2.Stop();
        Invoke("makeRep", 8f);
    }

    void AllZombiesDeth()
    {
        foreach (Transform child in zombies.transform)
        {
            child.GetComponent<ZombieController>().deth();
        }
    }
}
