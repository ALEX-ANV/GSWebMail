using System.Security.Cryptography.X509Certificates;

namespace Local.mail.core.LetterCore
{
    public interface ILetter
    {
        Letter CreateLetter();
    }
}