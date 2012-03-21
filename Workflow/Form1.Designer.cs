namespace Workflow
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.programmeList = new System.Windows.Forms.ListBox();
            this.todoList = new System.Windows.Forms.ListBox();
            this.startProgram = new System.Windows.Forms.Button();
            this.pursueProgram = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // programmeList
            // 
            this.programmeList.FormattingEnabled = true;
            this.programmeList.Location = new System.Drawing.Point(18, 37);
            this.programmeList.Name = "programmeList";
            this.programmeList.Size = new System.Drawing.Size(188, 264);
            this.programmeList.TabIndex = 0;
            // 
            // todoList
            // 
            this.todoList.FormattingEnabled = true;
            this.todoList.Location = new System.Drawing.Point(322, 37);
            this.todoList.Name = "todoList";
            this.todoList.Size = new System.Drawing.Size(192, 264);
            this.todoList.TabIndex = 1;
            // 
            // startProgram
            // 
            this.startProgram.Location = new System.Drawing.Point(18, 307);
            this.startProgram.Name = "startProgram";
            this.startProgram.Size = new System.Drawing.Size(188, 37);
            this.startProgram.TabIndex = 2;
            this.startProgram.Text = "Programm Starten";
            this.startProgram.UseVisualStyleBackColor = true;
            this.startProgram.Click += new System.EventHandler(this.startProgram_Click);
            // 
            // pursueProgram
            // 
            this.pursueProgram.Location = new System.Drawing.Point(322, 307);
            this.pursueProgram.Name = "pursueProgram";
            this.pursueProgram.Size = new System.Drawing.Size(192, 36);
            this.pursueProgram.TabIndex = 3;
            this.pursueProgram.Text = "Programm weiterführen";
            this.pursueProgram.UseVisualStyleBackColor = true;
            this.pursueProgram.Click += new System.EventHandler(this.pursueProgram_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 422);
            this.Controls.Add(this.pursueProgram);
            this.Controls.Add(this.startProgram);
            this.Controls.Add(this.todoList);
            this.Controls.Add(this.programmeList);
            this.Name = "Form1";
            this.Text = "Workflow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox programmeList;
        private System.Windows.Forms.ListBox todoList;
        private System.Windows.Forms.Button startProgram;
        private System.Windows.Forms.Button pursueProgram;
    }
}

