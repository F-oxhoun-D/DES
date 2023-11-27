namespace DES
{
    internal class EncryptDES
    {
        public static void Encrypt()
        {
            string GK = DES.InitialKeyPreparation(Global.inputBinaryKey); // начальная подготовка ключа (из 64 - х битного получаем 56 - ти битный)
            for (int i = 0; i < Global.numberOfBlocks; i++)
            {
                string[] L = new string[Global.quantityOfRounds + 1]; // массивы, которы хранят левую и правую части блока
                string[] R = new string[Global.quantityOfRounds + 1]; // соответственно
                for (int j = 0; j <= Global.quantityOfRounds; j++)
                {
                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            L[j] = Global.binaryBlocksIP[i][..(Global.sizeOfBlock / 2)]; // разделяем блок пополам
                            R[j] = Global.binaryBlocksIP[i].Substring(Global.sizeOfBlock / 2, Global.sizeOfBlock / 2); // ...
                            Global.C[j] = GK[..(GK.Length / 2)]; // разделяем ключ пополам
                            Global.D[j] = GK.Substring(GK.Length / 2, GK.Length / 2); // ...
                            Global.K[j] = "";
                        }
                        else
                        {
                            L[j] = R[j - 1];
                            if (j == 1 || j == 2 || j == 9 || j == 16)
                            {
                                Global.C[j] = DES.LeftShift(Global.C[j - 1], 1);
                                Global.D[j] = DES.LeftShift(Global.D[j - 1], 1);
                            }
                            else
                            {
                                Global.C[j] = DES.LeftShift(Global.C[j - 1], 2);
                                Global.D[j] = DES.LeftShift(Global.D[j - 1], 2);
                            }
                            Global.K[j] = DES.h(Global.C[j], Global.D[j]);
                            R[j] = DES.XOR(L[j - 1], DES.F(R[j - 1], Global.K[j]));
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            L[j] = Global.binaryBlocksIP[i][..(Global.sizeOfBlock / 2)];
                            R[j] = Global.binaryBlocksIP[i].Substring(Global.sizeOfBlock / 2, Global.sizeOfBlock / 2);
                        }
                        else
                        {
                            L[j] = R[j - 1];
                            Global.K[j] = DES.h(Global.C[j], Global.D[j]);
                            R[j] = DES.XOR(L[j - 1], DES.F(R[j - 1], Global.K[j]));
                        }
                    }
                }
                Global.binaryBlocksIP[i] = R[^1] + L[^1];
            }
            Global.binaryBlocksIP = DES.InitialPermutation1(Global.binaryBlocksIP);
            foreach (var i in Global.binaryBlocksIP)
                Global.outputText += DES.BinaryToStringFormat(i);
        }
    }
}
