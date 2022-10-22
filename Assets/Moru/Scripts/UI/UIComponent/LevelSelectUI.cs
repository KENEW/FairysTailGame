using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using PD;

namespace Moru.UI
{
    public class LevelSelectUI : MonoBehaviour
    {
        [SerializeField, LabelText("���� ��ư")] Button LeftBtn;
        [SerializeField, LabelText("������ ��ư")] Button RightBtn;
        [SerializeField, LabelText("������")] Transform contents;
        GAME_INDEX cur_Index;
        int maxStageInt;

        // Start is called before the first frame update
        void Awake()
        {
            PlayerData.instance.onSelectStage += SetUp;
            LeftBtn.onClick.AddListener
                (
                    () => SetUp(PlayerData.GetStageClearDataPerGame(cur_Index - 1), cur_Index - 1)
                );
            RightBtn.onClick.AddListener
                (
                    () => SetUp(PlayerData.GetStageClearDataPerGame(cur_Index + 1), cur_Index + 1)
                );
        }

        public void SetUp(bool[] stageArr, GAME_INDEX index)
        {
            cur_Index = index;
            bool isinit = false;
            //�������� ����Ʈ ������Ʈ
            for (int i = 0; i < contents.childCount; i++)
            {
                //�̷����� ����...���������� �ʹ� ������.
                var comp = contents.GetChild(i).GetComponent<Button>();
                comp.transform.GetChild(1).GetComponent<Text>().text = $"�������� {i + 1}";
                if (i < stageArr.Length)
                {

                    contents.GetChild(i).gameObject.SetActive(true);
                    //������ Ŭ������ ��������
                    if (stageArr[i])
                    {
                        comp.interactable = true;
                        comp.transform.GetChild(0).gameObject.SetActive(false);
                        comp.transform.GetChild(2).gameObject.SetActive(true);
                    }
                    else
                    {
                        if (!isinit)
                        {
                            maxStageInt = i;
                            isinit = true;
                        }
                        //������ �� �ִ� ��������
                        if (comp == contents.GetChild(maxStageInt).GetComponent<Button>())
                        {
                            comp.interactable = true;
                            comp.transform.GetChild(0).gameObject.SetActive(false);
                            comp.transform.GetChild(2).gameObject.SetActive(false);
                        }
                        //�����Ұ����� ��������
                        else
                        {
                            comp.interactable = false;
                            comp.transform.GetChild(0).gameObject.SetActive(true);
                            comp.transform.GetChild(2).gameObject.SetActive(false);
                        }
                    }
                }
                else
                {
                    contents.GetChild(i).gameObject.SetActive(false);
                }
            }

            //�� �� ��ư Ȱ��ȭ
            if (index == 0)
            {

                LeftBtn.interactable = false;
                RightBtn.interactable = PlayerData.IsOpenChapter((GAME_INDEX)index + 1) ? true : false;
            }
            else if (index + 1 == GAME_INDEX.None)
            {
                LeftBtn.interactable = PlayerData.IsOpenChapter((GAME_INDEX)index - 1) ? true : false;
                RightBtn.interactable = false;
            }
            else
            {
                LeftBtn.interactable = PlayerData.IsOpenChapter((GAME_INDEX)index - 1) ? true : false;
                RightBtn.interactable = PlayerData.IsOpenChapter((GAME_INDEX)index + 1) ? true : false;
            }

            //���Ӽ���â �˾� �̹��� ������Ʈ
        }
    }
}