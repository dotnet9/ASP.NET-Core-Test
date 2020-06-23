namespace RESTfullDemo.Helpers
{
    public class PropertyMapping
    {
        public PropertyMapping(string targetProperty,
            bool revert = false)
        {
            TargetProperty = targetProperty;
            IsRevert = revert;
        }

        public bool IsRevert { get; private set; }
        public string TargetProperty { get; private set; }
    }
}
