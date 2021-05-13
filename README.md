# Unity-VR_EscapeGame

## Title : Disorder

![disorder](https://user-images.githubusercontent.com/46181173/117296482-12557680-aeb0-11eb-887d-08fc37fdd7d0.jpg)
장르 : 공포 / 방탈출 <br>
개발 : 1인개발 <br>
플랫폼 : 모바일 VR <br>
유니티 버젼 : 2019.3.5f1 <br>
제작기간 : 5일

## Player Controller

필드

```C#
    public float speed; //스피드
    public float jump; //점프
    public float gravity; //중력

    private CharacterController controller; // 컨트롤러 콜라이더
    private Vector3 MoveDirection; // 움직이는 방향

```

업데이트 함수

```C#
        //땅에 있는지
        if (controller.isGrounded)
        {
            // 상하
            MoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            //로컬좌표계 > 월드좌표계
            MoveDirection = transform.TransformDirection(MoveDirection);

            //속도증가
            MoveDirection *= speed;

            //점프
            if (Input.GetButton("Jump"))
                MoveDirection.y = jump;
        }

        //중력계산
        MoveDirection.y -= gravity * Time.deltaTime;

        //캐릭터 이동
        controller.Move(MoveDirection * Time.deltaTime);

```

## Menu Script

```C#
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 1000;

        if (Physics.Raycast(transform.position, forward, out hit))
        {
            if (Input.GetMouseButtonDown(0) && (hit.collider.tag == "Button"))
            {
                Debug.Log(hit.collider.tag);
                hit.transform.GetComponent<Button>().onClick.Invoke();
            }
        }
```
