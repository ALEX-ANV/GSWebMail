using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Local.mail.core.LetterCore.Task
{
    public class LetterCn : ManagmentMail, ILetter
    {
        public LetterCn()
        {
            InstanceType = "letterCN";
        }

        public override string InstanceType { get; }

        Letter ILetter.CreateLetter()
        {
            throw new NotImplementedException();
        }

        public override string ToStringLetter()
        {
            throw new NotImplementedException();
        }
    }
}
