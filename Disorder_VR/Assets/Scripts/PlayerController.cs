using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed; //스피드
    public float jump; //점프
    public float gravity; //중력

    private CharacterController controller; // 컨트롤러 콜라이더
    private Vector3 MoveDirection; // 움직이는 방향
    // Start is called before the first frame update
    void Start()
    {
        speed = 5.0f;
        jump = 7.0f;
        gravity = 15.0f;

        MoveDirection = Vector3.zero;
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
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

        //중력
        MoveDirection.y -= gravity * Time.deltaTime;

        //캐릭터 이동
        controller.Move(MoveDirection * Time.deltaTime);
    }
}
