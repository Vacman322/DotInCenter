using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

static class Program
{
    static Form RedDot;

    [DllImport("user32.dll")]
    static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("user32.dll")]
    public static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    public static extern bool SetLayeredWindowAttributes(
      IntPtr hwnd,
      uint crKey,
      byte bAlpha,
      uint dwFlags);

    [STAThread]
    static void Main()
    {
        RedDot = new Form();
        RedDot.AutoSize = false;
        RedDot.BackColor = Color.Red;
        RedDot.FormBorderStyle = FormBorderStyle.None;
        RedDot.ShowInTaskbar = false;
        RedDot.Size = new Size(1, 1);
        //RedDot.StartPosition = FormStartPosition.CenterScreen;
        //RedDot.SetBounds(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2, 1, 1);
        
        RedDot.TopMost = true;
        RedDot.Shown += new EventHandler(RedDot_Shown);

        var initialStyle = GetWindowLong(RedDot.Handle, -20); //получаем старый стиль окна?
        SetWindowLong(RedDot.Handle, -20, (uint)initialStyle | 0x00080000 + 0x00000020); //добавляем к текущему окну стиль прозрачности?
        SetLayeredWindowAttributes(RedDot.Handle, 0U, byte.MaxValue, 2U);//ставим прозрачность хз как
        RedDot.Location = new Point(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2);
        Application.Run(RedDot);
    }

    static void RedDot_Shown(object sender, EventArgs e)
    {
        RedDot.Size = new Size(1, 1);
    }
}