using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public int Score => Kills + (2*Headshots);
    public string ScoreReadout => $"Score: {Score} ({Kills}/{Headshots}H)";
    public int Kills;
    public int Headshots;
    public Text ScoreText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReportKill(bool wasHeadshot)
    {
        Kills++;
        if (wasHeadshot)
            Headshots++;
        ScoreText.text = ScoreReadout;
    }


}
