namespace TmLox
{
    public interface ILexer
    {
        Token? Current { get; }

        Token? Next();

        void Reset();

        bool Finished();
    }
}