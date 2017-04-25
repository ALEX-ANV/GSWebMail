namespace Local.mail.core.LetterCore
{
    public abstract class ManagmentMail : Mail
    {
        public string H1 { get; set; }

        public string H2 { get; set; }

        public string H4 { get; set; }

        public abstract string ToStringLetter();
    }
}
