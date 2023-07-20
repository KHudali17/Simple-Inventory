namespace SimpleInv
{
    internal class Invoker : IInvoker
    {
        private readonly Dictionary<int, ICommand> commands;
        /// <summary>
        /// Retrieve command associated with choice and attempt to execute.
        /// </summary>
        /// <param name="choice"></param>
        public void Invoke(int choice)
        {
            if (commands.TryGetValue(choice, out ICommand command))
            {
                command.Execute();
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }
        /// <summary>
        /// Define a mapping between a chioce option and its relevant 
        /// executable command.
        /// </summary>
        /// <param name="inventory"></param>
        public Invoker(IInventory inventory)
        {
            commands = new Dictionary<int, ICommand>
            {
                { 1, new AddProductCommand(inventory) }
            };
        }
    }
}