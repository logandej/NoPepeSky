using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    [SerializeField] private Player player;
    private TerrainChecker terrainChecker;
    [SerializeField] private Terrain myTerrain;

    [SerializeField] private float timeWalk;
    [SerializeField] private float timeRun;

    private bool isPlaying = false;


    // Start is called before the first frame update
    void Start()
    {
        terrainChecker = new TerrainChecker();
    }

    // Update is called once per frame
    void Update()
    {
        // !!!!!!!!!!!!!!!!!!!!!! AUDIO CLIP MUST HAVE THE SAME NAME AS THE LAYER TEXTURE OTHERWISE IT WILL NOT WORK !!!!!!!!!!!!!!!!!!!!!!
        //
        //MusicManager.Instance.playRun((AudioClip)Resources.Load(terrainChecker.GetLayerName(player.gameObject.transform.position, myTerrain), typeof(AudioClip)));

        if (player.getIsMoving() && !player.getIsJumping() && !isPlaying)
        {
            StartCoroutine(Walk());
        }
    }

    IEnumerator Walk()
    {
        isPlaying = true;
        //MusicManager.Instance.playRun((AudioClip)Resources.Load(terrainChecker.GetLayerName(player.transform.position, myTerrain), typeof(AudioClip)));
        if (player.getIsRunning())
            yield return new WaitForSeconds(timeRun);
        if (player.getIsWalking())
            yield return new WaitForSeconds(timeWalk);
        isPlaying = false;
    }
}
