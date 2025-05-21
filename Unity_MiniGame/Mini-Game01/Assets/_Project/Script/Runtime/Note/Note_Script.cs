// using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note_Script : MonoBehaviour
{
    #region Variables
    [SerializeField] private List<Sprite> NoteSpriteList = null; // 노트 스프라이트 리스트
    [SerializeField] private GameObject ChildObj = null; // Note의 자식 오브젝트
    [SerializeField] private NoteType noteType;    // Note의 타입
    #endregion
    #region Unity Methods
    private void Awake()
    {
        int RandomNum = Random.Range(0, 11);
        if (RandomNum < 5)
            SetNoteType(NoteType.RottenApple);
        else if (RandomNum < 9)
            SetNoteType(NoteType.Apple);
        else
            SetNoteType(NoteType.GoldApple);
        // SetNoteType((NOTE_TYPE)Random.Range(0, 3));
    }
    #endregion

    #region Custom Methods
    // 노트의 위치를 설정하는 메서드
    internal void SetNotePos(Transform transform)
    {
        this.transform.position = transform.position;
    }

    // 노트의 타입을 설정하는 메서드
    internal void SetNoteType(NoteType noteType)
    {
        this.noteType = noteType;
        ChildObj.GetComponent<SpriteRenderer>().sprite = NoteSpriteList[(int)noteType];
    }

    // 노트의 타입을 가져오는 메서드
    internal NoteType GetNoteType()
    {
        return noteType;
    }
    #endregion
}
