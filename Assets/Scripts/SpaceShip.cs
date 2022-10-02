using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private GameObject ship;
    [SerializeField] private GameObject cameraObj;

    private Player player;
    public float speed;

    private bool controlShip = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        player = GameManager.Instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        if (controlShip)
        {
            rb.AddForce(transform.forward * Input.GetAxisRaw("Vertical") * speed, ForceMode.Impulse);
            transform.Rotate(0, Input.GetAxisRaw("Horizontal"), 0);
            if (Input.GetKeyDown(KeyCode.N))
            {
                
                    ControlPlayer();
                
            }
        }
    }

    public void ControlShip()
    {
        player.gameObject.SetActive(false);
        controlShip = true;
        cameraObj.SetActive(true);
        controlShip = true;
    }
    public void ControlPlayer()
    {
        player.gameObject.SetActive(true);
        controlShip = false;
        cameraObj.SetActive(false);
        controlShip = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name.Equals("Character"))
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                print("ok");
                if (!controlShip)
                {
                    ControlShip();
                }
               
            }
        }
    }
}
