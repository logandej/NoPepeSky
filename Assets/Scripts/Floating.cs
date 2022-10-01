using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    private float avance;
    [SerializeField] private float speed = 1;
    [SerializeField]  private float amplitude = 1;
    private float basy;

    // Start is called before the first frame update
    void Start()
    {
        basy = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        avance+= speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, basy+  amplitude * Mathf.Sin(avance) , transform.position.z);
        if (avance >= 2*Mathf.PI)
        {
            avance = 0;
        }
    }
}
