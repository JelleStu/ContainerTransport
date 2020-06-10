using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContainerVervoer
{
    public class Ship
    {
        public int lenght;
        public int width;
        private int size;
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
    }
}
