using UnityEngine;
using System;

public class DelegatesPractice : MonoBehaviour
{
    Action<int> example; //delegate which points to a method

    Action exampleTwo;

    Func<int> exampleFunction;

        int exampleThree()
        {
            return 1;
        }
        void ExampleTwo()
        {
            
        }

        void ExampleFour()
        {
            
        }
    void ActionExample(int a)
    {
        Debug.Log("Action called");
    }

    private void Awake()
    {
        example = ActionExample; //reference to function
        exampleTwo += ExampleTwo;
        exampleTwo += ExampleFour;
        exampleFunction = exampleThree;

        example.Invoke(1);
        exampleTwo.Invoke();
    }
}
