using System.Drawing;
using System.Windows.Forms;

namespace VoiceToPaint
{
    partial class Canvas
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // textBox1
            // 
            this.textBox1.AccessibleDescription = "Blue";
            this.textBox1.Location = new System.Drawing.Point(377, 542);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // listView1
            // 
            this.listView1.AutoArrange = false;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Right;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(583, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1, 562);
            this.listView1.SmallImageList = this.imgList;
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // imgList
            // 
            this.imgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imgList.ImageSize = new System.Drawing.Size(1, 20);
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.richTextBox1.Location = new System.Drawing.Point(483, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(100, 562);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "Draw, type, color, point, size, rotation \\n \\nDelete, object number, \\n \\nClear \\" +
    "n \\nRotate, object number";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::VoiceToPaint.Properties.Resources.voice_Icon;
            this.pictureBox1.Location = new System.Drawing.Point(351, 542);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            this.pictureBox1.VisibleChanged += new System.EventHandler(this.pictureBox1_VisibleChanged);
            // 
            // Canvas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(584, 562);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.textBox1);
            this.Name = "Canvas";
            this.Text = "Painting Window";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Canvas_FormClosing);
            this.Load += new System.EventHandler(this.Canvas_Load);
            this.Shown += new System.EventHandler(this.Canvas_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.z);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.Canvas_Layout);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.Resize += new System.EventHandler(this.Canvas_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListView listView1;
        private ImageList imgList;
        private RichTextBox richTextBox1;
        private PictureBox pictureBox1;
    }
}

