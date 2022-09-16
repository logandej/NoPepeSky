using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private GameObject cameraCine;
    [SerializeField] private Transform transformLookFront;
    [SerializeField] private Transform transformLookEnnemy;


    [SerializeField] private float timeFront = 5;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(wait());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    public void resetTransform(GameObject obj)
    {
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
    

    IEnumerator wait()
    {
        yield return new WaitForSeconds(timeFront);
        cameraCine.transform.parent = transformLookFront;
        resetTransform(cameraCine);
        yield return new WaitForSeconds(timeFront);
        cameraCine.transform.parent = transformLookEnnemy;
        resetTransform(cameraCine);
        //cameraCine.transform.Rotate(0, -90, 0);
    }
}
