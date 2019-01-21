using System;
using System.Dynamic;

namespace NConstrictor
{
    public class PyDynamic : DynamicObject
    {
        private PyObject _main;

        public PyDynamic(PyObject pyObject)
        {
            _main = pyObject;
        }

        // メンバ呼び出し
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            PyObject tuppleArgs = PyTuple.New(args.Length);

            for (int i = 0; i < args.Length; i++)
            {
                PyTuple.SetItem(tuppleArgs, i, (PyObject)args[i]);
            }

            result = PyObject.CallObject(_main[binder.Name], tuppleArgs);
            return true;
        }

        // プロパティに値を設定しようとしたときに呼ばれる
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            Py.IncRef(_main);
            if (value.GetType().IsArray)
            {
                _main[binder.Name] = (Array)value;
            }
            else
            {
                _main[binder.Name] = (PyObject)value;
            }

            return true;
        }

        // プロパティから値を取得しようとしたときに呼ばれる
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            Py.IncRef(_main);
            result = _main[binder.Name];
            return true;
        }
    }
}
