using System;
using System.Collections.Generic;
using System.IO;

namespace graph_visualization
{
    class Table
    {
        public List<List<int>[]> Tabl { get; } = new List<List<int>[]>();
        public List<string> Tier { get; } = new List<string>();
        public int n { get; set; } =0 ;
        public void Start(string pathtxt)
        {
            StreamReader fin = new StreamReader(pathtxt);
            string file = fin.ReadToEnd();
            fin.Close();
            string[] Lines = file.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            n = Lines.Length;
            Tabl.Add(new List<int>[Lines.Length]);
            for (int i = 0; i < Tabl[0].Length; i++)
            {
                string[] Items = Lines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                List<int> temp = new List<int>();
                foreach (string Item in Items)
                    temp.Add(Int32.Parse(Item));
                Tabl[0][i] = temp;
            }
            int countTier = 0;
            do
            {
                print_tier(countTier);
            }
            while (bild_tabl(ref countTier));
        }
        public string LastTier()
        {
            string temp = "";
            for(int i= 0; i < Tabl[Tabl.Count - 2].Length; i++)
            {
                if (Tabl[Tabl.Count - 2][i].Count >= 1)
                    temp += (i+1).ToString() + ",";
            }
            temp=temp.Remove(temp.Length - 1);
            return temp;
        }
        public void print_tier(int count)
        {
            string str = "     ###" + (count + 1) + "###\n";
            for (int i = 0; i < Tabl[count].GetLength(0); i++)
            {
                str += ((i + 1) + "->   ");
                for (int j = 0; j < Tabl[count][i].Count; j++)
                    str += (Tabl[count][i][j] + " ");
                str += "\n";
            }
            Tier.Add(str);
        }
        public (int, int) FindRadiusAndCentre()
        {
            for (int i = 0; i < Tabl.Count; i++)
            {
                for (int j = 0; j < Tabl[i].Length; j++)
                    if (Tabl[i][j].Count == 0)
                        return (i, j + 1);
            }
            return (0, 0);
        }
        private bool bild_tabl(ref int count)
        {
            List<int>[] NextTier = new List<int>[Tabl[0].Length];
            for (int i = 0; i < Tabl[count].Length; i++)
            {
                List<int> temp = new List<int>();
                for (int j = 0; j < Tabl[count][i].Count; j++)
                {
                    int key = Tabl[count][i][j];
                    for (int q = 0; q < Tabl[0][key - 1].Count; q++)
                    {
                        if (i + 1 != Tabl[0][key - 1][q] && check_number(temp, i, Tabl[0][key - 1][q]))
                            temp.Add(Tabl[0][key - 1][q]);
                    }
                }
                NextTier[i] = temp;
            }
            Tabl.Add(NextTier);
            count++;
            return check_ending_bild(Tabl[count]);
        }
        private bool check_number(List<int> list, int ind, int item)
        {
            for (int k = 0; k < Tabl.Count; k++)
            {
                for (int i = 0; i < Tabl[k][ind].Count; i++)
                    if (Tabl[k][ind][i] == item)
                        return false;
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == item)
                    return false;
            }
            return true;
        }
        private bool check_ending_bild(List<int>[] list)
        {
            int count = 0;
            for (int j = 0; j < list.Length; j++)
            {
                if (list[j].Count == 0)
                    count++;
                    if (count == list.Length)
                        return false;
            }
            return true;
        }
        public int VertexSpacing(int firstV, int secondV)
        {

            for (int stage = 0; stage < Tabl.Count; stage++)
            {
                for (int j = 0; j < Tabl[stage][firstV - 1].Count; j++)
                {
                    if (Tabl[stage][firstV - 1][j] == secondV)
                        return stage + 1;                   
                }
            }
            return -1;
        }
        public bool Connectivity()
        {
            for (int i = 1; i <= n; i++)
                for (int j = 1; j <= n; j++)
                {
                    if (i != j)
                        if (VertexSpacing(i, j) == -1)
                            return false;
                }
            return true;
        }
    }
}