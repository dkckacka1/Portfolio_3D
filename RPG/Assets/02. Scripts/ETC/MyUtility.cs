using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���ӳ� ���Ǵ� ��ƿ��Ƽ �Լ����� ��Ƴ��� ������
 */

public static class MyUtility
{
    // Ư�� �ؽ�Ʈ�� �� �������ִ� �±׷� �����ݴϴ�.
    public static string returnColorText(string text, Color color)
    {
        return $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{text}</color>"; ;
    }

    // Ư�� �ؽ�Ʈ�� ���� ����� �������ִ� �±׷� �����ݴϴ�.
    public static string returnAlignmentText(string text, alignmentType type)
    {
        return $"<align=\"{type}\">{text}</align>"; ;
    }

    // �ؽ�Ʈ�� �¿� ������ �� �ֵ��� �±׷� �����ݴϴ�.
    public static string returnSideText(string leftText, string rightText)
    {
        return $"<align=left>{leftText}<line-height=0>\n<align=right>{rightText}<line-height=1em></align>";
    }

    /// <summary>
    /// ���� Ȯ�� ����
    /// </summary>
    /// <param name="successPoint">���� Ȯ��(successPoint�� ������ ����)</param>
    /// <param name="minPoint">�ּ� ������</param>
    /// <param name="maxPoint">�ִ� ������(incluesive)</param>
    /// <returns>true �� ���� flase �� ����</returns>
    public static bool ProbailityCalc(float successPoint, float minPoint, float maxPoint)
    {
        float random = Random.Range(minPoint, maxPoint);

        return (random > successPoint);
    }
}
