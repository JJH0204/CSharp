using System;
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
    #endregion


}
