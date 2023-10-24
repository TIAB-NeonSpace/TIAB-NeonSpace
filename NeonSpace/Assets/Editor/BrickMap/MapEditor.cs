using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Reflection;



public class BlockShow { public BlockTypes type_; }

public class MapEditor : EditorWindow
{
    static MapEditor window;
    int maxRows = 9;
    int maxCols = 9;
    int levels = 1;
    int stages = 1;
    private string fileName = "1-1.txt";
    BlockTypes b_Types;
    Texture blankTx, normalTx, shieldTx;
    Vector2 scrollViewVector;
    TextureCover cover_;

    Texture[] arrayTex;


    public static BlockShow[] stageBlocks = new BlockShow[81];

    [MenuItem("Window/Map Edior")]
    public static void Init()
    {
        window = (MapEditor)GetWindow(typeof(MapEditor));
        window.Show();
    }
    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Save", new GUILayoutOption[] { GUILayout.Width(40), GUILayout.Height(40) })) SaveLevel();
        if (GUILayout.Button("Clear", new GUILayoutOption[] { GUILayout.Width(40), GUILayout.Height(40) })) ClearLavel();
        GUILayout.EndHorizontal();
        GUILayout.Space(-30);
        scrollViewVector = GUI.BeginScrollView(new Rect(25, 45, position.width - 30, position.height), scrollViewVector, new Rect(0, 0, 400, 1600));
        GUILayout.Label("Stage editor", EditorStyles.boldLabel, new GUILayoutOption[] { GUILayout.Width(150) });
        GUILevelSelector();
        GUIBlocks();
        GameField();
        GUI.EndScrollView();

    }

    private void OnFocus()
    {
        Initialize();
        LoadDataFromLocal(stages);
        ChangeTexture();
      
    }

    void ChangeTexture()
    {
        if (SceneManager.GetActiveScene().name == "MapEditor")
        {
            if (cover_ == null) cover_ = GameObject.Find("TextureCover").GetComponent<TextureCover>();
            arrayTex = new Texture[cover_.tex_.Length];
            arrayTex = cover_.tex_;
            
            blankTx = arrayTex[0];
            normalTx = arrayTex[1];
            shieldTx = arrayTex[2];

        }
        else Debug.LogError("PLZ MapEditor Scene ");
    }

    void Initialize()
    {
        stageBlocks = new BlockShow[maxCols * maxRows];
        for (int i = 0; i < stageBlocks.Length; i++)
        {
            BlockShow sqBlocks = new BlockShow();
            sqBlocks.type_ = BlockTypes.Balank;
            stageBlocks[i] = sqBlocks;
        }
    }

    void SaveLevel()
    {
        if (!fileName.Contains(".txt"))
            fileName += ".txt";
        SaveMap(fileName);
    }

    public void SaveMap(string fileName)
    {
        string saveString = "";

        //set map data
        for (int row = 0; row < maxRows; row++)
        {
            for (int col = 0; col < maxCols; col++)
            {
                saveString += (int)stageBlocks[row * maxCols + col].type_;
                //if this column not yet end of row, add space between them
                if (col < (maxCols - 1))
                    saveString += " ";
            }
            //if this row is not yet end of row, add new line symbol between rows
            if (row < (maxRows - 1))
                saveString += "\r\n";
        }
        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
        {
            //Write to file
            int writecnt;
            writecnt = stages * 10000;
            writecnt += levels;
            string activeDir = Application.dataPath + @"/Resources/Levels/";
            string newPath = System.IO.Path.Combine(activeDir, writecnt + ".txt");
            StreamWriter sw = new StreamWriter(newPath);
            sw.Write(saveString);
            sw.Close();
        }
        AssetDatabase.Refresh();
    }
    string nIntField;
    void GUILevelSelector()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Stage:", EditorStyles.boldLabel, new GUILayoutOption[] { GUILayout.Width(50) });
        GUILayout.Space(10);
        if (GUILayout.Button("<<", new GUILayoutOption[] { GUILayout.Width(50) })) PreviousStage();
      
       
        string changestage = GUILayout.TextField(stages.ToString(), new GUILayoutOption[] { GUILayout.Width(50) });
        try
        {
            if (int.Parse(changestage) != stages)
            {
                stages = int.Parse(changestage);
                if (stages < 1) stages = 1;
                if (!LoadDataFromLocal(stages))
                {
                    ClearLavel();
                    return;
                }
                ChangeTexture();
                LoadDataFromLocal(stages);
            }
        }
        catch (Exception) { throw; }

        if (GUILayout.Button(">>", new GUILayoutOption[] { GUILayout.Width(50) })) NextStage();

        GUILayout.Label("Level:", EditorStyles.boldLabel, new GUILayoutOption[] { GUILayout.Width(50) });
        GUILayout.Space(10);
        if (GUILayout.Button("<<", new GUILayoutOption[] { GUILayout.Width(50) })) PreviousLevel();
        string changeLvl = GUILayout.TextField(levels.ToString(), new GUILayoutOption[] { GUILayout.Width(50) });
        try
        {
              levels = int.Parse(changeLvl);
            if (int.Parse(changeLvl) != levels)
            {
                stages = int.Parse(changestage);
                if (stages < 1) stages = 1;
                if (!LoadDataFromLocal(stages))
                {
                    ClearLavel();
                    return;
                }
                ChangeTexture();
                LoadDataFromLocal(stages);


            }
        }
        catch (Exception) { throw; }

        if (GUILayout.Button(">>", new GUILayoutOption[] { GUILayout.Width(50) })) NextLevel();

        GUILayout.Label("Assets/Resouces/Levels/", EditorStyles.label, new GUILayoutOption[] { GUILayout.Width(200) });
        GUILayout.EndHorizontal();
    }

    void NextLevel()
    {
        ++levels;
        LoadDataFromLocal(stages);
        if (!LoadDataFromLocal(stages))
        {
            ClearLavel();
            return;
        }
    }

    void NextStage()
    {
        ++stages;
        if (!LoadDataFromLocal(stages))
        {
            ClearLavel();
            return;
        }
        ChangeTexture();
        LoadDataFromLocal(stages);
    }

    void PreviousLevel()
    {
        --levels;
        if (levels < 1)
            levels = 1;
        if (!LoadDataFromLocal(stages))
        {
            ClearLavel();
            return;
        }
        LoadDataFromLocal(stages);
    }

    void PreviousStage()
    {
        --stages;
        if (stages < 1) stages = 1;
        if (!LoadDataFromLocal(stages))
        {
            ClearLavel();
            return;
        }
        ChangeTexture();
        LoadDataFromLocal(stages);
    }

    void ClearLavel()
    {
        for (int i = 0; i < stageBlocks.Length; i++)
        {
            stageBlocks[i].type_ = BlockTypes.Balank;
        }
    }

    public bool LoadDataFromLocal(int currentStage)
    {
        currentStage *= 10000;
        currentStage += levels;
        ClearLogConsole();
        Debug.LogError(currentStage);
        //Read data from text file
        TextAsset mapText = Resources.Load("Levels/" + currentStage) as TextAsset;
        if (mapText == null)
        {
            return false;
        }
        ProcessGameDataFromString(mapText.text);
        return true;
    }

    void ProcessGameDataFromString(string mapText)
    {
        string[] lines = mapText.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

        int mapLine = 0;
        foreach (string line in lines)
        {
            string[] st = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < st.Length; i++)
            {
                stageBlocks[mapLine * maxCols + i].type_ = (BlockTypes)int.Parse(st[i].ToString());
            }
            ++mapLine;
        }
    }
 

    void GUIBlocks()
    {
        GUILayout.Label("Map Block:", EditorStyles.boldLabel);
        GUILayout.Label("Utils", EditorStyles.boldLabel);

        GUILayout.BeginHorizontal();
        GUI.color = new Color(1, 1, 1, 1f);
        if (GUILayout.Button(blankTx, new GUILayoutOption[] { GUILayout.Width(30), GUILayout.Height(30) }))
        {
            b_Types = BlockTypes.Balank;
        }
        GUILayout.Label(" - Blank", EditorStyles.boldLabel);

        if (GUILayout.Button(normalTx, new GUILayoutOption[] { GUILayout.Width(30), GUILayout.Height(30) }))
        {
            b_Types = BlockTypes.Normal;
        }
        GUILayout.Label(" - Normal", EditorStyles.boldLabel);

        if (GUILayout.Button(shieldTx, new GUILayoutOption[] { GUILayout.Width(30), GUILayout.Height(30) }))
        {
            b_Types = BlockTypes.Shield;
        }
        GUILayout.Label(" - Shield", EditorStyles.boldLabel);



        GUILayout.EndHorizontal();
        GUILayout.Space(30);
    }

    void GameField()
    {
        GUILayout.BeginVertical();
        for (int row = 0; row < maxRows; ++row)
        {
            GUILayout.BeginHorizontal();

            for (int col = 0; col < maxCols; ++col)
            {
                var imageButton = new object();
                switch(stageBlocks[row * maxCols + col].type_)
                {
                    case BlockTypes.Balank:
                        imageButton = blankTx;
                        break;
                    case BlockTypes.Normal:
                        imageButton = normalTx;
                        break;
                    case BlockTypes.Shield:
                        imageButton = shieldTx;
                        break;
                }
                
                GUI.color = new Color(1, 1, 1, 1f);
                if (GUILayout.Button(imageButton as Texture, new GUILayoutOption[] {
                        GUILayout.Width (30),
                        GUILayout.Height (30)
                    }))
                {
                    SetType(col, row);
                }
            }
            GUILayout.EndVertical();
        }
    }

    void SetType(int col , int row)
    {
        Debug.LogError(col + " " + row);
        stageBlocks[row * maxCols + col].type_ = b_Types;
    }

    public static void ClearLogConsole()
    {
        Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
        Type logEntries = assembly.GetType("UnityEditorInternal.LogEntries");
        MethodInfo clearConsoleMethod = logEntries.GetMethod("Clear");
        clearConsoleMethod.Invoke(new object(), null);
    }
}