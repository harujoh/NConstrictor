namespace NConstrictor
{
    public class PyDynamicModule : PyDynamic
    {
        public PyDynamicModule(string name) : base(PyImport.Import(name))
        {
        }
    }
}