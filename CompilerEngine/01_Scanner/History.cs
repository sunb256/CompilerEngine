using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerEngine
{
  [DebuggerDisplay("[History] : {this.Val.ToString()} : {this.Type}")]
  public class HistoryItem
  {
    public TokenType Type;
    public object Val;
  }


  public class History
  {
    public List<HistoryItem> items;

    public History()
    {
      items = new List<HistoryItem>();
    }

    public void Add(Scanner scanner)
    {
      var h = new HistoryItem() { Type = scanner.Type, Val = scanner.Val};
      items.Add(h);
    }

    public void Clear()
    {
      items.Clear();
    }

    public string Dump()
    {
      string s = "";
      int cnt = 1;
      foreach (var h in items)
      {
        s += String.Format("{0} :: [ {1} ] {2} \n", cnt.ToString("000"), h.Val.ToString(), h.Type.ToString());
        cnt++;
      }
      return s;
    }


  }
}
