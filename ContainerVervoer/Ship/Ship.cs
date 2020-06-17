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
