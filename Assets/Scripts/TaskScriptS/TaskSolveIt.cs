using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskSolveIt : MonoBehaviour
    
{
    // References
    public Text addend1Text;
    public Text addend2Text;
    public Text inputText;
    public AudioSource beepClick;
    public AudioSource beepSuccess;

    // Private Values
    private int addend1;
    private int addend2;
    private int sum;
    private string input = "";

    // Start is called before the first frame update
    void Start()
    {
        addend1 = Random.Range(0, 100);
        addend2 = Random.Range(0, 100);
        sum = addend1 + addend2;
        addend1Text.text = addend1.ToString();
        addend2Text.text = addend2.ToString();
    }

    public void Submit()
    {
        if (int.Parse(input) == sum)
        {
            GetComponent<Collider>().isTrigger = true;
            beepSuccess.volume = UnityEngine.Random.Range(0.4f, 0.6f);
            beepSuccess.pitch = UnityEngine.Random.Range(0.8f, 0.9f);
            beepSuccess.Play();
        }
        else
        {
            ClearInput();
            playSound();
        }
    }

    private void StringInput(string num)
    {
        if (input.Length < 3)
        {
            input += num;
            inputText.text = input;
            playSound();
        }        
    }

    public void ClearInput()
    {
        input = "";
        inputText.text = input;
        playSound();
    }

    private void playSound()
    {
        beepClick.volume = UnityEngine.Random.Range(0.4f, 0.6f);
        beepClick.pitch = UnityEngine.Random.Range(0.8f, 0.9f);
        beepClick.Play();
    }

    public void num0() { StringInput("0"); }
    public void num1() { StringInput("1"); }
    public void num2() { StringInput("2"); }
    public void num3() { StringInput("3"); }
    public void num4() { StringInput("4"); }
    public void num5() { StringInput("5"); }
    public void num6() { StringInput("6"); }
    public void num7() { StringInput("7"); }
    public void num8() { StringInput("8"); }
    public void num9() { StringInput("9"); }


}
