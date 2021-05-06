using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject credit;
    // Start is called before the first frame update
    void Start()
    {
        credit.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    public void GameStart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Credit()
    {
        credit.gameObject.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void Cancle()
    {
        credit.gameObject.SetActive(false);
    }
}
