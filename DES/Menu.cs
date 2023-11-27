using System.Text.RegularExpressions;

namespace DES
{
    internal static class Menu
    {
        internal static void MenuRun()
        {
            Console.WriteLine("\t\tМЕНЮ");
            Console.WriteLine("1. Зашифровать");
            Console.WriteLine("2. Расшифровать");
            Console.WriteLine("3. Выход");
        }

        internal static void EncryptDES()
        {
            string s = "encrypt#>";

            string? consoleRead;
            do
            {
                Console.Write(s + "Введите фразу: ");
                consoleRead = Console.ReadLine() ?? "";
                Global.inputText = Regex.Replace(consoleRead, @"\s+", string.Empty);
            } while (Global.inputText == "");
            DES.CheckLength(ref Global.inputText); // проверяем длину текста (должен нацело делиться на размер блока в битах)
            Global.numberOfBlocks = Global.inputText.Length * Global.sizeOfChar / Global.sizeOfBlock;
            Global.binaryBlocksIP = new string[Global.numberOfBlocks];
            DES.GetBlocksFromString(Global.inputText); // делим текст на блоки
            Global.binaryBlocksIP = DES.InitialPermutation(Global.binaryBlocks); // начальная перестановки блоков
            //Array.Copy(Global.binaryBlocks, Global.binaryBlocksIP, Global.binaryBlocks.Length);

            do
            {
                Console.Write(s + "Ключевое слово: ");
                consoleRead = Console.ReadLine() ?? "";
                Global.inputKey = Regex.Replace(consoleRead, @"\s+", string.Empty);
            } while (Global.inputKey == "");
            DES.CheckLengthKey(ref Global.inputKey, Global.lengthOfBlock); // проверяем длину ключа
            Global.inputBinaryKey = DES.StringToBinaryFormat(Global.inputKey); // переводим ключ в двоичный формат

            global::DES.EncryptDES.Encrypt();
            Console.WriteLine(s + "Зашифрованное слово: " + Global.outputText);
        }

        internal static void DecryptDES()
        {
            string s = "decrypt#";
            global::DES.DecryptDES.Decrypt();
            Console.WriteLine(s + "Расшифрованное слово: " + Global.outputText);
        }
    }
}
