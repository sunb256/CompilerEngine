using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerEngine
{
  public class SourceReader
  {
    public string Source { get; private set; }
    public int Idx { get; set; }

    public char Result { get { return _read_result; } }
    private char _read_result;

    public bool IsEof
    {
      get
      {
        if (this.Source.Length <= Idx)
          return true;

        return false;
      }
    }



    public SourceReader(string source)
    {
      this.Source = source;
      Idx = 0;
    }

    public void SkipSpace()
    {
      if (this.Source.Length <= Idx)
        return;

      var s = Read();

      while (s != -1 && Char.IsWhiteSpace((char)s))
      {
        s = Read();
      }

      UnRead();
    }

    public int Read()
    {
      if (this.Source.Length <= Idx)
        return -1;

      var s = _Read();

      Idx++;
      _read_result = Convert.ToChar(s);
      return (int)Convert.ToChar(s);
    }

    private string _Read()
    {
      return this.Source.Substring(Idx, 1);
    }

    public void UnRead()
    {
      Idx--;
    }

    public int GetPos()
    {
      return Idx;
    }

  }
}
