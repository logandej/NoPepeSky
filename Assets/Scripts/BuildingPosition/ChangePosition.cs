using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePosition : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    [SerializeField] private GameObject place;

    [SerializeField] private GameObject cubeReel;
    [SerializeField] private GameObject placeReel;


    [SerializeField] private Transform TransSimu;
    [SerializeField] private Transform TransReal; 

    [SerializeField] private int lenghtList=1;
    [SerializeField] private List<Vector2> points;
    [SerializeField] private int[,] liste;
    private int x = 0;
    private int z = 0;
    // Start is called before the first frame update
    void Start()
    {
        Transform find = FindScale(cubeReel.transform);

        liste = new int[lenghtList, lenghtList];
        foreach (Vector2 pts in points)
        {
            addSimu((int)pts.x, (int)pts.y);
            addReel((int)pts.x, (int)pts.y, find);

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && z < lenghtList-1 && liste[x,z+1] != 1)
        {
            z++;
            place.transform.position += Vector3.forward * place.transform.localScale.x;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && z > 0 && liste[x, z - 1] != 1)
        {
            z--;
            place.transform.position -= Vector3.forward * place.transform.localScale.x;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && x > 0 && liste[x -1, z] != 1)
        {
            x--;
            place.transform.position -= Vector3.right * place.transform.localScale.x;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) &&  x < lenghtList - 1 && liste[x + 1, z] != 1)
        {
            x++;
            place.transform.position += Vector3.right * place.transform.localScale.x;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            ApplyChange();
        }
    }
    //Ajoute les cube dans le panneau simulé
    private void addSimu(int x1, int z1)
    {
        GameObject obj = Instantiate(cube, new Vector3(10, 1, 10), Quaternion.identity, TransSimu);
        obj.transform.localPosition = new Vector3(x1, 1, z1);
        liste[x1, z1] = 1;
    }

    //Ajoute les batiments dans le jeu
    private void addReel(int x1, int z1, Transform find)
    {
        GameObject obj = Instantiate(cubeReel, new Vector3(10, 1, 10), Quaternion.identity, TransReal);

        obj.transform.localPosition = new Vector3(find.localScale.x * 2 * x1, 1, find.localScale.x * 2 * z1);
    }
    //TRouve la taille de l'objet qui contient un element Echelle
    private Transform FindScale(Transform find)
    {
        Transform tofind = find;
        foreach (Transform tr in find)
        {
            if (tr.name == "Echelle")
            {
                tofind = tr.GetComponent<Transform>();
            }
        }
        return tofind;
    }

    private void ApplyChange()
    {

        Transform find = FindScale(cubeReel.transform);
       
        print(find.localScale.z + " "+ find.name);
        placeReel.transform.localPosition = new Vector3(x * find.localScale.x * 2, 0, z * find.localScale.z * 2);
    }

}
