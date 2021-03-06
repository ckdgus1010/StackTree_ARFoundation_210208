using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TouchManager : MonoBehaviour
{
    private ARRaycastManager raycastMgr;
    private readonly List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private PointerCtrl pointerCtrl;

    public Camera cam;
    public GamePanelCtrl gamePanelCtrl;
    public ButtonManager03 buttonManager;
    public GameObject pointer;
    public CubeCtrl cubeCtrl;
    public AnswerManager answerManager;

    [Header("Game Board Info")]
    public GameObject gameBoardPrefab;
    public float boardSize = 0.1f;
    public GameObject checkboardPrefab;
    private Vector3 originScale;

    [Header("Quest Data - Alone Mode")]
    public GameObject questControllerPrefab;
    private GameObject questController;
    private AloneModeQuestCtrl aloneModeQuestCtrl;
    public GameObject playSceneCanvas;

    [Header("Guide Cube Info")]
    public GameObject guideCubePrefab;
    private GameObject guideCube;

    [HideInInspector]
    public GameObject currGameboard;
    private int touchNum = 0;

    void Start()
    {
        raycastMgr = GetComponent<ARRaycastManager>();
        pointerCtrl = GetComponent<PointerCtrl>();

        touchNum = 0;
        currGameboard = null;
    }

    void Update()
    {
        // 플레이어의 터치가 없는 경우
        if (Input.touchCount == 0 || touchNum != 0)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);

        // UI를 터치한 경우 터치 무시
        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {
            return;
        }

        if (touch.phase == TouchPhase.Began)
        {
            // 평면으로 인식한 곳만 ray로 검출
            if (raycastMgr.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                SetGameBoard();
            }
        }
    }

    void SetGameBoard()
    {
        touchNum += 1;

        // Game Board가 없는 경우
        if (currGameboard == null)
        {
            var rot = Quaternion.LookRotation(hits[0].pose.position - cam.transform.position);
            // 게임 보드 생성
            currGameboard = Instantiate(gameBoardPrefab);
            currGameboard.transform.position = hits[0].pose.position;
            currGameboard.transform.rotation = Quaternion.Euler(cam.transform.position.x
                                                               , rot.eulerAngles.y
                                                               , cam.transform.position.z);

            // 게임 보드 크기 조절
            originScale = currGameboard.transform.localScale;
            currGameboard.transform.localScale *= boardSize;

            // CubeCtrl에 Gameboard 데이터 세팅
            cubeCtrl.SetGameboardData(currGameboard);

            // 각 모드별 알맞은 Game Panel 생성
            gamePanelCtrl.ConvertGamePanel();

            // Gameboard의 Grid 켜기
            GameboardCtrl gameboardCtrl = currGameboard.GetComponent<GameboardCtrl>();
            gameboardCtrl.SetGameboardGrid(5);
            buttonManager.gameboardCtrl = gameboardCtrl;

            // Gameboard에 Checkboard 생성
            if (GameManager.Instance.modeType == ModeType.Alone_Minus || GameManager.Instance.modeType == ModeType.Alone_Plus)
            {
                GameObject checkerboard = Instantiate(checkboardPrefab, currGameboard.transform);
                answerManager.checkerboardCtrl = checkerboard.GetComponent<CheckerboardCtrl>();
                Debug.Log("TouchManager ::: Checkboard 생성");
            }

            // Quest Controller - Alone Mode 생성
            questController = Instantiate(questControllerPrefab);
            aloneModeQuestCtrl = questController.GetComponent<AloneModeQuestCtrl>();
            aloneModeQuestCtrl.SetCurrGameboard(currGameboard);

            // AnswerManager에 alonModeQuestCtrl 연결
            answerManager.aloneModeQuestCtrl = aloneModeQuestCtrl;

            // Guide Cube 생성
            guideCube = Instantiate(guideCubePrefab, currGameboard.transform);
            guideCube.transform.localScale = gameboardCtrl.GetCubeScale();
            guideCube.SetActive(false);
            pointerCtrl.guideCube = guideCube;
        }
        // Game Board가 있는 경우
        else
        {
            currGameboard.SetActive(true);
            currGameboard.transform.position = hits[0].pose.position;
            gamePanelCtrl.ConvertGamePanel();
            currGameboard.GetComponent<GameboardCtrl>().SetGameboardGrid(5);
        }

        pointer.SetActive(false);
        pointerCtrl.isGameboardReady = true;
        pointerCtrl.gameboard = currGameboard;

        buttonManager.aloneModeQuestCtrl = aloneModeQuestCtrl;
        aloneModeQuestCtrl.SetAloneModeQuest(buttonManager, playSceneCanvas);
    }

    public void ResetTouchData()
    {
        if (touchNum == 0)
        {
            return;
        }

        touchNum = 0;
        if (currGameboard != null)
        {
            currGameboard.SetActive(false);
        }

        gamePanelCtrl.ResetCurrGamePanel();

        if (GameManager.Instance.modeType == ModeType.Create)
        {
            currGameboard.transform.localScale = originScale;
            currGameboard.GetComponent<GameboardCtrl>().SetGameboardGrid(5);
        }
        else
        {
            ChangeBoardSize(1);
        }

        aloneModeQuestCtrl.ResetAloneModeQuestData();

        pointer.SetActive(true);
        pointerCtrl.isGameboardReady = false;
    }

    public void ChangeBoardSize(float value)
    {
        currGameboard.transform.localScale = originScale * value;
    }
}
