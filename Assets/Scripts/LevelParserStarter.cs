using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelParserStarter : MonoBehaviour
{
    public string filename;

    public GameObject Rock;

    public GameObject Brick;

    public GameObject QuestionBox;

    public GameObject Stone;

    public GameObject Player;

    public Transform parentTransform;
    // Start is called before the first frame update
    void Start()
    {
        RefreshParse();
    }


    private void FileParser()
    {
        string fileToParse = string.Format("{0}{1}{2}.txt", Application.dataPath, "/Resources/", filename);

        Stack<string> lines = new Stack<string>();
        
        using (StreamReader sr = new StreamReader(fileToParse))
        {
            string line = "";
            
            while ((line = sr.ReadLine()) != null)
            {
                lines.Push(line);
            }

            sr.Close();
        }
        
        int row = 0;

        while (lines.Count > 0)
        {
            int column = 0;
            char[] letters = lines.Peek().ToCharArray();
            
            foreach (var letter in letters)
            {
                //Call SpawnPrefab
                SpawnPrefab(letter, new Vector3(column++, row - 14, 0f));
            }

            row++;
            lines.Pop();
        }

        var player = GameObject.Instantiate(Player, parentTransform);
        player.transform.position = new Vector3(4, 4, 0);
    }

    private void SpawnPrefab(char spot, Vector3 positionToSpawn)
    {
        GameObject ToSpawn;

        switch (spot)
        {
            case 'b':
            {
                ToSpawn = Brick;
                // Debug.Log("Spawn Brick");
                break;
            }
            case '?':
            {
                ToSpawn = QuestionBox;
                // Debug.Log("Spawn QuestionBox"); 
                break;
            }
            case 'x':
            {
                ToSpawn = Rock;
                // Debug.Log("Spawn Rock");
                break;
            }
            case 's':
            {
                ToSpawn = Stone;
                // Debug.Log("Spawn Stone");
                break;
            }
            default: return;
        }

        ToSpawn = GameObject.Instantiate(ToSpawn, parentTransform);
        ToSpawn.transform.position += positionToSpawn;
    }

    public void RefreshParse()
    {
        GameObject newParent = new GameObject();
        newParent.name = "Environment";
        newParent.transform.position = parentTransform.position;
        newParent.transform.parent = this.transform;
        
        if (parentTransform) Destroy(parentTransform.gameObject);

        parentTransform = newParent.transform;
        FileParser();
    }
}
