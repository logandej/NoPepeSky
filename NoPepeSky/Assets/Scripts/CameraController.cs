using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform LookAtFocus;

    [SerializeField] private GameObject camPOV; // First Person View
    [SerializeField] private GameObject camFOV; // Third Person View


    [SerializeField] private Player player;

    [SerializeField] private float min;
    [SerializeField] private float max;

    [HideInInspector]
    public float minVisionEditor;
    private float minVision;
    [HideInInspector]
    public float maxVisionEditor;
    private float maxVision;

    [SerializeField] private bool firstPerson;

    private Vector3 positionThirdPerson;

#if UNITY_EDITOR
    [CustomEditor(typeof(CameraController))]
    public class CameraControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            CameraController cam = (CameraController)target;
            base.OnInspectorGUI();
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.LabelField("Vision From Horizontal view extend to Vertical view");

            EditorGUILayout.LabelField("POV_BottomVision");
            cam.minVisionEditor = EditorGUILayout.Slider(cam.minVisionEditor, -80, 0);

            EditorGUILayout.LabelField("POV_TopVision");
            cam.maxVisionEditor = EditorGUILayout.Slider(cam.maxVisionEditor, 0, 80);

            if (GUI.changed)
            {
                EditorUtility.SetDirty(cam);
            }
        }



    }
#endif
    // Start is called before the first frame update
    void Start()
    {
        positionThirdPerson = camFOV.transform.position; //Save of camFOV position;
        camPOV.SetActive(false);

        minVision = minVisionEditor + 79;
        maxVision = 359 - maxVisionEditor;
    }

    // Update is called once per frame
    void Update()
    {
        var sensi = Input.GetAxis("Mouse Y")*-0.1f;

        if (!firstPerson)//Controll camera if Third Person View
        {
            if (camFOV.transform.localPosition.y + sensi >= min && camFOV.transform.localPosition.y + sensi <= max)
            {
                camFOV.transform.localPosition += new Vector3(0, sensi, 0);

            }
            else if (camFOV.transform.localPosition.y < min)
            {
                camFOV.transform.localPosition = new Vector3(camFOV.transform.localPosition.x, min, camFOV.transform.localPosition.z);

            }
            else if (camFOV.transform.localPosition.y > max)
            {
                camFOV.transform.localPosition = new Vector3(camFOV.transform.localPosition.x, max, camFOV.transform.localPosition.z);
            }
            if (!player.getIsMoving())
            {
                player.canRotate = false;
                camFOV.transform.RotateAround(LookAtFocus.position, Vector3.up, Input.GetAxisRaw("Mouse X") * 2); // Make camera Rotate Around Player when he's not moving
            }
            else // Can Rotate the player when he's moving
            {
                player.canRotate = true;
                player.LookVector(camFOV.transform.rotation.eulerAngles);
                ForwardView();
            }
            camFOV.transform.LookAt(LookAtFocus);
        }
        else // Control camera when First Person View POV
        {
            camPOV.transform.Rotate(Input.GetAxisRaw("Mouse Y") * -2, 0, 0);
            if (camPOV.transform.localEulerAngles.x > minVision && camPOV.transform.localEulerAngles.x < 180)
                camPOV.transform.localEulerAngles = new Vector3(minVision, camPOV.transform.localEulerAngles.y, 0);

            if (camPOV.transform.localEulerAngles.x < maxVision && camPOV.transform.localEulerAngles.x > 180)
                camPOV.transform.localEulerAngles = new Vector3(maxVision - 360, camPOV.transform.localEulerAngles.y, 0);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FirstPersonView();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ThirdPersonView();
        }
    }

    public void ForwardView()
    {
        camFOV.transform.localPosition = new Vector3(0, camFOV.transform.localPosition.y, positionThirdPerson.z);
    }
    public void ThirdPersonView()
    {
        camFOV.SetActive(true);
        camPOV.SetActive(false);
        firstPerson = false;
        //transform.eulerAngles = positionThirdPerson.eulerAngles;
    }

    public void FirstPersonView()
    {
        camFOV.SetActive(false);
        camPOV.SetActive(true);
        player.canRotate = true;
        firstPerson = true;
    }
}
