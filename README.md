# Unity-VR_EscapeGame

## Title : Disorder

![disorder](https://user-images.githubusercontent.com/46181173/117296482-12557680-aeb0-11eb-887d-08fc37fdd7d0.jpg)
장르 : 공포 / 방탈출 <br>
개발 : 1인개발 <br>
플랫폼 : 모바일 VR <br>
유니티 버젼 : 2019.3.5f1 <br>
제작기간 : 5일

스토리 : 집 안에 갇혀, 열쇠를 찾아 집을 탈출한다.

## Menu Script

![menu](https://user-images.githubusercontent.com/46181173/118109733-877afb80-b41c-11eb-81e6-309f4082a023.png)

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

![movepoint](https://user-images.githubusercontent.com/46181173/118109559-53073f80-b41c-11eb-86f6-b20f594d8ad9.png)

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

<img src= "https://user-images.githubusercontent.com/46181173/118110075-ee001980-b41c-11eb-9f7f-915c5eb40286.png" width=45% height=45% >
<img src= "https://user-images.githubusercontent.com/46181173/118111249-67e4d280-b41e-11eb-8b8c-c4eb01557bcb.png" width=45% height=45% >

<img src= "https://user-images.githubusercontent.com/46181173/118111194-53083f00-b41e-11eb-8c51-712f0df09749.png" width=45% height=45% >

<img src= "https://user-images.githubusercontent.com/46181173/118110826-da08e780-b41d-11eb-8f6b-87aeac3246ed.png" width=45% height=45% >

<img src= "https://user-images.githubusercontent.com/46181173/118111296-7632ee80-b41e-11eb-9701-5952ad0f868a.png" width=45% height=45% >

<img src= "https://user-images.githubusercontent.com/46181173/118110916-f6a51f80-b41d-11eb-9b69-6ac900ac6176.png" width=45% height=45% >

<img src= "https://user-images.githubusercontent.com/46181173/118110971-07ee2c00-b41e-11eb-96cc-c6b3cb72e9a3.png" width=45% height=45% >
<img src= "https://user-images.githubusercontent.com/46181173/118110993-11779400-b41e-11eb-8a47-43a85ec38ecf.png" width=45% height=45% >

<img src= "https://user-images.githubusercontent.com/46181173/118111051-22c0a080-b41e-11eb-9942-83e62acff735.png" width=45% height=45% >

<img src= "https://user-images.githubusercontent.com/46181173/118111356-8945be80-b41e-11eb-8587-a287a5e14507.png" width=45% height=45% >
<br><br>

[![youtube](https://user-images.githubusercontent.com/46181173/118114156-2fdf8e80-b422-11eb-87d5-823b2c7b0c71.png)](https://youtu.be/HjjVIltVnp4?t=0s)

↑ 클릭 [Youtube 이동하기]

## ~~Player Controller Script - PC버젼 컨트롤러===~~

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
