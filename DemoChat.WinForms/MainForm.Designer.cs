namespace DemoChat.WinForms
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chatTextBox = new System.Windows.Forms.TextBox();
            this.newMessageTextBox = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.loadHistoryButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.createOwnServerButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ownServerPortTextBox = new System.Windows.Forms.TextBox();
            this.ownServerIpTextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.connectToServerButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.outServerPortTextBox = new System.Windows.Forms.TextBox();
            this.outServerIpTextBox = new System.Windows.Forms.TextBox();
            this.resetConnectionButton = new System.Windows.Forms.Button();
            this.logFilePathTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.clearChatButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // chatTextBox
            // 
            this.chatTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chatTextBox.Location = new System.Drawing.Point(12, 144);
            this.chatTextBox.Multiline = true;
            this.chatTextBox.Name = "chatTextBox";
            this.chatTextBox.ReadOnly = true;
            this.chatTextBox.Size = new System.Drawing.Size(800, 362);
            this.chatTextBox.TabIndex = 0;
            // 
            // newMessageTextBox
            // 
            this.newMessageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.newMessageTextBox.Location = new System.Drawing.Point(12, 512);
            this.newMessageTextBox.Name = "newMessageTextBox";
            this.newMessageTextBox.Size = new System.Drawing.Size(800, 20);
            this.newMessageTextBox.TabIndex = 1;
            // 
            // sendButton
            // 
            this.sendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sendButton.Location = new System.Drawing.Point(658, 538);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(154, 23);
            this.sendButton.TabIndex = 2;
            this.sendButton.Text = "Отправить сообщение";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // loadHistoryButton
            // 
            this.loadHistoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.loadHistoryButton.Location = new System.Drawing.Point(491, 538);
            this.loadHistoryButton.Name = "loadHistoryButton";
            this.loadHistoryButton.Size = new System.Drawing.Size(161, 23);
            this.loadHistoryButton.TabIndex = 3;
            this.loadHistoryButton.Text = "Загрузить историю";
            this.loadHistoryButton.UseVisualStyleBackColor = true;
            this.loadHistoryButton.Click += new System.EventHandler(this.loadHistoryButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.logFilePathTextBox);
            this.groupBox1.Controls.Add(this.createOwnServerButton);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.ownServerPortTextBox);
            this.groupBox1.Controls.Add(this.ownServerIpTextBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 126);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Создать сервер";
            // 
            // createOwnServerButton
            // 
            this.createOwnServerButton.Location = new System.Drawing.Point(194, 97);
            this.createOwnServerButton.Name = "createOwnServerButton";
            this.createOwnServerButton.Size = new System.Drawing.Size(100, 23);
            this.createOwnServerButton.TabIndex = 8;
            this.createOwnServerButton.Text = "Создать";
            this.createOwnServerButton.UseVisualStyleBackColor = true;
            this.createOwnServerButton.Click += new System.EventHandler(this.createOwnServerButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Порт";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "IP";
            // 
            // ownServerPortTextBox
            // 
            this.ownServerPortTextBox.Location = new System.Drawing.Point(76, 45);
            this.ownServerPortTextBox.Name = "ownServerPortTextBox";
            this.ownServerPortTextBox.Size = new System.Drawing.Size(218, 20);
            this.ownServerPortTextBox.TabIndex = 5;
            this.ownServerPortTextBox.Text = "8080";
            // 
            // ownServerIpTextBox
            // 
            this.ownServerIpTextBox.Location = new System.Drawing.Point(76, 19);
            this.ownServerIpTextBox.Name = "ownServerIpTextBox";
            this.ownServerIpTextBox.Size = new System.Drawing.Size(218, 20);
            this.ownServerIpTextBox.TabIndex = 4;
            this.ownServerIpTextBox.Text = "127.0.0.1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.connectToServerButton);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.outServerPortTextBox);
            this.groupBox2.Controls.Add(this.outServerIpTextBox);
            this.groupBox2.Location = new System.Drawing.Point(318, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(155, 126);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Подключиться к серверу";
            // 
            // connectToServerButton
            // 
            this.connectToServerButton.Location = new System.Drawing.Point(44, 97);
            this.connectToServerButton.Name = "connectToServerButton";
            this.connectToServerButton.Size = new System.Drawing.Size(100, 23);
            this.connectToServerButton.TabIndex = 4;
            this.connectToServerButton.Text = "Подключиться";
            this.connectToServerButton.UseVisualStyleBackColor = true;
            this.connectToServerButton.Click += new System.EventHandler(this.connectToServerButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Порт";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "IP";
            // 
            // outServerPortTextBox
            // 
            this.outServerPortTextBox.Location = new System.Drawing.Point(44, 45);
            this.outServerPortTextBox.Name = "outServerPortTextBox";
            this.outServerPortTextBox.Size = new System.Drawing.Size(100, 20);
            this.outServerPortTextBox.TabIndex = 1;
            this.outServerPortTextBox.Text = "8080";
            // 
            // outServerIpTextBox
            // 
            this.outServerIpTextBox.Location = new System.Drawing.Point(44, 19);
            this.outServerIpTextBox.Name = "outServerIpTextBox";
            this.outServerIpTextBox.Size = new System.Drawing.Size(100, 20);
            this.outServerIpTextBox.TabIndex = 0;
            this.outServerIpTextBox.Text = "127.0.0.1";
            // 
            // resetConnectionButton
            // 
            this.resetConnectionButton.Location = new System.Drawing.Point(479, 12);
            this.resetConnectionButton.Name = "resetConnectionButton";
            this.resetConnectionButton.Size = new System.Drawing.Size(123, 23);
            this.resetConnectionButton.TabIndex = 6;
            this.resetConnectionButton.Text = "Сброс подключения";
            this.resetConnectionButton.UseVisualStyleBackColor = true;
            this.resetConnectionButton.Click += new System.EventHandler(this.resetConnectionButton_Click);
            // 
            // logFilePathTextBox
            // 
            this.logFilePathTextBox.Location = new System.Drawing.Point(76, 71);
            this.logFilePathTextBox.Name = "logFilePathTextBox";
            this.logFilePathTextBox.Size = new System.Drawing.Size(218, 20);
            this.logFilePathTextBox.TabIndex = 9;
            this.logFilePathTextBox.Text = "chat.log";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Файл лога";
            // 
            // clearChatButton
            // 
            this.clearChatButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearChatButton.Location = new System.Drawing.Point(12, 538);
            this.clearChatButton.Name = "clearChatButton";
            this.clearChatButton.Size = new System.Drawing.Size(161, 23);
            this.clearChatButton.TabIndex = 7;
            this.clearChatButton.Text = "Очистить окно";
            this.clearChatButton.UseVisualStyleBackColor = true;
            this.clearChatButton.Click += new System.EventHandler(this.clearChatButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 573);
            this.Controls.Add(this.clearChatButton);
            this.Controls.Add(this.resetConnectionButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.loadHistoryButton);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.newMessageTextBox);
            this.Controls.Add(this.chatTextBox);
            this.Name = "MainForm";
            this.Text = "Чат";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox chatTextBox;
        private System.Windows.Forms.TextBox newMessageTextBox;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Button loadHistoryButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button createOwnServerButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ownServerPortTextBox;
        private System.Windows.Forms.TextBox ownServerIpTextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button connectToServerButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox outServerPortTextBox;
        private System.Windows.Forms.TextBox outServerIpTextBox;
        private System.Windows.Forms.Button resetConnectionButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox logFilePathTextBox;
        private System.Windows.Forms.Button clearChatButton;
    }
}

