# Unity-VR_EscapeGame

## Title : Disorder

![disorder](https://user-images.githubusercontent.com/46181173/117296482-12557680-aeb0-11eb-887d-08fc37fdd7d0.jpg)
장르 : 공포 / 방탈출 <br>
개발 : 1인개발 <br>
플랫폼 : 모바일 VR <br>
유니티 버젼 : 2019.3.5f1 <br>
제작기간 : 5일

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

Raycast를 통하여 해당 메뉴 항목에 관련된 함수를 호출한다. <br><br>

## MovePoint Script

```C#
        player.GetComponent<CharacterController>();
        targetDir = obj.transform.position - ob.transform.position;
```

현재 위치와 다음 위치 사이의 거리를 계산

```C#
        player.Move(targetDir * Time.deltaTime);
```

## Pointer Script

```C#
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

```

hit tag가 이동가능 포인트일 경우 해당 위치를 이동할 타겟으로 설정한다.<br><br>

## ~~Player Controller Script - PC버젼 구현중~~

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
