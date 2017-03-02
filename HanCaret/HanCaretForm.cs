/*
HanCaret
Copyright 2017, Kim Jongkook d0nzs00n@gmail.com

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and 
associated documentation files (the "Software"), to deal in the Software without restriction, 
including without limitation the rights to use, copy, modify, merge, publish, distribute, 
sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is 
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or 
substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT 
NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, 
DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using JKCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace HanCaret
{
    public partial class HanCaretForm : Form
    {
        #region Variables
        private NotifyIcon m_trayIcon = new NotifyIcon();
        private ContextMenu m_trayMenu = new ContextMenu();
        private Point m_caretPos = new Point(); // caret position
        #endregion

        public HanCaretForm()
        {
            InitializeComponent();

            // create tray icon
            {
                m_trayMenu.MenuItems.Add("Exit", OnExit);
                m_trayIcon.ContextMenu = m_trayMenu;
                m_trayIcon.Visible = true;
                m_trayIcon.Icon = this.Icon;
                m_trayIcon.Text = Application.ProductName;
            }
        }

        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
        public enum DeviceCap
        {
            VERTRES = 10,
            DESKTOPVERTRES = 117,

            // http://pinvoke.net/default.aspx/gdi32/GetDeviceCaps.html
        }


        private float getScalingFactor()
        {
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();
            int LogicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.VERTRES);
            int PhysicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.DESKTOPVERTRES);

            float ScreenScalingFactor = (float)PhysicalScreenHeight / (float)LogicalScreenHeight;

            return ScreenScalingFactor; // 1.25 = 125%
        }

        private void M_timer_Tick(object sender, EventArgs e)
        {
            IntPtr hWnd = Win32.GetForegroundWindow();
            if (hWnd == this.Handle)
                return;

            // Get the caret position and handle
            Win32.GUITHREADINFO ti = new Win32.GUITHREADINFO();
            ti.cbSize = Marshal.SizeOf(ti);
            if(!Win32.GetGUIThreadInfo(0, ref ti))
            {
                Trace.WriteLine(string.Format("Failed to Win32.GetGUIThreadInfo. error={0}", Win32.GetLastError()));
                return;
            }

            // caret의 핸들이 확인되지 않으면 리턴.
            // 일반적인 GDI윈도우가 아닌 경우로 추후에는 관련 프로그램들로 처리할 수 있는 방법이 필요함.
            if (ti.hwndCaret == IntPtr.Zero)
            {
                this.Visible = false;
                return;
            }

            // caret의 LeftBottom 으로 화면 이동
            m_caretPos.X = (int)ti.rcCaret.Left;
            m_caretPos.Y = (int)ti.rcCaret.Bottom;
            
            // DPI설정상태에 따라 좌표 재계산
            float dpi = getScalingFactor();
            m_caretPos.X = (int)((float)m_caretPos.X / dpi);
            m_caretPos.Y = (int)((float)m_caretPos.Y / dpi);

            Win32.ClientToScreen(ti.hwndCaret, ref m_caretPos);
                        
            this.Left = m_caretPos.X;
            this.Top = m_caretPos.Y;

            // 한영상태 확인
            bool isHan = false;
            IME_Status.ImeStatus.GetStatus(ti.hwndCaret, ref isHan);

            m_label.Text = isHan ? "가" : "A";


            this.Width = 20;
            this.Height = 20;
            this.Visible = true;
        }

        private void OnExit(object sender, EventArgs e)
        {
            m_timer.Stop();
            Application.Exit();
        }

        private void HanCaretForm_Load(object sender, EventArgs e)
        {
            m_timer.Interval = 250;
            m_timer.Tick += M_timer_Tick;
            m_timer.Start();

            this.BackColor = Color.LimeGreen;

            this.Width = 0;
            this.Height = 0;

            this.BackColor = Color.DarkGray;
            this.TransparencyKey = Color.DarkGray;
        }
    }
}
