﻿/* Scripted by Omabu - omabuarts@gmail.com */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuirkySeriesDemo : MonoBehaviour
{
    [Space(10)] private Transform animal_parent;

    private int animalIndex;

    private GameObject[] animals;

    private readonly List<string> animationList = new()
    {
        "Attack",
        "Bounce",
        "Clicked",
        "Death",
        "Eat",
        "Fear",
        "Fly",
        "Hit",
        "Idle_A", "Idle_B", "Idle_C",
        "Jump",
        "Roll",
        "Run",
        "Sit",
        "Spin/Splash",
        "Swim",
        "Walk"
    };

    private Dropdown dropdownAnimal;
    private Dropdown dropdownAnimation;
    private Dropdown dropdownShapekey;

    private readonly List<string> shapekeyList = new()
    {
        "Eyes_Annoyed",
        "Eyes_Blink",
        "Eyes_Cry",
        "Eyes_Dead",
        "Eyes_Excited",
        "Eyes_Happy",
        "Eyes_LookDown",
        "Eyes_LookIn",
        "Eyes_LookOut",
        "Eyes_LookUp",
        "Eyes_Rabid",
        "Eyes_Sad",
        "Eyes_Shrink",
        "Eyes_Sleep",
        "Eyes_Spin",
        "Eyes_Squint",
        "Eyes_Trauma",
        "Sweat_L",
        "Sweat_R",
        "Teardrop_L",
        "Teardrop_R"
    };

    private void Start()
    {
        animal_parent = GameObject.Find("Animals").transform;

        var canvas = GameObject.Find("Canvas").transform;
        /* Hierarchy reference:
         ** Canvas
         ** 		Animal
         ** 			Dropdown
         **			Next
         **			Prev
         ** 		Animation
         ** 			Dropdown
         **			Next
         **			Prev
         **		...
         */
        dropdownAnimal = canvas.Find("Animal").Find("Dropdown").GetComponent<Dropdown>();
        dropdownAnimation = canvas.Find("Animation").Find("Dropdown").GetComponent<Dropdown>();
        dropdownShapekey = canvas.Find("Shapekey").Find("Dropdown").GetComponent<Dropdown>();


        var count = animal_parent.childCount;
        animals = new GameObject[count];
        var animalList = new List<string>();

        for (var i = 0; i < count; i++)
        {
            animals[i] = animal_parent.GetChild(i).gameObject;
            var n = animal_parent.GetChild(i).name;
            animalList.Add(n);
            // animalList.Add(n.Substring(0, n.IndexOf("_")));

            if (i == 0) animals[i].SetActive(true);
            else animals[i].SetActive(false);
        }

        dropdownAnimal.AddOptions(animalList);
        dropdownAnimation.AddOptions(animationList);
        dropdownShapekey.AddOptions(shapekeyList);

        // Set to eyes_blink
        dropdownShapekey.value = 1;
        ChangeShapekey();

        // Bounds b = animals[0].transform.GetChild(0).GetChild(0).GetComponent<Renderer>().bounds;
    }

    private void Update()
    {
        if (Input.GetKeyDown("up"))
            PrevAnimal();
        else if (Input.GetKeyDown("down"))
            NextAnimal();
        else if (Input.GetKeyDown("right")
                 && (Input.GetKey(KeyCode.LeftControl)
                     || Input.GetKey(KeyCode.RightControl)))
            NextShapekey();
        else if (Input.GetKeyDown("left")
                 && (Input.GetKey(KeyCode.LeftControl)
                     || Input.GetKey(KeyCode.RightControl)))
            PrevShapekey();
        else if (Input.GetKeyDown("right"))
            NextAnimation();
        else if (Input.GetKeyDown("left")) PrevAnimation();
    }


    public void NextAnimal()
    {
        if (dropdownAnimal.value >= dropdownAnimal.options.Count - 1)
            dropdownAnimal.value = 0;
        else
            dropdownAnimal.value++;

        ChangeAnimal();
    }

    public void PrevAnimal()
    {
        if (dropdownAnimal.value <= 0)
            dropdownAnimal.value = dropdownAnimal.options.Count - 1;
        else
            dropdownAnimal.value--;

        ChangeAnimal();
    }

    public void ChangeAnimal()
    {
        animals[animalIndex].SetActive(false);
        animals[dropdownAnimal.value].SetActive(true);
        animalIndex = dropdownAnimal.value;

        ChangeAnimation();
        ChangeShapekey();
    }

    public void NextAnimation()
    {
        if (dropdownAnimation.value >= dropdownAnimation.options.Count - 1)
            dropdownAnimation.value = 0;
        else
            dropdownAnimation.value++;

        ChangeAnimation();
    }


    public void PrevAnimation()
    {
        if (dropdownAnimation.value <= 0)
            dropdownAnimation.value = dropdownAnimation.options.Count - 1;
        else
            dropdownAnimation.value--;

        ChangeAnimation();
    }

    public void ChangeAnimation()
    {
        var animator = animals[dropdownAnimal.value].GetComponent<Animator>();
        if (animator != null)
        {
            var index = dropdownAnimation.value;

            // If Spin/Splash animation
            if (index == 15)
            {
                if (animator.HasState(0, Animator.StringToHash("Spin")))
                    animator.Play("Spin");
                // dropdownAnimation.options[index] = new Dropdown.OptionData("Spin");
                else if (animator.HasState(0, Animator.StringToHash("Splash"))) animator.Play("Splash");
                // dropdownAnimation.options[index] = new Dropdown.OptionData("Splash");
            }
            else
            {
                animator.Play(dropdownAnimation.options[index].text);
            }
        }
    }

    public void NextShapekey()
    {
        if (dropdownShapekey.value >= dropdownShapekey.options.Count - 1)
            dropdownShapekey.value = 0;
        else
            dropdownShapekey.value++;

        ChangeShapekey();
    }

    public void PrevShapekey()
    {
        if (dropdownShapekey.value <= 0)
            dropdownShapekey.value = dropdownShapekey.options.Count - 1;
        else
            dropdownShapekey.value--;

        ChangeShapekey();
    }

    public void ChangeShapekey()
    {
        var animator = animals[dropdownAnimal.value].GetComponent<Animator>();
        if (animator != null) animator.Play(dropdownShapekey.options[dropdownShapekey.value].text);
    }

    public void GoToWebsite(string URL)
    {
        Application.OpenURL(URL);
    }
}