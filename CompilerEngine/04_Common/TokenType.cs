using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerEngine
{
  public enum TokenType
  {
    /// <summary>int</summary>
    INT,

    /// <summary>string</summary>
    STRING,

    /// <summary> = </summary>
    EQUAL,

    /// <summary> == </summary>
    EQ,

    /// <summary> ! (exclamation mark) </summary>
    EX,

    /// <summary> != </summary>
    NE,

    /// <summary> ＜ </summary>
    LT,

    /// <summary> ＜= </summary>
    LE,

    /// <summary> ＞ </summary>
    GT,

    /// <summary> ＞= </summary>
    GE,

    /// <summary> && </summary>
    AND,

    /// <summary> || </summary>
    OR,

    /// <summary> + </summary>
    PLUS,
    
    /// <summary> - </summary>
    MINUS,
    
    /// <summary> * </summary>
    MULTI,
    
    /// <summary> / </summary>
    DIVIDE,

    /// <summary>End Of Sentence</summary>
    EOS,

    /// <summary>End Of File</summary>
    EOF,

    /// <summary> ( </summary>
    APER,

    /// <summary> ) </summary>
    OPER,



    /// <summary> Valiable </summary>
    VALIABLE,

    /// <summary> true </summary>
    TRUE,

    /// <summary> false </summary>
    FALSE,


    /// <summary> if </summary>
    IF,

    /// <summary> else </summary>
    ELSE,

    /// <summary>NULL</summary>
    NULL,
  }

  public enum Operator
  {
    /// <summary> + </summary>
    PLUS,

    /// <summary> - </summary>
    MINUS,

    /// <summary> * </summary>
    MULTI,

    /// <summary> / </summary>
    DIVIDE,

    /// <summary> = </summary>
    EQUAL,

    /// <summary> == </summary>
    EQ,

    /// <summary> ! (exclamation mark) </summary>
    EX,

    /// <summary> != </summary>
    NE,

    /// <summary> ＜ </summary>
    LT,

    /// <summary> ＜= </summary>
    LE,

    /// <summary> ＞ </summary>
    GT,

    /// <summary> ＞= </summary>
    GE,

    /// <summary> && </summary>
    AND,

    /// <summary> || </summary>
    OR,

    /// <summary> none operator </summary>
    NULL,
  }

  public enum SymbolType
  {
    /// <summary> Valiable </summary>
    VALIABLE,

    /// <summary> Function </summary>
    FUNCTION,

    /// <summary> User Function </summary>
    USER_FUNCTION,
  }




}
