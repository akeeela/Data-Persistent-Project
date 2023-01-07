using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    
    public string playerName;
    public int highScore;
    public string bestPlayerName;
    [SerializeField] private TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        LoadHighScore();
        DontDestroyOnLoad(gameObject);
    }
    
    public void StartGame()
    {
        playerName = scoreText.text;
        SceneManager.LoadScene("main");
    }
    
    class SaveData
    {
        public int highScore;
        public string bestPlayerName;
    }
    
    public void SaveHighScore(int score, string playerName)
    {
        SaveData data = new SaveData();
        data.highScore = score;
        data.bestPlayerName = playerName;

        string json = JsonUtility.ToJson(data);
        
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScore = data.highScore;
            bestPlayerName = data.bestPlayerName;
        }
    }
}
