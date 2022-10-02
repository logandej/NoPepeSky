using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordReturn : MonoBehaviour
{
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !player.isDoingAction)
        {
            StartCoroutine(wait());
        }
    }
    IEnumerator wait()
    {
        player.isAttacking = true;
        player.isDoingAction = true;
        player.playAnim("SwordReturn");
        MusicManager.Instance.playEffect("Swoosh");
        yield return new WaitForSeconds(0.9f);
        player.isDoingAction = false;
        player.isAttacking = false;
    }
}
