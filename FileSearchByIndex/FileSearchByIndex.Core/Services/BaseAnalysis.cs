using FileSearchByIndex.Core.Consts;
using System.Text;
using System.Text.RegularExpressions;

namespace FileSearchByIndex.Core.Services
{
    public class BaseAnalysis<T> : BaseService<T> where T : class
    {
        protected Regex StarEmptyChar { get => new(@"^[\s]+"); }
        protected Regex EmptyChars { get => new(@"[\s]+"); }
        protected Regex LineWrap { get => new($"({EnviConst.NewLine})"); }
        protected Encoding FileEncoding { get; private set; } = Encoding.UTF8;
        public void InitCharEncoding(string charCodeing)
        {
            try
            {
                var enc = Encoding.GetEncoding(charCodeing);
                FileEncoding = enc;
            } catch(Exception ex) {
                _log.Error($"Failed to create Encoding with the name '{charCodeing}'", ex);
            }
        }
    }
}
