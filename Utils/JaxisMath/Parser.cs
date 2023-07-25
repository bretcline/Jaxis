using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using Jaxis.Util.Log4Net;

namespace JaxisMath
{

//        MathFunctions.MathParser mp = new MathFunctions.MathParser();
//        private void btnCalculateBasicFormula_Click(object sender, EventArgs e)
//        {
//            lblBasicFormulaResult.Text = mp.Calculate(txtBasicFormula.Text).ToString();
//        }
//
//        private void btnCalculateParametricFormula_Click(object sender, EventArgs e)
//        {
//            MathFunctions.MathParser mp = new MathFunctions.MathParser();
//            mp.Parameters.Add(MathFunctions.Parameters.A, 5);
//            mp.Parameters.Add(MathFunctions.Parameters.B, 2);
//            mp.Parameters.Add(MathFunctions.Parameters.C, 1);
//            mp.Parameters.Add(MathFunctions.Parameters.D, 3);
//            decimal result = mp.Calculate("3D-2B/C+(A-B)");
//        }

    public class Parser
    {
        public enum Parameters
        {
            A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z
        }
        public class MathParser
        {
            public Dictionary<Parameters, decimal> m_Parameters = new Dictionary<Parameters, decimal>();
            private List<String> m_OperationOrder = new List<string>();

            //public Dictionary<Parameters, decimal> m_Parameters
            //{
            //    get { return m_Parameters; }
            //    set { m_Parameters = value; }
            //}

            public MathParser()
            {
                m_OperationOrder.Add("/");
                m_OperationOrder.Add("*");
                m_OperationOrder.Add("-");
                m_OperationOrder.Add("+");
            }
            public decimal Calculate(string _Formula)
            {
                try
                {
                    string[] arr = _Formula.Split("/+-*()".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    foreach (KeyValuePair<Parameters, decimal> de in m_Parameters)
                    {
                        foreach (string s in arr)
                        {
                            if (s != de.Key.ToString() && s.EndsWith(de.Key.ToString()))
                            {
                                _Formula = _Formula.Replace(s, (Convert.ToDecimal(s.Replace(de.Key.ToString(), "")) * de.Value).ToString());
                            }
                        }
                        _Formula = _Formula.Replace(de.Key.ToString(), de.Value.ToString());
                    }
                    while (_Formula.LastIndexOf("(") > -1)
                    {
                        int lastOpenPhrantesisIndex = _Formula.LastIndexOf("(");
                        int firstClosePhrantesisIndexAfterLastOpened = _Formula.IndexOf(")", lastOpenPhrantesisIndex);
                        decimal result = ProcessOperation(_Formula.Substring(lastOpenPhrantesisIndex + 1, firstClosePhrantesisIndexAfterLastOpened - lastOpenPhrantesisIndex - 1));
                        bool AppendAsterix = false;
                        if (lastOpenPhrantesisIndex > 0)
                        {
                            if (_Formula.Substring(lastOpenPhrantesisIndex - 1, 1) != "(" && !m_OperationOrder.Contains(_Formula.Substring(lastOpenPhrantesisIndex - 1, 1)))
                            {
                                AppendAsterix = true;
                            }
                        }

                        _Formula = _Formula.Substring(0, lastOpenPhrantesisIndex) + (AppendAsterix ? "*" : "") + result.ToString() + _Formula.Substring(firstClosePhrantesisIndexAfterLastOpened + 1);

                    }
                }
                catch (Exception exp)
                {
                    Log.WriteException("Parser::Calculate", exp);
                }
                return ProcessOperation(_Formula);
            }

            private decimal ProcessOperation(string _operation)
            {
                ArrayList arr = new ArrayList();
                string s = "";
                try
                {
                    for (int i = 0; i < _operation.Length; i++)
                    {
                        string currentCharacter = _operation[i].ToString( );//.Substring(i, 1);
                        if (m_OperationOrder.IndexOf(currentCharacter) > -1)
                        {
                            if (s != "")
                            {
                                arr.Add(s);
                            }
                            arr.Add(currentCharacter);
                            s = "";
                        }
                        else
                        {
                            s += currentCharacter;
                        }
                    }
                    arr.Add(s);
                    s = "";
                    foreach (string op in m_OperationOrder)
                    {
                        while (arr.IndexOf(op) > -1)
                        {
                            int operatorIndex = arr.IndexOf(op);
                            decimal digitBeforeOperator = 0;
                            decimal digitAfterOperator = 0;
                            if( 0 < operatorIndex )
                            {
                                digitBeforeOperator = Convert.ToDecimal(arr[operatorIndex - 1]);
                            }
                            else
                            {
                                Debug.WriteLine( "operatorIndex is 0" );
                            }
                            if (arr[operatorIndex + 1].ToString() == "-")
                            {
                                arr.RemoveAt(operatorIndex + 1);
                                digitAfterOperator = Convert.ToDecimal(arr[operatorIndex + 1]) * -1;
                            }
                            else
                            {
                                digitAfterOperator = Convert.ToDecimal(arr[operatorIndex + 1]);
                            }
                            arr[operatorIndex] = CalculateByOperator(digitBeforeOperator, digitAfterOperator, op);
                            if( 0 < operatorIndex )
                            {
                                arr.RemoveAt(operatorIndex - 1);
                            }
                            arr.RemoveAt(operatorIndex);
                        }
                    }
                }
                catch (Exception exp)
                {
                    Log.WriteException("Parser::ProcessOperation", exp);
                }
                return Convert.ToDecimal(arr[0]);
            }

            private decimal CalculateByOperator(decimal _number1, decimal _number2, string _op)
            {
                if (_op == "/")
                {
                    return _number1 / _number2;
                }
                else if (_op == "*")
                {
                    return _number1 * _number2;
                }
                else if (_op == "-")
                {
                    return _number1 - _number2;
                }
                else if (_op == "+")
                {
                    return _number1 + _number2;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
