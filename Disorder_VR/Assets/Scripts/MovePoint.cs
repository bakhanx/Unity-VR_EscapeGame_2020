using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePoint : MonoBehaviour
{
    public CharacterController player;
    public GameObject obj;
    private float speed = 10.0f;
    public Vector3 targetDir;
    public GameObject ob;

    void Start()
    {
        player.GetComponent<CharacterController>();
        targetDir = obj.transform.position - ob.transform.position;
        //target.y = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        //transform.position = Vector3.MoveTowards(transform.position, target , speed * Time.deltaTime);
        //if (Input.GetMouseButtonDown(0))
        //{
            player.Move(targetDir * Time.deltaTime);
       // }

    }

    
}
