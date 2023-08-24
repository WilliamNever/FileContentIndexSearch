using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchByIndex.Core.Interfaces
{
    public interface IForm
    {
        void SetParentIForm(IForm parent) { throw new NotImplementedException(); }
        void CleanMessages() { throw new NotImplementedException(); }
        void AcceptMessage(string message) { throw new NotImplementedException(); }
        void CancelWorking(string workName = "NoName") { throw new NotImplementedException(); }
    }
}
