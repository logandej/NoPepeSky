using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePosition : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    [SerializeField] private GameObject cubeReel;
    [SerializeField] private GameObject place;

    [SerializeField] private int lenghtList=1;
    [SerializeField] private List<Vector2> points;
    [SerializeField] private int[,] liste;
    private int x=0;
    private int z = 0;
    // Start is called before the first frame update
    void Start()
    {
       liste = new int[lenghtList, lenghtList];
        foreach (Vector2 pts in points)
        {
            addCube((int)pts.x, (int)pts.y);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && z < lenghtList-1 && liste[x,z+1] != 1)
        {
            z++;
            cube.transform.position += Vector3.forward * cube.transform.localScale.x;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && z > 0 && liste[x, z - 1] != 1)
        {
            z--;
            cube.transform.position -= Vector3.forward * cube.transform.localScale.x;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && x > 0 && liste[x -1, z] != 1)
        {
            x--;
            cube.transform.position -= Vector3.right * cube.transform.localScale.x;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) &&  x < lenghtList - 1 && liste[x + 1, z] != 1)
        {
            x++;
            cube.transform.position += Vector3.right * cube.transform.localScale.x;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            Add();
        }
    }
    private void addCube(int x, int z)
    {
        GameObject obj = Instantiate(place, new Vector3(10, 1, 10), Quaternion.identity, transform);
        obj.transform.localPosition = new Vector3(x, 1, z);
        liste[x, z] = 1;
    }

    private void Add()
    {
        
        Transform t = cubeReel.transform;
        Transform find = t;
        foreach (Transform tr in t)
        {
            if (tr.name == "Echelle")
            {
                find = tr.GetComponent<Transform>();
            }
        }
        print(find.localScale.z + " "+ find.name);
        cubeReel.transform.localPosition = new Vector3(x * find.localScale.x * 2, 0, z * find.localScale.z * 2);
    }
}
