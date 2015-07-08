using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CompilerEngine;

namespace CompilerEngin_Example
{
  public partial class Form1 : Form
  {
    private Runnner _runner;

    public Form1()
    {
      InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      _runner = new Runnner();
      _runner.Run_With_Test();

      Application.Exit();
    }

  }
}
