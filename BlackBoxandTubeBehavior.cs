using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoxandTubeBehavior : MonoBehaviour {  //создание переменных
    GameObject [] Lines ;
    GameObject [] Sticks;
    int n;
    Transform Player, TubeCone, PinkBoxOut;
    Vector3 PlayerPosition, BlackOut, InTube, OutTube, PinkBoxIn, BlackIn, PinkTeleport, TeleportCubeStart;
    GameObject TeleportCube, PinkBox;
    AudioSource BoxAS;

    //переменные кот можно менять в игре на скрипте (делаем их паблик для этого)
    public AudioClip Tube;
    public AudioClip BlackBox;
    public AudioClip PinkBoxSound;   
    public float TubeRotationSpeed;   
    public float LinesScaleSpeed;
    public float TubeSticksSpeed;

    void Start () {   //заполнение переменных
        Lines = GameObject.FindGameObjectsWithTag("LN");
        Sticks = GameObject.FindGameObjectsWithTag("ST");
        PinkBox = GameObject.FindGameObjectWithTag("PinkBox");
        Player = GameObject.FindGameObjectWithTag("PL").GetComponent<Transform>();
        TubeCone = GameObject.FindGameObjectWithTag("CN").GetComponent<Transform>();
        InTube = GameObject.FindGameObjectWithTag("It").GetComponent<Transform>().position;
        OutTube = GameObject.FindGameObjectWithTag("OT").GetComponent<Transform>().position;
        BlackOut = GameObject.FindGameObjectWithTag("TP").GetComponent<Transform>().position;
        PinkBoxIn = GameObject.FindGameObjectWithTag("JBI").GetComponent<Transform>().position;
        PinkBoxOut = GameObject.FindGameObjectWithTag("JBO").GetComponent<Transform>();
        BlackIn = GameObject.FindGameObjectWithTag("BI").GetComponent<Transform>().position;
        TeleportCube = GameObject.FindGameObjectWithTag("CWH");
        TeleportCubeStart = TeleportCube.transform.position;
        PinkBox.SetActive(false);
        BoxAS = GameObject.FindGameObjectWithTag("SB").GetComponent<AudioSource>();
        Music(1);
        n = 0;
        
    }

    void Update() {
        PlayerPosition = Player.position;  //на каждом апдейте проверяем координаты игрока
        switch (n) {

            case 0:

             TubeCone.Rotate(0, TubeRotationSpeed, 0);   //вращение кона

        for (int i = 0; i < Lines.Length; i++)
        {
            Lines[i].transform.localScale += new Vector3(-LinesScaleSpeed, -LinesScaleSpeed, -LinesScaleSpeed);    //изменение размера линий в кубе
        }

        for(int i = 0; i < Sticks.Length; i++)
                {
                    Sticks[i].transform.Rotate(0, -TubeSticksSpeed, 0);   //вращение палок в тюбе
                }

        if (Vector3.Distance(PlayerPosition, BlackOut) < 2f)  //если близко подошёл к телепорту в чёрном кубе(сравнение вектора3 игрока и телепорт(пустой обьект с партикалами))
        {            
            Teleportation(1);  //отправляет на телепортацию со значением 1 (на кейс 1) (см. телепортацию далее)
                }

        if (Vector3.Distance(PlayerPosition, OutTube) < 10f)  //если близко упал к телепорту после тюба(сравнение вектора3 игрока и телепорт(пустой обьект с партикалами))
                {
            Teleportation(2); //отправляет на телепортацию со значением 2 (на кейс 2) (см. телепортацию далее)
                }

                break;

            case 1:
                PinkTeleport = PinkBoxOut.position;  //на каждом апдейте получаем координаты телепорта из розового куба

                if (Vector3.Distance(PinkTeleport, PinkBoxIn) < 10f) //сравниваем их с координатами входной точки в розовый куб
                {
                    TeleportCube.GetComponent<Move>().enabled=false; //если близко останавливаем куб
                }
                
                if (Vector3.Distance(PlayerPosition, PinkTeleport) < 3.5f) // если близко подошёл к телепорту в розовом кубе(сравнение вектора3 игрока и телепорт(пустой обьект с партикалами))
                {
                    Teleportation(3); //отправляет на телепортацию со значением 3 (на кейс 3) (см. телепортацию далее)
                }
                break;
    }
		
	}

    void Teleportation(int k)
    {
        switch (k){

            case 1:     //меняет координаты игрока на координаты входной точки в тюб (телепортирует)
                Player.position = InTube; 
                Music(2);
                break;

            case 2:     //меняет координаты игрока на координаты входной точки в розовый куб (телепортирует)
                Player.position = PinkBoxIn;
                n = 1;
                PinkBox.SetActive(true);  //включает розовый куб
                ResetGame();  //отправляет на сброс роста линий (см.далее)
                Music(3);  //отправляет на музыку кейс 3 (см. далее)
                break;

            case 3:   //меняет координаты игрока на координаты входной точки в чёрный куб (телепортирует)
                Player.position = BlackIn;
                n = 0;
                TeleportCube.transform.position = TeleportCubeStart;
                TeleportCube.GetComponent<Move>().enabled = true;
                PinkBox.SetActive(false);   //выключает розовый куб (делает невидимым)
                Music(1);  //отправляет на музыку кейс 1 (см. далее)

                break;
        }
             
    }

    void ResetGame()   //остановка роста линий и возвращение их обратно к начальному размеру
    {
        for(int i = 0; i < Lines.Length; i++)
        {
            Lines[i].transform.localScale=new Vector3(100f,100f,100f);
        }
    }

    void Music(int m)  //смена музыки
    {
        switch (m)
        {

            case 1:
                BoxAS.Stop();
                BoxAS.PlayOneShot(BlackBox);
                break;

            case 2:
                BoxAS.Stop();
                BoxAS.PlayOneShot(Tube);
                break;

            case 3:
                BoxAS.Stop();
                BoxAS.PlayOneShot(PinkBoxSound);
                break;
        }
    }
}
