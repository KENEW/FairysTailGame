using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum NowGameState
{
    GameReady,
    Gaming,
    GameEnd,
    GameStop
}

public class SnowWhiteGameManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("���� �ؽ�Ʈ")]
    private Text startText;

    [SerializeField]
    [Tooltip("���� �ð� �ؽ�Ʈ")]
    private Text timerText;

    [SerializeField]
    [Tooltip("���� ������Ʈ")]
    private GameObject correctObj;

    [SerializeField]
    [Tooltip("���� ��� ǥ�� �̹���")]
    private Image correctAppleImage;

    [SerializeField]
    [Tooltip("���� ��� ǥ�� ��������Ʈ��")]
    private Sprite[] displayCorrectAppleSprits;

    [SerializeField]
    [Tooltip("���� ��� ��������Ʈ��")]
    private Sprite[] correctAppleSprits;

    public float limitTime;

    private int FailCount = 0;
    private int CurrentStage;

    private NowGameState nowGameState;

    public AudioClip BGMs;


    // Start is called before the first frame update
    void Start()
    {
        CurrentStage = PlayerDataXref.instance.GetCurrentStage().StageNum;
        SoundManager.PlayBGM(BGMs);
        StartSetting();
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        CorrectAnswerDistinction();
    }

    private void Timer()
    {
        if (nowGameState == NowGameState.Gaming)
        {
            limitTime -= Time.deltaTime;
            timerText.text = $"���� �ð� {((int)limitTime / 60):D2} : {((int)limitTime % 60):D2}";
            if (limitTime <= 0)
            {
                var startTextComponent = startText.GetComponent<Text>();

                limitTime = 0;

                nowGameState = NowGameState.GameEnd;

                startTextComponent.fontSize = 120;

                timerText.text = $"���� �ð� 00 : 00";
                startText.text = "����...";
                FailCount++;
                WSceneManager.instance.OpenGameFailUI();
            }
        }
    }

    private void CorrectAnswerDistinction()
    {
        if (nowGameState == NowGameState.Gaming && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null && hit.collider.CompareTag("CorrectApple"))
            {
                var startTextComponent = startText.GetComponent<Text>();
                nowGameState = NowGameState.GameEnd;

                startTextComponent.fontSize = 120;

                startText.text = "Ŭ����!";
                //Moru
                PlayerDataXref.instance.ClearGame(GAME_INDEX.Snow_White, CurrentStage);
                WSceneManager.instance.OpenGameClearUI();


                //����é���� ���ϴ�. �ٸ��е鵵 �̷��� ���ֽø� �ǿ�
                if (CurrentStage == PlayerDataXref.instance.GetTargetState_ToOpenNextChapter(GAME_INDEX.Snow_White))
                {
                    //targetClearUI.gameObject.SetActive(true);
                    PlayerDataXref.instance.OpenChapter(GAME_INDEX.Snow_White + 1);
                }
                else
                {
                    //targetClearUI.gameObject.SetActive(false);
                }

                //��Ŭ����  & 1ȸ�� �������� �ʰ� Ŭ����� ���� �̺�Ʈ ����
                if (CurrentStage == PlayerDataXref.instance.GetMaxStageNumber(GAME_INDEX.Snow_White) - 1)
                {
                    PlayerDataXref.instance.SetAchieveSuccess(ACHEIVE_INDEX.SNOW_WHITE_ALL_CLEAR);
                    PlayerDataXref.instance.ClearChapter(GAME_INDEX.Snow_White);
                    if (FailCount == 0)
                    {
                        PlayerDataXref.instance.SetAchieveSuccess(ACHEIVE_INDEX.APPLE_SOMMELIER);
                    }
                }



            }
        }
    }

    private void StartSetting()
    {
        int randSpritsIndex = Random.Range(0, correctAppleSprits.Length);
        correctObj.GetComponent<SpriteRenderer>().sprite = correctAppleSprits[randSpritsIndex];
        correctAppleImage.sprite = displayCorrectAppleSprits[randSpritsIndex];

        limitTime = 30;

        correctObj.transform.position = new Vector3(Random.Range(-8, 9), Random.Range(-3, 4));

        if (correctObj.transform.position.x > 4 || correctObj.transform.position.x < -2)
        {
            correctObj.transform.position = new Vector3(Random.Range(-2, 4), correctObj.transform.position.y);
        }

        nowGameState = NowGameState.GameReady;

        StartCoroutine(StartTextAnim());
    }

    private IEnumerator StartTextAnim()
    {
        WaitForSecondsRealtime textAnimDelay = new WaitForSecondsRealtime(0.5f);
        var startTextComponent = startText.GetComponent<Text>();
        
        startTextComponent.fontSize = 120;

        startText.text = "�غ�..";
        yield return textAnimDelay;

        for (int nowRepetitionIndex = 3; nowRepetitionIndex >= 1; nowRepetitionIndex--)
        {
            startTextComponent.fontSize = 300;

            startText.text = $"{nowRepetitionIndex}";

            while (startTextComponent.fontSize > 2)
            {
                startTextComponent.fontSize -= 1;
                yield return null;
            }
        }

        startTextComponent.fontSize = 120;
        startText.text = "����!";
        yield return textAnimDelay;

        startText.text = "";
        nowGameState = NowGameState.Gaming;

        yield return null;
    }
}
