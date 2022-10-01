using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private Vector3 start; 
    private Vector3 end;
    private bool dash = false;
    private bool canDash = true;
    private bool waitingDash = false;

    [SerializeField] private float timeWait = 0.1f;

    [SerializeField] private float duration = 1f;
    private float elipseTime;

    [SerializeField] private GameObject post;
    [SerializeField] private Terrain t;
   
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.E) && canDash )
        {
            
            dash = true;
            start = transform.position;
            end = transform.position + transform.forward * 10;
            post.SetActive(true);
            canDash = false;
            StartCoroutine(waiDash());
            MusicManager.Instance.playRun("dash");
            
        }
        if (dash)
        {
            elipseTime += Time.deltaTime;
            float purcent = elipseTime / duration;
            transform.position = Vector3.Lerp(start, end, purcent);
            if (purcent >= 1)
            {
                ResetDash();

            }
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up/2, transform.TransformDirection(Vector3.forward), out hit, 1))
        {
            Debug.DrawRay(transform.position + Vector3.up/2, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
         //   Debug.Log(hit.collider.name);
            canDash = false;
            if (dash)
            {
                ResetDash();
                print("reseted");
            }
        }
        else
        {
            if (!waitingDash)
            {
                canDash = true;
            }
        }
       

    }

    public void ResetDash()
    {
        
        dash = false;
        elipseTime = 0;
        post.SetActive(false);
    }

    IEnumerator waiDash()
    {
        waitingDash = true;
        canDash = false;
        yield return new WaitForSeconds(timeWait);
        canDash = true;
        waitingDash = false;
    }


}
