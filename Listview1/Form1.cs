using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Listview1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetupListview();
        }

        private void SetupListview()
        {
            listView1.View = View.Details;

            listView1.AllowColumnReorder = false;

            listView1.FullRowSelect = true;

            listView1.GridLines = true;


            listView1.Columns.Add("part", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("size", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("min/max", 200, HorizontalAlignment.Left);
            listView1.Columns.Add("remain", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("ng/ok", 80, HorizontalAlignment.Center);

            //for (int i = 0; i < 5; i++)
            //{
            //    ListViewItem item1 = new ListViewItem("D");
            //    if (i == 0)
            //    {
            //        item1.Selected = true;
            //    }
            //    item1.SubItems.Add("10");
            //    item1.SubItems.Add("+0.002/+0.000");
            //    item1.SubItems.Add("5");
            //    listView1.Items.Add(item1);
            //}

            UpdateListview(AddListviewItem());
        }

        private List<ListItems> AddListviewItem()
        {
            List<ListItems> list = new List<ListItems>();
            list.Add(new ListItems() { Part = "D", Size = "10", MinMax = "+0.005/+0.000", Remain = "5", Active = "active" });
            list.Add(new ListItems() { Part = "L", Size = "10", MinMax = "+0.005/+0.000", Remain = "5" });
            list.Add(new ListItems() { Part = "P", Size = "10", MinMax = "+0.005/+0.000", Remain = "5" });
            list.Add(new ListItems() { Part = "W", Size = "10", MinMax = "+0.005/+0.000", Remain = "5" });
            return list;
        }

        private void UpdateListview(List<ListItems> listItems)
        {
            listView1.Items.Clear();
            foreach (var item in listItems)
            {
                ListViewItem item1 = new ListViewItem(item.Part);
                if (item.Active != null)
                {
                    if (item.NgOk == "NG")
                    {
                        item1.BackColor = Color.Red;
                    }
                    else
                    {
                        item1.BackColor = Color.Blue;
                        item1.ForeColor = Color.White;
                    }
                    //item1.Selected = true;
                }

                item1.SubItems.Add(item.Size);
                item1.SubItems.Add(item.MinMax);
                item1.SubItems.Add(item.Remain);
                item1.SubItems.Add(item.NgOk);
                listView1.Items.Add(item1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var items = listView1.Items;

            int index = 0;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].BackColor == Color.Blue)
                {
                    index = i;
                    break;
                }
            }
            //int index = listView1.Items.IndexOf(listView1.SelectedItems[0]);
            //int index = listView1.Items.IndexOf(listView1.SelectedItems[0]);

            List<ListItems> list = new List<ListItems>();
            //foreach (ListViewItem item in listView1.Items)
            //{
            //    list.Add(new ListItems() { Part = item[0].text, Size = item[0].SubItems[0].Text, MinMax = "+0.005/+0.000", Remain = "5" });
            //}

            for (int i = 0; i < items.Count; i++)
            {                
                list.Add(new ListItems()
                {
                    Part = items[i].Text,
                    Size = items[i].SubItems[1].Text,
                    MinMax = items[i].SubItems[2].Text,
                    Remain = index == i ? (int.Parse(items[i].SubItems[3].Text) - 1).ToString() : items[i].SubItems[3].Text,
                    NgOk = index == i ? "OK" : items[i].SubItems[4].Text == "OK" ? "OK" : "",
                    Active = i > index && (index + 1 == i) ? "active" : index == items.Count - 1 && i == 0 ? "active" : null
                });

                //if (int.Parse(items[i].SubItems[3].Text) <= 0 && index == items.Count - 1)
                //{
                //    return;
                //}
            }

            //list.Add(new ListItems() { Part = item[1].Text, Size = "10", MinMax = "+0.005/+0.000", Remain = "5" });
            //list.Add(new ListItems() { Part = item[2].Text, Size = "10", MinMax = "+0.005/+0.000", Remain = "5" });
            //list.Add(new ListItems() { Part = item[3].Text, Size = "10", MinMax = "+0.005/+0.000", Remain = "5" });

            UpdateListview(list);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                return;
            }

            //int index = listView1.Items.IndexOf(listView1.SelectedItems[0]);
            //if (listView1.Items[index].Focused == true)
            //{
            //    listView1.Items[index].BackColor = Color.Red;

            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var items = listView1.Items;

            int index = 0;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].BackColor == Color.Green)
                {
                    index = i;
                    break;
                }
            }

            List<ListItems> list = new List<ListItems>();

            //string text = "NG";

            for (int i = 0; i < items.Count; i++)
            {
                list.Add(new ListItems()
                {
                    Part = items[i].Text,
                    Size = items[i].SubItems[1].Text,
                    MinMax = items[i].SubItems[2].Text,
                    Remain = (int.Parse(items[i].SubItems[3].Text) - 1).ToString(),
                    NgOk = items[i].SubItems[4].Text == "" && index == i ? "NG" : items[i].SubItems[4].Text == "NG" ? "NG" : "",
                    //NgOk = index == items.Count - 1 ? "" : index == i ? "OK" : items[i].SubItems[4].Text != "" ? "OK" : "",
                    Active = i == index ? "active" : null
                    //Active = i > index && (index + 1 == i) ? "active" : index == items.Count - 1 && i == 0 ? "active" : null
                });
            }

            UpdateListview(list);
        }
    }



    public class ListItems
    {
        public string Part { get; set; }
        public string Size { get; set; }
        public string MinMax { get; set; }
        public string Remain { get; set; }
        public string NgOk { get; set; }
        public string Active { get; set; }
    }
}
