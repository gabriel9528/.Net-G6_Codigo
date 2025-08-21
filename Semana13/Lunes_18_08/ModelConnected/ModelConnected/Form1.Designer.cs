namespace ModelConnected
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            textBoxAirline = new TextBox();
            textBoxDestination = new TextBox();
            textBoxFlightNumber = new TextBox();
            comboBoxAirplaneType = new ComboBox();
            buttonAdd = new Button();
            comboBoxSelect = new ComboBox();
            buttonUpdate = new Button();
            buttonDelete = new Button();
            dataGridView1 = new DataGridView();
            buttonRefresh = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(52, 44);
            label1.Name = "label1";
            label1.Size = new Size(52, 20);
            label1.TabIndex = 0;
            label1.Text = "Airline";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(52, 175);
            label2.Name = "label2";
            label2.Size = new Size(100, 20);
            label2.TabIndex = 1;
            label2.Text = "Airplane Type";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(52, 129);
            label3.Name = "label3";
            label3.Size = new Size(85, 20);
            label3.TabIndex = 2;
            label3.Text = "Destination";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(52, 86);
            label4.Name = "label4";
            label4.Size = new Size(104, 20);
            label4.TabIndex = 3;
            label4.Text = "Flight Number";
            // 
            // textBoxAirline
            // 
            textBoxAirline.Location = new Point(189, 37);
            textBoxAirline.Name = "textBoxAirline";
            textBoxAirline.Size = new Size(262, 27);
            textBoxAirline.TabIndex = 4;
            // 
            // textBoxDestination
            // 
            textBoxDestination.Location = new Point(189, 125);
            textBoxDestination.Name = "textBoxDestination";
            textBoxDestination.Size = new Size(262, 27);
            textBoxDestination.TabIndex = 6;
            // 
            // textBoxFlightNumber
            // 
            textBoxFlightNumber.Location = new Point(189, 83);
            textBoxFlightNumber.Name = "textBoxFlightNumber";
            textBoxFlightNumber.Size = new Size(262, 27);
            textBoxFlightNumber.TabIndex = 7;
            // 
            // comboBoxAirplaneType
            // 
            comboBoxAirplaneType.FormattingEnabled = true;
            comboBoxAirplaneType.Items.AddRange(new object[] { "AirBus 320", "Boeing 777", "Boeing 737", "Boeing 747", "Boeing 787" });
            comboBoxAirplaneType.Location = new Point(189, 175);
            comboBoxAirplaneType.Name = "comboBoxAirplaneType";
            comboBoxAirplaneType.Size = new Size(258, 28);
            comboBoxAirplaneType.TabIndex = 8;
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new Point(568, 36);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(253, 29);
            buttonAdd.TabIndex = 9;
            buttonAdd.Text = "Add";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // comboBoxSelect
            // 
            comboBoxSelect.FormattingEnabled = true;
            comboBoxSelect.Location = new Point(570, 96);
            comboBoxSelect.Name = "comboBoxSelect";
            comboBoxSelect.Size = new Size(251, 28);
            comboBoxSelect.TabIndex = 10;
            comboBoxSelect.SelectedIndexChanged += comboBoxSelect_SelectedIndexChanged;
            // 
            // buttonUpdate
            // 
            buttonUpdate.Location = new Point(534, 174);
            buttonUpdate.Name = "buttonUpdate";
            buttonUpdate.Size = new Size(136, 29);
            buttonUpdate.TabIndex = 11;
            buttonUpdate.Text = "Update";
            buttonUpdate.UseVisualStyleBackColor = true;
            // 
            // buttonDelete
            // 
            buttonDelete.Location = new Point(718, 175);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(136, 29);
            buttonDelete.TabIndex = 12;
            buttonDelete.Text = "Delete";
            buttonDelete.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(52, 244);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(802, 238);
            dataGridView1.TabIndex = 13;
            // 
            // buttonRefresh
            // 
            buttonRefresh.Location = new Point(718, 527);
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new Size(136, 29);
            buttonRefresh.TabIndex = 14;
            buttonRefresh.Text = "Refresh";
            buttonRefresh.UseVisualStyleBackColor = true;
            buttonRefresh.Click += buttonRefresh_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(906, 578);
            Controls.Add(buttonRefresh);
            Controls.Add(dataGridView1);
            Controls.Add(buttonDelete);
            Controls.Add(buttonUpdate);
            Controls.Add(comboBoxSelect);
            Controls.Add(buttonAdd);
            Controls.Add(comboBoxAirplaneType);
            Controls.Add(textBoxFlightNumber);
            Controls.Add(textBoxDestination);
            Controls.Add(textBoxAirline);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Flights";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textBoxAirline;
        private TextBox textBoxDestination;
        private TextBox textBoxFlightNumber;
        private ComboBox comboBoxAirplaneType;
        private Button buttonAdd;
        private ComboBox comboBoxSelect;
        private Button buttonUpdate;
        private Button buttonDelete;
        private DataGridView dataGridView1;
        private Button buttonRefresh;
    }
}
