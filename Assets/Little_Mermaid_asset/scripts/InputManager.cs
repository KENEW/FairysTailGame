using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.EditorTools;
#endif
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{

    private Dictionary<KeyCode, string> keyValuePairs = new Dictionary<KeyCode, string>(); // Ű ������ ��ųʸ�

    public int signNumbers;// ���� ��ư�� ����

    GameObject makedsprite = null; // ���� �� ��ư ��ü

    GameObject[] spritesArray = new GameObject[50]; //������ ��ư ��ü���� �ٷ� �迭
    public GameObject[] spritesPrefab; // ������ �̹����� ���� �迭
    public GameObject extention; // ��Ʈ ������ ���� Ȯ��Ǵ� Panel

    public GameObject parentPrefab; // ������ �θ�

    GameObject[] spriteParents = new GameObject[10]; //������ �θ� ��ü��
    ChangeAnimation changeScripts;
    ScoreManager scoreManager;


    AudioSource audioSource;
    public AudioClip hit_sound;
    public AudioClip miss_sound;



    int adjustPanelPos = 10;

    Vector3 defaultPos = new Vector3(0,0,0);

    #region ���� ��ȯ
    public enum State //���� enum
    {
        success,
        fail,
        normal,
        clear
    }
    private State _nowState;

    public State nowState // enum ������
    {
        get { return nowState; } //get ������Ƽ

        set
        {
            _nowState = value;
            changeScripts.changeAnimation(_nowState);  // state �� set �� �� changeAnimation �޼ҵ� ����

        }
    }
    #endregion

    

    void Awake()
    {
        AddDictionary(); // Ű ����
        initallize(); // �ʱ� ������ ���� �̹��� �����ϴ� �޼ҵ�
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        extention.GetComponent<RectTransform>().sizeDelta = new Vector2((extention.GetComponent<RectTransform>().rect.width * (signNumbers / 5f) + (adjustPanelPos * signNumbers)), 1900);
        changeScripts = gameObject.GetComponent<ChangeAnimation>(); // changeAnimation �Լ��� �����ų �ν��Ͻ� ��ü
        scoreManager = gameObject.GetComponent<ScoreManager>();
    }


    #region ��Ʈ ����
    void initallize()
    {
        for (int i = 0; i < signNumbers; i++)
        {
            var parent = Instantiate(parentPrefab); // �θ� ��ü ����

            parent.name = i.ToString();
            spriteParents[i] = parent; // �θ� �迭�� ��ü �־��ֱ�
            spriteParents[i].transform.SetParent(this.gameObject.transform, false); // �Ŵ��� �Ʒ��� �θ� ����

            spriteParents[i].GetComponent<RectTransform>().anchoredPosition = gameObject.transform.position + new Vector3((i * 175) + 175, 6, 0);

            Vector3 tempParentPos = spriteParents[i].GetComponent<RectTransform>().anchoredPosition;

            int randomNumber = Random.Range(0, spritesPrefab.Length - 1); //���� �̹��� �ѹ�
            var temp_sprite = Instantiate(spritesPrefab[randomNumber]); // ���� �̹��� ����
            spritesArray[i] = temp_sprite;

            temp_sprite.transform.SetParent(spriteParents[i].transform, false); // i��° �θ� �̹��� �־��ֱ�
            temp_sprite.GetComponent<RectTransform>().anchoredPosition = defaultPos;
        }
    }

    void makeSprite() // ��������Ʈ�� ������ϴ� �޼ҵ�
    {
        int randomNumber = Random.Range(0, spritesPrefab.Length - 1); //���� �̹��� �ѹ�
        var temp_sprite = Instantiate(spritesPrefab[randomNumber]); // ���� �̹��� ����
        makedsprite = temp_sprite;
        spritesArray[signNumbers] = makedsprite;

    }

    void push(GameObject temp_sprite)
    {
        temp_sprite.transform.SetParent(spriteParents[signNumbers - 1].transform, false);
    }

    void Scroll() // �۵��ȵɽ� method�� ����
    {
        for (int i = 0; i < signNumbers; i++)
        {//������ �迭�� ��ĭ�� ����, ��ġ�� �ٲ۴� �׸��� ������ �ڸ��� push
            spritesArray[i] = spritesArray[i + 1]; // push�� ���� �������
            spritesArray[i].transform.SetParent(spriteParents[i].transform, false); //��ĭ�� �θ𿡰� ���� ��������Ʈ 1 -> 0����
        }
    } 
    #endregion
    #region Ű �Է�
    private void Update()
    {
        if (Input.anyKeyDown) // �ٿ�
        {
            isKeyDown();
        }
    }


    void isKeyDown()
    {
        foreach (var dic in keyValuePairs)
        {
            if (Input.GetKey(dic.Key))
            {
                isRight(dic.Value); // Ű���� �Է¿� ���� �� ����
            }
        }
    }

    public void AddDictionary()
    {
        keyValuePairs.Add(KeyCode.DownArrow, "Down(Clone)");
        keyValuePairs.Add(KeyCode.LeftArrow, "Left(Clone)");
        keyValuePairs.Add(KeyCode.RightArrow, "Right(Clone)");
        keyValuePairs.Add(KeyCode.UpArrow, "Up(Clone)");
        keyValuePairs.Add(KeyCode.Space, "Space(Clone)");

    } 
    #endregion

    void isRight(string keyString)
    {
        var temp_sprite = spriteParents[0].transform.GetChild(0);
        if (keyString == temp_sprite.name && Time.timeScale != 0)
        {
            nowState = State.success; // ����
            scoreManager.scoreDel(); // ������ ���ھ� ���� ���� ��������Ʈ ����

            SoundManager.PlaySFX(hit_sound);

            Destroy(temp_sprite.gameObject); // ù��° ��� �����ȵ� ����
            makeSprite(); // ����
            Scroll(); // �θ� ��ü�� ���� �̹��� ��ĭ�� ����
            push(makedsprite); // ������ �θ� ��ü�� �ڽ� �̹��� �ֱ�

        }
        else
        {
            scoreManager.failDel();

            SoundManager.PlaySFX(miss_sound);
            nowState = State.fail; // ����
        }
    }


}
