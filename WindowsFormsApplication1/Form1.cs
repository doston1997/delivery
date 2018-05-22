﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClothingClass;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        clothing GetModelFromUI()
        {
            return new clothing()
            {
                Filled = dateTimePicker1.Value,
                Goods = listBox1.Items.OfType<GoodsType>().ToList(),
                Price = numericUpDown1.Value
            };
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var form = new Form2(new GoodsType());
            var res = form.ShowDialog(this);
            if (res == DialogResult.OK)
            {
                listBox1.Items.Add(form.goods);
                RecalculatePrice();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var si = listBox1.SelectedIndex;
            listBox1.Items.RemoveAt(si);
            RecalculatePrice();
        }
        private void RecalculatePrice()
        {
            var dto = GetModelFromUI();
            decimal price = 0;

            foreach (var e in dto.Goods)
            {
                switch (e.Catalog)
                {
                    case GoodsCatalog.Jeans:
                        price += 1500;
                        break;
                    case GoodsCatalog.Shirts:
                        price += 700;
                        break;
                    case GoodsCatalog.Jerseys:
                        price += 1000;
                        break;
                    case GoodsCatalog.Shotters:
                        price += 1200;
                        break;
                    case GoodsCatalog.Jackets:
                        price += 500;
                        break;
                    case GoodsCatalog.Costumers:
                        price += 2000;
                        break;
                }
            }
            numericUpDown1.Value = price;
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var goods = listBox1.SelectedItem as GoodsType;
            if (goods == null)
                return;
            var form = new Form2(goods.Clone());
            var res = form.ShowDialog(this);
            if (res == DialogResult.OK)
            {
                var si = listBox1.SelectedIndex;
                listBox1.Items.RemoveAt(si);
                listBox1.Items.Insert(si, form.goods);
            }
        }
        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            button2.Enabled = true;
        }
    }
}
