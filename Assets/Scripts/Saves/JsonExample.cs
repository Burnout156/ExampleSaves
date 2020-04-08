using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public struct SaveStructure
{
    public PlayerStatus player;
    public bool isAutoSave;
}

public class JsonExample : MonoBehaviour
{
    public static SaveStructure MainSave;
    public static string filePath;
    public static string folderPath;
    public static string data;

    private void Awake()
    {        
        folderPath = Application.dataPath + "/LocalSave"; //caminho da pasta
        filePath = Application.dataPath + "/LocalSave" + "/" + "Save" + ".json"; //caminho do arquivo       
    }

    void Start()
    {

    }

    public void Save(Player player)
    {
        if (!Directory.Exists(folderPath)) //se a pasta do save não existir, será criada uma nova pasta
        {
            Directory.CreateDirectory(folderPath);
        }

        if (!File.Exists(filePath)) //se o arquivo de save não existir, será criado 
        {
            MainSave.player = new PlayerStatus(player.coins);
        }

        string json = JsonUtility.ToJson(MainSave); //passar uma estrutura com variáveis para uma string
        File.WriteAllText(filePath, json);
        print("Arquivo salvo com sucesso: " + json);
        GameObject.FindObjectOfType<Player>().UpdateCoinText();
    }

    public static void AutoSaving()
    {
        if (!Directory.Exists(folderPath)) //se a pasta do save não existir, será criada uma nova pasta
        {
            Directory.CreateDirectory(folderPath);
        }

        if (!File.Exists(filePath)) //se o arquivo de save não existir, será criado
        {
            MainSave.player = new PlayerStatus(GameObject.FindObjectOfType<Player>().coins);
        }

        string json = JsonUtility.ToJson(MainSave); //passar uma estrutura com variáveis para uma string
        File.WriteAllText(filePath, json);
    }

    public static void AutoLoading() //também pode funcionar como um auto carregamento, apenas precisando colocar no Start
    {
        if (File.Exists(filePath))
        {
            data = File.ReadAllText(filePath);
            MainSave = JsonUtility.FromJson<SaveStructure>(data);
            print("Arquivo lido com sucesso: " + data);
            GameObject.FindObjectOfType<Player>().coins = MainSave.player.coins;
            GameObject.FindObjectOfType<Player>().UpdateCoinText();
        }

        else
        {
            print("Arquivo não existe: " + data);
        }
    }

    public void Load() //também pode funcionar como um auto carregamento, apenas precisando colocar no Start
    {
        if (File.Exists(filePath))
        {
            data = File.ReadAllText(filePath);
            MainSave = JsonUtility.FromJson<SaveStructure>(data);
            print("Arquivo lido com sucesso: " + data);
            GameObject.FindObjectOfType<Player>().coins = MainSave.player.coins;
            GameObject.FindObjectOfType<Player>().UpdateCoinText();
        }

        else
        {
            print("Arquivo não existe: " + data);
        }
    }

    public void Delete()
    {
        if (Directory.Exists(folderPath)) //se a pasta do save existir, será deletada
        {
            Directory.Delete(folderPath, true);
            GameObject.FindObjectOfType<Player>().UpdateCoinText();
            print("Arquivo deletado com sucesso");
        }

        else
        {
            print("File Not Exist");
        }
    }
}
