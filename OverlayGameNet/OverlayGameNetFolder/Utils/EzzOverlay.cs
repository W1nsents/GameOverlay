using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using static OverlayGameNet.Utils.DllImport;
using static OverlayGameNet.Utils.Configuration;

namespace OverlayGameNet.Utils
{
    class EzzOverlay
    {
        public void setHandle(string window_name)
        {
            hand = FindWindow(null, window_name);

        }

        public void SetInvi(Form form)
        {
            form.BackColor = Color.Wheat;
            form.TransparencyKey = Color.Wheat;
            form.TopMost = true;
            form.FormBorderStyle = FormBorderStyle.None;
            ClickThrough(form.Handle);
        }

        public void GetRekt()
        {
            GetWindowRect(hand, out rect);
        }

        public void ClickThrough(IntPtr formHandle)
        {
            int initialStyle = GetWindowLong(formHandle, -20);
            SetWindowLong(formHandle, -20, initialStyle | 0x8000 | 0x20);
        }
        public Size CalcSize()
        {
            Size size = new Size(rect.right - rect.left, rect.bottom - rect.top);
            return size;
        }

        public void DoStuff(string WindowName, Form form)
        {
            setHandle(WindowName);
            GetRekt();
            form.Size = CalcSize();
            form.Left = rect.left;
            form.Top = rect.top;
        }

        public void PauseLoop()
        {
            threadwire = false;
        }

        public void UnPauseLoop()
        {
            threadwire = true;
        }
        public void StartLoop(int frequency, string WindowName, Form form)
        {
            Thread lp = new Thread(() => LOOP(frequency, WindowName, form)) { IsBackground = true };
            lp.Start();

        }


        public void LOOP(int frequency, string WindowName, Form form)
        {
            while (true)
            {
                if (threadwire == true)
                {
                    DoStuff(WindowName, form);

                }
                Thread.Sleep(frequency);

            }

        }
    }
}
