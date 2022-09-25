using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    private bool isChanging = false;
    [SerializeField] private Camera camSciFi;
    [SerializeField] private Camera camPlayer;

    // Start is called before the first frame update
    void Start()
    {
        camSciFi.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isChanging)
        {
            ChangeToSciFi();
        }
        else if (Input.GetKeyDown(KeyCode.E) && isChanging)
        {
            ChangeToNormal();
        }
    }

    public void ChangeToSciFi()
    {
        print("changeScifi");
        isChanging = true;
        camSciFi.gameObject.SetActive(true);
        camPlayer.gameObject.SetActive(false); 

    }

    public void ChangeToNormal()
    {
        print("changeNormal");

        isChanging = false;
        camSciFi.gameObject.SetActive(false);
        camPlayer.gameObject.SetActive(true) ; 


    }
}
