using System;
using System.Collections.Generic;
using System.Dynamic;

namespace NConstrictor
{
    public class PyDynamic : DynamicObject
    {
        public PyObject _pyObject;

        public PyDynamic(PyObject pyObject)
        {
            _pyObject = pyObject;
        }

        // メンバ呼び出し
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            List<PyObject> pyObjects = new List<PyObject>();

            if (args.Length == 0)//引数なし
            {
                result = _pyObject[binder.Name].Call();
            }
            else
            {
                bool hasDict = args[args.Length - 1] is PyDict;
                int argOffset = hasDict ? 1 : 0;

                for (int i = 0; i < args.Length - argOffset; i++)
                {
                    if (args[i] is IntPtr intptr)
                    {
                        pyObjects.Add(intptr);
                    }
                    else if (args[i] is int[] intArray)
                    {
                        pyObjects.Add((PyArray<int>)intArray);
                    }
                    else if (args[i] is long[] longArray)
                    {
                        pyObjects.Add((PyArray<long>)longArray);
                    }
                    else if (args[i] is float[] floatArray)
                    {
                        pyObjects.Add((PyArray<float>)floatArray);
                    }
                    else if (args[i] is double[] doubleArray)
                    {
                        pyObjects.Add((PyArray<double>)doubleArray);
                    }
                    else if (args[i] is byte[] byteArray)
                    {
                        pyObjects.Add((PyArray<byte>)byteArray);
                    }
                    else if (args[i] is Dictionary<PyObject, PyObject> dictionary)
                    {
                        pyObjects.Add((PyDict)dictionary);
                    }
                    else if (args[i] is PyArray<float> floatPyArray)
                    {
                        pyObjects.Add(floatPyArray);
                    }
                    else if (args[i] is PyArray<double> doublePyArray)
                    {
                        pyObjects.Add(doublePyArray);
                    }
                    else if (args[i] is PyArray<int> intPyArray)
                    {
                        pyObjects.Add(intPyArray);
                    }
                    else if (args[i] is PyArray<byte> bytePyArray)
                    {
                        pyObjects.Add(bytePyArray);
                    }
                    else if (args[i] is PyList pyList)
                    {
                        pyObjects.Add(pyList);
                    }
                    else if (args[i] is PyDict)
                    {
                        throw new Exception("引数の順番が間違っています");
                    }
                    else
                    {
                        pyObjects.Add((PyObject)args[i]);
                    }
                }

                PyTuple tuppleArgs = PyTuple.New(pyObjects.Count);
                for (int i = 0; i < pyObjects.Count; i++)
                {
                    tuppleArgs[i] = pyObjects[i];
                }

                //最後の引数が辞書か？
                if (hasDict)
                {
                    //辞書以外の引数があるか？
                    if(args.Length > 1)
                    {
                        //引数+辞書
                        result = _pyObject[binder.Name].Call(tuppleArgs, (PyDict)args[args.Length - 1]);
                    }
                    else
                    {
                        //辞書のみ
                        result = _pyObject[binder.Name].Call((PyDict)args[args.Length - 1]);
                    }
                }
                else
                {
                    //辞書なし
                    result = PyObject.CallObject(_pyObject[binder.Name], tuppleArgs);
                }
            }

            return true;
        }

        // プロパティに値を設定しようとしたときに呼ばれる
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            Py.IncRef(_pyObject);

            if (value is int intValue)
            {
                _pyObject[binder.Name] = intValue;
            }
            else if (value is float floatValue)
            {
                _pyObject[binder.Name] = floatValue;
            }
            else if (value is double doubleValue)
            {
                _pyObject[binder.Name] = doubleValue;
            }
            else if (value is long longValue)
            {
                _pyObject[binder.Name] = longValue;
            }
            else if (value is bool boolValue)
            {
                _pyObject[binder.Name] = boolValue;
            }
            else if (value is int[] intArray)
            {
                _pyObject[binder.Name] = (PyArray<int>)intArray;
            }
            else if (value is long[] longArray)
            {
                _pyObject[binder.Name] = (PyArray<long>)longArray;
            }
            else if (value is float[] floatArray)
            {
                _pyObject[binder.Name] = (PyArray<float>)floatArray;
            }
            else if (value is double[] doubleArray)
            {
                _pyObject[binder.Name] = (PyArray<double>)doubleArray;
            }
            else if (value is PyArray<float> floatPyArray)
            {
                _pyObject[binder.Name] = floatPyArray;
            }
            else if (value is PyArray<double> doublePyArray)
            {
                _pyObject[binder.Name] = doublePyArray;
            }
            else if (value is PyArray<int> intPyArray)
            {
                _pyObject[binder.Name] = intPyArray;
            }
            else if (value is PyArray<long> longPyArray)
            {
                _pyObject[binder.Name] = longPyArray;
            }
            else
            {
                _pyObject[binder.Name] = (PyObject)value;
            }

            return true;
        }

        // プロパティから値を取得しようとしたときに呼ばれる
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            Py.IncRef(_pyObject);
            result = new PyDynamic(_pyObject[binder.Name]);

            return true;
        }

        public static implicit operator PyDynamic(PyObject pyObject)
        {
            return new PyDynamic(pyObject);
        }

        public static implicit operator PyObject(PyDynamic main)
        {
            return main._pyObject;
        }

        public override string ToString()
        {
            return _pyObject.ToString();
        }
    }
}
