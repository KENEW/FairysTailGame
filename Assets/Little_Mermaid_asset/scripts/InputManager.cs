using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{

    private Dictionary<KeyCode, string> keyValuePairs = new Dictionary<KeyCode, string>();

    public int singNumbers;// ���� ��ư�� ����

    GameObject sprite = null; // ��ư ��ü
    GameObject[] spritesArray = new GameObject[50]; //������ ��ư ��ü���� �ٷ� �迭
    public GameObject[] spritesPrefab; // ������ �̹����� ���� �迭
    public GameObject extention;

    public GameObject parentPrefab; // ������ �θ�

    GameObject[] spriteParents = new GameObject[10]; //������ �θ� ��ü��
    ChangeAnimation changeScripts;

    Vector3 defaultPos = new Vector3(0,0,0);

    public enum State //���� enum
    {
        success,
        fail,
        normal,
        clear
    }
    public State nowState // enum ������
    {
        get { return nowState; } //^^^ ��ü������ �����÷ο� ��

        set 
        { 
            changeScripts.changeAnimation(nowState);  // state �� set �� �� changeAnimation �޼ҵ� ����
        }
    }

    void Awake()
    {
        AddDictionary(); // Ű ����
        initallize(); // �ʱ� ������ ���� �̹��� �����ϴ� �޼ҵ�
    }

    private void Start()
    {

        extention.GetComponent<RectTransform>().sizeDelta = new Vector2(0.5f * (singNumbers/5), 0.15f); // �Ⱥ���
        changeScripts = gameObject.GetComponent<ChangeAnimation>(); // changeAnimation �Լ��� �����ų �ν��Ͻ� ��ü
    }


    void initallize()
    {
        for (int i = 1; i < singNumbers+1; i++)
        {
            var parent = Instantiate(parentPrefab); // �θ� ��ü ����

            parent.name = i.ToString();
            spriteParents[i - 1] = parent; // �θ� �迭�� ��ü �־��ֱ�
            spriteParents[i - 1].transform.SetParent(this.gameObject.transform,false); // �Ŵ��� �Ʒ��� �θ� ����
            
            spriteParents[i - 1].GetComponent<RectTransform>().anchoredPosition = gameObject.transform.position + new Vector3(i*175, 6,0); // �θ� ��ġ ���� �� �����ؾ���

            Vector3 tempParentPos = spriteParents[i - 1].GetComponent<RectTransform>().anchoredPosition;

            int randomNumber = Random.Range(0, spritesPrefab.Length-1); //���� �̹��� �ѹ�
            var temp_sprite = Instantiate(spritesPrefab[randomNumber]); // ���� �̹��� ����
            sprite = temp_sprite;
            spritesArray[i - 1] = sprite;

            sprite.transform.SetParent(spriteParents[i - 1 ].transform,false); // i��° �θ� �̹��� �־��ֱ�
            sprite.GetComponent<RectTransform>().anchoredPosition = defaultPos;
        }
    }

    void makeSprite() // ��������Ʈ�� ������ϴ� �޼ҵ�
    {
        int randomNumber = Random.Range(0, spritesPrefab.Length - 1); //���� �̹��� �ѹ�
        var temp_sprite = Instantiate(spritesPrefab[randomNumber]); // ���� �̹��� ����
        sprite = temp_sprite;
    }

    void push(GameObject temp_sprite)
    {
        temp_sprite.transform.SetParent(spriteParents[spriteParents.Length-1].transform,false);
    }

    void Scroll() // �۵��ȵɽ� method�� ����
    {
        for (int i = 1; i < singNumbers; i++)
        {
            spritesArray[i].transform.SetParent(spriteParents[i - 1].transform, false); //��ĭ�� �θ𿡰� ����
            spriteParents[i].transform.position = defaultPos; // ��ġ�� �ʱ�ȭ
        }
        spritesArray[singNumbers].transform.SetParent(spriteParents[singNumbers - 1].transform); //������ ĭ �Ҵ�
        spriteParents[singNumbers].transform.position = defaultPos; // ��ġ�� �ʱ�ȭ

    }

    private void Update()
    {
        if (Input.anyKey)
        {
            isKeyDown();
        }
    }

    #region Ű �Է�
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
        keyValuePairs.Add(KeyCode.DownArrow, "Down");
        keyValuePairs.Add(KeyCode.LeftArrow, "Left");
        keyValuePairs.Add(KeyCode.RightArrow, "Right");
        keyValuePairs.Add(KeyCode.UpArrow, "Up");
        keyValuePairs.Add(KeyCode.Space, "Space");

    } 
    #endregion

    void isRight(string keyString)
    {
        if(keyString == sprite.name)
        {
            nowState = State.success; // ����

            Destroy(sprite); // destroy �´���?
            makeSprite(); // ����
            Scroll(); // �θ� ��ü�� ���� �̹��� ��ĭ�� ����
            push(sprite); // ������ �θ� ��ü�� �ڽ� �̹��� �ֱ�

        }
        else
        {
            nowState = State.fail; // ����
        }
    }

    //Ŭ���� �Լ� �߰�
}
