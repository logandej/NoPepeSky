using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    
    public bool dashing = false;
    private bool canDash = true;
    private bool waitingDash = false;

    [SerializeField] private float timeWait = 0.1f;
    [SerializeField] private float duration = 1f;

    [SerializeField] private GameObject post;
    private Player player;
    private TranslateToPoint tp;
   
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.player;
        tp = this.GetComponent<TranslateToPoint>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.E) && canDash && player.getIsRunning())
        {

            StartCoroutine(BeginDash());
            
        }


        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up/2, transform.TransformDirection(Vector3.forward), out hit, 1))
        {
            Debug.DrawRay(transform.position + Vector3.up/2, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            canDash = false;
            if (dashing)
            {
                if (hit.collider.CompareTag("Ennemy"))
                {
                    hit.collider.GetComponent<Ennemy>().BeginTime(this.gameObject);
                }
                EndDash();
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
    IEnumerator BeginDash()
    {
        dashing = true;
        post.SetActive(true);
        canDash = false;
        StartCoroutine(waiDash());
        MusicManager.Instance.playRun("dash");
        tp.TranslateObject(this.gameObject,this.transform.position, this.transform.position + transform.forward * 10, duration);
        yield return new WaitUntil(() => tp.isTranslating == false); // == false pour la compréhension du code, attendre que l'objet ait fini la translation
        EndDash();
    }
    public void EndDash()
    {
        dashing = false;
        tp.StopTRanslate();
        post.SetActive(false);
        StartCoroutine(waiDash());

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
