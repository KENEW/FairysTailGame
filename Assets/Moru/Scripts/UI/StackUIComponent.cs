using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class StackUIComponent : MonoBehaviour
{
    [ContextMenu("��������")]
    /// <summary>
    /// �ش� ������Ʈ�� ���� ������Ʈ�� SetActive(true)��� Show()�� ��������־�� �մϴ�.
    /// </summary>
    public void Show()
    {
        //���ӿ�����Ʈ�� Ȱ��ȭ��ŵ�ϴ�.
        this.gameObject.SetActive(true);
        StackUIManager.Instance.Push(this);
    }

    [ContextMenu("��������")]
    /// <summary>
    /// �ش� ������Ʈ�� ���� ������Ʈ�� SetActive(false)��� Hide()�� ��������־�� �մϴ�.
    /// </summary>
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
