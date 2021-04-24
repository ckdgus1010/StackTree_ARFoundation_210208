using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PointerCtrl : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private CubeCtrl cubeCtrl;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private Vector3 centerVec;

    [Header("Pointer Info")]
    public Transform pivot;
    public GameObject pointer;
    public float lerpSpeed = 0.5f;

    [Header("Ray Info")]
    public GameObject cam;
    public float rayDistance = 10.0f;

    [HideInInspector] public bool isGameboardReady = false;
    [HideInInspector] public GameObject gameboard;
    [HideInInspector] public GameObject guideCube;
    [HideInInspector] public GameObject currCube;

    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        cubeCtrl = GetComponent<CubeCtrl>();
        centerVec = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
    }

    void Update()
    {
        if (isGameboardReady == false)
        {
            hits.Clear();
            if (raycastManager.Raycast(centerVec, hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPose = hits[0].pose;   // 첫 번째로 측정된 면의 정보를 가져옴
                pivot.rotation = Quaternion.Lerp(pivot.rotation, hitPose.rotation, lerpSpeed);
            }
            else
            {
                ResetPointerRot();
            }
        }
        else
        {
            if (GameManager.Instance.modeType == ModeType.Alone_Count)
            {
                return;
            }

            Ray ray = new Ray(cam.transform.position, cam.transform.forward * rayDistance);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, rayDistance, LayerMask.GetMask("Props")))
            {
                GameObject hitObj = hitInfo.collider.gameObject;

                // Cube를 감지했을 때
                if (hitObj.CompareTag("CUBE"))
                {
                    GuideCubeOff();

                    if (hitObj != currCube && currCube != null)
                    {
                        currCube.GetComponent<MeshRenderer>().material.color = Color.white;
                    }

                    currCube = hitObj;
                    currCube.GetComponent<MeshRenderer>().material.color = Color.blue;

                    Vector3 normalVec = hitInfo.normal;

                    //윗면만 감지할 경우 - 모든 면을 감지할 경우는 주석처리 해야 함
                    if (normalVec == currCube.transform.up)
                    {
                        Transform objTr = currCube.transform.GetChild(0).transform;
                        GuideCubeOn(objTr);
                    }
                }
                else if (hitObj.CompareTag("GRID"))
                {
                    //Grid를 감지했을 때
                    if (currCube != null)
                    {
                        currCube.GetComponent<MeshRenderer>().material.color = Color.white;
                        currCube = null;
                    }

                    Transform _tr = hitInfo.collider.transform.Find("CubePos").transform;
                    GuideCubeOn(_tr);
                }
            }
            else
            {
                if (currCube != null)
                {
                    currCube.GetComponent<MeshRenderer>().material.color = Color.white;
                    currCube = null;
                }

                GuideCubeOff();
            }
        }
    }

    void ResetPointerRot()
    {
        Quaternion rot = Quaternion.Euler(90.0f, 0, 0);
        pivot.rotation = Quaternion.Lerp(pivot.rotation, rot, lerpSpeed);
    }

    void GuideCubeOn(Transform _tr)
    {
        if (guideCube == null)
        {
            Debug.LogError("PointerCtrl ::: Guide Cube 없음");
        }

        guideCube.SetActive(true);
        guideCube.transform.position = _tr.position;
        guideCube.transform.rotation = gameboard.transform.rotation;
        guideCube.transform.localScale = _tr.parent.localScale;

        cubeCtrl.cubeTr = _tr;
    }

    void GuideCubeOff()
    {
        if (guideCube == null)
        {
            Debug.LogError("PointerCtrl ::: Guide Cube 없음");
        }

        guideCube.transform.position = Vector3.zero;
        guideCube.SetActive(false);

        cubeCtrl.cubeTr = null;
    }
}
