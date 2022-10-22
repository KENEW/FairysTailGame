using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.UI;

public class Clear : MonoBehaviour
{

    public TMP_Text ending_text;
    public TMP_Text fail_text;

    public Image liquor;
    public int require_score;

    public delegate void endingDele(int score);

    public endingDele endingDelegate;

    ChangeAnimation changeAnimation;
    private void Start()
    {
        changeAnimation = gameObject.GetComponent<ChangeAnimation>();
        endingDelegate = ending; // ���ھ� �Ŵ����� �Ѱ��ֱ�
    }

    public void ending(int score)
    {
        if(score >= require_score )
        {
            liquor.gameObject.SetActive(true);
            ending_text.gameObject.SetActive(true);
            changeAnimation.happyEnding();
        }
        else
        {
            fail_text.gameObject.SetActive(true);
            changeAnimation.sadEnding();
            // ���� �ִϸ��̼� + ���� �ؽ�Ʈ
        }
    }

    // ���� ����
    // Ŭ���� ��������Ʈ ����
    // Ŭ���� ���� ���Խ� ��������Ʈ Ȱ��ȭ
    // Ÿ�� ��ĳ�� 0
    // ȭ�� ��Ӱ�
    // �б⿡ ���� �ִϸ��̼� ����
    // ������ �߾ӿ� ���� ǥ��
}
