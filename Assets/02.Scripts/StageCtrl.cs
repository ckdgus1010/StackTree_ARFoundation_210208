using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageCtrl : MonoBehaviour
{
    private RectTransform contentRectTr;
    public ButtonManager01 buttonManager01;

    [Header("팝업 제목")]
    public Text titleText;

    [Header("Prefabs related with Stage")]
    public GameObject stageGroupPrefab;
    public GameObject stagePrefab;
    public Sprite[] stateImages = new Sprite[3];
    public List<GameObject> stageList;

    [Header("Pagination Control")]
    public GameObject paginationParent;
    public ToggleGroup paginationToggleGroup;
    public GameObject paginationPrefab;
    public float paginationInterval = 15.0f;
    public List<Toggle> paginations;
    private float paginationSize = 0.0f;
    private int currPaginationNum = 0;

    [Header("한 그룹 안에 들어갈 개수")]
    public int stageCountInOneGroup = 9;
    private int stageCount = 0;
    private int share = 0;
    private int remainder = 0;

    [Header("Slider Control")]
    public Scrollbar horizontalSlider;
    public float lerpSpeed = 5.0f;

    [Header("Swipe Control")]
    public float sensitivity = 1.0f;
    private Vector2 destination = Vector2.zero;
    private Vector2 startPos;
    private Vector2 endPos;
    public int currentStageID = 0;
    public int currPoint = 0;
    public int[] points;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;

            // 방향 확인
            Vector2 dir = endPos - startPos;

            // startPos & endPos 초기화
            startPos = Vector2.zero;
            endPos = Vector2.zero;

            // 단순 터치인 경우
            if (dir.x == 0)
            {
                return;
            }

            if (dir.x > sensitivity)
            {
                //Debug.Log("왼쪽으로 이동");
                if (currPoint != 0)
                {
                    destination = new Vector2(points[--currPoint], 0);
                    paginations[--currPaginationNum].isOn = true;
                }
            }
            else if (dir.x < -sensitivity)
            {
                //Debug.Log("오른쪽으로 이동");
                if (currPoint < points.Length - 1)
                {
                    destination = new Vector2(points[++currPoint], 0);
                    paginations[++currPaginationNum].isOn = true;
                }
            }
            else
            {
                return;
            }
        }

        contentRectTr.anchoredPosition = Vector2.Lerp(contentRectTr.anchoredPosition, destination, lerpSpeed * Time.deltaTime);
    }

    void SetTitleText()
    {
        ModeType modeType = GameManager.Instance.modeType;
        int _stageID = GameManager.Instance.stageID;
        string _titleTxt = "";
        
        if (modeType == ModeType.Alone_Count)
        {
            _titleTxt = "유형 01\n큐브 보고 개수 맞추기";
        }
        else if (modeType == ModeType.Alone_Minus)
        {
            _titleTxt = "유형 02\n카드 보고 큐브 빼기";
        }
        else if (modeType == ModeType.Alone_Plus)
        {
            _titleTxt = "유형 03\n카드 보고 큐브 쌓기";
        }

        titleText.text = _titleTxt;
    }

    // Stage 상태 설정
    public void SetStageData()
    {
        // Stage 버튼이 생성되지 않았을 때
        if (stageList.Count == 0)
        {
            contentRectTr = GetComponent<RectTransform>();
            paginationSize = paginationPrefab.GetComponent<RectTransform>().sizeDelta.x;

            CheckWhetherEvenOdd();

            // 단계 그룹 수 만큼 Pagination 생성
            SetPaginations(share, remainder);

            // 단계 그룹 및 단계 버튼 생성
            SetStageButtons(share, remainder);
        }

        // 팝업 제목 설정
        SetTitleText();

        for (int i = 0; i < GameManager.Instance.currStageStateArray.Length; i++)
        {
            // 현재 단계 상태 확인
            AloneModeStageState status = GameManager.Instance.currStageStateArray[i];
            Transform obj = stageList[i].transform;
            Image image = obj.GetChild(0).GetComponent<Image>();
            image.sprite = stateImages[(int)status];
            GameObject profileImage = stageList[i].transform.GetChild(2).gameObject;

            // Current 상태인 경우 프로필 사진 표시
            if (status == AloneModeStageState.Current)
            {
                GameManager.Instance.currentStageID = i;
                profileImage.transform.gameObject.SetActive(true);
                profileImage.GetComponent<Image>().sprite = GameManager.Instance.profileImages[GameManager.Instance.profileImageNum];
            }
            else
            {
                profileImage.SetActive(false);
            }

            // 단계 표시
            Text text = stageList[i].transform.GetChild(1).GetComponent<Text>();
            int stageNum = i + 1;
            text.text = stageNum.ToString();
        }

        // 위치 설정
        SetDefaultPosition(share, remainder);
    }

    // 생성될 Stage Group의 개수가 짝수인지 홀수인지 확인
    void CheckWhetherEvenOdd()
    {
        stageCount = GameManager.Instance.stageCount;

        share = stageCount / stageCountInOneGroup;
        remainder = stageCount % stageCountInOneGroup;
    }

    void SetStageButtons(int share, int remainder)
    {
        if (remainder != 0)
        {
            share += 1;
        }

        for (int i = 0; i < share; i++)
        {
            // 단계 그룹 생성
            GameObject _stageGroup = Instantiate(stageGroupPrefab, this.transform);

            for (int j = 0; j < stageCountInOneGroup; j++)
            {
                int _totalStageCount = stageCountInOneGroup * i + j;

                // 마지막 단계인 경우
                if (_totalStageCount == stageCount)
                {
                    return;
                }

                // 단계 버튼 생성
                GameObject _stagePrefab = Instantiate(stagePrefab, _stageGroup.transform);
                stageList.Add(_stagePrefab);

                // 몇 단계인지 표시
                int parameter = i * stageCountInOneGroup + 1;
                int _stageNum = j + parameter;
                _stagePrefab.name = "Stage 0" + _stageNum.ToString();

                // 버튼마다 StageID 변경
                StageData data = _stagePrefab.GetComponent<StageData>();
                data.stageID = _stageNum;

                // 버튼 기능 추가
                Button button = _stagePrefab.GetComponent<Button>();
                button.onClick.AddListener(()=> { buttonManager01.SelectAloneStage(); });
            }
        }
    }

    // 메뉴 기본 위치 설정
    void SetDefaultPosition(int share, int remainder)
    {
        if (remainder != 0)
        {
            share += 1;
        }

        points = new int[share];
        for (int i = 0; i < share; i++)
        {
            if (i == 0)
            {
                points[0] = 600 * (share - 1);
            }
            else
            {
                points[i] = points[i - 1] - 1200;
            }
        }

        // Current Stage 확인
        currentStageID = GameManager.Instance.currentStageID;

        // Current Stage가 있는 Group 확인
        currPoint = currentStageID / stageCountInOneGroup;

        // 목적지 설정
        contentRectTr.anchoredPosition = new Vector3(points[currPoint], 0);
        destination = contentRectTr.anchoredPosition;
        currPaginationNum = currPoint;
        paginations[currPaginationNum].isOn = true;
    }

    // 알맞은 위치에 pagination 생성
    void SetPaginations(int share, int remainder)
    {
        if (remainder != 0)
        {
            share += 1;
        }

        int num = share % 2;
        float parameter = (paginationSize + paginationInterval) * 0.5f;
        float evenNumCase = (1 - share) * parameter;
        float oddNumCase = (2 - 2 * share) * parameter;
        float numCase = num == 0 ? evenNumCase : oddNumCase;

        for (int i = 0; i < share; i++)
        {
            GameObject _paginationPrefab = Instantiate(paginationPrefab, paginationParent.transform);

            RectTransform rectTr = _paginationPrefab.GetComponent<RectTransform>();
            float _numCase = numCase + 2 * parameter * i;
            rectTr.anchoredPosition = new Vector2(_numCase, 0);

            Toggle paginationToggle = _paginationPrefab.GetComponentInChildren<Toggle>();
            paginationToggle.group = paginationToggleGroup;
            paginations.Add(paginationToggle);
        }
    }
}
