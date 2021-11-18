namespace TmLox.Errors
{
    public class ValueError: LoxError
    {
        public ValueError()
        {

        }

        public ValueError(string message)
            : base(message)
        {
        }

        public override string ToString()
        {
            return Message;
        }
    }
}
