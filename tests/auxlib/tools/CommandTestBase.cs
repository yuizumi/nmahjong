using NUnit.Framework;
using System.IO;
using NMahjong.Testing;

namespace NMahjong.Aux.Tools
{
    public abstract class CommandTestBase : TestHelper
    {
        private StringWriter mErrorOutput;

        [SetUp]
        public void SetupErrorOutput()
        {
            mErrorOutput = new StringWriter();
            Command.ErrorOutput = mErrorOutput;
        }

        protected string ErrorOutput
        {
            get { return mErrorOutput.ToString(); }
        }
    }
}
