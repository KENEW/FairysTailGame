using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum GAME_INDEX
{
    Snow_White,         //�鼳����
    Cinderella,         //�ŵ�����
    Pinocchio,          //�ǳ�Ű��
    Little_Mermaid,     //�ξ����
    Jack_And_Beanstalk, //��� �ᳪ��
    Tree_Little_Pigs,    //�Ʊ� ���� ������
    None
}

public enum ACHEIVE_INDEX
{
    /// <summary>
    /// �������� : �ǳ�Ű�� ��Ŭ����
    /// </summary>
    PINOCCHIO_ALL_CLEAR,

    /// <summary>
    /// ������������ : �ŵ����� ��Ŭ����
    /// </summary>
    CINDERELLA_ALL_CLEAR,

    /// <summary>
    /// ��������� : �鼳���� ��Ŭ����
    /// </summary>
    SNOW_WHITE_ALL_CLEAR,

    /// <summary>
    /// ������ : �ξ���� ��Ŭ����
    /// </summary>
    LITTLE_MERMAID_ALL_CLEAR,

    /// <summary>
    /// �ڶ󳪶󳪹����� : ��� �ᳪ�� ��Ŭ����
    /// </summary>
    JACK_AND_BEANSTALK_ALL_CLEAR,

    /// <summary>
    /// ������, �Ʊ���������� ��Ŭ����
    /// </summary>
    TREE_LITTLE_PIGS_ALL_CLEAR,

    /// <summary>
    /// ���� ����Ŀ : ��� ���丮 ���� �ر�
    /// </summary>
    END_MAKER,

    /// <summary>
    /// The End : ��� �������� ��Ŭ����
    /// </summary>
    ALL_CLEAR,

    /// <summary>
    /// �������� : �ŵ����� �÷��� �� 1ȸ�� �й����� �ʴ� ���
    /// </summary>
    PUZZLE_MASTER,

    /// <summary>
    /// ���� ������ : �ǳ�Ű�� �÷��� �� 1ȸ�� �й����� �ʴ� ���
    /// </summary>
    SAWING_MASTER,

    /// <summary>
    /// ��� �ҹɸ��� : �鼳���� �÷��� �� 1ȸ�� �й����� �ʴ� ���
    /// </summary>
    APPLE_SOMMELIER,

    /// <summary>
    /// ��� : �ξ���� �÷��� �� 1ȸ�� �й����� �ʴ� ���
    /// </summary>
    DRUG_KING,

    /// <summary>
    /// �Ÿ� : ��� �ᳪ�� �÷��� �� 1ȸ�� �й����� �ʴ� ���
    /// </summary>
    GAINT_BEANSTALK,

    /// <summary>
    /// �ΰ����� : �Ʊ���������� �÷��� �� 1ȸ�� �й����� �ʴ� ���
    /// </summary>
    HUMAN_BRICK,

    /// <summary>
    /// ���� ������ �ε��� ��ȣ�Դϴ�.
    /// </summary>
    NONE,

}

public enum CUTSCENE_INDEX
{ 
    FIST_OPEN_GAME,

    OPEN_SNOW_WHITE_CHAPTER,

    SUCCESS_SNOW_WHITE_CHAPTER,

    OPEN_CINDERELLA_CHAPTER,

    SUCCESS_CINDERELLA_CHAPTER,

    OPEN_PINOCCHIO_CHAPTER,

    SUCCESS_PINOCCHIO_CHAPTER,

    OEPN_LITTLE_MERMAID_CHAPTER,

    SUCCESS_LITTLE_MERMAID_CHAPTER,

    OPEN_JACK_AND_BEANSTALK_CHAPTER,

    SUCCESS_JACK_AND_BEANSTALK_CHAPTER,

    OPEN_TREE_LITTLE_PIGS_CHAPTER,

    SUCCESS_TREE_LITTLE_PIGS_CHAPTER,

    LAST_CLEAR,

    NONE

}
