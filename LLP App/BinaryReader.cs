﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLP_App
{
    static class BinaryReader
    {
        private static int[] binaryTable = new int[1] { 1 };
        private static string makeFullbyte(string binary)
        {
            int lenght = binary.Length;
            int missingChars = Math.Abs((lenght % 4) - 4);
            if(missingChars == 4) { return binary; }
            for(int i = missingChars; i > 0; i--)
            {
                binary = "0" + binary;
            }
            return binary;
        }
        
        public static int BinaryToNumber(string binary)
        {
            binary = makeFullbyte(binary);
            createNewBinaryTableForLenght(binary.Length);
            char[] bChar = binary.ToArray();

            int b = 0;
            int sum = 0;
            for(int i = bChar.Length - 1; i >= 0; i--)
            {
                if(bChar[i] == 1)
                {
                    sum += binaryTable[b];
                }
                b++;
            }
            return sum;
        }
        public static string NumberToBinary(int number, bool makeByte)
        {
            createNewBinaryTableForNumber(number);
            string holder = "";
            bool firstTrueSet = false;
            for(int i = binaryTable.Length - 1; i >= 0; i--)
            {
                if(binaryTable[i] <= number)
                {
                    firstTrueSet = true;
                    holder += "1";
                    number -= binaryTable[i];
                }
                else
                {
                    if (firstTrueSet)
                    {
                        holder += "0";
                    }
                }
            }
            if (makeByte)
            {
                holder = makeFullbyte(holder);
            }
            return holder;
        }
        private static void createNewBinaryTableForLenght(int lenght)
        {
            if(binaryTable.Length >= lenght) { return; }
            int[] tableTemp = new int[lenght];

            for(int i = 0; i < binaryTable.Length; i++) //copying old
            {
                tableTemp[i] = binaryTable[i];
            }
            for(int i = binaryTable.Length; i < tableTemp.Length; i++) //creating new
            {
                tableTemp[i] = (int)Math.Pow(2, i);
            }
            binaryTable = tableTemp;
        }
        private static void createNewBinaryTableForNumber(int number)
        {         
            if (number >= binaryTable[binaryTable.Length - 1])
            {
                List<int> newValues = new List<int>();
                int[] tableTemp;
                for (int i = binaryTable.Length; true; i++)
                {
                    int cal = (int)Math.Pow(2, i);
                    newValues.Add(cal);
                    if (cal >= number)
                    {
                        tableTemp = new int[i + 1];
                        break;
                    }
                }
                for (int i = 0; i < binaryTable.Length; i++) //copying old 
                {
                    tableTemp[i] = binaryTable[i];
                }
                int b = binaryTable.Length;
                for (int i = 0; i < newValues.Count; i++) //coping new
                {
                    tableTemp[b] = newValues[i];
                    b++;
                }
                binaryTable = tableTemp;
            }
            else { return; }
        }
    }
}
