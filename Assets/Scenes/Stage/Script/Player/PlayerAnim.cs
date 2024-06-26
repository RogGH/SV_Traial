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

    // 基本は左向き
    float defaultDir = 1;

    void AnimInit()
    {
        animator = GetComponent<Animator>();

        // 現状は０番のみ右向きに
        defaultDir = 1;
    }
}
