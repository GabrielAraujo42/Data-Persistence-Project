[System.Serializable]
public class HighScoreData
{
    public string name;
    public int score;

    public HighScoreData()
    {
        this.name = "";
        this.score = 0;
    }

    public HighScoreData(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}