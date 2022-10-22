using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PD;

namespace Moru.UI
{
    public class ChangeTitlePopUp : MonoBehaviour
    {
        [SerializeField] Text text;
        [SerializeField] Button AcceptBtn;

        public void Init(AchieveResult result)
        {
            text.text = $"Īȣ�� \"{result.Title}\"���� �ٲٽðڽ��ϱ�?";
            AcceptBtn.onClick.RemoveAllListeners();
            AcceptBtn.onClick.AddListener(
                () =>
                PlayerData.instance.PlayerTitle = result.Title
                );

        }

    }
}