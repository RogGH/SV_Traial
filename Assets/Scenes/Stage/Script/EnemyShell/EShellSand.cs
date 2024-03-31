using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EShellSand : MonoBehaviour
{
    SpriteRenderer spComp;

    void Start()
    {
        spComp = GetComponent<SpriteRenderer>();
        SeManager.Instance.Play("Sand");
    }

    // Update is called once per frame
    void Update()
    {
        // ���݂̍��W����A�\���D������肳���Ă݂�
        spComp.sortingOrder = IObject.GetSpriteOrder(transform.position);
    }


    public void HitEnd()
    {
        GetComponent<HitBase>().SetAtkActive(false);
    }

    public void AnimEnd()
    {
        Destroy(gameObject);
    }

}
