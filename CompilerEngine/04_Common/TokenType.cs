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

    /// <summary> = </summary>
    EQUAL,

    /// <summary> Valiable </summary>
    VALIABLE,

    /// <summary> true </summary>
    TRUE,

    /// <summary> false </summary>
    FALSE,

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

    /// <summary> / </summary>
    EQUAL,

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
