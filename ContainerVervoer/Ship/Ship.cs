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
            //First we need to get the "front rows"
            int frontWeight = 0;
            for (int i = 0; i < rows.Count / 2; i++)
            {
                frontWeight += rows[i].GetRowkWeight();
            }
            //Secondly we need to get the "back rows"
            int backWeight = 0;
            for (int i = 0; i < rows.Count / 2 + 1; i++)
            {
                frontWeight += rows[i].GetRowkWeight();
            }
            //The ship can only have max difference from 20% so we need to have 20% first from the combined weight
            int maxdifference = (int) ((frontWeight + backWeight) * 0.2);
            int difference = frontWeight - backWeight;
            if (difference < maxdifference)
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
            if (currentshipweight < maxweight / 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
