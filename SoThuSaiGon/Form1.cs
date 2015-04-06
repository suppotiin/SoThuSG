using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SoThuSaiGon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = String.Format("Bây giờ là {0}:{1}:{2} ngày {3} tháng {4} năm {5}",
            DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
        
        }

        private void mnuClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mnuLoad_Click(object sender, EventArgs e)
        {
            // Mở tập tin
            StreamReader reader = new StreamReader("thumoi.txt");
            if (reader == null) return;
            // Đọc từng dòng văn bản trong tập tin
            string input = null;
            while ((input = reader.ReadLine()) != null)
            {
                lstThuMoi.Items.Add(input);
            }
            // Đóng tập tin
            reader.Close();

            using (StreamReader rs = new StreamReader("danhsachthu.txt"))
            {
                input = null;
                while ((input = rs.ReadLine()) != null)
                {
                    lstThuMoi.Items.Add(input);
                }
            }

        }

        private void mnuSave_Click(object sender, EventArgs e)
        {
            //// Mở tập tin
            StreamWriter writer = new StreamWriter("danhsachthu.txt");
            if (writer == null) return; // error

            // Ghi dữ liệu vào tập tin
            foreach (var item in lstDanhSach.Items)
             writer.WriteLine(item.ToString());

            // Chèn ký tự xuống dòng
            writer.Write(writer.NewLine);
            // Đóng tập tin
            writer.Close();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //// Mở tập tin
            StreamWriter writer = new StreamWriter("danhsachthu.txt");
            if (writer == null) return; // error

            // Ghi dữ liệu vào tập tin
            foreach (var item in lstDanhSach.Items)
                writer.WriteLine(item.ToString());

            // Chèn ký tự xuống dòng
            writer.Write(writer.NewLine);
            // Đóng tập tin
            writer.Close();
        }

        private void lstThuMoi_MouseDown(object sender, MouseEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            int index = lb.IndexFromPoint(e.X, e.Y);
            //Nếu đã chọn đc item
            if (index != -1)
            {
                //Bắt đầu drag item (chỉ cần text. của item đó)
                DragDropEffects effect = lb.DoDragDrop(lb.Items[index].ToString(), DragDropEffects.Move);
            }
        }

        private void lstThuMoi_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void lstDanhSach_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat)) //ktra dữ liệu vào cần sao chép đúng ko
            {
                ListBox lb = (ListBox)sender;
                //listbox.item.contains ktra item đã có trong lst chưa, có trả về true, ko trả về false
                if (!lstDanhSach.Items.Contains(lstThuMoi.SelectedItem.ToString()))
                    lb.Items.Add(e.Data.GetData(DataFormats.Text));
            }
        }


        
    }
}
