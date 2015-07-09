using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CompilerEngine;


namespace CompilerEngin_Example
{
  public class Runnner
  {
    private string _GetSource(int selector)
    {
      string ret = "";

      if (selector == 0)
      {
        ret = @"1234";
      }
      else if (selector == 1)
      {
        ret = @"1234 5678";
      }
      else if (selector == 2)
      {
        ret = @"1+2;";
      }
      else if (selector == 3)
      {
        ret = @"-1+2;";
      }
      else if (selector == 4)
      {
        ret = @"1-2;";
      }
      else if (selector == 5)
      {
        ret = @"x = 34; y = x * x;";
      }
      else if (selector == 6)
      {
        ret = @"
x = 13; 
// test
y = x * x;
";
      }
      else if (selector == 7)
      {
        ret = @"
x = 13; 
/* -----------------
/* -test
/* ----------------- */
y = x * x;
";
      }
      else if (selector == 8)
      {
        ret = @"
s1 = ""Hello "";
s2 = ""World!"";
s1 + s2;
";
      }
      else if (selector == 9)
      {
        ret = @"
1==2;
1==1;
";
      }
      else if (selector == 10)
      {
        ret = @"
s = 10;
s==10;
1==1;
";
      }
      else if (selector == 11)
      {
        ret = @"
if(1 < 2)
  ""less""
else
  ""greater""
";
      }
      else if (selector == 12)
      {
        ret = @"
x = 0;
while(x < 5)
  x = x + 1;
";
      }
      else if (selector == 13)
      {
        ret = @"
x = 1;
y = 0;
while(x <= 10){
  y = y + x;
  x = x + 1;
};
y;
";
      }
      else
      {
        return "";
      }
      return ret;
    }

    public bool Run_With_Test()
    {
      Console.WriteLine("");
      //var items = new int[] { 0, 1, 2, 3, 4 };
      var items = new int[] {13};

      foreach (var s in items)
      {
        Console.WriteLine("[TEST NUMBER - " + s.ToString() + "]");

        var src = _GetSource(s);  // プログラム入力
        var ret = Run(src);
        if (!ret) { 
          Console.WriteLine("NG >>> " + s.ToString());
          return false;
        }
      }
      return true;
    }


    public bool Run(string src)
    {
      string ret = "";
      try
      {
        ret = Engine.Run(src);
      }
      catch (Exception ex)
      {
        Console.WriteLine("[Result] NG >>> " + ex.Message);
        return false;
      }

      int cnt = 1;
      Console.WriteLine("------------------------------");
      Console.WriteLine(Engine.Yacc.History.Dump());

      foreach(var s in ret.TrimEnd('\n').Split('\n').ToList()){
        Console.WriteLine(String.Format("[Result {0}] :: {1}", cnt.ToString("000"), s));
        cnt++;
      }
      Console.WriteLine("------------------------------");

      return true;
    }


  }
}
