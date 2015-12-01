using UnityEngine;
using UnityEngine.UI;

public class RoundPopup : MonoBehaviour
{
    public RectTransform roundRect;
    public RectTransform numberRect;
    public RectTransform fightRect;

    private Image roundImg;
    private Image numberImg;
    private Image fightImg;

    public Vector2 numberSize = new Vector2(125, 125);
    public float numberShrinkSpeed = 3;
    public float shrinkDeadzone = 2;
    public float fadeInSpeed = 5;
    public float fadeSpeed1 = 4;
    public float fadeSpeed2 = 10;
    public float fightSpeed = 10;
    public float fightDeadzone = .2f;

    public Sprite[] numbers;

    void Start()
    {
        roundImg = roundRect.GetComponent<Image>();
        numberImg = numberRect.GetComponent<Image>();
        fightImg = fightRect.GetComponent<Image>();

        numberImg.sprite = numbers[RoundManager.roundNumber];
    }
    
    void Update()
    {
        //number shrink
        numberRect.sizeDelta = Vector2.Lerp(numberRect.sizeDelta, numberSize, numberShrinkSpeed * Time.deltaTime);
        
        if (Mathf.Abs(numberRect.sizeDelta.x - numberSize.x) <= shrinkDeadzone)
        {
            //fade out round and number
            roundImg.color = Color.Lerp(roundImg.color, Color.clear, fadeSpeed1 * Time.deltaTime);
            numberImg.color = Color.Lerp(numberImg.color, Color.clear, fadeSpeed1 * Time.deltaTime);

            //fight slide in
            fightRect.anchoredPosition = Vector2.Lerp(fightRect.anchoredPosition, Vector2.zero, fightSpeed * Time.deltaTime);

            if (fightRect.anchoredPosition.x <= fightDeadzone)
            {
                //fight fade out
                fightImg.color = Color.Lerp(fightImg.color, Color.clear, fadeSpeed2 * Time.deltaTime);

                if (fightImg.color.a <= .1f)
                {
                    //fire off round
                    if (RoundManager.roundNumber < numbers.Length - 1)
                        RoundManager.roundNumber++;
                    else
                        RoundManager.roundNumber = 0;
                    
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            roundImg.color = Color.Lerp(roundImg.color, Color.white, fadeInSpeed * Time.deltaTime);
        }
    }

    
}
