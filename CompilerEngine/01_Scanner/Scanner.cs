using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CompilerEngine
{
  public class Scanner
  {
    public TokenType Type { get; private set; }
    public Object Val { get; private set; }
    public Char Char { get { return this._reader.Result; } }

    private static CodeDomProvider _p;
    private SourceReader _reader;

    private static string R_IF = "if";
    private static string R_ELSE = "else";
    private static string R_TRUE = "true";
    private static string R_FALSE = "false";
    private static string R_WHILE = "while";

    public enum ScanResult
    {
      OK,
      EOF,
      ERROR
    }

    public Scanner()
    {
      _p = CodeDomProvider.CreateProvider("C#");
    }

    public Scanner(string source)
    {
      _p = CodeDomProvider.CreateProvider("C#");
      _reader = new SourceReader(source);
    }

    public void SetSource(string source)
    {
      _reader = new SourceReader(source);
    }


    public ScanResult Next()
    {
      try
      {
        _reader.SkipSpace();

        var c = _reader.Read();
        if (c < 0)
          return ScanResult.EOF;

        Char ch = (char)c;

        switch (ch)
        {
          case ';':
            this.Type = TokenType.EOS;
            this.Val = this.Char.ToString();
            break;

          case '(':
            this.Type = TokenType.APER;
            this.Val = this.Char.ToString();
            break;

          case ')':
            this.Type = TokenType.OPER;
            this.Val = this.Char.ToString();
            break;

          case '{':
            this.Type = TokenType.BLOCK_APER;
            this.Val = this.Char.ToString();
            break;

          case '}':
            this.Type = TokenType.BLOCK_OPER;
            this.Val = this.Char.ToString();
            break;

          case ',':
            this.Type = TokenType.COMMA;
            this.Val = this.Char.ToString();
            break;

          case '=':

            this.Val = (string)ScanEqual(ch);

            // == の場合
            if ((string)this.Val == "==")
            {
              this.Type = TokenType.EQ;
            }
            // = の場合
            else
            {
              _reader.UnRead();
              this.Type = TokenType.EQUAL;
            }
            break;

          case '!': 

            this.Val = (string)ScanExmark(ch);

            // != の場合
            if ((string)this.Val == "!=")
            {
              this.Type = TokenType.EQ;
            }
            // ! の場合
            else
            {
              _reader.UnRead();
              this.Type = TokenType.EQUAL;
            }
            break;

          case '<':

            this.Val = (string)ScanLess(ch);

            // <= の場合
            if ((string)this.Val == "<=")
            {
              this.Type = TokenType.LE;
            }
            // < の場合
            else
            {
              _reader.UnRead();
              this.Type = TokenType.LT;
            }
            break;

          case '>':

            this.Val = (string)ScanGreater(ch);

            // >= の場合
            if ((string)this.Val == ">=")
            {
              this.Type = TokenType.GE;
            }
            // > の場合
            else
            {
              _reader.UnRead();
              this.Type = TokenType.GT;
            }
            break;

          case '&':

            this.Val = (string)ScanAndOp(ch);

            // && の場合
            if ((string)this.Val == "&&")
            {
              this.Type = TokenType.AND;
            }
            else
            {
              throw new Exception("scanner error :: not && (and operator)");
            }
            break;

          case '|':

            this.Val = (string)ScanOrOp(ch);

            // || の場合
            if ((string)this.Val == "||")
            {
              this.Type = TokenType.OR;
            }
            else
            {
              throw new Exception("scanner error :: not || (or operator)");
            }
            break;


          case '+':
            this.Type = TokenType.PLUS;
            this.Val = this.Char.ToString();
            break;

          case '-':
            this.Type = TokenType.MINUS;
            this.Val = this.Char.ToString();
            break;

          case '*':
            this.Type = TokenType.MULTI;
            this.Val = this.Char.ToString();
            break;

          case '"':
            this.Type = TokenType.STRING;
            this.Val = ScanString();
            break;

          case '/':

            c = _reader.Read();
            if (c == '/')
            {
              SkipLineComment();
              return this.Next();
            }
            else if (c == '*')
            {
              SkipMultiComment();
              return this.Next();
            }
            else
            {
              this.Type = TokenType.DIVIDE;
              this.Val = this.Char.ToString();
              break;
            }

          // number
          default:

            if (_reader.IsEof)
            {
              this.Type = TokenType.EOF;
              this.Val = @"\n";
              return ScanResult.EOF;
            }

            if (Char.IsDigit(ch))
            {
              _reader.UnRead();
              this.Type = TokenType.INT;
              this.Val = ScanDigit();
            }
            else if (isSymbol(ch.ToString()))
            {
              _reader.UnRead();
              this.Val = ScanSymbol();

              // 予約語token
              if ((string)this.Val == R_TRUE)
              {
                this.Type = TokenType.TRUE;
              }
              else if ((string)this.Val == R_FALSE)
              {
                this.Type = TokenType.FALSE;
              }
              else if ((string)this.Val == R_IF)
              {
                this.Type = TokenType.IF;
              }
              else if ((string)this.Val == R_ELSE)
              {
                this.Type = TokenType.ELSE;
              }
              else if ((string)this.Val == R_WHILE)
              {
                this.Type = TokenType.WHILE;
              }
              else
              {
                this.Type = TokenType.VALIABLE;
              }

            }
            else
            {
              throw new Exception("scanner error :: not symbol or digit");
            }
            break;
        }

      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return ScanResult.ERROR;
      }
      return ScanResult.OK;
    }


    // コメント
    private void SkipLineComment()
    {
      Char c = ' ';
      while (c != '\n')
      {
        if (!_reader.IsEof)
        {
          c = (char)_reader.Read();
        }
        else
        {
          throw new Exception("scanner error :: comment is eof");
        }
      }
    }

    // 複数コメント
    private void SkipMultiComment()
    {
      Char c = ' ';
      while (true)
      {
        c = (char)_reader.Read();

        if (_reader.IsEof)
        {
          throw new Exception("scanner error :: comment is eof");
        }
        else if (c == '*')
        {
          c = (char)_reader.Read();
          if (c == '/')
          {
            break;
          }
        }
      }
    }


    // 文字列を評価
    private string ScanString()
    {
      string str = "";
      Char c = ' ';

      while (true)
      {
        c = (char)_reader.Read();
        str += c.ToString();

        if (_reader.IsEof)
        {
          throw new Exception("scanner error :: comment is eof");
        }
        else if (c == '"')
        {
          c = (char)_reader.Read();

          if (str.Length > 0)
            str = str.Substring(0, str.Length - 1);

          _reader.UnRead();
          return str;
        }
        else if (c == '\\')
        {
          c = (char)_reader.Read();
          str += c.ToString();

          if (_reader.IsEof)
          {
            throw new Exception("scanner error :: comment is eof");
          }
        }
      }
    }


    // 数字を評価
    private int ScanDigit()
    {
      int num = 0;

      while (true)
      {
        _reader.SkipSpace();

        var c = _reader.Read();
        if (c < 0)
          break;

        if (!Char.IsDigit((char)c))
        {
          _reader.UnRead();
          break;
        }

        num = (num * 10) + (c - '0');
      }

      return (int)num;
    }

    // シンボルを評価
    private string ScanSymbol()
    {
      string str = "";
      Char c = ' ';

      while (true)
      {
        c = (char)_reader.Read();
        str += c.ToString();

        if (_reader.IsEof)
        {
          throw new Exception("scanner error :: comment is eof");
        }

        string tmp = c.ToString();
        if (!ValidSymbol(tmp))
        {
          _reader.UnRead();

          if (str.Length >0)
            str = str.Substring(0, str.Length - 1);

          break;
        }
      }



      return str;
    }


    // = 演算子を評価
    private string ScanEqual(char currentValue)
    {
      return _ScanOperator(currentValue);
    }

    // ! 演算子を評価
    private string ScanExmark(char currentValue)
    {
      return _ScanOperator(currentValue);
    }

    // < 演算子を評価
    private string ScanLess(char currentValue)
    {
      return _ScanOperator(currentValue);
    }

    // > 演算子を評価
    private string ScanGreater(char currentValue)
    {
      return _ScanOperator(currentValue);
    }

    // && 演算子を評価
    private string ScanAndOp(char currentValue)
    {
      return _ScanOperator(currentValue);
    }

    // || 演算子を評価
    private string ScanOrOp(char currentValue)
    {
      return _ScanOperator(currentValue);
    }

    // ! 演算子を評価
    private string _ScanOperator(char currentValue)
    {
      string str = currentValue.ToString();
      char c = (char)_reader.Read();

      str += c;

      return str;
    }


    /// ------------------------------------------------------
    /// <summary>識別子が正しいか調べる</summary>
    /// ------------------------------------------------------
    public bool isSymbol(string str)
    {
      if (_p.IsValidIdentifier(str))
        return true;
      else
        return false;
    }

    public bool ValidSymbol(string str)
    {
      var m = new Regex("[a-zA-Z0-9]").Match(str);
      if (m.Success)
        return true;
      else
        return false;
    }

  }



}
