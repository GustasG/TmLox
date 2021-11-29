namespace TmLox.Interpreter
{
    internal interface ILexer
    {
        Token? Current { get; }

        Token? Next();

        void Reset();

        bool Finished();
    }
}