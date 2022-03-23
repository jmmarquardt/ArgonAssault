using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    const string SCORETEXT = "Score: ";
    int _score = 0;
    TextMeshProUGUI _scoreText;

    void Awake() => _scoreText = GetComponent<TextMeshProUGUI>();

    void Start () => ScoreTextUpdater();

    void Update () => ScoreTextUpdater();

    void ScoreTextUpdater() => _scoreText.text = SCORETEXT + _score.ToString();

    public void IncreaseScore(int value) => _score += value;
}