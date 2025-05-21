using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGroup_Script : MonoBehaviour
{
    [SerializeField] private int nNoteMaxNum = 4;
    [SerializeField] private GameObject gNotePrefab = null;
    [SerializeField] private List<Transform> notePosList = null;
    private List<Note_Script> noteList = null;

    #region Unity Methods
    void Awake()
    {
        noteList = new List<Note_Script>();
    }
    void Start()
    {
        Init();
    }
    #endregion

    #region Custom Methods
    // 노트 그룹 초기화
    private void Init()
    {
        for (int i = 0; i < nNoteMaxNum; i++)
        {
            GameObject gNoteObj = GameObject.Instantiate(gNotePrefab);

            noteList.Add(gNoteObj.GetComponent<Note_Script>());
            noteList[i].SetNotePos(notePosList[i]);
        }
    }
    // 위치(index)에 해당하는 노트의 속성 반환 메서드
    public NoteType GetNoteType(int index)
    {
        if (index < 0 || index >= nNoteMaxNum)
        {
            Debug.LogError("Index out of range");
            return NoteType.Apple;
        }
        return noteList[index].GetNoteType();
    }
    // 노트 처리 메서드
    public void NoteProcess()
    {
        Destroy(noteList[0].gameObject);
        noteList.RemoveAt(0);
        GameObject gNoteObj = GameObject.Instantiate(gNotePrefab);
        noteList.Add(gNoteObj.GetComponent<Note_Script>());
        
        for (int i = 0; i < nNoteMaxNum; i++)
            noteList[i].SetNotePos(notePosList[i]);
    }
    #endregion
}
