﻿using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Text;

namespace UsingControls
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // System.Drawing 네임스페이스를 사용해야 한다.
            FontFamily[] fontFamilies;

            // System.Drawing.Text 네임스페이스를 사용해야 한다.
            InstalledFontCollection installedFontCollection = new InstalledFontCollection();

            // OS에 설치되어 있는 모든 Font정보를 가져 온다.
            fontFamilies = installedFontCollection.Families;

            foreach (var fontFamily in fontFamilies)
                cboFont.Items.Add(fontFamily.Name);
        }

        private void cboFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeFont();
        }

        private void chkBold_CheckedChanged(object sender, EventArgs e)
        {
            ChangeFont();
        }

        private void chkItalic_CheckedChanged(object sender, EventArgs e)
        {
            ChangeFont();
        }

        private void tbDummy_Scroll(object sender, EventArgs e)
        {
            //슬라이더의 위치에 따라 프로그레스바의 내용도 변경
            pgDummy.Value = tbDummy.Value;
        }

        void ChangeFont()
        {
            // cboFont에서 선택한 항목이 없으면 메소드 종료
            if (cboFont.SelectedIndex < 0)
                return;

            //FontStyle 객체를 초기화
            FontStyle style = FontStyle.Regular;

            // "굵게" 체크 박스가 선택되어 있으면 Bold 논리합 수행
            if (chkBold.Checked)
                style |= FontStyle.Bold;

            // "이텔릭" 체크 박스가 선택되어 있으면 Italic 논리합 수행
            if (chkItalic.Checked)
                style |= FontStyle.Italic;

            // txtSampleText의 Font 프로퍼티를 앞에서 만든 style로 수정
            txtSampleText.Font = new Font((string)cboFont.SelectedItem, 10, style);
        }

        private void btnModal_Click(object sender, EventArgs e)
        {
            Form frm = new Form();
            frm.Text = "Modal Form";
            frm.Width = 300;
            frm.Height = 100;
            frm.BackColor = Color.Red;
            frm.ShowDialog(); // Modal 창을 띄운다.
        }

        private void btnModaless_Click(object sender, EventArgs e)
        {
            Form frm = new Form();
            frm.Text = "Modaless Form";
            frm.Width = 300;
            frm.Height = 300;
            frm.BackColor = Color.Green;
            frm.Show(); // Modaless 창을 띄운다.
        }

        private void btnMsgBox_Click(object sender, EventArgs e)
        {
            MessageBox.Show(txtSampleText.Text, "MessageBox Test", 
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }   
    }
}
