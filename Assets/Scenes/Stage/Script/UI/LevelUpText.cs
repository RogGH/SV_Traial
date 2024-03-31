using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpText : MonoBehaviour
{
    public float colorChangeInterval = 1f; // �F���ς��Ԋu�i�b�j
    public Color[] colors; // �A�j���[�V�����Ŏg�p����F�̔z��

    private Text textComponent;
    private int currentIndex = 0;

    float timer = 0;


    void Start()
    {
        textComponent = GetComponent<Text>();

        // �����̐F��ݒ�
        textComponent.color = colors[currentIndex];
    }

    void Update()
    {
        timer += (1.0f / 60.0f );
        if (timer >colorChangeInterval ) {
            timer = 0;
            ChangeColor();
        }
    }

    void ChangeColor()
    {
        // ���̐F�ւ̃C���f�b�N�X���v�Z���A���[�v������
        currentIndex = (currentIndex + 1) % colors.Length;

        // �F��ύX
        textComponent.color = colors[currentIndex];
    }
}
