namespace SampleMqttClient
{
    partial class ControlLog
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TextBoxLog = new System.Windows.Forms.TextBox();
            this.ContextMenuLogClear = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ClearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuLogClear.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBoxLog
            // 
            this.TextBoxLog.ContextMenuStrip = this.ContextMenuLogClear;
            this.TextBoxLog.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TextBoxLog.Location = new System.Drawing.Point(-1, 0);
            this.TextBoxLog.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TextBoxLog.Multiline = true;
            this.TextBoxLog.Name = "TextBoxLog";
            this.TextBoxLog.ReadOnly = true;
            this.TextBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBoxLog.Size = new System.Drawing.Size(534, 128);
            this.TextBoxLog.TabIndex = 0;
            this.TextBoxLog.TabStop = false;
            this.TextBoxLog.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TextBoxLogTop_MouseDown);
            // 
            // ContextMenuLogClear
            // 
            this.ContextMenuLogClear.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ContextMenuLogClear.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClearToolStripMenuItem});
            this.ContextMenuLogClear.Name = "ContextMenuLogClear";
            this.ContextMenuLogClear.Size = new System.Drawing.Size(102, 26);
            // 
            // ClearToolStripMenuItem
            // 
            this.ClearToolStripMenuItem.Name = "ClearToolStripMenuItem";
            this.ClearToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.ClearToolStripMenuItem.Text = "Clear";
            this.ClearToolStripMenuItem.Click += new System.EventHandler(this.ClearToolStripMenuItem_Click);
            // 
            // ControlLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.TextBoxLog);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ControlLog";
            this.Size = new System.Drawing.Size(533, 128);
            this.ContextMenuLogClear.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextBoxLog;
        private System.Windows.Forms.ContextMenuStrip ContextMenuLogClear;
        private System.Windows.Forms.ToolStripMenuItem ClearToolStripMenuItem;
    }
}
