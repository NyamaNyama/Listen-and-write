
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetAnswerButton : MonoBehaviour
{
    [SerializeField] private GameObject answerPanel;
    [SerializeField] private Button answerButtonPref;

    [SerializeField] private GameObject choicePanel;
    [SerializeField] private Button choiceButtonPref;

    private Button _currentButton;
    private Color _btnColor;

    private string _exampleResult;



    [SerializeField] ExampleValues exampleData;

    public void Init()
    {
        _exampleResult = GetComponent<ExampleOnList>().Result;
        _btnColor = answerButtonPref.GetComponent<Image>().color;
        CreateAnswerButtonPanel();
        CreateButtonChoicePanel();
    }

    private void CreateAnswerButtonPanel()
    {
        
        for (int i = 0; i < _exampleResult.Length; i++)
        {
            
            Button btn = Instantiate(answerButtonPref, answerPanel.transform);
            AddAnswerButtonListeners(btn);
        }
    }

    private void AddAnswerButtonListeners(Button btn)
    {
        btn.onClick.AddListener(() => ActivatePanel(btn));
        btn.onClick.AddListener(() => SetLight(btn));
        btn.onClick.AddListener(() => SetCurrencyButton(btn));
 
    }

    private void ActivatePanel(Button btn)
    {
        choicePanel.SetActive(true);
        InteractionManager.ActivePanel = choicePanel;
    }
    private void ChoiceSpriteToAnswerButton(Button choiceButton)
    {
        _currentButton.GetComponent<Image>().sprite = choiceButton.GetComponent<Image>().sprite;
    }
    private void SetCurrencyButton(Button btn)
    {
        if(_currentButton != null)
        {
            SetOld(_currentButton);
        }
        _currentButton = btn;
    }

    public void SetLight(Button btn)
    {
        btn.image.color = _btnColor * 1.5f;
    }

    public void SetOld(Button btn)
    {
        btn.image.color = _btnColor;

    }



    private void CreateButtonChoicePanel()
    {

        for (int i = 0; i < exampleData.GetLength(); i++)
        {
            Button btn = Instantiate(choiceButtonPref, choicePanel.transform);
            btn.onClick.AddListener(() => ChoiceSpriteToAnswerButton(btn));
            if (i < exampleData.Numbers.Count)
            {
                btn.image.sprite = exampleData.NumbersSprite[i];
            }
            else
            {
                btn.image.sprite = exampleData.OperatorsSprite[i % exampleData.Numbers.Count];
            }
        }
        choicePanel.SetActive(false);
    }
}
