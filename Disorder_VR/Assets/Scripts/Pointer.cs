using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pointer : MonoBehaviour
{ 
    private Vector3 ScreenCenter;
    private float turnspeed = 2.0f;
    public GameObject[] door;

    //movePoint
    public CharacterController player;
    public GameObject movePoint;
    private Vector3 targetDir;
    private Vector3 temp;
    private float speed = 10.0f;
    float distance = 0f;
    private GameObject nextMovePoint; // 문 뒤 다음 포인트

    //Boolean
    private bool isOpenDoor = false;
    private bool frontbool = true;
    private bool useSound = true;
    private bool useSound2 = true;

    //Body
    public GameObject hand;
    public GameObject grab;

    //Obejct
    public GameObject jack;
    public GameObject img;

    //Background
    public Material redsky;
    public GameObject dirLight;
    public GameObject frontLight;

    //Text
    public Text textUI;

    //AudioSource
    public AudioSource audioSource;
    public AudioClip[] bgm = new AudioClip[3];
    public AudioClip[] soundEffect = new AudioClip[5];

    //Delay
    private bool delay = true;

    //FindGameObject
    private GameObject move_point26;
    private GameObject move_point22;
    private GameObject orgel;

    //열쇠 소지 여부
    //0 : 열쇠x , 1: 열쇠o, 2:문열기
    private int[] flag = new int[] { 0, 0, 0, 0, 0, 0 };


    void Start()
    {
        ScreenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);

        player.GetComponent<CharacterController>();

        audioSource = GetComponent<AudioSource>();

        SoundBgm0();

        move_point26 = GameObject.Find("Move_point (26)");
        move_point22 = GameObject.Find("Move_point (22)");
        orgel = GameObject.Find("Orgel");
        



        //audioSource.clip = soundEffect[0];

        //Vector3.magnitude(targetDir);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(ScreenCenter);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, 100.0f))
        {

            if (Input.GetMouseButtonDown(0))
            {
                //hit가 movePoint이고 이동가능 포인트일 경우
                if (hit.collider.tag == "movePoint")
                {
                    movePoint = hit.collider.gameObject;
                    Debug.Log(hit.collider.gameObject.name);
                    Debug.Log(distance);

                }

                if (hit.collider.gameObject.name == "Flashlight")
                {
                    hit.collider.transform.SetParent(hand.transform);
                    hit.collider.transform.localPosition = new Vector3(0.5f, -0.2f, 0.2f);
                    hit.collider.transform.localRotation = Quaternion.Euler(0f, -95f, 0f);
                    SoundGet();
                }

            }

            if (Input.GetMouseButtonDown(0))
            {

                //가짜키 이벤트
                if (hit.collider.tag == "Fake_key")
                {
                    Destroy(hit.transform.gameObject);
                    textUI.text = "You got a Fake key";
                    StartCoroutine(Surprise());
                }



                //현관열쇠
                if (hit.collider.gameObject.name == "Key_Enter")
                {
                    Destroy(hit.transform.gameObject);
                    flag[0] = 1;
                    textUI.text = "You got a front door key";
                    SoundGetKey();
                }
                //현관열기
                if (flag[0] == 1 && hit.collider.gameObject.name == "DeadboltOut_1")
                {
                    door[0].GetComponent<Rigidbody>().isKinematic = false;
                    movePoint = GameObject.Find("Move_point (5)");
                    flag[0] = 2;
                    isOpenDoor = true;
                    SoundOpenDoorKey();
                }

                //Room01 열쇠
                if (hit.collider.gameObject.name == "Key_Room01")
                {
                    flag[0] = 1; //현관문
                    Destroy(hit.transform.gameObject);
                    flag[1] = 1;
                    textUI.text = "You got a Room_A door key";
                    SoundGetKey();

                    SoundBgm2();
                }

                if (flag[1] == 1 && hit.collider.gameObject.name == "Doorknob_LOD0_Room01")
                {
                    door[1].GetComponent<Rigidbody>().isKinematic = false;

                    movePoint = GameObject.Find("Move_point (13)");
                    flag[1] = 2;
                    isOpenDoor = true;
                    SoundOpenDoor();
                }

                //Room02 열쇠
                if (hit.collider.gameObject.name == "Key_Room02")
                {
                    Destroy(hit.transform.gameObject);
                    flag[2] = 1;
                    textUI.text = "You got a Room_B door key";
                    SoundGetKey();
                }

                if (flag[2] == 1 && hit.collider.gameObject.name == "Doorknob_LOD0_Room02")
                {
                    door[2].GetComponent<Rigidbody>().isKinematic = false;
                    movePoint = GameObject.Find("Move_point (17)");
                    flag[2] = 2;
                    isOpenDoor = true;
                    SoundOpenDoor();
                }

                //Room03 열쇠
                if (hit.collider.gameObject.name == "Key_Room03")
                {
                    Destroy(hit.transform.gameObject);
                    flag[3] = 1;
                    textUI.text = "You got a Room_C door key";
                    SoundGetKey();
                }

                if (flag[3] == 1 && hit.collider.gameObject.name == "Doorknob_LOD0_Room03")
                {
                    door[3].GetComponent<Rigidbody>().isKinematic = false;
                    movePoint = GameObject.Find("Move_point (19)");
                    flag[3] = 2;
                    isOpenDoor = true;
                    SoundOpenDoor();
                }

                //Room04 열쇠
                if (hit.collider.gameObject.name == "Key_Room04")
                {
                    Destroy(hit.transform.gameObject);
                    flag[4] = 1;
                    textUI.text = "Go down to the first floor";
                    SoundGetKey();
                    SoundGlass();
                }

                if (flag[4] == 1 && hit.collider.gameObject.name == "Doorknob_LOD0_Room04")
                {
                    door[4].GetComponent<Rigidbody>().isKinematic = false;
                    movePoint = GameObject.Find("Move_point (27)");
                    flag[4] = 2;
                    isOpenDoor = true;
                    audioSource.Stop();
                    SoundOpenDoor();
                   
                }

                //Car 열쇠
                if (hit.collider.gameObject.name == "Key_Car")
                {
                    Destroy(hit.transform.gameObject);
                    flag[5] = 1;
                    textUI.text = "Get in the car";
                    SoundGetKey();
                }

                if (flag[5] == 1 && hit.collider.gameObject.name == "Door_Car")
                {
                    textUI.text = "This car... Are you crazy?!";
                    img.GetComponent<FadeinOut>().StartFadeIn();
                    img.GetComponent<FadeinOut>().fadeTime = 5.0f;
                    SoundEnd();
                    Invoke("SceneChange", 10.0f);
                }


                //일정속도 포인트 이동변수
                temp = movePoint.transform.position - player.transform.position;

                //특정 이벤트
                //2층올라갈때
                if (movePoint == move_point26)
                {

                    if (useSound)
                    {
                        audioSource.Stop();
                        SoundHorror01();
                    }
                }
                //Room03 오르골사운드
                if (movePoint == move_point22)
                {
                    if (useSound2)
                    {
                        orgel.GetComponent<AudioSource>().Play();
                        useSound2 = false;
                    }
                }



            }

            //문열기
            if (isOpenDoor)
            {
                if (flag[0] == 2)
                {
                    door[0].transform.rotation = Quaternion.Slerp(door[0].transform.rotation, Quaternion.Euler(0, -90f, 0), Time.deltaTime);

                }
                else if (flag[1] == 2)
                {
                    door[1].transform.rotation = Quaternion.Slerp(door[1].transform.rotation, Quaternion.Euler(0, 90.0f, 0), Time.deltaTime);
                }
                else if (flag[2] == 2)
                {
                    door[2].transform.rotation = Quaternion.Slerp(door[2].transform.rotation, Quaternion.Euler(0, -90.0f, 0), Time.deltaTime);
                }
                else if (flag[3] == 2)
                {
                    door[3].transform.rotation = Quaternion.Slerp(door[3].transform.rotation, Quaternion.Euler(0, 90.0f, 0), Time.deltaTime);
                }
                else if (flag[4] == 2)
                {
                    door[4].transform.rotation = Quaternion.Slerp(door[4].transform.rotation, Quaternion.Euler(0, 90.0f, 0), Time.deltaTime);
                }

                StartCoroutine(WaitForMoving());
                StartCoroutine(WaitForClose());
                //2초 뒤 문 다음 point까지 이동 (Moving)
                //5초 뒤 문닫기 (Close)


            }

            else
            {
                Moving();
            }

            if (textUI.text != "")
            {
                Invoke("TextNull", 2.0f);
            }

        }

    }

    IEnumerator WaitForMoving()
    {
        yield return new WaitForSeconds(2.0f);
        Moving();
    }

    IEnumerator WaitForClose()
    {
        yield return new WaitForSeconds(5.0f);
        Close();
    }

    //포인트로 이동


    void Moving()
    {

        targetDir = movePoint.transform.position - player.transform.position;

        distance = targetDir.magnitude;

        if (distance > 1.85)
        {
            player.Move(temp * Time.deltaTime * 0.5f);

        }
    }

    void Close()
    {
        isOpenDoor = false;
        for (int i = 1; i < 6; i++)
        {
            if (flag[i] == 2)
            {
                flag[i] = 1;
                Debug.Log(i);
                Debug.Log(flag[i]);
            }
        }
        //현관문 잠금 //환경변화
        if (flag[0] == 2)
        {
            door[0].transform.rotation = Quaternion.Slerp(door[0].transform.rotation, Quaternion.Euler(0, 180.0f, 0), Time.deltaTime * 10);
            if (frontbool)
            {
                door[0].GetComponent<Rigidbody>().isKinematic = true;
                RenderSettings.skybox = redsky;
                dirLight.SetActive(false);
                frontLight.SetActive(false);
                textUI.text = "Find a flash_light on 1st floor";

                SoundCloseDoor();
                SoundBgm1();

                Invoke("SoundThunder", 1.0f);

                frontbool = false;
            }
        }
    }

    IEnumerator Surprise()
    {
        jack.SetActive(true);
        SoundFake();
        jack.transform.SetParent(grab.transform);
        jack.transform.localPosition = new Vector3(0.5f, -0.2f, 0.2f);
        jack.transform.localRotation = Quaternion.Euler(0f, -95f, 0f);

        yield return new WaitForSeconds(2.0f);
        textUI.text = "";
        jack.SetActive(false);
    }

    //텍스트 비우기
    void TextNull()
    {
        textUI.text = "";
    }

    //씬전환
    void SceneChange()
    {
        SceneManager.LoadScene("Menu");
    }

    //BGM
    void SoundBgm0()
    {
        audioSource.clip = bgm[0];
        audioSource.loop = true;
        audioSource.Play();
    }

    void SoundBgm1()
    {
        audioSource.clip = bgm[1];
        audioSource.loop = true;
        audioSource.Play();
    }

    void SoundBgm2()
    {
        audioSource.clip = bgm[2];
        audioSource.loop = true;
        audioSource.Play();
    }


    //Effect
    void SoundWalk()
    {
        audioSource.PlayOneShot(soundEffect[1]);
    }

    void SoundOpenDoor()
    {
        audioSource.PlayOneShot(soundEffect[2]);
    }

    void SoundCloseDoor()
    {
        audioSource.PlayOneShot(soundEffect[3]);
    }

    void SoundOpenDoorKey()
    {
        audioSource.PlayOneShot(soundEffect[4]);
    }

    void SoundThunder()
    {
        audioSource.PlayOneShot(soundEffect[5]);
    }

    void SoundGetKey()
    {
        audioSource.PlayOneShot(soundEffect[6]);
    }

    void SoundGet()
    {
        audioSource.PlayOneShot(soundEffect[7]);
    }

    void SoundHorror01()
    {
        audioSource.PlayOneShot(soundEffect[8]);
        useSound = false;
    }

    void SoundFake()
    {
        audioSource.PlayOneShot(soundEffect[9]);
    }

    void SoundGlass()
    {
        audioSource.PlayOneShot(soundEffect[10]);
    }

    void SoundEnd()
    {
        audioSource.PlayOneShot(soundEffect[11]);
    }
}
