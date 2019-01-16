using System;
using System.Dynamic;

namespace NConstrictor
{
    public class PyValue : DynamicObject
    {
        private PyObject _pyObject;

        public PyValue(PyObject pyObject)
        {
            _pyObject = pyObject;
        }

        // メンバ呼び出し
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            PyObject tuppleArgs = PyTuple.New(args.Length);

            for (int i = 0; i < args.Length; i++)
            {
                PyTuple.SetItem(tuppleArgs, i, (PyObject)args[i]);
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
    }
}
