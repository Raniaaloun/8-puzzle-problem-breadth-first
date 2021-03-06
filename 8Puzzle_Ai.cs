﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Rania Salem Aloun 115571

class node
{
    public static int generatedStates = 0;
    public static int expandedStates = 0;

    public int[] state = new int[9];

    int[] goal = new int[9] { 0,1,2,3,3,4,4,4,8 };
    
    public node[] children = new node[4];
    public node parent;
    //public int space = 0; //zero
    public node(int[] State)
    {
        for (int i = 0; i < 9; i++)
        {
            this.state[i] = State[i];
        }
       
    }
    public void printPuzzle()
    {
        int index = searchForZeroLocation();
        char[] tempArr = new char[9];
        for (int i = 0; i < 9; i++)
        {
            tempArr[i] = Convert.ToChar(state[i] + '0');
        }

        tempArr[index] = ' ';
        Console.WriteLine(tempArr[0] + " " + tempArr[1] + " " + tempArr[2]);
        Console.WriteLine(tempArr[3] + " " + tempArr[4] + " " + tempArr[5]);
        Console.WriteLine(tempArr[6] + " " + tempArr[7] + " " + tempArr[8]);
        Console.WriteLine("");
    }

    public bool goalTest()
    {
        int flag = 0;    //flag to find if the test fails
        for (int i = 0; i < 9; i++)
        {
            if (this.state[i] != this.goal[i])
            {
                flag = 1;
                break;
            }

        }
        if (flag == 0) //all are equal
        {
            Console.WriteLine("Puzzle is solved!");
            return true;
        }
        else
        {
            return false;
        }
    }
    public int searchForZeroLocation()
    {
        int location = 10;
        int target = 0;
        for (int i = 0; i < 9; i++)
        {
            if (state[i] == target)
            {
                location = i;
            }
        }

        return location;
    }
    public void Up()
    {
        int[] arrayUp = new int[9];
        for (int i = 0; i < 9; i++)
        {
            arrayUp[i] = state[i];
        }
        int index = searchForZeroLocation();
        arrayUp[index] = arrayUp[index - 3];
        arrayUp[index - 3] = 0;

        children[0] = new node(arrayUp);
        children[0].parent = this; 


    }
    public void down()
    {
        int[] arrayDown = new int[9];
        for (int i = 0; i < 9; i++)
        {
            arrayDown[i] = state[i];
        }
        int index = searchForZeroLocation();
        arrayDown[index] = arrayDown[index + 3];
        arrayDown[index + 3] = 0;

        children[1] = new node(arrayDown);
        children[1].parent = this;

    }
    public void Right()
    {
        int[] arrayRight = new int[9];
        for (int i = 0; i < 9; i++)
        {
            arrayRight[i] = state[i];
        }
        int index = searchForZeroLocation();
        arrayRight[index] = arrayRight[index + 1];
        arrayRight[index + 1] = 0;

        children[2] = new node(arrayRight);
        children[2].parent = this;

    }
    public void Left()
    {
        int[] arrayLeft = new int[9];
        for (int i = 0; i < 9; i++)
        {
            arrayLeft[i] = state[i];
        }
        int index = searchForZeroLocation();
        arrayLeft[index] = arrayLeft[index - 1];
        arrayLeft[index - 1] = 0;

        children[3] = new node(arrayLeft);
        children[3].parent = this;

    }
    public void Expand()
    {
        expandedStates++;
        int location = searchForZeroLocation();
        if (location != 0 && location != 1 && location != 2)
        {
            Up();
            generatedStates++;

        }
        if (location != 6 && location != 7 && location != 8)
        {
            down();
            generatedStates++;
        }
        if (location != 2 && location != 5 && location != 8)
        {
            Right();
            generatedStates++;
        }
        if (location != 0 && location != 3 && location != 6)
        {
            Left();
            generatedStates++;
        }

    }
    

}
namespace Ai
{
    class Program
    {
        static bool duplication(List<node> x, int[] arr)
        {
            for(int i=0; i<x.Count; i++)
            {
              if (x[i].state.SequenceEqual(arr))
                   return true; 
            }
            return false;
        }
        static void Main(string[] args)
        {
            int[] arr = new int[9] { 1, 0, 2, 3, 4, 5, 6, 7, 8};

            Console.WriteLine("Welcome to the 8-Puzzle AI solver!");
            Console.Write("Enter initial state of puzzle: ");
            string inititial_State = Console.ReadLine();

            if (inititial_State.Length != 9)
            {
                Console.WriteLine("Error, missing values");
            }
            else
            {
                for (int i = 0; i < 9; i++)
                {
                    arr[i] = (int)Char.GetNumericValue(inititial_State[i]);

                }



                node Leaf = new node(arr);
                Leaf.printPuzzle();
                Leaf.goalTest();

                List<node> listOfObjects = new List<node>();
                listOfObjects.Add(Leaf);
                int flag = 0;   //to catch the goal
                node ListPointer;
                int count = 0;
                node goalPointer = new node(arr);
                while (flag == 0)
                {
                    ListPointer = listOfObjects[count]; //leaf at begining 
                    ListPointer.Expand();
                    for (int i = 0; i < 4; i++)
                    {
                        if (ListPointer.children[i] != null)
                        {
                            if (ListPointer.children[i].goalTest())
                            {
                                flag = 1;
                                goalPointer = ListPointer.children[i];
                                break;
                            }
                            else
                            {
                                if (duplication(listOfObjects, ListPointer.children[i].state) == false)
                                {
                                    listOfObjects.Add(ListPointer.children[i]);
                                    
                                }

                            }

                        }
                    }
                    count++;

                }
                int flag2 = 0;
                List<node> goalList = new List<node>();
                while (flag2==0)
                {
                    
                    goalList.Add(goalPointer); 
                    goalPointer = goalPointer.parent;
                    if (goalPointer.parent == null)
                    {
                        flag2 = 1;
                        //goalPointer.printPuzzle();
                        break;

                    }


                }
                Console.WriteLine("Path to Solution:");
                for (int i=goalList.Count-1; i>=0; i--)
                {
                    goalList[i].printPuzzle();
                }
                
                Console.WriteLine("Number of generated states = " + node.generatedStates);
                Console.WriteLine("Number of expanded states = " + node.expandedStates);
                string end;
                Console.WriteLine("Press enter to close...");
                Console.ReadLine();
            }
        }
    }
}
