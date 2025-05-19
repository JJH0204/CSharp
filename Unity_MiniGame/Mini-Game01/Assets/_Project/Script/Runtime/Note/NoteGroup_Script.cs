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
        for (int i = 0; i < nNoteMaxNum; i++)
        {
            GameObject gNoteObj = GameObject.Instantiate(gNotePrefab);

            noteList.Add(gNoteObj.GetComponent<Note_Script>());
        }

        for (int i = 0; i < nNoteMaxNum; i++)
        {
            noteList[i].SetNotePos(notePosList[i]);
            // 랜덤으로 노트 타입을 설정
            // 0 : APPLE, 1 : GOLDAPPLE, 2 : ROTTENAPPLE
            int randomNum = Random.Range(0, 3);
            switch (randomNum)
            {
                case 0:
                    noteList[i].SetNoteType(NOTE_TYPE.APPLE);
                    break;
                case 1:
                    noteList[i].SetNoteType(NOTE_TYPE.GOLDAPPLE);
                    break;
                case 2:
                    noteList[i].SetNoteType(NOTE_TYPE.ROTTENAPPLE);
                    break;
            }
            // noteList[i].SetNoteType(Random.Range(0, 3) == 0 ? NOTE_TYPE.APPLE : NOTE_TYPE.BLUEBARRY);
        }
    }
    #endregion

}
