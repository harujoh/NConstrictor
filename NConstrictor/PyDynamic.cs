using System;
using System.Dynamic;

namespace NConstrictor
{
    public class PyDynamic : DynamicObject
    {
        private PyObject _pyObject;

        public PyDynamic(PyObject pyObject)
        {
            _pyObject = pyObject;
        }

        // メンバ呼び出し
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            PyObject tuppleArgs = PyTuple.New(args.Length);

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].GetType().IsArray)
                {
                    PyTuple.SetItem(tuppleArgs, i, (Array)args[i]);
                }
                else if (args[i] is PyArray<float> floatPyArray)
                {
                    PyTuple.SetItem(tuppleArgs, i, floatPyArray);
                }
                else if (args[i] is PyArray<double> doublePyArray)
                {
                    PyTuple.SetItem(tuppleArgs, i, doublePyArray);
                }
                else if (args[i] is PyArray<int> intPyArray)
                {
                    PyTuple.SetItem(tuppleArgs, i, intPyArray);
                }
                else
                {
                    PyTuple.SetItem(tuppleArgs, i, (PyObject)args[i]);
                }
            }

            result = PyObject.CallObject(_pyObject[binder.Name], tuppleArgs);

            return true;
        }

        // プロパティに値を設定しようとしたときに呼ばれる
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            Py.IncRef(_pyObject);

            if (value.GetType().IsArray)
            {
                _pyObject[binder.Name] = (Array)value;
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
            result = _pyObject[binder.Name];

            return true;
        }

        public static implicit operator PyObject(PyDynamic main)
        {
            return main._pyObject;
        }
    }
}
