using Moru.Cinderella;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChangeAnimation : MonoBehaviour
{
    public Image mermade;
    public GameObject witch;

    Animator witch_animator;

    string Isend;
    string Isgood;

    public Sprite[] sprite;
    public Sprite[] witchAnime;

    WaitForSeconds waitTime = new WaitForSeconds(1.5f);

    private void Start()
    {
        witch_animator = witch.GetComponent<Animator>();
    }

    public void changeAnimation(InputManager.State state) //����� �ξ���� ��������Ʈ ��ü
    {
        switch (state)
        {
            case InputManager.State.normal: // idle
                mermade.sprite = sprite[0]; // �ξ����
                break;
            case InputManager.State.success: // ��Ʈ ����
                mermade.sprite = sprite[0];
                break;
            case InputManager.State.fail:   // Ŭ���� ����
                mermade.sprite = sprite[1];
                break;
            case InputManager.State.clear:   // Ŭ���� ����
                mermade.sprite = sprite[2];
                break;
            default:
                break;
        }
    }

    public void sadEnding()
    {
        witch_animator.SetBool("Isend", true);
        mermade.sprite = sprite[1];
        WSceneManager.instance.OpenGameFailUI();
    }

    public void happyEnding()
    {
        witch_animator.SetBool("Isgood", true);
        mermade.sprite = sprite[2];
        WSceneManager.instance.OpenGameClearUI();

    }
}
