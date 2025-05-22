using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class NoteScript : MonoBehaviour
{
    #region Variables
    [FormerlySerializedAs("NoteSpriteList")] [SerializeField] private List<Sprite> noteSpriteList; // 노트 스프라이트 리스트
    [FormerlySerializedAs("ChildObj")] [SerializeField] private GameObject childObj; // Note의 자식 오브젝트
    private NoteType _noteType;    // Note의 타입
    #endregion
    #region Unity Methods
    private void Awake()
    {
        SetRandomNoteType();
    }

    public void SetRandomNoteType()
    {
        int randomNum = Random.Range(0, 11);
        if (randomNum < 5)
            SetNoteType(NoteType.RottenApple);
        else if (randomNum < 9)
            SetNoteType(NoteType.Apple);
        else
            SetNoteType(NoteType.GoldApple);
    }

    #endregion

    #region Custom Methods
    // 노트의 위치를 설정하는 메서드
    internal void SetNotePos(Transform tr)
    {
        this.transform.position = tr.position;
    }

    // 노트의 타입을 설정하는 메서드
    private void SetNoteType(NoteType type)
    {
        this._noteType = type;
        childObj.GetComponent<SpriteRenderer>().sprite = noteSpriteList[(int)type];
    }

    // 노트의 타입을 가져오는 메서드
    internal NoteType GetNoteType()
    {
        return _noteType;
    }
    #endregion
}
