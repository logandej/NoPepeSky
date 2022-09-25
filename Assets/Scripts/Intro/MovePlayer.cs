using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private GameObject cameraCine;
    [SerializeField] private List<Transform> transformList;
    [SerializeField] private Animator anim;
    [SerializeField] private Animator animGrab;
    [SerializeField] private Animator animEnnemy;

    [SerializeField] private ParticleSystem gunParticle;
    [SerializeField] private Image cache;
    [SerializeField] private GameObject child;
    [SerializeField] private float timeFront = 5;
    [SerializeField] private float speed;

    private bool canRun = true;
    private bool canChildRun = false;
    // Start is called before the first frame update
    void Start()
    {
        child.SetActive(false);
        StartCoroutine(wait());
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canRun)
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if (canChildRun)
            child.transform.Translate(Vector3.forward * Time.deltaTime * (speed/2));

    }

    public void resetTransform(GameObject obj)
    {
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
    

    IEnumerator wait()
    {
        //Dead1
        yield return new WaitForSeconds(5);
        cameraCine.transform.parent = transformList[0];
        resetTransform(cameraCine);
        canRun = false;
        yield return new WaitForSeconds(3.5f);

        //Front
        canRun = true;
        cameraCine.transform.parent = transformList[1];
        resetTransform(cameraCine);
        yield return new WaitForSeconds(6.7f);

        //Moon
        canRun = false;
        cameraCine.transform.parent = transformList[2];
        resetTransform(cameraCine);
        yield return new WaitForSeconds(3.5f);

        //Ennemy
        canRun = true;
        cameraCine.transform.parent = transformList[3];
        resetTransform(cameraCine);
        yield return new WaitForSeconds(3.5f);

        //Child
        canRun = false;
        cameraCine.transform.parent = transformList[4];
        resetTransform(cameraCine);
        yield return new WaitForSeconds(5.5f);

        //ChildGrab
        canRun = false;
        cameraCine.transform.parent = transformList[5];
        resetTransform(cameraCine);
        animGrab.Play("Grab");
        this.transform.position += Vector3.forward * 50;
        yield return new WaitForSeconds(1.2f);

        //ChildGrabbedSide
        canRun = true;
        cameraCine.transform.parent = transformList[6];
        resetTransform(cameraCine);
        anim.Play("HoldingRun");
        child.SetActive(true);
        yield return new WaitForSeconds(7.1f);

        //ChildGrabbedSide
        canRun = true;
        cameraCine.transform.parent = transformList[7];
        resetTransform(cameraCine);
        yield return new WaitForSeconds(5);

        //ChildGrabbedFRont
        canRun = true;
        cameraCine.transform.parent = transformList[8];
        resetTransform(cameraCine);
        yield return new WaitForSeconds(5);

        //Ennemy
        canRun = true;
        cameraCine.transform.parent = transformList[9];
        resetTransform(cameraCine);
        yield return new WaitForSeconds(5);


        //ChildAgain
        canRun = true;
        cameraCine.transform.parent = transformList[10];
        resetTransform(cameraCine);
        yield return new WaitForSeconds(5);

        //GunStanding
        canRun = false;
        cameraCine.transform.parent = transformList[11];
        resetTransform(cameraCine);
        animEnnemy.Play("GunStanding");
        yield return new WaitForSeconds(1);


        //Death
        gunParticle.Stop();
        canRun = false;
        anim.Play("DeathBack");
        cameraCine.transform.parent = transformList[12];
        resetTransform(cameraCine);
        yield return new WaitForSeconds(5);

        //Turn
        canRun = false;
        cameraCine.transform.parent = transformList[13];
        resetTransform(cameraCine);
        animEnnemy.Play("TurnEnnemy");
        yield return new WaitForSeconds(2.5f);

        //Standup
        canRun = false;
        animEnnemy.transform.parent.parent.gameObject.SetActive(false);
        cameraCine.transform.parent = transformList[14];
        resetTransform(cameraCine);
       
        child.transform.rotation = Quaternion.Euler(Vector3.zero);
        child.GetComponent<Animator>().Play("GettingUp");
        yield return new WaitForSeconds(4);

        //Standup
        canChildRun = true;
        animEnnemy.transform.parent.parent.gameObject.SetActive(false);
        child.transform.Rotate(0, -90, 0);
        child.transform.position -= Vector3.up*0.2f;
        cameraCine.transform.parent = transformList[15];
        resetTransform(cameraCine);
        child.GetComponent<Animator>().Play("FastRun");
        yield return new WaitForSeconds(3);
        for(int i=0; i<100; i++)
        {
            print("salam");
            Color c = cache.color;
            c.a += 0.01f;
            cache.color = c;
            yield return new WaitForSeconds(0.05f);

        }
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("SampleScene");


    }
}
