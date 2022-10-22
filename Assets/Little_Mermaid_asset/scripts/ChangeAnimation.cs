using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChangeAnimation : MonoBehaviour
{
    public Image[] image;
    public Sprite[] sprite;

    public Sprite[] witchAnime;

    WaitForSeconds waitTime = new WaitForSeconds(1.5f);
    public void changeAnimation(InputManager.State state) //����� �ξ���� ��������Ʈ ��ü
    {
        Debug.Log("�ִϸ��̼� ����");
        switch (state)
        {
            case InputManager.State.normal: // idle
                image[0].sprite = sprite[0]; // �ξ����
                //StartCoroutine(witchAnimation(state));
                witchAnimation(state);
                break;

            case InputManager.State.success: // ��Ʈ ����
                image[0].sprite = sprite[0];
                witchAnimation(state);

                //StartCoroutine(witchAnimation(state));

                break;

            case InputManager.State.fail:   // Ŭ���� ����
                image[0].sprite = sprite[1];
                image[1].sprite = sprite[3];

                break;

            case InputManager.State.clear:   // Ŭ���� ����
                image[0].sprite = sprite[2];
                image[1].sprite = sprite[4];
                break;
            default:
                break;
        }

    }

    void witchAnimation(InputManager.State state)
    {
        while(state != InputManager.State.fail)
        {
            for (int i = 0; i < witchAnime.Length; i++)
            {
                image[1].sprite = witchAnime[i];
            }
        }

    }
}
