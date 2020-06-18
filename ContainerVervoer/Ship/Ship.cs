using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ContainerVervoer
{
    public class Ship
    {
        public int lenght;
        public int width;
        private int maxweight;
        public List<Row> rows;
        public bool IsInBalance;

        public Ship()
        {
            rows = new List<Row>();
        }

        public Row ReturnRow(int rownumber)
        {
            return rows.FirstOrDefault(r=>r.row == rownumber);
        }

        public void CreateRows()
        {
            for (int i = 0; i < width; i++)
            {
                var row = new Row(width, i);
                rows.Add(row);
            }
        }

        public void CheckBalance()
        {
            //First we need to get the "left rows" select all rows, but we need only the half of the stackcontainers.
            int leftWeight = 0;
            foreach (var row in rows)
            {
                foreach (var stack in row.Stacks)
                {
                    if (stack.stackNumber < width / 2)
                    {
                        leftWeight += stack.weight;
                    }
                }
            }

            //Secondly we need to get the "right rows"
            int rightWeight = 0;
            foreach (var row in rows)
            {
                foreach (var stack in row.Stacks)
                {
                    if (stack.stackNumber >= width / 2)
                    {
                        rightWeight += stack.weight;
                    }
                }
            }


            //The ship can only have max difference from 20% so we need to have 20% first from the combined weight
            double difference = (double)leftWeight / (double)rightWeight;

            if (difference > 0.8 && difference < 1.2)
            {
                IsInBalance = true;
            }
            else
            {
                IsInBalance = false;
            }

        }

        public bool CheckForCapSize()
        {
            maxweight = lenght * width * 120000;
            int currentshipweight = rows.Sum(row => row.GetRowkWeight());
            return currentshipweight > maxweight / 2;
        }
    }
}
