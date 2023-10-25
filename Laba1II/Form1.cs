using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Laba1II.Models;
using Newtonsoft.Json;

namespace Laba1II
{
    public partial class Form1 : Form
    {
        List<BaseItem> items = new List<BaseItem>();
        BaseItem currentItem = new BaseItem();
        string Json = "file.json";
        public Form1()
        {
            InitializeComponent();
            StartGameInits();
        }
        void StartGameInits()
        {
            GetItemsFromJson();
            currentItem = items[0];
            labelQuestion.Text = currentItem.Question;
        }
        void Restart()
        {
            ShowingYesNo(true);
            pictureBox1.Visible = false;
            StartGameInits();
        }

        #region buttons
        private void button1_Click(object sender, EventArgs e)
        {
            ChooseYes();
            labelQuestion.Text = currentItem.Question;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChooseNo();
            labelQuestion.Text = currentItem.Question;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Restart();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveInfo();
            ShowingQuestionInputs(false);
            labelQuestion.Text = "Спасибо за ответ :*";
            textBox1.Clear();
            textBox2.Clear();
        }
        #endregion

        #region Visual

        void ShowingYesNo(bool isShouldShow)
        {
            button1.Visible = isShouldShow;
            button2.Visible = isShouldShow;
        }

        void ShowingQuestionInputs(bool isShouldShow)
        {
            label2.Visible = isShouldShow;
            label3.Visible = isShouldShow;
            textBox1.Visible = isShouldShow;
            textBox2.Visible = isShouldShow;
            button3.Visible = isShouldShow;
        }
        #endregion

        #region Json
        private void GetItemsFromJson()
        {
            items.Clear();
            var lines = File.ReadAllLines(Json);
            foreach (var line in lines)
            {
                var item = JsonConvert.DeserializeObject<BaseItem>(line);
                items.Add(item);
            }
        }

        private void RewriteJson()
        {
            File.WriteAllText(Json, string.Empty);
            for (int i = 0; i < items.Count(); i++)
                AddToJsonFile(items[i]);
        }
        
        private void AddToJsonFile(BaseItem item)
        {
            var json = JsonConvert.SerializeObject(item);
            StreamWriter file = new StreamWriter(Json, true);
            file.WriteLine(json);
            file.Close();
        }
        #endregion
        private void ChooseYes()
        {
            if (currentItem.YesBaseNumber == 0)
            {
                Win();
                return;
            }
            var index = (int)currentItem.YesBaseNumber - 1;
            try
            {
                currentItem = items[index];
            }
            catch
            { }
        }
        void Win()
        {
            labelQuestion.Text = "Урааа я угадал!!!";
            ShowingYesNo(false);
            pictureBox1.Visible = true;
        }

        private void ChooseNo()
        {
            if (currentItem.NoBaseNumber == -1)
            {
                Lose();
                return;
            }
            var index = (int)currentItem.NoBaseNumber - 1;
            try
            {
                currentItem = items[index];
            }
            catch
            { }
        }
        void Lose()
        {
            labelQuestion.Text = "Я не знаю что это(";
            ShowingYesNo(false);
            ShowingQuestionInputs(true);
        }

        void SaveInfo()
        {
            var item = new BaseItem();
            item.Id = items.Count() + 1;
            currentItem.NoBaseNumber = item.Id;
            item.Question = "Это " + textBox2.Text + "?";
            item.NoBaseNumber = -1;
            item.YesBaseNumber = item.Id + 1;

            var itemAnswer = new BaseItem();
            itemAnswer.Id = item.Id + 1;
            itemAnswer.Question = "Это " + textBox1.Text;
            itemAnswer.NoBaseNumber = -1;
            itemAnswer.YesBaseNumber = 0;

            items.Add(item);
            items.Add(itemAnswer);
            RewriteJson();
        }

        #region FirstJsonInit
        private void CreateStartJson()
        {
            var items = TempInit();
            for (int i = 0; i < items.Length; i++)
                AddToJsonFile(items[i]);
        }
        private BaseItem[] TempInit()
        {
            BaseItem[] items = new BaseItem[5];
            BaseItem item = new BaseItem();
            item.Id = 1;
            item.Question = "Это кухонная техника?";
            item.NoBaseNumber = 4;
            item.YesBaseNumber = 2;
            items[0] = item;
            BaseItem item2 = new BaseItem();
            item2.Id = 2;
            item2.Question = "Это для хранения?";
            item2.NoBaseNumber = -1;
            item2.YesBaseNumber = 3;
            items[1] = item2;
            BaseItem item3 = new BaseItem();
            item3.Id = 3;
            item3.Question = "Это холодильник";
            item3.NoBaseNumber = -1;
            item3.YesBaseNumber = 0;//победа
            items[2] = item3;
            BaseItem item4 = new BaseItem();
            item4.Id = 4;
            item4.Question = "Это для уборки?";
            item4.NoBaseNumber = -1;
            item4.YesBaseNumber = 5;
            items[3] = item4;
            BaseItem item5 = new BaseItem();
            item5.Id = 5;
            item5.Question = "Это пылесос";
            item5.NoBaseNumber = -1;
            item5.YesBaseNumber = 0;
            items[4] = item5;
            return items;
        }
        #endregion
    }
}
