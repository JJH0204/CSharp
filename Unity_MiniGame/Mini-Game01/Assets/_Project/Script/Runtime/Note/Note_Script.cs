// using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note_Script : MonoBehaviour
{
    #region Variables
    [SerializeField] private List<Sprite> NoteSpriteList = null; // 노트 스프라이트 리스트
    [SerializeField] private GameObject ChildObj = null; // Note의 자식 오브젝트
    [SerializeField] private NOTE_TYPE noteType;    // Note의 타입
    #endregion
    #region Unity Methods
    private void Awake()
    {
        // 랜덤으로 노트 타입을 설정
        // 0 : APPLE, 1 : GOLDAPPLE, 2 : ROTTENAPPLE
        // int randomNum = Random.Range(0, 3);
        // switch (randomNum)
        // {
        //     case 0:
        //         SetNoteType(NOTE_TYPE.APPLE);
        //         break;
        //     case 1:
        //         SetNoteType(NOTE_TYPE.GOLDAPPLE);
        //         break;
        //     case 2:
        //         SetNoteType(NOTE_TYPE.ROTTENAPPLE);
        //         break;
        // }
        SetNoteType((NOTE_TYPE)Random.Range(0, 3));
    }
    #endregion

    #region Custom Methods
    // 노트의 위치를 설정하는 메서드
    internal void SetNotePos(Transform transform)
    {
        this.transform.position = transform.position;
    }

    // 노트의 타입을 설정하는 메서드
    internal void SetNoteType(NOTE_TYPE noteType)
    {
        this.noteType = noteType;
        ChildObj.GetComponent<SpriteRenderer>().sprite = NoteSpriteList[(int)noteType];
    }

    // 노트의 타입을 가져오는 메서드
    internal NOTE_TYPE GetNoteType()
    {
        return noteType;
    }
    #endregion
}
