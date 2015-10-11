using System;

namespace LiveSplit.UI.Components
{
    partial class NotesComponentSettings
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.filePathTxtBox = new System.Windows.Forms.TextBox();
            this.colorsGroupBox = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.btnClrTrphyBg = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.btnClrTrphyTxt = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.btnClrSkpBg = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.btnClrSkpTxt = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.btnClrKllBg = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.btnClrKillTxt = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.btnClrItemBg = new System.Windows.Forms.Button();
            this.btnClrTpBg = new System.Windows.Forms.Button();
            this.btnClrDrpBg = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.btnClrLvlupBg = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClrItemTxt = new System.Windows.Forms.Button();
            this.btnClrTpTxt = new System.Windows.Forms.Button();
            this.btnClrDrpTxt = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClrLvlupTxt = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.mainTableLayout.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.colorsGroupBox.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTableLayout
            // 
            this.mainTableLayout.ColumnCount = 1;
            this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainTableLayout.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.mainTableLayout.Controls.Add(this.colorsGroupBox, 0, 1);
            this.mainTableLayout.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.mainTableLayout.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayout.Name = "mainTableLayout";
            this.mainTableLayout.RowCount = 4;
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.81746F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 77.18254F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainTableLayout.Size = new System.Drawing.Size(601, 525);
            this.mainTableLayout.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.button1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.filePathTxtBox, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 425);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(595, 76);
            this.tableLayoutPanel2.TabIndex = 25;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(247, 45);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Load notes";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // filePathTxtBox
            // 
            this.filePathTxtBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.filePathTxtBox.Location = new System.Drawing.Point(3, 8);
            this.filePathTxtBox.Name = "filePathTxtBox";
            this.filePathTxtBox.Size = new System.Drawing.Size(589, 22);
            this.filePathTxtBox.TabIndex = 4;
            // 
            // colorsGroupBox
            // 
            this.colorsGroupBox.Controls.Add(this.label16);
            this.colorsGroupBox.Controls.Add(this.btnClrTrphyBg);
            this.colorsGroupBox.Controls.Add(this.label17);
            this.colorsGroupBox.Controls.Add(this.btnClrTrphyTxt);
            this.colorsGroupBox.Controls.Add(this.label10);
            this.colorsGroupBox.Controls.Add(this.btnClrSkpBg);
            this.colorsGroupBox.Controls.Add(this.label11);
            this.colorsGroupBox.Controls.Add(this.btnClrSkpTxt);
            this.colorsGroupBox.Controls.Add(this.label8);
            this.colorsGroupBox.Controls.Add(this.btnClrKllBg);
            this.colorsGroupBox.Controls.Add(this.label9);
            this.colorsGroupBox.Controls.Add(this.btnClrKillTxt);
            this.colorsGroupBox.Controls.Add(this.label15);
            this.colorsGroupBox.Controls.Add(this.label14);
            this.colorsGroupBox.Controls.Add(this.label13);
            this.colorsGroupBox.Controls.Add(this.btnClrItemBg);
            this.colorsGroupBox.Controls.Add(this.btnClrTpBg);
            this.colorsGroupBox.Controls.Add(this.btnClrDrpBg);
            this.colorsGroupBox.Controls.Add(this.label12);
            this.colorsGroupBox.Controls.Add(this.btnClrLvlupBg);
            this.colorsGroupBox.Controls.Add(this.label7);
            this.colorsGroupBox.Controls.Add(this.label4);
            this.colorsGroupBox.Controls.Add(this.label5);
            this.colorsGroupBox.Controls.Add(this.label3);
            this.colorsGroupBox.Controls.Add(this.btnClrItemTxt);
            this.colorsGroupBox.Controls.Add(this.btnClrTpTxt);
            this.colorsGroupBox.Controls.Add(this.btnClrDrpTxt);
            this.colorsGroupBox.Controls.Add(this.label6);
            this.colorsGroupBox.Controls.Add(this.label2);
            this.colorsGroupBox.Controls.Add(this.btnClrLvlupTxt);
            this.colorsGroupBox.Location = new System.Drawing.Point(3, 99);
            this.colorsGroupBox.Name = "colorsGroupBox";
            this.colorsGroupBox.Size = new System.Drawing.Size(595, 281);
            this.colorsGroupBox.TabIndex = 0;
            this.colorsGroupBox.TabStop = false;
            this.colorsGroupBox.Text = "Colors";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(296, 207);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(206, 17);
            this.label16.TabIndex = 23;
            this.label16.Text = "Achievement Background Color";
            // 
            // btnClrTrphyBg
            // 
            this.btnClrTrphyBg.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnClrTrphyBg.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClrTrphyBg.Location = new System.Drawing.Point(512, 204);
            this.btnClrTrphyBg.Name = "btnClrTrphyBg";
            this.btnClrTrphyBg.Size = new System.Drawing.Size(23, 23);
            this.btnClrTrphyBg.TabIndex = 24;
            this.btnClrTrphyBg.UseVisualStyleBackColor = false;
            this.btnClrTrphyBg.Click += new System.EventHandler(this.ColorButtonClick);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(30, 207);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(157, 17);
            this.label17.TabIndex = 21;
            this.label17.Text = "Achievement Text Color";
            // 
            // btnClrTrphyTxt
            // 
            this.btnClrTrphyTxt.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnClrTrphyTxt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClrTrphyTxt.Location = new System.Drawing.Point(224, 204);
            this.btnClrTrphyTxt.Name = "btnClrTrphyTxt";
            this.btnClrTrphyTxt.Size = new System.Drawing.Size(23, 23);
            this.btnClrTrphyTxt.TabIndex = 22;
            this.btnClrTrphyTxt.UseVisualStyleBackColor = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(296, 179);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(152, 17);
            this.label10.TabIndex = 19;
            this.label10.Text = "Skip Background Color";
            // 
            // btnClrSkpBg
            // 
            this.btnClrSkpBg.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnClrSkpBg.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClrSkpBg.Location = new System.Drawing.Point(512, 176);
            this.btnClrSkpBg.Name = "btnClrSkpBg";
            this.btnClrSkpBg.Size = new System.Drawing.Size(23, 23);
            this.btnClrSkpBg.TabIndex = 20;
            this.btnClrSkpBg.UseVisualStyleBackColor = false;
            this.btnClrSkpBg.Click += new System.EventHandler(this.ColorButtonClick);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(30, 178);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(103, 17);
            this.label11.TabIndex = 17;
            this.label11.Text = "Skip Text Color";
            // 
            // btnClrSkpTxt
            // 
            this.btnClrSkpTxt.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnClrSkpTxt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClrSkpTxt.Location = new System.Drawing.Point(224, 175);
            this.btnClrSkpTxt.Name = "btnClrSkpTxt";
            this.btnClrSkpTxt.Size = new System.Drawing.Size(23, 23);
            this.btnClrSkpTxt.TabIndex = 18;
            this.btnClrSkpTxt.UseVisualStyleBackColor = false;
            this.btnClrSkpTxt.Click += new System.EventHandler(this.ColorButtonClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(296, 150);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(143, 17);
            this.label8.TabIndex = 15;
            this.label8.Text = "Kill Background Color";
            // 
            // btnClrKllBg
            // 
            this.btnClrKllBg.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnClrKllBg.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClrKllBg.Location = new System.Drawing.Point(512, 147);
            this.btnClrKllBg.Name = "btnClrKllBg";
            this.btnClrKllBg.Size = new System.Drawing.Size(23, 23);
            this.btnClrKllBg.TabIndex = 16;
            this.btnClrKllBg.UseVisualStyleBackColor = false;
            this.btnClrKllBg.Click += new System.EventHandler(this.ColorButtonClick);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(30, 150);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 17);
            this.label9.TabIndex = 13;
            this.label9.Text = "Kill Text Color";
            // 
            // btnClrKillTxt
            // 
            this.btnClrKillTxt.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnClrKillTxt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClrKillTxt.Location = new System.Drawing.Point(224, 147);
            this.btnClrKillTxt.Name = "btnClrKillTxt";
            this.btnClrKillTxt.Size = new System.Drawing.Size(23, 23);
            this.btnClrKillTxt.TabIndex = 14;
            this.btnClrKillTxt.UseVisualStyleBackColor = false;
            this.btnClrKillTxt.Click += new System.EventHandler(this.ColorButtonClick);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(296, 60);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(210, 17);
            this.label15.TabIndex = 7;
            this.label15.Text = "Item Tracking Background Color";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(296, 92);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(178, 17);
            this.label14.TabIndex = 8;
            this.label14.Text = "Teleport Background Color";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(296, 121);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(156, 17);
            this.label13.TabIndex = 9;
            this.label13.Text = "Drop Background Color";
            // 
            // btnClrItemBg
            // 
            this.btnClrItemBg.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnClrItemBg.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClrItemBg.Location = new System.Drawing.Point(512, 60);
            this.btnClrItemBg.Name = "btnClrItemBg";
            this.btnClrItemBg.Size = new System.Drawing.Size(23, 23);
            this.btnClrItemBg.TabIndex = 10;
            this.btnClrItemBg.UseVisualStyleBackColor = false;
            this.btnClrItemBg.Click += new System.EventHandler(this.ColorButtonClick);
            // 
            // btnClrTpBg
            // 
            this.btnClrTpBg.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnClrTpBg.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClrTpBg.Location = new System.Drawing.Point(512, 89);
            this.btnClrTpBg.Name = "btnClrTpBg";
            this.btnClrTpBg.Size = new System.Drawing.Size(23, 23);
            this.btnClrTpBg.TabIndex = 11;
            this.btnClrTpBg.UseVisualStyleBackColor = false;
            this.btnClrTpBg.Click += new System.EventHandler(this.ColorButtonClick);
            // 
            // btnClrDrpBg
            // 
            this.btnClrDrpBg.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnClrDrpBg.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClrDrpBg.Location = new System.Drawing.Point(512, 118);
            this.btnClrDrpBg.Name = "btnClrDrpBg";
            this.btnClrDrpBg.Size = new System.Drawing.Size(23, 23);
            this.btnClrDrpBg.TabIndex = 12;
            this.btnClrDrpBg.UseVisualStyleBackColor = false;
            this.btnClrDrpBg.Click += new System.EventHandler(this.ColorButtonClick);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(296, 31);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(175, 17);
            this.label12.TabIndex = 5;
            this.label12.Text = "Levelup Background Color";
            // 
            // btnClrLvlupBg
            // 
            this.btnClrLvlupBg.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnClrLvlupBg.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClrLvlupBg.Location = new System.Drawing.Point(512, 31);
            this.btnClrLvlupBg.Name = "btnClrLvlupBg";
            this.btnClrLvlupBg.Size = new System.Drawing.Size(23, 23);
            this.btnClrLvlupBg.TabIndex = 6;
            this.btnClrLvlupBg.UseVisualStyleBackColor = false;
            this.btnClrLvlupBg.Click += new System.EventHandler(this.ColorButtonClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(161, 17);
            this.label7.TabIndex = 3;
            this.label7.Text = "Item Tracking Text Color";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Item Tracking Text Color";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 17);
            this.label5.TabIndex = 3;
            this.label5.Text = "Teleport Text Color";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Drop Text Color";
            // 
            // btnClrItemTxt
            // 
            this.btnClrItemTxt.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnClrItemTxt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClrItemTxt.Location = new System.Drawing.Point(224, 60);
            this.btnClrItemTxt.Name = "btnClrItemTxt";
            this.btnClrItemTxt.Size = new System.Drawing.Size(23, 23);
            this.btnClrItemTxt.TabIndex = 4;
            this.btnClrItemTxt.UseVisualStyleBackColor = false;
            this.btnClrItemTxt.Click += new System.EventHandler(this.ColorButtonClick);
            // 
            // btnClrTpTxt
            // 
            this.btnClrTpTxt.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnClrTpTxt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClrTpTxt.Location = new System.Drawing.Point(224, 89);
            this.btnClrTpTxt.Name = "btnClrTpTxt";
            this.btnClrTpTxt.Size = new System.Drawing.Size(23, 23);
            this.btnClrTpTxt.TabIndex = 4;
            this.btnClrTpTxt.UseVisualStyleBackColor = false;
            this.btnClrTpTxt.Click += new System.EventHandler(this.ColorButtonClick);
            // 
            // btnClrDrpTxt
            // 
            this.btnClrDrpTxt.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnClrDrpTxt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClrDrpTxt.Location = new System.Drawing.Point(224, 118);
            this.btnClrDrpTxt.Name = "btnClrDrpTxt";
            this.btnClrDrpTxt.Size = new System.Drawing.Size(23, 23);
            this.btnClrDrpTxt.TabIndex = 4;
            this.btnClrDrpTxt.UseVisualStyleBackColor = false;
            this.btnClrDrpTxt.Click += new System.EventHandler(this.ColorButtonClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "Levelup Text Color";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Levelup Text Color";
            // 
            // btnClrLvlupTxt
            // 
            this.btnClrLvlupTxt.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnClrLvlupTxt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClrLvlupTxt.Location = new System.Drawing.Point(224, 31);
            this.btnClrLvlupTxt.Name = "btnClrLvlupTxt";
            this.btnClrLvlupTxt.Size = new System.Drawing.Size(23, 23);
            this.btnClrLvlupTxt.TabIndex = 2;
            this.btnClrLvlupTxt.UseVisualStyleBackColor = false;
            this.btnClrLvlupTxt.Click += new System.EventHandler(this.ColorButtonClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.button2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(595, 90);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Location = new System.Drawing.Point(434, 33);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(23, 23);
            this.button2.TabIndex = 3;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.ColorButtonClick);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(291, 90);
            this.label1.TabIndex = 2;
            this.label1.Text = "BackgroundColor";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NotesComponentSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainTableLayout);
            this.Name = "NotesComponentSettings";
            this.Size = new System.Drawing.Size(601, 525);
            this.mainTableLayout.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.colorsGroupBox.ResumeLayout(false);
            this.colorsGroupBox.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainTableLayout;
        private System.Windows.Forms.GroupBox colorsGroupBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClrLvlupTxt;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnClrItemTxt;
        private System.Windows.Forms.Button btnClrDrpTxt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnClrTpTxt;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnClrItemBg;
        private System.Windows.Forms.Button btnClrTpBg;
        private System.Windows.Forms.Button btnClrDrpBg;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnClrLvlupBg;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnClrKllBg;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnClrKillTxt;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnClrTrphyBg;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnClrTrphyTxt;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnClrSkpBg;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnClrSkpTxt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox filePathTxtBox;
    }
}
