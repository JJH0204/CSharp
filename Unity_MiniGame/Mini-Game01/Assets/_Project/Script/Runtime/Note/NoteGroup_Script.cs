using System.Collections.Generic;
using UnityEngine;

public class NoteGroupScript : MonoBehaviour
{
    [SerializeField] private int nNoteMaxNum = 4;
    [SerializeField] private GameObject gNotePrefab;
    [SerializeField] private List<Transform> notePosList;
    private List<NoteScript> _noteList;

    #region Unity Methods
    void Awake()
    {
        _noteList = new List<NoteScript>();
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
            var gNoteObj = Instantiate(gNotePrefab, transform);

            _noteList.Add(gNoteObj.GetComponent<NoteScript>());
            _noteList[i].SetNotePos(notePosList[i]);
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
        return _noteList[index].GetNoteType();
    }
    // 노트 처리 메서드
    public void NoteProcess()
    {
        Destroy(_noteList[0].gameObject);
        _noteList.RemoveAt(0);
        GameObject gNoteObj = GameObject.Instantiate(gNotePrefab);
        _noteList.Add(gNoteObj.GetComponent<NoteScript>());
        
        for (int i = 0; i < nNoteMaxNum; i++)
            _noteList[i].SetNotePos(notePosList[i]);
    }
    // 노트 그룹 리로드 메서드
    public void ReLoad()
    {
        foreach (var note in _noteList)
            note.SetRandomNoteType();
    }
    #endregion
}
