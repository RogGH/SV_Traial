using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    Animator animator;

    enum AnimNo {
        Idle, Move,
    };
    AnimNo animNo;
    string[] nameTbl = {
        "Idle", "Move",
    };

    // ��{�͍�����
    float defaultDir = 1;

    void AnimInit()
    {
        animator = GetComponent<Animator>();

        // ����͂O�Ԃ̂݉E������
        defaultDir = 1;
    }
}
