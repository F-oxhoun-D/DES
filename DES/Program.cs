namespace DES
{
    internal class Program
    {
        enum MenuItems
        {
            a = 1, b, c, d, e
        };

        static bool Run = true;

        static void Main()
        {
            Global.SLists.Add(Global.S1); Global.SLists.Add(Global.S2); Global.SLists.Add(Global.S3); Global.SLists.Add(Global.S4);
            Global.SLists.Add(Global.S5); Global.SLists.Add(Global.S6); Global.SLists.Add(Global.S7); Global.SLists.Add(Global.S8);

            Console.Title = "DES";
            Console.ForegroundColor = ConsoleColor.Green;
            Menu.MenuRun();

            int command;
            while (Run)
            {
                Console.Write("Выберите пункт меню: ");
                while (!int.TryParse(Console.ReadLine(), out command))
                {
                    Console.Write("Введите число: ");
                }

                switch (command)
                {
                    case (int)(MenuItems.a):
                        Menu.EncryptDES();
                        break;
                    case (int)(MenuItems.b):
                        Menu.DecryptDES();
                        return;
                    default:
                        Run = false;
                        break;
                }
            }
        }
    }
}