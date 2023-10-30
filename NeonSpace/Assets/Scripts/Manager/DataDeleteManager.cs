using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DataDeleteManager
{
    [MenuItem("GameUtility/Clear All SaveData")]
    static void ClearAllSaveData()
    {
        if (EditorUtility.DisplayDialog("게임 세이브 정보 삭제",
            "정말 삭제 하시겠습니까?", "네", "아니오"))
        {
            Debug.Log("삭제 완료");
             PlayerPrefs.DeleteAll();
        }
    }
}