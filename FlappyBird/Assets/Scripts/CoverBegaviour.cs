using UnityEngine;
using UnityEngine.UI;

public class CoverBegaviour : MonoBehaviour
{
    public Image Mask;
    public Text GameStartTxt;
    public Text ScoreTxt;
    public Text GameOverTxt;

    // Start is called before the first frame update
    void Start()
    {
        ScoreTxt.text = $"Score : {GameController.Instance.Score}";
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.IsPlay)
        {
            Mask.gameObject.SetActive(false);
            GameStartTxt.gameObject.SetActive(false);
            ScoreTxt.text = $"Score : {GameController.Instance.Score}";
        }
        else if (GameController.Instance.IsOver)
        {
            GameOverTxt.gameObject.SetActive(true);
            Mask.gameObject.SetActive(true);
        }
    }
}
