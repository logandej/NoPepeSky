using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    private Rigidbody rb;
   
   
    [SerializeField] private float duration = 1f;
    [SerializeField] private GameObject post;
    [SerializeField] private ParticleSystem deadParticle;

    private TranslateToPoint tp;
    private Dash dashplayer;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        player = GameManager.Instance.player;
        dashplayer = player.GetComponent<Dash>();
        tp = this.GetComponent<TranslateToPoint>();
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void BeginTime(GameObject player)
    {
        tp.TranslateObject(this.gameObject, this.transform.position, this.transform.position + player.transform.forward * 4, duration);
        StartCoroutine(wait());
    }

    private void OnCollisionEnter(Collision other)
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Weapon") && player.isAttacking)
        {
            Time.timeScale = 1f;
            post.SetActive(false);
            MusicManager.Instance.playRun("pixel");
            Instantiate(deadParticle, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
    IEnumerator wait()
    {
        post.SetActive(true);
        Time.timeScale = 0.3f;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse1));
        yield return new WaitForSeconds(1);
        Time.timeScale = 1f;
        post.SetActive(false);


    }

   
}
