namespace ProductionASP.Exceptions
{
    public class ModelException : Exception
    {
        public ModelException(string fieldName, string message)
            : base(message)
        {
            FieldName = fieldName;
        }

        public string FieldName { get; set; }
    }
}
