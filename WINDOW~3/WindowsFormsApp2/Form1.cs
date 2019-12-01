using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button_add_Click(object sender, EventArgs e) //추가하기 버튼ㅎ
        {
            if (textBox1.Text == "" || textBox2.Text == "") //이름 or 번호 입력 안됐을 때 출력 메세지
            {
                MessageBox.Show("이름또는 전화번호가 입력되지 않았습니다.");
            }

            else //이름, 번호 입력 됐을 때 리스트 뷰에 추가
            {

                string name = textBox1.Text;
                string number = textBox2.Text;

                string[] strs = new string[] { name, number };
                ListViewItem item = new ListViewItem(strs);

                item.Text = name;
                listView1.Items.Add(item);
                ClearInputControl();
                SaveContact();

                MessageBox.Show("전화번호부를 저장 하였습니다.");
            }
        }

        private void ClearInputControl() //저장하거나 수정 ,삭제한 뒤 텍스트 박스 비우기
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
        }

        private void SaveContact() //추가, 수정, 삭제 뒤 txt파일에 즉각적으로 저장
        {
            StreamWriter sw = new StreamWriter("test.txt");

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                sw.WriteLine(listView1.Items[i].Text + "|" + listView1.Items[i].SubItems[1].Text + "|");
            }
            sw.Close();
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e) // 리스트뷰 항목 선택 관련
        {
            bool selected = listView1.SelectedItems.Count > 0; // 항목 미선택시 추가하기 제외한 버튼 비활성화
            button_remove.Enabled = button_modi.Enabled = selected;
            if (selected == false)
            {
                ClearInputControl();
                return;
            }
            ListViewItem lvi = listView1.SelectedItems[0]; // 항목 선택시 텍스트 박스에 들어감
            textBox1.Text = lvi.SubItems[0].Text;
            textBox2.Text = lvi.SubItems[1].Text;
        }

        private void Button_modi_Click(object sender, EventArgs e) //수정버튼 
        {
            bool selected = listView1.SelectedItems.Count > 0;
            if (selected == false)
            {
                MessageBox.Show("선택되지 않았습니다.");
            }
            else
            {
                ListViewItem lvi = listView1.SelectedItems[0];
                string name = textBox1.Text;
                string number = textBox2.Text;
                lvi.SubItems[0].Text = name;
                lvi.SubItems[1].Text = number;
                ClearInputControl();
                SaveContact();
                MessageBox.Show("수정 되었습니다");
            }
        }

        private void Button_remove_Click(object sender, EventArgs e) //삭제하기 버튼
        {
            bool selected = listView1.SelectedItems.Count > 0;
            if (selected == false) //선택된거 없을 때
            {
                MessageBox.Show("선택되지 않았습니다.");
            }
            else //삭제확인
            {
                if(MessageBox.Show("정말 삭제 하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo,MessageBoxIcon.Question)== DialogResult.Yes)
                { 
                     ListViewItem lvi = listView1.SelectedItems[0];
                     listView1.Items.Remove(lvi);
                     ClearInputControl();
                     SaveContact();
                }

            }
        }

        private void Button_txt_Click(object sender, EventArgs e) //메뉴아이템의 저장버튼 클릭시 이벤트 
        {
            StreamWriter sw = new StreamWriter("test.txt");

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                sw.WriteLine(listView1.Items[i].Text + "|" + listView1.Items[i].SubItems[1].Text + "|");
            }
            sw.Close();

            MessageBox.Show("전화번호부를 저장 하였습니다.");

        }

        private void Button_open_Click(object sender, EventArgs e) //메뉴 아이템의 오픈버튼 클릭시 이벤트
        {
            using (TextReader tReader = new StreamReader("test.txt"))
            {
                string[] stringLines = tReader.ReadToEnd().Replace("\n", "").Split((char)Keys.Enter);

                foreach (string stringLine in stringLines)
                {
                    if (stringLine != string.Empty)
                    {
                        string[] lineItems = stringLine.Split('|');

                        ListViewItem lv = new ListViewItem();
                        lv.Text = lineItems[0];
                        lv.SubItems.Add(lineItems[1]);
                        listView1.Items.Add(lv);
                    }
                }
            }
        }

        private void 종료XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 저장SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter("test.txt");

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                sw.WriteLine(listView1.Items[i].Text + "|" + listView1.Items[i].SubItems[1].Text + "|");
            }
            sw.Close();

            MessageBox.Show("전화번호부를 저장 하였습니다.");
        }

        private void 열ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (TextReader tReader = new StreamReader("test.txt"))
            {
                string[] stringLines = tReader.ReadToEnd().Replace("\n", "").Split((char)Keys.Enter);

                foreach (string stringLine in stringLines)
                {
                    if (stringLine != string.Empty)
                    {
                        string[] lineItems = stringLine.Split('|');

                        ListViewItem lv = new ListViewItem();
                        lv.Text = lineItems[0];
                        lv.SubItems.Add(lineItems[1]);
                        listView1.Items.Add(lv);
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e) //폼 실행 될 때 txt에 저장돼있는 글들 리스트뷰로 띄우기
        {
            using (TextReader tReader = new StreamReader("test.txt"))
            {
                string[] stringLines = tReader.ReadToEnd().Replace("\n", "").Split((char)Keys.Enter);

                foreach (string stringLine in stringLines)
                {
                    if (stringLine != string.Empty)
                    {
                        string[] lineItems = stringLine.Split('|');

                        ListViewItem lv = new ListViewItem();
                        lv.Text = lineItems[0];
                        lv.SubItems.Add(lineItems[1]);
                        listView1.Items.Add(lv);
                    }
                }
            }
        }
    }
}
