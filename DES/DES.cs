using System.Text;


namespace DES
{
    internal static class DES
    {
        public static string F(string R, string K) // функция шифрования
        {
            R = ExtensionFunction(R); // функция расширения 
            string RK = XOR(R, K);    // операция исключающего ИЛИ
            RK = S(RK);               // функция преобразования
            RK = p(RK);               // функция конечной перестановки
            return RK;
        }

        public static string h(string C, string D) // функция получения ключа
        {
            int size = 48;
            string cd = C + D;
            char[] CD0 = cd.ToCharArray();
            char[] CD = new char[size];
            for (int i = 0; i < size; i++)
                CD[i] = CD0[Global.H[i] - 1];

            return string.Join("", CD);
        }

        public static string S(string R) // функция преобразования
        {
            int size = R.Length / 8;
            int count = R.Length / 6;
            string RS = string.Empty;
            for (int i = 0; i < count; i++)
            {
                string str = R.Substring(i * size, size);
                string row = Convert.ToString(str[0]) + Convert.ToString(str[5]);
                int r = Convert.ToInt32(row, 2);
                string column = Convert.ToString(str[1]) + Convert.ToString(str[2]) + Convert.ToString(str[3]) + Convert.ToString(str[4]);
                int c = Convert.ToInt32(column, 2);
                int rc = Global.SLists[i][r, c];
                string rs = Convert.ToString(rc, 2).PadLeft(4, '0');
                RS += rs;
            }
            return RS;
        }

        public static string p(string R) // функция перестановки P
        {
            char[] r = R.ToCharArray();
            char[] p = new char[r.Length];
            for (int i = 0; i < p.Length; i++)
                p[i] = r[Global.P[i] - 1];
            return string.Join("", p);
        }

        public static string LeftShift(string key, in int count)
        {
            if (count == 1)
            {
                key += "0";
            }
            else
            {
                key += "00";
            }
            return key.Remove(0, count);
        }

        public static string ExtensionFunction(string R) // ф-ция расширения
        {
            int size = 48;
            char[] charBinaryR = new char[size];
            for (int i = 0; i < size; i++)
                charBinaryR[i] = R[Global.E[i] - 1];

            return String.Join("", charBinaryR);
        }

        public static string XOR(string L, string f) // операция логического исключающего ИЛИ
        {
            string result = "";

            for (int i = 0; i < L.Length; i++)
            {
                bool a = Convert.ToBoolean(Convert.ToInt32(L[i].ToString()));
                bool b = Convert.ToBoolean(Convert.ToInt32(f[i].ToString()));

                if (a ^ b)
                    result += "1";
                else
                    result += "0";
            }
            return result;
        }

        public static string[] InitialPermutation(string[] binaryBlocks) // перестановка
        {
            string[] binaryBlocksIP = new string[binaryBlocks.Length];
            for (int i = 0; i < Global.numberOfBlocks; i++)
            {
                char[] charBinaryArray = binaryBlocks[i].ToCharArray();
                char[] charBinaryArrayIP = new char[Global.sizeOfBlock];

                for (int j = 0; j < Global.sizeOfBlock; j++)
                    charBinaryArrayIP[j] = charBinaryArray[Global.IP[j] - 1];

                binaryBlocksIP[i] = string.Join("", charBinaryArrayIP);
            }
            return binaryBlocksIP;
        }

        public static string[] InitialPermutation1(string[] binaryBlocksIP) // перестановка
        {
            for (int i = 0; i < Global.numberOfBlocks; i++)
            {
                char[] charBinaryArray = binaryBlocksIP[i].ToCharArray();
                char[] charBinaryArrayIP = new char[Global.sizeOfBlock];

                for (int j = 0; j < Global.sizeOfBlock; j++)
                    charBinaryArrayIP[j] = charBinaryArray[Global.IP1[j] - 1];

                binaryBlocksIP[i] = string.Join("", charBinaryArrayIP);
            }
            return binaryBlocksIP;
        }

        public static string InitialKeyPreparation(string Key) // начальная подготовка ключа (из 64-х битного получаем 56-ти битный)
        {
            int size = 56;
            char[] k = Key.ToCharArray();
            char[] gk = new char[size];
            for (int i = 0; i < size; i++)
                gk[i] = k[Global.G[i] - 1];

            return string.Join("", gk);
        }

        public static void GetBlocksFromString(in string text) // получаем блоки в двоичном формате из исходного текста
        {
            Global.Blocks = new string[text.Length * Global.sizeOfChar / Global.sizeOfBlock];
            Global.binaryBlocks = new string[text.Length * Global.sizeOfChar / Global.sizeOfBlock];

            for (int i = 0; i < Global.Blocks.Length; i++)
            {
                Global.Blocks[i] = text.Substring(i * Global.lengthOfBlock, Global.lengthOfBlock);
                Global.binaryBlocks[i] = StringToBinaryFormat(Global.Blocks[i]);
            }
        }

        public static string StringToBinaryFormat(string text) // перевод строки в двоичный формат
        {
            string outText = "";
            foreach (byte b in Encoding.ASCII.GetBytes(text))
            {
                string charBinary = Convert.ToString(b, 2).PadLeft(8, '0');
                outText += charBinary;
            }
            return outText;
        }

        public static string BinaryToStringFormat(string text) // перевод строки в двоичный формат
        {
            int numOfBytes = text.Length / 8;
            byte[] bytes = new byte[numOfBytes];
            for (int i = 0; i < numOfBytes; ++i)
            {
                bytes[i] = Convert.ToByte(text.Substring(8 * i, 8), 2);
            }
            return Encoding.ASCII.GetString(bytes);
        }

        public static void CheckLength(ref string text) // проверяем длину
        {
            char[] chars = @"$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&".ToCharArray();
            Random rnd = new();
            while ((text.Length * Global.sizeOfChar) % Global.sizeOfBlock != 0)
                text += chars[rnd.Next(0, chars.Length - 1)];
        }

        public static void CheckLengthKey(ref string key, int lengthKey) // проверяем длину ключа (должна равняться 64 битам)
        {
            char[] chars = @"$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&".ToCharArray();
            Random rnd = new();
            if (key.Length > lengthKey) // если больше - вырежем нужное
                key = key[..lengthKey];
            else                        // если меньше - добавим
                while (key.Length < lengthKey)
                    key += chars[rnd.Next(0, chars.Length - 1)];
        }
    }
}
