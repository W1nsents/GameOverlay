using OverlayGameNet.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverlayGameNet
{
    public partial class OverlayForm : Form
    {
        public OverlayForm()
        {
            InitializeComponent();
        }


        EzzOverlay ez = new EzzOverlay();
        private void OverlayForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            ez.SetInvi(this);
            ez.StartLoop(10, Configuration.WINDOW_NAME, this);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;
                return handleParam;
            }
        }
    }
}
