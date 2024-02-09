using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	public static ScoreManager Instance;

	public int score = 0;

	private TextMeshProUGUI scoreText;
	public GameObject scoreTextObject;
	public GameObject winObject;
	public int winCount = 10;
	void Awake()
	{
		scoreText = scoreTextObject.GetComponent<TextMeshProUGUI>();
		winObject.SetActive(false);

		// if (Instance != null && Instance != this)
		// {
		// 	Destroy(gameObject);
		// }
		// else
		// {
		Instance = this;
		// }
	}

	void Start()
	{
		UpdateScoreText();
	}

	public void AddScore(int amount)
	{
		score += amount;
		UpdateScoreText();
	}

	void UpdateScoreText()
	{
		if (scoreText != null)
			scoreText.text = "Count: " + score;
		if (score >= winCount)
		{
			winObject.SetActive(true);
		}
	}
}
