using WHMS.Core.Contracts;

namespace WHMS.Core
{
    public class Engine : IEngine
    {
        private IInputProcessor processor;
        private IReader reader;
        private IWriter writer;

        public Engine(
            IInputProcessor inputProcessor,
            IReader reader,
            IWriter writer)
        {

            this.processor = inputProcessor;
            this.reader = reader;
            this.writer = writer;
        }

        public void Start()
        {
            while (true)
            {
                var input = this.reader.ReadLine();
                var output = this.processor.Process(input);
                this.writer.WriteLine(output);
            }
        }
    }
}
